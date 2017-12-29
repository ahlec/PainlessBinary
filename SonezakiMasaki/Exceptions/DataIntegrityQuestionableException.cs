// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace SonezakiMasaki.Exceptions
{
    public sealed class DataIntegrityQuestionableException : SonezakiMasakiException
    {
        internal DataIntegrityQuestionableException( int computedHash, int readHash )
            : base( "The hash that was encountered when reading does not match what was computed when deserializing." )
        {
            ComputedHash = computedHash;
            ReadHash = readHash;
        }

        public int ComputedHash { get; }

        public int ReadHash { get; }
    }
}
