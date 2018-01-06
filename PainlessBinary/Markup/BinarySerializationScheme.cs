// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace PainlessBinary.Markup
{
    public enum BinarySerializationScheme
    {
        /// <summary>
        /// Every instance of a class that is encountered will be serialized in-place, even if it
        /// has been previously serialized in the file.
        /// </summary>
        Value,

        /// <summary>
        /// A class will only be serialized the first time that it is encountered in the file. All
        /// subsequent encounters of a value that has already been serialized will serialize a reference
        /// to the first value, and upon deserialization, all subsequent references will be given a
        /// reference to that first value that was deserialized.
        /// </summary>
        Reference
    }
}
