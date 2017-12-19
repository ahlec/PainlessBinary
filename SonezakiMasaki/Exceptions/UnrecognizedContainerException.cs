// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace SonezakiMasaki.Exceptions
{
    public sealed class UnrecognizedContainerException : SonezakiMasakiException
    {
        internal UnrecognizedContainerException( byte containerId )
            : base( $"There is no container with the encountered ID of {containerId}." )
        {
            ContainerId = containerId;
        }

        public byte ContainerId { get; }
    }
}
