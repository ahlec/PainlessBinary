// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using PainlessBinary.Exceptions;
using PainlessBinary.IO;

namespace PainlessBinary
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

            using ( StreamWrapper streamWrapper = new StreamWrapper( dataStream ) )
            {
                using ( PainlessBinaryWriter writer = new PainlessBinaryWriter( streamWrapper, _typeManager, HashBaseValue, HashMultiplicationConstant ) )
                {
                    SerializeFilePayload( writer, file.Payload );
                }
            }
        }

        public SerializationFile<T> DeserializeFile<T>( Stream dataStream )
        {
            using ( StreamWrapper streamWrapper = new StreamWrapper( dataStream ) )
            {
                using ( PainlessBinaryReader reader = new PainlessBinaryReader( streamWrapper, _typeManager, HashBaseValue, HashMultiplicationConstant ) )
                {
                    T payload = DeserializeFilePayload<T>( reader );
                    return new SerializationFile<T>
                    {
                        Payload = payload
                    };
                }
            }
        }

        static T DeserializeFilePayload<T>( PainlessBinaryReader reader )
        {
            Type fileType = reader.ReadNextType();
            if ( fileType != typeof( T ) )
            {
                throw new DifferentFileTypeException( typeof( T ), fileType );
            }

            object deserializedObject = reader.ReadPainlessBinaryObject( typeof( T ) );
            return (T) deserializedObject;
        }

        void SerializeFilePayload<T>( PainlessBinaryWriter writer, T payload )
        {
            writer.WriteType( typeof( T ) );

            writer.WritePainlessBinaryObject( typeof( T ), payload );
        }
    }
}
