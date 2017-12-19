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
                ITypeInfo typeInfo = _typeResolver.GetTypeInfoFromType( typeof( T ) );
                return (T) DeserializeItem( reader, new NoneContainer(), typeInfo );
            }
        }

        object DeserializeObject( BinaryReader reader )
        {
            ReadObjectHeader( reader, out IContainer container, out ITypeInfo typeInfo );
            return DeserializeItem( reader, container, typeInfo );
        }

        object DeserializeItem( BinaryReader reader, IContainer container, ITypeInfo type )
        {
        }

        void ReadObjectHeader( BinaryReader reader, out IContainer container, out ITypeInfo typeInfo )
        {
            byte containerId = reader.ReadByte();
            container = _containerResolver.ResolveContainer( containerId );
            container.ReadHeader( reader );

            int typeId = reader.ReadInt32();
            typeInfo = _typeResolver.ResolveType( typeId );
        }
    }
}
