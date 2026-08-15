using System;
using System.Collections.Generic;
using System.Windows;
using QuizBuilder.Models;

namespace QuizBuilder.Views
{
    public partial class QuestionEditorWindow : Window
    {
        public Question? ResultQuestion { get; private set; }
        private readonly Question? _editingQuestion;

        // Add mode
        public QuestionEditorWindow()
        {
            InitializeComponent();
        }

        // Edit mode
        public QuestionEditorWindow(Question existing) : this()
        {
            _editingQuestion = existing;

            QuestionTextBox.Text = existing.Text;
            TopicBox.Text = existing.Topic;
            DifficultyBox.SelectedIndex = (int)existing.Difficulty;

            if (existing.Options.Count > 0) Option1Box.Text = existing.Options[0];
            if (existing.Options.Count > 1) Option2Box.Text = existing.Options[1];
            if (existing.Options.Count > 2) Option3Box.Text = existing.Options[2];
            if (existing.Options.Count > 3) Option4Box.Text = existing.Options[3];

            CorrectIndexBox.Text = (existing.CorrectOptionIndex + 1).ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous error message
            ErrorText.Text = "";

            // Validate question
            if (string.IsNullOrWhiteSpace(QuestionTextBox.Text))
            {
                ErrorText.Text = "Please enter the question text.";
                return;
            }

            // Validate topic
            if (string.IsNullOrWhiteSpace(TopicBox.Text))
            {
                ErrorText.Text = "Please enter a topic.";
                return;
            }

            // Validate difficulty
            if (DifficultyBox.SelectedIndex == -1)
            {
                ErrorText.Text = "Please select a difficulty level.";
                return;
            }

            // Validate options
            if (string.IsNullOrWhiteSpace(Option1Box.Text) ||
                string.IsNullOrWhiteSpace(Option2Box.Text) ||
                string.IsNullOrWhiteSpace(Option3Box.Text) ||
                string.IsNullOrWhiteSpace(Option4Box.Text))
            {
                ErrorText.Text = "Please fill in all four options.";
                return;
            }

            // Validate correct option number
            if (!int.TryParse(CorrectIndexBox.Text, out int correctNum) ||
                correctNum < 1 || correctNum > 4)
            {
                ErrorText.Text = "Correct option must be a number between 1 and 4.";
                return;
            }

            // Store trimmed options
            List<string> options = new List<string>
            {
                Option1Box.Text.Trim(),
                Option2Box.Text.Trim(),
                Option3Box.Text.Trim(),
                Option4Box.Text.Trim()
            };

            // Extra validation
            if (string.IsNullOrWhiteSpace(options[correctNum - 1]))
            {
                ErrorText.Text = "The correct option cannot be empty.";
                return;
            }

            // Create or update question
            var question = _editingQuestion ?? new Question();

            question.Text = QuestionTextBox.Text.Trim();
            question.Topic = TopicBox.Text.Trim();
            question.Difficulty = (Difficulty)DifficultyBox.SelectedIndex;
            question.Options = options;
            question.CorrectOptionIndex = correctNum - 1;

            ResultQuestion = question;

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}