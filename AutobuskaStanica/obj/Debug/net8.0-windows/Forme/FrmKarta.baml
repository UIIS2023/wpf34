   M S B A M L   `   `   `  ����K  GAutobuskaStanica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:AutobuskaStanica.FormeAutobuskaStanica.Forme  5   	   %    AutobuskaStanica.Forme.FrmKarta   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:AutobuskaStanica.Forme     5   	     � Title$  FrmKarta��5   	   $	��450��6   $	��308��6'   .��5	      � $��0,0,0,-6q�6    � ColumnDefinitions 5
   
   �� 5      �� 5      $��0* �6   .+�5   
   �� $��Cena karte:��6   $
��Left=�6&   $��	30,20,0,0q�6A   $	��Top=�6T   $��26��6l   $��69��6x   �� 5   
   $��Datum izdavanja:��6   $
��Left=�6+   $��	30,68,0,0q�6F   $	��Top=�6Y   $��26��6q   $	��101��6}   �� 5   
   -   5         ��Name$ txtCena��5      $
��Left=�6#   $��23��6>   $��
158,20,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $�� ��6r   $	��Top=�6z   $	��120��6�   �� 5   
   $��Broj sedista:��6   $
��Left=�6(   $��
28,114,0,0q�6C   $	��Top=�6W   $��26��6o   $��73��6{   �� 5   
   -   5        $ txtBrSedista��5      $
��Left=�6(   $��23��6C   $��158,117,0,0q�6O   $
 Wrap=�6d   $�� ��6x   $	��Top=�6�   $	��120��6�   �� 5   
   $
��Tip:��6   $
��Left=�6   $��
30,160,0,0q�6:   $	��Top=�6N   $��26��6f   $��29��6r   �� 5   
   $��	Korisnik:��6   $
��Left=�6$   $��
28,208,0,0q�6?   $	��Top=�6S   $��26��6k   $��54��6w   �� 5   
   $��Autobus:��6   $
��Left=�6#   $��
30,298,0,0q�6>   $	��Top=�6R   $��26��6j   $��57��6v   �� 5   
   -   5         ��Name$ 
btnSacuvaj��5      $��Sačuvaj��6%   $
��Left=�67   $��
30,369,0,0q�6R   $	��Top=�6f   $��75��6~   $��20��6�   �� 5   
   -   5        $ 	btnOtkazi��5      $��Otkaži��6$   $
��Left=�65   $��180,369,0,0q�6P   $	��Top=�6e   $��76��6}   $��20��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dpDatum��5      $
��Left=�6&   $��
176,70,0,0q�6A   $	��Top=�6U   $��24��6m   $	��102��6y   �� 5   
   -   5         ��Name$ 
cbKorisnik��5      $
��Left=�6'   $��160,210,0,0q�6B   $	��Top=�6W   $	��122��6o    �� SelectedValuePath$ 
korisnikID��6{    �� DisplayMemberPath$ imeKor��6�   $��22��6�   �� 5   
   -   5        $ 	cbAutobus��5      $
��Left=�6&   $��160,298,0,0q�6A   $	��Top=�6V   $	��122��6n   $ 	autobusID��6z   $ nazivKompanije��6�   $��22��6�   �� 5   
   -   5        $ cbTip��5      $
��Left=�6"   $��160,164,0,0q�6=   $	��Top=�6R   $	��122��6j   $ tipID��6v   $ 
nazivKarte��6�   $��22��6�   �� 5   
   -	   5        $ cbKupac��5      $
��Left=�6$   $��160,255,0,0q�6?   $	��Top=�6T   $	��122��6l   $ kupacID��6x   $ imeKup��6�   $��22��6�   �� 5   
   $��Kupac:��6   $
��Left=�6!   $��
30,255,0,0q�6<   $	��Top=�6P   $��26��6h   $��45��6t   5      5      