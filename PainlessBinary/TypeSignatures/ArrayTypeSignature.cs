// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using PainlessBinary.IO;

namespace PainlessBinary.TypeSignatures
{
    internal sealed class ArrayTypeSignature : ITypeSignature
    {
        readonly uint _id;

        ArrayTypeSignature( uint id )
        {
            _id = id;
        }

        public static ArrayTypeSignature Create( uint id, Type baseType )
        {
            return new ArrayTypeSignature( id );
        }

        public Type Read( SonezakiReader reader )
        {
            Type elementType = reader.ReadNextType();
            int rank = reader.ReadInt32();

            if ( rank == 1 )
            {
                return elementType.MakeArrayType();
            }

            return elementType.MakeArrayType( rank );
        }

        public void Write( SonezakiWriter writer, Type fullType )
        {
            writer.Write( _id );

            Type elementType = fullType.GetElementType();
            writer.WriteType( elementType );

            int rank = fullType.GetArrayRank();
            writer.Write( rank );
        }
    }
}
