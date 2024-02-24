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

    public partial class FrmParking : Window

    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmParking()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
        }

        public FrmParking(bool azuriraj, DataRowView red)
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
                cmd.Parameters.Add("@lokacija", SqlDbType.NVarChar).Value = txtLokacija.Text;
                cmd.Parameters.Add("@brParkinga", SqlDbType.Int).Value = txtBrParkinga.Text;



                if (azuriraj)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblParking set lokacija = @lokacija, brParkinga = @brParkinga where parkingID = @ID";
                    red = null;
                }
                else
                {

                    cmd.CommandText = @"insert into tblParking (lokacija, brParkinga)values(@lokacija, @brParkinga)";
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
