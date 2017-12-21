// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace SonezakiMasaki
{
    internal interface ITypeDefinition
    {
        Type Type { get; }

        ISerializableValue Instantiate( BinaryReader reader );
    }
}
