// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace SonezakiMasaki.Tests.ExampleTypes
{
    public sealed class Date
    {
        [SerializedMember( 1 )]
        public byte Day { get; set; }

        [SerializedMember( 2 )]
        public Month Month { get; set; }

        [SerializedMember( 3 )]
        public int Year { get; set; }
    }
}
