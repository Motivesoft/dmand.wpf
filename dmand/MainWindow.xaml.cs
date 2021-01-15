using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;

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

            new Thread( () => 
            { 
                Dispatcher.Invoke( () => {
                    Contents.Focus();
                } );
            } ).Start();
        }

        private void Grid_KeyDown( object sender, KeyEventArgs e )
        {
            var key = e.Key == Key.System ? e.SystemKey : e.Key;
            if ( key == Key.D && IsAltPressed )
            {
                Location.Focus();
                Location.SelectAll();
                e.Handled = true;
            }
            else
            {
                if ( key == Key.Down )
                {
                }
            }
        }

        private void Grid_PreviewKeyDown( object sender, KeyEventArgs e )
        {
            UpdateKeyStates();

            var key = e.Key == Key.System ? e.SystemKey : e.Key;
        }

        private void Grid_PreviewKeyUp( object sender, KeyEventArgs e )
        {
            UpdateKeyStates();
        }

        void UpdateKeyStates()
        {
            IsCtrlPressed = ( Keyboard.IsKeyDown( Key.LeftCtrl ) || Keyboard.IsKeyDown( Key.RightCtrl ) );
            IsAltPressed = ( Keyboard.IsKeyDown( Key.LeftAlt ) || Keyboard.IsKeyDown( Key.RightAlt ) );
            IsShiftPressed = ( Keyboard.IsKeyDown( Key.LeftShift ) || Keyboard.IsKeyDown( Key.RightShift ) );
        }

        private bool IsAltPressed
        {
            get; set;
        }
        private bool IsCtrlPressed
        {
            get; set;
        }
        private bool IsShiftPressed
        {
            get; set;
        }
    }
}
