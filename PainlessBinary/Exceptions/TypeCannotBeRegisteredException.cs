// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace PainlessBinary.Exceptions
{
    public sealed class TypeCannotBeRegisteredException : PainlessBinaryException
    {
        internal TypeCannotBeRegisteredException( Type type )
            : base( $"The type {type} is invalid to register." )
        {
            Type = type;
        }

        public Type Type { get; }
    }
}
