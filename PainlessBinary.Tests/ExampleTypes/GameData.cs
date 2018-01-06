// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using PainlessBinary.Markup;

namespace PainlessBinary.Tests.ExampleTypes
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
