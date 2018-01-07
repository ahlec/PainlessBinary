// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace PainlessBinary.IO
{
    internal sealed class WriterReferenceTable
    {
        readonly Dictionary<Type, List<Reference>> _referencesByType = new Dictionary<Type, List<Reference>>();
        uint _nextReferenceId = 1;

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

            Reference newReference = new Reference( _nextReferenceId, value );
            _nextReferenceId++;
            references.Add( newReference );
            return newReference.Id;
        }

        sealed class Reference
        {
            readonly object _value;

            public Reference( uint id, object value )
            {
                Id = id;
                _value = value;
            }

            public uint Id { get; }

            public bool Is( object other )
            {
                return _value.Equals( other );
            }
        }
    }
}
