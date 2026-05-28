using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClubManagementSystem.DataAccess;
using ClubManagementSystem.Models;

namespace ClubManagementSystem.Views
{
    public partial class EventsView : UserControl
    {
        public EventsView()
        {
            InitializeComponent();
            LoadEvents();
        }

        private void LoadEvents()
        {
            try
            {
                using (var context = new ClubDbContext())
                {
                    EventsDataGrid.ItemsSource = context.Events.OrderBy(e => e.EventDate).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Etkinlikler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            AddEventWindow addWindow = new AddEventWindow();
            addWindow.Owner = Window.GetWindow(this);

            if (addWindow.ShowDialog() == true)
            {
                LoadEvents();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var eventToDelete = (sender as Button)?.DataContext as ClubEvent;
            if (eventToDelete != null)
            {
                var result = MessageBox.Show($"{eventToDelete.EventName} etkinliğini silmek istediğine emin misin?", 
                                             "Silme Onayı", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new ClubDbContext())
                    {
                        context.Events.Remove(eventToDelete);
                        context.SaveChanges();
                    }
                    LoadEvents();
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var eventToEdit = (sender as Button)?.DataContext as ClubEvent;
            if (eventToEdit != null)
            {
                AddEventWindow editWindow = new AddEventWindow(eventToEdit);
                editWindow.Owner = Window.GetWindow(this);
                if (editWindow.ShowDialog() == true)
                {
                    LoadEvents();
                }
            }
        }
    }
}