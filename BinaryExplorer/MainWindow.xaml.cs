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
            CreateTestFile( Filename );

            TypeManager typeManager = new TypeManager();
            Serializer serializer = new Serializer( typeManager );
            SerializationFile<List<List<int>>> file = DeserializeFile<List<List<int>>>( serializer, Filename );

            List<int> today = file.Payload[0];
            List<int> lastLeapDay = file.Payload[1];
            List<int> favouriteNumbers = file.Payload[2];

            file.ToString();
        }

        static void CreateTestFile( string filename )
        {
            using ( Stream fileStream = File.Create( filename ) )
            {
                using ( BinaryWriter writer = new BinaryWriter( fileStream ) )
                {
                    writer.Write( TypeIdList );
                    writer.Write( TypeIdList );
                    writer.Write( TypeIdInt );

                    writer.Write( 3 );

                        writer.Write( 3 );

                            writer.Write( 28 );
                            writer.Write( 12 );
                            writer.Write( 2017 );

                        writer.Write( 3 );

                            writer.Write( 29 );
                            writer.Write( 2 );
                            writer.Write( 2016 );

                        writer.Write( 1 );

                            writer.Write( 17 );
                }
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
