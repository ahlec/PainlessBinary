// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using PainlessBinary.Exceptions;
using PainlessBinary.IO;
using PainlessBinary.Markup;
using PainlessBinary.TypeSignatures;

namespace PainlessBinary
{
    internal sealed class TypeManager
    {
        readonly TypeRegistry _registry;

        public TypeManager( TypeRegistry registry )
        {
            _registry = registry;
        }

        public ITypeSignature ResolveTypeSignature( uint typeId )
        {
            if ( !_registry.TryGetRegisteredType( typeId, out RegisteredType registeredType ) )
            {
                throw new UnrecognizedTypeException( typeId );
            }

            return registeredType.TypeSignature;
        }

        public ITypeSignature ResolveTypeSignature( Type type )
        {
            RegisteredType registeredType = GetRegisteredType( type );
            return registeredType.TypeSignature;
        }

        public bool DetermineIsTypeSerializedAsReference( Type type )
        {
            if ( !TryGetBinaryDataTypeAttribute( type, out BinaryDataTypeAttribute attribute ) )
            {
                return false;
            }

            return ( attribute.Scheme == BinarySerializationScheme.Reference );
        }

        public ReferenceDetectionMethod GetTypeReferenceDetectionMethod( Type type )
        {
            if ( !TryGetBinaryDataTypeAttribute( type, out BinaryDataTypeAttribute attribute ) )
            {
                throw new InvalidOperationException();
            }

            return attribute.ReferenceDetectionMethod;
        }

        internal ISerializableValue Instantiate( Type type, PainlessBinaryReader reader )
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

        static bool TryGetBinaryDataTypeAttribute( Type type, out BinaryDataTypeAttribute attribute )
        {
            attribute = type.GetCustomAttribute<BinaryDataTypeAttribute>();
            return ( attribute != null );
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

            if ( !_registry.TryGetRegisteredType( baseType, out registeredType ) )
            {
                throw new UninstantiatableTypeException( baseType, type );
            }

            return registeredType;
        }

        bool TryGetSpecialRegisteredType( Type baseType, out RegisteredType registeredType )
        {
            if ( baseType.IsArray )
            {
                return _registry.TryGetRegisteredType( _registry.ArrayId, out registeredType );
            }

            registeredType = null;
            return false;
        }
    }
}
