// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using SonezakiMasaki.Markup;

namespace SonezakiMasaki.Reflection
{
    internal abstract class SerializedMember : IComparable<SerializedMember>
    {
        protected SerializedMember( int order, Type type )
        {
            Order = order;
            Type = type;
        }

        delegate SerializedMember MemberConstructor<in TReflectionInfo>( TReflectionInfo memberInfo, BinaryMemberAttribute attribute )
            where TReflectionInfo : MemberInfo;

        public int Order { get; }

        public Type Type { get; }

        public static IReadOnlyList<SerializedMember> GetOrderedSerializedMembers( Type type )
        {
            List<SerializedMember> serializedMembers = new List<SerializedMember>();

            serializedMembers.AddRange( CollectSerializedMembers( type.GetFields(), SerializedField.Create ) );
            serializedMembers.AddRange( CollectSerializedMembers( type.GetProperties(), SerializedProperty.Create ) );

            serializedMembers.Sort();

            return serializedMembers;
        }

        public int CompareTo( SerializedMember other )
        {
            return Order.CompareTo( other.Order );
        }

        public abstract object GetValue( object item );

        public abstract void SetValue( object item, object value );

        static IEnumerable<SerializedMember> CollectSerializedMembers<TReflectionInfo>( IEnumerable<TReflectionInfo> info, MemberConstructor<TReflectionInfo> constructor )
            where TReflectionInfo : MemberInfo
        {
            foreach ( TReflectionInfo member in info )
            {
                BinaryMemberAttribute attribute = member.GetCustomAttribute<BinaryMemberAttribute>();
                if ( attribute == null )
                {
                    continue;
                }

                SerializedMember serializedMember = constructor( member, attribute );
                yield return serializedMember;
            }
        }
    }
}
