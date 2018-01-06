// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

namespace PainlessBinary
{
    internal interface IMultiKeyValue<out TKey1, out TKey2>
    {
        TKey1 Key1 { get; }

        TKey2 Key2 { get; }
    }
}
