   M S B A M L   `   `   `  ����K  GLunaBuildCreator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:LunaBuildCreatorLunaBuildCreator  5   	   !    LunaBuildCreator.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:LunaBuildCreator     5   	     � Title$  Danko builder��5   	   $	��600��6   $	��500��6,    � 
ResizeMode$ NoResize=�68   .��5	      � .+�5
   
   �� $��/Src/images/logo.png��6   $��Center=�6.   $	��Top=�6K   $	��400��6c   �� 5   
   -   5         ��Name$ bt��5   x   $��50��6   $	��120��6   $��Center=�6C   $	��Top=�6`   $��	0,140,0,0q�6�   .��5      �� Choose html file5      5      �� 5   
   -   5        $��50��5      $	��120��6   $��Center=�6H   .��5      �� Choose directory5      5      �� 5   
   -   5        $��OpenPathButton��5   i   $��30��6   $��60��6   $��Right=�6@   $	��100q�6\   .��5      �� 	Open path5      5      �� 5   
   -   5        $��CreateBuildsButton��5   n   $	��100��6   $	��100��6   $��Bottom=�6G   $��20q�6b   .��5      �� Create builds5      5      �� 5   
   -   5        $��50��5      $	��120��6   $��Center=�6E   $	��Top=�6b   $��	0,374,0,0q�6{   .��5       �� Choose Image5"      5#      �� 5$   
   -   5         ��Name$ ChooseFileText��5$   J   $��Center=�6   $	��Top=�61   $��	0,119,0,0q�6b   5%      �� 5&   
   -   5        $ ChooseDirectoryText��5&   \   $��Center=�6   $	��Top=�61   $��	0,230,0,0q�6I   5'      �� 5(   
   $	��Top=�6   $��Center=�6,   $��0,58,0,0q�6I   !Название плеебла5*      �� 5+   
   -   5         ��Name$ PlayableName��5+   T   $	��400��6   $��30��6   $	��Top=�6*   $��0,79,0,0q�6B   .��6j   5,      �� 5-   
   -	   5        $ ImageDirection��5-   J   $��Center=�6   $	��Top=�61   $��	0,353,0,0q�6b   5.      �� 50   
   -
   5        $��OpenAlChech��50   �   $��30��6   $��60��6   $��Right=�6A   $��Bottom=�6]   $��
0,0,100,20q�6x   .��51      �� 
Check AL53      54      �� 55   
   -   5        $��OpenMvChech��55   �   $��30��6   $��60��6   $��Right=�6A   $��Bottom=�6]   $��	0,0,24,20q�6x   .��56      �� 
Check MV58      59      5:      5;      