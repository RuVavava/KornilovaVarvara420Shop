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
    /// Логика взаимодействия для EditProductPage.xaml
    /// </summary>
    public partial class EditProductPage : Page
    {
        public static List<Produts> products { get; set; }

        Produts contextProduct;
        public EditProductPage(Produts product)
        {
            InitializeComponent();
            contextProduct = product;

            DescriptionTbx.Text = contextProduct.Description;
            NameTbx.Text = contextProduct.Name;
            CostTbx.Text = contextProduct.Cost.ToString();
            Photo.Source = new BitmapImage(new Uri(product.MainImage, UriKind.Relative));
        }

        private void AddPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = $"/pr/{openFileDialog.SafeFileName}";

                Photo.Source = new BitmapImage(new Uri(selectedImagePath, UriKind.Relative));

                contextProduct.MainImage = selectedImagePath;

                DBConnection.shopEntities.SaveChanges();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();

                Produts product = contextProduct;

                if (string.IsNullOrWhiteSpace(NameTbx.Text))
                {
                    error.AppendLine("Заполните необходимые поля!");
                }

                else
                {
                    product.Name = NameTbx.Text;
                    product.Description = DescriptionTbx.Text;
                    product.Cost = Convert.ToInt32(CostTbx.Text);

                    DBConnection.shopEntities.SaveChanges();

                    DescriptionTbx.Text = String.Empty;
                    NameTbx.Text = String.Empty;

                    DBConnection.shopEntities.SaveChanges();
                    NavigationService.Navigate(new AllProductPage());

                }
            }
            catch
            {
                MessageBox.Show("Ошибка.");
            }

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AllProductPage());
        }
    }
}
