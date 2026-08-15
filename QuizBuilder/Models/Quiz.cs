using System;
using System.Collections.Generic;

namespace QuizBuilder.Models
{
    public class Quiz
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}