// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumericUpDownViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls.Examples.ViewModels
{
    using System;
    using Catel.Collections;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Threading.Tasks;
    using Catel.MVVM;
    using Catel.Threading;

    public class NumericUpDownViewModel : ViewModelBase
    {
        #region Constructors
        public NumericUpDownViewModel()
        {
            IsDecimalAllowed = true;
            ShowSpinButtons = true;
            SpinButtonsLocation = SpinButtonsLocation.Right;
            SpinButtonsSize = SpinButtonsSize.Normal;
            SpinOnKeyboardEvents = true;
            SpinOnMouseEvents = true;
        }
        #endregion

        #region Properties
        public bool IsDecimalAllowed { get; set; }
        public bool ShowSpinButtons { get; set; }
        public SpinButtonsLocation SpinButtonsLocation { get; set; }
        public SpinButtonsSize SpinButtonsSize { get; set; }
        public bool SpinOnKeyboardEvents { get; set; }
        public bool SpinOnMouseEvents { get; set; }
        #endregion
    }
}
