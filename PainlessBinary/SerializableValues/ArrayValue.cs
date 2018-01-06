// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using PainlessBinary.IO;

namespace PainlessBinary.SerializableValues
{
    internal sealed class ArrayValue : ISerializableValue
    {
        readonly Type _elementType;
        readonly int _rank;
        readonly IList _array;
        readonly int _arrayLength;

        ArrayValue( Type arrayType, IList array, int arrayLength )
        {
            _elementType = arrayType.GetElementType();
            _rank = arrayType.GetArrayRank();
            _array = array;
            _arrayLength = arrayLength;
        }

        public object Value => _array;

        public static ArrayValue Instantiate( TypeManager typeManager, Type fullType, PainlessBinaryReader reader )
        {
            int arrayLength = reader.ReadInt32();
            IList array = (IList) Activator.CreateInstance( fullType, arrayLength );
            return new ArrayValue( fullType, array, arrayLength );
        }

        public static ArrayValue WrapRawValue( TypeManager typeManager, Type fullType, object value )
        {
            IList array = (IList) value;
            return new ArrayValue( fullType, array, array.Count );
        }

        public void Read( PainlessBinaryReader reader )
        {
            if ( _rank == 1 )
            {
                ReadIntoArray( reader, _array );
                return;
            }

            throw new NotImplementedException();
        }

        public void Write( PainlessBinaryWriter writer )
        {
            writer.Write( _arrayLength );

            for ( int rank = 0; rank < _rank; ++rank )
            {
                for ( int index = 0; index < _arrayLength; ++index )
                {
                    writer.WritePainlessBinaryObject( _elementType, _array[index] );
                }
            }
        }

        void ReadIntoArray( PainlessBinaryReader reader, IList destination )
        {
            for ( int index = 0; index < _arrayLength; ++index )
            {
                destination[index] = reader.ReadPainlessBinaryObject( _elementType );
            }
        }
    }
}