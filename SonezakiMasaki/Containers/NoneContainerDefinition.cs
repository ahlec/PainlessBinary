// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace SonezakiMasaki.Containers
{
    internal sealed class NoneContainerDefinition : IContainerDefinition
    {
        static readonly IDictionary<Type, NoneContainerDefinition> _cachedDefinitions = new Dictionary<Type, NoneContainerDefinition>();

        NoneContainerDefinition( Type type )
        {
            Type = type;
        }

        public Type Type { get; }

        internal static NoneContainerDefinition Read( BinaryReader reader, ObjectSerializer objectSerializer )
        {
            ITypeDefinition listGenericType = objectSerializer.ReadNextTypeDefinition( reader );
            return GetContainerDefinitionForType( listGenericType );
        }

        static NoneContainerDefinition GetContainerDefinitionForType( ITypeDefinition typeDefinition )
        {
            if ( _cachedDefinitions.TryGetValue( typeDefinition.Type, out NoneContainerDefinition cached ) )
            {
                return cached;
            }

            NoneContainerDefinition containerDefinition = CreateContainerDefinitionForType( typeDefinition );
            _cachedDefinitions.Add( typeDefinition.Type, containerDefinition );
            return containerDefinition;
        }

        static NoneContainerDefinition CreateContainerDefinitionForType( ITypeDefinition typeDefinition )
        {
            return new NoneContainerDefinition( typeDefinition.Type );
        }
    }
}
