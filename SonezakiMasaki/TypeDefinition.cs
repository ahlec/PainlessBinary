// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace SonezakiMasaki
{
    internal abstract class TypeDefinition
    {
        public Type Type { get; }

        public abstract ISerializableValue Instantiate( BinaryReader reader );
    }
}
