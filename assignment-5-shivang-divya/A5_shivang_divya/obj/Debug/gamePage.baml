   M S B A M L   `   `   `  ����K  GA5_shivang_divya, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:A5_shivang_divyaA5_shivang_divya  5          A5_shivang_divya.gamePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:A5_shivang_divya     5        N� 
Background
  ����� N� Title$ gamePage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��	WordQuest��6!   $��Center=�62   $��24(�6O   $
��Bold"�6]   $
��0,10q�6o   �� 5   
   -   5         ��Name$ 	EndButton��5      $��0��6   $��Right=�61   $��End Game��6M   $��70��6`   $��30��6k   $��10q�6w   6�   �� 5   
   -   5         ��Name$ 
NameHolder��5      $��18(�6$   $��Center=�62   $��1��6O   $�� ��6\   $��10q�6g   �� 5   
   $��2��6    �� Orientation$ Vertical=�6"   $��Center=�69   $��10q�6V   .+�5      �� $#��Guess words from this string:��6   $��Center=�6<   $��14(�6Y   �� 5      -   5         ��Name$ StringHolder��5      $��Center=�6.   $��2��6K   $�� ��6X   $��18(�6`   5      �� 5"   
   $��3��6   $ 
Horizontal=�6"   $��Center=�6;   $��10q�6X   .+�5#      �� $��Number of Words: ��6   $��14(�61   �� 5$      -   5        $ NumberOfWordsTextBlock��5$      $��18(�68   5%      �� 5(   
   $��4��6   $ Vertical=�6"   $��Center=�69   $��10q�6V   .+�5)      �� $��Center=�6   $��Guess Input: ��65   $��14(�6J   �� 5*      -   5         ��Name$ GuessInputTextBox��5*      $	��200��61   $��27��6=   $��5q�6I   $��18(�6T   5+      �� 5,   
   -   5        $ guessButton��5,      $��5��6   $	��Try��63   $��70��6A   $��30��6L   6t   �� 5/   
   -   5        $��infoBox��5/   !   $��6��6   $��Center=�60   $�� ��6M   $��16(�6U   
��� ���6c   50      51      