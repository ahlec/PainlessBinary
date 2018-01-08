// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace PainlessBinary.Exceptions
{
    public sealed class TypeAlreadyRegisteredException : PainlessBinaryException
    {
        internal TypeAlreadyRegisteredException( Type registeredType )
            : base( $"The type {registeredType} has already been registered." )
        {
            Type = registeredType;
        }

        public Type Type { get; }
    }
}
