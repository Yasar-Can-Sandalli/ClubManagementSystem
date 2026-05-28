using System.Windows;
using System.Windows.Input;

namespace ClubManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Uygulama açılışında Dashboard Yüklensin
            MainContent.Children.Add(new ClubManagementSystem.Views.DashboardView());
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            // Basit bir çıkış işlemi, istersen onay kutusu ekleyebilirsin
            Application.Current.Shutdown();
        }

        // --- SAYFA GEÇİŞLERİ ---
        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new ClubManagementSystem.Views.DashboardView());
        }

        private void BtnMembers_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new ClubManagementSystem.Views.MembersView());
        }

        private void BtnEvents_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new ClubManagementSystem.Views.EventsView());
        }

        private void BtnProjects_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new ClubManagementSystem.Views.ProjectsView());
        }
    }
}