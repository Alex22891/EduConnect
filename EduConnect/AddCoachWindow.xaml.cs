using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Windows;

namespace EduConnect
{
    /// <summary>
    /// Логика взаимодействия для AddCoachWindow.xaml
    /// </summary>
    public partial class AddCoachWindow : MetroWindow
    {
        public DatabaseHelper dbHelper;
        public List<Coaches> Coaches {  get; set; }
        public AddCoachWindow()
        {
            InitializeComponent();

            dbHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");

            LoadTrainers();
        }

        private void LoadTrainers()
        {
            Coaches = dbHelper.GetTrainers();
            CoachesDataGrid.ItemsSource = Coaches;
        }

        private void AddCoachButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(NewCoachTextBox.Text))
                {
                    MessageBox.Show("Введите фио тренера");
                }
                string newCoachName = NewCoachTextBox.Text;

                Coaches newCoach = new Coaches { FullName = newCoachName };

                bool success = dbHelper.AddCoaches(newCoach);

                if (success)
                {
                    LoadTrainers();

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

        private void DeleteCoachButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Coaches selectedCoaches = (Coaches)CoachesDataGrid.SelectedItem;

                if (selectedCoaches != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данные?", "Подтверждение удаления", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        dbHelper.DeleteCoaches(selectedCoaches.Id);
                        MessageBox.Show("Данные успешно удалено");

                        LoadTrainers();
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

        private void CloseCoachButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
