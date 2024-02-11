using Syncfusion.Maui.Scheduler;

namespace App.GRRInnovations.AgendaPro.Views;

public partial class CalendarView : ContentPage
{
	public CalendarView()
	{
        new SchedulerAppointment
        {
            StartTime = DateTime.Now.Date.AddHours(10),
            EndTime = DateTime.Now.Date.AddHours(11),
            Subject = "Client Meeting",
            Background = new SolidColorBrush(Color.FromArgb("#FF8B1FA9")),
        }

        InitializeComponent();
	}
}