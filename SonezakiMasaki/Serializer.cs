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
        readonly TypeInstantiator _typeInstantiator = new TypeInstantiator();

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

        T DeserializeFilePayload<T>( BinaryReader reader )
        {
            Type fileType = _objectSerializer.ReadNextType( reader );
            if ( fileType != typeof( T ) )
            {
                throw new DifferentFileTypeException( typeof( T ), fileType );
            }

            ISerializableValue value = _typeInstantiator.Instantiate( fileType, reader );
            object payload = value.Read( reader, _objectSerializer );
            return (T) payload;
        }
    }
}
