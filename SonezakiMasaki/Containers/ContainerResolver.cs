// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using SonezakiMasaki.Exceptions;

namespace SonezakiMasaki.Containers
{
    internal sealed class ContainerResolver
    {
        static readonly IReadOnlyDictionary<ContainerId, ContainerDefinitionCreator> _containers = new Dictionary<ContainerId, ContainerDefinitionCreator>
        {
            { ContainerId.List, ListContainerDefinition.GetDefinitionFor }
        };

        delegate IContainerDefinition ContainerDefinitionCreator( BinaryReader reader, ObjectSerializer objectSerializer );

        public IContainerDefinition ResolveContainer( uint containerId, BinaryReader reader, ObjectSerializer objectSerializer )
        {
            if ( !_containers.TryGetValue( (ContainerId) containerId, out ContainerDefinitionCreator creator ) )
            {
                throw new UnrecognizedContainerException( containerId );
            }

            IContainerDefinition containerDefinition = creator( reader, objectSerializer );
            return containerDefinition;
        }
    }
}
