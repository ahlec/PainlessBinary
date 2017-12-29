// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SonezakiMasaki.Exceptions;
using SonezakiMasaki.IO;
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

        public static ReflectedClassValue<T> Instantiate( TypeManager typeManager, Type fullType, SonezakiReader reader )
        {
            object value = Activator.CreateInstance( typeof( T ) );
            return new ReflectedClassValue<T>( typeManager, value );
        }

        public static ReflectedClassValue<T> WrapRawValue( TypeManager typeManager, object value )
        {
            return new ReflectedClassValue<T>( typeManager, value );
        }

        public void Read( SonezakiReader reader, ObjectSerializer objectSerializer )
        {
            reader.PushCompoundingHash();

            foreach ( SerializedMember member in _members )
            {
                Type memberType = objectSerializer.ReadNextType( reader );
                ISerializableValue serializableValue = _typeManager.Instantiate( memberType, reader );
                serializableValue.Read( reader, objectSerializer );
                member.SetValue( Value, serializableValue.Value );
            }

            int computedHash = reader.PopCompoundingHash();
            int readHash = reader.ReadInt32();
            if ( computedHash != readHash )
            {
                throw new DataIntegrityQuestionableException( computedHash, readHash );
            }
        }

        public void Write( SonezakiWriter writer, ObjectSerializer objectSerializer )
        {
            writer.PushCompoundingHash();

            foreach ( SerializedMember member in _members )
            {
                object rawValue = member.GetValue( Value );
                ISerializableValue value = _typeManager.WrapRawValue( member.Type, rawValue );
                objectSerializer.WriteType( writer, member.Type );
                value.Write( writer, objectSerializer );
            }

            int computedHash = writer.PopCompoundingHash();
            writer.Write( computedHash );
        }
    }
}
