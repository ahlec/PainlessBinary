// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace SonezakiMasaki
{
    internal interface ISerializableValue
    {
        /// <summary>
        /// The type itself.
        /// </summary>
        Type Type { get; }

        void ReadHeader( BinaryReader reader, ObjectSerializer objectSerializer );

        object Read( BinaryReader reader );
    }
}
