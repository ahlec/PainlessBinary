// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using PainlessBinary.IO;

namespace PainlessBinary.SerializableValues
{
    internal sealed class ListValue : ISerializableValue
    {
        readonly Type _contentType;
        readonly IList _list;
        readonly int _listLength;

        ListValue( Type listType, IList list, int listLength )
        {
            _contentType = listType.GenericTypeArguments[0];
            _list = list;
            _listLength = listLength;
        }

        public object Value => _list;

        public static ListValue Instantiate( TypeManager typeManager, Type fullType, PainlessBinaryReader reader )
        {
            int listLength = reader.ReadInt32();
            IList list = (IList) Activator.CreateInstance( fullType, listLength );
            return new ListValue( fullType, list, listLength );
        }

        public static ListValue WrapRawValue( TypeManager typeManager, Type fullType, object value )
        {
            IList list = (IList) value;
            return new ListValue( fullType, list, list.Count );
        }

        public void Read( PainlessBinaryReader reader )
        {
            for ( int index = 0; index < _listLength; ++index )
            {
                object value = reader.ReadPainlessBinaryObject( _contentType );
                _list.Add( value );
            }
        }

        public void Write( PainlessBinaryWriter writer )
        {
            writer.Write( _listLength );

            for ( int index = 0; index < _listLength; ++index )
            {
                writer.WritePainlessBinaryObject( _contentType, _list[index] );
            }
        }
    }
}
