// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SonezakiMasaki.IO;

namespace SonezakiMasaki.SerializableValues
{
    internal sealed class EnumValue : ISerializableValue
    {
        static readonly IReadOnlyDictionary<Type, ReadUnderlyingTypeFunction> _readFunctions = new Dictionary<Type, ReadUnderlyingTypeFunction>
        {
            { typeof( sbyte ), reader => reader.ReadSByte() },
            { typeof( byte ), reader => reader.ReadByte() },
            { typeof( short ), reader => reader.ReadInt16() },
            { typeof( ushort ), reader => reader.ReadUInt16() },
            { typeof( int ), reader => reader.ReadInt32() },
            { typeof( uint ), reader => reader.ReadUInt32() },
            { typeof( long ), reader => reader.ReadInt64() },
            { typeof( ulong ), reader => reader.ReadUInt64() }
        };

        static readonly IReadOnlyDictionary<Type, WriteUnderlyingTypeFunction> _writeFunctions = new Dictionary<Type, WriteUnderlyingTypeFunction>
        {
            { typeof( sbyte ), ( writer, value ) => writer.Write( (sbyte) value ) },
            { typeof( byte ), ( writer, value ) => writer.Write( (byte) value ) },
            { typeof( short ), ( writer, value ) => writer.Write( (short) value ) },
            { typeof( ushort ), ( writer, value ) => writer.Write( (ushort) value ) },
            { typeof( int ), ( writer, value ) => writer.Write( (int) value ) },
            { typeof( uint ), ( writer, value ) => writer.Write( (uint) value ) },
            { typeof( long ), ( writer, value ) => writer.Write( (long) value ) },
            { typeof( ulong ), ( writer, value ) => writer.Write( (ulong) value ) }
        };

        readonly Type _enumType;
        readonly Type _underlyingType;

        EnumValue( Type enumType, object defaultValue )
        {
            _enumType = enumType;
            _underlyingType = enumType.GetEnumUnderlyingType();
            Value = defaultValue;
        }

        delegate object ReadUnderlyingTypeFunction( SonezakiReader reader );

        delegate void WriteUnderlyingTypeFunction( SonezakiWriter writer, object underlyingValue );

        public object Value { get; private set; }

        public static ValueInstantiator CreateInstantiator( Type enumType )
        {
            object defaultValue = Enum.ToObject( enumType, 0 );
            return ( typeManager, fullType, reader ) => new EnumValue( enumType, defaultValue );
        }

        public static ValueWrapper CreateWrapper( Type enumType )
        {
            return ( typeManager, value ) => new EnumValue( enumType, value );
        }

        public void Read( SonezakiReader reader )
        {
            ReadUnderlyingTypeFunction readFunction = _readFunctions[_underlyingType];
            object underlyingValue = readFunction( reader );
            Value = Enum.ToObject( _enumType, underlyingValue );
        }

        public void Write( SonezakiWriter writer )
        {
            WriteUnderlyingTypeFunction writeFunction = _writeFunctions[_underlyingType];
            object underlyingValue = Convert.ChangeType( Value, _underlyingType );
            writeFunction( writer, underlyingValue );
        }
    }
}
