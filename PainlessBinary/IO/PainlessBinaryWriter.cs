// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using PainlessBinary.TypeSignatures;

namespace PainlessBinary.IO
{
    internal sealed class PainlessBinaryWriter : BinaryWriter
    {
        readonly StreamWrapper _streamWrapper;
        readonly TypeManager _typeManager;
        readonly WriterReferenceTable _referenceTable;
        readonly int _hashSeed;
        readonly int _hashMultiplicationConstant;

        public PainlessBinaryWriter( StreamWrapper dataStream, TypeManager typeManager, int hashSeed, int hashMultiplicationConstant )
            : base( dataStream )
        {
            _streamWrapper = dataStream;
            _typeManager = typeManager;
            _referenceTable = new WriterReferenceTable( typeManager );
            _hashSeed = hashSeed;
            _hashMultiplicationConstant = hashMultiplicationConstant;
        }

        public void PushCompoundingHash()
        {
            CompoundingHash hash = new CompoundingHash( _hashSeed, _hashMultiplicationConstant );
            _streamWrapper.PushCompoundingHash( hash );
        }

        public int PopCompoundingHash()
        {
            CompoundingHash hash = _streamWrapper.PopCompoundingHash();
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

        public void WritePainlessBinaryObject( Type typeForSerializing, object value )
        {
            bool doesSerializeAsReference = _typeManager.DetermineIsTypeSerializedAsReference( typeForSerializing );
            SerializationType serializationType = DetermineSerializationType( value, doesSerializeAsReference );
            Write( (byte) serializationType );

            switch ( serializationType )
            {
                case SerializationType.Null:
                    break;

                case SerializationType.RegularValue:
                    {
                        WriteRegularValue( typeForSerializing, value, doesSerializeAsReference );
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

        SerializationType DetermineSerializationType( object value, bool doesSerializeAsReference )
        {
            if ( value == null )
            {
                return SerializationType.Null;
            }

            if ( doesSerializeAsReference && _referenceTable.IsAlreadyRegistered( value ) )
            {
                return SerializationType.Reference;
            }

            return SerializationType.RegularValue;
        }

        void WriteRegularValue( Type type, object value, bool doesTypeSerializeAsReference )
        {
            ISerializableValue itemSerializableValue = _typeManager.WrapRawValue( type, value );
            itemSerializableValue.Write( this );

            if ( doesTypeSerializeAsReference )
            {
                uint referenceNumber = _referenceTable.Register( value );
                Write( referenceNumber );
            }
        }

        void WriteReference( object value )
        {
            uint referenceId = _referenceTable.GetReferenceId( value );
            Write( referenceId );
        }
    }
}
