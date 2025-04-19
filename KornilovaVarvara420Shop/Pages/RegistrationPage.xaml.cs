using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KornilovaVarvara420Shop.DB;

namespace KornilovaVarvara420Shop.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public static List<Users> users { get; set; }
        public RegistrationPage()
        {
            InitializeComponent();

            users = new List<Users>(DBConnection.shopEntities.Users.ToList());
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();

                if (string.IsNullOrWhiteSpace(NameTbx.Text) || string.IsNullOrWhiteSpace(SurnameTbx.Text)
                    || (LoginTbx.Text.Trim() == null) || (PasswordTbx.Text.Trim() == null))

                {
                    error.AppendLine("Заполните все поля!");
                }

                else
                {
                    Users user = new Users();

                    user.FirstName = NameTbx.Text.Trim();
                    user.LastName = SurnameTbx.Text.Trim();
                    user.Patronymic = PatrTbx.Text.Trim();
                    user.Login = Convert.ToInt16(LoginTbx.Text.Trim());
                    user.Password = Convert.ToInt32(PasswordTbx.Text.Trim());

                    user.Balance = 0;
                    user.IdRole = 5;
                    user.MainPhoto = null;


                    DBConnection.shopEntities.Users.Add(user);
                    DBConnection.shopEntities.SaveChanges();
                    NavigationService.Navigate(new AuthorizationPage());
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }
        }

        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);

        }
        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);

        }
    }
}

