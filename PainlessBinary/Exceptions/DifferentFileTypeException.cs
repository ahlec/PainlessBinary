// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace PainlessBinary.Exceptions
{
    public sealed class DifferentFileTypeException : PainlessBinaryException
    {
        internal DifferentFileTypeException( Type expectedFileType, Type actualFileType )
            : base( $"Expected the file type to be of type {expectedFileType.Name}, but it was actually {actualFileType.Name}" )
        {
            ExpectedFileType = expectedFileType;
            ActualFileType = actualFileType;
        }

        public Type ExpectedFileType { get; }

        public Type ActualFileType { get; }
    }
}
