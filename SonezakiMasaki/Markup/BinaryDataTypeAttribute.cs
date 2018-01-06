// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace SonezakiMasaki.Markup
{
    [AttributeUsage( AttributeTargets.Class )]
    public sealed class BinaryDataTypeAttribute : Attribute
    {
        public BinaryDataTypeAttribute( BinarySerializationScheme scheme = BinarySerializationScheme.Value )
        {
            Scheme = scheme;
        }

        public BinarySerializationScheme Scheme { get; }
    }
}
