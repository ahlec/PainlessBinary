// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using SonezakiMasaki.Exceptions;
using SonezakiMasaki.SerializableValues;

namespace SonezakiMasaki
{
    internal sealed class TypeInstantiator
    {
        static readonly IReadOnlyDictionary<Type, ValueInstantiator> _instantiators = new Dictionary<Type, ValueInstantiator>
        {
            { typeof( List<> ), ListValue.Instantiate }
        };

        delegate ISerializableValue ValueInstantiator( TypeInstantiator typeInstantiator, Type fullType, BinaryReader reader );

        public ISerializableValue Instantiate( Type type, BinaryReader reader )
        {
            Type baseType = type;
            if ( baseType.IsGenericType )
            {
                baseType = type.GetGenericTypeDefinition();
            }

            if ( !_instantiators.TryGetValue( baseType, out ValueInstantiator instantiator ) )
            {
                throw new UninstantiatableTypeException( baseType, type );
            }

            return instantiator( this, type, reader );
        }
    }
}
