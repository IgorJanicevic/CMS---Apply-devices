using Notification.Wpf;
using Projekat.Helpers;
using Projekat.Model;
using Projekat.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace Projekat
{
   
    public partial class MainWindow : Window
    {

        private DataIO serializer = new DataIO();
        public ObservableCollection<User> users;
        private Role userRoleForLogin;

        public MainWindow()
        {
            InitializeComponent();
            users = serializer.DeSerializeObject<ObservableCollection<User>>("Users.xml");
            
        }

       

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (DoesUserExist())
            {
                if (userRoleForLogin == Role.Admin)
                {
                    AdminPage adminPage = new AdminPage();
                    adminPage.Show();
                    this.Close();
                }
                else
                {
                    GuestPage guestPage = new GuestPage();
                    guestPage.Show();
                    this.Close();
                }
            }
            else
            {

            }

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private bool DoesUserExist()
        {

            foreach (User user in users)
            {
                if (user.Name.Equals(NameTextBox.Text))
                {
                    if (user.Password.Equals(UserPasswordBox.Password))
                    {
                        userRoleForLogin = user.Role;
                        return true;
                    }
                }


            }
            int nameCorrect = 0;
            int passCorrect = 0;
            foreach (User user in users)
            {
                if (user.Name.Equals(NameTextBox.Text))
                {
                    nameCorrect = 1;
                    break;
                }
            }
            foreach (User user in users)
            {
                if (user.Password.Equals(UserPasswordBox.Password))
                {
                    passCorrect = 1;
                    break;
                }
            }
            if (nameCorrect == 0)
            {
                NameLabelError.Content = "Username is not valid";
            }
            else
            {
                NameLabelError.Content = "";
            }

            if (passCorrect == 0)
            {
                PasswordLabelError.Content = "Password is not correct";
            }
            else
            {
                PasswordLabelError.Content = "";
            }


            return false;
        }
    }
}
