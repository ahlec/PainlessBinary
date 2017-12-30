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
            TypeManager typeManager = CreateTypeManager();
            Serializer serializer = new Serializer( typeManager );
            return serializer;
        }

        static TypeManager CreateTypeManager()
        {
            TypeManager typeManager = new TypeManager();
            typeManager.RegisterType<Person>();
            typeManager.RegisterType<Date>();
            typeManager.RegisterType<Month>();
            typeManager.RegisterType<Pronoun>();
            return typeManager;
        }
    }
}
