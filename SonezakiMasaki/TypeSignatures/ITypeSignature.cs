// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using SonezakiMasaki.IO;

namespace SonezakiMasaki.TypeSignatures
{
    internal interface ITypeSignature
    {
        Type Read( SonezakiReader reader );

        void Write( SonezakiWriter writer, Type fullType );
    }
}
