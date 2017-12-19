// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;
using SonezakiMasaki.Containers;

namespace SonezakiMasaki
{
    public sealed class Serializer
    {
        readonly ContainerResolver _containerResolver = new ContainerResolver();
        readonly TypeResolver _typeResolver;

        public T Deserialize<T>( Stream dataStream )
        {
            using ( BinaryReader reader = new BinaryReader( dataStream ) )
            {
                ISerializableValue typeInfo = _typeResolver.GetTypeInfoFromType( typeof( T ) );
                return (T) DeserializeItem( reader, new NoneContainer(), typeInfo );
            }
        }

        object DeserializeObject( BinaryReader reader )
        {
            ReadObjectHeader( reader, out Container container, out ISerializableValue typeInfo );
            return DeserializeItem( reader, container, typeInfo );
        }

        object DeserializeItem( BinaryReader reader, Container container, ISerializableValue type )
        {
        }

        void ReadObjectHeader( BinaryReader reader, out Container container, out ISerializableValue typeInfo )
        {
            byte containerId = reader.ReadByte();
            container = _containerResolver.ResolveContainer( containerId );
            container.ReadHeader( reader );

            int typeId = reader.ReadInt32();
            typeInfo = _typeResolver.ResolveType( typeId );
        }
    }
}
