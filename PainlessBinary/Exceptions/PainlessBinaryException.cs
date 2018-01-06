// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace PainlessBinary.Exceptions
{
    public abstract class PainlessBinaryException : Exception
    {
        internal PainlessBinaryException( string message )
            : base( message )
        {
        }

        internal PainlessBinaryException( string message, Exception innerException )
            : base( message, innerException )
        {
        }
    }
}
