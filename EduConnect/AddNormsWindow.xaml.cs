using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace EduConnect
{
    /// <summary>
    /// Логика взаимодействия для AddNormsWindow.xaml
    /// </summary>
    public partial class AddNormsWindow : MetroWindow
    {
        private Normative EditedNorms;
        private AdminMainWindow adminMainWindow;
        public DatabaseHelper dbHelper;

        public List<Coaches> Coaches { get; set; }
        public List<Sports> Sport { get; set; }
        public AddNormsWindow(Normative EditedNorms, AdminMainWindow adminMainWindow)
        {
            InitializeComponent();
            this.EditedNorms = EditedNorms;
            this.adminMainWindow = adminMainWindow;

            dbHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");

            LoadTrainers();
            LoadSports();

            if (EditedNorms != null)
            {
                FullNameTextBox.Text = EditedNorms.FullName;
                SportNameComboBox.Text = EditedNorms.SportName;
                YearTextBox.Text = Convert.ToString(EditedNorms.YearComplete);
                TrainerNameComboBox.Text = EditedNorms.TrainerName;
                NormsAtTheBeginningTextBox.Text = EditedNorms.NormsAtTheBeginning;
                NormsAtTheEndOfTextBox.Text = EditedNorms.NormsAtTheEndOf;
                SurrenderRateTextBox.Text = EditedNorms.SurrenderRate;
                CommentTextBox.Text = EditedNorms.Comment;
            }
        }

        private void LoadTrainers()
        {
            Coaches = dbHelper.GetTrainers();
            TrainerNameComboBox.ItemsSource = Coaches.Select(coach => coach.FullName);
        }

        private void LoadSports()
        {
            Sport = dbHelper.GetSports();
            SportNameComboBox.ItemsSource = Sport.Select(sport => sport.SportName);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Normative newNorms = CreateStudentObject();

                if (EditedNorms == null)
                {
                    dbHelper.AddNorms(newNorms);
                    MessageBox.Show("Данные успешно добавлены");
                }
                else
                {
                    newNorms.Id = EditedNorms.Id;
                    dbHelper.UpdateNorms(newNorms);
                    MessageBox.Show("Данные успешно обновлены");
                }

                adminMainWindow.LoadNormsData();

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

        private Normative CreateStudentObject()
        {
            // Получение значений полей из элементов управления

            string FullName = FullNameTextBox.Text;
            string SportName = SportNameComboBox.Text;
            string TrainerName = TrainerNameComboBox.Text;
            string NormsAtTheBeginning = NormsAtTheBeginningTextBox.Text;
            string NormsAtTheEndOf = NormsAtTheEndOfTextBox.Text;
            int Year = Convert.ToInt32(YearTextBox.Text);
            string SurrenderRate = SurrenderRateTextBox.Text;
            string Comment = CommentTextBox.Text;

            // Создание объекта Student с полученными значениями

            Normative normative = new Normative
            {
                FullName = FullName,
                SportName = SportName,
                YearComplete = Year,
                TrainerName = TrainerName,
                NormsAtTheBeginning = NormsAtTheBeginning,
                NormsAtTheEndOf = NormsAtTheEndOf,
                SurrenderRate = SurrenderRate,
                Comment = Comment
            };

            return normative;
        }
    }
}
