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
    /// Логика взаимодействия для AddProductPaage.xaml
    /// </summary>
    public partial class AddProductPaage : Page
    {
        public static List<Produts> products { get; set; }

        public static Produts product = new Produts();
        public AddProductPaage()
        {
            InitializeComponent();
            products = new List<Produts>(DBConnection.shopEntities.Produts.ToList());
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();

                if (string.IsNullOrWhiteSpace(NameTbx.Text) || string.IsNullOrWhiteSpace(DescriptionTbx.Text))

                {
                    error.AppendLine("Заполните все поля!");
                }

                else
                {
                    product.Name = NameTbx.Text.Trim();
                    product.Description = DescriptionTbx.Text.Trim();
                    product.Cost = Convert.ToInt16(CostTbx.Text.Trim());


                    DBConnection.shopEntities.Produts.Add(product);
                    DBConnection.shopEntities.SaveChanges();
                    NavigationService.Navigate(new AllProductPage());
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

        private void AddPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = $"/pr/{openFileDialog.SafeFileName}";

                Photo.Source = new BitmapImage(new Uri(selectedImagePath, UriKind.Relative));

                product.MainImage = selectedImagePath;


            }
            DBConnection.shopEntities.SaveChanges();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AllProductPage());
        }
    }
}
