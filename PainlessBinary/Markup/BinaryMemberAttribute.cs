// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace PainlessBinary.Markup
{
    [AttributeUsage( AttributeTargets.Field | AttributeTargets.Property )]
    public sealed class BinaryMemberAttribute : Attribute
    {
        public BinaryMemberAttribute( int order )
        {
            Order = order;
        }

        public int Order { get; }
    }
}
