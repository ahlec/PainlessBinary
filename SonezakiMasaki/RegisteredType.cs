// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using SonezakiMasaki.IO;

namespace SonezakiMasaki
{
    internal delegate ISerializableValue ValueInstantiator( TypeManager typeManager, Type fullType, SonezakiReader reader );

    internal delegate ISerializableValue ValueWrapper( TypeManager typeManager, object value );

    internal sealed class RegisteredType : IMultiKeyValue<uint, Type>
    {
        readonly TypeManager _typeManager;
        readonly ValueInstantiator _instantiator;
        readonly ValueWrapper _wrapper;

        public RegisteredType( TypeManager typeManager, uint id, Type type, ValueInstantiator instantiator, ValueWrapper wrapper )
        {
            _typeManager = typeManager;
            Id = id;
            Type = type;
            _instantiator = instantiator;
            _wrapper = wrapper;
        }

        public uint Id { get; }

        public uint Key1 => Id;

        public Type Type { get; }

        public Type Key2 => Type;

        public ISerializableValue Instantiate( Type fullType, SonezakiReader reader )
        {
            return _instantiator( _typeManager, fullType, reader );
        }

        public ISerializableValue Wrap( object value )
        {
            return _wrapper( _typeManager, value );
        }
    }
}
