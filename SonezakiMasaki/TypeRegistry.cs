// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SonezakiMasaki.IO;
using SonezakiMasaki.Markup;
using SonezakiMasaki.SerializableValues;
using SonezakiMasaki.TypeSignatures;

namespace SonezakiMasaki
{
    public class TypeRegistry
    {
        const uint MaxBuiltInTypeId = 999;
        readonly MultiKeyDictionary<uint, Type, RegisteredType> _registeredTypes = new MultiKeyDictionary<uint, Type, RegisteredType>();
        uint _nextBuiltInType = 1;
        uint _nextProprietaryType = MaxBuiltInTypeId + 1;

        public TypeRegistry()
        {
            RegisterBuiltInTypes();
            RegisterSpecialTypes();
        }

        enum RegisterTypeReturnCode
        {
            DoesNotHaveBinaryDataType,
            TypeAlreadyRegistered,
            InvalidTypeToRegister,
            Success
        }

        internal uint ArrayId { get; private set; }

        public void RegisterType( Type type )
        {
            RegisterTypeReturnCode returnCode = RegisterTypeInternal( type );
            switch ( returnCode )
            {
                case RegisterTypeReturnCode.DoesNotHaveBinaryDataType:
                    throw new InvalidOperationException( $"The type {type} is not marked up with {nameof( BinaryDataTypeAttribute )}." );
                case RegisterTypeReturnCode.TypeAlreadyRegistered:
                    throw new InvalidOperationException( $"The type {type} has already been registered." );
                case RegisterTypeReturnCode.InvalidTypeToRegister:
                    throw new InvalidOperationException( $"The type {type} is invalid to register." );
                case RegisterTypeReturnCode.Success:
                    return;
                default:
                    throw new NotImplementedException();
            }
        }

        public bool IsTypeRegistered( Type type )
        {
            if ( _registeredTypes.Contains( type ) )
            {
                return true;
            }

            if ( IsSpecialRegisteredType( type ) )
            {
                return true;
            }

            return false;
        }

        public bool TryRegisterType( Type type )
        {
            RegisterTypeReturnCode returnCode = RegisterTypeInternal( type );
            return ( returnCode == RegisterTypeReturnCode.Success );
        }

        internal bool TryGetRegisteredType( uint typeId, out RegisteredType registeredType )
        {
            return _registeredTypes.TryGetValue( typeId, out registeredType );
        }

        internal bool TryGetRegisteredType( Type type, out RegisteredType registeredType )
        {
            return _registeredTypes.TryGetValue( type, out registeredType );
        }

        static bool DoesTypeRequireDataTypeAttribute( Type baseType )
        {
            if ( baseType.IsEnum )
            {
                return false;
            }

            return ( baseType.IsClass || baseType.IsValueType );
        }

        static bool IsSpecialRegisteredType( Type baseType )
        {
            return baseType.IsArray;
        }

        static bool IsValidTypeToRegister( Type type )
        {
            if ( type.IsInterface )
            {
                return false;
            }

            Type[] genericArguments = type.GetGenericArguments();
            if ( genericArguments.Length == 0 )
            {
                return true;
            }

            return genericArguments.All( IsValidTypeToRegister );
        }

        static void GetInstantiatorAndWrapper( Type type, out ValueInstantiator instantiator, out ValueWrapper wrapper )
        {
            if ( type.IsEnum )
            {
                instantiator = EnumValue.CreateInstantiator( type );
                wrapper = EnumValue.CreateWrapper( type );
                return;
            }

            instantiator = ReflectedClassValue.Instantiate;
            wrapper = ReflectedClassValue.WrapRawValue;
        }

        RegisterTypeReturnCode RegisterTypeInternal( Type type )
        {
            if ( DoesTypeRequireDataTypeAttribute( type ) )
            {
                BinaryDataTypeAttribute dataTypeAttribute = type.GetCustomAttribute<BinaryDataTypeAttribute>();
                if ( dataTypeAttribute == null )
                {
                    return RegisterTypeReturnCode.DoesNotHaveBinaryDataType;
                }
            }

            if ( IsTypeRegistered( type ) )
            {
                return RegisterTypeReturnCode.TypeAlreadyRegistered;
            }

            if ( !IsValidTypeToRegister( type ) )
            {
                return RegisterTypeReturnCode.InvalidTypeToRegister;
            }

            GetInstantiatorAndWrapper( type, out ValueInstantiator instantiator, out ValueWrapper wrapper );
            RegisterTypeInternal( ref _nextProprietaryType, type, StandardTypeSignature.Create, instantiator, wrapper );
            return RegisterTypeReturnCode.Success;
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
            RegisterBuiltInValueType( StandardReadWriteOperations.DateTime );
            RegisterBuiltInValueType( StandardReadWriteOperations.Guid );
            RegisterBuiltInValueType( StandardReadWriteOperations.TimeSpan );
            RegisterBuiltInValueType( StandardReadWriteOperations.DateTimeOffset );
            RegisterBuiltInValueType( StandardReadWriteOperations.Uri );

            RegisterBuiltInType( typeof( List<> ), StandardTypeSignature.Create, ListValue.Instantiate, ListValue.WrapRawValue );
            RegisterBuiltInType( typeof( Nullable<> ), StandardTypeSignature.Create, NullableValue.Instantiate, NullableValue.WrapRawValue );
        }

        void RegisterSpecialTypes()
        {
            // Special type for ALL arrays! We just need *A* type here, so we'll use object[]. We ignore this value otherwise.
            ArrayId = RegisterBuiltInType( typeof( object[] ), ArrayTypeSignature.Create, ArrayValue.Instantiate, ArrayValue.WrapRawValue );
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
