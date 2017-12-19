// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace SonezakiMasaki.Containers
{
    internal sealed class ListContainer : IContainer
    {
        int _listLength;
        IList _list;

        public object FinalValue => _list;

        public void ReadHeader( BinaryReader reader )
        {
            _listLength = reader.ReadInt32();
        }

        public void Prepare( ITypeInfo typeInfo )
        {
            Type listType = typeof( List<> ).MakeGenericType( typeInfo.Type );
            _list = (IList) Activator.CreateInstance( listType, _listLength );
        }

        public void AddItem( object item )
        {
            if ( _list.Count >= _listLength )
            {
                throw new InvalidOperationException( "Attempting to read more items than there are in the list." );
            }

            _list.Add( item );
        }
    }
}
