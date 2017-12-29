// ------------------------------------------------------------------------------------------------------------------------
// SonezakiMasaki library project (https://github.com/ahlec/SonezakiMasaki/), a subproject of the Pokémon Kristall project.
// This library is available to the public under the MIT license.
// ------------------------------------------------------------------------------------------------------------------------

using System.Reflection;

namespace SonezakiMasaki.Reflection
{
    internal sealed class SerializedField : SerializedMember
    {
        readonly FieldInfo _fieldInfo;

        SerializedField( int order, FieldInfo fieldInfo )
            : base( order, fieldInfo.FieldType )
        {
            _fieldInfo = fieldInfo;
        }

        public static SerializedField Create( FieldInfo fieldInfo, SerializedMemberAttribute attribute )
        {
            return new SerializedField( attribute.Order, fieldInfo );
        }

        public override object GetValue( object item )
        {
            return _fieldInfo.GetValue( item );
        }

        public override void SetValue( object item, object value )
        {
            _fieldInfo.SetValue( item, value );
        }
    }
}
