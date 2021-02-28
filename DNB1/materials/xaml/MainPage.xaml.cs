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
using DNB1.materials.cs;
using System.Collections.ObjectModel;

namespace DNB1
{

    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public ObservableCollection<Currency> currencys = new ObservableCollection<Currency>();
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

            From.BlackoutDates.Add(new CalendarDateRange(new DateTime(2050, 12, 12), new DateTime(3999, 1, 1)));
            From.BlackoutDates.Add(new CalendarDateRange(new DateTime(1000, 12, 12), new DateTime(1995, 1, 1)));
            To.BlackoutDates.Add(new CalendarDateRange(new DateTime(2050, 12, 12), new DateTime(3999, 1, 1)));
            To.BlackoutDates.Add(new CalendarDateRange(new DateTime(1000, 12, 12), new DateTime(1995, 1, 1)));

            From.Text = DateTime.Now.ToString();
            To.Text = DateTime.Now.ToString();
        }

        private async void GetTheRate_Click(object sender, RoutedEventArgs e)
        {
            MoreInfo.Visibility = Visibility.Visible;
            Filt.Visibility = Visibility.Visible;
            search.Visibility = Visibility.Visible;
            gree.ItemsSource = null;
            if (IsInRange(DateTime.ParseExact(From.Text, "dd.MM.yyyy", null), DateTime.ParseExact(To.Text, "dd.MM.yyyy", null), DateTime.ParseExact(To.Text, "dd.MM.yyyy", null), DateTime.ParseExact(From.Text, "dd.MM.yyyy", null)))
            {
                currencys.Clear();
                HttpClient httpClient = new HttpClient();
                string request = "https://www.nbrb.by/api/exrates/currencies";
                string responseBody = await
                    (await httpClient.GetAsync(request)).
                    EnsureSuccessStatusCode().
                    Content.ReadAsStringAsync();

                Dictionary<string, List<Currency>> PairList =
                    new Dictionary<string, List<Currency>>();
                JArray jObject = (JArray)JsonConvert.DeserializeObject(responseBody);
                bool k = false;
                foreach (JObject item in jObject)
                {
                    string responseString = "{\"" + (string)JObject.Parse(item.ToString())["Cur_Abbreviation"] + "\":[" + item.ToString() + "]}";
                    PairList = JsonConvert.DeserializeObject<Dictionary<string, List<Currency>>>
                                                  (responseString, new JsonSerializerSettings { ContractResolver = Currency.Instance });
                    foreach (var order1 in PairList)
                    {
                        Currency currency = new Currency();
                        foreach (var order in order1.Value)
                        {
                            k = IsInRange(order.Cur_DateStart, order.Cur_DateEnd, DateTime.ParseExact(From.Text, "dd.MM.yyyy", null), DateTime.ParseExact(To.Text, "dd.MM.yyyy", null));
                            if (k)
                            {

                                currency.Cur_ID = order.Cur_ID;
                                currency.Cur_Code = order.Cur_Code;
                                currency.Cur_Abbreviation = order.Cur_Abbreviation;
                                currency.Cur_Name = order.Cur_Name;
                                currency.Cur_Name_Eng = order.Cur_Name_Eng;
                                currency.Cur_Periodicity = order.Cur_Periodicity;
                                if(order.Cur_Periodicity==0)
                                    currency.Cur_PeriodicityString = "Ежедневно";
                                else
                                    currency.Cur_PeriodicityString = "Ежемесячно";
                            }
                        }
                        if (k)
                            currencys.Add(currency);
                    }
                }
                gree.ItemsSource = currencys;
                DataContext = new ViewMod(this);
            }

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

        private void MoreInfo_F(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new SecondPage(this));

        }
    }
}
