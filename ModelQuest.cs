using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    public class Question ///pattern Data Acces Object
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }

    // Интерфейс DAO для работы с вопросами
    public interface IQuestionDAO
    {
        Question GetQuestionById(int id);
        void AddQuestion(Question question);
        void UpdateQuestion(Question question);
        void DeleteQuestion(int id);
    }

    // Реализация DAO для работы с вопросами в базе данных
    public class QuestionDAO : IQuestionDAO
    {
        // Метод для получения вопроса по идентификатору
        public Question GetQuestionById(int id)
        {
            Question question = null;
            // Логика для получения вопроса из базы данных по идентификатору
            // ...
            return question;
        }

        // Метод для добавления нового вопроса
        public void AddQuestion(Question question)
        {
            // Логика для добавления вопроса в базу данных
            // ...
        }

        // Метод для обновления информации о вопросе
        public void UpdateQuestion(Question question)
        {
            // Логика для обновления информации о вопросе в базе данных
            // ...
        }

        // Метод для удаления вопроса
        public void DeleteQuestion(int id)
        {
            // Логика для удаления вопроса из базы данных по идентификатору
            // ...
        }
    }
}
