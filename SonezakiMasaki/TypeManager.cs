// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using SonezakiMasaki.Exceptions;
using SonezakiMasaki.IO;
using SonezakiMasaki.SerializableValues;

namespace SonezakiMasaki
{
    public sealed class TypeManager
    {
        const uint MaxBuiltInTypeId = 999;
        readonly MultiKeyDictionary<uint, Type, RegisteredType> _registeredTypes = new MultiKeyDictionary<uint, Type, RegisteredType>();
        uint _nextBuiltInType = 1;
        uint _nextProprietaryType = MaxBuiltInTypeId + 1;

        public TypeManager()
        {
            RegisterBuiltInTypes();
        }

        public void RegisterType<T>()
        {
            if ( _registeredTypes.Contains( typeof( T ) ) )
            {
                throw new InvalidOperationException( $"The type {typeof( T )} has already been registered." );
            }

            RegisterTypeInternal( ref _nextProprietaryType, typeof( T ), ReflectedClassValue<T>.Instantiate, ReflectedClassValue<T>.WrapRawValue );
        }

        internal Type ResolveType( uint typeId )
        {
            if ( !_registeredTypes.TryGetValue( typeId, out RegisteredType registeredType ) )
            {
                throw new UnrecognizedTypeException( typeId );
            }

            return registeredType.Type;
        }

        internal RegisteredType GetRegisteredType( Type type )
        {
            Type baseType = type;
            if ( baseType.IsGenericType )
            {
                baseType = type.GetGenericTypeDefinition();
            }

            if ( !_registeredTypes.TryGetValue( baseType, out RegisteredType registeredType ) )
            {
                throw new UninstantiatableTypeException( baseType, type );
            }

            return registeredType;
        }

        internal ISerializableValue Instantiate( Type type, BinaryReader reader )
        {
            RegisteredType registeredType = GetRegisteredType( type );
            ISerializableValue value = registeredType.Instantiate( type, reader );
            return value;
        }

        internal ISerializableValue WrapRawValue( Type type, object value )
        {
            RegisteredType registeredType = GetRegisteredType( type );
            ISerializableValue serializableValue = registeredType.Wrap( value );
            return serializableValue;
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

            RegisterBuiltInType( typeof( List<> ), ListValue.Instantiate, ListValue.WrapRawValue );
        }

        void RegisterBuiltInValueType<T>( ReadWriteOperations<T> operations )
        {
            ValueInstantiator instantiator = BuiltInValue<T>.CreateInstantiator( operations );
            ValueWrapper wrapper = BuiltInValue<T>.CreateWrapper( operations );
            RegisterBuiltInType( typeof( T ), instantiator, wrapper );
        }

        void RegisterBuiltInType( Type type, ValueInstantiator instantiator, ValueWrapper wrapper )
        {
            if ( _nextBuiltInType > MaxBuiltInTypeId )
            {
                throw new InvalidOperationException();
            }

            RegisterTypeInternal( ref _nextBuiltInType, type, instantiator, wrapper );
        }

        void RegisterTypeInternal( ref uint nextIdVariable, Type type, ValueInstantiator instantiator, ValueWrapper wrapper )
        {
            RegisteredType registeredType = new RegisteredType( this, nextIdVariable, type, instantiator, wrapper );
            _registeredTypes.Add( registeredType );
            nextIdVariable++;
        }
    }
}
