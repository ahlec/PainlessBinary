// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using PainlessBinary.TypeSignatures;

namespace PainlessBinary.IO
{
    internal sealed class PainlessBinaryReader : BinaryReader
    {
        readonly StreamWrapper _streamWrapper;
        readonly TypeManager _typeManager;
        readonly int _hashSeed;
        readonly int _hashMultiplicationConstant;

        public PainlessBinaryReader( StreamWrapper dataStream, TypeManager typeManager, int hashSeed, int hashMultiplicationConstant )
            : base( dataStream )
        {
            _streamWrapper = dataStream;
            _typeManager = typeManager;
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

        public Type ReadNextType()
        {
            uint typeId = ReadUInt32();
            ITypeSignature typeSignature = _typeManager.ResolveTypeSignature( typeId );
            return typeSignature.Read( this );
        }

        public object ReadPainlessBinaryObject( Type expectedType )
        {
            SerializationType serializationType = (SerializationType) ReadByte();
            switch ( serializationType )
            {
                case SerializationType.Null:
                    return null;
                case SerializationType.RegularValue:
                    return ReadRegularValue( expectedType );
                case SerializationType.Reference:
                    return ReadReference();
                default:
                    throw new NotSupportedException();
            }
        }

        object ReadRegularValue( Type expectedType )
        {
            ISerializableValue itemSerializableValue = _typeManager.Instantiate( expectedType, this );
            itemSerializableValue.Read( this );
            return itemSerializableValue.Value;
        }

        object ReadReference()
        {
            throw new NotImplementedException();
        }
    }
}
