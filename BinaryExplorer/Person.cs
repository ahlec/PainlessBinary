using SonezakiMasaki;

namespace BinaryExplorer
{
    public sealed class Person
    {
        [SerializedMember( 1 )]
        public string FirstName { get; set; }

        [SerializedMember( 2 )]
        public string LastName { get; set; }

        [SerializedMember( 3 )]
        public int Age { get; set; }
    }
}
