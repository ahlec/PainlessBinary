// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using PainlessBinary.Markup;

namespace PainlessBinary.IO
{
    internal sealed class WriterReferenceTable
    {
        readonly TypeManager _typeManager;
        readonly Dictionary<Type, List<Reference>> _referencesByType = new Dictionary<Type, List<Reference>>();
        uint _nextReferenceId = 1;

        public WriterReferenceTable( TypeManager typeManager )
        {
            _typeManager = typeManager;
        }

        public bool IsAlreadyRegistered( object value )
        {
            Type valueType = value.GetType();

            if ( !_referencesByType.TryGetValue( valueType, out List<Reference> references ) )
            {
                return false;
            }

            return references.Any( reference => reference.Is( value ) );
        }

        public uint GetReferenceId( object value )
        {
            Type valueType = value.GetType();
            List<Reference> references = _referencesByType[valueType];
            return references.First( reference => reference.Is( value ) ).Id;
        }

        public uint Register( object value )
        {
            Type valueType = value.GetType();

            if ( !_referencesByType.TryGetValue( valueType, out List<Reference> references ) )
            {
                references = new List<Reference>();
                _referencesByType.Add( valueType, references );
            }

            ReferenceDetectionMethod detectionMethod = _typeManager.GetTypeReferenceDetectionMethod( valueType );
            Reference newReference = new Reference( detectionMethod, _nextReferenceId, value );
            _nextReferenceId++;
            references.Add( newReference );
            return newReference.Id;
        }

        sealed class Reference
        {
            readonly ReferenceDetectionMethod _detectionMethod;
            readonly object _value;

            public Reference( ReferenceDetectionMethod detectionMethod, uint id, object value )
            {
                _detectionMethod = detectionMethod;
                Id = id;
                _value = value;
            }

            public uint Id { get; }

            public bool Is( object other )
            {
                switch ( _detectionMethod )
                {
                    case ReferenceDetectionMethod.ReferenceEquals:
                        return ReferenceEquals( _value, other );
                    case ReferenceDetectionMethod.Equals:
                        return _value.Equals( other );
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
