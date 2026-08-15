using System.Collections.Generic;
using System.Linq;
using QuizBuilder.Models;

namespace QuizBuilder.Services
{
    public static class QuestionFilterService
    {
        public static List<Question> Filter(
            List<Question> source,
            string? topic = null,
            Difficulty? difficulty = null,
            string? keyword = null)
        {
            var query = source.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(topic))
                query = query.Where(q => q.Topic.Equals(topic, System.StringComparison.OrdinalIgnoreCase));

            if (difficulty.HasValue)
                query = query.Where(q => q.Difficulty == difficulty.Value);

            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(q =>
                    q.Text.Contains(keyword, System.StringComparison.OrdinalIgnoreCase));

            return query.ToList();
        }

        // Used by the Randomized Quiz Generator (Part 8)
        public static List<Question> GetRandomQuestions(List<Question> source, int count)
        {
            var random = new System.Random();
            return source.OrderBy(_ => random.Next()).Take(count).ToList();
        }
    }
}