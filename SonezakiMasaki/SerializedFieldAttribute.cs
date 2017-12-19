// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace SonezakiMasaki
{
    [AttributeUsage( AttributeTargets.Field | AttributeTargets.Property )]
    internal sealed class SerializedFieldAttribute : Attribute
    {
        public SerializedFieldAttribute( int order )
        {
            Order = order;
        }

        public int Order { get; set; }
    }
}
