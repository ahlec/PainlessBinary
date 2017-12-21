// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace SonezakiMasaki.Exceptions
{
    public sealed class UnrecognizedTypeException : SonezakiMasakiException
    {
        internal UnrecognizedTypeException( uint typeId )
            : base( $"There is no type with the encountered ID of {typeId}." )
        {
            TypeId = typeId;
        }

        public uint TypeId { get; }
    }
}
