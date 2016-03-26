﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumericUpDownViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls
{
    using System.ComponentModel;
    using System.IO;
    using Catel;
    using Catel.MVVM;
    using Catel.Services;
    using Catel.Data;

    public class NumericUpDownViewModel : ViewModelBase
    {
        #region Fields
        private double _value = 0;
        #endregion

        #region Constructors
        public NumericUpDownViewModel()
        {
            AllowSpinUp = (Value >= MaximumValue) ? false : true;
            AllowSpinDown = (Value <= MinimumValue) ? false : true;

            Spin = new Command<SpinEventArgs>(OnSpinExecute);
        }
        #endregion

        #region Properties
        public bool AllowSpinDown { get; private set; }
        public bool AllowSpinUp { get; private set; }
        public double DecrementValue { get; set; }
        public double IncrementValue { get; set; }
        public bool IsDecimalAllowed { get; set; }
        public double MinimumValue { get; set; }
        public double MaximumValue { get; set; }
        public bool ShowSpinButtons { get; set; }
        public SpinButtonsLocation SpinButtonsLocation { get; set; }
        public SpinButtonsSize SpinButtonsSize { get; set; }
        public bool SpinOnKeyboardEvents { get; set; }
        public bool SpinOnMouseEvents { get; set; }
        public Command<SpinEventArgs> Spin { get; private set; }

        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                RaisePropertyChanging("Value");

                if (value > MaximumValue)
                {
                    _value = MaximumValue;
                }
                else if (value < MinimumValue)
                {
                    _value = MinimumValue;
                }
                else
                {
                    _value = value;
                }

                RaisePropertyChanged("Value");
            }
        }
        #endregion

        #region Methods
        protected override void OnPropertyChanged(AdvancedPropertyChangedEventArgs e)
        {
            if (e.HasPropertyChanged("MinimumValue") && Value < MinimumValue)
            {
                Value = MinimumValue;
            }
            else if (e.HasPropertyChanged("MaximumValue") && Value > MaximumValue)
            {
                Value = MaximumValue;
            }

            if (e.HasPropertyChanged("MinimumValue") || e.HasPropertyChanged("MaximumValue") || e.HasPropertyChanged("Value"))
            {
                AllowSpinUp = (Value >= MaximumValue) ? false : true;
                AllowSpinDown = (Value <= MinimumValue) ? false : true;
            }

            base.OnPropertyChanged(e);
        }

        private void OnSpinExecute(SpinEventArgs args)
        {
            if (args.Direction == SpinDirection.Up && AllowSpinUp)
            {
                Value += IncrementValue;
            }
            else if (args.Direction == SpinDirection.Down && AllowSpinDown)
            {
                Value -= DecrementValue;
            }
        }
        #endregion
    }
}
