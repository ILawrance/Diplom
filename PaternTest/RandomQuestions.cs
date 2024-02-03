using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Diplom.PaternTest
{
    public partial class RandomQuestions : Form
    {
        Dictionary<string, Bitmap> dict = new Dictionary<string, Bitmap>();
        Bitmap[] bitmapsCheck = new Bitmap[5];
        public RandomQuestions()
        {
            InitializeComponent();
            Random random = new Random();
            string path = @"D:\zSOFT\VisualStudioCommuniti\VSProjects\sourses\Diplom\Resources";
            List<int> povtorQuests = new List<int>();
            List<int> povtorImg = new List<int>();
            bool isRelevant = false;
            PictureBox[] pictureBoxes = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };
            Label[] labels = { label2, label3, label4, label5, label6 };
            List<string> allImageNames = Directory.GetFiles($@"{path}\Images", "*.*", SearchOption.TopDirectoryOnly).ToList<string>();
            List<Bitmap> bitmaps = new List<Bitmap>();


            foreach (string fileName in allImageNames)
            {
                try
                {
                    bitmaps.Add(new Bitmap(fileName));
                }
                catch { continue; }
            }

            IEnumerable<string> AllLines = File.ReadAllLines($@"{path}\Questions\Questions.txt", Encoding.UTF8);
            List<string> AllQuestions = AllLines.ToList();

            while (!isRelevant)
            {
           
                dict.Clear();
                for (int i = 0; i < 5; i++)
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

                for (int i = 0; i < bitmaps.Count(); i++)
                {
                    dict.Add(AllQuestions[i], bitmaps[i]);
                }

                for (int i = 0; i < 5; i++)
                {
                    int rand = random.Next(0, bitmaps.Count());
                    while (povtorImg.Contains(rand))
                    {
                        rand = random.Next(0, bitmaps.Count());
                    }
                    pictureBoxes[i].Image = bitmaps[rand];
                    bitmapsCheck[i] = bitmaps[rand];
                    povtorImg.Add(rand);
                }
                povtorImg.Clear();

                int count = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (dict[labels[i].Text] == bitmapsCheck[i])
                    {
                        count++;
                        isRelevant = true;
                    }
                    
                }
                if (count != 1)
                {
                    isRelevant = false;
                }
            }

            int variant = random.Next(0, 100);
            if (variant > 49)
            {
                VariableLabel.Text = "Сопоставьте формулу с текстом";
                for (int i = 0; i < labels.Length; i++)
                {
                    labels[i].Location = new Point(240, labels[i].Location.Y);
                }
                for (int i = 0; i < pictureBoxes.Length; i++)
                {
                    pictureBoxes[i].Location = new Point(90, pictureBoxes[i].Location.Y);
                }
            }
            else
            {
                VariableLabel.Text = "Сопоставьте текст с формулой";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked && dict[label2.Text] == bitmapsCheck[0])
            {
                label7.Text = "Ваш ответ: \nВерный";
            }
            else if (radioButton2.Checked && dict[label3.Text] == bitmapsCheck[1])
            {
                label7.Text = "Ваш ответ: \nВерный";
            }
            else if(radioButton3.Checked && dict[label4.Text] == bitmapsCheck[2])
            {
                label7.Text = "Ваш ответ: \nВерный";
            }
            else if(radioButton4.Checked && dict[label5.Text] == bitmapsCheck[3])
            {
                label7.Text = "Ваш ответ: \nВерный";
            }
            else if (radioButton5.Checked && dict[label6.Text] == bitmapsCheck[4])
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
