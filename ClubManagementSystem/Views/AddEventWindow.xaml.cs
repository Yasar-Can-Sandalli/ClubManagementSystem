using System;
using System.Windows;
using ClubManagementSystem.Models;
using ClubManagementSystem.DataAccess;

namespace ClubManagementSystem.Views
{
    public partial class AddEventWindow : Window
    {
        private ClubEvent _eventToEdit = null;

        public AddEventWindow() // Ekleme
        {
            InitializeComponent();
            DpEventDate.SelectedDate = DateTime.Today;
        }

        public AddEventWindow(ClubEvent eventToEdit) // Güncelleme
        {
            InitializeComponent();
            _eventToEdit = eventToEdit;
            
            this.Title = "Etkinlik Güncelle";
            
            TxtEventName.Text = eventToEdit.EventName;
            DpEventDate.SelectedDate = eventToEdit.EventDate;
            TxtLocation.Text = eventToEdit.Location;
            TxtDescription.Text = eventToEdit.Description;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtEventName.Text) || DpEventDate.SelectedDate == null)
            {
                MessageBox.Show("Etkinlik adı ve tarihi zorunludur!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new ClubDbContext())
            {
                if (_eventToEdit == null) // YENİ
                {
                    ClubEvent newEvent = new ClubEvent
                    {
                        EventName = TxtEventName.Text,
                        EventDate = DpEventDate.SelectedDate.Value,
                        Location = TxtLocation.Text,
                        Description = TxtDescription.Text
                    };
                    context.Events.Add(newEvent);
                }
                else // GÜNCELLE
                {
                    var existingEvent = context.Events.Find(_eventToEdit.Id);
                    if (existingEvent != null)
                    {
                        existingEvent.EventName = TxtEventName.Text;
                        existingEvent.EventDate = DpEventDate.SelectedDate.Value;
                        existingEvent.Location = TxtLocation.Text;
                        existingEvent.Description = TxtDescription.Text;
                        
                        context.Events.Update(existingEvent);
                    }
                }
                context.SaveChanges();
            }

            this.DialogResult = true;
            this.Close();
        }
    }
}