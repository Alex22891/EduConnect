using MahApps.Metro.Controls;
using System;
using System.Windows;

namespace EduConnect
{
    /// <summary>
    /// Логика взаимодействия для AddHistoryWindow.xaml
    /// </summary>
    public partial class AddHistoryWindow : MetroWindow
    {
        private History EditedHistory;
        private AdminMainWindow adminMainWindow;
        public DatabaseHelper dbHelper;
        public AddHistoryWindow(History EditedHistory, AdminMainWindow adminMainWindow)
        {
            InitializeComponent();
            this.EditedHistory = EditedHistory;
            this.adminMainWindow = adminMainWindow;

            dbHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");

            if (EditedHistory != null)
            {
                FullNameTextBox.Text = EditedHistory.FullName;
                RankTextBox.Text = EditedHistory.Rank;
                CompetitionsTextBox.Text = EditedHistory.Competitions;
                NormsTextBox.Text = EditedHistory.Norms;
                YearTextBox.Text = Convert.ToString(EditedHistory.Year);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                History newHistory = CreateStudentObject();

                if (EditedHistory == null)
                {
                    dbHelper.AddHistory(newHistory);
                    MessageBox.Show("Данные успешно добавлены");
                }
                else
                {
                    newHistory.ID = EditedHistory.ID;
                    dbHelper.UpdateHistory(newHistory);
                    MessageBox.Show("Данные успешно обновлены");
                }

                adminMainWindow.LoadHistoryData();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private History CreateStudentObject()
        {
            // Получение значений полей из элементов управления

            string FullName = FullNameTextBox.Text;
            string Rank = RankTextBox.Text;
            string Competitions = CompetitionsTextBox.Text;
            string Norms = NormsTextBox.Text;
            int Year = Convert.ToInt32(YearTextBox.Text);

            // Создание объекта Student с полученными значениями

            History history = new History
            {
                FullName = FullName,
                Rank = Rank,
                Competitions = Competitions,
                Norms = Norms,
                Year = Year
            };

            return history;
        }
    }
}
