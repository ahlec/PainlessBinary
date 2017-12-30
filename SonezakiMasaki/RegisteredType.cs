// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using SonezakiMasaki.IO;
using SonezakiMasaki.TypeSignatures;

namespace SonezakiMasaki
{
    internal delegate ITypeSignature TypeSignatureCreator( uint id, Type baseType );

    internal delegate ISerializableValue ValueInstantiator( TypeManager typeManager, Type fullType, SonezakiReader reader );

    internal delegate ISerializableValue ValueWrapper( TypeManager typeManager, Type fullType, object value );

    internal sealed class RegisteredType : IMultiKeyValue<uint, Type>
    {
        public RegisteredType( uint id, Type type, TypeSignatureCreator signatureCreator, ValueInstantiator instantiator, ValueWrapper wrapper )
        {
            Id = id;
            Type = type;
            TypeSignature = signatureCreator( id, type );
            Instantiator = instantiator;
            Wrapper = wrapper;
        }

        public uint Id { get; }

        public uint Key1 => Id;

        public Type Type { get; }

        public Type Key2 => Type;

        public ITypeSignature TypeSignature { get; }

        public ValueInstantiator Instantiator { get; }

        public ValueWrapper Wrapper { get; }
    }
}
