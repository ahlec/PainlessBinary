// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;

namespace SonezakiMasaki
{
    internal interface ISerializableValue
    {
        ITypeDefinition TypeDefinition { get; }

        object Read( BinaryReader reader );
    }
}
