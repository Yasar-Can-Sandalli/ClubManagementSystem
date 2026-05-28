using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClubManagementSystem.DataAccess;
using ClubManagementSystem.Models;

namespace ClubManagementSystem.Views
{
    public partial class MembersView : UserControl
    {
        public MembersView()
        {
            InitializeComponent();
            LoadMembers();
        }

        private void LoadMembers()
        {
            try
            {
                using (var context = new ClubDbContext())
                {
                    MembersDataGrid.ItemsSource = context.Members.ToList();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Veritabanından üyeler çekilirken bir sorun oluştu.\n\nSebep: {ex.Message}", 
                                "Sistem Hatası Yakalandı", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddMember_Click(object sender, RoutedEventArgs e)
        {
            AddMemberWindow addWindow = new AddMemberWindow();
            addWindow.Owner = Window.GetWindow(this);

            if (addWindow.ShowDialog() == true)
            {
                LoadMembers();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var memberToDelete = (sender as Button)?.DataContext as ClubMember;

            if (memberToDelete != null)
            {
                var result = MessageBox.Show($"{memberToDelete.FirstName} {memberToDelete.LastName} adlı üyeyi silmek istediğine emin misin?", 
                                             "Silme Onayı", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new ClubDbContext())
                    {
                        context.Members.Remove(memberToDelete);
                        context.SaveChanges();
                    }
                    LoadMembers(); 
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var memberToEdit = (sender as Button)?.DataContext as ClubMember;
            
            if (memberToEdit != null)
            {
                AddMemberWindow editWindow = new AddMemberWindow(memberToEdit); // Overloaded constructor
                editWindow.Owner = Window.GetWindow(this);

                if (editWindow.ShowDialog() == true)
                {
                    LoadMembers(); 
                }
            }
        }
    }
}