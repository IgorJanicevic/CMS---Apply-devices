using Projekat.DAO;
using Projekat.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for GuestPage.xaml
    /// </summary>
    public partial class GuestPage : Window
    {
        public static ObservableCollection<ContentItem> ContentItems { get; set; }
        private DataIO serializer = new DataIO();
        public GuestPage()
        {
            InitializeComponent();
            LoadData();
            dgContentItems.DataContext = this;
            cntItems.Text= ContentItems.Count().ToString();
        }

        private void LoadData()
        {
            ContentItems = serializer.DeSerializeObject<ObservableCollection<ContentItem>>("Content.xml");         
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit and save changes?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                serializer.SerializeObject<ObservableCollection<ContentItem>>(ContentItems, "Content.xml");

                MainWindow userLoginWindow = new MainWindow();
                userLoginWindow.Show();

                Window parentWindow = Window.GetWindow(this);
                parentWindow.Close();

            }
        }


        private void OpenShowObjectWindow(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = e.OriginalSource as Hyperlink;

            if (hyperlink != null && hyperlink.DataContext is ContentItem item)
            {
                ShowItemPage showItemPage = new ShowItemPage(item);
                showItemPage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("DataContext is not a ContentItem");
            }
        }
    }
}
