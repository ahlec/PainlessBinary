// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using PainlessBinary.Tests.ExampleTypes;

namespace PainlessBinary.Tests
{
    public abstract class UnitTestBase
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
            typeRegistry.RegisterType( typeof( Character ) );
            typeRegistry.RegisterType( typeof( Date ) );
            typeRegistry.RegisterType( typeof( GameData ) );
            typeRegistry.RegisterType( typeof( Item ) );
            typeRegistry.RegisterType( typeof( Month ) );
            return typeRegistry;
        }
    }
}
