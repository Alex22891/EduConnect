using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using MySql.Data.MySqlClient;

namespace EduConnect
{
    public class DatabaseHelper
    {
        private readonly MySqlConnection connection;

        public DatabaseHelper(string server, string database, string username, string password)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = server,
                Database = database,
                UserID = username,
                Password = password
            };

            connection = new MySqlConnection(builder.ConnectionString);
        }

        public bool ValidateUser(string username, string password)
        {
            string query = "SELECT * FROM Users WHERE BINARY Username = @Username;";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            string role = dataTable.Rows[0]["Role"].ToString();

                            string hashedInputPassword = GetHashedPassword(password);
                            string hashedPasswordFromDB = dataTable.Rows[0]["Password"].ToString();

                            if (hashedPasswordFromDB == hashedInputPassword)
                            {
                                if (role == "Администратор")
                                {
                                    AdminMainWindow adminWindow = new AdminMainWindow(username, role);
                                    adminWindow.Show();
                                    if (Application.Current.MainWindow != null)
                                    {
                                        Application.Current.MainWindow.Close();
                                    }
                                    return true;
                                }
                                else if (role == "Заместитель директора по УВР")
                                {
                                    DeputyDirectorForEducationWorkWindow deputyDirectorForEducationWorkWindow = new DeputyDirectorForEducationWorkWindow(username, role);
                                    deputyDirectorForEducationWorkWindow.Show();
                                    if (Application.Current.MainWindow != null)
                                    {
                                        Application.Current.MainWindow.Close();
                                    }
                                    return true;
                                }
                                else if (role == "Заместитель директора по СМР")
                                {
                                    DeputyDirectorForSportsAndMassSportsWorkWindow deputyDirectorForSportsAndMassSportsWork = new DeputyDirectorForSportsAndMassSportsWorkWindow(username, role);
                                    deputyDirectorForSportsAndMassSportsWork.Show();
                                    if (Application.Current.MainWindow != null)
                                    {
                                        Application.Current.MainWindow.Close();
                                    }
                                    return true;
                                }
                                else if (role == "Методист")
                                {
                                    MethodistWindow methodistWindow = new MethodistWindow(username, role);
                                    methodistWindow.Show();
                                    if (Application.Current.MainWindow != null)
                                    {
                                        Application.Current.MainWindow.Close();
                                    }
                                    return true;
                                }
                                else if (role == "Тренер-преподаватель")
                                {
                                    MainWindow mainWindow = new MainWindow(username, role);
                                    mainWindow.Show();
                                    if (Application.Current.MainWindow != null)
                                    {
                                        Application.Current.MainWindow.Close();
                                    }
                                    return true;
                                }
                            }
                        }

                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                    return false;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        private string GetHashedPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public bool RegisterUser(string username, string password, string role)
        {
            try
            {
                // Проверяем, существует ли пользователь с указанным именем
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username;";
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@username", username);
                    connection.Open();
                    int userCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (userCount > 0)
                    {
                        Console.WriteLine("Пользователь с таким именем уже существует.");
                        return false;
                    }
                }

                string hashedPassword = GetHashedPassword(password);

                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@username, @password, @role);";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@role", role);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при регистрации пользователя: {ex.Message}");
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool ChangePassword(string username, string newPassword)
        {
            try
            {
                string hashedPassword = GetHashedPassword(newPassword);

                string query = "UPDATE Users SET Password = @Password WHERE BINARY Username = @Username;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при изменении пароля: {ex.Message}");
                return false;
            }
        }

        public bool AddStudent(Student student)
        {
            try
            {
                string query = "INSERT INTO Students (Surname, Name, Patronymic, BirthDate, School, Class, Sport, TrainersName, EnrollmentGroup, `Rank`, OrderNumber, " +
                               "DateOfEnrollment, OrderEnrollment, OrderDismissal, PaymentType, ORP_or_SP, ParentsFullName, PhoneNumber, Address, " +
                               "ParentsWorkPlace, ParentsPosition, BirthCertificate, DateOfIssue, IssuedBy, SNILS, INN) " +
                               "VALUES (@Surname, @Name, @Patronymic, @BirthDate, @School, @Class, @Sport, @TrainersName, @EnrollmentGroup, @Rank, " +
                               "@OrderNumber, @DateOfEnrollment, @OrderEnrollment, @OrderDismissal, @PaymentType, @ORP_or_SP, @ParentsFullName, " +
                               "@PhoneNumber, @Address, @ParentsWorkPlace, @ParentsPosition, @BirthCertificate, @DateOfIssue, @IssuedBy, @SNILS, @INN);";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Surname", student.Surname);
                    cmd.Parameters.AddWithValue("@Name", student.Name);
                    cmd.Parameters.AddWithValue("@Patronymic", student.Patronymic);
                    cmd.Parameters.AddWithValue("@BirthDate", student.BirthDate);
                    cmd.Parameters.AddWithValue("@School", student.School);
                    cmd.Parameters.AddWithValue("@Class", student.Class);
                    cmd.Parameters.AddWithValue("@Sport", student.Sport);
                    cmd.Parameters.AddWithValue("@TrainersName", student.TrainersName);
                    cmd.Parameters.AddWithValue("@EnrollmentGroup", student.EnrollmentGroup);
                    cmd.Parameters.AddWithValue("@Rank", student.Rank);
                    cmd.Parameters.AddWithValue("@OrderNumber", student.OrderNumber);
                    cmd.Parameters.AddWithValue("@DateOfEnrollment", student.DateOfEnrollment);
                    cmd.Parameters.AddWithValue("@OrderEnrollment", student.OrderEnrollment);
                    cmd.Parameters.AddWithValue("@OrderDismissal", (object)student.OrderDismissal ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PaymentType", student.PaymentType);
                    cmd.Parameters.AddWithValue("@ORP_or_SP", student.ORP_or_SP);
                    cmd.Parameters.AddWithValue("@ParentsFullName", student.ParentsFullName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", student.Address);
                    cmd.Parameters.AddWithValue("@ParentsWorkPlace", student.ParentsWorkPlace);
                    cmd.Parameters.AddWithValue("@ParentsPosition", student.ParentsPosition);
                    cmd.Parameters.AddWithValue("@BirthCertificate", student.BirthCertificate);
                    cmd.Parameters.AddWithValue("@DateOfIssue", student.DateOfIssue);
                    cmd.Parameters.AddWithValue("@IssuedBy", student.IssuedBy);
                    cmd.Parameters.AddWithValue("@SNILS", student.SNILS);
                    cmd.Parameters.AddWithValue("@INN", student.INN);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении ученика: {ex.Message}");
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool AddHistory(History history)
        {
            try
            {
                string query = "INSERT INTO History (FullName, `Rank`, Competitions, Norms, `Year`) " +
                               "VALUES (@FullName, @Rank, @Competitions, @Norms, @Year);";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@FullName", history.FullName);
                    cmd.Parameters.AddWithValue("@Rank", history.Rank);
                    cmd.Parameters.AddWithValue("@Competitions", history.Competitions);
                    cmd.Parameters.AddWithValue("@Norms", history.Norms);
                    cmd.Parameters.AddWithValue("@Year", history.Year);
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении: {ex.Message}");
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            try
            {
                string query = "SELECT * FROM Students;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                Id = reader.GetInt32(0),
                                Surname = reader.GetString(1),
                                Name = reader.GetString(2),
                                Patronymic = reader.GetString(3),
                                BirthDate = reader.GetDateTime(4),
                                School = reader.GetString(5),
                                Class = reader.GetString(6),
                                Sport = reader.GetString(7),
                                TrainersName = reader.GetString(8),
                                EnrollmentGroup = reader.GetString(9),
                                Rank = reader.GetString(10),
                                OrderNumber = reader.GetString(11),
                                DateOfEnrollment = reader.GetDateTime(12),
                                OrderEnrollment = reader.GetString(13),
                                OrderDismissal = reader.IsDBNull(14) ? null : reader.GetString(14),
                                PaymentType = reader.GetString(15),
                                ORP_or_SP = reader.GetString(16),
                                ParentsFullName = reader.GetString(17),
                                PhoneNumber = reader.GetString(18),
                                Address = reader.GetString(19),
                                ParentsWorkPlace = reader.GetString(20),
                                ParentsPosition = reader.GetString(21),
                                BirthCertificate = reader.GetString(22),
                                DateOfIssue = reader.GetDateTime(23),
                                IssuedBy = reader.GetString(24),
                                SNILS = reader.GetString(25),
                                INN = reader.GetString(26)
                            };

                            students.Add(student);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка учеников: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return students;
        }

        public List<Sports> GetSports()
        {
            List<Sports> sportsList = new List<Sports>();

            try
            {
                string query = "SELECT * FROM Sports;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Sports sports = new Sports
                            {
                                Id = reader.GetInt32(0),
                                SportName = reader.GetString(1)
                            };
                            sportsList.Add(sports);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка видов спорта: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return sportsList;
        }

        public List<Coaches> GetTrainers()
        {
            List<Coaches> trainersList = new List<Coaches>();

            try
            {
                string query = "SELECT * FROM Coaches;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Coaches coaches = new Coaches
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1)
                            };
                            trainersList.Add(coaches);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка тренеров: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return trainersList;
        }

        public List<History> GetHistory()
        {
            List<History> historylist = new List<History>();

            try
            {
                string query = "SELECT * FROM History;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            History history = new History
                            {
                                ID = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Rank = reader.GetString(2),
                                Competitions = reader.GetString(3),
                                Norms = reader.GetString(4),
                                Year = reader.GetInt32(5)
                            };
                            historylist.Add(history);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка тренеров: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return historylist;
        }

        public List<Groups> GetGroups()
        {
            List<Groups> groupsList = new List<Groups>();

            try
            {
                string query = "SELECT * FROM `Groups`;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Groups groups = new Groups
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1)
                            };
                            groupsList.Add(groups);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка групп: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return groupsList;
        }

        public List<Competition> ReadCompetitions()
        {
            List<Competition> competitions = new List<Competition>();

            try
            {
                string query = "SELECT * FROM Competitions;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Competition competition = new Competition
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                SportType = reader.GetString(2),
                                EventDate = reader.GetDateTime(3),
                                ParticipantsCount = reader.GetInt32(4),
                                Results = reader.GetString(5),
                                Year = reader.GetInt32(6)
                            };

                            competitions.Add(competition);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка соревнований: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return competitions;
        }

        public List<Student> SearchStudents(string searchQuery)
        {
            List<Student> searchStudents = new List<Student>();

            try
            {
                string query = "SELECT * FROM Students WHERE Surname LIKE @searchQuery OR EnrollmentGroup LIKE @searchQuery OR TrainersName LIKE @searchQuery";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@searchQuery", $"%{searchQuery}%");

                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                Id = reader.GetInt32(0),
                                Surname = reader.GetString(1),
                                Name = reader.GetString(2),
                                Patronymic = reader.GetString(3),
                                BirthDate = reader.GetDateTime(4),
                                School = reader.GetString(5),
                                Class = reader.GetString(6),
                                Sport = reader.GetString(7),
                                TrainersName = reader.GetString(8),
                                EnrollmentGroup = reader.GetString(9),
                                Rank = reader.GetString(10),
                                OrderNumber = reader.GetString(11),
                                DateOfEnrollment = reader.GetDateTime(12),
                                OrderEnrollment = reader.GetString(13),
                                OrderDismissal = reader.IsDBNull(14) ? null : reader.GetString(14),
                                PaymentType = reader.GetString(15),
                                ORP_or_SP = reader.GetString(16),
                                ParentsFullName = reader.GetString(17),
                                PhoneNumber = reader.GetString(18),
                                Address = reader.GetString(19),
                                ParentsWorkPlace = reader.GetString(20),
                                ParentsPosition = reader.GetString(21),
                                BirthCertificate = reader.GetString(22),
                                DateOfIssue = reader.GetDateTime(23),
                                IssuedBy = reader.GetString(24),
                                SNILS = reader.GetString(25),
                                INN = reader.GetString(26)
                            };

                            searchStudents.Add(student);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске учеников: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return searchStudents;
        }

        public void UpdateStudent(Student student)
        {
            try
            {
                string query = "UPDATE Students SET Surname = @Surname, Name = @Name, Patronymic = @Patronymic, BirthDate = @BirthDate, " +
               "School = @School, Class = @Class, Sport = @Sport, TrainersName = @TrainersName, EnrollmentGroup = @EnrollmentGroup, `Rank` = @Rank, " +
               "OrderNumber = @OrderNumber, DateOfEnrollment = @DateOfEnrollment, OrderEnrollment = @OrderEnrollment, " +
               "OrderDismissal = @OrderDismissal, PaymentType = @PaymentType, ORP_or_SP = @ORP_or_SP, ParentsFullName = @ParentsFullName, " +
               "PhoneNumber = @PhoneNumber, Address = @Address, ParentsWorkPlace = @ParentsWorkPlace, ParentsPosition = @ParentsPosition, " +
               "BirthCertificate = @BirthCertificate, DateOfIssue = @DateOfIssue, IssuedBy = @IssuedBy, SNILS = @SNILS, INN = @INN " +
               "WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Surname", student.Surname);
                    cmd.Parameters.AddWithValue("@Name", student.Name);
                    cmd.Parameters.AddWithValue("@Patronymic", student.Patronymic);
                    cmd.Parameters.AddWithValue("@BirthDate", student.BirthDate);
                    cmd.Parameters.AddWithValue("@School", student.School);
                    cmd.Parameters.AddWithValue("@Class", student.Class);
                    cmd.Parameters.AddWithValue("@Sport", student.Sport);
                    cmd.Parameters.AddWithValue("@TrainersName", student.TrainersName);
                    cmd.Parameters.AddWithValue("@EnrollmentGroup", student.EnrollmentGroup);
                    cmd.Parameters.AddWithValue("@Rank", student.Rank);
                    cmd.Parameters.AddWithValue("@OrderNumber", student.OrderNumber);
                    cmd.Parameters.AddWithValue("@DateOfEnrollment", student.DateOfEnrollment);
                    cmd.Parameters.AddWithValue("@OrderEnrollment", student.OrderEnrollment);
                    cmd.Parameters.AddWithValue("@OrderDismissal", (object)student.OrderDismissal ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PaymentType", student.PaymentType);
                    cmd.Parameters.AddWithValue("@ORP_or_SP", student.ORP_or_SP);
                    cmd.Parameters.AddWithValue("@ParentsFullName", student.ParentsFullName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", student.Address);
                    cmd.Parameters.AddWithValue("@ParentsWorkPlace", student.ParentsWorkPlace);
                    cmd.Parameters.AddWithValue("@ParentsPosition", student.ParentsPosition);
                    cmd.Parameters.AddWithValue("@BirthCertificate", student.BirthCertificate);
                    cmd.Parameters.AddWithValue("@DateOfIssue", student.DateOfIssue);
                    cmd.Parameters.AddWithValue("@IssuedBy", student.IssuedBy);
                    cmd.Parameters.AddWithValue("@SNILS", student.SNILS);
                    cmd.Parameters.AddWithValue("@INN", student.INN);
                    cmd.Parameters.AddWithValue("@Id", student.Id);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении информации об ученике: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void UpdateHistory(History history)
        {
            try
            {
                string query = "UPDATE History SET FullName = @FullName, `Rank` = @Rank, Competitions = @Competitions, Norms = @Norms, `Year` = @Year WHERE ID = @ID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@FullName", history.FullName);
                    cmd.Parameters.AddWithValue("@Rank", history.Rank);
                    cmd.Parameters.AddWithValue("@Competitions", history.Competitions);
                    cmd.Parameters.AddWithValue("@Norms", history.Norms);
                    cmd.Parameters.AddWithValue("@Year", history.Year);
                    cmd.Parameters.AddWithValue("@ID", history.ID);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении информации: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void DeleteStudent(int studentId)
        {
            try
            {
                string query = "DELETE FROM Students WHERE Id = @StudentId";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении ученика: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void DeleteHistory(int historyid)
        {
            try
            {
                string query = "DELETE FROM History WHERE ID = @historyid";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@historyid", historyid);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении ученика: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public List<Student> FilterStudentsBySport(string sport)
        {
            List<Student> filteredStudents = new List<Student>();

            try
            {
                string query = "SELECT * FROM Students WHERE Sport = @Sport";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Sport", sport);
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = reader.GetInt32(0),
                            Surname = reader.GetString(1),
                            Name = reader.GetString(2),
                            Patronymic = reader.GetString(3),
                            BirthDate = reader.GetDateTime(4),
                            School = reader.GetString(5),
                            Class = reader.GetString(6),
                            Sport = reader.GetString(7),
                            TrainersName = reader.GetString(8),
                            EnrollmentGroup = reader.GetString(9),
                            Rank = reader.GetString(10),
                            OrderNumber = reader.GetString(11),
                            DateOfEnrollment = reader.GetDateTime(12),
                            OrderEnrollment = reader.GetString(13),
                            OrderDismissal = reader.IsDBNull(14) ? null : reader.GetString(14),
                            PaymentType = reader.GetString(15),
                            ORP_or_SP = reader.GetString(16),
                            ParentsFullName = reader.GetString(17),
                            PhoneNumber = reader.GetString(18),
                            Address = reader.GetString(19),
                            ParentsWorkPlace = reader.GetString(20),
                            ParentsPosition = reader.GetString(21),
                            BirthCertificate = reader.GetString(22),
                            DateOfIssue = reader.GetDateTime(23),
                            IssuedBy = reader.GetString(24),
                            SNILS = reader.GetString(25),
                            INN = reader.GetString(26)
                        };
                        filteredStudents.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при фильтрации учеников по виду спорта: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return filteredStudents;
        }

        public List<Student> FilterStudentsByRank(string rank)
        {
            List<Student> filteredStudents = new List<Student>();

            try
            {
                string query = "SELECT * FROM Students WHERE `Rank` = @Rank";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.Add(new MySqlParameter("@Rank", MySqlDbType.VarChar));
                    cmd.Parameters["@Rank"].Value = rank;
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = reader.GetInt32(0),
                            Surname = reader.GetString(1),
                            Name = reader.GetString(2),
                            Patronymic = reader.GetString(3),
                            BirthDate = reader.GetDateTime(4),
                            School = reader.GetString(5),
                            Class = reader.GetString(6),
                            Sport = reader.GetString(7),
                            TrainersName = reader.GetString(8),
                            EnrollmentGroup = reader.GetString(9),
                            Rank = reader.GetString(10),
                            OrderNumber = reader.GetString(11),
                            DateOfEnrollment = reader.GetDateTime(12),
                            OrderEnrollment = reader.GetString(13),
                            OrderDismissal = reader.IsDBNull(14) ? null : reader.GetString(14),
                            PaymentType = reader.GetString(15),
                            ORP_or_SP = reader.GetString(16),
                            ParentsFullName = reader.GetString(17),
                            PhoneNumber = reader.GetString(18),
                            Address = reader.GetString(19),
                            ParentsWorkPlace = reader.GetString(20),
                            ParentsPosition = reader.GetString(21),
                            BirthCertificate = reader.GetString(22),
                            DateOfIssue = reader.GetDateTime(23),
                            IssuedBy = reader.GetString(24),
                            SNILS = reader.GetString(25),
                            INN = reader.GetString(26)
                        };
                        filteredStudents.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при фильтрации учеников по разряду: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return filteredStudents;
        }
        public bool AddCompetition(Competition competition)
        {
            try
            {
                string query = "INSERT INTO Competitions (name, sport_type, event_date, participants_count, results, year) VALUES (@Name, @SportType, @EventDate, @ParticipantsCount, @Results, @Year);";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", competition.Name);
                    cmd.Parameters.AddWithValue("@SportType", competition.SportType);
                    cmd.Parameters.AddWithValue("@EventDate", competition.EventDate);
                    cmd.Parameters.AddWithValue("@ParticipantsCount", competition.ParticipantsCount);
                    cmd.Parameters.AddWithValue("@Results", string.IsNullOrEmpty(competition.Results) ? DBNull.Value : (object)competition.Results);
                    cmd.Parameters.AddWithValue("@Year", competition.Year);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении соревнования: {ex.Message}");
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void DeleteCompetition(int competitionId)
        {
            try
            {
                string query = "DELETE FROM Competitions WHERE Id = @CompetitionId";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CompetitionId", competitionId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении соревнования: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public void UpdateCompetition(Competition competition)
        {
            try
            {
                string query = "UPDATE Competitions SET name = @Name, sport_type = @SportType, event_date = @EventDate, participants_count = @ParticipantsCount, year = @Year WHERE id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", competition.Name);
                    cmd.Parameters.AddWithValue("@SportType", competition.SportType);
                    cmd.Parameters.AddWithValue("@EventDate", competition.EventDate);
                    cmd.Parameters.AddWithValue("@ParticipantsCount", competition.ParticipantsCount);
                    cmd.Parameters.AddWithValue("@Year", competition.Year);
                    cmd.Parameters.AddWithValue("@Id", competition.Id);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении информации о соревновании: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public List<Competition> SearchCompetitions(string searchText)
        {
            List<Competition> searchCompetitions = new List<Competition>();

            try
            {
                string query = "SELECT * FROM Competitions WHERE name LIKE @searchText OR sport_type LIKE @searchText;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@searchText", $"%{searchText}%");

                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Competition competition = new Competition
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                SportType = reader.GetString(2),
                                EventDate = reader.GetDateTime(3),
                                ParticipantsCount = reader.GetInt32(4),
                                Results = reader.IsDBNull(5) ? null : reader.GetString(5)
                            };

                            searchCompetitions.Add(competition);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске соревнований: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return searchCompetitions;
        }

        public List<History> SearchHistory(string searchText)
        {
            List<History> searchHistory = new List<History>();

            try
            {
                string query = "SELECT * FROM History WHERE FullName LIKE @searchText OR Year LIKE @searchText;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@searchText", $"%{searchText}%");

                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            History history = new History
                            {
                                ID = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Rank = reader.GetString(2),
                                Competitions = reader.GetString(3),
                                Norms = reader.GetString(4),
                                Year = reader.GetInt32(5)
                            };

                            searchHistory.Add(history);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске соревнований: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return searchHistory;
        }

        public List<Normative> GetNorms()
        {
            List<Normative> normatives = new List<Normative>();

            try
            {
                string query = "SELECT * FROM Norms;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Normative normative = new Normative
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                SportName = reader.GetString(2),
                                YearComplete = reader.GetInt32(3),
                                TrainerName = reader.GetString(4),
                                NormsAtTheBeginning = reader.GetString(5),
                                NormsAtTheEndOf = reader.GetString(6),
                                SurrenderRate = reader.GetString(7),
                                Comment = reader.GetString(8)
                            };
                            normatives.Add(normative);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка тренеров: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return normatives;
        }

        public bool AddNorms(Normative norms)
        {
            try
            {
                string query = "INSERT INTO Norms (FullName, SportName, YearComplete, TrainerName, NormsAtTheBeginning, NormsAtTheEndOf, SurrenderRate, Comment) VALUES (@FullName, @SportName, @YearComplete, @TrainerName, @NormsAtTheBeginning, @NormsAtTheEndOf, @SurrenderRate, @Comment);";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@FullName", norms.FullName);
                    cmd.Parameters.AddWithValue("@SportName", norms.SportName);
                    cmd.Parameters.AddWithValue("@YearComplete", norms.YearComplete);
                    cmd.Parameters.AddWithValue("@TrainerName", norms.TrainerName);
                    cmd.Parameters.AddWithValue("@NormsAtTheBeginning", norms.NormsAtTheBeginning);
                    cmd.Parameters.AddWithValue("@NormsAtTheBeginning", norms.NormsAtTheEndOf);
                    cmd.Parameters.AddWithValue("@SurrenderRate", norms.SurrenderRate);
                    cmd.Parameters.AddWithValue("@Comment", norms.Comment);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении нормативов: {ex.Message}");
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void UpdateNorms(Normative normative)
        {
            try
            {
                string query = "UPDATE Norms SET FullName = @FullName, SportName = @SportName, YearComplete = @YearComplete, TrainerName = @TrainerName, NormsAtTheBeginning = @NormsAtTheBeginning, NormsAtTheEndOf = @NormsAtTheEndOf, SurrenderRate = @SurrenderRate, Comment = @Comment WHERE id = @ID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@FullName", normative.FullName);
                    cmd.Parameters.AddWithValue("@SportName", normative.SportName);
                    cmd.Parameters.AddWithValue("@YearComplete", normative.YearComplete);
                    cmd.Parameters.AddWithValue("@TrainerName", normative.TrainerName);
                    cmd.Parameters.AddWithValue("@NormsAtTheBeginning", normative.NormsAtTheBeginning);
                    cmd.Parameters.AddWithValue("@NormsAtTheBeginning", normative.NormsAtTheEndOf);
                    cmd.Parameters.AddWithValue("@SurrenderRate", normative.SurrenderRate);
                    cmd.Parameters.AddWithValue("@Comment", normative.Comment);
                    cmd.Parameters.AddWithValue("@ID", normative.Id);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении информации: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void DeleteNorms(int normsId)
        {
            try
            {
                string query = "DELETE FROM Norms WHERE id = @normsId";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@normsId", normsId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении ученика: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public List<Normative> SearchNorms(string searchText)
        {
            List<Normative> searchNormative = new List<Normative>();

            try
            {
                string query = "SELECT * FROM Norms WHERE FullName LIKE @searchText OR YearComplete LIKE @searchText;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@searchText", $"%{searchText}%");

                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Normative normative = new Normative 
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                SportName = reader.GetString(2),
                                YearComplete = reader.GetInt32(3),
                                TrainerName = reader.GetString(4),
                                NormsAtTheBeginning = reader.GetString(5),
                                NormsAtTheEndOf = reader.GetString(6),
                                SurrenderRate = reader.GetString(7),
                                Comment = reader.GetString(8)
                            };

                            searchNormative.Add(normative);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске соревнований: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return searchNormative;
        }
        public bool AddCoaches(Coaches coaches)
        {
            try
            {
                string query = "INSERT INTO Coaches (FullName) VALUES (@FullName);";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@FullName", coaches.FullName);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении тренера: {ex.Message}");
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public void DeleteCoaches(int сoachesId)
        {
            try
            {
                string query = "DELETE FROM Coaches WHERE id = @сoachesId";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@сoachesId", сoachesId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении ученика: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool AddSport(Sports sports)
        {
            try
            {
                string query = "INSERT INTO Sports (SportName) VALUES (@SportName);";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SportName", sports.SportName);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении тренера: {ex.Message}");
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public void DeleteSport(int sportsId)
        {
            try
            {
                string query = "DELETE FROM Sports WHERE id = @sportsId";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@sportsId", sportsId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении ученика: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public List<Events> GetEvents()
        {
            List<Events> Event = new List<Events>();

            try
            {
                string query = "SELECT * FROM Events;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Events events = new Events
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Level = reader.GetString(2),
                                Date = reader.GetDateTime(3),
                                Amount = reader.GetString(4),
                                Result = reader.GetString(5),
                                Year = reader.GetInt32(6)
                            };
                            Event.Add(events);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка тренеров: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return Event;
        }

        public bool AddEvents(Events events)
        {
            try
            {
                string query = "INSERT INTO Events (Title, `Level`, `Date`, Amount, `Result`, `Year`) VALUES (@Title, @Level, @Date, @Amount, @Result, @Year);";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", events.Title);
                    cmd.Parameters.AddWithValue("@Level", events.Level);
                    cmd.Parameters.AddWithValue("@Date", events.Date);
                    cmd.Parameters.AddWithValue("@Amount", events.Amount);
                    cmd.Parameters.AddWithValue("@Result", events.Result);
                    cmd.Parameters.AddWithValue("@Year", events.Year);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении нормативов: {ex.Message}");
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void UpdateEvents(Events events)
        {
            try
            {
                string query = "UPDATE Events SET Title = @Title, `Level` = @Level, `Date` = @Date, Amount = @Amount, `Result` = @Result, `Year` = @Year WHERE Id = @ID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", events.Title);
                    cmd.Parameters.AddWithValue("@Level", events.Level);
                    cmd.Parameters.AddWithValue("@Date", events.Date);
                    cmd.Parameters.AddWithValue("@Amount", events.Amount);
                    cmd.Parameters.AddWithValue("@Result", events.Result);
                    cmd.Parameters.AddWithValue("@Year", events.Year);
                    cmd.Parameters.AddWithValue("@ID", events.Id);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении информации: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void DeleteEvents(int eventId)
        {
            try
            {
                string query = "DELETE FROM Events WHERE Id = @eventId";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@eventId", eventId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении мероприятия: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public List<Events> SearchEvents(string searchText)
        {
            List<Events> searchEvents = new List<Events>();

            try
            {
                string query = "SELECT * FROM Events WHERE Level LIKE @searchText OR Year LIKE @searchText;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@searchText", $"%{searchText}%");

                    connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Events events = new Events
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Level = reader.GetString(2),
                                Date = reader.GetDateTime(3),
                                Amount = reader.GetString(4),
                                Result = reader.GetString(5),
                                Year = reader.GetInt32(6)
                            };

                            searchEvents.Add(events);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске соревнований: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return searchEvents;
        }

        public bool AddGroups(Groups groups)
        {
            try
            {
                string query = "INSERT INTO `Groups` (Title) VALUES (@Title);";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", groups.Title);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении группы: {ex.Message}");
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public void DeleteGroups(int groupsId)
        {
            try
            {
                string query = "DELETE FROM `Groups` WHERE Id = @groupsId";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@groupsId", groupsId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении группы: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}