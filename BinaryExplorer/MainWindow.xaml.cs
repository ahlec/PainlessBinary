using System;
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

            Type r1 = typeof( Pronoun? );

            TypeManager typeManager = new TypeManager();
            typeManager.RegisterType<Person>();
            typeManager.RegisterType<Date>();
            typeManager.RegisterType<Month>();
            typeManager.RegisterType<Pronoun>();

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
                            Age = 17,
                            Birthday = new Date
                            {
                                Day = 21,
                                Month = Month.December,
                                Year = 1712
                            },
                            OtherNames = new[]
                            {
                                "Jackie",
                                "Jackson",
                                "Jokul"
                            },
                            IsHappy = true,
                            Pronoun = Pronoun.He
                        },
                        new Person
                        {
                            FirstName = "Alec",
                            LastName = "Deitloff",
                            Age = 25,
                            Birthday = new Date
                            {
                                Day = 26,
                                Month = Month.October,
                                Year = 1992
                            },
                            OtherNames = new[]
                            {
                                "Jacob"
                            },
                            IsHappy = true,
                            Pronoun = Pronoun.He
                        },
                        new Person
                        {
                            FirstName = "Katie",
                            LastName = "Holt",
                            Age = 15,
                            Birthday = new Date
                            {
                                Day = 10,
                                Month = Month.February,
                                Year = 2000
                            },
                            IsHappy = false,
                            OtherNames = new[]
                            {
                                "Pidge",
                                "Pidge Gunderson"
                            }
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
