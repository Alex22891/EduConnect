using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EduConnect
{
    /// <summary>
    /// Логика взаимодействия для AddEventsMethodistWindow.xaml
    /// </summary>
    public partial class AddEventsMethodistWindow : MetroWindow
    {
        private Events EditedEvent;
        private MethodistWindow methodistWindow;
        public DatabaseHelper dbHelper;
        public AddEventsMethodistWindow(Events EditedEvent, MethodistWindow methodistWindow)
        {
            InitializeComponent();
            this.methodistWindow = methodistWindow;
            this.EditedEvent = EditedEvent;

            dbHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");

            if (EditedEvent != null)
            {
                TitleTextBox.Text = EditedEvent.Title;
                LevelCombobox.Text = EditedEvent.Level;
                DatePicker.SelectedDate = EditedEvent.Date;
                AmountTextBox.Text = EditedEvent.Amount;
                ResultTextBox.Text = EditedEvent.Result;
                YearTextBox.Text = Convert.ToString(EditedEvent.Year);
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Events newEvent = CreateStudentObject();

                if (EditedEvent == null)
                {
                    dbHelper.AddEvents(newEvent);
                    MessageBox.Show("Мероприятие успешно добавлено");
                }
                else
                {
                    newEvent.Id = EditedEvent.Id;
                    dbHelper.UpdateEvents(newEvent);
                    MessageBox.Show("Данные успешно обновлены");
                }

                methodistWindow.LoadEventsData();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Events CreateStudentObject()
        {
            // Получение значений полей из элементов управления

            string Title = TitleTextBox.Text;
            string Level = LevelCombobox.Text;
            DateTime Date = DatePicker.SelectedDate ?? DateTime.Now;
            string Amount = AmountTextBox.Text;
            string Result = ResultTextBox.Text;
            int Year = Convert.ToInt32(YearTextBox.Text);

            // Создание объекта Student с полученными значениями

            Events events = new Events
            {
                Title = Title,
                Level = Level,
                Date = Date,
                Amount = Amount,
                Result = Result,
                Year = Year
            };

            return events;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
