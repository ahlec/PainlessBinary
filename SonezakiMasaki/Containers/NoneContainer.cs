// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace SonezakiMasaki.Containers
{
    internal sealed class NoneContainer : Container
    {
        object _value;

        public override ContainerId Id => ContainerId.None;

        protected override object FinalValue => _value;

        /// <inheritdoc />
        protected override IEnumerable<ISerializableValue> ReadContainedValues( BinaryReader reader, ObjectSerializer objectSerializer )
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void AddItem( object item )
        {
            if ( _value != null )
            {
                throw new InvalidOperationException( "A value has already been deserialized." );
            }

            _value = item;
        }

        public void Prepare( ISerializableValue typeInfo )
        {
        }
    }
}
