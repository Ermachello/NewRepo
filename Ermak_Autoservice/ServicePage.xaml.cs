﻿using System;
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

namespace Ermak_Autoservice
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();

            var currentServices = Ermak_autoserviceEntities.GetContext().Service.ToList();
            ServiceListView.ItemsSource = currentServices;

            ComboType.SelectedIndex = 0;
            UpdateSevices();
        }
        
        private void UpdateSevices()
        {
            var currentServices = Ermak_autoserviceEntities.GetContext().Service.ToList();
            //Условия фильтрации
            if(ComboType.SelectedIndex == 0) 
            {
                currentServices = currentServices.Where(p=> (p.Discount >= 0 && p.Discount <= 100)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentServices = currentServices.Where(p => (p.Discount >= 0 && p.Discount < 5)).ToList();
            }
            if(ComboType.SelectedIndex == 2)
            {
                currentServices = currentServices.Where(p => (p.Discount >= 5 && p.Discount < 15)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentServices = currentServices.Where(p => (p.Discount >= 15 && p.Discount < 30)).ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {
                currentServices = currentServices.Where(p => (p.Discount >= 30 && p.Discount < 70)).ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {
                currentServices = currentServices.Where(p => (p.Discount >= 70 && p.Discount < 100)).ToList();
            }
            //Поиск данных при вводе текста в окно
            currentServices = currentServices.Where (p=> p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            if(RButtonDown.IsChecked.Value)
            {
                currentServices = currentServices.OrderByDescending (p => p.Cost).ToList();
            }
            if (RButtonUp.IsChecked.Value)
            {
                currentServices = currentServices.OrderBy(p => p.Cost).ToList();
            }
            ServiceListView.ItemsSource = currentServices;
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdtPage(null));
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSevices();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSevices();
        }
        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSevices();
        }
        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSevices();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdtPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible) 
            {
                Ermak_autoserviceEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ServiceListView.ItemsSource = Ermak_autoserviceEntities.GetContext().Service.ToList();
            }
        }

        private void EditButton_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdtPage((sender as Button).DataContext as Service));
        }
    }
}
