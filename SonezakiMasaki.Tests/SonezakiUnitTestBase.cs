// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using SonezakiMasaki.Tests.ExampleTypes;

namespace SonezakiMasaki.Tests
{
    public abstract class SonezakiUnitTestBase
    {
        protected static Serializer CreateSerializer()
        {
            TypeRegistry typeRegistry = CreateTypeRegistry();
            Serializer serializer = new Serializer( typeRegistry );
            return serializer;
        }

        static TypeRegistry CreateTypeRegistry()
        {
            TypeRegistry typeRegistry = new TypeRegistry();
            typeRegistry.RegisterType( typeof( Person ) );
            typeRegistry.RegisterType( typeof( Date ) );
            typeRegistry.RegisterType( typeof( Month ) );
            typeRegistry.RegisterType( typeof( Pronoun ) );
            return typeRegistry;
        }
    }
}
