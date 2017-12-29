// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using SonezakiMasaki.Reflection;

namespace SonezakiMasaki.SerializableValues
{
    internal sealed class ReflectedClassValue<T> : ISerializableValue
    {
        static readonly IReadOnlyList<SerializedMember> _members = SerializedMember.GetOrderedSerializedMembers( typeof( T ) );
        readonly TypeManager _typeManager;

        ReflectedClassValue( TypeManager typeManager, object value )
        {
            _typeManager = typeManager;
            Value = value;
        }

        public object Value { get; }

        public static ReflectedClassValue<T> Instantiate( TypeManager typeManager, Type fullType, BinaryReader reader )
        {
            object value = Activator.CreateInstance( typeof( T ) );
            return new ReflectedClassValue<T>( typeManager, value );
        }

        public static ReflectedClassValue<T> WrapRawValue( TypeManager typeManager, object value )
        {
            return new ReflectedClassValue<T>( typeManager, value );
        }

        public void Read( BinaryReader reader, ObjectSerializer objectSerializer )
        {
            foreach ( SerializedMember member in _members )
            {
                Type memberType = objectSerializer.ReadNextType( reader );
                ISerializableValue serializableValue = _typeManager.Instantiate( memberType, reader );
                serializableValue.Read( reader, objectSerializer );
                member.SetValue( Value, serializableValue.Value );
            }
        }

        public void Write( BinaryWriter writer )
        {

        }
    }
}
