using System;
using System.Collections.Generic;

namespace QuizBuilder.Models
{
    public class AnswerRecord
    {
        public string QuestionText { get; set; } = string.Empty;
        public string SelectedAnswer { get; set; } = string.Empty;
        public string CorrectAnswer { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }

    public class Result
    {
        public string QuizTitle { get; set; } = string.Empty;
        public DateTime TakenOn { get; set; } = DateTime.Now;
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public double Percentage =>
            TotalQuestions == 0 ? 0 : Math.Round((double)Score / TotalQuestions * 100, 1);

        public List<AnswerRecord> Answers { get; set; } = new List<AnswerRecord>();
    }
}