namespace PROGPOEP2
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public string[] Options { get; set; }      // null if True/False
        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; }
        public bool IsTrueFalse { get; set; }

        public QuizQuestion(string question, string[] options, string correctAnswer, string explanation)
        {
            Question = question;
            Options = options;
            CorrectAnswer = correctAnswer;
            Explanation = explanation;
            IsTrueFalse = false;
        }

        // True/False constructor
        public QuizQuestion(string question, bool correctAnswer, string explanation)
        {
            Question = question;
            Options = new[] { "True", "False" };
            CorrectAnswer = correctAnswer ? "True" : "False";
            Explanation = explanation;
            IsTrueFalse = true;
        }
    }
}