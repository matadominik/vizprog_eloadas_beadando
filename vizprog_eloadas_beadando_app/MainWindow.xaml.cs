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
using System.Linq;


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

        private void Hozzaadas_click(object sender, RoutedEventArgs e)
        {

        }

        private void Modositas_click(object sender, RoutedEventArgs e)
        {

        }

        private void Torles_click(object sender, RoutedEventArgs e)
        {
            if (myDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Nincs kijelölt rekord!");
                return;
            }

            MessageBoxResult valasz = MessageBox.Show("Biztosan törölni szeretnéd?", "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (valasz != MessageBoxResult.Yes) return;

            if (aktivTabla == "diakok")
            {
                Diak kijeloltDiak = (Diak)myDataGrid.SelectedItem;

                // nem maradhat "árva" rekord, ezért előbb törölni kell a kapcsolódó jegyeket
                var kapcsolodoJegyek = context.Jegyek.Where(j => j.diakid == kijeloltDiak.id).ToList();
                context.Jegyek.RemoveRange(kapcsolodoJegyek);
                // egyből mentünk, hogy ne legyen hiba futáskor
                context.SaveChanges();

                // majd csak utána jöhet a diák
                context.Diakok.Remove(kijeloltDiak);
                context.SaveChanges();
                DiakokBetoltese();
            }
        }

        private void Frissites_click(object sender, RoutedEventArgs e)
        {
            if (aktivTabla == "diakok") DiakokBetoltese();
            else if (aktivTabla == "jegyek") JegyekBetoltese();
            else if (aktivTabla == "targyak") TargyakBetoltese();
        }
    }
}