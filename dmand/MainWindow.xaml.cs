using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dmand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string defaultLocation = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );

        public MainWindow( string[] args )
        {
            InitializeComponent();

            var location = defaultLocation;
            if ( args.Length > 0 )
            {
                var candidate = args[ 0 ];
                if ( Directory.Exists( candidate ) )
                {
                    location = candidate;
                }
                else if ( File.Exists( candidate ) )
                {
                    location = Directory.GetParent( candidate ).FullName;
                }
            }

            UpdateView( location );
        }

        private void UpdateView( string location )
        {
            Contents.Items.Clear();
            Location.Text = location;

            foreach ( var file in Directory.GetDirectories( location ) )
            {
                var d = new DirectoryInfo( file );
                Console.WriteLine( $"{d.Attributes}" );
                Contents.Items.Add( new DirectoryInfoItem( file ) );
            }

            foreach ( var file in Directory.GetFiles( location ) )
            {
                var d = new FileInfo( file );
                Console.WriteLine( $"{d.Attributes}" );
                Contents.Items.Add( new FileInfoItem( file ) );
            }
        }
    }
}
