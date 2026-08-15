using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using QuizBuilder.Models;
using QuizBuilder.Services;

namespace QuizBuilder.Views
{
    public partial class QuizWindow : Window
    {
        // EVENT: fired when the whole quiz is scored
        public event EventHandler<Result>? QuizScored;

        private static readonly string QuestionsFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "questions.xml");

        private static readonly string ResultsFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "last_result.xml");

        private const int QuestionsPerQuiz = 5;

        private List<Question> _quizQuestions = new();
        private int _currentIndex = 0;
        private readonly Result _result = new();

        public QuizWindow()
        {
            InitializeComponent();

            Loaded += QuizWindow_Loaded;
            QuizScored += QuizWindow_QuizScored;
        }

        private async void QuizWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var allQuestions = await XmlDataService.LoadQuestionsAsync(QuestionsFilePath);

                if (allQuestions == null || allQuestions.Count == 0)
                {
                    MessageBox.Show(
                        "No questions are available.\n\nPlease add questions first.",
                        "Quiz Builder",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    Close();
                    return;
                }

                // Randomized Quiz Generator
                int count = Math.Min(QuestionsPerQuiz, allQuestions.Count);
                _quizQuestions = QuestionFilterService.GetRandomQuestions(allQuestions, count);

                _result.TotalQuestions = _quizQuestions.Count;

                ShowQuestion(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load the quiz.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                Close();
            }
        }

        private void ShowQuestion(int index)
        {
            var q = _quizQuestions[index];

            ProgressText.Text = $"Question {index + 1} of {_quizQuestions.Count}";
            QuestionText.Text = q.Text;

            var radios = new[]
            {
                Option1Radio,
                Option2Radio,
                Option3Radio,
                Option4Radio
            };

            for (int i = 0; i < radios.Length; i++)
            {
                if (i < q.Options.Count)
                {
                    radios[i].Content = q.Options[i];
                    radios[i].Visibility = Visibility.Visible;
                    radios[i].IsChecked = false;
                }
                else
                {
                    radios[i].Visibility = Visibility.Collapsed;
                }
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            var radios = new[]
            {
                Option1Radio,
                Option2Radio,
                Option3Radio,
                Option4Radio
            };

            int selectedIndex = Array.FindIndex(radios, r => r.IsChecked == true);

            if (selectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select an answer before continuing.",
                    "Answer Required",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            var q = _quizQuestions[_currentIndex];

            bool correct = selectedIndex == q.CorrectOptionIndex;

            if (correct)
                _result.Score++;

            _result.Answers.Add(new AnswerRecord
            {
                QuestionText = q.Text,
                SelectedAnswer = q.Options[selectedIndex],
                CorrectAnswer = q.CorrectAnswerText,
                IsCorrect = correct
            });

            _currentIndex++;

            if (_currentIndex < _quizQuestions.Count)
            {
                ShowQuestion(_currentIndex);
            }
            else
            {
                _result.QuizTitle = "Practice Quiz";

                QuizScored?.Invoke(this, _result);
            }
        }

        // EVENT HANDLER
        private async void QuizWindow_QuizScored(object? sender, Result result)
        {
            try
            {
                await XmlDataService.SaveResultAsync(result, ResultsFilePath);

                var resultWindow = new ResultWindow(result);
                resultWindow.ShowDialog();

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to save quiz result.\n\n" + ex.Message,
                    "Save Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}