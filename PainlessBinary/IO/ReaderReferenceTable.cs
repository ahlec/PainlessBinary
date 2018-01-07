// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace PainlessBinary.IO
{
    internal sealed class ReaderReferenceTable
    {
        readonly Dictionary<uint, object> _references = new Dictionary<uint, object>();

        public object GetReference( uint referenceId )
        {
            return _references[referenceId];
        }

        public void Add( uint referenceId, object value )
        {
            _references.Add( referenceId, value );
        }
    }
}
