// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumericUpDown.xaml.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls
{
    using System.Collections;
    using System.Windows;
    using System.Windows.Media;
    using Catel.MVVM.Views;
    using System.Windows.Controls;
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown
    {
        #region Constructors
        static NumericUpDown()
        {
            typeof(NumericUpDown).AutoDetectViewPropertiesToSubscribe();
        }

        public NumericUpDown()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public double DecrementValue
        {
            get { return (double)GetValue(DecrementValueProperty); }
            set { SetValue(DecrementValueProperty, value); }
        }

        public static readonly DependencyProperty DecrementValueProperty = DependencyProperty.Register("DecrementValue", typeof(double),
            typeof(NumericUpDown), new PropertyMetadata(1D));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public double IncrementValue
        {
            get { return (double)GetValue(IncrementValueProperty); }
            set { SetValue(IncrementValueProperty, value); }
        }

        public static readonly DependencyProperty IncrementValueProperty = DependencyProperty.Register("IncrementValue", typeof(double),
            typeof(NumericUpDown), new PropertyMetadata(1D));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public bool IsDecimalAllowed
        {
            get { return (bool)GetValue(IsDecimalAllowedProperty); }
            set { SetValue(IsDecimalAllowedProperty, value); }
        }

        public static readonly DependencyProperty IsDecimalAllowedProperty = DependencyProperty.Register("IsDecimalAllowed", typeof(bool),
            typeof(NumericUpDown), new PropertyMetadata(true));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool),
            typeof(NumericUpDown), new PropertyMetadata(false, (sender, e) => ((NumericUpDown)sender).OnIsReadOnlyChanged()));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public double MinimumValue
        {
            get { return (double)GetValue(MinimumValueProperty); }
            set { SetValue(MinimumValueProperty, value); }
        }

        public static readonly DependencyProperty MinimumValueProperty = DependencyProperty.Register("MinimumValue", typeof(double),
            typeof(NumericUpDown), new PropertyMetadata(double.MinValue));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public double MaximumValue
        {
            get { return (double)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }

        public static readonly DependencyProperty MaximumValueProperty = DependencyProperty.Register("MaximumValue", typeof(double),
            typeof(NumericUpDown), new PropertyMetadata(double.MaxValue));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public bool ShowSpinButtons
        {
            get { return (bool)GetValue(ShowSpinButtonsProperty); }
            set { SetValue(ShowSpinButtonsProperty, value); }
        }

        public static readonly DependencyProperty ShowSpinButtonsProperty = DependencyProperty.Register("ShowSpinButtons", typeof(bool),
            typeof(NumericUpDown), new PropertyMetadata(true));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public SpinButtonsLocation SpinButtonsLocation
        {
            get { return (SpinButtonsLocation)GetValue(SpinButtonsLocationProperty); }
            set { SetValue(SpinButtonsLocationProperty, value); }
        }

        public static readonly DependencyProperty SpinButtonsLocationProperty = DependencyProperty.Register("SpinButtonsLocation", typeof(SpinButtonsLocation),
            typeof(NumericUpDown), new PropertyMetadata(SpinButtonsLocation.Right));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public SpinButtonsSize SpinButtonsSize
        {
            get { return (SpinButtonsSize)GetValue(SpinButtonsSizeProperty); }
            set { SetValue(SpinButtonsSizeProperty, value); }
        }

        public static readonly DependencyProperty SpinButtonsSizeProperty = DependencyProperty.Register("SpinButtonsSize", typeof(SpinButtonsSize),
            typeof(NumericUpDown), new PropertyMetadata(SpinButtonsSize.Normal));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public bool SpinOnKeyboardEvents
        {
            get { return (bool)GetValue(SpinOnKeyboardEventsProperty); }
            set { SetValue(SpinOnKeyboardEventsProperty, value); }
        }

        public static readonly DependencyProperty SpinOnKeyboardEventsProperty = DependencyProperty.Register("SpinOnKeyboardEvents", typeof(bool),
            typeof(NumericUpDown), new PropertyMetadata(true));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public bool SpinOnMouseEvents
        {
            get { return (bool)GetValue(SpinOnMouseEventsProperty); }
            set { SetValue(SpinOnMouseEventsProperty, value); }
        }

        public static readonly DependencyProperty SpinOnMouseEventsProperty = DependencyProperty.Register("SpinOnMouseEvents", typeof(bool),
            typeof(NumericUpDown), new PropertyMetadata(true));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double),
            typeof(NumericUpDown), new PropertyMetadata(0D));
        #endregion

        #region Methods
        public void OnIsReadOnlyChanged()
        {
            TextBox textBoxValue = FindName("TextBoxValue") as TextBox;
            if (textBoxValue != null)
            {
                textBoxValue.IsReadOnly = IsReadOnly;
            }
        }
        #endregion
    }
}
