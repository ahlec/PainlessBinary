// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.Reflection;

namespace SonezakiMasaki.Reflection
{
    internal sealed class SerializedProperty : SerializedMember
    {
        readonly PropertyInfo _propertyInfo;

        SerializedProperty( int order, PropertyInfo propertyInfo )
            : base( order )
        {
            _propertyInfo = propertyInfo;
        }

        public static SerializedProperty Create( PropertyInfo propertyInfo, SerializedMemberAttribute attribute )
        {
            return new SerializedProperty( attribute.Order, propertyInfo );
        }

        public override void SetValue( object item, object value )
        {
            _propertyInfo.SetValue( item, value );
        }
    }
}
