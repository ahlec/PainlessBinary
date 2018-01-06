// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace PainlessBinary.IO
{
    internal sealed class CompoundingHash
    {
        readonly int _multiplicationConstant;

        public CompoundingHash( int hashSeed, int multiplicationConstant )
        {
            HashValue = hashSeed;
            _multiplicationConstant = multiplicationConstant;
        }

        public int HashValue { get; private set; }

        public void AddBytes( byte[] buffer, int numBytes )
        {
            for ( int index = 0; index < numBytes; ++index )
            {
                AddByte( buffer[index] );
            }
        }

        public void AddByte( byte singleByte )
        {
            HashValue *= _multiplicationConstant;
            HashValue += singleByte;
        }
    }
}
