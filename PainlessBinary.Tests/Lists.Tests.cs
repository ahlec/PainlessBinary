// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace PainlessBinary.Tests
{
    [TestFixture]
    public sealed class ListsTests : UnitTestBase
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

        [Test]
        public void NestedLists_CanBeNull()
        {
            List<List<int>> deserialized = SerializeDeserializeList<List<int>>( null );
            Assert.That( deserialized, Is.Null );
        }

        [Test]
        public void NestedLists_CanBeEmpty()
        {
            List<List<int>> deserialized = SerializeDeserializeList( new List<List<int>>() );
            Assert.That( deserialized, Is.Empty );

            deserialized = SerializeDeserializeList( new List<List<int>>
            {
                new List<int>()
            } );
            Assert.That( deserialized.Count, Is.EqualTo( 1 ) );
            Assert.That( deserialized[0], Is.Empty );
        }

        [Test]
        public void NestedLists_MixedNestedNullAndNotNull()
        {
            List<List<int>> deserialized = SerializeDeserializeList( new List<List<int>>
            {
                new List<int>(),
                null,
                new List<int>()
            } );
            Assert.That( deserialized.Count, Is.EqualTo( 3 ) );
            Assert.That( deserialized[0], Is.Not.Null );
            Assert.That( deserialized[1], Is.Null );
            Assert.That( deserialized[2], Is.Not.Null );
        }

        [Test]
        public void NestedLists_WithOneItem()
        {
            const int ItemValue = 17;
            List<List<int>> deserialized = SerializeDeserializeList( new List<List<int>>
            {
                new List<int>
                {
                    ItemValue
                }
            } );
            Assert.That( deserialized.Count, Is.EqualTo( 1 ) );
            Assert.That( deserialized[0].Count, Is.EqualTo( 1 ) );
            Assert.That( deserialized[0][0], Is.EqualTo( ItemValue ) );
        }

        [Test]
        public void NestedLists_WithMultipleItems()
        {
            const int ItemValue00 = 1;
            const int ItemValue01 = 22;
            const int ItemValue10 = 333;
            List<List<int>> deserialized = SerializeDeserializeList( new List<List<int>>
            {
                new List<int>
                {
                    ItemValue00,
                    ItemValue01
                },
                new List<int>
                {
                    ItemValue10
                }
            } );
            Assert.That( deserialized.Count, Is.EqualTo( 2 ) );
            Assert.That( deserialized[0].Count, Is.EqualTo( 2 ) );
            Assert.That( deserialized[0][0], Is.EqualTo( ItemValue00 ) );
            Assert.That( deserialized[0][1], Is.EqualTo( ItemValue01 ) );
            Assert.That( deserialized[1][0], Is.EqualTo( ItemValue10 ) );
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
