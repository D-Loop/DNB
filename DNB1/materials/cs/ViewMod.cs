using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls;

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
            DependencyProperty.Register("ValueFilter", typeof(string), typeof(ViewMod), new PropertyMetadata(""));


        public ICollectionView Item
        {
            get { return (ICollectionView)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(ICollectionView), typeof(ViewMod), new PropertyMetadata(null));


        ViewMod(MainWindow MW )
        {
            Item = (ICollectionView)MW.gree.ItemsSource;

        }
    }
}
