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
    public sealed class ArraysTests : UnitTestBase
    {
        [Test]
        public void Arrays_CanBeNull()
        {
            int[] deserialized = SerializeDeserializeArray<int>( null );
            Assert.That( deserialized, Is.Null );
        }

        [Test]
        public void Arrays_CanBeEmpty()
        {
            int[] deserialized = SerializeDeserializeArray( new int[0] );
            Assert.That( deserialized, Is.Empty );
        }

        [Test]
        public void Arrays_WithOneItem()
        {
            const int ItemValue = 17;
            int[] deserialized = SerializeDeserializeArray( new[] { ItemValue } );
            Assert.That( deserialized.Length, Is.EqualTo( 1 ) );
            Assert.That( deserialized[0], Is.EqualTo( ItemValue ) );
        }

        [Test]
        public void Arrays_WithMultipleItems()
        {
            const int ItemValue0 = 1;
            const int ItemValue1 = 277;
            const int ItemValue2 = 9000;
            int[] deserialized = SerializeDeserializeArray( new[] { ItemValue0, ItemValue1, ItemValue2 } );
            Assert.That( deserialized.Length, Is.EqualTo( 3 ) );
            Assert.That( deserialized[0], Is.EqualTo( ItemValue0 ) );
            Assert.That( deserialized[1], Is.EqualTo( ItemValue1 ) );
            Assert.That( deserialized[2], Is.EqualTo( ItemValue2 ) );
        }

        static T[] SerializeDeserializeArray<T>( T[] array )
        {
            Serializer serializer = CreateSerializer();

            using ( MemoryStream dataStream = new MemoryStream() )
            {
                serializer.SerializeFile( dataStream, new SerializationFile<T[]>
                {
                    Payload = array
                } );

                dataStream.Seek( 0, SeekOrigin.Begin );

                T[] deserialized = serializer.DeserializeFile<T[]>( dataStream ).Payload;
                return deserialized;
            }
        }
    }
}
