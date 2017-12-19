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
        readonly TypeResolver _typeResolver;

        public T Deserialize<T>( Stream dataStream )
        {
            using ( BinaryReader reader = new BinaryReader( dataStream ) )
            {
                ITypeInfo typeInfo = _typeResolver.GetTypeInfo( typeof( T ) );
                return (T) DeserializeItem( reader, new NoneContainer(), typeInfo );
            }
        }

        object DeserializeItem( BinaryReader reader, IContainer container, ITypeInfo type )
        {
        }
    }
}
