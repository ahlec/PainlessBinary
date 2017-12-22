// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace SonezakiMasaki.Containers
{
    internal sealed class ListContainerDefinition : TypeDefinition
    {
        ListContainerDefinition( Type type, TypeDefinition contentTypeDefinition )
        {
            Type = type;
            ContentTypeDefinition = contentTypeDefinition;
        }

        public Type Type { get; }

        public TypeDefinition ContentTypeDefinition { get; }

        public override ISerializableValue Instantiate( BinaryReader reader )
        {
            int listLength = reader.ReadInt32();
            return new ListContainer( this, listLength );
        }
    }
}
