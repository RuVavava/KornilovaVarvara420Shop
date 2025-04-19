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
    /// Логика взаимодействия для AllProductPage.xaml
    /// </summary>
    public partial class AllProductPage : Page
    {
        public static List<Produts> products { get; set; }

        private int pageNumber = 1;
        private int pageCount = 1;
        public List<Produts> CurrentPageProducts { get; set; }

        public AllProductPage()
        {
            InitializeComponent();
            products = DBConnection.shopEntities.Produts.ToList();
            this.DataContext = this;
            Refresh();
        }
        private void UpdatePageNavigation()
        {
            NavSp.Children.Clear();
            if (pageCount > 1)
            {
                Button button1 = new Button
                {
                    Content = "<",
                    IsHitTestVisible = pageNumber > 1,
                    Background = new SolidColorBrush(Colors.Transparent),
                    BorderBrush = new SolidColorBrush(Colors.Transparent),
                };
                button1.Click += PageBtn_Click;
                NavSp.Children.Add(button1);

                int totalProducts = products.Count;
                int startItem = (pageNumber - 1) * 6 + 1;
                int endItem = Math.Min(pageNumber * 6, totalProducts);

                CountTB.Text = $"{startItem}-{endItem} из {totalProducts}";

                Button button3 = new Button
                {
                    Content = ">",
                    IsHitTestVisible = pageNumber < pageCount,
                    Background = new SolidColorBrush(Colors.Transparent),
                    BorderBrush = new SolidColorBrush(Colors.Transparent)
                };
                button3.Click += PageBtn_Click;
                NavSp.Children.Add(button3);
            }
            else
            {
                // Если всего одна страница, показываем просто общее количество
                int totalProducts = products.Count;
                TextBlock rangeText = new TextBlock()
                {
                    Text = $"1-{totalProducts} из {totalProducts}",
                    Margin = new Thickness(10, 0, 10, 0),
                    VerticalAlignment = VerticalAlignment.Center
                };
                NavSp.Children.Add(rangeText);
            }
        }

        private void Refresh()
        {
            var filteredProducts = products;

            // Применяем поиск (если есть текст в SearchTB)
            if (SearchTB.Text.Length > 0)
            {
                filteredProducts = filteredProducts.Where(i => i.Name.ToLower().Contains(SearchTB.Text.Trim().ToLower())
                                                    || i.Description.ToLower().StartsWith(SearchTB.Text.Trim().ToLower())).ToList();
            }

            // Обновляем количество страниц
            pageCount = (int)Math.Ceiling((double)filteredProducts.Count / 6);

            // Получаем продукты для текущей страницы
            CurrentPageProducts = filteredProducts.Skip((pageNumber - 1) * 6).Take(6).ToList();

            // Обновляем отображаемые данные
            ProductsLV.ItemsSource = CurrentPageProducts;

            // Обновляем навигацию по страницам
            UpdatePageNavigation();
        }
        private void ProductsLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsLV.SelectedItem is Produts selectedProduct)
            {
                NavigationService.Navigate(new EditProductPage(selectedProduct));

                Refresh();

            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPaage());
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Вы точно хотите закрыть приложение?",
            "Подтверждение закрытия",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                Window.GetWindow(this)?.Close();
                Application.Current.Shutdown();
            }
        }

        //Метод свертывания приложения
        private void SvernutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Вы точно хотите свернуть окно?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Window window = Window.GetWindow(this);
                if (window != null)
                {
                    window.WindowState = WindowState.Minimized;
                }
            }
        }


        private void KormBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void PageBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Content.ToString())
            {
                case "<":
                    pageNumber--;
                    break;
                case ">":
                    pageNumber++;
                    break;
                default:
                    pageNumber = int.Parse(((TextBlock)button.Content).Text);
                    break;
            }
            Refresh();
        }
    }
}