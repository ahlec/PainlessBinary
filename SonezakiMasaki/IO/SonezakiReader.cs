// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;

namespace SonezakiMasaki.IO
{
    internal sealed class SonezakiReader : BinaryReader
    {
        readonly SonezakiStreamWrapper _sonezakiStreamWrapper;
        readonly int _hashSeed;
        readonly int _hashMultiplicationConstant;

        public SonezakiReader( SonezakiStreamWrapper dataStream, int hashSeed, int hashMultiplicationConstant )
            : base( dataStream )
        {
            _sonezakiStreamWrapper = dataStream;
            _hashSeed = hashSeed;
            _hashMultiplicationConstant = hashMultiplicationConstant;
        }

        public void PushCompoundingHash()
        {
            CompoundingHash hash = new CompoundingHash( _hashSeed, _hashMultiplicationConstant );
            _sonezakiStreamWrapper.PushCompoundingHash( hash );
        }

        public int PopCompoundingHash()
        {
            CompoundingHash hash = _sonezakiStreamWrapper.PopCompoundingHash();
            return hash.HashValue;
        }
    }
}
