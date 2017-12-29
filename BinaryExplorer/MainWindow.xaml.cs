using System.Collections.Generic;
using System.IO;
using SonezakiMasaki;

namespace BinaryExplorer
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            const string Filename = @"test-data.bin";

            TypeManager typeManager = new TypeManager();
            typeManager.RegisterType<Person>();

            Serializer serializer = new Serializer( typeManager );

            CreateTestFile( serializer, Filename );

            SerializationFile<List<Person>> file = DeserializeFile<List<Person>>( serializer, Filename );

            file.ToString();
        }

        static void CreateTestFile( Serializer serializer, string filename )
        {
            using ( Stream fileStream = File.Create( filename ) )
            {
                serializer.SerializeFile( fileStream, new SerializationFile<List<Person>>
                {
                    Payload = new List<Person>
                    {
                        new Person
                        {
                            FirstName = "Jack",
                            LastName = "Frost",
                            Age = 17
                        },
                        new Person
                        {
                            FirstName = "Alec",
                            LastName = "Deitloff",
                            Age = 25
                        }
                    }
                } );
            }

            filename.ToString();
        }

        SerializationFile<T> DeserializeFile<T>( Serializer serializer, string filename )
        {
            using ( Stream readStream = File.OpenRead( filename ) )
            {
                return serializer.DeserializeFile<T>( readStream );
            }
        }
    }
}
