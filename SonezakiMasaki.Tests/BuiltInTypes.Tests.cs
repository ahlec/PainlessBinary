// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using NUnit.Framework;

namespace SonezakiMasaki.Tests
{
    [TestFixture]
    public sealed class BuiltInTypes : SonezakiUnitTestBase
    {
        [Test]
        public void BuiltInType_Boolean()
        {
            RunBuiltInTypeTest<bool>( true );
            RunBuiltInTypeTest<bool>( false );
        }

        [Test]
        public void BuiltInType_Byte()
        {
            RunBuiltInTypeTest<byte>( byte.MinValue );
            RunBuiltInTypeTest<byte>( byte.MaxValue );
            RunBuiltInTypeTest<byte>( 17 );
        }

        [Test]
        public void BuiltInType_SByte()
        {
            RunBuiltInTypeTest<sbyte>( 0 );
            RunBuiltInTypeTest<sbyte>( sbyte.MinValue );
            RunBuiltInTypeTest<sbyte>( sbyte.MaxValue );
            RunBuiltInTypeTest<sbyte>( 23 );
            RunBuiltInTypeTest<sbyte>( -48 );
        }

        [Test]
        public void BuiltInType_Char()
        {
            RunBuiltInTypeTest<char>( (char) 0 );
            RunBuiltInTypeTest<char>( 'A' );
            RunBuiltInTypeTest<char>( '川' );
            RunBuiltInTypeTest<char>( '%' );
        }

        [Test]
        public void BuiltInType_Decimal()
        {
            RunBuiltInTypeTest<decimal>( 0M );
            RunBuiltInTypeTest<decimal>( decimal.MinValue );
            RunBuiltInTypeTest<decimal>( decimal.MaxValue );
            RunBuiltInTypeTest<decimal>( 3.141592653M );
        }

        [Test]
        public void BuiltInType_Double()
        {
            RunBuiltInTypeTest<double>( 0D );
            RunBuiltInTypeTest<double>( double.MinValue );
            RunBuiltInTypeTest<double>( double.MaxValue );
            RunBuiltInTypeTest<double>( -43D );
            RunBuiltInTypeTest<double>( 6.99878D );
        }

        [Test]
        public void BuiltInType_Single()
        {
            RunBuiltInTypeTest<float>( 0f );
            RunBuiltInTypeTest<float>( float.MinValue );
            RunBuiltInTypeTest<float>( float.MaxValue );
            RunBuiltInTypeTest<float>( 0.000112f );
            RunBuiltInTypeTest<float>( -99f );
        }

        [Test]
        public void BuiltInType_Int32()
        {
            RunBuiltInTypeTest<int>( 0 );
            RunBuiltInTypeTest<int>( int.MinValue );
            RunBuiltInTypeTest<int>( int.MaxValue );
            RunBuiltInTypeTest<int>( -10000 );
            RunBuiltInTypeTest<int>( 17 );
        }

        [Test]
        public void BuiltInType_UInt32()
        {
            RunBuiltInTypeTest<uint>( uint.MinValue );
            RunBuiltInTypeTest<uint>( uint.MaxValue );
            RunBuiltInTypeTest<uint>( 65535U );
        }

        [Test]
        public void BuiltInType_Int64()
        {
            RunBuiltInTypeTest<long>( 0L );
            RunBuiltInTypeTest<long>( long.MinValue );
            RunBuiltInTypeTest<long>( long.MaxValue );
            RunBuiltInTypeTest<long>( int.MaxValue + 1L );
        }

        [Test]
        public void BuiltInType_UInt64()
        {
            RunBuiltInTypeTest<ulong>( ulong.MinValue );
            RunBuiltInTypeTest<ulong>( ulong.MaxValue );
            RunBuiltInTypeTest<ulong>( long.MaxValue + 1UL );
        }

        [Test]
        public void BuiltInType_Int16()
        {
            RunBuiltInTypeTest<short>( 0 );
            RunBuiltInTypeTest<short>( short.MinValue );
            RunBuiltInTypeTest<short>( short.MaxValue );
            RunBuiltInTypeTest<short>( 27 );
            RunBuiltInTypeTest<short>( -98 );
        }

        [Test]
        public void BuiltInType_UInt16()
        {
            RunBuiltInTypeTest<ushort>( ushort.MinValue );
            RunBuiltInTypeTest<ushort>( ushort.MaxValue );
            RunBuiltInTypeTest<ushort>( 40 );
        }

        [Test]
        public void BuiltInType_String()
        {
            RunBuiltInTypeTest<string>( null );
            RunBuiltInTypeTest<string>( string.Empty );
            RunBuiltInTypeTest<string>( "h" );
            RunBuiltInTypeTest<string>( "hi" );
            RunBuiltInTypeTest<string>( "hello" );
        }

        [Test]
        public void BuiltInType_DateTime()
        {
            RunBuiltInTypeTest<DateTime>( DateTime.MinValue );
            RunBuiltInTypeTest<DateTime>( DateTime.MaxValue );
            RunBuiltInTypeTest<DateTime>( DateTime.Now );
            RunBuiltInTypeTest<DateTime>( DateTime.UtcNow );
            RunBuiltInTypeTest<DateTime>( DateTime.Today );
        }

        [Test]
        public void BuiltInType_Guid()
        {
            RunBuiltInTypeTest<Guid>( Guid.Empty );
            RunBuiltInTypeTest<Guid>( Guid.NewGuid() );
            RunBuiltInTypeTest<Guid>( new Guid( 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 ) );
        }

        [Test]
        public void BuiltInType_TimeSpan()
        {
            RunBuiltInTypeTest<TimeSpan>( TimeSpan.MinValue );
            RunBuiltInTypeTest<TimeSpan>( TimeSpan.MaxValue );
            RunBuiltInTypeTest<TimeSpan>( TimeSpan.Zero );
            RunBuiltInTypeTest<TimeSpan>( DateTime.UtcNow - DateTime.Now );
        }

        [Test]
        public void BuiltInType_DateTimeOffset()
        {
            RunBuiltInTypeTest<DateTimeOffset>( DateTimeOffset.MinValue );
            RunBuiltInTypeTest<DateTimeOffset>( DateTimeOffset.MaxValue );
            RunBuiltInTypeTest<DateTimeOffset>( DateTimeOffset.Now );
            RunBuiltInTypeTest<DateTimeOffset>( DateTimeOffset.UtcNow );
        }

        [Test]
        public void BuiltInType_Uri()
        {
            RunBuiltInTypeTest<Uri>( new Uri( "https://www.google.com/" ) );
            RunBuiltInTypeTest<Uri>( new Uri( "ftp://www.dropbox.com/" ) );
            RunBuiltInTypeTest<Uri>( new Uri( "c:/my/files/myfile.txt" ) );
        }

        static void RunBuiltInTypeTest<T>( T value )
        {
            Serializer serializer = CreateSerializer();
            byte[] streamBuffer = new byte[50];

            using ( MemoryStream serializeStream = new MemoryStream( streamBuffer, true ) )
            {
                serializer.SerializeFile( serializeStream, new SerializationFile<T>
                {
                    Payload = value
                } );
            }

            using ( MemoryStream deserializeStream = new MemoryStream( streamBuffer ) )
            {
                T deserializedValue = serializer.DeserializeFile<T>( deserializeStream ).Payload;
                Assert.That( deserializedValue, Is.EqualTo( value ) );
            }
        }
    }
}
