using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Npgsql;

namespace Diplom.PaternTest
{
    public partial class TargetInQuestion : Form
    {
        Dictionary<string, Bitmap> dict = new Dictionary<string, Bitmap>();
        Dictionary<string, Bitmap> dictSours = new Dictionary<string, Bitmap>();
        Bitmap[] bitmapsCheck = new Bitmap[4]; // Битмапа равная кол-ву ячеек в вопросе
        Random random = new Random();
        int questsCount;
        int randomId;
        List<int> randomIDMemory = new List<int>();
        
        public TargetInQuestion()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Connection.connectionStringSQL)) 
            {
                string query = "SELECT COUNT(*) FROM public.questionsimage";  // Узнаем кол-во достурных вопросов для использования в генерации
                connection.Open();
                using(NpgsqlCommand cmd = new NpgsqlCommand(query, connection)) 
                {
                    questsCount = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
                }
            }

            byte[] imageData;
            string text;
            Bitmap bpmImage;
            dictSours.Clear();
            for (int id = 1; id < 5; id++)
            {
                randomId = random.Next(1, questsCount);
                while (randomIDMemory.Contains(randomId))
                {
                    randomId = random.Next(1, questsCount);
                }
                randomIDMemory.Add(randomId);
                string queryTakeDataText = $@"
                    SELECT text FROM public.questionsimage WHERE questionimageid = {randomId};
                    ";
                string queryTakeDataImage = $@"
                    SELECT image FROM public.questionsimage WHERE questionimageid = {randomId};
                    ";
                using (NpgsqlConnection connection = new NpgsqlConnection(Connection.connectionStringSQL))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(queryTakeDataText, connection))
                    {
                        text = Convert.ToString(command.ExecuteScalar());
                    }
                    using (NpgsqlCommand command = new NpgsqlCommand(queryTakeDataImage, connection))
                    {
                        imageData = (byte[])command.ExecuteScalar();
                    }
                }
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Image image = Image.FromStream(ms);
                    bpmImage = new Bitmap(image);
                }
                dictSours.Add(text, bpmImage); 

            }
            randomIDMemory.Clear();


            InitializeComponent();
            List<int> povtorQuests = new List<int>();
            List<int> povtorImg = new List<int>();
            bool isRelevant = false;
            PictureBox[] pictureBoxes = { pictureBox1, pictureBox2, pictureBox3, pictureBox4 }; // боксы под варианты 
            Label[] labels = { label2, label3, label4, label5 };
            List<Bitmap> bitmaps = new List<Bitmap>();
            List<string> allImageNames = new List<string>();
            foreach (string key in dictSours.Keys)
            {
                allImageNames.Add(key);
            }

            foreach (string key in dictSours.Keys) // Добавление всех изображений в лист
            {
                    dictSours.TryGetValue(key, out Bitmap bitImage);
                    bitmaps.Add(bitImage);
            }
            List<string> AllQuestions = new List<string>(); 
            foreach (string key in dictSours.Keys)    // Все вопросы в лист
            {
                AllQuestions.Add(key);
            }

            int memOfPosRightAns = 0;
            while (!isRelevant) // Генерация релевантного варианта задания
            {
                dict.Clear();
                for (int i = 0; i < 4; i++) // Случайное заполнение лейблов текстом
                {
                    int rand = random.Next(0, labels.Length);
                    while (povtorQuests.Contains(rand))
                    {
                        rand = random.Next(0, labels.Length);
                    }
                    labels[i].Text = AllQuestions[rand];
                    povtorQuests.Add(rand);
                }
                povtorQuests.Clear();

                for (int i = 0; i < bitmaps.Count; i++) // Добавление всех заданий в словарь
                {
                    dict.Add(AllQuestions[i], bitmaps[i]);
                }

                for (int i = 0; i < 4; i++) // Случайное заполнение боксов изображениями
                {
                    int rand = random.Next(0, bitmaps.Count());
                    while (povtorImg.Contains(rand))
                    {
                        rand = random.Next(0, bitmaps.Count);
                    }
                    pictureBoxes[i].Image = bitmaps[rand];
                    bitmapsCheck[i] = bitmaps[rand];
                    povtorImg.Add(rand);
                }
                povtorImg.Clear();

                
                int count = 0;
                for (int i = 0; i < 4; i++) // Поиск существует ли верный вариант сгенерированных пар
                {
                    if (dict[labels[i].Text] == bitmapsCheck[i])
                    {
                        count++;
                        isRelevant = true;
                        memOfPosRightAns = i;
                    }
                }
                if (count != 1) // Проверка что верная пара ровно одна
                {
                    isRelevant = false;
                }
            }



            int variant = random.Next(0, 100);
            if (variant > 49) // Перестройка задания под 1 из 2 видов
            {
                VariableLabel.Text = "Дана диаграмма ДКА, на которой зеленым треугольником отмечено начальное состояние, " +
                    "\nа красным шестиугольником отмечено(ы) заключительное(ые) состояние(я). " +
                    "\nВыяснить какое слово будет допущено этим автоматом.";
                for (int i = 0; i < labels.Length; i++)
                {
                    labels[i].Location = new Point(120, labels[i].Location.Y);
                }
                for (int i = 0; i < pictureBoxes.Length; i++)
                {
                    pictureBoxes[i].Location = new Point(90, pictureBoxes[i].Location.Y);
                }
                
                foreach (PictureBox pictureBox in pictureBoxes)
                {
                    pictureBox.Visible = false;
                }
                pictureBoxes[memOfPosRightAns].Visible = true;
                pictureBoxes[memOfPosRightAns].Location = new Point(320, 110);
            }
            else
            {
                VariableLabel.Text = "Дано слово. Выяснить какой ДКА допустит это слово. " +
                    "\nНа диаграммах ДКА зелеными треугольниками отмечаются начальные состояния, " +
                    "\nкрасными шестиугольниками – заключительные." + " \n" + labels[memOfPosRightAns].Text + " это: ";

                foreach (Label label in labels)
                {
                    label.Visible = false;
                }
                foreach (PictureBox pictureBox in pictureBoxes)
                {
                    pictureBox.Location = new Point(pictureBox.Location.X - 100, pictureBox.Location.Y);
                }
            }
        }
        

        private void button1_Click_1(object sender, EventArgs e) // Проверка пользователя на верный ответ
        {
            if (radioButton1.Checked && dict[label2.Text] == bitmapsCheck[0])
            {
                label7.Text = "Ваш ответ: \nВерный";
            }
            else if (radioButton2.Checked && dict[label3.Text] == bitmapsCheck[1])
            {
                label7.Text = "Ваш ответ: \nВерный";
            }
            else if (radioButton3.Checked && dict[label4.Text] == bitmapsCheck[2])
            {
                label7.Text = "Ваш ответ: \nВерный";
            }
            else if (radioButton4.Checked && dict[label5.Text] == bitmapsCheck[3])
            {
                label7.Text = "Ваш ответ: \nВерный";
            }
            else
            {
                label7.Text = "Ваш ответ: \nНеверный!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close();

        }
    }
}
