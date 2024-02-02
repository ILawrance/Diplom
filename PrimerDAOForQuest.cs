using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    internal class PrimerDAOForQuest
    {
        static void PrimerDAOForQuests(string[] args)
        {
            IQuestionDAO questionDAO = new QuestionDAO();
            // Создание нового вопроса
            Question newQuestion = new Question
            {
                Text = "Какая задача выполняется на этапе оптимизации компиляции?",
                Options = new List<string> { "Преобразование исходного кода в машинный код.", "Проверка синтаксической корректности исходного кода.", "Улучшение эффективности и производительности программы.", "Генерация абстрактного синтаксического дерева (AST)." },
                CorrectAnswer = "Улучшение эффективности и производительности программы."
            };
            questionDAO.AddQuestion(newQuestion);
            // Получение вопроса по идентификатору и вывод информации
            Question retrievedQuestion = questionDAO.GetQuestionById(1);
            Console.WriteLine("Вопрос: " + retrievedQuestion.Text);
            Console.WriteLine("Варианты ответов: ");
            foreach (string option in retrievedQuestion.Options)
            {
                Console.WriteLine("- " + option);
            }
            Console.WriteLine("Правильный ответ: " + retrievedQuestion.CorrectAnswer);
            // Обновление информации о вопросе
            retrievedQuestion.Text = "Каким образом компилятор отличается от интерпретатора?";
            retrievedQuestion.Options = new List<string> { " Компилятор преобразует исходный код в машинный код, а интерпретатор выполняет код построчно.", "Компилятор выполняет код построчно, а интерпретатор преобразует исходный код в машинный код. ", "Компилятор работает с исходным кодом на языке программирования, а интерпретатор работает с машинным кодом." , "Компилятор и интерпретатор выполняют одни и те же операции, но в разном порядке." };
            retrievedQuestion.CorrectAnswer = "Компилятор преобразует исходный код в машинный код, а интерпретатор выполняет код построчно.";
            questionDAO.UpdateQuestion(retrievedQuestion);
            // Удаление вопроса
            questionDAO.DeleteQuestion(1);
        }
    }
}
