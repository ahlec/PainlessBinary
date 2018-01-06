// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using PainlessBinary.IO;

namespace PainlessBinary
{
    internal interface ISerializableValue
    {
        object Value { get; }

        void Read( SonezakiReader reader );

        void Write( SonezakiWriter writer );
    }
}
