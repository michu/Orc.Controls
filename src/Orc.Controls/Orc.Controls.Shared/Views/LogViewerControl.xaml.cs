﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogViewerControl.xaml.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Catel;
    using Catel.Logging;
    using Catel.MVVM.Views;
    using Logging;
    using ViewModels;

    /// <summary>
    /// Interaction logic for LogViewerControl.xaml.
    /// </summary>
    public partial class LogViewerControl
    {
        #region Constants
        private static readonly Dictionary<LogEvent, Brush> ColorSets = new Dictionary<LogEvent, Brush>();
        #endregion

        #region Fields
        private LogViewerViewModel _lastKnownViewModel;
        #endregion

        #region Events
        public event EventHandler<LogEntryDoubleClickEventArgs> LogEntryDoubleClick;
        #endregion

        #region Constructors
        static LogViewerControl()
        {
            typeof (LogViewerControl).AutoDetectViewPropertiesToSubscribe();
        }

        public LogViewerControl()
        {
            InitializeComponent();

            ColorSets[LogEvent.Debug] = Brushes.Gray;
            ColorSets[LogEvent.Info] = Brushes.Black;
            ColorSets[LogEvent.Warning] = Brushes.DarkOrange;
            ColorSets[LogEvent.Error] = Brushes.Red;
        }
        #endregion

        #region Properties
        public bool EnableTimestamp
        {
            get { return (bool) GetValue(EnableTimestampProperty); }
            set { SetValue(EnableTimestampProperty, value); }
        }

        public static readonly DependencyProperty EnableTimestampProperty = DependencyProperty.Register("EnableTimestamp", typeof (bool),
            typeof (LogViewerControl), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (sender, e) => ((LogViewerControl) sender).UpdateControl()));

        public bool EnableIcons
        {
            get { return (bool) GetValue(EnableIconsProperty); }
            set { SetValue(EnableIconsProperty, value); }
        }

        public static readonly DependencyProperty EnableIconsProperty = DependencyProperty.Register("EnableIcons", typeof (bool),
            typeof (LogViewerControl), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (sender, e) => ((LogViewerControl) sender).UpdateControl()));

        public bool EnableTextColoring
        {
            get { return (bool) GetValue(EnableTextColoringProperty); }
            set { SetValue(EnableTextColoringProperty, value); }
        }

        public static readonly DependencyProperty EnableTextColoringProperty = DependencyProperty.Register("EnableTextColoring", typeof (bool),
            typeof (LogViewerControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (sender, e) => ((LogViewerControl) sender).UpdateControl()));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public string LogFilter
        {
            get { return (string) GetValue(LogFilterProperty); }
            set { SetValue(LogFilterProperty, value); }
        }

        public static readonly DependencyProperty LogFilterProperty = DependencyProperty.Register("LogFilter", typeof (string),
            typeof (LogViewerControl), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (sender, e) => ((LogViewerControl) sender).UpdateControl()));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public string TypeFilter
        {
            get { return (string) GetValue(TypeFilterProperty); }
            set { SetValue(TypeFilterProperty, value); }
        }

        public static readonly DependencyProperty TypeFilterProperty = DependencyProperty.Register("TypeFilter", typeof (string),
            typeof (LogViewerControl), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (sender, e) => ((LogViewerControl) sender).UpdateControl()));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public Type LogListenerType
        {
            get { return (Type) GetValue(LogListenerTypeProperty); }
            set { SetValue(LogListenerTypeProperty, value); }
        }

        public static readonly DependencyProperty LogListenerTypeProperty = DependencyProperty.Register("LogListenerType", typeof (Type),
            typeof (LogViewerControl), new FrameworkPropertyMetadata(typeof (LogViewerLogListener), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (sender, e) => ((LogViewerControl) sender).UpdateControl()));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public bool ShowDebug
        {
            get { return (bool) GetValue(ShowDebugProperty); }
            set { SetValue(ShowDebugProperty, value); }
        }

        public static readonly DependencyProperty ShowDebugProperty = DependencyProperty.Register("ShowDebug", typeof (bool),
            typeof (LogViewerControl), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (sender, e) => ((LogViewerControl) sender).UpdateControl()));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public bool ShowInfo
        {
            get { return (bool) GetValue(ShowInfoProperty); }
            set { SetValue(ShowInfoProperty, value); }
        }

        public static readonly DependencyProperty ShowInfoProperty = DependencyProperty.Register("ShowInfo", typeof (bool),
            typeof (LogViewerControl), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (sender, e) => ((LogViewerControl) sender).UpdateControl()));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public bool ShowWarning
        {
            get { return (bool) GetValue(ShowWarningProperty); }
            set { SetValue(ShowWarningProperty, value); }
        }

        public static readonly DependencyProperty ShowWarningProperty = DependencyProperty.Register("ShowWarning", typeof (bool),
            typeof (LogViewerControl), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (sender, e) => ((LogViewerControl) sender).UpdateControl()));

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.TwoWayViewWins)]
        public bool ShowError
        {
            get { return (bool) GetValue(ShowErrorProperty); }
            set { SetValue(ShowErrorProperty, value); }
        }

        public static readonly DependencyProperty ShowErrorProperty = DependencyProperty.Register("ShowError", typeof (bool),
            typeof (LogViewerControl), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (sender, e) => ((LogViewerControl) sender).UpdateControl()));
        #endregion

        #region Methods
        protected override void OnViewModelChanged()
        {
            base.OnViewModelChanged();

            if (_lastKnownViewModel != null)
            {
                _lastKnownViewModel.LogMessage -= OnViewModelLogMessage;
                _lastKnownViewModel = null;
            }

            _lastKnownViewModel = ViewModel as LogViewerViewModel;
            if (_lastKnownViewModel != null)
            {
                _lastKnownViewModel.LogMessage += OnViewModelLogMessage;
            }
        }

        protected override void OnViewModelPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnViewModelPropertyChanged(e);

            if (string.Equals(e.PropertyName, "LogListenerType"))
            {
                UpdateControl();
            }
        }

        private void OnViewModelLogMessage(object sender, LogMessageEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var vm = (LogViewerViewModel) sender;

                var logEntry = new LogEntry(e);
                if (vm.IsValidLogEntry(logEntry))
                {
                    AddLogEntry(logEntry);

                    ScrollToEnd();
                }
            }));
        }

        private void UpdateControl()
        {
            // Using BeginInvoke in order to call properties mapping first. Otherwise filtering by buttons doesen't work.
            // UpdateControl will be called *before* the properties mapping,
            // but because we call BeginInvoke, it will be placed at the end of the execution stack

            Dispatcher.BeginInvoke(new Action(() =>
            {
                ClearScreen();

                var vm = ViewModel as LogViewerViewModel;
                if (vm != null)
                {
                    IEnumerable<LogEntry> logEntries = vm.GetFilteredLogEntries();
                    foreach (LogEntry logEntry in logEntries)
                    {
                        AddLogEntry(logEntry);
                    }
                }

                ScrollToEnd();
            }));
        }

        private void AddLogEntry(LogEntry logEntry)
        {
            var vm = ViewModel as LogViewerViewModel;
            if (vm == null)
            {
                return;
            }

            var paragraph = new RichTextBoxParagraph(logEntry);
            paragraph.MouseLeftButtonDown += (sender, args) =>
            {
                if (args.ClickCount == 2)
                {
                    LogEntryDoubleClick.SafeInvoke(this, new LogEntryDoubleClickEventArgs(logEntry));
                }
            };

            if (EnableIcons)
            {
                var icon = new Label() {DataContext = logEntry};
                paragraph.Inlines.Add(icon);
            }

            if (EnableTextColoring)
            {
                paragraph.Foreground = ColorSets[logEntry.LogEvent];
            }

            paragraph.SetData(EnableTimestamp);

            LogRecordsRichTextBox.Document.Blocks.Add(paragraph);
        }

        private void ScrollToEnd()
        {
            // TODO: only scroll to end if user has not manually scrolled somewhere else, this code is already in Catel somewhere

            LogRecordsRichTextBox.ScrollToEnd();
        }

        private void ClearScreen()
        {
            LogRecordsRichTextBox.Document.Blocks.Clear();
        }

        public void Clear()
        {
            var vm = ViewModel as LogViewerViewModel;
            if (vm != null)
            {
                vm.ClearEntries();
            }

            ClearScreen();
        }

        public void CopyToClipboard()
        {
            var text = LogRecordsRichTextBox.GetInlineText();
            Clipboard.SetText(text);
        }
        #endregion
    }
}