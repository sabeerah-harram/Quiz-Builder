using System.Windows;
using QuizBuilder.Views;

namespace QuizBuilder
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ManageQuestionsButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new QuestionManagerWindow();
            window.ShowDialog();
        }
        private void StartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new QuizWindow();
            window.ShowDialog();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}