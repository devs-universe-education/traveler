using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Traveler.UI.Controls.TravelerCalendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TravelerCalendar : Grid
    {
        public TravelerCalendar()
        {
            //InitializeComponent();

            this.HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            this.VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);

            int rowCount = 6;
            int columnCount = 7;

            for (int i = 0; i < rowCount; i++)
            {
                this.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Star
                });
            }

            for (int i = 0; i < columnCount; i++)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = GridLength.Star,
                });
            }

            travelColor = new Dictionary<int, Color>
            {
                { 0, Color.Red },
                { 1, Color.Green },
                { 2, Color.Blue },
                { 3, Color.Purple },
                { 4, Color.Orange },
                { 5, Color.Violet },
                { 6, Color.Lime }
            };
        }

        private void Redraw()
        {
            if (Year <= 0 || Month <= 0)
                return;

            this.Children.Clear();

            int daysInMonth = DateTime.DaysInMonth(Year, Month);

            int row = 0;
            //int travel = 0;
            for (int day = 0; day < daysInMonth; day++)
            {
                var date = new DateTime(Year, Month, day + 1);
                //var events = GetEvents(date);
                var view = CreateView(date);

                var column = GetDayOfWeek(date);
                if (day > 0 && date.DayOfWeek == DayOfWeek.Monday)
                {
                    row++;
                }

                this.Children.Add(view, column, row);
            }
        }

        private int GetDayOfWeek(DateTime date)
        {
            int day = (int)date.DayOfWeek;

            return day == 0 ? 6 : day - 1;
        }

        private View CreateView(DateTime date)
        {
            int num = NumberOfTravel(date);
            Color color = num == -1 ? Color.Transparent : travelColor[num];

            Frame frame = new Frame
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                BorderColor = Color.Gray,
                BackgroundColor = color,
                Content = new Label() { Text = date.Day.ToString(), TextColor = Color.Black },
                Padding = 1

            };

            frame.GestureRecognizers.Add(BuildTapGesture(date));

            return frame;
        }

        private int NumberOfTravel(DateTime day)
        {
            if (Travels == null)
                return -1;

            for (int i = 0; i < Travels.Count(); i++)
            {
                var travel = Travels.ElementAt(i);
                if (day.Day >= travel.StartDate.Day && day.Day <= travel.EndDate.Day)
                    return i;
            }

            return -1;
        }

        private TapGestureRecognizer BuildTapGesture(DateTime day)
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Command = DayClicked;
            tapGesture.CommandParameter = day.Day;

            return tapGesture;
        }

        private Dictionary<int, Color> travelColor;
    }
}