   M S B A M L   `   `   `  ���� D  @Milestone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:milestone01milestone01      milestone01.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     M IFluent, Version=7.0.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fFluenturn:fluent-ribbon  :d2http://schemas.microsoft.com/expression/blend/2008  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  %localclr-namespace:milestone01  -     � Title$  Quản Lý Bán Hàng��  Fluent.RibbonWindow   TitleBackground
 ����   TitleForeground
 �   � � WindowState$ 	Maximized=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�$��1370.205�� � Icon$ /Icons/logo.png��$��739.555�� � WindowStartupLocation$ CenterScreen=� � 	Resources 3  -milestone01.doublePriceToStringPriceConverter   priceConverter2  ,milestone01.idProductToNameCategoryConverter  nameCategoryConverter+  %milestone01.descriptCategoryConverter  deScriptCategoryConverter��%-   &	        &	      &	 
           .��� 	 � RowDefinitions	 �� $
��Auto ��� $��40 ��� $��* ��� $��40 �
 � ColumnDefinitions
 �� $	��680 ��� $��* �.+�  Fluent.Ribbon  -   $��0��$��2��   Menu   Fluent.Backstage     Content.    Fluent.BackstageTabControl  .@�  Fluent.Button  -      Header$ 
Thông Tin��  -   $ Thoát��   Tabs. 	  Fluent.RibbonTabItem	   	  Header$ Thông Tin Sản Phẩm�� 	  KeyTip$ M�� 	  Groups. 
  Fluent.RibbonGroupBox
  $��Import Dữ Liệu��
�������.@�  -   $��importExcel��   Size$ Large=�$��80��$��80��$ Excel��   	LargeIcon$ /Icons/excel.png�� J� SnapsToDevicePixels . 
  $��Loại Sản Phẩm��
�������.@�  -   $��addCategory��$ Large=�$ Add Category��$��80��$��80��$ /Icons/category_add.png�� .   -   $��deleteCategory��$ Large=�$ Delete Category��$��80��$��80��$  /Icons/category_delete.png�� . $��Center=�$��Center=�$��Center=�  -   $��editCategory��$ Large=�$ Edite Category��$��80��$��80��$ /Icons/edit_category.png�� . $��Center=�$��Center=�$��Center=�
  $��Sản Phẩm��
�������.@�  -	   $��
addProduct��$ Large=�$ Add Product��$��80��$��80��$ /Icons/add.png�� .   -
   $��deleteProduct��$ Large=�$ Delete Product��$��80��$��80��$ /Icons/delete.png�� .   -   $��editProduct��$ Large=�$ Edite Product��$��80��$��80��$ /Icons/edit.png�� . $��Center=�$��Center=�$��Center=�	  $ Thông Tin Đơn Hàng��$ T��. 
  $��Giỏ Hàng��.@�  -   $
��Cart��$ Large=�$ Giỏ hàng��$��80��$��80��$ /Icons/logo.png�� . 	  $ 	Báo Cáo��$ R��. 
  $��Thống kê��.@�  -   $��
btnRevenue��$ Large=�$ 	Doanh thu��$	��100��$	��100��$ /Icons/revenue.png�� .   -   $��btnInventory��$ Large=�$ Doanh thu theo loại��$	��100��$	��100��$ /Icons/inventory.png�� .   -   $��
btnSellSum��$ Large=�$ 	Đã bán��$	��100��$	��100��$ /Icons/sell.png�� . � $��2��$��0��$��2,0,0,0q�.+��� -   $��listProduct��
������ $��Disabled=�
������ ���� .R�� ����  �� 
TargetType" ��.�-   ���� .R��� -   $	��150��$��	0,10,0,10q�$��Center=�$��Center=�.+��� $	��140��$	��140������ *Image+��  �� TextAlignment$ Center=�$
��Bold"�$��WrapWithOverflow=�$��14(�
���   ����� *Name+�� $ Center=�$��14(�
���   ����� *Price+ �� 	Converter# [  � $��1��
�������
 �� $	��240 ��� $��* �.+��� $)��#Tìm kiếm sản phẩm theo tên:��$��18(�$��5,5,0,0q��� -   $��textBoxFillProduct��$��1��
�������$��25��$��16(�� $��1��$��1��
�������$��0,0,5,0q�
 �� $	��150 ��� $��* �.+��� $��0��$��Loại Sản Phẩm��$��10,5,0,0q�$��18(��� -   $��comboboxCategory��$��1��$	��360��$��25��$
��Left=����� .R��� $
��Bold"�$��16(����� *Name+� -   $��grid_productDetail��$��2��$��1��$��10,0,5,0q�
������ $y�Hidden=�	 �� $��60 ��� $��* �.+��� -   $��categoryDetail��$��0��$��0,0,10,0q� � DataContext ��  �� ElementName$ listProduct�� �� Path$ SelectedItem�.+���  �� Orientation$ 
Horizontal=�.+��� $��Thông Tin Sản Phẩm: ��$
��Wrap=�$��18(�$��Normal$�$
��Bold"��� -   $��categoryNameDetail��$��0,0,0,0q�$��18(�
��� ���$��Italic$�$
��Bold"����� *CateID+# [ �� -   $��NameProductDetail��$ Center=�$
��Wrap=�$��20(�$��	0,10,0,10q�
��� ���$
��Bold"����� *Name+�� -   $��productDetail��$��1�� �� $ listProduct��$ SelectedItem�.+��� $ 
Horizontal=�$��0,10,5,0q�.+��� -   $��productSelected��$	��420��$	��350��$
��Left=����� *Image+�� $ Vertical=�.+��� $ 
Horizontal=�$��	0,110,0,0q�$
��Left=�.+��� $��Giá tiền: ��$��18(�$
��Bold"��� -   $��PriceProductDetail��$
 Left=�$��18(�
���   ����� *Price+# [  �� $ 
Horizontal=�$��0,20,0,0q�$
��Left=�.+��� $��Số lượng: ��$��18(�$
��Bold"��� -   $��	txtAmount��$
 Left=�$
��Wrap=�$��18(�
���   �$��Normal"����� *Amount+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� .+��� -   $��Thêm vào giỏ hàng�����#1976D2
�������$
��Bold"�$��16(�$��50��$	��180��$��0,20,0,0q��� $ 
Horizontal=�$��0,10,0,0q�.+��� $��
Mô tả: ��$��18(�$
��Bold"��� $��16(�
���   �$��Normal$����� *CateID+# [ ]� $��3��$��2��$��2,0,-2,0q���. 
�������.+��� -   $��	statusBar��$��Bottom=�$��60,0,0,0q�
�������$��Center=�.@��� -   $��PreviousButton��$��40��$��25��$��<���� -    $��pagingInfoLabel��$��Trang 1 đến 1���� -!   $��
NextButton��$��40��$��25��$��>���� $!��Số lượng sản phẩm:���� -"   $��itemsPerPageComboBox��$��40�� �� SelectedIndex$ 0��.@��� .��5�� .��8�� .��10