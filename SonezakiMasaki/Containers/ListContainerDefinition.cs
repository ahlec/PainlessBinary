// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace SonezakiMasaki.Containers
{
    internal sealed class ListContainerDefinition : IContainerDefinition
    {
        static readonly IDictionary<Type, ListContainerDefinition> _cachedDefinitions = new Dictionary<Type, ListContainerDefinition>();

        ListContainerDefinition( Type type )
        {
            Type = type;
        }

        public Type Type { get; }

        internal static ListContainerDefinition Read( BinaryReader reader, ObjectSerializer objectSerializer )
        {
            ITypeDefinition listGenericType = objectSerializer.ReadNextTypeDefinition( reader );
            return GetContainerDefinitionForType( listGenericType );
        }

        static ListContainerDefinition GetContainerDefinitionForType( ITypeDefinition typeDefinition )
        {
            if ( _cachedDefinitions.TryGetValue( typeDefinition.Type, out ListContainerDefinition cached ) )
            {
                return cached;
            }

            ListContainerDefinition containerDefinition = CreateContainerDefinitionForType( typeDefinition );
            _cachedDefinitions.Add( typeDefinition.Type, containerDefinition );
            return containerDefinition;
        }

        static ListContainerDefinition CreateContainerDefinitionForType( ITypeDefinition typeDefinition )
        {
            Type listType = typeof( List<> ).MakeGenericType( typeDefinition.Type );
            return new ListContainerDefinition( listType );
        }
    }
}
