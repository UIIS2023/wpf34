using AutobuskaStanica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    
    public partial class FrmKupac : Window

    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmKupac()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            fillComboBox();
        }

        public FrmKupac(bool azuriraj, DataRowView red)
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
                string PopuniStatus = @"select statusID, nazivSta from tblStatus";
                SqlDataAdapter daStatus = new SqlDataAdapter(PopuniStatus, konekcija);
                DataTable dtStatus = new DataTable();
                daStatus.Fill(dtStatus);
                cbStatus.ItemsSource = dtStatus.DefaultView;
                daStatus.Dispose();
                dtStatus.Dispose();
            }
            catch
            {
                MessageBox.Show("Greška pri ucitavanju", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally { if (konekcija != null) konekcija.Close(); }
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();


                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@imeKup", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@prezimeKup", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@jmbgKup", SqlDbType.NVarChar).Value = txtJmbg.Text;
                cmd.Parameters.Add("@lokacijaKup", SqlDbType.NVarChar).Value = txtLokacija.Text;
                cmd.Parameters.Add("@kontaktKup", SqlDbType.NVarChar).Value = txtKontakt.Text;
              
                cmd.Parameters.Add("@statusID", SqlDbType.Int).Value = (int)cbStatus.SelectedValue;


                if (azuriraj)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblKupac set imeKup = @imeKup, prezimeKup = @prezimeKup, jmbgKup = @jmbgKup, lokacijaKup = @lokacijaKup, kontaktKup = @kontaktKup, statusID = @statusID where kupacID = @ID";
                    red = null;
                }
                else
                {

                    cmd.CommandText = @"insert into tblKupac (imeKup , prezimeKup, jmbgKup, lokacijaKup, kontaktKup, statusID)values(@imeKup, @prezimeKup, @jmbgKup, @lokacijaKup, @kontaktKup, @statusID)";
                }

                cmd.Dispose();
                cmd.ExecuteNonQuery();
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

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
