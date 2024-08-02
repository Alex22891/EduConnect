using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace EduConnect
{
    public partial class AddStudentAdminWindow : MetroWindow
    {
        private Student EditedStudent;
        private AdminMainWindow adminMainWindow;
        public DatabaseHelper dbHelper;
        public List<Sports> Sport { get; set; }
        public List<Coaches> Coaches { get; set; }
        public List<Student> students { get; set; }
        public List<Groups> groups { get; set; }

        public AddStudentAdminWindow(Student EditedStudent, AdminMainWindow adminMainWindow)
        {
            InitializeComponent();
            this.adminMainWindow = adminMainWindow;

            this.EditedStudent = EditedStudent;
            dbHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");

            LoadSports();
            LoadTrainers();
            LoadGroups();

            if (EditedStudent != null)
            {
                LastNameTextBox.Text = EditedStudent.Surname;
                FirstNameTextBox.Text = EditedStudent.Name;
                MiddleNameTextBox.Text = EditedStudent.Patronymic;
                BirthDatePicker.SelectedDate = EditedStudent.BirthDate;
                SchoolTextBox.Text = EditedStudent.School;
                ClassTextBox.Text = EditedStudent.Class;
                SportTextBox.Text = EditedStudent.Sport;
                TrainerFullNameTextBox.Text = EditedStudent.TrainersName;
                EnrollmentGroupComboBox.Text = EditedStudent.EnrollmentGroup;
                RankTextBox.Text = EditedStudent.Rank;
                OrderNumberTextBox.Text = EditedStudent.OrderNumber;
                EnrollmentDatePicker.SelectedDate = EditedStudent.DateOfEnrollment;
                EnrollmentOrderTextBox.Text = EditedStudent.OrderEnrollment;
                DismissalOrderTextBox.Text = EditedStudent.OrderDismissal;
                PaymentTypeComboBox.Text = EditedStudent.PaymentType;
                ORPSPTextBox.Text = EditedStudent.ORP_or_SP;
                ParentsFullNameTextBox.Text = EditedStudent.ParentsFullName;
                PhoneNumberTextBox.Text = EditedStudent.PhoneNumber;
                AddressTextBox.Text = EditedStudent.Address;
                ParentsWorkPlaceTextBox.Text = EditedStudent.ParentsWorkPlace;
                ParentsPositionTextBox.Text = EditedStudent.ParentsPosition;
                BirthCertificateTextBox.Text = EditedStudent.BirthCertificate;
                DocumentIssuedDatePicker.SelectedDate = EditedStudent.DateOfIssue;
                IssuedByTextBox.Text = EditedStudent.IssuedBy;
                SNILSTextBox.Text = EditedStudent.SNILS;
                INNTextBox.Text = EditedStudent.INN;
            }
        }

        private void LoadSports()
        {
            Sport = dbHelper.GetSports();
            SportTextBox.ItemsSource = Sport.Select(sport => sport.SportName);
        }

        private void LoadTrainers()
        {
            Coaches = dbHelper.GetTrainers();
            TrainerFullNameTextBox.ItemsSource = Coaches.Select(coach => coach.FullName);
        }

        public void LoadGroups()
        {
            groups = dbHelper.GetGroups();
            EnrollmentGroupComboBox.ItemsSource = groups.Select(group => group.Title);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Student newStudent = CreateStudentObject();

                if (EditedStudent == null)
                {
                    dbHelper.AddStudent(newStudent);
                    MessageBox.Show("Ученик успешно добавлен");
                }
                else
                {
                    newStudent.Id = EditedStudent.Id;
                    dbHelper.UpdateStudent(newStudent);
                    MessageBox.Show("Данные успешно обновлены");
                }

                adminMainWindow.LoadStudentsData();

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

        private Student CreateStudentObject()
        {
            // Получение значений полей из элементов управления

            string lastName = LastNameTextBox.Text;
            string firstName = FirstNameTextBox.Text;
            string middleName = MiddleNameTextBox.Text;
            DateTime birthDate = BirthDatePicker.SelectedDate ?? DateTime.Now;
            string school = SchoolTextBox.Text;
            string Class = ClassTextBox.Text;
            string sport = SportTextBox.Text;
            string trainerFullName = TrainerFullNameTextBox.Text;
            string enrollmentGroup = EnrollmentGroupComboBox.Text;
            string rank = RankTextBox.Text;
            string orderNumber = OrderNumberTextBox.Text;
            DateTime enrollmentDate = EnrollmentDatePicker.SelectedDate ?? DateTime.Now;
            string enrollmentOrder = EnrollmentOrderTextBox.Text;
            string dismissalOrder = DismissalOrderTextBox.Text;
            string paymentType = PaymentTypeComboBox.Text;
            string orpsp = ORPSPTextBox.Text;
            string parentsFullName = ParentsFullNameTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;
            string address = AddressTextBox.Text;
            string parentsWorkPlace = ParentsWorkPlaceTextBox.Text;
            string parentsPosition = ParentsPositionTextBox.Text;
            string birthCertificate = BirthCertificateTextBox.Text;
            DateTime documentIssuedDate = DocumentIssuedDatePicker.SelectedDate ?? DateTime.Now;
            string issuedBy = IssuedByTextBox.Text;
            string snils = SNILSTextBox.Text;
            string inn = INNTextBox.Text;

            // Создание объекта Student с полученными значениями

            Student student = new Student
            {
                Surname = lastName,
                Name = firstName,
                Patronymic = middleName,
                BirthDate = birthDate,
                School = school,
                Class = Class,
                Sport = sport,
                TrainersName = trainerFullName,
                EnrollmentGroup = enrollmentGroup,
                Rank = rank,
                OrderNumber = orderNumber,
                DateOfEnrollment = enrollmentDate,
                OrderEnrollment = enrollmentOrder,
                OrderDismissal = dismissalOrder,
                PaymentType = paymentType,
                ORP_or_SP = orpsp,
                ParentsFullName = parentsFullName,
                PhoneNumber = phoneNumber,
                Address = address,
                ParentsWorkPlace = parentsWorkPlace,
                ParentsPosition = parentsPosition,
                BirthCertificate = birthCertificate,
                DateOfIssue = documentIssuedDate,
                IssuedBy = issuedBy,
                SNILS = snils,
                INN = inn
            };

            return student;
        }
    }
}
