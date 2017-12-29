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
        readonly TypeManager _typeManager;

        public ObjectSerializer( TypeManager typeManager )
        {
            _typeManager = typeManager;
        }

        public Type ReadNextType( BinaryReader reader )
        {
            uint typeId = reader.ReadUInt32();
            Type baseType = _typeManager.ResolveType( typeId );

            int numGenericParameters = CountNumberGenericParameters( baseType );
            if ( numGenericParameters == 0 )
            {
                return baseType;
            }

            return ResolveGenericArguments( baseType, numGenericParameters, reader );
        }

        public void WriteType( BinaryWriter writer, Type type )
        {
            if ( type.ContainsGenericParameters )
            {
                throw new InvalidOperationException( "Cannot write an incomplete type. The type in question has generic parameters still." );
            }

            RegisteredType registeredType = _typeManager.GetRegisteredType( type );
            WriteRegisteredType( writer, registeredType, type );
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

        Type ResolveGenericArguments( Type baseType, int numGenericParameters, BinaryReader reader )
        {
            Type[] genericArguments = new Type[numGenericParameters];

            for ( int index = 0; index < numGenericParameters; ++index )
            {
                genericArguments[index] = ReadNextType( reader );
            }

            return baseType.MakeGenericType( genericArguments );
        }

        void WriteRegisteredType( BinaryWriter writer, RegisteredType registeredType, Type fullType )
        {
            writer.Write( registeredType.Id );

            int numGenericParameters = CountNumberGenericParameters( registeredType.Type );
            if ( numGenericParameters == 0 )
            {
                return;
            }

            Type[] genericArguments = fullType.GetGenericArguments();
            for ( int index = 0; index < numGenericParameters; ++index )
            {
                WriteType( writer, genericArguments[index] );
            }
        }
    }
}
