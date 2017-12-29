// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using SonezakiMasaki.IO;

namespace SonezakiMasaki
{
    internal interface ISerializableValue
    {
        object Value { get; }

        void Read( SonezakiReader reader, ObjectSerializer objectSerializer );

        void Write( SonezakiWriter writer, ObjectSerializer objectSerializer );
    }
}
