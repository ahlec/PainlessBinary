using SonezakiMasaki;

namespace BinaryExplorer
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
