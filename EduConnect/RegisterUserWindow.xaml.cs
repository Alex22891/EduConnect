using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

namespace EduConnect
{
    /// <summary>
    /// Логика взаимодействия для RegisterUserWindow.xaml
    /// </summary>
    public partial class RegisterUserWindow : MetroWindow
    {
        private readonly DatabaseHelper databaseHelper;
        public RegisterUserWindow()
        {
            InitializeComponent();

            // Здесь вы указываете свои данные для подключения к базе данных
            databaseHelper = new DatabaseHelper("83.166.232.220", "MySQL-7832", "user", "64s1eQ,9n28TmgC33");
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || role == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            bool registrationSuccess = databaseHelper.RegisterUser(username, password, role);

            if (!registrationSuccess)
            {
                MessageBox.Show("Пользователь с таким именем уже существует!");
                return;
            }

            MessageBox.Show("Пользователь успешно зарегистрирован!");
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
