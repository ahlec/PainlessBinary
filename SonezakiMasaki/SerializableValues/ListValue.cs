// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.IO;

namespace SonezakiMasaki.SerializableValues
{
    internal sealed class ListValue : ISerializableValue
    {
        readonly Type _listType;
        readonly TypeInstantiator _typeInstantiator;
        readonly int _listLength;

        public ListValue( Type listType, TypeInstantiator typeInstantiator, int length )
        {
            _listType = listType;
            _typeInstantiator = typeInstantiator;
            _listLength = length;
        }

        public object Read( BinaryReader reader, ObjectSerializer objectSerializer )
        {
            IList list = (IList) Activator.CreateInstance( _listType, _listLength );

            for ( int index = 0; index < _listLength; ++index )
            {
                ISerializableValue value = _typeInstantiator.Instantiate( _listType.GenericTypeArguments[0], reader );
                object deserializedValue = value.Read( reader, objectSerializer );
                list.Add( deserializedValue );
            }

            return list;
        }
    }
}
