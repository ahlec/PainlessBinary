﻿// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using PainlessBinary.IO;

namespace PainlessBinary.SerializableValues
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
            return ( typeManager, fullType, value ) => new BuiltInValue<T>( readWriteOperations, (T) value );
        }

        public void Read( PainlessBinaryReader reader )
        {
            _value = _readWriteOperations.ReadFunction( reader );
        }

        public void Write( PainlessBinaryWriter writer )
        {
            _readWriteOperations.WriteFunction( writer, _value );
        }
    }
}
