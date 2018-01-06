// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using SonezakiMasaki.Markup;

namespace SonezakiMasaki.Tests.ExampleTypes
{
    [BinaryDataType]
    public sealed class Item
    {
        [BinaryMember( 1 )]
        public string Name { get; set; }
    }
}
