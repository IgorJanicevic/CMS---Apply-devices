using Projekat.DAO;
using Projekat.Helpers;
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
using System.Windows.Shapes;

namespace Projekat.Pages
{
    /// <summary>
    /// Interaction logic for ShowItemPage.xaml
    /// </summary>
    public partial class ShowItemPage : Window
    {
        public ShowItemPage(ContentItem item)
        {
            InitializeComponent();
            LoadData(item);

        }

        private void LoadData(ContentItem item)
        {
            PreviewImageFunction(item.ImagePath);
            RTFHelper.GetTextFromRTF(item.RtfFilePath, DescriptionRichTextBox);
            TitleTextBox.Text = item.Title;
            DateAddedLabel.Content = item.DataAdded.ToString();
        }

        private void PreviewImageFunction(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            ItemImage.Source = bitmap;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want go back", "Save", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                GuestPage guestPage = new GuestPage();
                guestPage.Show();
                this.Close();
            }
        }
    }
}
