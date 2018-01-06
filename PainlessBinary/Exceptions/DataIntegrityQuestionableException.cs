// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace PainlessBinary.Exceptions
{
    public sealed class DataIntegrityQuestionableException : PainlessBinaryException
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
