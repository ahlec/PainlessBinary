// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using SonezakiMasaki.Exceptions;
using SonezakiMasaki.IO;
using SonezakiMasaki.TypeSignatures;

namespace SonezakiMasaki
{
    internal sealed class TypeManager
    {
        static readonly IDictionary<Type, bool> _isTypeSerializedAsReference = new Dictionary<Type, bool>();
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
            if ( _isTypeSerializedAsReference.TryGetValue( type, out bool isSerializedAsReference ) )
            {
                return isSerializedAsReference;
            }

            isSerializedAsReference = ( type.GetCustomAttribute<BinarySerializedAsReferenceAttribute>() != null );
            _isTypeSerializedAsReference[type] = isSerializedAsReference;
            return isSerializedAsReference;
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
