// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using PainlessBinary.Markup;

namespace PainlessBinary.Tests.ExampleTypes
{
    [BinaryDataType]
    public sealed class Item
    {
        [BinaryMember( 1 )]
        public string Name { get; set; }
    }
}
