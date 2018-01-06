// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace PainlessBinary
{
    internal sealed class MultiKeyDictionary<TKey1, TKey2, TValue>
        where TValue : IMultiKeyValue<TKey1, TKey2>
    {
        readonly Dictionary<TKey1, TValue> _key1Dictionary = new Dictionary<TKey1, TValue>();
        readonly Dictionary<TKey2, TValue> _key2Dictionary = new Dictionary<TKey2, TValue>();

        public void Add( TValue value )
        {
            if ( value == null )
            {
                throw new ArgumentNullException( nameof( value ) );
            }

            if ( Contains( value.Key1 ) || Contains( value.Key2 ) )
            {
                throw new InvalidOperationException( "This dictionary already contains an instance of this item!" );
            }

            _key1Dictionary.Add( value.Key1, value );
            _key2Dictionary.Add( value.Key2, value );
        }

        public bool Contains( TKey1 key1 )
        {
            return _key1Dictionary.ContainsKey( key1 );
        }

        public bool Contains( TKey2 key2 )
        {
            return _key2Dictionary.ContainsKey( key2 );
        }

        public bool TryGetValue( TKey1 key1, out TValue value )
        {
            return _key1Dictionary.TryGetValue( key1, out value );
        }

        public bool TryGetValue( TKey2 key2, out TValue value )
        {
            return _key2Dictionary.TryGetValue( key2, out value );
        }
    }
}
