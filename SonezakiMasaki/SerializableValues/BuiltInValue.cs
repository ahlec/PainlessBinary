// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using SonezakiMasaki.IO;

namespace SonezakiMasaki.SerializableValues
{
    internal sealed class BuiltInValue<T> : ISerializableValue
    {
        readonly ReadWriteOperations<T> _readWriteOperations;
        T _value;

        BuiltInValue( ReadWriteOperations<T> readWriteOperations, T defaultValue )
        {
            _readWriteOperations = readWriteOperations;
            _value = defaultValue;
        }

        public object Value => _value;

        public static ValueInstantiator CreateInstantiator( ReadWriteOperations<T> readWriteOperations )
        {
            return ( typeManager, fullType, reader ) => new BuiltInValue<T>( readWriteOperations, default( T ) );
        }

        public static ValueWrapper CreateWrapper( ReadWriteOperations<T> readWriteOperations )
        {
            return ( typeManager, value ) => new BuiltInValue<T>( readWriteOperations, (T) value );
        }

        public void Read( SonezakiReader reader, ObjectSerializer objectSerializer )
        {
            _value = _readWriteOperations.ReadFunction( reader );
        }

        public void Write( SonezakiWriter writer, ObjectSerializer objectSerializer )
        {
            _readWriteOperations.WriteFunction( writer, _value );
        }
    }
}
