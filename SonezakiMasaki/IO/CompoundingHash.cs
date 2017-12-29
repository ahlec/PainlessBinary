// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace SonezakiMasaki.IO
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
                HashValue *= _multiplicationConstant;
                HashValue += buffer[index];
            }
        }
    }
}
