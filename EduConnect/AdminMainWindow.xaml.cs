using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.IO;
using OfficeOpenXml;

namespace EduConnect
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : MetroWindow
    {
        DatabaseHelper databaseHelper;
        public List<Student> Students { get; set; }
        public List<Competition> Competitions { get; set; }
        public List<History> History { get; set; }
        public List<Normative> normatives { get; set; }
        public List<Events> Event { get; set; }

        public AdminMainWindow(string username, string role)
        {
            InitializeComponent();
            databaseHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");
            RoleTextBlock.Text = $"Роль: {role}";
            UsernameTextBlock.Text = $"Имя пользователя: {username}";

            LoadStudentsData();
            LoadCompetitionsData();
            LoadHistoryData();
            LoadNormsData();
            LoadEventsData();
        }

        public void LoadStudentsData()
        {
            Students = databaseHelper.GetStudents();
            DataContext = this;
            StudentsDataGrid.ItemsSource = Students;
        }

        public void LoadCompetitionsData()
        {
            Competitions = databaseHelper.ReadCompetitions();
            CompetitionsDataGrid.ItemsSource = Competitions;
        }

        public void LoadHistoryData()
        {
            History = databaseHelper.GetHistory();
            HistoryDataGrid.ItemsSource = History;
        }

        public void LoadNormsData()
        {
            normatives = databaseHelper.GetNorms();
            NormativesDataGrid.ItemsSource = normatives;
        }

        public void LoadEventsData()
        {
            Event = databaseHelper.GetEvents();
            EventsDataGrid.ItemsSource = Event;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void RegisterUserButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterUserWindow registerUserWindow = new RegisterUserWindow();
            registerUserWindow.ShowDialog();
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            AddStudentAdminWindow addStudentAdminWindow = new AddStudentAdminWindow(null, this);
            addStudentAdminWindow.Show();
        }

        private void ExportExcelButton_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel(Students);
        }

        private void ExportToExcel(List<Student> students)
        {
            if (students == null || students.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта в Excel.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Создание нового пакета Excel
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    // Добавление нового листа
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Учащиеся");

                    // Заголовки столбцов
                    worksheet.Cells[1, 1].Value = "Фамилия";
                    worksheet.Cells[1, 2].Value = "Имя";
                    worksheet.Cells[1, 3].Value = "Отчество";
                    worksheet.Cells[1, 4].Value = "Дата рождения";
                    worksheet.Cells[1, 5].Value = "Школа";
                    worksheet.Cells[1, 6].Value = "Класс";
                    worksheet.Cells[1, 7].Value = "Вид спорта";
                    worksheet.Cells[1, 8].Value = "ФИО Тренера";
                    worksheet.Cells[1, 9].Value = "Разряд";
                    worksheet.Cells[1, 10].Value = "Номер приказа(Разряда)";
                    worksheet.Cells[1, 11].Value = "Дата зачисления";
                    worksheet.Cells[1, 12].Value = "Группа";
                    worksheet.Cells[1, 13].Value = "№ Приказа зачисления";
                    worksheet.Cells[1, 14].Value = "№ Приказа отчисления";
                    worksheet.Cells[1, 15].Value = "Форма обучения";
                    worksheet.Cells[1, 16].Value = "ОРП или СП";
                    worksheet.Cells[1, 17].Value = "ФИО родителей";
                    worksheet.Cells[1, 18].Value = "Номер телефона";
                    worksheet.Cells[1, 19].Value = "Адрес";
                    worksheet.Cells[1, 20].Value = "Место работы родителей";
                    worksheet.Cells[1, 21].Value = "Должность родителей";
                    worksheet.Cells[1, 22].Value = "Свидетельство о рождении/паспорт";
                    worksheet.Cells[1, 23].Value = "Дата выдачи";
                    worksheet.Cells[1, 24].Value = "Кем выдан";
                    worksheet.Cells[1, 25].Value = "СНИЛС";
                    worksheet.Cells[1, 26].Value = "ИНН";

                    // Заполнение данными
                    for (int i = 0; i < students.Count; i++)
                    {
                        Student student = students[i];
                        worksheet.Cells[i + 2, 1].Value = student.Surname;
                        worksheet.Cells[i + 2, 2].Value = student.Name;
                        worksheet.Cells[i + 2, 3].Value = student.Patronymic;
                        worksheet.Cells[i + 2, 4].Value = student.BirthDate.ToString("dd-MM-yyyy");
                        worksheet.Cells[i + 2, 5].Value = student.School;
                        worksheet.Cells[i + 2, 6].Value = student.Class;
                        worksheet.Cells[i + 2, 7].Value = student.Sport;
                        worksheet.Cells[i + 2, 8].Value = student.TrainersName;
                        worksheet.Cells[i + 2, 9].Value = student.Rank;
                        worksheet.Cells[i + 2, 10].Value = student.OrderNumber;
                        worksheet.Cells[i + 2, 11].Value = student.EnrollmentGroup;
                        worksheet.Cells[i + 2, 12].Value = student.DateOfEnrollment.ToString("dd-MM-yyyy");
                        worksheet.Cells[i + 2, 13].Value = student.OrderEnrollment;
                        worksheet.Cells[i + 2, 14].Value = student.OrderDismissal;
                        worksheet.Cells[i + 2, 15].Value = student.PaymentType;
                        worksheet.Cells[i + 2, 16].Value = student.ORP_or_SP;
                        worksheet.Cells[i + 2, 17].Value = student.ParentsFullName;
                        worksheet.Cells[i + 2, 18].Value = student.PhoneNumber;
                        worksheet.Cells[i + 2, 19].Value = student.Address;
                        worksheet.Cells[i + 2, 20].Value = student.ParentsWorkPlace;
                        worksheet.Cells[i + 2, 21].Value = student.ParentsPosition;
                        worksheet.Cells[i + 2, 22].Value = student.BirthCertificate;
                        worksheet.Cells[i + 2, 23].Value = student.DateOfIssue.ToString("dd-MM-yyyy");
                        worksheet.Cells[i + 2, 24].Value = student.IssuedBy;
                        worksheet.Cells[i + 2, 25].Value = student.SNILS;
                        worksheet.Cells[i + 2, 26].Value = student.INN;
                    }

                    // Установка формата даты
                    using (ExcelRange col = worksheet.Cells[2, 4, students.Count + 1, 4])
                    {
                        col.Style.Numberformat.Format = "yyyy-MM-dd";
                    }

                    // Автоподгонка ширины столбцов
                    worksheet.Cells.AutoFitColumns();

                    // Сохранение файла Excel
                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                    saveFileDialog.FileName = "Учащиеся";
                    saveFileDialog.DefaultExt = ".xlsx";
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        excelPackage.SaveAs(excelFile);
                        MessageBox.Show("Данные успешно экспортированы в Excel!", "Экспорт в Excel", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте данных в Excel: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = SearchTextBox.Text;

            List<Student> searchResult = databaseHelper.SearchStudents(searchQuery);

            StudentsDataGrid.ItemsSource = searchResult;
        }

        private void StudentsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Student selectedStudent = (Student)StudentsDataGrid.SelectedItem;

            if (selectedStudent != null)
            {
                AddStudentAdminWindow addStudentAdminWindow = new AddStudentAdminWindow(selectedStudent, this);
                addStudentAdminWindow.ShowDialog();
            }
        }

        private void DeleteStudentButton_Click(object sender, RoutedEventArgs e)
        {
            Student selectedStudent = (Student)StudentsDataGrid.SelectedItem;

            if (selectedStudent != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого учащегося?", "Подтверждение удаления", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    databaseHelper.DeleteStudent(selectedStudent.Id);
                    MessageBox.Show("Учащийся успешно удален из базы данных.");

                    LoadStudentsData();
                }
            }
            else
            {
                MessageBox.Show("Выберите учащиегося для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SportComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SportComboBox == null || DischargesComboBox == null)
            {
                return;
            }

            string selectedSport = (SportComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string selectedRank = (DischargesComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            List<Student> filteredStudents;

            if (selectedSport == null || selectedRank == null)
            {
                return;
            }

            if (selectedSport == "Все" && selectedRank == "Все")
            {
                LoadStudentsData();
                return;
            }

            if (selectedSport == "Все")
            {
                filteredStudents = databaseHelper.FilterStudentsByRank(selectedRank);
            }
            else if (selectedRank == "Все")
            {
                filteredStudents = databaseHelper.FilterStudentsBySport(selectedSport);
            }
            else
            {
                // Применяем сначала фильтрацию по виду спорта, затем по разряду
                List<Student> sportFilteredStudents = databaseHelper.FilterStudentsBySport(selectedSport);
                filteredStudents = sportFilteredStudents.Where(s => s.Rank == selectedRank).ToList();
            }

            StudentsDataGrid.ItemsSource = filteredStudents;
        }

        private void DischargesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DischargesComboBox == null)
            {
                return;
            }

            string selectedRank = (DischargesComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            List<Student> filteredStudents;

            if (selectedRank == null)
            {
                return;
            }

            if (selectedRank == "Все")
            {
                LoadStudentsData();
                return;
            }

            filteredStudents = databaseHelper.FilterStudentsByRank(selectedRank);

            StudentsDataGrid.ItemsSource = filteredStudents;
        }

        private void CreateCompetition_Click(object sender, RoutedEventArgs e)
        {
            AddCompetitionsAdminWindow addCompetitionsAdminWindow = new AddCompetitionsAdminWindow(this, null);
            addCompetitionsAdminWindow.Show();
        }

        private void DeleteCompetition_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем выбранное соревнование
                Competition selectedCompetition = (Competition)CompetitionsDataGrid.SelectedItem;

                if (selectedCompetition != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данные о соревнованиях?", "Подтверждение удаления", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Удаляем соревнование из базы данных
                        databaseHelper.DeleteCompetition(selectedCompetition.Id);
                        MessageBox.Show("Данные о соревнованиях успешно удалено");

                        // Обновляем данные в списке соревнований
                        LoadCompetitionsData();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите данные о соревнованиях для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchCompetitionsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchCompetitionsTextBox.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadCompetitionsData();
            }
            else
            {
                List<Competition> searchedCompetitions = databaseHelper.SearchCompetitions(searchText);
                CompetitionsDataGrid.ItemsSource = searchedCompetitions;
            }
        }

        private void CompetitionsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Competition selectedCompetition = (Competition)CompetitionsDataGrid.SelectedItem;

            if (selectedCompetition != null)
            {
                AddCompetitionsAdminWindow detailsWindow = new AddCompetitionsAdminWindow(this,selectedCompetition);
                detailsWindow.Show();
            }
        }

        private void ExportExcelCompetition_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание нового пакета Excel
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    // Добавление нового листа
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Соревнования");

                    // Заголовки столбцов
                    worksheet.Cells[1, 1].Value = "ID";
                    worksheet.Cells[1, 2].Value = "Название";
                    worksheet.Cells[1, 3].Value = "Тип спорта";
                    worksheet.Cells[1, 4].Value = "Дата события";
                    worksheet.Cells[1, 5].Value = "Количество участников";
                    worksheet.Cells[1, 6].Value = "Результаты";

                    // Заполнение данными
                    List<Competition> competitions = (List<Competition>)CompetitionsDataGrid.ItemsSource;
                    for (int i = 0; i < competitions.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = competitions[i].Id;
                        worksheet.Cells[i + 2, 2].Value = competitions[i].Name;
                        worksheet.Cells[i + 2, 3].Value = competitions[i].SportType;
                        worksheet.Cells[i + 2, 4].Value = competitions[i].EventDate;
                        worksheet.Cells[i + 2, 5].Value = competitions[i].ParticipantsCount;
                        worksheet.Cells[i + 2, 6].Value = competitions[i].Results;
                    }

                    // Установка формата даты
                    using (ExcelRange col = worksheet.Cells[2, 4, competitions.Count + 1, 4])
                    {
                        col.Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                    }

                    // Автоподгонка ширины столбцов
                    worksheet.Cells.AutoFitColumns();

                    // Сохранение файла Excel
                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                    saveFileDialog.FileName = "Соревнования";
                    saveFileDialog.DefaultExt = ".xlsx";
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        excelPackage.SaveAs(excelFile);
                        MessageBox.Show("Данные успешно экспортированы в Excel!", "Экспорт в Excel", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте данных в Excel: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportExcelNormatives_Click(object sender, RoutedEventArgs e)
        {
            ExportExcelNorms(normatives);
        }

        private void ExportExcelNorms(List<Normative> normatives)
        {
            if (normatives == null || normatives.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта в Excel.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Создание нового пакета Excel
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    // Добавление нового листа
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Нормативы");

                    // Заголовки столбцов
                    worksheet.Cells[1, 1].Value = "ID";
                    worksheet.Cells[1, 2].Value = "ФИО";
                    worksheet.Cells[1, 3].Value = "Вид спорта";
                    worksheet.Cells[1, 4].Value = "Год сдачи";
                    worksheet.Cells[1, 5].Value = "ФИО Тренера";
                    worksheet.Cells[1, 6].Value = "Нормативы на начало периода";
                    worksheet.Cells[1, 7].Value = "Нормативы на конец периода";
                    worksheet.Cells[1, 8].Value = "Процент сдачи";
                    worksheet.Cells[1, 9].Value = "Комментарий";

                    // Заполнение данными
                    for (int i = 0; i < normatives.Count; i++)
                    {
                        Normative normative = normatives[i];
                        worksheet.Cells[i + 2, 1].Value = normative.Id;
                        worksheet.Cells[i + 2, 2].Value = normative.FullName;
                        worksheet.Cells[i + 2, 3].Value = normative.SportName;
                        worksheet.Cells[i + 2, 4].Value = normative.YearComplete;
                        worksheet.Cells[i + 2, 5].Value = normative.TrainerName;
                        worksheet.Cells[i + 2, 6].Value = normative.NormsAtTheBeginning;
                        worksheet.Cells[i + 2, 7].Value = normative.NormsAtTheEndOf;
                        worksheet.Cells[i + 2, 8].Value = normative.SurrenderRate;
                        worksheet.Cells[i + 2, 9].Value = normative.Comment;
                    }

                    // Автоподгонка ширины столбцов
                    worksheet.Cells.AutoFitColumns();

                    // Сохранение файла Excel
                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                    saveFileDialog.FileName = "Нормативы";
                    saveFileDialog.DefaultExt = ".xlsx";
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        excelPackage.SaveAs(excelFile);
                        MessageBox.Show("Данные успешно экспортированы в Excel!", "Экспорт в Excel", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте данных в Excel: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteNormativeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем выбранное соревнование
                Normative selectedNorms = (Normative)NormativesDataGrid.SelectedItem;

                if (selectedNorms != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данные?", "Подтверждение удаления", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Удаляем нормативы из базы данных
                        databaseHelper.DeleteNorms(selectedNorms.Id);
                        MessageBox.Show("Данные успешно удалено");

                        // Обновляем данные в списке нормативов
                        LoadNormsData();
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

        private void AddNormativeButton_Click(object sender, RoutedEventArgs e)
        {
            AddNormsWindow addNormsWindow = new AddNormsWindow(null, this);
            addNormsWindow.Show();
        }

        private void SearchNormativesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchNormativesTextBox.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadNormsData();
            }
            else
            {
                List<Normative> searchedNorms = databaseHelper.SearchNorms(searchText);
                NormativesDataGrid.ItemsSource = searchedNorms;
            }
        }

        private void NormativesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Normative selectedNorms = (Normative)NormativesDataGrid.SelectedItem;

            if (selectedNorms != null)
            {
                AddNormsWindow addNormsWindow = new AddNormsWindow(selectedNorms, this);
                addNormsWindow.ShowDialog();
            }
        }

        private void DeleteHistory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем выбранное соревнование
                History selectedHistory = (History)HistoryDataGrid.SelectedItem;

                if (selectedHistory != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данные?", "Подтверждение удаления", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Удаляем соревнование из базы данных
                        databaseHelper.DeleteHistory(selectedHistory.ID);
                        MessageBox.Show("Данные успешно удалено");

                        // Обновляем данные в списке соревнований
                        LoadHistoryData();
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

        private void SearchHistory_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchHistory.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadHistoryData();
            }
            else
            {
                List<History> searchedHistory = databaseHelper.SearchHistory(searchText);
                HistoryDataGrid.ItemsSource = searchedHistory;
            }
        }

        private void AddHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            AddHistoryWindow addHistoryWindow = new AddHistoryWindow(null, this);
            addHistoryWindow.Show();
        }

        private void ExportExcelHistory_Click(object sender, RoutedEventArgs e)
        {
            ExportExcelHistories(History);
        }

        private void ExportExcelHistories(List<History> history)
        {
            if (history == null || history.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта в Excel.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Создание нового пакета Excel
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    // Добавление нового листа
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("История учащихся");

                    // Заголовки столбцов
                    worksheet.Cells[1, 1].Value = "ФИО";
                    worksheet.Cells[1, 2].Value = "Разряды";
                    worksheet.Cells[1, 3].Value = "Соревнования";
                    worksheet.Cells[1, 4].Value = "Соревнования";
                    worksheet.Cells[1, 5].Value = "Год";

                    // Заполнение данными
                    for (int i = 0; i < history.Count; i++)
                    {
                        History histories = history[i];
                        worksheet.Cells[i + 2, 1].Value = histories.FullName;
                        worksheet.Cells[i + 2, 2].Value = histories.Rank;
                        worksheet.Cells[i + 2, 3].Value = histories.Competitions;
                        worksheet.Cells[i + 2, 4].Value = histories.Norms;
                        worksheet.Cells[i + 2, 5].Value = histories.Year;
                    }

                    // Автоподгонка ширины столбцов
                    worksheet.Cells.AutoFitColumns();

                    // Сохранение файла Excel
                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                    saveFileDialog.FileName = "История учащихся";
                    saveFileDialog.DefaultExt = ".xlsx";
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        excelPackage.SaveAs(excelFile);
                        MessageBox.Show("Данные успешно экспортированы в Excel!", "Экспорт в Excel", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте данных в Excel: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HistoryDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            History selectedHistory = (History)HistoryDataGrid.SelectedItem;

            if (selectedHistory != null)
            {
                AddHistoryWindow addHistoryWindow = new AddHistoryWindow(selectedHistory, this);
                addHistoryWindow.ShowDialog();
            }
        }

        private void AddCoachButton_Click(object sender, RoutedEventArgs e)
        {
            AddCoachWindow addCoachWindow = new AddCoachWindow();
            addCoachWindow.ShowDialog();
        }

        private void AddSportButton_Click(object sender, RoutedEventArgs e)
        {
            AddSportWindow addSportWindow = new AddSportWindow();
            addSportWindow.ShowDialog();
        }

        private void ExportExcelEventsButton_Click(object sender, RoutedEventArgs e)
        {
            ExportExcelEvents(Event);
        }

        private void ExportExcelEvents(List<Events> events)
        {
            if (events == null || events.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта в Excel.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Создание нового пакета Excel
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    // Добавление нового листа
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Мероприятия");

                    // Заголовки столбцов
                    worksheet.Cells[1, 1].Value = "Название";
                    worksheet.Cells[1, 2].Value = "Уровень";
                    worksheet.Cells[1, 3].Value = "Дата проведения";
                    worksheet.Cells[1, 4].Value = "Кол-во";
                    worksheet.Cells[1, 5].Value = "Результат";
                    worksheet.Cells[1, 6].Value = "Год";

                    // Заполнение данными
                    for (int i = 0; i < events.Count; i++)
                    {
                        Events events1 = events[i];
                        worksheet.Cells[i + 2, 1].Value = events1.Title;
                        worksheet.Cells[i + 2, 2].Value = events1.Level;
                        worksheet.Cells[i + 2, 3].Value = events1.Date;
                        worksheet.Cells[i + 2, 4].Value = events1.Amount;
                        worksheet.Cells[i + 2, 5].Value = events1.Result;
                        worksheet.Cells[i + 2, 6].Value = events1.Year;
                    }

                    // Автоподгонка ширины столбцов
                    worksheet.Cells.AutoFitColumns();

                    // Сохранение файла Excel
                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                    saveFileDialog.FileName = "Мероприятия";
                    saveFileDialog.DefaultExt = ".xlsx";
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        excelPackage.SaveAs(excelFile);
                        MessageBox.Show("Данные успешно экспортированы в Excel!", "Экспорт в Excel", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте данных в Excel: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем выбранное соревнование
                Events selectedEvent = (Events)EventsDataGrid.SelectedItem;

                if (selectedEvent != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данные о мероприятие?", "Подтверждение удаления", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Удаляем соревнование из базы данных
                        databaseHelper.DeleteEvents(selectedEvent.Id);
                        MessageBox.Show("Данные о мероприятие успешно удалены");

                        // Обновляем данные в списке соревнований
                        LoadEventsData();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите данные о мероприятие для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddEventsButton_Click(object sender, RoutedEventArgs e)
        {
            AddEventsWindow addEventsWindow = new AddEventsWindow(null, this);
            addEventsWindow.Show();
        }

        private void SearchEventsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchEventsTextBox.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadEventsData();
            }
            else
            {
                List<Events> searchedEvents = databaseHelper.SearchEvents(searchText);
                EventsDataGrid.ItemsSource = searchedEvents;
            }
        }

        private void EventsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Events selectedEvent = (Events)EventsDataGrid.SelectedItem;

            if (selectedEvent != null)
            {
                AddEventsWindow addEventsWindow = new AddEventsWindow(selectedEvent, this);
                addEventsWindow.ShowDialog();
            }
        }

        private void AddGroupButton_Click(object sender, RoutedEventArgs e)
        {
            AddGroupWindow addGroupWindow = new AddGroupWindow();
            addGroupWindow.Show();
        }

        private void UpdatePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePassword = new ChangePasswordWindow();
            changePassword.Show();
        }
    }
}
