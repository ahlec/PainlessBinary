// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using PainlessBinary.Markup;

namespace PainlessBinary.Tests.ExampleTypes
{
    [BinaryDataType]
    public sealed class Date
    {
        [BinaryMember( 1 )]
        public byte Day { get; set; }

        [BinaryMember( 2 )]
        public Month Month { get; set; }

        [BinaryMember( 3 )]
        public int Year { get; set; }
    }
}
