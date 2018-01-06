// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;
using NUnit.Framework;
using PainlessBinary.Tests.ExampleTypes;

namespace PainlessBinary.Tests
{
    public sealed class ComplexTypesTests : SonezakiUnitTestBase
    {
        [Test]
        public void ComplexTypes_CanBeNull()
        {
            GameData deserialized = SerializeDeserializeGameData( null );
            Assert.That( deserialized, Is.Null );
        }

        [Test]
        public void ComplexTypes_CanBeFilledWithNull()
        {
            GameData deserialized = SerializeDeserializeGameData( new GameData() );
            Assert.That( deserialized.DateGameDataCompiled, Is.Null );
            Assert.That( deserialized.Characters, Is.Null );
            Assert.That( deserialized.Items, Is.Null );
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
