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
using ProjectLopushok.Entities;
using ProjectLopushok.Utilities;

namespace ProjectLopushok.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        private Product currentProduxt;
        public AddProductPage(Product product)
        {
            InitializeComponent();
            currentProduxt = product ?? new Product();
            CmbType.ItemsSource = DBcontext.Context.ProductType.ToList();
            DataContext = currentProduxt;
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentProduxt.ArticleNumber))
                builder.AppendLine("Укажите артикул");
            if (string.IsNullOrWhiteSpace(currentProduxt.Title))
                builder.AppendLine("Укажите наименование");
            if (string.IsNullOrWhiteSpace(currentProduxt.MinCostForAgent.ToString()))
                builder.AppendLine("Укажите минимальную стоимость для Агента");
            if (currentProduxt.MinCostForAgent.ToString().StartsWith("-"))
                builder.AppendLine("Вы ввели отрицательное значение");
            if (string.IsNullOrWhiteSpace(currentProduxt.Description))
                currentProduxt.Description = "";

            if (builder.Length > 0)
            {
                MessageBox.Show(builder.ToString());
                return;
            }

            if (currentProduxt.ID == 0)
            {
                DBcontext.Context.Product.Add(currentProduxt);
            }

            try
            {
                DBcontext.Context.SaveChanges();
                MessageBox.Show("Данные сохранены");
                FrameManeger.frmMain.Navigate(new PageProduct());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            FrameManeger.frmMain.GoBack();
        }
    }
}
