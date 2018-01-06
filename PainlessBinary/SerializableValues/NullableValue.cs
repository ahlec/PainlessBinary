// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using PainlessBinary.IO;

namespace PainlessBinary.SerializableValues
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

        public static NullableValue Instantiate( TypeManager typeManager, Type fullType, PainlessBinaryReader reader )
        {
            return new NullableValue( fullType, null );
        }

        public static NullableValue WrapRawValue( TypeManager typeManager, Type fullType, object value )
        {
            return new NullableValue( fullType, value );
        }

        public void Read( PainlessBinaryReader reader )
        {
            Value = reader.ReadPainlessBinaryObject( _contentType );
        }

        public void Write( PainlessBinaryWriter writer )
        {
            object containedValue = _valuePropertyInfo.GetValue( Value );
            writer.WritePainlessBinaryObject( _contentType, containedValue );
        }
    }
}
