using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Windows;

namespace EduConnect
{
    /// <summary>
    /// Логика взаимодействия для AddSportWindow.xaml
    /// </summary>
    public partial class AddSportWindow : MetroWindow
    {
        public DatabaseHelper dbHelper;
        public List<Sports> Sport {  get; set; }
        public AddSportWindow()
        {
            InitializeComponent();

            dbHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");

            LoadSports();
        }

        private void LoadSports()
        {
            Sport = dbHelper.GetSports();
            SportDataGrid.ItemsSource = Sport;
        }

        private void AddSportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(NewSportTextBox.Text))
                {
                    MessageBox.Show("Введите вид спорта");
                }
                string newSportName = NewSportTextBox.Text;

                Sports newSport = new Sports { SportName = newSportName };

                bool success = dbHelper.AddSport(newSport);

                if (success)
                {
                    LoadSports();

                    MessageBox.Show("Тренер успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении тренера.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении тренера: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteSportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sports selectedSports = (Sports)SportDataGrid.SelectedItem;

                if (selectedSports != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данные?", "Подтверждение удаления", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        dbHelper.DeleteSport(selectedSports.Id);
                        MessageBox.Show("Данные успешно удалено");

                        LoadSports();
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

        private void CloseSportButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
