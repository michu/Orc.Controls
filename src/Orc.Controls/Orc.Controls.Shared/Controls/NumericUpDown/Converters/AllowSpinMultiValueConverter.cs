// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AllowSpinMultiValueConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls.Converters
{
    using System;
    using System.Windows.Data;
    using System.Globalization;
    using System.Windows.Markup;

    internal class AllowSpinMultiValueConverter : MarkupExtension, IMultiValueConverter
    {
        public AllowSpinMultiValueConverter()
        {

        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 && values[0] is bool && values[1] is bool)
            {
                bool isReadOnly = (bool)values[0];
                bool allowSpin = (bool)values[1];

                return !isReadOnly && allowSpin;
            }

            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
