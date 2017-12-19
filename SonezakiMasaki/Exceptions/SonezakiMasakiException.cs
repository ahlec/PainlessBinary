// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace SonezakiMasaki.Exceptions
{
    public abstract class SonezakiMasakiException : Exception
    {
        internal SonezakiMasakiException( string message )
            : base( message )
        {
        }

        internal SonezakiMasakiException( string message, Exception innerException )
            : base( message, innerException )
        {
        }
    }
}
