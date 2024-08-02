using MahApps.Metro.Controls;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace EduConnect
{
    /// <summary>
    /// Логика взаимодействия для DeputyDirectorForSportsAndMassSportsWorkWindow.xaml
    /// </summary>
    public partial class DeputyDirectorForSportsAndMassSportsWorkWindow : MetroWindow
    {
        DatabaseHelper databaseHelper;
        public List<Competition> Competitions { get; set; }
        public List<Normative> normatives { get; set; }
        public DeputyDirectorForSportsAndMassSportsWorkWindow(string username, string role)
        {
            InitializeComponent();
            databaseHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");
            RoleTextBlock.Text = $"Роль: {role}";
            UsernameTextBlock.Text = $"Имя пользователя: {username}";

            LoadCompetitionsData();
            LoadNormsData();
        }

        public void LoadCompetitionsData()
        {
            Competitions = databaseHelper.ReadCompetitions();
            CompetitionsDataGrid.ItemsSource = Competitions;
        }

        public void LoadNormsData()
        {
            normatives = databaseHelper.GetNorms();
            NormativesDataGrid.ItemsSource = normatives;
        }

        private void CreateCompetition_Click(object sender, RoutedEventArgs e)
        {
            AddCompetitionsDeputyDirectorSportAndMassWindow directorSportAndMassWindow = new AddCompetitionsDeputyDirectorSportAndMassWindow(this, null);
            directorSportAndMassWindow.Show();
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

        private void AddNormativeButton_Click(object sender, RoutedEventArgs e)
        {
            AddNormsDeputyDirectorSportAndMassWindow directorSportAndMassWindow = new AddNormsDeputyDirectorSportAndMassWindow(null, this);
            directorSportAndMassWindow.Show();
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
                AddNormsDeputyDirectorSportAndMassWindow addNormsWindow = new AddNormsDeputyDirectorSportAndMassWindow(selectedNorms, this);
                addNormsWindow.ShowDialog();
            }
        }

        private void CompetitionsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Competition selectedCompetition = (Competition)CompetitionsDataGrid.SelectedItem;

            if (selectedCompetition != null)
            {
                AddCompetitionsDeputyDirectorSportAndMassWindow detailsWindow = new AddCompetitionsDeputyDirectorSportAndMassWindow(this, selectedCompetition);
                detailsWindow.Show();
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
