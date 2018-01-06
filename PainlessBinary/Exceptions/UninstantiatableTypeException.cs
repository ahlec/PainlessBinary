// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace PainlessBinary.Exceptions
{
    public sealed class UninstantiatableTypeException : PainlessBinaryException
    {
        internal UninstantiatableTypeException( Type baseType, Type fullType )
            : base( $"The type {baseType} could not be instantiated." )
        {
            BaseType = baseType;
            FullType = fullType;
        }

        public Type BaseType { get; }

        public Type FullType { get; }
    }
}
