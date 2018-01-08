// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using PainlessBinary.Markup;

namespace PainlessBinary.Exceptions
{
    public sealed class TypeMissingMarkupException : PainlessBinaryException
    {
        internal TypeMissingMarkupException( Type type )
            : base( $"The type {type} is not marked up with {nameof( BinaryDataTypeAttribute )}." )
        {
            Type = type;
        }

        public Type Type { get; }
    }
}
