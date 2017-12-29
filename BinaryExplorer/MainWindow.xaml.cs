using System.Collections.Generic;
using System.IO;
using SonezakiMasaki;

namespace BinaryExplorer
{
    public partial class MainWindow
    {
        const int TypeIdInt = 8;
        const int TypeIdList = 15;

        public MainWindow()
        {
            InitializeComponent();

            const string Filename = @"test-data.bin";

            TypeManager typeManager = new TypeManager();
            Serializer serializer = new Serializer( typeManager );

            CreateTestFile( serializer, Filename );

            SerializationFile<List<List<int>>> file = DeserializeFile<List<List<int>>>( serializer, Filename );

            file.ToString();
        }

        static void CreateTestFile( Serializer serializer, string filename )
        {
            using ( Stream fileStream = File.Create( filename ) )
            {
                serializer.SerializeFile( fileStream, new SerializationFile<List<List<int>>>
                {
                    Payload = new List<List<int>>
                    {
                        new List<int>
                        {
                            1,
                            2,
                            3
                        },
                        new List<int>
                        {
                            10,
                            12,
                            14,
                            16,
                            18,
                            20
                        },
                        new List<int>
                        {
                            17
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
