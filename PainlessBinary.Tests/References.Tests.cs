// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
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
                },
                Characters = new[]
                {
                    new Character { Name = "Kyle" },
                    new Character { Name = "Tara" }
                }
            };

            GameData deserialized = SerializeDeserializeGameData( original );

            Assert.That( deserialized.Items.Length, Is.EqualTo( 2 ) );
            Assert.That( deserialized.Items[0], Is.Not.SameAs( deserialized.Items[1] ) );
            Assert.That( deserialized.Characters.Length, Is.EqualTo( 2 ) );
            Assert.That( deserialized.Characters[0], Is.Not.SameAs( deserialized.Characters[1] ) );
        }

        [Test]
        public void References_EqualsDeserializesAsSameObject()
        {
            const string ItemName = "Potion";
            GameData deserialized = SerializeDeserializeGameData( new GameData
            {
                Items = new[]
                {
                    new Item { Name = ItemName },
                    new Item { Name = ItemName },
                    new Item { Name = ItemName }
                }
            } );

            Assert.That( deserialized.Items.Length, Is.EqualTo( 3 ) );
            Assert.That( deserialized.Items[0], Is.SameAs( deserialized.Items[1] ) );
            Assert.That( deserialized.Items[0], Is.SameAs( deserialized.Items[2] ) );
        }

        [Test]
        public void References_ReferenceEqualsDeserializesAsSameObject()
        {
            Character character = new Character { Name = "Rya" };
            GameData deserialized = SerializeDeserializeGameData( new GameData
            {
                Characters = new[] { character, character, character }
            } );

            Assert.That( deserialized.Characters.Length, Is.EqualTo( 3 ) );
            Assert.That( deserialized.Characters[0], Is.SameAs( deserialized.Characters[1] ) );
            Assert.That( deserialized.Characters[0], Is.SameAs( deserialized.Characters[2] ) );
        }

        [Test]
        public void References_ReferenceEqualsDoesntCatchObjectsWithSameData()
        {
            GameData deserialized = SerializeDeserializeGameData( new GameData
            {
                Characters = new[]
                {
                    new Character(),
                    new Character()
                }
            } );

            Assert.That( deserialized.Characters.Length, Is.EqualTo( 2 ) );
            Assert.That( deserialized.Characters[0], Is.Not.SameAs( deserialized.Characters[1] ) );
        }

        [Test]
        public void References_UsableAcrossMultipleObjects()
        {
            Item potion = new Item { Name = "Potion" };

            GameData deserialized = SerializeDeserializeGameData( new GameData
            {
                Items = new[] { potion },
                Characters = new[]
                {
                    new Character { Inventory = new List<Item> { potion } },
                    new Character { Inventory =new List<Item> { potion } }
                }
            } );

            Assert.That( deserialized.Items.Length, Is.EqualTo( 1 ) );
            Assert.That( deserialized.Characters.Length, Is.EqualTo( 2 ) );
            Assert.That( deserialized.Characters[0].Inventory.Count, Is.EqualTo( 1 ) );
            Assert.That( deserialized.Characters[0].Inventory[0], Is.SameAs( deserialized.Items[0] ) );
            Assert.That( deserialized.Characters[1].Inventory.Count, Is.EqualTo( 1 ) );
            Assert.That( deserialized.Characters[1].Inventory[0], Is.SameAs( deserialized.Items[0] ) );
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
