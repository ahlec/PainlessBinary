// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;

namespace SonezakiMasaki.Containers
{
    internal sealed class NoneContainer : IContainer
    {
        public ContainerId Id => ContainerId.None;

        public object FinalValue { get; private set; }

        public void ReadHeader( BinaryReader reader )
        {
        }

        public void Prepare( ITypeInfo typeInfo )
        {
        }

        public void AddItem( object item )
        {
            FinalValue = item;
        }
    }
}
