   M S B A M L   `   `   `  ����F  BWPFLibreria, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WPFLibreriaWPFLibreria  5      ?$clr-namespace:WPFLibreria.ViewModelsWPFLibreria.ViewModels  5      !    WPFLibreria.Consulta_Libros   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      +localclr-namespace:WPFLibreria     5      3vm$clr-namespace:WPFLibreria.ViewModels     5        � DataContext  5      .   (WPFLibreria.ViewModels.LibroDisplayModel  5   
   .��5
      � $	��326��6    � RowDefinitions 5   
   �� 5      $��3* �6   6*   �� 5      $
��Auto �6   6,   �� 5      $��7* �6   6*   .+�5   
   �� $��Right=�6    �� Orientation$ 
Horizontal=�62   $��	5,2,10,10q�6K   .+�5      �� $��2,5,0,0q�6:   $��5q�6L   
���/kU�$��35��6t   $��84��6�   ��6   �� *GetLibrosCommand+.��5      DB Actualizar6#   �� 5      $��2,5,0,0q�6   $��2q�6'   
���/kU�$��35��6O   $��50��6[   ��5      �� *ClearCommand+.��64   	Limpiar6=   �� 5      $��2,5,0,0q�6   $��2q�6'   
���/kU�$��35��6O   $��61��6[   ��5      �� *UpdateCommand+.��65   
Actualizar6A   �� 5      $��2,5,0,0q�6   $��2q�6'   
���/kU�$��35��6O   $��50��6[   ��5      �� *DeleteCommand+.��65   
Eliminar6?   �� 5      $��2,5,0,0q�6   
���/kU�$��35��6C   $��50��6O   $��2q�5   2   ��6   �� *
AddCommand+.��6>   	Agregar6G   5      � 5   
   $��10q�6   $��1��6    � ColumnDefinitions 5      �� 5       $
��Auto �6#   62   �� 5!      6%    5#      �� 5$      $
��Auto �6    60   �� 5%      $
��Auto �6    60   �� 5&      $
��Auto �6    60   �� 5'      $
��Auto �6    60   �� 5(      $
��Auto �6    60   �� 5)      $��* �6    6-   �� 5*      $
��Auto �6    60   �� 5+      $
��Auto �6    60   .+�5.      �� 
����  �$��Bernard MT Condensed)�6.   $��7q�6P   Editorial ID:6k   �� 5/      $��5q�6   $��1��6!   $��1q�6d   ��61   ��  �� Path$ Stat.EditorialIDBrush���50      �� $  DisplayedLibro.EditorialID� �� UpdateSourceTrigger$ PropertyChanged=�6n   �� 52      
����  �$��Bernard MT Condensed)�6.   $��7q�6P   $��1��6[   Nombre de Autor:6z   �� 53      $��5q�6   $��1��6"   $��1��6/   $��1q�6r   ��6?   �� $ Stat.NombreAutorBrush���54      �� $  DisplayedLibro.NombreAutor�$ PropertyChanged=�6n   �� 56      
����  �$��Bernard MT Condensed)�6.   $��7q�6P   $��2��6[   Precio Unitario:6z   �� 57      $��5q�6   $��2��6!   $��1��6.   $��1q�6v   ��6@   �� $ Stat.PrecioUnitarioBrush���58      �� $# DisplayedLibro.PrecioUnitario�$ PropertyChanged=�6q   �� 5:      
����  �$��Bernard MT Condensed)�6.   $��7q�6P   $��3��6[   	Genero:6q   �� 5;      $��5q�6   $��3��6!   $��1��6.   $��1q�6m   ��6?   �� $ Stat.GeneroBrush���5<      �� $ DisplayedLibro.Genero�$ PropertyChanged=�6i   �� 5>      
����  �$��Bernard MT Condensed)�6.   $��7,7,7,0q�6P   $��4��6a   Descripcion:6|   �� 5?      $��7q�6   $��5��6!   $��0��6.   $��2��6>    �� TextWrapping$
 Wrap=�5@      " �� VerticalScrollBarVisibility$ Visible=�6&   ��6L   �� $ DisplayedLibro.Descricion�6~   �� 5B      
����  �$��Bernard MT Condensed)�6.   $��	7,25,15,0q�6P   $��6��6c   	Status:6y   �� 5C      $��7q�6   $��7��6!   $��0��6.   $��2��6>   ��5D      �� $ Stat.Status�66   5E      �� 5G   
   $��1��6   $��Stretch=�6$   $��Bottom=�6B    �� ResizeBehavior$ PreviousAndNext=�6]   $��5��5H      5J      5K      