// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SonezakiMasaki.Exceptions;

namespace SonezakiMasaki
{
    public sealed class TypeResolver
    {
        readonly IDictionary<uint, Type> _registeredTypes = new Dictionary<uint, Type>();

        internal Type ResolveType( uint typeId )
        {
            if ( !_registeredTypes.TryGetValue( typeId, out Type baseType ) )
            {
                throw new UnrecognizedTypeException( typeId );
            }

            return baseType;
        }
    }
}
