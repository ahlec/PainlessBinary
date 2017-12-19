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
    internal sealed class ListContainer : Container
    {
        int _listLength;
        IList _list;

        public override ContainerId Id => ContainerId.List;

        protected override object FinalValue => _list;

        /// <inheritdoc />
        protected override IEnumerable<ISerializableValue> ReadContainedValues( BinaryReader reader, ObjectSerializer objectSerializer )
        {
            _listLength = reader.ReadInt32();
            ITypeDefinition typeDefinition = objectSerializer.ReadNextTypeDefinition( reader );

            for ( int index = 0; index < _listLength; ++index )
            {
                yield return typeDefinition.CreateValue();
            }
        }

        public void Prepare( ISerializableValue typeInfo )
        {
            Type listType = typeof( List<> ).MakeGenericType( typeInfo.Type );
            _list = (IList) Activator.CreateInstance( listType, _listLength );
        }

        protected override void AddItem( object item )
        {
            if ( _list.Count >= _listLength )
            {
                throw new InvalidOperationException( "Attempting to read more items than there are in the list." );
            }

            _list.Add( item );
        }
    }
}
