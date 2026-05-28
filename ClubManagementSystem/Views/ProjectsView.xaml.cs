using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClubManagementSystem.DataAccess;
using ClubManagementSystem.Models;

namespace ClubManagementSystem.Views
{
    public partial class ProjectsView : UserControl
    {
        public ProjectsView()
        {
            InitializeComponent();
            LoadProjects();
        }

        private void LoadProjects()
        {
            try
            {
                using (var context = new ClubDbContext())
                {
                    ProjectsDataGrid.ItemsSource = context.Projects.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Projeler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddProject_Click(object sender, RoutedEventArgs e)
        {
            // Yeni Ekleme Formunu Aç
            AddProjectWindow addWindow = new AddProjectWindow();
            addWindow.Owner = Window.GetWindow(this);

            if (addWindow.ShowDialog() == true)
            {
                LoadProjects();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var projectToDelete = (sender as Button)?.DataContext as ClubProject;

            if (projectToDelete != null)
            {
                var result = MessageBox.Show($"{projectToDelete.ProjectCode} kodlu projeyi silmek istediğine emin misin?", 
                                             "Silme Onayı", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new ClubDbContext())
                    {
                        context.Projects.Remove(projectToDelete); 
                        context.SaveChanges(); 
                    }
                    LoadProjects(); 
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var projectToEdit = (sender as Button)?.DataContext as ClubProject;
            
            if (projectToEdit != null)
            {
                // Güncelleme Formunu Aç (Dolu Şekilde)
                AddProjectWindow editWindow = new AddProjectWindow(projectToEdit);
                editWindow.Owner = Window.GetWindow(this);

                if (editWindow.ShowDialog() == true)
                {
                    LoadProjects();
                }
            }
        }
    }
}