// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace PainlessBinary.IO
{
    internal static class StandardReadWriteOperations
    {
        public static ReadWriteOperations<bool> Boolean { get; } = new ReadWriteOperations<bool>( reader => reader.ReadBoolean(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<byte> Byte { get; } = new ReadWriteOperations<byte>( reader => reader.ReadByte(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<sbyte> SByte { get; } = new ReadWriteOperations<sbyte>( reader => reader.ReadSByte(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<char> Char { get; } = new ReadWriteOperations<char>( reader => reader.ReadChar(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<decimal> Decimal { get; } = new ReadWriteOperations<decimal>( reader => reader.ReadDecimal(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<double> Double { get; } = new ReadWriteOperations<double>( reader => reader.ReadDouble(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<float> Single { get; } = new ReadWriteOperations<float>( reader => reader.ReadSingle(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<int> Int32 { get; } = new ReadWriteOperations<int>( reader => reader.ReadInt32(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<uint> UInt32 { get; } = new ReadWriteOperations<uint>( reader => reader.ReadUInt32(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<long> Int64 { get; } = new ReadWriteOperations<long>( reader => reader.ReadInt64(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<ulong> UInt64 { get; } = new ReadWriteOperations<ulong>( reader => reader.ReadUInt64(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<short> Int16 { get; } = new ReadWriteOperations<short>( reader => reader.ReadInt16(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<ushort> UInt16 { get; } = new ReadWriteOperations<ushort>( reader => reader.ReadUInt16(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<string> String { get; } = new ReadWriteOperations<string>( reader => reader.ReadString(), ( writer, value ) => writer.Write( value ) );

        public static ReadWriteOperations<DateTime> DateTime { get; } = new ReadWriteOperations<DateTime>( ReadDateTime, WriteDateTime );

        public static ReadWriteOperations<Guid> Guid { get; } = new ReadWriteOperations<Guid>( ReadGuid, WriteGuid );

        public static ReadWriteOperations<TimeSpan> TimeSpan { get; } = new ReadWriteOperations<TimeSpan>( ReadTimeSpan, WriteTimeSpan );

        public static ReadWriteOperations<DateTimeOffset> DateTimeOffset { get; } = new ReadWriteOperations<DateTimeOffset>( ReadDateTimeOffset, WriteDateTimeOffset );

        public static ReadWriteOperations<Uri> Uri { get; } = new ReadWriteOperations<Uri>( ReadUri, WriteUri );

        static DateTime ReadDateTime( PainlessBinaryReader reader )
        {
            long binary = reader.ReadInt64();
            return System.DateTime.FromBinary( binary );
        }

        static void WriteDateTime( PainlessBinaryWriter writer, DateTime value )
        {
            writer.Write( value.ToBinary() );
        }

        static Guid ReadGuid( PainlessBinaryReader reader )
        {
            const int GuidByteArrayLength = 16;
            byte[] byteArray = new byte[GuidByteArrayLength];
            int numBytesRead = reader.Read( byteArray, 0, GuidByteArrayLength );
            if ( numBytesRead != GuidByteArrayLength )
            {
                throw new InvalidDataException( "Could not read the 16 bytes required for a Guid." );
            }

            return new Guid( byteArray );
        }

        static void WriteGuid( PainlessBinaryWriter writer, Guid value )
        {
            byte[] byteArray = value.ToByteArray();
            writer.Write( byteArray );
        }

        static TimeSpan ReadTimeSpan( PainlessBinaryReader reader )
        {
            long ticks = reader.ReadInt64();
            return System.TimeSpan.FromTicks( ticks );
        }

        static void WriteTimeSpan( PainlessBinaryWriter writer, TimeSpan value )
        {
            writer.Write( value.Ticks );
        }

        static DateTimeOffset ReadDateTimeOffset( PainlessBinaryReader reader )
        {
            string value = reader.ReadString();
            return System.DateTimeOffset.Parse( value );
        }

        static void WriteDateTimeOffset( PainlessBinaryWriter writer, DateTimeOffset value )
        {
            string dataRepresentation = value.ToString( "yyyy-MM-ddTHH:mm:ss.fffffffzzz" );
            writer.Write( dataRepresentation );
        }

        static Uri ReadUri( PainlessBinaryReader reader )
        {
            string value = reader.ReadString();
            return new Uri( value );
        }

        static void WriteUri( PainlessBinaryWriter writer, Uri value )
        {
            writer.Write( value.ToString() );
        }
    }
}
