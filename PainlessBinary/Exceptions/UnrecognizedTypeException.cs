// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace PainlessBinary.Exceptions
{
    public sealed class UnrecognizedTypeException : PainlessBinaryException
    {
        internal UnrecognizedTypeException( uint typeId )
            : base( $"There is no type with the encountered ID of {typeId}." )
        {
            TypeId = typeId;
        }

        public uint TypeId { get; }
    }
}
