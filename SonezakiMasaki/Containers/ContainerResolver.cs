// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using SonezakiMasaki.Exceptions;

namespace SonezakiMasaki.Containers
{
    internal sealed class ContainerResolver
    {
        static readonly IReadOnlyDictionary<ContainerId, ContainerConstructor> _containers = new Dictionary<ContainerId, ContainerConstructor>
        {
            { ContainerId.None, () => new NoneContainer() },
            { ContainerId.List, () => new ListContainer() }
        };

        delegate Container ContainerConstructor();

        public Container ResolveContainer( int containerId )
        {
            if ( !_containers.TryGetValue( (ContainerId) containerId, out ContainerConstructor constructor ) )
            {
                throw new UnrecognizedContainerException( containerId );
            }

            Container container = constructor();
            return container;
        }
    }
}
