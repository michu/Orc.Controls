// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Spinner.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls
{
    using Catel;
    using System;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Content")]
    public class Spinner : Control
    {
        #region Constructors
        public Spinner()
        {
            DefaultStyleKey = typeof(Spinner);
        }
        #endregion

        #region Events
        public event EventHandler<SpinEventArgs> Spin;
        #endregion

        #region Properties
        public Brush AccentColorBrush
        {
            get { return (Brush)GetValue(AccentColorBrushProperty); }
            set { SetValue(AccentColorBrushProperty, value); }
        }

        public static readonly DependencyProperty AccentColorBrushProperty = DependencyProperty.Register("AccentColorBrush", typeof(Brush),
            typeof(Spinner), new FrameworkPropertyMetadata(Brushes.LightGray, (sender, e) => ((Spinner)sender).OnAccentColorBrushChanged()));

        public bool AllowSpinDown
        {
            get { return (bool)GetValue(AllowSpinDownProperty); }
            set { SetValue(AllowSpinDownProperty, value); }
        }

        public static readonly DependencyProperty AllowSpinDownProperty = DependencyProperty.Register("AllowSpinDown", typeof(bool),
            typeof(Spinner), new PropertyMetadata(true));

        public bool AllowSpinUp
        {
            get { return (bool)GetValue(AllowSpinUpProperty); }
            set { SetValue(AllowSpinUpProperty, value); }
        }

        public static readonly DependencyProperty AllowSpinUpProperty = DependencyProperty.Register("AllowSpinUp", typeof(bool),
            typeof(Spinner), new PropertyMetadata(true));

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object),
            typeof(Spinner), new PropertyMetadata(null));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool),
            typeof(Spinner), new PropertyMetadata(false));

        public bool SpinOnKeyboardEvents
        {
            get { return (bool)GetValue(SpinOnKeyboardEventsProperty); }
            set { SetValue(SpinOnKeyboardEventsProperty, value); }
        }

        public static readonly DependencyProperty SpinOnKeyboardEventsProperty = DependencyProperty.Register("SpinOnKeyboardEvents", typeof(bool),
            typeof(Spinner), new PropertyMetadata(true));

        public bool SpinOnMouseEvents
        {
            get { return (bool)GetValue(SpinOnMouseEventsProperty); }
            set { SetValue(SpinOnMouseEventsProperty, value); }
        }

        public static readonly DependencyProperty SpinOnMouseEventsProperty = DependencyProperty.Register("SpinOnMouseEvents", typeof(bool),
            typeof(Spinner), new PropertyMetadata(true));

        public bool ShowSpinButtons
        {
            get { return (bool)GetValue(ShowSpinButtonsProperty); }
            set { SetValue(ShowSpinButtonsProperty, value); }
        }

        public static readonly DependencyProperty ShowSpinButtonsProperty = DependencyProperty.Register("ShowSpinButtons", typeof(bool),
            typeof(Spinner), new PropertyMetadata(true));

        public SpinButtonsLocation SpinButtonsLocation
        {
            get { return (SpinButtonsLocation)GetValue(SpinButtonsLocationProperty); }
            set { SetValue(SpinButtonsLocationProperty, value); }
        }

        public static readonly DependencyProperty SpinButtonsLocationProperty = DependencyProperty.Register("SpinButtonsLocation", typeof(SpinButtonsLocation),
            typeof(Spinner), new PropertyMetadata(SpinButtonsLocation.Right));

        public SpinButtonsSize SpinButtonsSize
        {
            get { return (SpinButtonsSize)GetValue(SpinButtonsSizeProperty); }
            set { SetValue(SpinButtonsSizeProperty, value); }
        }

        public static readonly DependencyProperty SpinButtonsSizeProperty = DependencyProperty.Register("SpinButtonsSize", typeof(SpinButtonsSize),
            typeof(Spinner), new PropertyMetadata(SpinButtonsSize.Normal));

        private RepeatButton SpinDownLargeButton { get; set; }
        private RepeatButton SpinDownNormalButton { get; set; }
        private RepeatButton SpinUpLargeButton { get; set; }
        private RepeatButton SpinUpNormalButton { get; set; }

        #endregion

        #region Methods
        private void OnAccentColorBrushChanged()
        {
            var solidColorBrush = AccentColorBrush as SolidColorBrush;
            if (solidColorBrush != null)
            {
                var accentColor = ((SolidColorBrush)AccentColorBrush).Color;
                accentColor.CreateAccentColorResourceDictionary("SpinButton");
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            AccentColorBrush = TryFindResource("AccentColorBrush") as SolidColorBrush;

            SpinDownLargeButton = GetTemplateChild("SpinDownLargeButton") as RepeatButton;
            SpinDownNormalButton = GetTemplateChild("SpinDownNormalButton") as RepeatButton;
            SpinUpLargeButton = GetTemplateChild("SpinUpLargeButton") as RepeatButton;
            SpinUpNormalButton = GetTemplateChild("SpinUpNormalButton") as RepeatButton;

            SubscribeSpinButtons();
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);

            if (SpinOnMouseEvents && !IsReadOnly)
            {
                if (e.Delta > 0 && AllowSpinUp)
                {
                    Spin.SafeInvoke<SpinEventArgs>(this, new SpinEventArgs(SpinDirection.Up));

                    e.Handled = true;
                }
                else if (e.Delta < 0 && AllowSpinDown)
                {
                    Spin.SafeInvoke<SpinEventArgs>(this, new SpinEventArgs(SpinDirection.Down));

                    e.Handled = true;
                }
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (SpinOnKeyboardEvents && !IsReadOnly)
            {
                if (e.Key == Key.Up && AllowSpinUp)
                {
                    Spin.SafeInvoke<SpinEventArgs>(this, new SpinEventArgs(SpinDirection.Up));

                    e.Handled = true;
                }
                else if (e.Key == Key.Down && AllowSpinDown)
                {
                    Spin.SafeInvoke<SpinEventArgs>(this, new SpinEventArgs(SpinDirection.Down));

                    e.Handled = true;
                }
            }

            base.OnPreviewKeyDown(e);
        }

        private void SubscribeSpinButtons()
        {
            SpinDownLargeButton.Click -= SpinDownButton_Click;
            SpinDownNormalButton.Click -= SpinDownButton_Click;
            SpinDownLargeButton.Click += SpinDownButton_Click;
            SpinDownNormalButton.Click += SpinDownButton_Click;

            SpinUpLargeButton.Click -= SpinUpButton_Click;
            SpinUpNormalButton.Click -= SpinUpButton_Click;
            SpinUpLargeButton.Click += SpinUpButton_Click;
            SpinUpNormalButton.Click += SpinUpButton_Click;
        }

        private void SpinDownButton_Click(object sender, RoutedEventArgs e)
        {
            Spin.SafeInvoke<SpinEventArgs>(this, new SpinEventArgs(SpinDirection.Down));
        }

        private void SpinUpButton_Click(object sender, RoutedEventArgs e)
        {
            Spin.SafeInvoke<SpinEventArgs>(this, new SpinEventArgs(SpinDirection.Up));
        }
        #endregion
    }
}
