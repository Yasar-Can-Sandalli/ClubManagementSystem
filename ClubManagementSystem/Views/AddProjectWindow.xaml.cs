using System.Windows;
using System.Windows.Controls;
using ClubManagementSystem.Models;
using ClubManagementSystem.DataAccess;

namespace ClubManagementSystem.Views
{
    public partial class AddProjectWindow : Window
    {
        private ClubProject _projectToEdit = null;

        public AddProjectWindow() // Ekleme Modu
        {
            InitializeComponent();
        }

        public AddProjectWindow(ClubProject projectToEdit) // Güncelleme Modu
        {
            InitializeComponent();
            _projectToEdit = projectToEdit;
            
            this.Title = "Proje Güncelle";
            
            TxtProjectCode.Text = projectToEdit.ProjectCode;
            TxtProjectName.Text = projectToEdit.ProjectName;
            TxtAdvisorName.Text = projectToEdit.AdvisorName;
            
            foreach (ComboBoxItem item in CmbStatus.Items)
            {
                if (item.Content.ToString() == projectToEdit.Status)
                {
                    CmbStatus.SelectedItem = item;
                    break;
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtProjectCode.Text) || string.IsNullOrWhiteSpace(TxtProjectName.Text))
            {
                MessageBox.Show("Proje kodu ve adı boş bırakılamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new ClubDbContext())
            {
                if (_projectToEdit == null) // YENİ KAYIT
                {
                    ClubProject newProject = new ClubProject
                    {
                        ProjectCode = TxtProjectCode.Text,
                        ProjectName = TxtProjectName.Text,
                        AdvisorName = TxtAdvisorName.Text,
                        Status = ((ComboBoxItem)CmbStatus.SelectedItem)?.Content.ToString()
                    };
                    context.Projects.Add(newProject);
                }
                else // GÜNCELLEME
                {
                    var existingProject = context.Projects.Find(_projectToEdit.Id);
                    if (existingProject != null)
                    {
                        existingProject.ProjectCode = TxtProjectCode.Text;
                        existingProject.ProjectName = TxtProjectName.Text;
                        existingProject.AdvisorName = TxtAdvisorName.Text;
                        existingProject.Status = ((ComboBoxItem)CmbStatus.SelectedItem)?.Content.ToString();
                        
                        context.Projects.Update(existingProject);
                    }
                }
                context.SaveChanges();
            }

            this.DialogResult = true;
            this.Close();
        }
    }
}