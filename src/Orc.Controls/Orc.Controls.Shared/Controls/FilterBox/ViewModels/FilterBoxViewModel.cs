﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterBoxViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls.ViewModels
{
    using System.Collections;
    using Catel.MVVM;

    public class FilterBoxViewModel : ViewModelBase
    {
        private IEnumerable _filterSource;
        private string _propertyName;
        private string _text;
        public FilterBoxViewModel()
        {
            ClearFilter = new Command(OnClearFilterExecute, OnClearFilterCanExecute);
        }

        public string Watermark { get; set; }

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                {
                    return;
                }

                _text = value;

                // Required for mappings
                RaisePropertyChanged("Text");
            }
        }

        public IEnumerable FilterSource
        {
            get { return _filterSource; }
            set
            {
                if (_filterSource == value)
                {
                    return;
                }

                _filterSource = value;

                // Required for mappings
                RaisePropertyChanged("FilterSource");
            }
        }

        public string PropertyName
        {
            get { return _propertyName; }
            set
            {
                if (_propertyName == value)
                {
                    return;
                }

                _propertyName = value;

                // Required for mappings
                RaisePropertyChanged("PropertyName");
            }
        }

        public Command ClearFilter { get; private set; }

        private bool OnClearFilterCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Text);
        }

        private void OnClearFilterExecute()
        {
            Text = null;
        }
    }
}