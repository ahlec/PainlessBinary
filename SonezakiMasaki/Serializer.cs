// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using SonezakiMasaki.Exceptions;

namespace SonezakiMasaki
{
    public sealed class Serializer
    {
        readonly ObjectSerializer _objectSerializer;

        public Serializer( TypeResolver typeResolver )
        {
            _objectSerializer = new ObjectSerializer( typeResolver ?? throw new ArgumentNullException( nameof( typeResolver ) ) );
        }

        public SerializationFile<T> DeserializeFile<T>( Stream dataStream )
        {
            using ( BinaryReader reader = new BinaryReader( dataStream ) )
            {
                T payload = DeserializeFilePayload<T>( reader );
                return new SerializationFile<T>
                {
                    Payload = payload
                };
            }
        }

        static bool DoesTypeDefinitionMatchGenericType( ITypeDefinition typeDefinition, Type genericType )
        {
            if ( genericType.IsInterface )
            {
                return typeDefinition.Type.IsInstanceOfType( genericType );
            }

            return typeDefinition.Type == genericType;
        }

        T DeserializeFilePayload<T>( BinaryReader reader )
        {
            ITypeDefinition fileTypeDefinition = _objectSerializer.ReadNextTypeDefinition( reader );
            if ( !DoesTypeDefinitionMatchGenericType( fileTypeDefinition, typeof( T ) ) )
            {
                throw new DifferentFileTypeException( typeof( T ), fileTypeDefinition.Type );
            }

            ISerializableValue value = fileTypeDefinition.Instantiate( reader );
            object payload = value.Read( reader, _objectSerializer );
            return (T) payload;
        }
    }
}
