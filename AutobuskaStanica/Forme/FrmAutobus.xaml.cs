using AutobuskaStanica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
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

    public partial class FrmAutobus : Window

    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;
        public FrmAutobus()
        {
            InitializeComponent();
           
            konekcija = kon.KreirajKonekciju();
            fillComboBox();
        }

        public FrmAutobus(bool azuriraj, DataRowView red)
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
                string PopuniParking = @"select parkingID, lokacija from tblParking";
                SqlDataAdapter daParking = new SqlDataAdapter(PopuniParking, konekcija);
                DataTable dtParking = new DataTable();
                daParking.Fill(dtParking);
                cbParking.ItemsSource = dtParking.DefaultView;
                daParking.Dispose();
                dtParking.Dispose();

                string PopuniPolazak = @"select polazakID, datum from tblPolazak";
                SqlDataAdapter daPolazak = new SqlDataAdapter(PopuniPolazak, konekcija);
                DataTable dtPolazak = new DataTable();
                daPolazak.Fill(dtPolazak);
                cbPolazak.ItemsSource = dtPolazak.DefaultView;
                daPolazak.Dispose();
                dtPolazak.Dispose();
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
                
                

                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@nazivKompanije", SqlDbType.NVarChar).Value = txtNaziv.Text;
                cmd.Parameters.Add("@tipAut", SqlDbType.NVarChar).Value = txtTip.Text;
                cmd.Parameters.Add("@putanjaAut", SqlDbType.NVarChar).Value = txtPutanja.Text;
                
                cmd.Parameters.Add("@parkingID", SqlDbType.Int).Value = (int)cbParking.SelectedValue;
                cmd.Parameters.Add("@polazakID", SqlDbType.Int).Value = (int)cbPolazak.SelectedValue;



                if (azuriraj)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblAutobus set nazivKompanije = @nazivKompanije, tipAut = @tipAut, putanjaAut = @putanjaAut, parkingID = @parkingID, polazakID = @polazakID where autobusID = @ID";
                    red = null;
                }
                else
                {

                    cmd.CommandText = @"insert into tblAutobus (nazivKompanije ,tipAut, putanjaAut, parkingID, polazakID)values(@nazivKompanije, @tipAut, @putanjaAut, @parkingID, @polazakID)";
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
