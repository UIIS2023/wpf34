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

    public partial class FrmTipKarte : Window

    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;

        public FrmTipKarte()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
        }

        public FrmTipKarte(bool azuriraj, DataRowView red)
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
                cmd.Parameters.Add("@nazivKarte", SqlDbType.NVarChar).Value = txtNaziv.Text;
                cmd.Parameters.Add("@popust", SqlDbType.Bit).Value = ckPopust.ContextMenu;



                if (azuriraj)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblTipKarte set nazivKarte = @nazivKarte, popust = @popust where tipID = @ID";
                    red = null;
                }
                else
                {

                    cmd.CommandText = @"insert into tblTipKarte (nazivKarte, popust)values(@nazivKarte, @popust)";
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
