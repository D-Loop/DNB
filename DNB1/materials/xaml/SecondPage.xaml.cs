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
using NbrbAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Reflection;
using System.Net.Http;

namespace DNB1
{

    public class Rate
    {
        [JsonProperty("Cur_ID")]
        public int Cur_ID { get; set; }
        [JsonProperty("Date")]
        public DateTime Date { get; set; }
        [JsonProperty("Cur_Abbreviation")]
        public string Cur_Abbreviation { get; set; }
        [JsonProperty("Cur_Scale")]
        public int Cur_Scale { get; set; }
        [JsonProperty("Cur_Name")]
        public string Cur_Name { get; set; }
        [JsonProperty("Cur_OfficialRate")]
        public decimal? Cur_OfficialRate { get; set; }
    }

 

    public partial class SecondPage : Page
    {
        private List<Currency> currencys = new List<Currency>();
        public SecondPage()
        {
            InitializeComponent();
            Loaded += SecondPage_Loaded;
        }

        private void SecondPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new Currency();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());

        }

        public static bool IsInRange(DateTime dateToCheck, DateTime dateToCheck2, DateTime startDate, DateTime endDate)
        {
            return dateToCheck <= startDate && dateToCheck2 >= endDate;
        }

        private void UploadToExcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
