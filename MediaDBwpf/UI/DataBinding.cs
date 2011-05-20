using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MediaDBwpf.UI
{
    [ValueConversion(typeof(Image), typeof(BitmapImage))]
    public class ImgtoImg : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage X = (BitmapImage)value;
            return X;
            //string s = (string)value;
            //return s.Substring(s.LastIndexOf("\\") + 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Image s = (Image)value;
            return s;
            //string strValue = value as string;
            //return DependencyProperty.UnsetValue;
        }
    }

    [ValueConversion(typeof(string), typeof(String))]
    public class FilepathToName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = (string)value;
            return s.Substring(s.LastIndexOf("\\") + 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            return DependencyProperty.UnsetValue;
        }
    }
    public class PlainView : ViewBase
    {

        public static readonly DependencyProperty
          ItemContainerStyleProperty =
          ItemsControl.ItemContainerStyleProperty.AddOwner(typeof(PlainView));

        public Style ItemContainerStyle
        {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            ItemsControl.ItemTemplateProperty.AddOwner(typeof(PlainView));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemWidthProperty =
            WrapPanel.ItemWidthProperty.AddOwner(typeof(PlainView));

        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }


        public static readonly DependencyProperty ItemHeightProperty =
            WrapPanel.ItemHeightProperty.AddOwner(typeof(PlainView));

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }


        protected override object DefaultStyleKey
        {
            get
            {
                return new ComponentResourceKey(GetType(), "myPlainViewDSK");
            }
        }

    }
}