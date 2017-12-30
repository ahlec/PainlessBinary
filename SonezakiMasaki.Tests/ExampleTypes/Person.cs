// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace SonezakiMasaki.Tests.ExampleTypes
{
    public sealed class Person
    {
        [SerializedMember( 1 )]
        public string FirstName { get; set; }

        [SerializedMember( 2 )]
        public string LastName { get; set; }

        [SerializedMember( 3 )]
        public int Age { get; set; }

        [SerializedMember( 4 )]
        public Date Birthday { get; set; }

        [SerializedMember( 5 )]
        public string[] OtherNames { get; set; }

        [SerializedMember( 6 )]
        public bool IsHappy { get; set; }

        [SerializedMember( 7 )]
        public Pronoun? Pronoun { get; set; }
    }
}
