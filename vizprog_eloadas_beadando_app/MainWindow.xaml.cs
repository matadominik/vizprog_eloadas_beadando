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

            //myDataGrid.ItemsSource = context.Diakok.ToList();
        }

        AdatbazisContext context = new AdatbazisContext();
        string aktivTabla = "";

        private void Fajl_bezaras_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Adatok_diakok_click(object sender, RoutedEventArgs e)
        {
            aktivTabla = "diakok";
            DiakokBetoltese();
        }

        private void Adatok_jegyek_click(object sender, RoutedEventArgs e)
        {
            aktivTabla = "jegyek";
            JegyekBetoltese();
        }

        private void Adatok_targyak_click(object sender, RoutedEventArgs e)
        {
            aktivTabla = "targyak";
            TargyakBetoltese();
        }

        private void DiakokBetoltese()
        {
            myDataGrid.ItemsSource = context.Diakok.ToList();
        }

        private void JegyekBetoltese()
        {
            myDataGrid.ItemsSource = context.Jegyek.ToList();
        }

        private void TargyakBetoltese()
        {
            myDataGrid.ItemsSource = context.Targyak.ToList();
        }
    }
}