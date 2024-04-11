using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.DAO
{
    [Serializable]
    public class ContentItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string RtfFilePath { get; set; }
        public DateTime DataAdded { get; set; }
        public bool IsSelected { get; set; } // Dodato polje IsSelected


        public ContentItem()
        {
        }

        public ContentItem(int iD, string title, string description, string imagePath, string rtfFilePath, DateTime dataAdded)
        {
            ID = iD;
            Title = title;
            Description = description;
            ImagePath = imagePath;
            RtfFilePath = rtfFilePath;
            DataAdded = dataAdded;
        }
    }
}
