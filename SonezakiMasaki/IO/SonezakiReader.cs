// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using SonezakiMasaki.TypeSignatures;

namespace SonezakiMasaki.IO
{
    internal sealed class SonezakiReader : BinaryReader
    {
        readonly SonezakiStreamWrapper _sonezakiStreamWrapper;
        readonly TypeManager _typeManager;
        readonly int _hashSeed;
        readonly int _hashMultiplicationConstant;

        public SonezakiReader( SonezakiStreamWrapper dataStream, TypeManager typeManager, int hashSeed, int hashMultiplicationConstant )
            : base( dataStream )
        {
            _sonezakiStreamWrapper = dataStream;
            _typeManager = typeManager;
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

        public Type ReadNextType()
        {
            uint typeId = ReadUInt32();
            ITypeSignature typeSignature = _typeManager.ResolveTypeSignature( typeId );
            return typeSignature.Read( this );
        }
    }
}
