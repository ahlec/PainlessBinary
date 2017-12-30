// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace SonezakiMasaki.Tests
{
    [TestFixture]
    public sealed class ListsTests : SonezakiUnitTestBase
    {
        [Test]
        public void Lists_CanBeNull()
        {
            List<int> deserialized = SerializeDeserializeList<int>( null );
            Assert.That( deserialized, Is.Null );
        }

        [Test]
        public void Lists_CanBeEmpty()
        {
            List<int> deserialized = SerializeDeserializeList( new List<int>() );
            Assert.That( deserialized, Is.Empty );
        }

        [Test]
        public void Lists_WithOneItem()
        {
            const int ItemValue = 17;
            List<int> deserialized = SerializeDeserializeList( new List<int>
            {
                ItemValue
            } );
            Assert.That( deserialized.Count, Is.EqualTo( 1 ) );
            Assert.That( deserialized[0], Is.EqualTo( ItemValue ) );
        }

        [Test]
        public void Lists_WithMultipleItems()
        {
            const int ItemValue0 = 1;
            const int ItemValue1 = 277;
            const int ItemValue2 = 9000;
            List<int> deserialized = SerializeDeserializeList( new List<int>
            {
                ItemValue0,
                ItemValue1,
                ItemValue2
            } );
            Assert.That( deserialized.Count, Is.EqualTo( 3 ) );
            Assert.That( deserialized[0], Is.EqualTo( ItemValue0 ) );
            Assert.That( deserialized[1], Is.EqualTo( ItemValue1 ) );
            Assert.That( deserialized[2], Is.EqualTo( ItemValue2 ) );
        }

        static List<T> SerializeDeserializeList<T>( List<T> list )
        {
            Serializer serializer = CreateSerializer();

            using ( MemoryStream dataStream = new MemoryStream() )
            {
                serializer.SerializeFile( dataStream, new SerializationFile<List<T>>
                {
                    Payload = list
                } );

                dataStream.Seek( 0, SeekOrigin.Begin );

                List<T> deserialized = serializer.DeserializeFile<List<T>>( dataStream ).Payload;
                return deserialized;
            }
        }
    }
}
