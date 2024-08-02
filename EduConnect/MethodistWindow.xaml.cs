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
    /// Логика взаимодействия для MethodistWindow.xaml
    /// </summary>
    public partial class MethodistWindow : MetroWindow
    {
        DatabaseHelper databaseHelper;
        public List<Normative> normatives { get; set; }
        public List<Events> Event { get; set; }
        public MethodistWindow(string username, string role)
        {
            InitializeComponent();
            databaseHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");
            RoleTextBlock.Text = $"Роль: {role}";
            UsernameTextBlock.Text = $"Имя пользователя: {username}";

            LoadNormsData();
            LoadEventsData();
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

        private void AddNormativeButton_Click(object sender, RoutedEventArgs e)
        {
            AddNormativeMethodistWindow addNormativeMethodistWindow = new AddNormativeMethodistWindow(null, this);
            addNormativeMethodistWindow.Show();
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
                AddNormativeMethodistWindow addNormsWindow = new AddNormativeMethodistWindow(selectedNorms, this);
                addNormsWindow.ShowDialog();
            }
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

        private void AddEventsButton_Click(object sender, RoutedEventArgs e)
        {
            AddEventsMethodistWindow eventsMethodistWindow = new AddEventsMethodistWindow(null, this);
            eventsMethodistWindow.ShowDialog();
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

        private void EventsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Events selectedEvent = (Events)EventsDataGrid.SelectedItem;

            if (selectedEvent != null)
            {
                AddEventsMethodistWindow addEventsWindow = new AddEventsMethodistWindow(selectedEvent, this);
                addEventsWindow.ShowDialog();
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
