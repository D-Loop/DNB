using DNB1.materials.cs;
using DNB1.materials.xaml;
using NbrbAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Navigation;

namespace DNB1
{


    public partial class SecondPage : Page
    {
        public ObservableCollection<Rate> lrate = new ObservableCollection<Rate>();
        private readonly MainPage mainPage;
        public FolderBrowserDialog FBD = new FolderBrowserDialog();
        public string nameFile = "";

        public SecondPage(MainPage mPage)
        {
            mainPage = mPage;
            InitializeComponent();
            Loaded += SecondPage_Loaded;
        }



        private async void SecondPage_Loaded(object sender, RoutedEventArgs e)
        {

            SecondGrid.ItemsSource = null;

            (SecondGrid.Columns[0] as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";

            bool StatusFilMonth = true;
            for (DateTime xDate = DateTime.ParseExact(mainPage.From.Text, "dd.MM.yyyy", null); xDate <= DateTime.Now; xDate = xDate.AddDays(1))
            {
                if (xDate.Day == 1)
                    StatusFilMonth = true;

                foreach (var LCurr in mainPage.currencys)
                {
                    var g = LCurr.Cur_Abbreviation;

                    if (LCurr.Cur_Periodicity == 0)
                    {
                        HttpClient Monthhttp = new HttpClient();
                        string request = "https://www.nbrb.by/api/exrates/rates?ondate=" + xDate.Date.ToString("yyyy-MM-dd") + "&periodicity=0";
                        string responseBody = await
                            (await Monthhttp.GetAsync(request)).
                            EnsureSuccessStatusCode().
                            Content.ReadAsStringAsync();
                        JArray jarr = (JArray)JsonConvert.DeserializeObject(responseBody);
                        bool d = false;
                        foreach (JObject item in jarr)
                        {
                            string responseString = "{\"" + (string)JObject.Parse(item.ToString())["Cur_Abbreviation"] + "\":[" + item.ToString() + "]}";
                            Dictionary<string, List<Rate>> itemFromJSON = JsonConvert.DeserializeObject<Dictionary<string, List<Rate>>>
                                                 (responseString, new JsonSerializerSettings { ContractResolver = Rate.Instance });
                            foreach (var KeyValueItemFromJSON in itemFromJSON)
                            {
                                Rate rate = new Rate();
                                foreach (var xKeyValueItemJSON in KeyValueItemFromJSON.Value)
                                {
                                    d = LCurr.Cur_Abbreviation == xKeyValueItemJSON.Cur_Abbreviation;
                                    if (d)
                                    {
                                        rate.Cur_ID = LCurr.Cur_ID;
                                        rate.Date = xKeyValueItemJSON.Date;
                                        rate.Cur_Abbreviation = xKeyValueItemJSON.Cur_Abbreviation;
                                        rate.Cur_Name = xKeyValueItemJSON.Cur_Name;
                                        rate.Cur_OfficialRate = xKeyValueItemJSON.Cur_OfficialRate;
                                    }

                                }
                                if (d)
                                    lrate.Add(rate);
                            }
                        }
                    }
                    else
                    {
                        if (StatusFilMonth)
                        {
                            HttpClient Monthhttp = new HttpClient();
                            string request = "https://www.nbrb.by/api/exrates/rates?ondate=" + xDate.Date.ToString("yyyy-MM-dd") + "&periodicity=1";
                            string responseBody = await
                                (await Monthhttp.GetAsync(request)).
                                EnsureSuccessStatusCode().
                                Content.ReadAsStringAsync();
                            JArray jarr = (JArray)JsonConvert.DeserializeObject(responseBody);
                            bool d = false;
                            foreach (JObject item in jarr)
                            {
                                string responseString = "{\"" + (string)JObject.Parse(item.ToString())["Cur_Abbreviation"] + "\":[" + item.ToString() + "]}";
                                Dictionary<string, List<Rate>> itemFromJSON = JsonConvert.DeserializeObject<Dictionary<string, List<Rate>>>
                                                     (responseString, new JsonSerializerSettings { ContractResolver = Rate.Instance });
                                foreach (var KeyValueItemFromJSON in itemFromJSON)
                                {
                                    Rate rate = new Rate();
                                    foreach (var xKeyValueItemJSON in KeyValueItemFromJSON.Value)
                                    {
                                        d = LCurr.Cur_Abbreviation == xKeyValueItemJSON.Cur_Abbreviation;
                                        if (d)
                                        {
                                            rate.Cur_ID = LCurr.Cur_ID;
                                            rate.Date = xKeyValueItemJSON.Date;
                                            rate.Cur_Abbreviation = xKeyValueItemJSON.Cur_Abbreviation;
                                            rate.Cur_Name = xKeyValueItemJSON.Cur_Name;
                                            rate.Cur_OfficialRate = xKeyValueItemJSON.Cur_OfficialRate;
                                        }

                                    }
                                    if (d)
                                        lrate.Add(rate);
                                }
                            }
                        }
                    }

                }

                StatusFilMonth = false;
                DataContext = new ViewSecondMod(this);

            }

            SecondGrid.ItemsSource = lrate;

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
            FN2 fN = new FN2(this);


            if (FBD.ShowDialog() == DialogResult.OK)

                fN.Show();

        }

        private async void gree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            PointCollection pc = new PointCollection();



            HttpClient Monthhttp = new HttpClient();
            DateTime f = (DateTime)mainPage.To.SelectedDate;
            string request = "https://www.nbrb.by/API/ExRates/Rates/Dynamics/"
                             + ID.Content
                             + "?startDate="
                             + ((DateTime)mainPage.To.SelectedDate).Date.AddYears(-1).ToString("yyyy-MM-dd")
                             + "&endDate="
                             + ((DateTime)mainPage.To.SelectedDate).Date.ToString("yyyy-MM-dd");
            string responseBody = await
                (await Monthhttp.GetAsync(request)).
                EnsureSuccessStatusCode().
                Content.ReadAsStringAsync();
            JArray jarr = (JArray)JsonConvert.DeserializeObject(responseBody);
      

            int k = 0;
                foreach (JObject item in jarr)
                {
                    string responseString = "{\"" + (string)JObject.Parse(item.ToString())["Cur_Abbreviation"] + "\":[" + item.ToString() + "]}";
                    Dictionary<string, List<RateShort>> itemFromJSON = JsonConvert.DeserializeObject<Dictionary<string, List<RateShort>>>
                                         (responseString, new JsonSerializerSettings { ContractResolver = RateShort.Instance });
                    foreach (var KeyValueItemFromJSON in itemFromJSON)
                    {
                        Rate rate = new Rate();
                        foreach (var xKeyValueItemJSON in KeyValueItemFromJSON.Value)
                        {
                                
                                 pc.Add(new Point(+k,+(double)xKeyValueItemJSON.Cur_OfficialRate*100));
                                    k ++;

                        }
                    }
                }


            
        
            
            Graph.Points = pc;



        }
    }
}
