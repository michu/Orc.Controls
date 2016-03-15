// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpinEventArgs.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls
{
    using System.Windows;

    public class SpinEventArgs : RoutedEventArgs
    {
        public SpinEventArgs(SpinDirection direction)
        {
            Direction = direction;
        }

        public SpinDirection Direction
        {
            get;
            set;
        }
    }
}
