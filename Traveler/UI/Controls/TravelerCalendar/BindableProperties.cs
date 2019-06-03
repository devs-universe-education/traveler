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
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public static readonly BindableProperty DateProperty =
            BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(TravelerCalendar), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnDatePropertyChanged);

        private static void OnDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var context = (TravelerCalendar)bindable;
            if (context != null)
            {
                context.Date = (DateTime)newValue;
                context.Redraw(clearSelection: false);
            }
        }

        public IEnumerable<TravelDataObject> Travels
        {
            get => (IEnumerable<TravelDataObject>)GetValue(TravelsProperty);
            set => SetValue(TravelsProperty, value);
        }

        public static readonly BindableProperty TravelsProperty =
            BindableProperty.Create(nameof(Travels), typeof(IEnumerable<TravelDataObject>), typeof(TravelerCalendar), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnTravelsPropertyChanged);

        private static void OnTravelsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var context = (TravelerCalendar)bindable;
            if (context != null)
            {
                context.Travels = (IEnumerable<TravelDataObject>)newValue;
                context.Redraw(clearSelection: false);
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
                context.Redraw(clearSelection: false);
            }
        }

        public ValueTuple<DateTime, DateTime> NewTravelDates
        {
            get => (ValueTuple<DateTime, DateTime>)GetValue(NewTravelDatesProperty);
            set => SetValue(NewTravelDatesProperty, value);
        }

        public static readonly BindableProperty NewTravelDatesProperty =
            BindableProperty.Create(nameof(NewTravelDates), typeof(ValueTuple<DateTime, DateTime>), typeof(TravelerCalendar), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnNewTravelDatesPropertyChanged);

        private static void OnNewTravelDatesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var context = (TravelerCalendar)bindable;
            if (context != null)
            {
                context.NewTravelDates = (ValueTuple<DateTime, DateTime>)newValue;

                if (context.NewTravelDates.Item1 == default(DateTime))
                {
                    context.Redraw(clearSelection: true);
                }
            }
        }
    }
}
