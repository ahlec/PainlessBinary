// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;

namespace SonezakiMasaki.SerializableValues
{
    internal sealed class BuiltInValue : ISerializableValue
    {
        readonly BinaryReaderFunction _function;

        BuiltInValue( BinaryReaderFunction function )
        {
            _function = function;
        }

        internal delegate object BinaryReaderFunction( BinaryReader reader );

        public static ValueInstantiator CreateInstantiator( BinaryReaderFunction function )
        {
            return ( typeManager, fullType, reader ) => new BuiltInValue( function );
        }

        public object Read( BinaryReader reader, ObjectSerializer objectSerializer )
        {
            return _function( reader );
        }
    }
}
