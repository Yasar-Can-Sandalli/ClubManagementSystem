using System;
using System.Linq;
using System.Windows.Controls;
using ClubManagementSystem.DataAccess;

namespace ClubManagementSystem.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                using (var context = new ClubDbContext())
                {
                    TxtTotalMembers.Text = context.Members.Count().ToString();
                    TxtTotalEvents.Text = context.Events.Count().ToString();

                    // Etkinlik listelemesinde DateTime.Today filtresi kullanıldı
                    var upcomingEvents = context.Events
                        .Where(e => e.EventDate >= DateTime.Today) 
                        .OrderBy(e => e.EventDate)
                        .Take(4)
                        .ToList();

                    LstUpcomingEvents.ItemsSource = upcomingEvents;
                    ClubCalendar.SelectedDate = DateTime.Today;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Dashboard verileri işlenirken hata oluştu: {ex.Message}", "Veri Hatası", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
    }
}