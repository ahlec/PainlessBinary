// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using SonezakiMasaki.TypeSignatures;

namespace SonezakiMasaki.IO
{
    internal sealed class SonezakiWriter : BinaryWriter
    {
        readonly SonezakiStreamWrapper _sonezakiStreamWrapper;
        readonly TypeManager _typeManager;
        readonly int _hashSeed;
        readonly int _hashMultiplicationConstant;

        public SonezakiWriter( SonezakiStreamWrapper dataStream, TypeManager typeManager, int hashSeed, int hashMultiplicationConstant )
            : base( dataStream )
        {
            _sonezakiStreamWrapper = dataStream;
            _typeManager = typeManager;
            _hashSeed = hashSeed;
            _hashMultiplicationConstant = hashMultiplicationConstant;
        }

        public void PushCompoundingHash()
        {
            CompoundingHash hash = new CompoundingHash( _hashSeed, _hashMultiplicationConstant );
            _sonezakiStreamWrapper.PushCompoundingHash( hash );
        }

        public int PopCompoundingHash()
        {
            CompoundingHash hash = _sonezakiStreamWrapper.PopCompoundingHash();
            return hash.HashValue;
        }

        public void WriteType( Type type )
        {
            if ( type.ContainsGenericParameters )
            {
                throw new InvalidOperationException( "Cannot write an incomplete type. The type in question has generic parameters still." );
            }

            ITypeSignature typeSignature = _typeManager.ResolveTypeSignature( type );
            typeSignature.Write( this, type );
        }

        public void WriteSonezakiObject( Type typeForSerializing, object value )
        {
            SerializationType serializationType = DetermineSerializationType( typeForSerializing, value );
            Write( (byte) serializationType );

            switch ( serializationType )
            {
                case SerializationType.Null:
                    break;

                case SerializationType.RegularValue:
                    {
                        WriteRegularValue( typeForSerializing, value );
                        break;
                    }

                case SerializationType.Reference:
                    {
                        WriteReference( value );
                        break;
                    }

                default:
                    throw new NotImplementedException();
            }
        }

        SerializationType DetermineSerializationType( Type type, object value )
        {
            if ( value == null )
            {
                return SerializationType.Null;
            }

            if ( _typeManager.DetermineIsTypeSerializedAsReference( type ) )
            {
                return SerializationType.Reference;
            }

            return SerializationType.RegularValue;
        }

        void WriteRegularValue( Type type, object value )
        {
            ISerializableValue itemSerializableValue = _typeManager.WrapRawValue( type, value );
            itemSerializableValue.Write( this );
        }

        void WriteReference( object value )
        {
            throw new NotImplementedException();
        }
    }
}
