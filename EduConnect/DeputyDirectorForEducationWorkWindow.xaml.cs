using MahApps.Metro.Controls;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EduConnect
{
    /// <summary>
    /// Логика взаимодействия для DeputyDirectorForEducationWorkWindow.xaml
    /// </summary>
    public partial class DeputyDirectorForEducationWorkWindow : MetroWindow
    {
        DatabaseHelper databaseHelper;
        public List<Student> Students { get; set; }
        public List<History> History { get; set; }
        public DeputyDirectorForEducationWorkWindow(string username, string role)
        {
            InitializeComponent();
            databaseHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");
            RoleTextBlock.Text = $"Роль: {role}";
            UsernameTextBlock.Text = $"Имя пользователя: {username}";
        }

        public void LoadStudentsData()
        {
            Students = databaseHelper.GetStudents();
            DataContext = this;
            StudentsDataGrid.ItemsSource = Students;
        }

        public void LoadHistoryData()
        {
            History = databaseHelper.GetHistory();
            HistoryDataGrid.ItemsSource = History;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            AddStudentDeputyDirectorForEducationWindow addStudentDeputyDirectorForEducation = new AddStudentDeputyDirectorForEducationWindow(null, this);
            addStudentDeputyDirectorForEducation.Show();
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
                AddStudentDeputyDirectorForEducationWindow addStudentDeputyDirectorForEducation = new AddStudentDeputyDirectorForEducationWindow(selectedStudent, this);
                addStudentDeputyDirectorForEducation.ShowDialog();
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
            AddHistoryDeputyDirectorForEducationWindow addHistoryDeputyDirectorForEducation = new AddHistoryDeputyDirectorForEducationWindow(null, this);
            addHistoryDeputyDirectorForEducation.Show();
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
                AddHistoryDeputyDirectorForEducationWindow addHistoryDeputyDirectorForEducation = new AddHistoryDeputyDirectorForEducationWindow(selectedHistory, this);
                addHistoryDeputyDirectorForEducation.ShowDialog();
            }
        }
    }
}
