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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static List<Users> users { get; set; }
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int login = Convert.ToInt32(LoginTbx.Text.Trim());
                int password = Convert.ToInt32(PasswordPbx.Password.Trim());

                users = new List<Users>(DBConnection.shopEntities.Users.ToList());
                var currentUser = users.FirstOrDefault(i => i.Login == login && i.Password == password);
                DBConnection.logginedUser = currentUser;

                if (currentUser != null && currentUser.IdRole == 1)
                {
                    NavigationService.Navigate(new UserPage());
                }

                else if (currentUser != null && currentUser.IdRole == 2)
                {
                    NavigationService.Navigate(new AllProductPage());
                }

                else if (currentUser != null && currentUser.IdRole == 3)
                {
                    NavigationService.Navigate(new UserPage());
                }

                else if (currentUser != null && currentUser.IdRole == 4)
                {
                    NavigationService.Navigate(new UserPage());
                }

                else if (currentUser != null && currentUser.IdRole == 5)
                {
                    NavigationService.Navigate(new UserPage());
                }

                else

                {
                    MessageBox.Show("Неверный логин или пароль. Попробуйте снова.");
                }
            }
            catch
            {
                MessageBox.Show("Возникла ошибка");
            }

        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
