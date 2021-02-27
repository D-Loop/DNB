using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
//using DNB1.materials.cs;
using NbrbAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace DNB1
{

    class Currency
    {

        [JsonProperty("Cur_Code")]
        public string Cur_Code { get; set; }
        [JsonProperty("Cur_Abbreviation")]
        public string Cur_Abbreviation { get; set; }
        [JsonProperty("Cur_Name")]
        public string Cur_Name { get; set; }
        [JsonProperty("Cur_Name_Eng")]
        public string Cur_Name_Eng { get; set; }
        [JsonProperty("Cur_Periodicity")]
        public int Cur_Periodicity { get; set; }
        [JsonProperty("Cur_DateStart")]
        public DateTime Cur_DateStart { get; set; }
        [JsonProperty("Cur_DateEnd")]
        public DateTime Cur_DateEnd { get; set; }
        


    }

    class Order : DefaultContractResolver
    {
        public static readonly Order Instance =
          new Order();

        protected override JsonProperty CreateProperty(MemberInfo member,
           MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            return property;
        }
    }



    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Currency> currencys = new List<Currency>();
        private List<Currency> currencys1 = new List<Currency>();


        public MainWindow()
        {
            InitializeComponent();
            Loaded += MWindow_Loaded;
       
        }

        private void MWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new Currency();

            From.BlackoutDates.Add(new CalendarDateRange(new DateTime(2050, 12, 12), new DateTime(3999, 1, 1)));
            From.BlackoutDates.Add(new CalendarDateRange(new DateTime(1000, 12, 12), new DateTime(1995, 1, 1)));
            To.BlackoutDates.Add(new CalendarDateRange(new DateTime(2050, 12, 12), new DateTime(3999, 1, 1)));
            To.BlackoutDates.Add(new CalendarDateRange(new DateTime(1000, 12, 12), new DateTime(1995, 1, 1)));

            From.Text = DateTime.Now.ToString();
            To.Text  = DateTime.Now.ToString();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void fuulscreen_Click(object sender, RoutedEventArgs e)
        {
            if (MaterialDesignThemes.Wpf.PackIconKind.FullscreenExit != fspi.Kind)
            {
                Launch.WindowState = WindowState.Maximized;
                fspi.Kind = MaterialDesignThemes.Wpf.PackIconKind.FullscreenExit;
            }
            else
            {
                Launch.WindowState = WindowState.Normal;
                fspi.Kind = MaterialDesignThemes.Wpf.PackIconKind.Fullscreen;

            }
        }

        private void hide_Click(object sender, RoutedEventArgs e)
        {
            Launch.WindowState = WindowState.Minimized;
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private async void GetTheRate_Click(object sender, RoutedEventArgs e)
        {
            currencys = currencys1;
            HttpClient httpClient = new HttpClient();
            string request = "https://www.nbrb.by/api/exrates/currencies";
            string responseBody = await
                (await httpClient.GetAsync(request)).
                EnsureSuccessStatusCode().
                Content.ReadAsStringAsync();
           
            Dictionary<string, List<Currency>> PairList =
                new Dictionary<string, List<Currency>>();
            JArray jObject = (JArray)JsonConvert.DeserializeObject(responseBody);
            bool k=false;
            foreach (JObject item in jObject)
            {
                //datepicker как abbr =====================================================>\/
                string responseString = "{\"" + (string)JObject.Parse(item.ToString())["Cur_Abbreviation"] + "\":[" + item.ToString() + "]}";
                PairList = JsonConvert.DeserializeObject<Dictionary<string, List<Currency>>>
                                              (responseString, new JsonSerializerSettings { ContractResolver = Order.Instance });
                foreach (var order1 in PairList)
                { 
                     Currency currency = new Currency();
                        foreach (var order in order1.Value)
                        {
                         k = IsInRange(order.Cur_DateStart, order.Cur_DateEnd, DateTime.ParseExact(From.Text, "dd.MM.yyyy", null), DateTime.ParseExact(To.Text, "dd.MM.yyyy", null));
                            if (k)
                            {
                                currency.Cur_Code = order.Cur_Code;
                                currency.Cur_Abbreviation = order.Cur_Abbreviation;
                                currency.Cur_Name = order.Cur_Name;
                                currency.Cur_Name_Eng = order.Cur_Name_Eng;
                                currency.Cur_Periodicity = order.Cur_Periodicity;
                            }
                        }
                        if(k)
                     currencys.Add(currency);
                }
            }
            gree.ItemsSource = currencys;
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
