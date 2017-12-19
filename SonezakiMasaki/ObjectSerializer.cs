// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using SonezakiMasaki.Containers;

namespace SonezakiMasaki
{
    internal sealed class ObjectSerializer
    {
        readonly ContainerResolver _containerResolver;
        readonly TypeResolver _typeResolver;

        public ObjectSerializer( ContainerResolver containerResolver, TypeResolver typeResolver )
        {
            _containerResolver = containerResolver;
            _typeResolver = typeResolver;
        }

        enum TypeCategory
        {
            RegularType = 0,
            Container = 1
        }

        public ITypeDefinition ReadNextTypeDefinition( BinaryReader reader )
        {
            TypeCategory category = (TypeCategory) reader.ReadByte();
            int typeId = reader.ReadInt32();
            ITypeDefinition typeInfo = ResolveTypeInfo( category, typeId, reader );
            return typeInfo;
        }

        ITypeDefinition ResolveTypeInfo( TypeCategory category, int typeId, BinaryReader reader )
        {
            switch ( category )
            {
                case TypeCategory.RegularType:
                    return _typeResolver.ResolveType( typeId );
                case TypeCategory.Container:
                    return _containerResolver.ResolveContainer( typeId, reader, this );
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
