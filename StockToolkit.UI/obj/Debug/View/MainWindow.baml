   M S B A M L   `   `   `  ����J  FStockToolkit.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:StockToolkit.UI.ViewStockToolkit.UI.View  5   	   %    StockToolkit.UI.View.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3view"clr-namespace:StockToolkit.UI.View     5   	     � Title$   Stock Toolkit 1.0 for CIBC��5   	   $	��570��6,   $
��1000��69   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��200 �6   �� 5      $
��Auto �6   �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��10q�6   $��40(�6    $��3��6.   �� 5      .�6   Stocks Toolkit6%   5      � 5   
   $��2��6    5      �� 5      $��* �6    �� 5      $
��Auto �6    .+�5!   
   )   #StockToolkit.UI.View.NavigationView   � ToolTip$3 -Please select stocks to show the trade status��5"       � DataContext 5!      �� *NavigationViewModel+5"      �� 5#      $��1��68   $��20(�6E   $
��Bold"�6S    �� Padding$ 3q�6e   
���  ��
����ح���6   �� *LoadingMessage+6�   5$      �� 5&   
   $��1��6   $��2��6$   $��1��65   $��5��6E   
���  ��$��Center=�6d   �� 5)   
   $��1��5+      $��2��6   $��2��6-   
���   ���5)      �� *StockEditViewModels+ �� SelectedItem 5*      �� *SelectedStockEditViewModel+ �� Mode$ TwoWay=���5,      �� 5-       �� 
TargetType" ��6   	 �� BasedOn#	 [B��6-   
 �� Triggers
 5.      P� 5/       P� Property �@��
IsSelected P� Value . . �50      ��  �� Property � �� �� Value$
 Bold"�6;   51      53      ��55      �� 56      .R�57      ��  �� Orientation$ 
Horizontal=�6!   .+�58      �� $��2,2,5,2q�6E   ��6$   �� *Stock.StockName+6E   �� 59      $��X��5:      $��20��5?   %   $��20��5@   %   $
��Bold"�5A   %   $��12(�5B   %   
���������5;      ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" �� �� Path$&  DataContext.CloseStockTabCommand���5>   %   �� 5D      5E      5F       �� ContentTemplate 5H      �� 5I      .R�5J      (   "StockToolkit.UI.View.StockEditView  5K      5M      5N      5O      