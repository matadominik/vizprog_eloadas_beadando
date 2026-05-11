using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace vizprog_eloadas_beadando_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //DataContext context = new DataContext();
            //myDataGrid.ItemsSource = context.Diakok.ToList();
        }

        private void Fajl_bezaras_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Adatok_diakok_click(object sender, RoutedEventArgs e)
        {

        }

        private void Adatok_jegyek_click(object sender, RoutedEventArgs e)
        {

        }

        private void Adatok_targyak_click(object sender, RoutedEventArgs e)
        {

        }
    }
}