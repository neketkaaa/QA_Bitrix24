using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ATframework3demo.TestEntities
{
    public class House
    {
        public House(string title, string info, string pathID, int numberOfApartments, string adress)
        {
            Title = title;
            Info = info;
            PathID = pathID;
            NumberOfApartments = numberOfApartments;
            Adress = adress;
        }
        public string Title { get; set; }
        public string Info { get; set; }
        public string PathID { get; set; }
        public int NumberOfApartments { get; set; }
        public string Adress { get; set; }
    }
}