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
using ProjectLopushok.Utilities;
using ProjectLopushok.Entities;

namespace ProjectLopushok.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageProduct.xaml
    /// </summary>
    public partial class PageProduct : Page
    {
        public List<ProductMaterial> ItemsProdusctMaterial { get { return DBcontext.Context.ProductMaterial.ToList(); } }
        public PageProduct()
        {
            InitializeComponent();

            ListProduct.ItemsSource = ItemsProdusctMaterial;

            var ProductType = DBcontext.Context.ProductType.ToList();
            ProductType.Insert(0, new ProductType { Title = "Все типы"});
            cmbType.ItemsSource = ProductType;
            cmbType.SelectedIndex = 0;

            cmbSort.Items.Insert(0, "");
            cmbSort.Items.Insert(1, "Наименование");
            cmbSort.Items.Insert(2, "Номер цеха");
            cmbSort.Items.Insert(3, "стоимость для агентов");
            cmbSort.SelectedIndex = 0;
        }

        private void SortingChane()
        {
            var sortingItem = DBcontext.Context.ProductMaterial.ToList();
            if(string.IsNullOrWhiteSpace(TxtSearch.Text) == false)
            {
                sortingItem = sortingItem.Where(sort => sort.Product.Title.ToLower().Contains(TxtSearch.Text.ToLower()) || sort.Product.Description.ToLower().Contains(TxtSearch.Text.ToLower())).ToList();
            }
            if(cmbType.SelectedIndex > 0)
            {
                sortingItem = sortingItem.Where(sort => sort.Product.ProductTypeID == cmbType.SelectedIndex).ToList();
            }
            switch (cmbSort.SelectedIndex)
            {
                case 1:
                    {
                        if(chbfiltr.IsChecked == true)
                        {
                            sortingItem = sortingItem.OrderByDescending(sort => sort.Product.Title).ToList();
                        }
                        else
                        {
                            sortingItem = sortingItem.OrderBy(sort => sort.Product.Title).ToList();
                        }
                        break;
                    }
                case 2:
                    {
                        if (chbfiltr.IsChecked == true)
                        {
                            sortingItem = sortingItem.OrderByDescending(sort => sort.Product.ProductionWorkshopNumber).ToList();
                        }
                        else
                        {
                            sortingItem = sortingItem.OrderBy(sort => sort.Product.ProductionWorkshopNumber).ToList();
                        }
                        break;
                    }
                case 3:
                    {
                        if (chbfiltr.IsChecked == true)
                        {
                            sortingItem = sortingItem.OrderByDescending(sort => sort.Product.MinCostForAgent).ToList();
                        }
                        else
                        {
                            sortingItem = sortingItem.OrderBy(sort => sort.Product.MinCostForAgent).ToList();
                        }
                        break;
                    }
            }
            ListProduct.ItemsSource = sortingItem;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SortingChane();
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortingChane();
        }

        private void chbfiltr_Checked(object sender, RoutedEventArgs e)
        {
            SortingChane();
        }

        private void chbfiltr_Unchecked(object sender, RoutedEventArgs e)
        {
            SortingChane();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortingChane();
        }
    }
}
