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
        readonly ObjectSerializer _objectSerializer;

        public Serializer( TypeManager typeManager )
        {
            _typeManager = typeManager ?? throw new ArgumentNullException( nameof( typeManager ) );
            _objectSerializer = new ObjectSerializer( typeManager );
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
                using ( SonezakiWriter writer = new SonezakiWriter( streamWrapper, HashBaseValue, HashMultiplicationConstant ) )
                {
                    SerializeFilePayload( writer, file.Payload );
                }
            }
        }

        public SerializationFile<T> DeserializeFile<T>( Stream dataStream )
        {
            using ( SonezakiStreamWrapper streamWrapper = new SonezakiStreamWrapper( dataStream ) )
            {
                using ( SonezakiReader reader = new SonezakiReader( streamWrapper, HashBaseValue, HashMultiplicationConstant ) )
                {
                    T payload = DeserializeFilePayload<T>( reader );
                    return new SerializationFile<T>
                    {
                        Payload = payload
                    };
                }
            }
        }

        void SerializeFilePayload<T>( SonezakiWriter writer, T payload )
        {
            _objectSerializer.WriteType( writer, typeof( T ) );

            ISerializableValue wrappedValue = _typeManager.WrapRawValue( typeof( T ), payload );
            wrappedValue.Write( writer, _objectSerializer );
        }

        T DeserializeFilePayload<T>( SonezakiReader reader )
        {
            Type fileType = _objectSerializer.ReadNextType( reader );
            if ( fileType != typeof( T ) )
            {
                throw new DifferentFileTypeException( typeof( T ), fileType );
            }

            ISerializableValue value = _typeManager.Instantiate( fileType, reader );
            value.Read( reader, _objectSerializer );
            return (T) value.Value;
        }
    }
}
