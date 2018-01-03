// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using SonezakiMasaki.IO;

namespace SonezakiMasaki.SerializableValues
{
    internal sealed class NullableValue : ISerializableValue
    {
        readonly PropertyInfo _valuePropertyInfo;
        readonly Type _contentType;

        NullableValue( Type fullType, object value )
        {
            _valuePropertyInfo = fullType.GetProperty( "Value" );
            _contentType = fullType.GenericTypeArguments[0];
            Value = value;
        }

        public object Value { get; private set; }

        public static NullableValue Instantiate( TypeManager typeManager, Type fullType, SonezakiReader reader )
        {
            return new NullableValue( fullType, null );
        }

        public static NullableValue WrapRawValue( TypeManager typeManager, Type fullType, object value )
        {
            return new NullableValue( fullType, value );
        }

        public void Read( SonezakiReader reader )
        {
            Value = reader.ReadSonezakiObject( _contentType );
        }

        public void Write( SonezakiWriter writer )
        {
            object containedValue = _valuePropertyInfo.GetValue( Value );
            writer.WriteSonezakiObject( _contentType, containedValue );
        }
    }
}
