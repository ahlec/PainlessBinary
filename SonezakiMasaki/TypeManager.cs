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
    public sealed class TypeManager
    {
        const uint MaxBuiltInTypeId = 999;
        readonly MultiKeyDictionary<uint, Type, RegisteredType> _registeredTypes = new MultiKeyDictionary<uint, Type, RegisteredType>();
        uint _nextBuiltInType = 1;
        uint _nextProprietaryType = MaxBuiltInTypeId + 1;

        public TypeManager()
        {
            RegisterBuiltInTypes();
        }

        internal Type ResolveType( uint typeId )
        {
            if ( !_registeredTypes.TryGetValue( typeId, out RegisteredType registeredType ) )
            {
                throw new UnrecognizedTypeException( typeId );
            }

            return registeredType.Type;
        }

        internal ISerializableValue Instantiate( Type type, BinaryReader reader )
        {
            Type baseType = type;
            if ( baseType.IsGenericType )
            {
                baseType = type.GetGenericTypeDefinition();
            }

            if ( !_registeredTypes.TryGetValue( baseType, out RegisteredType registeredType ) )
            {
                throw new UninstantiatableTypeException( baseType, type );
            }

            ISerializableValue value = registeredType.Instantiate( this, type, reader );
            return value;
        }

        void RegisterBuiltInTypes()
        {
            RegisterBuiltInType( typeof( bool ), BuiltInValue.CreateInstantiator( reader => reader.ReadBoolean() ) );
            RegisterBuiltInType( typeof( byte ), BuiltInValue.CreateInstantiator( reader => reader.ReadByte() ) );
            RegisterBuiltInType( typeof( sbyte ), BuiltInValue.CreateInstantiator( reader => reader.ReadSByte() ) );
            RegisterBuiltInType( typeof( char ), BuiltInValue.CreateInstantiator( reader => reader.ReadChar() ) );
            RegisterBuiltInType( typeof( decimal ), BuiltInValue.CreateInstantiator( reader => reader.ReadDecimal() ) );
            RegisterBuiltInType( typeof( double ), BuiltInValue.CreateInstantiator( reader => reader.ReadDouble() ) );
            RegisterBuiltInType( typeof( float ), BuiltInValue.CreateInstantiator( reader => reader.ReadSingle() ) );
            RegisterBuiltInType( typeof( int ), BuiltInValue.CreateInstantiator( reader => reader.ReadInt32() ) );
            RegisterBuiltInType( typeof( uint ), BuiltInValue.CreateInstantiator( reader => reader.ReadUInt32() ) );
            RegisterBuiltInType( typeof( long ), BuiltInValue.CreateInstantiator( reader => reader.ReadInt64() ) );
            RegisterBuiltInType( typeof( ulong ), BuiltInValue.CreateInstantiator( reader => reader.ReadUInt64() ) );
            RegisterBuiltInType( typeof( short ), BuiltInValue.CreateInstantiator( reader => reader.ReadInt16() ) );
            RegisterBuiltInType( typeof( ushort ), BuiltInValue.CreateInstantiator( reader => reader.ReadUInt16() ) );
            RegisterBuiltInType( typeof( string ), BuiltInValue.CreateInstantiator( reader => reader.ReadString() ) );
            RegisterBuiltInType( typeof( List<> ), ListValue.Instantiate );
        }

        void RegisterBuiltInType( Type type, ValueInstantiator instantiator )
        {
            if ( _nextBuiltInType > MaxBuiltInTypeId )
            {
                throw new InvalidOperationException();
            }

            RegisterTypeInternal( ref _nextBuiltInType, type, instantiator );
        }

        void RegisterTypeInternal( ref uint nextIdVariable, Type type, ValueInstantiator instantiator )
        {
            RegisteredType registeredType = new RegisteredType( nextIdVariable, type, instantiator );
            _registeredTypes.Add( registeredType );
            nextIdVariable++;
        }
    }
}
