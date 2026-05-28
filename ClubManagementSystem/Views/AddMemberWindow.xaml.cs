using System.Windows;
using System.Windows.Controls;
using ClubManagementSystem.Models;
using ClubManagementSystem.DataAccess;

namespace ClubManagementSystem.Views
{
    public partial class AddMemberWindow : Window
    {
        private ClubMember _memberToEdit = null;

        public AddMemberWindow() // Ekleme Modu
        {
            InitializeComponent();
        }

        public AddMemberWindow(ClubMember memberToEdit) // Güncelleme Modu (Aşırı Yükleme)
        {
            InitializeComponent();
            _memberToEdit = memberToEdit; 

            this.Title = "Üye Güncelle";

            TxtFirstName.Text = memberToEdit.FirstName;
            TxtLastName.Text = memberToEdit.LastName;
            TxtStudentNumber.Text = memberToEdit.StudentNumber;
            TxtDepartment.Text = memberToEdit.Department;

            foreach (ComboBoxItem item in CmbRole.Items)
            {
                if (item.Content.ToString() == memberToEdit.Role)
                {
                    CmbRole.SelectedItem = item;
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
            if (string.IsNullOrWhiteSpace(TxtFirstName.Text) || string.IsNullOrWhiteSpace(TxtLastName.Text))
            {
                MessageBox.Show("Ad ve Soyad alanları boş bırakılamaz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new ClubDbContext())
            {
                if (_memberToEdit == null)
                {
                    ClubMember newMember = new ClubMember
                    {
                        FirstName = TxtFirstName.Text,
                        LastName = TxtLastName.Text,
                        StudentNumber = TxtStudentNumber.Text,
                        Department = TxtDepartment.Text,
                        IsActive = true,
                        Role = ((ComboBoxItem)CmbRole.SelectedItem)?.Content.ToString()
                    };
                    context.Members.Add(newMember);
                }
                else
                {
                    var existingMember = context.Members.Find(_memberToEdit.Id);
                    if (existingMember != null)
                    {
                        existingMember.FirstName = TxtFirstName.Text;
                        existingMember.LastName = TxtLastName.Text;
                        existingMember.StudentNumber = TxtStudentNumber.Text;
                        existingMember.Department = TxtDepartment.Text;
                        existingMember.Role = ((ComboBoxItem)CmbRole.SelectedItem)?.Content.ToString();
                        
                        context.Members.Update(existingMember);
                    }
                }
                
                context.SaveChanges(); 
            }

            this.DialogResult = true;
            this.Close();
        }
    }
}