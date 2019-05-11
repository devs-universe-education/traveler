using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Traveler.DAL.DataObjects;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Traveler.UI.Controls.TravelerCalendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TravelerCalendar : Grid
    {
        #region CreateTravelTest

        public int NewTravelStartDay { get; private set; }
        public int NewTravelEndDay { get; private set; }
        //public Tuple<int, int> NewTravelDays { get; private set; }

        #endregion

        private Dictionary<int, Color> travelColors;
        private Dictionary<int, CalendarFrame> frames;

        public TravelerCalendar()
        {
            //InitializeComponent();
            InitializeColors();
            frames = new Dictionary<int, CalendarFrame>();

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
        }

        private void InitializeColors()
        {
            var colors = new[] { Color.Aquamarine, Color.Blue, Color.Chocolate, Color.Crimson, Color.DarkCyan, Color.DarkGreen,
                Color.DeepPink, Color.Fuchsia, Color.GreenYellow, Color.Red, Color.Purple, Color.Orange,
                Color.Violet, Color.Lime, Color.Magenta, Color.Yellow, Color.DarkOrange, Color.Brown,
                Color.DarkSalmon, Color.Lavender, Color.MediumVioletRed, Color.PaleGreen, Color.PapayaWhip, Color.Plum,
                Color.SandyBrown, Color.Sienna, Color.Tan, Color.Turquoise, Color.OldLace, Color.MistyRose, Color.Ivory };

            travelColors = new Dictionary<int, Color>();

            Random rand = new Random(DateTime.Now.Millisecond);
            int i = 0;
            foreach(var color in colors.OrderBy(x => rand.Next()))
            {
                travelColors.Add(i, color);
                i++;
            }
        }

        private void Redraw()
        {
            if (Year <= 0 || Month <= 0)
                return;

            this.Children.Clear();
            this.frames.Clear();

            int daysInMonth = DateTime.DaysInMonth(Year, Month);

            int row = 0;
            for (int day = 0; day < daysInMonth; day++)
            {
                var date = new DateTime(Year, Month, day + 1);
                var view = CreateView(date);                

                if (day > 0 && date.DayOfWeek == DayOfWeek.Monday)
                {
                    row++;
                }

                var column = GetDayOfWeek(date);
                this.Children.Add(view, column, row);
            }
        }

        private int GetDayOfWeek(DateTime date)
        {
            int day = (int)date.DayOfWeek;
            return day == 0 ? 6 : day - 1;
        }

        private View CreateView(DateTime day)
        {
            var numOfTravel = NumberOfTravel(day);
            Color color = numOfTravel.num == -1 ? Color.Transparent : travelColors[numOfTravel.num];

            CalendarFrame frame = new CalendarFrame()
            {
                Day = day.Day,
                
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BorderColor = Color.Gray,
                BackgroundColor = color,
                Content = new Label() { Text = day.Day.ToString(), TextColor = Color.Black },
                Padding = 1
            };

            frames.Add(day.Day, frame);

            if(numOfTravel.travel != null)
                frame.GestureRecognizers.Add(BuildTapGesture(day, numOfTravel.travel));
            else
                frame.GestureRecognizers.Add(BuildTapGesture());
            
            return frame;
        }

        private (int num, Travel travel) NumberOfTravel(DateTime day)
        {
            if (Travels == null)
                return (-1, null);

            for (int i = 0; i < Travels.Count(); i++)
            {
                var travel = Travels.ElementAt(i);
                if (day.Day >= travel.StartDate.Day && day.Day <= travel.EndDate.Day)
                    return (i, travel);
            }

            return (-1, null);
        }

        private TapGestureRecognizer BuildTapGesture(DateTime day, Travel travel)
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Command = DayClicked;
            tapGesture.CommandParameter = new Tuple<int, DateTime>(travel.Id, day);

            return tapGesture;
        }

        private TapGestureRecognizer BuildTapGesture()
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += CalendarDay_Tapped;
            return tapGesture;
        }

        private void CalendarDay_Tapped(object sender, EventArgs e)
        {
            var frame = sender as CalendarFrame;

            if (NewTravelStartDay == 0)
            {
                NewTravelStartDay = frame.Day;
                frame.BorderColor = Color.Yellow;
                frame.BackgroundColor = Color.Yellow;
            }
            else if (NewTravelEndDay == 0)
            {
                if (frame.Day < NewTravelStartDay)
                {
                    var oldFrame = frames[NewTravelStartDay];
                    NewTravelEndDay = oldFrame.Day;
                }
                else if (NewTravelStartDay < frame.Day)
                {
                    NewTravelEndDay = frame.Day;
                }
            }
            else if (frame.Day != NewTravelStartDay && frame.Day != NewTravelEndDay)
            {
                if (frame.Day < NewTravelStartDay)
                {
                    var oldFrame = frames[NewTravelStartDay];
                    oldFrame.BorderColor = Color.Gray;
                    oldFrame.BackgroundColor = Color.Transparent;

                    NewTravelStartDay = frame.Day;
                }
                else if (NewTravelEndDay < frame.Day)
                {
                    var oldFrame = frames[NewTravelEndDay];
                    oldFrame.BorderColor = Color.Gray;
                    oldFrame.BackgroundColor = Color.Transparent;

                    NewTravelEndDay = frame.Day;
                }
            }

            //repaint
            if(NewTravelStartDay != 0 && NewTravelEndDay != 0)
            {
                for(int i = NewTravelStartDay; i <= NewTravelEndDay; i++)
                {
                    var paintFrame = frames[i];
                    paintFrame.BorderColor = Color.Yellow;
                    paintFrame.BackgroundColor = Color.Yellow;
                }
            }

            NewTravelDays = new Tuple<int, int>(NewTravelStartDay, NewTravelEndDay);
        }
    }
}