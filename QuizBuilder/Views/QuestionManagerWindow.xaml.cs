using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using QuizBuilder.Models;
using QuizBuilder.Services;

namespace QuizBuilder.Views
{
    public partial class QuestionManagerWindow : Window
    {
        // Central in-memory question bank, backed by an XML file
        private static readonly string DataFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "questions.xml");

        private List<Question> _allQuestions = new List<Question>();
        private Question? _selectedQuestion;

        public QuestionManagerWindow()
        {
            InitializeComponent();
            Loaded += QuestionManagerWindow_Loaded;
        }

        private async void QuestionManagerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _allQuestions = await XmlDataService.LoadQuestionsAsync(DataFilePath);
            RefreshGrid(_allQuestions);
        }

        private void RefreshGrid(List<Question> questions)
        {
            QuestionsGrid.ItemsSource = null;
            QuestionsGrid.ItemsSource = questions;
        }

        private void QuestionsGrid_SelectionChanged(object sender,
     System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedQuestion = QuestionsGrid.SelectedItem as Question;

            if (_selectedQuestion != null)
            {
                QuestionDetailsText.Text = _selectedQuestion.Text;
                TopicDetailsText.Text = _selectedQuestion.Topic;
                DifficultyDetailsText.Text = _selectedQuestion.Difficulty.ToString();
                CorrectAnswerDetailsText.Text = _selectedQuestion.CorrectAnswerText;
            }
            else
            {
                QuestionDetailsText.Text = "";
                TopicDetailsText.Text = "";
                DifficultyDetailsText.Text = "";
                CorrectAnswerDetailsText.Text = "";
            }
        }

        // ---------- CRUD ----------

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editor = new QuestionEditorWindow();

            if (editor.ShowDialog() == true && editor.ResultQuestion != null)
            {
                bool exists = _allQuestions.Any(q =>
                    q.Text.Equals(editor.ResultQuestion.Text,
                    StringComparison.OrdinalIgnoreCase));

                if (exists)
                {
                    MessageBox.Show(
                        "A question with the same text already exists.",
                        "Duplicate Question",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                _allQuestions.Add(editor.ResultQuestion);
                RefreshGrid(_allQuestions);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedQuestion == null)
            {
                MessageBox.Show("Select a question to edit first.");
                return;
            }

            var editor = new QuestionEditorWindow(_selectedQuestion);

            if (editor.ShowDialog() == true)
            {
                RefreshGrid(_allQuestions);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedQuestion == null)
            {
                MessageBox.Show("Select a question to delete first.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Delete \"{_selectedQuestion.Text}\"?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                _allQuestions.Remove(_selectedQuestion);
                _selectedQuestion = null;
                RefreshGrid(_allQuestions);
            }
        }

        // ---------- LINQ Filtering ----------

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string? topic =
                string.IsNullOrWhiteSpace(TopicFilterBox.Text)
                ? null
                : TopicFilterBox.Text.Trim();

            string? keyword =
                string.IsNullOrWhiteSpace(KeywordFilterBox.Text)
                ? null
                : KeywordFilterBox.Text.Trim();

            Difficulty? difficulty = null;

            if (DifficultyFilterBox.SelectedItem is System.Windows.Controls.ComboBoxItem item
                && item.Content.ToString() != "Any")
            {
                difficulty = (Difficulty)System.Enum.Parse(
                    typeof(Difficulty),
                    item.Content.ToString()!);
            }

            var filtered = QuestionFilterService.Filter(
                _allQuestions,
                topic,
                difficulty,
                keyword);

            RefreshGrid(filtered);

            // Step 4: Handle Empty Search Results
            if (filtered.Count == 0)
            {
                MessageBox.Show(
                    "No matching questions found.",
                    "Search Results",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            TopicFilterBox.Clear();
            KeywordFilterBox.Clear();
            DifficultyFilterBox.SelectedIndex = -1;

            RefreshGrid(_allQuestions);
        }

        // ---------- XML Save (async) ----------

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await XmlDataService.SaveQuestionsAsync(_allQuestions, DataFilePath);

                MessageBox.Show(
                    "Questions saved successfully.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(
                    "Unable to save questions.\n\n" + ex.Message,
                    "Save Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}