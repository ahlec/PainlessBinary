// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.IO;

namespace SonezakiMasaki.Containers
{
    internal interface IContainer
    {
        ContainerId Id { get; }

        object FinalValue { get; }

        /// <summary>
        /// Reads any data that is necessary in order to deserialize the container portion of the
        /// final value. This is read from the reader prior to reading or interpretting the data type
        /// of the item inside of the container.
        /// </summary>
        /// <param name="reader">The stream reader being deserialized from.</param>
        void ReadHeader( BinaryReader reader );

        /// <summary>
        /// Called after the container and the type have been fully deserialized, and actual data is
        /// about to be deserialized from the reader. This gives the container a chance to do any preparation
        /// work that is necessary before <seealso cref="AddItem"/> will begin to be called.
        /// </summary>
        /// <param name="typeInfo">The type of the data that this container contains.</param>
        void Prepare( ITypeInfo typeInfo );

        void AddItem( object item );
    }
}
