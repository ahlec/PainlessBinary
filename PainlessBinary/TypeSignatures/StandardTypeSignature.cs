// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using PainlessBinary.IO;

namespace PainlessBinary.TypeSignatures
{
    internal sealed class StandardTypeSignature : ITypeSignature
    {
        readonly uint _id;
        readonly Type _baseType;

        StandardTypeSignature( uint id, Type baseType )
        {
            _id = id;
            _baseType = baseType;
        }

        public static StandardTypeSignature Create( uint id, Type baseType )
        {
            return new StandardTypeSignature( id, baseType );
        }

        public Type Read( PainlessBinaryReader reader )
        {
            int numGenericParameters = CountNumberGenericParameters( _baseType );
            if ( numGenericParameters == 0 )
            {
                return _baseType;
            }

            return ResolveGenericArguments( reader, _baseType, numGenericParameters );
        }

        public void Write( PainlessBinaryWriter writer, Type fullType )
        {
            writer.Write( _id );

            int numGenericParameters = CountNumberGenericParameters( _baseType );
            if ( numGenericParameters == 0 )
            {
                return;
            }

            Type[] genericArguments = fullType.GetGenericArguments();
            for ( int index = 0; index < numGenericParameters; ++index )
            {
                writer.WriteType( genericArguments[index] );
            }
        }

        static int CountNumberGenericParameters( Type type )
        {
            if ( !type.ContainsGenericParameters )
            {
                return 0;
            }

            int numGenericParameters = type.GetGenericArguments().Count( argument => argument.IsGenericParameter );
            return numGenericParameters;
        }

        Type ResolveGenericArguments( PainlessBinaryReader reader, Type baseType, int numGenericParameters )
        {
            Type[] genericArguments = new Type[numGenericParameters];

            for ( int index = 0; index < numGenericParameters; ++index )
            {
                genericArguments[index] = reader.ReadNextType();
            }

            return baseType.MakeGenericType( genericArguments );
        }
    }
}
