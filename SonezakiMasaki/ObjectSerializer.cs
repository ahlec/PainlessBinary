// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;

namespace SonezakiMasaki
{
    internal sealed class ObjectSerializer
    {
        readonly TypeResolver _typeResolver;

        public ObjectSerializer( TypeResolver typeResolver )
        {
            _typeResolver = typeResolver;
        }

        public Type ReadNextType( BinaryReader reader )
        {
            uint typeId = reader.ReadUInt32();
            Type baseType = _typeResolver.ResolveType( typeId );
            Type type = CompleteType( baseType, reader );
            return type;
        }

        Type CompleteType( Type baseType, BinaryReader reader )
        {
            if ( !baseType.ContainsGenericParameters )
            {
                return baseType;
            }

            return ResolveGenericArguments( baseType, reader );
        }

        Type ResolveGenericArguments( Type baseType, BinaryReader reader )
        {
            int numGenericParameters = baseType.GetGenericArguments().Count( argument => argument.IsGenericParameter );
            Type[] genericArguments = new Type[numGenericParameters];

            for ( int index = 0; index < numGenericParameters; ++index )
            {
                genericArguments[index] = ReadNextType( reader );
            }

            return baseType.MakeGenericType( genericArguments );
        }
    }
}
