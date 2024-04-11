using Projekat.DAO;
using Projekat.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;

namespace Projekat.Pages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public static ObservableCollection<ContentItem> ContentItems { get; set; }
        private DataIO serializer = new DataIO();
        
        public AdminPage()
        {
            InitializeComponent();
            LoadData();
            dgContentItems.DataContext = this;
            StatusForItemsCount();          


        }

        private void StatusForItemsCount()
        {
            ContentItems.CollectionChanged += (sender, e) =>
            {
                cntItems.Text = ContentItems.Count().ToString();
            };
        }

        private void LoadData()
        {
            ContentItems = serializer.DeSerializeObject<ObservableCollection<ContentItem>>("Content.xml");
            cntItems.Text = ContentItems.Count().ToString();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            AddItemPage addItemPage = new AddItemPage();
            addItemPage.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result= MessageBox.Show("Are you sure you want delete this items?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var itemsToDelete = new ObservableCollection<ContentItem>(ContentItems.Where(item => item.IsSelected).ToList());
                int i = 0;
                foreach (ContentItem item in itemsToDelete)
                {
                    if (item.IsSelected)
                    {
                        i++;
                        if (File.Exists(item.RtfFilePath))
                            File.Delete(item.RtfFilePath);
                        ContentItems.Remove(item);
                    }
                }
                if (i == 0)
                {
                    MessageBox.Show("You did not select item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Successfully deleted " + i + " items!", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit and save changes?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                serializer.SerializeObject<ObservableCollection<ContentItem>>(ContentItems, "Content.xml");

                MainWindow userLoginWindow= new MainWindow();
                userLoginWindow.Show();
                this.Close();

            }
        }

        private void OpenShowObjectWindow(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = e.OriginalSource as Hyperlink;

            if (hyperlink != null && hyperlink.DataContext is ContentItem item)
            {

                AddItemPage addItemPage = new AddItemPage(item);
                addItemPage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("DataContext is not a ContentItem");
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
