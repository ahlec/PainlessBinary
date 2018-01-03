﻿// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SonezakiMasaki.Tests
{
    [TestFixture]
    public sealed class RegisteringTypesTests : SonezakiUnitTestBase
    {
        [Test]
        public void RegisteringTypes_CannotRegisterInterface()
        {
            TypeRegistry typeRegistry = new TypeRegistry();
            Assert.Throws<InvalidOperationException>( () => typeRegistry.RegisterType( typeof( IList<int> ) ) );
        }

        [Test]
        public void RegisteringTypes_CannotRegisterNestedInterfaces()
        {
            TypeRegistry typeRegistry = new TypeRegistry();
            Assert.Throws<InvalidOperationException>( () => typeRegistry.RegisterType( typeof( List<IEnumerable<int>> ) ) );
        }
    }
}