// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using SonezakiMasaki.IO;

namespace SonezakiMasaki.SerializableValues
{
    internal sealed class ListValue : ISerializableValue
    {
        readonly TypeManager _typeManager;
        readonly Type _contentType;
        readonly IList _list;
        readonly int _listLength;

        ListValue( TypeManager typeManager, Type listType, IList list, int listLength )
        {
            _typeManager = typeManager;
            _contentType = listType.GenericTypeArguments[0];
            _list = list;
            _listLength = listLength;
        }

        public object Value => _list;

        public static ListValue Instantiate( TypeManager typeManager, Type fullType, SonezakiReader reader )
        {
            int listLength = reader.ReadInt32();
            IList list = (IList) Activator.CreateInstance( fullType, listLength );
            return new ListValue( typeManager, fullType, list, listLength );
        }

        public static ListValue WrapRawValue( TypeManager typeManager, Type fullType, object value )
        {
            IList list = (IList) value;
            return new ListValue( typeManager, fullType, list, list.Count );
        }

        public void Read( SonezakiReader reader )
        {
            for ( int index = 0; index < _listLength; ++index )
            {
                ISerializableValue itemSerializableValue = _typeManager.Instantiate( _contentType, reader );
                itemSerializableValue.Read( reader );
                _list.Add( itemSerializableValue.Value );
            }
        }

        public void Write( SonezakiWriter writer )
        {
            writer.Write( _list.Count );

            for ( int index = 0; index < _listLength; ++index )
            {
                ISerializableValue itemSerializableValue = _typeManager.WrapRawValue( _contentType, _list[index] );
                itemSerializableValue.Write( writer );
            }
        }
    }
}
