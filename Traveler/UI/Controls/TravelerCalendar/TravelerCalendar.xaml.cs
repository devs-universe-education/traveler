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
        public DateTime NewTravelStartDate { get; private set; }
        public DateTime NewTravelEndDate { get; private set; }
        public bool NewTravelStartDay { get; private set; }
        public bool NewTravelEndDay { get; private set; }

        private Dictionary<int, Color> travelColors;
        private List<Frame> frames;

        public TravelerCalendar()
        {
            InitializeColors();
            frames = new List<Frame>(31);

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
                    Width = GridLength.Star
                });
            }
        }

        private void InitializeColors()
        {
            var colors = new[] { Color.Aquamarine, Color.Blue, Color.Chocolate, Color.Crimson, Color.DarkCyan, Color.DarkGreen,
                Color.DeepPink, Color.DarkSlateGray, Color.GreenYellow, Color.Red, Color.Purple, Color.Orange,
                Color.Violet, Color.Lime, Color.Magenta, Color.Olive, Color.MediumSlateBlue, Color.Brown,
                Color.DarkSalmon, Color.PaleVioletRed, Color.MediumVioletRed, Color.PaleGreen, Color.Peru, Color.Plum,
                Color.SandyBrown, Color.Sienna, Color.Teal, Color.Turquoise, Color.SeaGreen, Color.Maroon, Color.Indigo };

            travelColors = new Dictionary<int, Color>();

            Random rand = new Random(DateTime.Now.Millisecond);
            int i = 0;
            foreach(var color in colors.OrderBy(x => rand.Next()))
            {
                travelColors.Add(i, color);
                i++;
            }
        }

        private void Redraw(bool clearSelection)
        {
            this.Children.Clear();
            this.frames.Clear();

            int daysInMonth = DateTime.DaysInMonth(Date.Year, Date.Month);

            int row = 0;
            for (int day = 0; day < daysInMonth; day++)
            {
                var date = new DateTime(Date.Year, Date.Month, day + 1);
                var view = CreateView(date);                

                if (day > 0 && date.DayOfWeek == DayOfWeek.Monday)
                {
                    row++;
                }

                var column = GetDayOfWeek(date);
                this.Children.Add(view, column, row);
            }

            if (clearSelection)
                ClearNewTravelSelection();

            PaintNewTravelDays();            
        }

        private int GetDayOfWeek(DateTime date)
        {
            int day = (int)date.DayOfWeek;
            return day == 0 ? 6 : day - 1;
        }

        private View CreateView(DateTime date)
        {
            var (num, travel) = NumberOfTravel(date);
            Color color = num == -1 ? Color.Transparent : travelColors[num];

            Frame frame = new Frame()
            {
                BindingContext = date,
                
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BorderColor = Color.Gray,
                BackgroundColor = color,
                Content = new Label() { Text = date.Day.ToString(), TextColor = Color.Black },
                Padding = 1
            };

            frames.Add(frame);

            if(travel != null)
                frame.GestureRecognizers.Add(BuildTapGesture(date, travel));
            else
                frame.GestureRecognizers.Add(BuildTapGesture());
            
            return frame;
        }

        private (int num, TravelDataObject travel) NumberOfTravel(DateTime date)
        {
            if (Travels == null)
                return (-1, null);

            for (int i = 0; i < Travels.Count(); i++)
            {
                var travel = Travels.ElementAt(i);
                if (date >= travel.StartDate && date <= travel.EndDate)
                    return (i, travel);
            }

            return (-1, null);
        }

        private TapGestureRecognizer BuildTapGesture(DateTime date, TravelDataObject travel)
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Command = DayClicked;
            tapGesture.CommandParameter = new ValueTuple<int, DateTime>(travel.Id, date);

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
            var frame = sender as Frame;
            DateTime frameDate = (DateTime)frame.BindingContext;

            if (!NewTravelStartDay)
            {
                NewTravelStartDay = true;
                NewTravelStartDate = frameDate;
                //frame.BorderColor = Color.Yellow;
                //frame.BackgroundColor = Color.Yellow;
            }
            else if (!NewTravelEndDay)
            {
                if (frameDate < NewTravelStartDate)
                {
                    NewTravelEndDay = true;
                    NewTravelEndDate = NewTravelStartDate;
                    NewTravelStartDate = frameDate;
                }
                else if (NewTravelStartDate < frameDate)
                {
                    NewTravelEndDate = frameDate;
                }
            }
            else if (frameDate != NewTravelStartDate && frameDate != NewTravelEndDate)
            {
                if (frameDate < NewTravelStartDate)
                {
                    NewTravelStartDate = frameDate;
                }
                else if (NewTravelEndDate < frameDate)
                {
                    NewTravelEndDate = frameDate;
                }
            }

            PaintNewTravelDays();
            NewTravelDates = (NewTravelStartDate, NewTravelEndDate);            
        }

        private void PaintNewTravelDays()
        {
            for (int i = 0; i < frames.Count; i++)
            {
                var frame = frames[i];
                var frameDate = (DateTime)frame.BindingContext;

                if (NewTravelStartDate <= frameDate && frameDate <= NewTravelEndDate)
                {
                    frame.BorderColor = Color.Yellow;
                    frame.BackgroundColor = Color.Yellow;
                }
                else if (NewTravelStartDate == frameDate && NewTravelEndDay == false)
                {
                    frame.BorderColor = Color.Yellow;
                    frame.BackgroundColor = Color.Yellow;
                    return;
                }
            }
        }

        public void ClearNewTravelSelection()
        {
            NewTravelStartDay = false;
            NewTravelEndDay = false;            
            NewTravelStartDate = default(DateTime);
            NewTravelEndDate = default(DateTime);
        }
    }
}