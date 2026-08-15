using QuizBuilder.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace QuizBuilder.Services
{
    public static class XmlDataService
    {
        // Save a full question bank (all questions) to XML
        public static async Task SaveQuestionsAsync(List<Question> questions, string filePath)
        {
            try
            {
                await Task.Run(() =>
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));

                    using FileStream stream = new FileStream(filePath, FileMode.Create);
                    serializer.Serialize(stream, questions);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to save questions.\n\n" + ex.Message,
                    "Save Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // Load the question bank from XML
        public static async Task<List<Question>> LoadQuestionsAsync(string filePath)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (!File.Exists(filePath))
                        return new List<Question>();

                    XmlSerializer serializer =
                        new XmlSerializer(typeof(List<Question>));

                    using FileStream stream =
                        new FileStream(filePath, FileMode.Open);

                    return (List<Question>)serializer.Deserialize(stream);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load questions.\n\n" + ex.Message,
                    "Load Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return new List<Question>();
            }
        }

        // Save a completed quiz attempt result
        public static async Task SaveResultAsync(Result result, string filePath)
        {
            await Task.Run(() =>
            {
                var serializer = new XmlSerializer(typeof(Result));
                using var writer = new StreamWriter(filePath);
                serializer.Serialize(writer, result);
            });
        }

        // Load all saved quizzes (list of Quiz objects) from one XML file
        public static async Task SaveQuizAsync(Quiz quiz, string filePath)
        {
            await Task.Run(() =>
            {
                var serializer = new XmlSerializer(typeof(Quiz));
                using var writer = new StreamWriter(filePath);
                serializer.Serialize(writer, quiz);
            });
        }

        public static async Task<Quiz?> LoadQuizAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                if (!File.Exists(filePath)) return null;
                var serializer = new XmlSerializer(typeof(Quiz));
                using var reader = new StreamReader(filePath);
                return (Quiz)serializer.Deserialize(reader)!;
            });
        }
    }
}