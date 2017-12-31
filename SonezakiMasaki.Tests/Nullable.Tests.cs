// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;
using NUnit.Framework;
using SonezakiMasaki.Exceptions;
using SonezakiMasaki.Tests.ExampleTypes;

namespace SonezakiMasaki.Tests
{
    public sealed class NullableTests : SonezakiUnitTestBase
    {
        /// <summary>
        /// Tests to make sure that if we serialize int?, we get an error trying to deserialize it as regular int.
        /// </summary>
        [Test]
        public void Nullable_CannotIntermixNullableAndNonNullable()
        {
            Serializer serializer = CreateSerializer();
            byte[] streamBuffer = new byte[20];

            using ( MemoryStream serializeStream = new MemoryStream( streamBuffer, true ) )
            {
                serializer.SerializeFile( serializeStream, new SerializationFile<int?>
                {
                    Payload = 23
                } );
            }

            using ( MemoryStream deserializeStream = new MemoryStream( streamBuffer ) )
            {
                Assert.Throws<DifferentFileTypeException>( () => serializer.DeserializeFile<int>( deserializeStream ) );
            }
        }

        [Test]
        public void Nullable_IntIsNull()
        {
            int? deserialized = SerializeDeserializeNullableValue<int>( null );
            Assert.That( deserialized, Is.Null );
        }

        [Test]
        public void Nullable_IntWithValue()
        {
            const int IntValue = 17;
            int? deserialized = SerializeDeserializeNullableValue<int>( IntValue );
            Assert.That( deserialized, Is.Not.Null );
            Assert.That( deserialized, Is.EqualTo( IntValue ) );
        }

        [Test]
        public void Nullable_MonthIsNull()
        {
            Month? deserialized = SerializeDeserializeNullableValue<Month>( null );
            Assert.That( deserialized, Is.Null );
        }

        [Test]
        public void Nullable_MonthWithValue()
        {
            const Month MonthValue = Month.April;
            Month? deserialized = SerializeDeserializeNullableValue<Month>( MonthValue );
            Assert.That( deserialized, Is.Not.Null );
            Assert.That( deserialized, Is.EqualTo( MonthValue ) );
        }

        static T? SerializeDeserializeNullableValue<T>( T? value )
            where T : struct
        {
            Serializer serializer = CreateSerializer();
            byte[] streamBuffer = new byte[20];

            using ( MemoryStream serializeStream = new MemoryStream( streamBuffer, true ) )
            {
                serializer.SerializeFile( serializeStream, new SerializationFile<T?>
                {
                    Payload = value
                } );
            }

            using ( MemoryStream deserializeStream = new MemoryStream( streamBuffer ) )
            {
                T? deserializedValue = serializer.DeserializeFile<T?>( deserializeStream ).Payload;
                return deserializedValue;
            }
        }
    }
}
