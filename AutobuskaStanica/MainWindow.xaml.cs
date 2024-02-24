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
using AutobuskaStanica.Forme;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using AutobuskaStanica;

namespace AutobuskaStanica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private string ucitanatabela;
        private bool azuriraj;
        private DataRowView red;



        // razliciti regioni za selecte
        #region SelectSaUslovom 
        private static string selectUslovKarta = @"select * from tblKarta where kartaID = ";
        private static string selectUslovAutobus = @"select * from tblAutobus where autobusID = ";
        private static string selectUslovKorisnik = @"select * from tblKorisnik where korisnikID = ";
        private static string selectUslovKupac = @"select * from tblKupac where kupacID = ";
        private static string selectUslovTipKarte = @"select * from tblTipKarte where tipID = ";
        private static string selectUslovParking = @"select * from tblParking where parkingID = ";
        private static string selectUslovPolazak = @"select * from tblPolazak where polazakID = ";
        private static string selectUslovStatus = @"select * from tblStatus where statusID = ";
        #endregion

        

        #region Delete sa uslovom
        private static string KartaDelete = @"delete from tblKarta where kartaID = ";
        private static string AutobusDelete = @"delete from tblAutobus where autobusID = ";
        private static string KorisnikDelete = @"delete from tblKorisnik where korisnikID = ";
        private static string KupacDelete = @"delete from tblKupac where kupacID = ";
        private static string ParkingDelete = @"delete from tblPArking where parkingID = ";
        private static string PolazakDelete = @"delete from tblPolazak where polazakID = ";
        private static string StatusDelete = @"delete from tblStatus where statusID = ";
        private static string TipKarteDelete = @"delete from tblTipKarte where tipID = ";

        #endregion

        #region Select Upiti
        private static string KartaSelect = @"select kartaID as ID, cenaKarte as 'Cena karte', brSedista as 'Broj sedista', datumIzdavanja as 'Datum izdavanja', tblTipKarte.nazivKarte, tblKupac.imeKup, tblKorisnik.imeKor, tblAutobus.nazivKompanije from tblKarta 
                                                        join tblTipKarte on tblKarta.tipID = tblTipKarte.tipID
                                                        join tblKupac on tblKarta.kupacID = tblKupac.kupacID
                                                        join tblKorisnik on tblKarta.korisnikID = tblKorisnik.korisnikID
                                                        join tblAutobus on tblKarta.autobusID = tblAutobus.autobusID";

        private static string AutobusSelect = @"select autobusID as ID, nazivKompanije as 'Naziv kompanije', tipAut as 'Tip autobusa', putanjaAut as 'Putanja autobusa', tblParking.lokacija as Parking , tblPolazak.datum as Polazak from tblAutobus
                                                        join tblParking on tblAutobus.parkingID = tblParking.parkingID
                                                        join tblPolazak on tblAutobus.polazakID = tblPolazak.polazakID";
                                                        
        private static string StatusSelect = @"select statusID as ID, nazivSta as Status from tblStatus";

        private static string TipKarteSelect = @"select tipID as ID, nazivKarte as 'Naziv karte',popust as Popust from tblTipKarte"; 

        private static string PolazakSelect = @"select polazakID as ID, datum as Datum, vreme as Vreme, vremeDolaska as 'Vreme dolaska' from tblPolazak";

        private static string ParkingSelect = @"select parkingID as ID, lokacija as Lokacija, brParkinga as 'Broj parkinga' from tblParking";

        private static string KupacSelect = @"select kupacID as ID, imeKup as Ime, prezimeKup as 'Prezime', jmbgKup as JMBG, lokacijaKup as Lokacija, kontaktKup as Kontakt, tblStatus.nazivSta from tblKupac
                                                        join tblStatus on tblKupac.statusID = tblStatus.statusID";

        private static string KorisnikSelect = @"select korisnikID as ID, imeKor as Ime, prezimeKor as Prezime, jmbgKor as JMBG, koriIme as 'Korisnicko ime', lozKor as Lozinka, kontaktKor as Kontakt from tblKorisnik";

        

        #endregion


        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UcitajPodatke(KartaSelect);
        }

        private void UcitajPodatke(string selectUpiti)
        {


            try
            {
                konekcija.Open();
                SqlDataAdapter dataadapter = new SqlDataAdapter(selectUpiti, konekcija);
                DataTable dataTable = new DataTable();
                dataadapter.Fill(dataTable);

                if (dataGridCentralni != null)
                {
                    dataGridCentralni.ItemsSource = dataTable.DefaultView;


                }
                ucitanatabela = selectUpiti;
                dataadapter.Dispose();
                dataTable.Dispose();


            }
            catch (SqlException)
            {

                MessageBox.Show("Neuspesno ucitani podaci", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnAutobus_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(AutobusSelect);
        }

        private void btnKorisnik_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(KorisnikSelect);
        }
        private void btnKupac_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(KupacSelect);
        }

        private void btnParking_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(ParkingSelect);
        }

        private void btnPolazak_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(PolazakSelect);
        }

        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(StatusSelect);
        }

        private void btnTipKarte_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(TipKarteSelect);
        }
        private void btnKarta_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(KartaSelect);
        }


        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;

            if (ucitanatabela.Equals(KartaSelect))
            {

                prozor = new FrmKarta();
                prozor.ShowDialog();
                UcitajPodatke(KartaSelect);
            }
            if (ucitanatabela.Equals(AutobusSelect))
            {

                prozor = new FrmAutobus();
                prozor.ShowDialog();
                UcitajPodatke(AutobusSelect);
            }

            if (ucitanatabela.Equals(KorisnikSelect))
            {

                prozor = new FrmKorisnik();
                prozor.ShowDialog();
                UcitajPodatke(KorisnikSelect);
            }
            if (ucitanatabela.Equals(KupacSelect))
            {

                prozor = new FrmKupac();
                prozor.ShowDialog();
                UcitajPodatke(KupacSelect);
            }
            if (ucitanatabela.Equals(ParkingSelect))
            {

                prozor = new FrmParking();
                prozor.ShowDialog();
                UcitajPodatke(ParkingSelect);
            }
            if (ucitanatabela.Equals(PolazakSelect))
            {

                prozor = new FrmPolazak();
                prozor.ShowDialog();
                UcitajPodatke(PolazakSelect);
            }
            if (ucitanatabela.Equals(StatusSelect))
            {

                prozor = new FrmStatus();
                prozor.ShowDialog();
                UcitajPodatke(StatusSelect);
            }
            if (ucitanatabela.Equals(TipKarteSelect))
            {

                prozor = new FrmTipKarte();
                prozor.ShowDialog();
                UcitajPodatke(TipKarteSelect);
            }



        }

        private void btnIzmeni_Click_1(object sender, RoutedEventArgs e)
        {
            if (ucitanatabela.Equals(AutobusSelect))
            {
                PopuniFormu(selectUslovAutobus);
                UcitajPodatke(AutobusSelect);
            }
            else if (ucitanatabela.Equals(KartaSelect))
            {
                PopuniFormu(selectUslovKarta);
                UcitajPodatke(KartaSelect);
            }
            else if (ucitanatabela.Equals(ParkingSelect))
            {
                PopuniFormu(selectUslovParking);
                UcitajPodatke(ParkingSelect);
            }
            else if (ucitanatabela.Equals(PolazakSelect))
            {
                PopuniFormu(selectUslovPolazak);
                UcitajPodatke(PolazakSelect);
            }
            else if (ucitanatabela.Equals(StatusSelect))
            {
                PopuniFormu(selectUslovStatus);
                UcitajPodatke(StatusSelect);
            }
            else if (ucitanatabela.Equals(TipKarteSelect))
            {
                PopuniFormu(selectUslovTipKarte);
                UcitajPodatke(TipKarteSelect);
            }
            else if (ucitanatabela.Equals(KupacSelect))
            {
                PopuniFormu(selectUslovKupac);
                UcitajPodatke(KupacSelect);
            }
            else if (ucitanatabela.Equals(KorisnikSelect))
            {
                PopuniFormu(selectUslovKorisnik);
                UcitajPodatke(KorisnikSelect);
            }
        }

        private void PopuniFormu(object selectUslov)
        {


            try
            {
                konekcija.Open();
                azuriraj = true;
                red = (DataRowView)dataGridCentralni.SelectedItems[0];
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader();
                if (citac.Read())
                {
                    if (ucitanatabela.Equals(KartaSelect))
                    {
                        FrmKarta prozorKarta = new FrmKarta(azuriraj, red);
                        prozorKarta.txtCena.Text = citac["cenaKarte"].ToString();
                        prozorKarta.txtBrSedista.Text = citac["brSedista"].ToString();
                        prozorKarta.dpDatum.SelectedDate = (DateTime)citac["datumIzdavanja"];
                        prozorKarta.cbTip.SelectedValue = citac["tipID"].ToString();
                        prozorKarta.cbKupac.SelectedValue = citac["kupacID"].ToString();
                        prozorKarta.cbKorisnik.SelectedValue = citac["korisnikID"].ToString();
                        prozorKarta.cbAutobus.SelectedValue = citac["autobusID"].ToString();
                        prozorKarta.ShowDialog();
                    }
                    else if (ucitanatabela.Equals(AutobusSelect))
                    {
                        FrmAutobus prozorAutobus = new FrmAutobus(azuriraj, red);
                        prozorAutobus.txtNaziv.Text = citac["nazivKompanije"].ToString();
                        prozorAutobus.txtTip.Text = citac["tipAut"].ToString();
                        prozorAutobus.txtPutanja.Text = citac["putanjaAut"].ToString();
                        prozorAutobus.cbParking.SelectedValue = citac["parkingID"].ToString();
                        prozorAutobus.cbPolazak.SelectedValue = citac["polazakID"].ToString();
                        prozorAutobus.ShowDialog();
                    }
                    else if (ucitanatabela.Equals(KorisnikSelect))
                    {
                        FrmKorisnik prozorKorisnik = new FrmKorisnik(azuriraj, red);
                        prozorKorisnik.txtIme.Text = citac["imeKor"].ToString();
                        prozorKorisnik.txtPrezime.Text = citac["prezimeKor"].ToString();
                        prozorKorisnik.txtJmbg.Text = citac["jmbgKor"].ToString();
                        prozorKorisnik.txtKorIme.Text = citac["koriIme"].ToString();
                        prozorKorisnik.txtLozinka.Text = citac["lozKor"].ToString();
                        prozorKorisnik.txtKontakt.Text = citac["kontaktKor"].ToString();
                        prozorKorisnik.ShowDialog();
                    }
                    else if (ucitanatabela.Equals(KupacSelect))
                    {
                        FrmKupac prozorKupac = new FrmKupac(azuriraj, red);
                        prozorKupac.txtIme.Text = citac["imeKup"].ToString();
                        prozorKupac.txtPrezime.Text = citac["prezimeKup"].ToString();
                        prozorKupac.txtJmbg.Text = citac["jmbgKup"].ToString();
                        prozorKupac.txtLokacija.Text = citac["lokacijaKup"].ToString();
                        prozorKupac.txtKontakt.Text = citac["kontaktKup"].ToString();
                        prozorKupac.cbStatus.SelectedValue = citac["statusID"].ToString();
                        prozorKupac.ShowDialog();
                    }
                    else if (ucitanatabela.Equals(StatusSelect))
                    {
                        FrmStatus prozorStatus = new FrmStatus(azuriraj, red);
                        prozorStatus.txtNaziv.Text = citac["nazivSta"].ToString();
                        prozorStatus.ShowDialog();
                    }
                    else if (ucitanatabela.Equals(ParkingSelect))
                    {
                        FrmParking prozorParking = new FrmParking(azuriraj, red);
                        prozorParking.txtLokacija.Text = citac["lokacija"].ToString();
                        prozorParking.txtBrParkinga.Text = citac["brParkinga"].ToString();
                        prozorParking.ShowDialog();
                    }
                    else if (ucitanatabela.Equals(PolazakSelect))
                    {
                        FrmPolazak prozorPolazak = new FrmPolazak(azuriraj, red);
                        prozorPolazak.dpDatum.SelectedDate = (DateTime)citac["datum"];
                        prozorPolazak.dpDatum.SelectedDate = (DateTime)citac["vreme"];
                        prozorPolazak.dpDatum.SelectedDate = (DateTime)citac["vremeDolaska"];
                        prozorPolazak.ShowDialog();
                    }
                    else if (ucitanatabela.Equals(TipKarteSelect))
                    {
                        FrmTipKarte prozorTipKarte = new FrmTipKarte(azuriraj, red);
                        prozorTipKarte.txtNaziv.Text = citac["nazivKarte"].ToString();
                        prozorTipKarte.ckPopust.ContextMenu = (ContextMenu)citac["popust"];
                        prozorTipKarte.ShowDialog();
                    }


                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Nije selektovan ni jedan red!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }



        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanatabela.Equals(KartaSelect))
            {
                BrisanjePodataka(KartaDelete);
                UcitajPodatke(KartaSelect);
            }
            else if (ucitanatabela.Equals(AutobusSelect))
            {
                BrisanjePodataka(AutobusDelete);
                UcitajPodatke(AutobusSelect);
            }
            else if (ucitanatabela.Equals(KorisnikSelect))
            {
                BrisanjePodataka(KorisnikDelete);
                UcitajPodatke(KorisnikSelect);
            }
            else if (ucitanatabela.Equals(KupacSelect))
            {
                BrisanjePodataka(KupacDelete);
                UcitajPodatke(KupacSelect);
            }
            else if (ucitanatabela.Equals(StatusSelect))
            {
                BrisanjePodataka(StatusDelete);
                UcitajPodatke(StatusSelect);
            }
            else if (ucitanatabela.Equals(ParkingSelect))
            {
                BrisanjePodataka(ParkingDelete);
                UcitajPodatke(ParkingSelect);
            }
            else if (ucitanatabela.Equals(TipKarteSelect))
            {
                BrisanjePodataka(TipKarteDelete);
                UcitajPodatke(TipKarteSelect);
            }
            else if (ucitanatabela.Equals(PolazakSelect))
            {
                BrisanjePodataka(PolazakDelete);
                UcitajPodatke(PolazakSelect);
            }
        }
        private void BrisanjePodataka(object DeleteUslov)
        {
            try
            {
                konekcija.Open();
                red = (DataRowView)dataGridCentralni.SelectedItems[0];
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da obrisete?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = DeleteUslov + "@ID";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Nije selektovan ni jedan red!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Ima povezanih podataka sa drugim tabelama!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }


    }
}