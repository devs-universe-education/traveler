using System;
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

        public IEnumerable<TravelDataObject> Travels
        {
            get => (IEnumerable<TravelDataObject>)GetValue(TravelsProperty);
            set => SetValue(TravelsProperty, value);
        }

        public static readonly BindableProperty TravelsProperty =
            BindableProperty.Create(nameof(Travels), typeof(IEnumerable<TravelDataObject>), typeof(TravelerCalendar), propertyChanged: OnTravelsPropertyChanged);

        private static void OnTravelsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var context = (TravelerCalendar)bindable;
            if (context != null)
            {
                context.Travels = (IEnumerable<TravelDataObject>)newValue;
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

        public ValueTuple<int, int> NewTravelDays
        {
            get => (ValueTuple<int, int>)GetValue(NewTravelDaysProperty);
            set => SetValue(NewTravelDaysProperty, value);
        }

        public static readonly BindableProperty NewTravelDaysProperty =
            BindableProperty.Create(nameof(NewTravelDays), typeof(ValueTuple<int, int>), typeof(TravelerCalendar), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnNewTravelDaysPropertyChanged);

        private static void OnNewTravelDaysPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var context = (TravelerCalendar)bindable;
            if (context != null)
            {
                context.NewTravelDays = (ValueTuple<int, int>)newValue;
                
                if(context.NewTravelDays == (0, 0))
                {
                    var (startDay, endDay) = (ValueTuple<int, int>)oldValue;
                    context.ClearNewTravelDays(startDay, endDay);
                }
            }
        }
    }
}
