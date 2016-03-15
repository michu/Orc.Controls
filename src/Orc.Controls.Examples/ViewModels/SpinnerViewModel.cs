// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpinnerViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls.Examples.ViewModels
{
    using Catel.MVVM;

    public class SpinnerViewModel : ViewModelBase
    {
        #region Constructors
        public SpinnerViewModel()
        {
            ShowSpinButtons = true;
            SpinOnKeyboardEvents = true;
            Spin = new Command<SpinEventArgs>(OnSpinExecute);
        }
        #endregion

        #region Properties
        public bool ShowSpinButtons { get; set; }
        public bool SpinOnKeyboardEvents { get; set; }
        public Command<SpinEventArgs> Spin { get; private set; }
        public int Value { get; set; }
        #endregion

        #region Methods
        private void OnSpinExecute(SpinEventArgs args)
        {
            if(args.Direction == SpinDirection.Up)
            {
                Value++;
            }
            else
            {
                Value--;
            }
        }
        #endregion
    }
}
