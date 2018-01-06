// ------------------------------------------------------------------------------------------------------------------------
// PainlessBinary library project (https://github.com/ahlec/PainlessBinary/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.Reflection;
using PainlessBinary.Markup;

namespace PainlessBinary.Reflection
{
    internal sealed class SerializedProperty : SerializedMember
    {
        readonly PropertyInfo _propertyInfo;

        SerializedProperty( int order, PropertyInfo propertyInfo )
            : base( order, propertyInfo.PropertyType )
        {
            _propertyInfo = propertyInfo;
        }

        public static SerializedProperty Create( PropertyInfo propertyInfo, BinaryMemberAttribute attribute )
        {
            return new SerializedProperty( attribute.Order, propertyInfo );
        }

        public override object GetValue( object item )
        {
            return _propertyInfo.GetValue( item );
        }

        public override void SetValue( object item, object value )
        {
            _propertyInfo.SetValue( item, value );
        }
    }
}
