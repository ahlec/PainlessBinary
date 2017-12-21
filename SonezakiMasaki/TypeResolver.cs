// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using SonezakiMasaki.Exceptions;

namespace SonezakiMasaki
{
    public sealed class TypeResolver
    {
        readonly IDictionary<uint, ITypeDefinition> _typeDefinitions = new Dictionary<uint, ITypeDefinition>();

        internal ITypeDefinition ResolveType( uint typeId )
        {
            if ( !_typeDefinitions.TryGetValue( typeId, out ITypeDefinition typeDefinition ) )
            {
                throw new UnrecognizedTypeException( typeId );
            }

            // TODO: We probably want to merge containers and types because we need to read header here to determine type
            // TODO: same as container, because what of things like Animal<Human>?

            return typeDefinition;
        }
    }
}
