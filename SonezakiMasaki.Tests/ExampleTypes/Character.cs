// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using SonezakiMasaki.Markup;

namespace SonezakiMasaki.Tests.ExampleTypes
{
    [BinaryDataType]
    public sealed class Character
    {
        [BinaryMember( 1 )]
        public string Name { get; set; }

        [BinaryMember( 2 )]
        public int Age { get; set; }

        [BinaryMember( 3 )]
        public bool IsFriendly { get; set; }

        [BinaryMember( 4 )]
        public List<Item> Inventory { get; set; } = new List<Item>();
    }
}
