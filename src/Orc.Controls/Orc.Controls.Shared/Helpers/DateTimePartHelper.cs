﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimePartHelper.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using Catel.IoC;
    using Services;

    public class DateTimePartHelper
    {
        #region Fields
        private readonly DateTime _dateTime;
        private readonly DateTimePart _dateTimePart;
        private readonly NumericTextBox _textBox;
        private readonly ToggleButton _toggleButton;
        #endregion

        #region Constructors
        public DateTimePartHelper(DateTime dateTime, DateTimePart dateTimePart, NumericTextBox textBox, ToggleButton activeToggleButton)
        {
            _dateTime = dateTime;
            _textBox = textBox;
            _toggleButton = activeToggleButton;
            _dateTimePart = dateTimePart;
        }
        #endregion

        #region Methods
        public Popup CreatePopup()
        {
            var popup = new Popup
            {
                MinWidth = _textBox.ActualWidth + 25,
                MaxHeight = 100,
                PlacementTarget = _textBox,
                Placement = PlacementMode.Bottom,
                VerticalOffset = 2,
                IsOpen = true,
                StaysOpen = false,
            };

            popup.Closed += PopupOnClosed;

            var popupSource = CreatePopupSource();
            popupSource.PreviewKeyDown += popupSource_PreviewKeyDown;
            popupSource.MouseUp += PopupSourceOnMouseUp;

            popup.Child = popupSource;
            SelectItem(popupSource);
            popupSource.Focus();
            
            return popup;
        }

        private void PopupSourceOnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var listbox = ((ListBox)sender);
            UpdateTextBox((KeyValuePair<string, string>)listbox.SelectedItems[0]);

            ((Popup)listbox.Parent).IsOpen = false;
            _textBox.Focus();
        }

        private void popupSource_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var listbox = ((ListBox) sender);
            if (e.Key == Key.Down)
            {
                if (listbox.SelectedIndex < listbox.Items.Count)
                {
                    listbox.SelectedIndex++;
                    listbox.ScrollIntoView(listbox.SelectedItem);
                    e.Handled = true;
                }
            }
            if (e.Key == Key.Up)
            {
                if (listbox.SelectedIndex > 0)
                {
                    listbox.SelectedIndex--;
                    listbox.ScrollIntoView(listbox.SelectedItem);
                    e.Handled = true;
                }
            }
            if (e.Key == Key.Escape)
            {
                ((Popup) listbox.Parent).IsOpen = false;
                _textBox.Focus();
                e.Handled = true;
            }
            if (e.Key == Key.Enter)
            {
                UpdateTextBox((KeyValuePair<string, string>)listbox.SelectedItems[0]);
                ((Popup) listbox.Parent).IsOpen = false;
                _textBox.Focus();
                e.Handled = true;
            }
        }

        private void SelectItem(ListBox listBox)
        {
            foreach (var item in listBox.Items)
            {
                if ((((KeyValuePair<string, string>)item).Key) == _textBox.Text)
                {
                    listBox.SelectedItem = item;
                    listBox.ScrollIntoView(listBox.SelectedItem);
                    listBox.Focus();

                    break;
                }
            }
        }

        private void PopupOnClosed(object sender, EventArgs eventArgs)
        {
            _toggleButton.IsChecked = false;
        }

        private void UpdateTextBox(KeyValuePair<string, string> selectedItem)
        {
            var value = Convert.ToDouble(selectedItem.Key);
            _textBox.Value = value;
        }

        private ListBox CreatePopupSource()
        {
            var serviceLocator = ServiceLocator.Default;
            var suggestionListService = serviceLocator.ResolveType<ISuggestionListService>();
            var source = suggestionListService.GetSuggestionList(_dateTime, _dateTimePart);

            var listbox = new ListBox()
            {
                ItemsSource = source,
                IsSynchronizedWithCurrentItem = false,
                DisplayMemberPath = "Value",
                Margin = new Thickness(0, 0, 0, 0),
            };
            return listbox;
        }
        #endregion
    }
}