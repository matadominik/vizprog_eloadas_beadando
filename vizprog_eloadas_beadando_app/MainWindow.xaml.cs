using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace vizprog_eloadas_beadando_app
{
    public partial class MainWindow : Window
    {
        private AdatbazisContext context = new AdatbazisContext();
        private string aktivTabla = "";

        public MainWindow()
        {
            InitializeComponent();
            FoMenuMegjelenit();
        }

        private void Fajl_bezaras_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void FoMenuMegjelenit()
        {
            foMenuGrid.Visibility = Visibility.Visible;
            adatOldalGrid.Visibility = Visibility.Collapsed;

            inputPanel.Visibility = Visibility.Collapsed;
            diakInput.Visibility = Visibility.Collapsed;
            jegyInput.Visibility = Visibility.Collapsed;
            targyInput.Visibility = Visibility.Collapsed;

            myDataGrid.ItemsSource = null;
            aktivTabla = "";

            MezoTorles();
            oldalCimTextBlock.Text = "Főoldal";
        }

        private void AdatOldalMegjelenit(string tabla)
        {
            foMenuGrid.Visibility = Visibility.Collapsed;
            adatOldalGrid.Visibility = Visibility.Visible;

            inputPanel.Visibility = Visibility.Visible;

            diakInput.Visibility = Visibility.Collapsed;
            jegyInput.Visibility = Visibility.Collapsed;
            targyInput.Visibility = Visibility.Collapsed;

            aktivTabla = tabla;
            MezoTorles();

            if (tabla == "diakok")
            {
                oldalCimTextBlock.Text = "Diákok kezelése";
                diakInput.Visibility = Visibility.Visible;
                DiakokBetoltese();
            }
            else if (tabla == "jegyek")
            {
                oldalCimTextBlock.Text = "Jegyek kezelése";
                jegyInput.Visibility = Visibility.Visible;
                JegyekBetoltese();
            }
            else if (tabla == "targyak")
            {
                oldalCimTextBlock.Text = "Tárgyak kezelése";
                targyInput.Visibility = Visibility.Visible;
                TargyakBetoltese();
            }
        }

        private void VisszaFoMenu_click(object sender, RoutedEventArgs e)
        {
            FoMenuMegjelenit();
        }

        private void Adatok_diakok_click(object sender, RoutedEventArgs e)
        {
            AdatOldalMegjelenit("diakok");
        }

        private void Adatok_jegyek_click(object sender, RoutedEventArgs e)
        {
            AdatOldalMegjelenit("jegyek");
        }

        private void Adatok_targyak_click(object sender, RoutedEventArgs e)
        {
            AdatOldalMegjelenit("targyak");
        }

        private void UjContext()
        {
            context.Dispose();
            context = new AdatbazisContext();
        }

        private bool BiztonsagosMentes()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string hiba = ex.InnerException != null
                    ? ex.InnerException.Message
                    : ex.Message;

                MessageBox.Show(
                    "Mentési hiba történt:\n\n" + hiba,
                    "Adatbázis hiba",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );

                UjContext();
                return false;
            }
        }

        private void DiakokBetoltese()
        {
            UjContext();
            myDataGrid.ItemsSource = context.Diakok.ToList();
        }

        private void JegyekBetoltese()
        {
            UjContext();
            myDataGrid.ItemsSource = context.Jegyek.ToList();
        }

        private void TargyakBetoltese()
        {
            UjContext();
            myDataGrid.ItemsSource = context.Targyak.ToList();
        }

        private void MezoTorles()
        {
            diakNevTextBox.Text = "";
            diakOsztalyTextBox.Text = "";
            diakFiuCheckBox.IsChecked = false;

            jegyDiakIdTextBox.Text = "";
            jegyDatumDatePicker.SelectedDate = DateTime.Today;
            jegyErtekTextBox.Text = "";
            jegyTipusTextBox.Text = "";
            jegyTargyIdTextBox.Text = "";

            targyNevTextBox.Text = "";
            targyKategoriaTextBox.Text = "";
        }

        private int KovetkezoDiakId()
        {
            if (context.Diakok.Any())
            {
                return context.Diakok.Max(d => d.id) + 1;
            }

            return 1;
        }

        private int KovetkezoTargyId()
        {
            if (context.Targyak.Any())
            {
                return context.Targyak.Max(t => t.id) + 1;
            }

            return 1;
        }

        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myDataGrid.SelectedItem == null)
            {
                return;
            }

            if (aktivTabla == "diakok")
            {
                Diak diak = myDataGrid.SelectedItem as Diak;

                if (diak != null)
                {
                    diakNevTextBox.Text = diak.nev;
                    diakOsztalyTextBox.Text = diak.osztaly;
                    diakFiuCheckBox.IsChecked = diak.fiu != 0;
                }
            }
            else if (aktivTabla == "jegyek")
            {
                Jegy jegy = myDataGrid.SelectedItem as Jegy;

                if (jegy != null)
                {
                    jegyDiakIdTextBox.Text = jegy.diakid.ToString();
                    jegyDatumDatePicker.SelectedDate = jegy.datum;
                    jegyErtekTextBox.Text = jegy.ertek.ToString();
                    jegyTipusTextBox.Text = jegy.tipus;
                    jegyTargyIdTextBox.Text = jegy.targyid.ToString();
                }
            }
            else if (aktivTabla == "targyak")
            {
                Targy targy = myDataGrid.SelectedItem as Targy;

                if (targy != null)
                {
                    targyNevTextBox.Text = targy.nev;
                    targyKategoriaTextBox.Text = targy.kategoria;
                }
            }
        }

        private bool DiakAdatokEllenorzese()
        {
            if (string.IsNullOrWhiteSpace(diakNevTextBox.Text))
            {
                MessageBox.Show("A név megadása kötelező!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(diakOsztalyTextBox.Text))
            {
                MessageBox.Show("Az osztály megadása kötelező!");
                return false;
            }

            return true;
        }

        private bool TargyAdatokEllenorzese()
        {
            if (string.IsNullOrWhiteSpace(targyNevTextBox.Text))
            {
                MessageBox.Show("A tárgy nevének megadása kötelező!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(targyKategoriaTextBox.Text))
            {
                MessageBox.Show("A kategória megadása kötelező!");
                return false;
            }

            return true;
        }

        private bool JegyAdatokEllenorzese(
            out int diakId,
            out DateTime datum,
            out int ertek,
            out string tipus,
            out int targyId)
        {
            diakId = 0;
            datum = DateTime.Today;
            ertek = 0;
            tipus = "";
            targyId = 0;

            if (!int.TryParse(jegyDiakIdTextBox.Text, out diakId))
            {
                MessageBox.Show("A Diák ID csak szám lehet!");
                return false;
            }

            if (jegyDatumDatePicker.SelectedDate == null)
            {
                MessageBox.Show("A dátum megadása kötelező!");
                return false;
            }

            datum = jegyDatumDatePicker.SelectedDate.Value;

            if (!int.TryParse(jegyErtekTextBox.Text, out ertek))
            {
                MessageBox.Show("Az érték csak szám lehet!");
                return false;
            }

            if (ertek < 1 || ertek > 5)
            {
                MessageBox.Show("A jegy értéke 1 és 5 között lehet!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(jegyTipusTextBox.Text))
            {
                MessageBox.Show("A típus megadása kötelező!");
                return false;
            }

            tipus = jegyTipusTextBox.Text.Trim();

            if (!int.TryParse(jegyTargyIdTextBox.Text, out targyId))
            {
                MessageBox.Show("A Tárgy ID csak szám lehet!");
                return false;
            }

            int keresettDiakId = diakId;
            int keresettTargyId = targyId;

            bool diakLetezik = context.Diakok.Any(d => d.id == keresettDiakId);

            if (!diakLetezik)
            {
                MessageBox.Show("Nincs ilyen Diák ID az adatbázisban!");
                return false;
            }

            bool targyLetezik = context.Targyak.Any(t => t.id == keresettTargyId);

            if (!targyLetezik)
            {
                MessageBox.Show("Nincs ilyen Tárgy ID az adatbázisban!");
                return false;
            }

            return true;
        }

        private void Hozzaadas_click(object sender, RoutedEventArgs e)
        {
            if (aktivTabla == "")
            {
                MessageBox.Show("Először válassz ki egy oldalt!");
                return;
            }

            if (aktivTabla == "diakok")
            {
                if (!DiakAdatokEllenorzese())
                {
                    return;
                }

                Diak ujDiak = new Diak
                {
                    id = KovetkezoDiakId(),
                    nev = diakNevTextBox.Text.Trim(),
                    osztaly = diakOsztalyTextBox.Text.Trim(),
                    fiu = diakFiuCheckBox.IsChecked == true ? 1 : 0
                };

                context.Diakok.Add(ujDiak);

                if (!BiztonsagosMentes())
                {
                    return;
                }

                DiakokBetoltese();
                MezoTorles();

                MessageBox.Show("Diák sikeresen hozzáadva!");
            }
            else if (aktivTabla == "jegyek")
            {
                if (!JegyAdatokEllenorzese(out int diakId, out DateTime datum, out int ertek, out string tipus, out int targyId))
                {
                    return;
                }

                Jegy ujJegy = new Jegy
                {
                    diakid = diakId,
                    datum = datum,
                    ertek = ertek,
                    tipus = tipus,
                    targyid = targyId
                };

                context.Jegyek.Add(ujJegy);

                if (!BiztonsagosMentes())
                {
                    return;
                }

                JegyekBetoltese();
                MezoTorles();

                MessageBox.Show("Jegy sikeresen hozzáadva!");
            }
            else if (aktivTabla == "targyak")
            {
                if (!TargyAdatokEllenorzese())
                {
                    return;
                }

                Targy ujTargy = new Targy
                {
                    id = KovetkezoTargyId(),
                    nev = targyNevTextBox.Text.Trim(),
                    kategoria = targyKategoriaTextBox.Text.Trim()
                };

                context.Targyak.Add(ujTargy);

                if (!BiztonsagosMentes())
                {
                    return;
                }

                TargyakBetoltese();
                MezoTorles();

                MessageBox.Show("Tárgy sikeresen hozzáadva!");
            }
        }

        private void Modositas_click(object sender, RoutedEventArgs e)
        {
            if (myDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Nincs kijelölt rekord!");
                return;
            }

            if (aktivTabla == "diakok" && myDataGrid.SelectedItem is Diak kijeloltDiak)
            {
                if (!DiakAdatokEllenorzese())
                {
                    return;
                }

                kijeloltDiak.nev = diakNevTextBox.Text.Trim();
                kijeloltDiak.osztaly = diakOsztalyTextBox.Text.Trim();
                kijeloltDiak.fiu = diakFiuCheckBox.IsChecked == true ? 1 : 0;

                if (!BiztonsagosMentes())
                {
                    return;
                }

                DiakokBetoltese();
                MezoTorles();

                MessageBox.Show("Diák sikeresen módosítva!");
            }
            else if (aktivTabla == "jegyek" && myDataGrid.SelectedItem is Jegy kijeloltJegy)
            {
                if (!JegyAdatokEllenorzese(out int diakId, out DateTime datum, out int ertek, out string tipus, out int targyId))
                {
                    return;
                }

                kijeloltJegy.diakid = diakId;
                kijeloltJegy.datum = datum;
                kijeloltJegy.ertek = ertek;
                kijeloltJegy.tipus = tipus;
                kijeloltJegy.targyid = targyId;

                if (!BiztonsagosMentes())
                {
                    return;
                }

                JegyekBetoltese();
                MezoTorles();

                MessageBox.Show("Jegy sikeresen módosítva!");
            }
            else if (aktivTabla == "targyak" && myDataGrid.SelectedItem is Targy kijeloltTargy)
            {
                if (!TargyAdatokEllenorzese())
                {
                    return;
                }

                kijeloltTargy.nev = targyNevTextBox.Text.Trim();
                kijeloltTargy.kategoria = targyKategoriaTextBox.Text.Trim();

                if (!BiztonsagosMentes())
                {
                    return;
                }

                TargyakBetoltese();
                MezoTorles();

                MessageBox.Show("Tárgy sikeresen módosítva!");
            }
        }

        private void Torles_click(object sender, RoutedEventArgs e)
        {
            if (myDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Nincs kijelölt rekord!");
                return;
            }

            MessageBoxResult valasz = MessageBox.Show(
                "Biztosan törölni szeretnéd?",
                "Törlés megerősítése",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (valasz != MessageBoxResult.Yes)
            {
                return;
            }

            if (aktivTabla == "diakok" && myDataGrid.SelectedItem is Diak kijeloltDiak)
            {
                var kapcsolodoJegyek = context.Jegyek
                    .Where(j => j.diakid == kijeloltDiak.id)
                    .ToList();

                context.Jegyek.RemoveRange(kapcsolodoJegyek);
                context.Diakok.Remove(kijeloltDiak);

                if (!BiztonsagosMentes())
                {
                    return;
                }

                DiakokBetoltese();
                MezoTorles();

                MessageBox.Show("Diák sikeresen törölve!");
            }
            else if (aktivTabla == "jegyek" && myDataGrid.SelectedItem is Jegy kijeloltJegy)
            {
                context.Jegyek.Remove(kijeloltJegy);

                if (!BiztonsagosMentes())
                {
                    return;
                }

                JegyekBetoltese();
                MezoTorles();

                MessageBox.Show("Jegy sikeresen törölve!");
            }
            else if (aktivTabla == "targyak" && myDataGrid.SelectedItem is Targy kijeloltTargy)
            {
                var kapcsolodoJegyek = context.Jegyek
                    .Where(j => j.targyid == kijeloltTargy.id)
                    .ToList();

                context.Jegyek.RemoveRange(kapcsolodoJegyek);
                context.Targyak.Remove(kijeloltTargy);

                if (!BiztonsagosMentes())
                {
                    return;
                }

                TargyakBetoltese();
                MezoTorles();

                MessageBox.Show("Tárgy sikeresen törölve!");
            }
        }

        private void Frissites_click(object sender, RoutedEventArgs e)
        {
            if (aktivTabla == "diakok")
            {
                DiakokBetoltese();
            }
            else if (aktivTabla == "jegyek")
            {
                JegyekBetoltese();
            }
            else if (aktivTabla == "targyak")
            {
                TargyakBetoltese();
            }
            else
            {
                FoMenuMegjelenit();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            context.Dispose();
            base.OnClosed(e);
        }
    }
}