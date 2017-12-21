// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.IO;

namespace SonezakiMasaki.Containers
{
    internal sealed class ListContainer : ISerializableValue
    {
        readonly ListContainerDefinition _listDefinition;
        readonly int _listLength;

        public ListContainer( ListContainerDefinition listDefinition, int length )
        {
            _listDefinition = listDefinition;
            _listLength = length;
        }

        public ContainerId Id => ContainerId.List;

        public ITypeDefinition TypeDefinition => _listDefinition;

        public object Read( BinaryReader reader )
        {
            IList list = (IList) Activator.CreateInstance( _listDefinition.Type, _listLength );

            for ( int index = 0; index < _listLength; ++index )
            {
                ISerializableValue value = _listDefinition.ContentTypeDefinition.Instantiate( reader );
                object deserializedValue = value.Read( reader );
                list.Add( deserializedValue );
            }

            return list;
        }
    }
}
