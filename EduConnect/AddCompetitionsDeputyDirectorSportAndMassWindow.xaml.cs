using MahApps.Metro.Controls;
using System;
using System.Windows;

namespace EduConnect
{
    /// <summary>
    /// Логика взаимодействия для AddCompetitionsDeputyDirectorSportAndMassWindow.xaml
    /// </summary>
    public partial class AddCompetitionsDeputyDirectorSportAndMassWindow : MetroWindow
    {
        DatabaseHelper databaseHelper;
        private Competition EditedCompetition;
        private DeputyDirectorForSportsAndMassSportsWorkWindow deputyDirectorForSports;
        public AddCompetitionsDeputyDirectorSportAndMassWindow(DeputyDirectorForSportsAndMassSportsWorkWindow deputyDirectorForSports, Competition EditedCompetition)
        {
            InitializeComponent();

            this.deputyDirectorForSports = deputyDirectorForSports;
            this.EditedCompetition = EditedCompetition;

            databaseHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");

            if (EditedCompetition != null)
            {
                NameTextBox.Text = EditedCompetition.Name;
                SportTypeTextBox.Text = EditedCompetition.SportType;
                EventDateTimePicker.SelectedDate = EditedCompetition.EventDate;
                ParticipantsCountTextBox.Text = EditedCompetition.ParticipantsCount.ToString();
                ResultsTextBox.Text = EditedCompetition.Results;
                YearTextBox.Text = Convert.ToString(EditedCompetition.Year);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Competition newCompetition = CreateCompetitionObject();

                if (EditedCompetition == null)
                {
                    databaseHelper.AddCompetition(newCompetition);
                    MessageBox.Show("Данные о соревнованиях успешно добавлены");
                }
                else
                {
                    newCompetition.Id = EditedCompetition.Id;
                    databaseHelper.UpdateCompetition(newCompetition);
                    MessageBox.Show("Данные о соревнованиях успешно обновлены");
                }

                deputyDirectorForSports.LoadCompetitionsData();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Competition CreateCompetitionObject()
        {
            string name = NameTextBox.Text;
            string sportType = SportTypeTextBox.Text;
            DateTime eventDate = EventDateTimePicker.SelectedDate ?? DateTime.Now;
            int participantsCount = int.Parse(ParticipantsCountTextBox.Text);
            string results = ResultsTextBox.Text;
            int Year = Convert.ToInt32(YearTextBox.Text);

            Competition competition = new Competition
            {
                Name = name,
                SportType = sportType,
                EventDate = eventDate,
                ParticipantsCount = participantsCount,
                Results = results,
                Year = Year
            };

            return competition;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
