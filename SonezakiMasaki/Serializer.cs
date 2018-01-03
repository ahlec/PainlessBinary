// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using SonezakiMasaki.Exceptions;
using SonezakiMasaki.IO;

namespace SonezakiMasaki
{
    public sealed class Serializer
    {
        readonly TypeManager _typeManager;

        public Serializer( TypeRegistry typeRegistry )
        {
            if ( typeRegistry == null )
            {
                throw new ArgumentNullException( nameof( typeRegistry ) );
            }

            _typeManager = new TypeManager( typeRegistry );
        }

        public int HashBaseValue { get; set; } = 31;

        public int HashMultiplicationConstant { get; set; } = 17;

        public void SerializeFile<T>( Stream dataStream, SerializationFile<T> file )
        {
            if ( file == null )
            {
                throw new ArgumentNullException( nameof( file ) );
            }

            using ( SonezakiStreamWrapper streamWrapper = new SonezakiStreamWrapper( dataStream ) )
            {
                using ( SonezakiWriter writer = new SonezakiWriter( streamWrapper, _typeManager, HashBaseValue, HashMultiplicationConstant ) )
                {
                    SerializeFilePayload( writer, file.Payload );
                }
            }
        }

        public SerializationFile<T> DeserializeFile<T>( Stream dataStream )
        {
            using ( SonezakiStreamWrapper streamWrapper = new SonezakiStreamWrapper( dataStream ) )
            {
                using ( SonezakiReader reader = new SonezakiReader( streamWrapper, _typeManager, HashBaseValue, HashMultiplicationConstant ) )
                {
                    T payload = DeserializeFilePayload<T>( reader );
                    return new SerializationFile<T>
                    {
                        Payload = payload
                    };
                }
            }
        }

        static T DeserializeFilePayload<T>( SonezakiReader reader )
        {
            Type fileType = reader.ReadNextType();
            if ( fileType != typeof( T ) )
            {
                throw new DifferentFileTypeException( typeof( T ), fileType );
            }

            object deserializedObject = reader.ReadSonezakiObject( typeof( T ) );
            return (T) deserializedObject;
        }

        void SerializeFilePayload<T>( SonezakiWriter writer, T payload )
        {
            writer.WriteType( typeof( T ) );

            writer.WriteSonezakiObject( typeof( T ), payload );
        }
    }
}
