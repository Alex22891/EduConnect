   M S B A M L   `   `   `  ���� E  AEduConnect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:EduConnect
EduConnect  5    /EduConnect.DeputyDirectorForEducationWorkWindow   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f;Controls,http://metro.mahapps.com/winfx/xaml/controls  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:EduConnect       � Title$T  NЗаместитель директора по УВР: Главное окно��$	��800��$
��1200�� � WindowState$ 	Maximized=� � WindowStartupLocation$ CenterScreen=� � 	Resources &    EduConnect.RecordsCountConverter   RecordsCountConverter��%   &	          .��� .+��� $��Stretch=�$��Stretch=�.@��� $��Учащиеся��.���  � RowDefinitions �� $
��Auto ��� $��* � � ColumnDefinitions �� $��158.2 ��� $
��51.8 ��� $��37.896 ��� $��127.534 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� -    ��Name$ SearchTextBox��*  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$5 /Введите данные для поиска��$��0��$��0��$	��200��$��5,5,5,5q�$��2���� -   $)��#Добавить учащегося��$��0��$��2��$��5,5,5,5q�$��2���� -    ��Name$ ExportExcelButton��$��Экспорт в Excel��$��0��$��4��$��5,5,5,5q��� -   $ DeleteStudentButton��$'��!Удалить учащегося��$��0��$��5��$��5,5,5,5q��� -   	 ��Name$	 SportComboBox��$* $Выберите вид спорта��$��0��$��6��$��5,5,5,5q�.@��� 
 �� 
IsSelected
 . .��Все�� .��
Дзюдо�� .��Баскетбол�� .��Вольная борьба�� .��Чир спорт�� .��Ушу�� -   $	 DischargesComboBox��$# Выберите разряд��$��0��$��7��$��5,5,5,5q�.@��� 
 . .��Все�� .��	3 юн.�� .��	2 юн.�� .��	1 юн.�� .��	3 сп.�� .��	2 сп.�� .��	1 сп.�� .��КМС�� .��МС�� .��
МСМК&   System.Windows.Controls.DataGrid  -     Name$ StudentsDataGrid��   AutoGenerateColumns .     CanUserAddRows .  $��	5,26,5,28q�$��1��$��8������ *
Students+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $ Фамилия�� �� *	Surname+  $ Имя�� �� *Name+  $ Отчество�� �� *
Patronymic+  $ Дата рождения�� �� *	BirthDate+ �� StringFormat$ 
dd-MM-yyyy��  $ 
Школа�� �� *School+  $ 
Класс�� �� *Class+  $ Вид спорта�� �� *Sport+  $ ФИО Тренера�� �� *TrainersName+  $ Группа�� �� *EnrollmentGroup+  $ Разряд�� �� *Rank+  $( "№ Приказа(Разряда)�� �� *OrderNumber+  $# Дата зачисления�� �� *DateOfEnrollment+$ 
dd-MM-yyyy��  $- '№ Приказа зачисления�� �� *OrderEnrollment+  $- '№ Приказа отчисления�� �� *OrderDismissal+  $! Форма обучения�� �� *PaymentType+  $ ОРП/СП�� �� *	ORP_or_SP+  $ ФИО родителей�� �� *ParentsFullName+  $! Номер телефона�� �� *PhoneNumber+  $ 
Адрес�� �� *	Address+  $0 *Место работы родителей�� �� *ParentsWorkPlace+  $+ %Должность родителей�� �� *ParentsPosition+  $C =Свидетельство о рождении/паспорт�� �� *BirthCertificate+  $ Когда выдан�� �� *DateOfIssue+$ 
dd-MM-yyyy��  $ Кем выдан�� �� *
IssuedBy+  $ 
СНИЛС�� �� *SNILS+  $ ИНН�� �� *INN+�� $��2��$��5q�����  �� ElementName$ StudentsDataGrid�� �� Path$ Items.Count� �� 	Converter# [  �� $%��История учащихся��.���  �� $
��Auto ��� $��* �.+��� $��0�� �� Orientation$ 
Horizontal=�$
��Left=�$��5q�.+��� -   $ AddHistoryBtn��$��Добавить��$��5q��� -	   $ DeleteHistory��$��Удалить��$��5q��� -
   $ ExportExcelHistory��$��Экспорт в Excel��$��5q��� -   $ SearchHistory��$5 /Введите данные для поиска��$��5q�$��Center=�  -   $ HistoryDataGrid��$��1�� .   .  $��	5,26,5,33q�   $ ID�� �� *ID+  $ ФИО�� �� *
FullName+  $ Разряды�� �� *Rank+  $ Соревнования�� �� *Competitions+  $ Нормативы�� �� *Norms+  $ Год�� �� *Year+�� $��2��$��5q����� $ HistoryDataGrid��$ Items.Count�# [  �� $!��Личный кабинет��.��� .+��� .+��� -    ��Name$ UsernameTextBlock��$'��!Имя пользователя: ��$��16(��� -   $ RoleTextBlock��$��
Роль: ��$��16(�$��44��$	��317��$
��Left=��� $ Vertical=��� -   $ LogoutButton��$��
Выйти��$	��100��$��30��$��Right=�$��Bottom=�$��
864,0,0,10q�