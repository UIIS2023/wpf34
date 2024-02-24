using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Shapes;

namespace AutobuskaStanica.Forme
{
   
    public partial class FrmKarta : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public FrmKarta()
        {

            InitializeComponent();
           
            konekcija = kon.KreirajKonekciju();
            fillComboBox();
        }

        public FrmKarta(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            fillComboBox();
            this.azuriraj = azuriraj;
            this.red = red;
            
        }

        private void fillComboBox()
        {
            try
            {
                konekcija.Open();
                string PopuniTip = @"select tipID, nazivKarte from tblTipKarte";
                SqlDataAdapter daTip = new SqlDataAdapter(PopuniTip, konekcija);
                DataTable dtTip = new DataTable();
                daTip.Fill(dtTip);
                cbTip.ItemsSource = dtTip.DefaultView;
                daTip.Dispose();
                dtTip.Dispose();

                string PopuniKorisnik = @"select korisnikID, imeKor from tblKorisnik";
                SqlDataAdapter daKorisnik = new SqlDataAdapter(PopuniKorisnik, konekcija);
                DataTable dtKorisnik = new DataTable();
                daKorisnik.Fill(dtKorisnik);
                cbKorisnik.ItemsSource = dtKorisnik.DefaultView;
                daKorisnik.Dispose();
                dtKorisnik.Dispose();

                string PopuniKupac = @"select kupacID, imeKup from tblKupac";
                SqlDataAdapter daKupac = new SqlDataAdapter(PopuniKupac, konekcija);
                DataTable dtKupac = new DataTable();
                daKupac.Fill(dtKupac);
                cbKupac.ItemsSource = dtKupac.DefaultView;
                daKupac.Dispose();
                dtKupac.Dispose();

                string PopuniAutobus = @"select autobusID, nazivKompanije from tblAutobus";
                SqlDataAdapter daAutobus = new SqlDataAdapter(PopuniAutobus, konekcija);
                DataTable dtAutobus = new DataTable();
                daAutobus.Fill(dtAutobus);
                cbAutobus.ItemsSource = dtAutobus.DefaultView;
                daAutobus.Dispose();
                dtAutobus.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri učitavanju: {ex.Message}", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally { if (konekcija != null) konekcija.Close(); }
        }
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                DateTime date = (DateTime)dpDatum.SelectedDate;
                string datum = date.ToString("yyyy-MM-dd");

                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@cenaKarte", SqlDbType.Int).Value = txtCena.Text;
                cmd.Parameters.Add("@brSedista", SqlDbType.Int).Value = txtBrSedista.Text;
                cmd.Parameters.Add("@datumIzdavanja", SqlDbType.Date).Value = date;
                

                cmd.Parameters.Add("@tipID", SqlDbType.Int).Value = (int)cbTip.SelectedValue;
                cmd.Parameters.Add("@korisnikID", SqlDbType.Int).Value = (int)cbKorisnik.SelectedValue;
                cmd.Parameters.Add("@kupacID", SqlDbType.Int).Value = (int)cbKupac.SelectedValue;
                cmd.Parameters.Add("@autobusID", SqlDbType.Int).Value = (int)cbAutobus.SelectedValue;

                if (this.azuriraj)
                {
                    
                    cmd.Parameters.Add(@"ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update tblKarta set cenaKarte = @cenaKarte, brSedista = @brSedista, datumIzdavanja = @datumIzdavanja, tipID = @tipID, kupacID = @kupacID, korisnikID = @korisnikID, autobusID = @autobusID Where kartaID = @ID";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblKarta (cenaKarte, brSedista, datumIzdavanja, tipID, korisnikID, kupacID, autobusID)values(@cenaKarte, @brSedista, @datumIzdavanja, @tipID, @kupacID, @korisnikID, @autobusID)";
                }
                
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();



            }
            catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih vrednosti nije validan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (FormatException)
            {
                MessageBox.Show("Unete vrednosti nisu u odredjenom formatu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }
        private void btnOtkazi_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
