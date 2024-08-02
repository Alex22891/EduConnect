using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
namespace EduConnect
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        private readonly DatabaseHelper databaseHelper;
        public LoginWindow()
        {
            InitializeComponent();

            databaseHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");

            //CheckLicense();
        }

        private async void CheckLicense()
        {
            LicenseChecker licenseChecker = new LicenseChecker();
            string username = "user1";

            var (licenseKeys, userLicenseKey) = await licenseChecker.GetLicenseDataFromGitHub(username);
            bool isValid = licenseChecker.CheckLicenseValidity(licenseKeys, userLicenseKey);

            if (!isValid)
            {
                MessageBox.Show("У вас неактивна лицензия. Обратитесь к разработчику.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (databaseHelper.ValidateUser(username, password))
            {
            }
            else
            {
                UsernameTextBox.Clear();
                PasswordBox.Clear();
                MessageBox.Show("Неправильное имя пользователя или пароль.");
            }
        }
    }
}
