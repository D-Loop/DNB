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

namespace DNB1.materials.cs
{
    class ViewMod : DependencyObject
    {

        public string ValueFilter
        {
            get { return (string)GetValue(ValueFilterProperty); }
            set { SetValue(ValueFilterProperty, value); }
        }

        public static readonly DependencyProperty ValueFilterProperty =
            DependencyProperty.Register("ValueFilter", typeof(string), typeof(ViewMod), new PropertyMetadata("",FilterChanged));

        private static void FilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cur = d as ViewMod;
            if(cur!=null)
            {
                cur.Item.Filter = null;
                cur.Item.Filter = cur.FilterCurrency;

            }

        }

        public ICollectionView Item
        {
            get { return (ICollectionView)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(ICollectionView), typeof(ViewMod), new PropertyMetadata(null));


        public ViewMod(MainPage MW)
        {
            Item = CollectionViewSource.GetDefaultView(MW.currencys);
            Item.Filter = FilterCurrency;

        }



        private bool FilterCurrency(object obj)
        {
            
            Currency currency = obj as Currency;
            bool res = true;
            if (currency != null && !currency.Cur_Name_Eng.Contains(ValueFilter) && 
                    !currency.Cur_Code.Contains(ValueFilter) && !currency.Cur_Name.Contains(ValueFilter) &&
                        !currency.Cur_PeriodicityString.Contains(ValueFilter) && !currency.Cur_Abbreviation.Contains(ValueFilter))
                res = false;

            return res;
        }
    }
}
