   M S B A M L   `   `   `  ����K  GAutobuskaStanica, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:AutobuskaStanica.FormeAutobuskaStanica.Forme  5   	   '    !AutobuskaStanica.Forme.FrmAutobus   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:AutobuskaStanica.Forme     5   	     � Title$  
FrmAutobus��5   	   $	��450��6   $	��338��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ 
btnSacuvaj��5
      $��Sačuvaj��6%   $
��Left=�67   $��
28,331,0,0q�6R   $	��Top=�6f   $��75��6~   6�   �� 5   
   -   5        $ 	btnOtkazi��5      $��Otkaži��6$   $
��Left=�65   $��203,331,0,0q�6P   $	��Top=�6e   $��75��6}   6�   �� 5   
   $��Naziv kompanije:��6   $
��Left=�6+   $��	47,52,0,0q�6F   $	��Top=�6Y   �� 5   
   $��Tip autobusa:��6   $
��Left=�6(   $��	47,98,0,0q�6C   $	��Top=�6V   �� 5   
   $��Putanja autobusa:��6   $
��Left=�6,   $��
47,146,0,0q�6G   $	��Top=�6[   �� 5   
   $��Parking:��6   $
��Left=�6#   $��
47,191,0,0q�6>   $	��Top=�6R   �� 5   
   $��Polazak:��6   $
��Left=�6#   $��
47,242,0,0q�6>   $	��Top=�6R   �� 5   
   -   5         ��Name$ txtNaziv��5      $
��Left=�6$   $��
180,56,0,0q�6?    �� TextWrapping$
 Wrap=�6S   $�� ��6g   $	��Top=�6o   $	��120��6�   �� 5   
   -   5        $ txtTip��5      $
��Left=�6"   $��
180,98,0,0q�6=   $
 Wrap=�6Q   $�� ��6e   $	��Top=�6m   $	��120��6�   �� 5   
   -   5        $ 
txtPutanja��5      $
��Left=�6&   $��180,146,0,0q�6A   $
 Wrap=�6V   $�� ��6j   $	��Top=�6r   $	��120��6�   �� 5   
   -   5         ��Name$ 	cbParking��5      $
��Left=�6&   $��180,193,0,0q�6A   $	��Top=�6V   $	��120��6n    �� SelectedValuePath$ 	parkingID��6z    �� DisplayMemberPath$ lokacija��6�   �� 5   
   -   5        $ 	cbPolazak��5      $
��Left=�6&   $��180,252,0,0q�6A   $	��Top=�6V   $	��120��6n   $ 	polazakID��6z   $ datum��6�   5      5      