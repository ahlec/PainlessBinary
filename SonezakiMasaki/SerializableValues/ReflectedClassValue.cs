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
    internal sealed class ReflectedClassValue : ISerializableValue
    {
        readonly IReadOnlyList<SerializedMember> _members;

        ReflectedClassValue( Type type, object value )
        {
            _members = SerializedMember.GetOrderedSerializedMembers( type );
            Value = value;
        }

        public object Value { get; }

        public static ReflectedClassValue Instantiate( TypeManager typeManager, Type fullType, SonezakiReader reader )
        {
            object value = Activator.CreateInstance( fullType );
            return new ReflectedClassValue( fullType, value );
        }

        public static ReflectedClassValue WrapRawValue( TypeManager typeManager, Type fullType, object value )
        {
            return new ReflectedClassValue( fullType, value );
        }

        public void Read( SonezakiReader reader )
        {
            reader.PushCompoundingHash();

            foreach ( SerializedMember member in _members )
            {
                object memberValue = reader.ReadSonezakiObject( member.Type );
                member.SetValue( Value, memberValue );
            }

            int computedHash = reader.PopCompoundingHash();
            int readHash = reader.ReadInt32();
            if ( computedHash != readHash )
            {
                throw new DataIntegrityQuestionableException( computedHash, readHash );
            }
        }

        public void Write( SonezakiWriter writer )
        {
            writer.PushCompoundingHash();

            foreach ( SerializedMember member in _members )
            {
                object rawValue = member.GetValue( Value );
                writer.WriteSonezakiObject( member.Type, rawValue );
            }

            int computedHash = writer.PopCompoundingHash();
            writer.Write( computedHash );
        }
    }
}
