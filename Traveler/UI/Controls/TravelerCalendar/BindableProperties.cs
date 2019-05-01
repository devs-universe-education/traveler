﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Traveler.DAL.DataObjects;
using Xamarin.Forms;

namespace Traveler.UI.Controls.TravelerCalendar
{
    partial class TravelerCalendar : Grid
    {
        public int Year
        {
            get => (int)GetValue(YearProperty);
            set => SetValue(YearProperty, value);
        }

        public static readonly BindableProperty YearProperty =
            BindableProperty.Create(nameof(Year), typeof(int), typeof(TravelerCalendar), propertyChanged: OnYearPropertyChanged);

        private static void OnYearPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var context = (TravelerCalendar)bindable;
            if (context != null)
            {
                context.Year = (int)newValue;
                context.Redraw();
            }
        }

        public int Month
        {
            get => (int)GetValue(MonthProperty);
            set => SetValue(MonthProperty, value);
        }

        public static readonly BindableProperty MonthProperty =
            BindableProperty.Create(nameof(Month), typeof(int), typeof(TravelerCalendar), propertyChanged: OnMonthPropertyChanged);

        private static void OnMonthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var context = (TravelerCalendar)bindable;
            if (context != null)
            {
                context.Month = (int)newValue;
                context.Redraw();
            }
        }

        public IEnumerable<Travel> Travels
        {
            get => (IEnumerable<Travel>)GetValue(TravelsProperty);
            set => SetValue(TravelsProperty, value);
        }

        public static readonly BindableProperty TravelsProperty =
            BindableProperty.Create(nameof(Travels), typeof(IEnumerable<Travel>), typeof(TravelerCalendar), propertyChanged: OnTravelsPropertyChanged);

        private static void OnTravelsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var context = (TravelerCalendar)bindable;
            if (context != null)
            {
                context.Travels = (IEnumerable<Travel>)newValue;
                context.Redraw();
            }
        }

        public ICommand DayClicked
        {
            get => (ICommand)GetValue(DayClickedProperty);
            set => SetValue(DayClickedProperty, value);
        }

        public static readonly BindableProperty DayClickedProperty =
            BindableProperty.Create(nameof(DayClicked), typeof(ICommand), typeof(TravelerCalendar), propertyChanged: OnDayClickedPropertyChanged);

        private static void OnDayClickedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var context = (TravelerCalendar)bindable;
            if (context != null)
            {
                context.DayClicked = (ICommand)newValue;
                context.Redraw();
            }
        }
    }
}
