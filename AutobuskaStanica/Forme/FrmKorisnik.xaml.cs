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
  
    public partial class FrmKorisnik : Window

    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmKorisnik()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
        }

        public FrmKorisnik(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            this.azuriraj = azuriraj;
            this.red = red;
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
                cmd.Parameters.Add("@imeKor", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@prezimeKor", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@jmbgKor", SqlDbType.NVarChar).Value = txtJmbg.Text;
                cmd.Parameters.Add("@koriIme", SqlDbType.NVarChar).Value = txtKorIme.Text;
                cmd.Parameters.Add("@lozKor", SqlDbType.NVarChar).Value = txtLozinka.Text;
                cmd.Parameters.Add("@kontaktKor", SqlDbType.NVarChar).Value = txtKontakt.Text;


                if (azuriraj)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblKorisnik set imeKor = @imeKor, prezimeKor = @prezimeKor, jmbgkor = @jmbgKor, koriIme = @koriIme, lozKor  = @lozKor, kontaktKor = @kontaktKor where korisnikID = @ID";
                    red = null;
                }
                else
                {

                    cmd.CommandText = @"insert into tblKorisnik (imeKor , prezimeKor, jmbgKor, koriIme, lozKor, kontaktKor)values(@imeKor, @prezimeKor, @jmbgKor, @koriIme, @lozKor, @kontaktKor)";
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
