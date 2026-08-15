using System.Collections.Generic;
using System.Xml.Serialization;

namespace QuizBuilder.Models
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    public class Question
    {
        public string Id { get; set; } = System.Guid.NewGuid().ToString();
        public string Text { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public Difficulty Difficulty { get; set; } = Difficulty.Easy;

        // List of possible answers
        public List<string> Options { get; set; } = new List<string>();

        // Index into Options that is correct (0-based)
        public int CorrectOptionIndex { get; set; }

        [XmlIgnore]
        public string CorrectAnswerText =>
            (CorrectOptionIndex >= 0 && CorrectOptionIndex < Options.Count)
                ? Options[CorrectOptionIndex]
                : string.Empty;

        public override string ToString() => Text;
    }
}