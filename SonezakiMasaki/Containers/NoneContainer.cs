// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace SonezakiMasaki.Containers
{
    internal sealed class NoneContainer : ISerializableValue
    {
        public NoneContainer( )

        public ITypeDefinition TypeDefinition { get; }

        public ContainerId Id => ContainerId.None;

    }
}
