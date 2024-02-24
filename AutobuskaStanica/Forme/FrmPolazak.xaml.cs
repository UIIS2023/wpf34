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

    public partial class FrmPolazak : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmPolazak()
        {

            InitializeComponent();
            konekcija = kon.KreirajKonekciju();


        }

        public FrmPolazak(bool azuriraj, DataRowView red)
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
                DateTime date = (DateTime)dpDatum.SelectedDate;
                string datum = date.ToString("yyyy-MM-dd");

                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@datum", SqlDbType.Date).Value = date;
                cmd.Parameters.Add("@vreme", SqlDbType.Date).Value = date;
                cmd.Parameters.Add("@vremeDolaska", SqlDbType.Date).Value = date;


                if (azuriraj)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblPolazak set datum = @datum, vreme = @vreme, vremeDolaska = @vremeDolaska where polazakID = @ID";
                    red = null;
                }
                else
                {

                    cmd.CommandText = @"insert into tblPolazak (datum, vreme, vremeDolaska)values(@datum, @vreme, @vremeDolaska)";
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
