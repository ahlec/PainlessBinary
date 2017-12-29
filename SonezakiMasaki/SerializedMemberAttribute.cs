// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace SonezakiMasaki
{
    [AttributeUsage( AttributeTargets.Field | AttributeTargets.Property )]
    public sealed class SerializedMemberAttribute : Attribute
    {
        public SerializedMemberAttribute( int order )
        {
            Order = order;
        }

        public int Order { get; }
    }
}
