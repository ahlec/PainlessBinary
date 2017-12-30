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
        readonly TypeManager _typeManager;
        readonly Type _fullType;
        readonly PropertyInfo _valuePropertyInfo;
        readonly Type _contentType;

        NullableValue( TypeManager typeManager, Type fullType, object value )
        {
            _typeManager = typeManager;
            _fullType = fullType;
            _valuePropertyInfo = fullType.GetProperty( "Value" );
            _contentType = fullType.GenericTypeArguments[0];
            Value = value;
        }

        public object Value { get; private set; }

        public static NullableValue Instantiate( TypeManager typeManager, Type fullType, SonezakiReader reader )
        {
            return new NullableValue( typeManager, fullType, null );
        }

        public static NullableValue WrapRawValue( TypeManager typeManager, Type fullType, object value )
        {
            return new NullableValue( typeManager, fullType, value );
        }

        public void Read( SonezakiReader reader )
        {
            bool hasValue = reader.ReadBoolean();

            if ( !hasValue )
            {
                Value = null;
                return;
            }

            ISerializableValue serializableValue = _typeManager.Instantiate( _contentType, reader );
            serializableValue.Read( reader );
            Value = serializableValue.Value;
        }

        public void Write( SonezakiWriter writer )
        {
            bool hasValue = ( Value != null );
            writer.Write( hasValue );

            if ( !hasValue )
            {
                return;
            }

            object containedValue = _valuePropertyInfo.GetValue( Value );
            ISerializableValue serializableValue = _typeManager.WrapRawValue( _contentType, containedValue );
            serializableValue.Write( writer );
        }
    }
}
