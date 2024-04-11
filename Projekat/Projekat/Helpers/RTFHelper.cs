using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;

namespace Projekat.Helpers
{
    public static class RTFHelper
    {

        public static void SaveRichTextBoxContent(string fileName, RichTextBox EditorRichTextBox)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            TextRange textRange = new TextRange(EditorRichTextBox.Document.ContentStart, EditorRichTextBox.Document.ContentEnd);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                textRange.Save(fileStream, DataFormats.Rtf);
            }

        }
        public static void GetTextFromRTF(string fileName, RichTextBox richTextBox)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                    textRange.Load(fileStream, DataFormats.Rtf);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading RTF file: {ex.Message}");
            }
        }

    }
}
