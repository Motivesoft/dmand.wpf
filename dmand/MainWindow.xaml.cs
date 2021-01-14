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
        public MainWindow()
        {
            InitializeComponent();

            UpdateView( @"C:\Users\ian\source\repos\dmand\dmand\bin\Debug\net5.0-windows\" );
        }

        private void UpdateView( string location )
        {
            Contents.Items.Clear();

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
