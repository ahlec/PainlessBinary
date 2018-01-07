// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;
using NUnit.Framework;
using PainlessBinary.Tests.ExampleTypes;

namespace PainlessBinary.Tests
{
    [TestFixture]
    public sealed class References : UnitTestBase
    {
        [Test]
        public void References_UniqueInstancesSerializeUniquely()
        {
            GameData original = new GameData
            {
                Items = new[]
                {
                    new Item { Name = "Potion" },
                    new Item { Name = "Super Potion" }
                }
            };

            GameData deserialized = SerializeDeserializeGameData( original );

            Assert.That( deserialized.Items.Length, Is.EqualTo( 2 ) );
            Assert.That( deserialized.Items[0], Is.Not.SameAs( deserialized.Items[1] ) );
        }

        [Test]
        public void References_SameArrayReferencesDeserializeAsSameObject()
        {
            Item potion = new Item { Name = "Potion" };

            GameData deserialized = SerializeDeserializeGameData( new GameData
            {
                Items = new[] { potion, potion, potion }
            } );

            Assert.That( deserialized.Items.Length, Is.EqualTo( 3 ) );
            Assert.That( deserialized.Items[0], Is.SameAs( deserialized.Items[1] ) );
            Assert.That( deserialized.Items[0], Is.SameAs( deserialized.Items[2] ) );
        }

        static GameData SerializeDeserializeGameData( GameData gameData )
        {
            Serializer serializer = CreateSerializer();
            using ( MemoryStream dataStream = new MemoryStream() )
            {
                serializer.SerializeFile( dataStream, new SerializationFile<GameData>
                {
                    Payload = gameData
                } );

                dataStream.Seek( 0, SeekOrigin.Begin );

                GameData deserialized = serializer.DeserializeFile<GameData>( dataStream ).Payload;
                return deserialized;
            }
        }
    }
}
