// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;

namespace SonezakiMasaki.IO
{
    internal sealed class ReadWriteOperations<T>
    {
        public ReadWriteOperations( BinaryReadFunction readFunction, BinaryWriteFunction writeFunction )
        {
            ReadFunction = readFunction;
            WriteFunction = writeFunction;
        }

        internal delegate T BinaryReadFunction( BinaryReader reader );

        internal delegate void BinaryWriteFunction( BinaryWriter writer, T value );

        public BinaryReadFunction ReadFunction { get; }

        public BinaryWriteFunction WriteFunction { get; }
    }
}
