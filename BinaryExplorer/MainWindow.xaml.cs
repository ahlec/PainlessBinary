using SonezakiMasaki;

namespace BinaryExplorer
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            TypeManager typeManager = new TypeManager();
            Serializer serializer = new Serializer( typeManager );
        }
    }
}
