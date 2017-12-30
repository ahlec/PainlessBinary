// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace SonezakiMasaki.IO
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

        public static ReadWriteOperations<string> String { get; } = new ReadWriteOperations<string>( ReadNullableString, WriteNullableString );

        static string ReadNullableString( SonezakiReader reader )
        {
            bool hasValue = reader.ReadBoolean();

            if ( !hasValue )
            {
                return null;
            }

            return reader.ReadString();
        }

        static void WriteNullableString( SonezakiWriter writer, string value )
        {
            bool hasValue = ( value != null );
            writer.Write( hasValue );

            if ( !hasValue )
            {
                return;
            }

            writer.Write( value );
        }
    }
}
