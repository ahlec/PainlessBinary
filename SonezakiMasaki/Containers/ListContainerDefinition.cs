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

        ListContainerDefinition( Type type, ITypeDefinition contentTypeDefinition )
        {
            Type = type;
            ContentTypeDefinition = contentTypeDefinition;
        }

        public Type Type { get; }

        public ITypeDefinition ContentTypeDefinition { get; }

        public ISerializableValue Instantiate( BinaryReader reader )
        {
            int listLength = reader.ReadInt32();
            return new ListContainer( this, listLength );
        }

        internal static ListContainerDefinition GetDefinitionFor( BinaryReader reader, ObjectSerializer objectSerializer )
        {
            ITypeDefinition contentTypeDefinition = objectSerializer.ReadNextTypeDefinition( reader );
            return GetContainerDefinitionForType( contentTypeDefinition );
        }

        static ListContainerDefinition GetContainerDefinitionForType( ITypeDefinition contentTypeDefinition )
        {
            if ( _cachedDefinitions.TryGetValue( contentTypeDefinition.Type, out ListContainerDefinition cached ) )
            {
                return cached;
            }

            ListContainerDefinition containerDefinition = CreateContainerDefinitionForType( contentTypeDefinition );
            _cachedDefinitions.Add( contentTypeDefinition.Type, containerDefinition );
            return containerDefinition;
        }

        static ListContainerDefinition CreateContainerDefinitionForType( ITypeDefinition contentTypeDefinition )
        {
            Type listType = typeof( List<> ).MakeGenericType( contentTypeDefinition.Type );
            return new ListContainerDefinition( listType, contentTypeDefinition );
        }
    }
}
