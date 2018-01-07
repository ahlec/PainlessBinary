// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using PainlessBinary.Markup;

namespace PainlessBinary.Tests.ExampleTypes
{
    [BinaryDataType( Scheme = BinarySerializationScheme.Reference, ReferenceDetectionMethod = ReferenceDetectionMethod.Equals )]
    public sealed class Item
    {
        [BinaryMember( 1 )]
        public string Name { get; set; }

        /// <inheritdoc />
        public override bool Equals( object obj )
        {
            Item other = obj as Item;
            if ( other == null )
            {
                return false;
            }

            return Name.Equals( other.Name );
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
