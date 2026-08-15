using System;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using QuizBuilder.Models;

namespace QuizBuilder.Views
{
    public partial class ResultWindow : Window
    {
        private readonly Result _result;

        public ResultWindow(Result result)
        {
            InitializeComponent();
            _result = result;

            SummaryText.Text = $"Score: {result.Score}/{result.TotalQuestions} ({result.Percentage}%)";
            AnswersList.ItemsSource = result.Answers;
        }

        private async void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = $"QuizResult_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            };

            if (dialog.ShowDialog() == true)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Question,SelectedAnswer,CorrectAnswer,IsCorrect");

                foreach (var a in _result.Answers)
                {
                    sb.AppendLine($"\"{a.QuestionText}\",\"{a.SelectedAnswer}\",\"{a.CorrectAnswer}\",{a.IsCorrect}");
                }

                await File.WriteAllTextAsync(dialog.FileName, sb.ToString());
                MessageBox.Show("Exported successfully.");
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}