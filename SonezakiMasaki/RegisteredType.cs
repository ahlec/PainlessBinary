// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace SonezakiMasaki
{
    internal delegate ISerializableValue ValueInstantiator( TypeManager typeManager, Type fullType, BinaryReader reader );

    internal sealed class RegisteredType : IMultiKeyValue<uint, Type>
    {
        readonly ValueInstantiator _instantiator;

        public RegisteredType( uint id, Type type, ValueInstantiator instantiator )
        {
            Id = id;
            Type = type;
            _instantiator = instantiator;
        }

        public uint Id { get; }

        public uint Key1 => Id;

        public Type Type { get; }

        public Type Key2 => Type;

        public ISerializableValue Instantiate( TypeManager typeManager, Type fullType, BinaryReader reader )
        {
            return _instantiator( typeManager, fullType, reader );
        }
    }
}
