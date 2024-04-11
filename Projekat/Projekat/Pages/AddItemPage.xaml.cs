using Projekat.DAO;
using Projekat.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for AddItemPage.xaml
    /// </summary>
    public partial class AddItemPage : Window
    {
        private ObservableCollection<ContentItem> ContentItems;
        private DataIO serializer = new DataIO();
        private string imagePathString = null;
        private int IDforUpdate = 0;
        private ContentItem itemForUpdate;
        private bool newObject = true;
        List<int> fontsize = new List<int>();


        public AddItemPage()
        {
            InitObject();
            newObject = true;
        }

        public AddItemPage(ContentItem itemForUpdate)
        {
            InitObject();
            EditObject(itemForUpdate);
            newObject = false;

        }


        private void InitObject()
        {
            InitializeComponent();
            ContentItems = AdminPage.ContentItems;
            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            for (int i = 10; i < 36; i += 2)
                fontsize.Add(i);
            FontSizeComboBox.ItemsSource = fontsize;
            FontColorComboBox.ItemsSource = typeof(Colors).GetProperties().Select(c => new { Name = c.Name, Color = (Color)c.GetValue(null) }).ToList();

        }
        private void EditObject(ContentItem item)
        {
            TitleTextBox.Text = item.Title;
            TitleTextBox.Foreground = Brushes.Black;
            IDTextBox.Text = item.ID.ToString();
            IDforUpdate = item.ID;
            IDTextBox.Foreground = Brushes.Black;
            RTFHelper.GetTextFromRTF(item.RtfFilePath, EditorRichTextBox);
            imagePathString = item.ImagePath;
            PreviewImageFunction(item.ImagePath);

        }

        private void PreviewImageFunction(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            PreviewImage.Source = bitmap;
        }

        private void FontColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (FontColorComboBox.SelectedItem != null && !EditorRichTextBox.Selection.IsEmpty)
            {
                var selectedColor = ((dynamic)FontColorComboBox.SelectedItem).Color;
                SolidColorBrush colorBrush = new SolidColorBrush(selectedColor);

                EditorRichTextBox.Selection.ApplyPropertyValue(Inline.ForegroundProperty, colorBrush);
            }
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem != null && !EditorRichTextBox.Selection.IsEmpty)
            {
                EditorRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FontFamilyComboBox.SelectedItem);
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeComboBox.SelectedItem != null && !EditorRichTextBox.Selection.IsEmpty)
            {

                EditorRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, Convert.ToDouble(FontSizeComboBox.SelectedItem));
                
            }

        }


        private void btnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                imagePathString = openFileDialog.FileName;
                PreviewImageFunction(imagePathString);
            }
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationForm())
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want save changes", "Save", MessageBoxButton.YesNo, MessageBoxImage.Question); ;
                if (result == MessageBoxResult.Yes)
                {
                    if (newObject)
                    {
                        CreateObject();
                        AdminPage adminPage= new AdminPage();
                        adminPage.Show();
                        this.Close();
                    }
                    else
                    {
                        UpdateObject();
                        AdminPage adminPage= new AdminPage();
                        adminPage.Show();
                        this.Close();
                    }

                }
            }
        }

        private bool ValidationForm()
        {
            bool isValid = true;
            if (int.TryParse(IDTextBox.Text, out _) && !IDTextBox.Text.Equals("0"))
            {
                
                IDErrorLabel.Content = "";

                foreach (ContentItem item in AdminPage.ContentItems)
                {
                    if (item.ID == Convert.ToInt32(IDTextBox.Text) && item.ID!=IDforUpdate)
                    {
                        isValid = false;
                        IDErrorLabel.Content = "Item with this ID already exists";
                        break;
                    }

                }
            }
            else
            {
                if (IDTextBox.Text.Equals("") || IDTextBox.Foreground==Brushes.Gray)
                {
                    isValid = false;
                    IDErrorLabel.Content = "ID is not valid!";
                }

               
            }

            if (!TitleTextBox.Text.Equals("Title"))
            {
                TitleErrorLabel.Content = "";
            }
            else
            {
                if (TitleTextBox.Foreground != Brushes.Gray)
                {
                    isValid = false;
                    TitleErrorLabel.Content = "Enter a title!";
                }

            }


            if (imagePathString != null)
            {

            }
            else
            {
                isValid = false;
                MessageBox.Show("Upload image for item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



            return isValid;
        }

        private void CreateObject()
        {

            int id = Convert.ToInt32(IDTextBox.Text);
            string titleForRtf = TitleTextBox.Text.Replace(" ", "-");
            string rtfFile = String.Format("{0}{1}.rtf", titleForRtf, id);

            RTFHelper.SaveRichTextBoxContent(rtfFile, EditorRichTextBox);
            ContentItem item = new ContentItem(id, TitleTextBox.Text, "", imagePathString, rtfFile, DateTime.Now);
            ContentItems.Add(item);
            serializer.SerializeObject<ObservableCollection<ContentItem>>(ContentItems, "Content.xml");



        }



        private void UpdateObject()
        {
            int id = Convert.ToInt32(IDTextBox.Text);
            string titleForRtf = TitleTextBox.Text.Replace(" ", "-");
            string rtfFile = String.Format("{0}{1}.rtf", titleForRtf, id);

            foreach (ContentItem item in ContentItems)
            {
                if (item.ID == IDforUpdate)
                {

                    string OldRtf = item.RtfFilePath;
                    if (File.Exists(OldRtf))                    
                        File.Delete(OldRtf);

                    RTFHelper.SaveRichTextBoxContent(rtfFile, EditorRichTextBox);

                    item.Title = TitleTextBox.Text;
                    item.RtfFilePath = rtfFile;
                    item.ImagePath = imagePathString;
                    item.ID = id;
                    item.Description = "";

                    serializer.SerializeObject<ObservableCollection<ContentItem>>(ContentItems, "Content.xml");

                }
            }
             
        }

        private void TitleTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TitleTextBox.Text.Equals("Title"))
            {
                TitleTextBox.Text = string.Empty;
                TitleTextBox.Foreground = Brushes.Black;
            }
        }

        private void TitleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TitleTextBox.Text == string.Empty || TitleTextBox.Text.Equals("Title"))
            {
                TitleTextBox.Text = "Title";
                TitleTextBox.Foreground = Brushes.LightGray;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                AdminPage adminPage = new AdminPage();
                adminPage.Show();
                this.Close();
            }
        }

        private void EditorRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            string text = new TextRange(EditorRichTextBox.Document.ContentStart, EditorRichTextBox.Document.ContentEnd).Text;

            int wordCount = CountWords(text);

            WordCountTextBlock.Text = wordCount.ToString();

        }
        private void EditorRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {

            object fontWeight = EditorRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            BoldToggleButton.IsChecked = (fontWeight != DependencyProperty.UnsetValue) && (fontWeight.Equals(FontWeights.Bold));

            object fontStyle = EditorRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            ItalicToggleButton.IsChecked = (fontStyle != DependencyProperty.UnsetValue) && (fontStyle.Equals(FontStyles.Italic));

            object underline = EditorRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            UnderlineToggleButton.IsChecked = (underline != DependencyProperty.UnsetValue) && (underline.Equals(TextDecorations.Underline));
        
            object fontFamily = EditorRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            FontFamilyComboBox.SelectedItem = fontFamily;

            object fontSize = EditorRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty);
            if (int.TryParse(fontSize.ToString(), out int result))
                FontSizeComboBox.SelectedItem = Convert.ToInt32(result);

            object fontColor = EditorRichTextBox.Selection.GetPropertyValue(Inline.ForegroundProperty);
            if (fontColor != DependencyProperty.UnsetValue)
            {
                SolidColorBrush colorBrush = fontColor as SolidColorBrush;
                if (colorBrush != null)
                {
                    Color selectedColor = colorBrush.Color;

                    var selectedColorItem = FontColorComboBox.Items.Cast<dynamic>().FirstOrDefault(item => item.Color == selectedColor);

                    FontColorComboBox.SelectedItem = selectedColorItem;
                }
            }

        }


        private int CountWords(string text)
        {
            int wordCount = 0;
            bool inWord = false;

            foreach (char c in text)
            {
                if (char.IsWhiteSpace(c) || char.IsPunctuation(c))
                {
                    if (inWord)
                    {
                        wordCount++;
                        inWord = false;
                    }
                }
                else
                {
                    inWord = true;
                }
            }

            if (inWord)
            {
                wordCount++;
            }

            return wordCount;
        }

        private void IDTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!IDTextBox.Text.Equals("0"))
            {  
                IDTextBox.Foreground = Brushes.Black;

            }
            else
            {
                IDTextBox.Text = "";
                IDTextBox.Foreground= Brushes.Gray;
            }
        }

        private void IDTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IDTextBox.Text.Equals("0"))
            {
                IDTextBox.Foreground = Brushes.Gray;
            }
            else
            {
                IDTextBox.Foreground = Brushes.Black;
            }

        }
    }
}
