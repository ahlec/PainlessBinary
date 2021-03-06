﻿// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using PainlessBinary.Exceptions;
using PainlessBinary.IO;
using PainlessBinary.Reflection;

namespace PainlessBinary.SerializableValues
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

        public static ReflectedClassValue Instantiate( TypeManager typeManager, Type fullType, PainlessBinaryReader reader )
        {
            object value = Activator.CreateInstance( fullType );
            return new ReflectedClassValue( fullType, value );
        }

        public static ReflectedClassValue WrapRawValue( TypeManager typeManager, Type fullType, object value )
        {
            return new ReflectedClassValue( fullType, value );
        }

        public void Read( PainlessBinaryReader reader )
        {
            reader.PushCompoundingHash();

            foreach ( SerializedMember member in _members )
            {
                object memberValue = reader.ReadPainlessBinaryObject( member.Type );
                member.SetValue( Value, memberValue );
            }

            int computedHash = reader.PopCompoundingHash();
            int readHash = reader.ReadInt32();
            if ( computedHash != readHash )
            {
                throw new DataIntegrityQuestionableException( computedHash, readHash );
            }
        }

        public void Write( PainlessBinaryWriter writer )
        {
            writer.PushCompoundingHash();

            foreach ( SerializedMember member in _members )
            {
                object rawValue = member.GetValue( Value );
                writer.WritePainlessBinaryObject( member.Type, rawValue );
            }

            int computedHash = writer.PopCompoundingHash();
            writer.Write( computedHash );
        }
    }
}
