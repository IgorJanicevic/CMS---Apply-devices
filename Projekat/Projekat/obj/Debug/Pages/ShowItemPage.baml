   M S B A M L   `   `   `  ����C  ?Projekat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Projekat.PagesProjekat.Pages  5      !    Projekat.Pages.ShowItemPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Projekat.Pages     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5          � Title$  ShowItemPage��5       � AllowsTransparency . 
������  � WindowStyle$
 None=�5      .��5      �� $	��700��5   	   $	��450��5   	   
������ .+�5   
   ��    StyleForPage#��[  6   � 5   
   $	��700��5      $	��450��5      
������  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $
��1.4* �6#   �� 5       $��* �6#   .+�5#      �� -   5         ��Name$ TitleTextBox��5$      $��Center=�5%        	titleText#��[ 5&      $��TITLE��5'      �� 5(      -   5         ��Name$ btnExit��5)      $��1��5*      $��0,8,10,0q�5+      $	��Top=�5,        CloseButtonStyle#��[ 5.      �� 50      -   5         ��Name$ 	ItemImage��51      $��1��52      $��2��53      $��0��54      $��10,10,10,40q�55      �� 56      $��2��6    �� Orientation$ 
Horizontal=�6&   .+�57      �� $��2��58      $��
20,170,0,0q�59      $��Date Added:��5:      $��10(�5;      $
��Bold"�5<      �� 5=      -   5        	 ��Name$	 DateAddedLabel��5>      $��2��5?      $��	0,170,0,0q�5@      $��10(�5A      $
��Bold"�5B      5C      �� 5E      $��1��5F      $��1��5G      $��Description:��5H      $
��Bold"�5I      
�������5J      �� 5K      $��1��5L      $��1��5M      $��0,0,210,150q�5N      �� 5O      -   5        
 ��Name$
 DescriptionRichTextBox��5P      $��1��5Q      $��2��5R      $��1��5S      $��5,30,10,100q�5T      
������ 
������  �� 
IsReadOnly . " �� VerticalScrollBarVisibility$ Hidden=�5X      �� 5Z      -   5        $ 	btnGoBack��5[      $��2��5\      $��1��5]      $	��120��5^      $��15q�5_      $��Center=�5`      $��Bottom=�5a      $��Go Back  🢩��5c       
 button#��[ 5d      5f      5g      5h      