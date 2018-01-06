// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using SonezakiMasaki.Markup;

namespace SonezakiMasaki.Tests.ExampleTypes
{
    [BinaryDataType]
    public sealed class GameData
    {
        [BinaryMember( 1 )]
        public Date DateGameDataCompiled { get; set; }

        [BinaryMember( 2 )]
        public Item[] Items { get; set; }

        [BinaryMember( 3 )]
        public Character[] Characters { get; set; }
    }
}
