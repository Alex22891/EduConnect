using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Windows;

namespace EduConnect
{
    /// <summary>
    /// Логика взаимодействия для AddGroupWindow.xaml
    /// </summary>
    public partial class AddGroupWindow : MetroWindow
    {
        public DatabaseHelper dbHelper;
        public List<Groups> groups { get; set; }
        public AddGroupWindow()
        {
            InitializeComponent();

            dbHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");

            LoadGroups();
        }

        private void LoadGroups()
        {
            groups = dbHelper.GetGroups();
            GroupsDataGrid.ItemsSource = groups;
        }

        private void AddGroupsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(NewGroupsTextBox.Text)) {
                    MessageBox.Show("Введите наименование группы");
                }

                string newGroups = NewGroupsTextBox.Text;

                Groups newGroup = new Groups { Title = newGroups };

                bool success = dbHelper.AddGroups(newGroup);

                if (success)
                {
                    LoadGroups();

                    MessageBox.Show("Группа успешна добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении группы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении группы: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteGroupsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Groups selectedGroups = (Groups)GroupsDataGrid.SelectedItem;

                if (selectedGroups != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данные?", "Подтверждение удаления", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        dbHelper.DeleteGroups(selectedGroups.Id);
                        MessageBox.Show("Данные успешно удалено");

                        LoadGroups();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите данные для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseGroupsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
