// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SonezakiMasaki.Exceptions;
using SonezakiMasaki.IO;
using SonezakiMasaki.SerializableValues;
using SonezakiMasaki.TypeSignatures;

namespace SonezakiMasaki
{
    public sealed class TypeManager
    {
        const uint MaxBuiltInTypeId = 999;
        readonly MultiKeyDictionary<uint, Type, RegisteredType> _registeredTypes = new MultiKeyDictionary<uint, Type, RegisteredType>();
        uint _nextBuiltInType = 1;
        uint _nextProprietaryType = MaxBuiltInTypeId + 1;
        uint _arrayId;

        public TypeManager()
        {
            RegisterBuiltInTypes();
            RegisterSpecialTypes();
        }

        public void RegisterType<T>()
            where T : new()
        {
            if ( _registeredTypes.Contains( typeof( T ) ) )
            {
                throw new InvalidOperationException( $"The type {typeof( T )} has already been registered." );
            }

            if ( IsSpecialRegisteredType( typeof( T ) ) )
            {
                throw new InvalidOperationException( $"The type {typeof( T )} is a specially registered type that you don't need to register yourself." );
            }

            GetInstantiatorAndWrapper<T>( out ValueInstantiator instantiator, out ValueWrapper wrapper );
            RegisterTypeInternal( ref _nextProprietaryType, typeof( T ), StandardTypeSignature.Create, instantiator, wrapper );
        }

        internal ITypeSignature ResolveTypeSignature( uint typeId )
        {
            if ( !_registeredTypes.TryGetValue( typeId, out RegisteredType registeredType ) )
            {
                throw new UnrecognizedTypeException( typeId );
            }

            return registeredType.TypeSignature;
        }

        internal ITypeSignature ResolveTypeSignature( Type type )
        {
            RegisteredType registeredType = GetRegisteredType( type );
            return registeredType.TypeSignature;
        }

        internal ISerializableValue Instantiate( Type type, SonezakiReader reader )
        {
            RegisteredType registeredType = GetRegisteredType( type );
            ISerializableValue value = registeredType.Instantiator( this, type, reader );
            return value;
        }

        internal ISerializableValue WrapRawValue( Type type, object value )
        {
            RegisteredType registeredType = GetRegisteredType( type );
            ISerializableValue serializableValue = registeredType.Wrapper( this, type, value );
            return serializableValue;
        }

        static bool IsSpecialRegisteredType( Type baseType )
        {
            return baseType.IsArray;
        }

        static void GetInstantiatorAndWrapper<T>( out ValueInstantiator instantiator, out ValueWrapper wrapper )
        {
            if ( typeof( T ).IsEnum )
            {
                instantiator = EnumValue.CreateInstantiator( typeof( T ) );
                wrapper = EnumValue.CreateWrapper( typeof( T ) );
                return;
            }

            instantiator = ReflectedClassValue<T>.Instantiate;
            wrapper = ReflectedClassValue<T>.WrapRawValue;
        }

        RegisteredType GetRegisteredType( Type type )
        {
            Type baseType = type;
            if ( baseType.IsGenericType )
            {
                baseType = type.GetGenericTypeDefinition();
            }

            if ( TryGetSpecialRegisteredType( baseType, out RegisteredType registeredType ) )
            {
                return registeredType;
            }

            if ( !_registeredTypes.TryGetValue( baseType, out registeredType ) )
            {
                throw new UninstantiatableTypeException( baseType, type );
            }

            return registeredType;
        }

        bool TryGetSpecialRegisteredType( Type baseType, out RegisteredType registeredType )
        {
            if ( baseType.IsArray )
            {
                return _registeredTypes.TryGetValue( _arrayId, out registeredType );
            }

            registeredType = null;
            return false;
        }

        void RegisterBuiltInTypes()
        {
            RegisterBuiltInValueType( StandardReadWriteOperations.Boolean );
            RegisterBuiltInValueType( StandardReadWriteOperations.Byte );
            RegisterBuiltInValueType( StandardReadWriteOperations.SByte );
            RegisterBuiltInValueType( StandardReadWriteOperations.Char );
            RegisterBuiltInValueType( StandardReadWriteOperations.Decimal );
            RegisterBuiltInValueType( StandardReadWriteOperations.Double );
            RegisterBuiltInValueType( StandardReadWriteOperations.Single );
            RegisterBuiltInValueType( StandardReadWriteOperations.Int32 );
            RegisterBuiltInValueType( StandardReadWriteOperations.UInt32 );
            RegisterBuiltInValueType( StandardReadWriteOperations.Int64 );
            RegisterBuiltInValueType( StandardReadWriteOperations.UInt64 );
            RegisterBuiltInValueType( StandardReadWriteOperations.Int16 );
            RegisterBuiltInValueType( StandardReadWriteOperations.UInt16 );
            RegisterBuiltInValueType( StandardReadWriteOperations.String );

            RegisterBuiltInType( typeof( List<> ), StandardTypeSignature.Create, ListValue.Instantiate, ListValue.WrapRawValue );
            RegisterBuiltInType( typeof( Nullable<> ), StandardTypeSignature.Create, NullableValue.Instantiate, NullableValue.WrapRawValue );
        }

        void RegisterSpecialTypes()
        {
            // Special type for ALL arrays! We just need *A* type here, so we'll use object[]. We ignore this value otherwise.
            _arrayId = RegisterBuiltInType( typeof( object[] ), ArrayTypeSignature.Create, ArrayValue.Instantiate, ArrayValue.WrapRawValue );
        }

        void RegisterBuiltInValueType<T>( ReadWriteOperations<T> operations )
        {
            ValueInstantiator instantiator = BuiltInValue<T>.CreateInstantiator( operations );
            ValueWrapper wrapper = BuiltInValue<T>.CreateWrapper( operations );
            RegisterBuiltInType( typeof( T ), StandardTypeSignature.Create, instantiator, wrapper );
        }

        uint RegisterBuiltInType( Type type, TypeSignatureCreator signatureCreator, ValueInstantiator instantiator, ValueWrapper wrapper )
        {
            if ( _nextBuiltInType > MaxBuiltInTypeId )
            {
                throw new InvalidOperationException();
            }

            return RegisterTypeInternal( ref _nextBuiltInType, type, signatureCreator, instantiator, wrapper );
        }

        uint RegisterTypeInternal( ref uint nextIdVariable, Type type, TypeSignatureCreator signatureCreator, ValueInstantiator instantiator, ValueWrapper wrapper )
        {
            RegisteredType registeredType = new RegisteredType( nextIdVariable, type, signatureCreator, instantiator, wrapper );
            _registeredTypes.Add( registeredType );
            nextIdVariable++;
            return registeredType.Id;
        }
    }
}
