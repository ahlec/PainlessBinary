// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SonezakiMasaki.Containers
{
    internal abstract class Container : ISerializableValue
    {
        IReadOnlyList<ISerializableValue> _containedValues;

        public abstract ContainerId Id { get; }

        public void ReadHeader( BinaryReader reader, ObjectSerializer objectSerializer )
        {
            _containedValues = ReadContainedValues( reader, objectSerializer ).ToList();
        }

        public object Read( BinaryReader reader )
        {
            foreach ( ISerializableValue value in _containedValues )
            {
                object deserializedValue = value.Read( reader );
                AddItem( deserializedValue );
            }

            return FinalValue;
        }

        protected abstract object FinalValue { get; }

        /// <summary>
        /// Called after the container and the type have been fully deserialized, and actual data is
        /// about to be deserialized from the reader. This gives the container a chance to do any preparation
        /// work that is necessary before <seealso cref="AddItem"/> will begin to be called.
        /// </summary>
        /// <param name="typeInfo">The type of the data that this container contains.</param>
        void Prepare( ISerializableValue typeInfo );

        protected abstract IEnumerable<ISerializableValue> ReadContainedValues( BinaryReader reader, ObjectSerializer objectSerializer );

        protected abstract void AddItem( object item );
    }
}
