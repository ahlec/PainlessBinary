// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace PainlessBinary.IO
{
    internal sealed class ReadWriteOperations<T>
    {
        public ReadWriteOperations( BinaryReadFunction readFunction, BinaryWriteFunction writeFunction )
        {
            ReadFunction = readFunction;
            WriteFunction = writeFunction;
        }

        internal delegate T BinaryReadFunction( SonezakiReader reader );

        internal delegate void BinaryWriteFunction( SonezakiWriter writer, T value );

        public BinaryReadFunction ReadFunction { get; }

        public BinaryWriteFunction WriteFunction { get; }
    }
}
