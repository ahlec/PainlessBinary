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

        public ISerializableValue ReadNextTypeInfo( BinaryReader reader )
        {
            TypeCategory category = (TypeCategory) reader.ReadByte();
            int typeId = reader.ReadInt32();
            ISerializableValue typeInfo = ResolveTypeInfo( category, typeId );
            typeInfo.ReadHeader( reader, this );
            return typeInfo;
        }

        ISerializableValue ResolveTypeInfo( TypeCategory category, int typeId )
        {
            switch ( category )
            {
                case TypeCategory.RegularType:
                    return _typeResolver.ResolveType( typeId );
                case TypeCategory.Container:
                    return _containerResolver.ResolveContainer( typeId );
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
