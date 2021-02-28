using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using NbrbAPI.Models;
using System.Collections.ObjectModel;

namespace DNB1.materials.cs
{
    class ViewSecondMod : DependencyObject, INotifyPropertyChanged
    {

        public string rateFilter
        {
            get { return (string)GetValue(rateFilterProperty); }
            set { SetValue(rateFilterProperty, value); }
        }

        public static readonly DependencyProperty rateFilterProperty =
            DependencyProperty.Register("ValueFilter", typeof(string), typeof(ViewSecondMod), new PropertyMetadata("", FiltersChanged));

        private static void FiltersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cur = d as ViewSecondMod;
            if (cur != null)
            {
                cur.Items.Filter = null;
                cur.Items.Filter = cur.FilterRate;

            }

        }

        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ViewSecondMod), new PropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;

        private Rate _SelectedItem;

        public Rate SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                
                _SelectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }


        public ViewSecondMod(SecondPage MW)
        {
            Items = CollectionViewSource.GetDefaultView(MW.lrate);
            Items.Filter = FilterRate;

        }


        private bool FilterRate(object obj)
        {

            Rate rat = obj as Rate;
            bool res = true;
            if (rat != null
                && !(rat.Cur_OfficialRate).ToString().Contains(rateFilter)
                && !rat.Date.ToString().Contains(rateFilter)
                && !rat.Cur_Name.Contains(rateFilter)
                && !rat.Cur_Abbreviation.Contains(rateFilter))
            {
                res = false;

                return res;
            }

            return res;
        }
    }
}
