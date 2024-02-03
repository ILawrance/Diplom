using Npgsql;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public partial class AddQuestion : Form
    {
        NpgsqlConnection connection = new NpgsqlConnection(Connection.connectionStringSQL);

        public AddQuestion()
        {
            InitializeComponent();
            Querys.CreateTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Querys.QuestInsertInTable(richTextBox1, pictureBox1);
            richTextBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(); 
            mainForm.Show();
            this.Close();
        }
    }
}

/// <summary>
///  Идея для кода ниже
/// </summary>
/*
class Program
{
    static void Main()
    {
        string connectionString = "YourConnectionString";

        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string query = $"SELECT image, text FROM public.questionsimage LIMIT 1"; // Пример: ограничиваем выборку одной записью (or WHERE quesionID = {id})

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        byte[] imageData = (byte[])reader["image"];

                        // Отображение изображения в PictureBox
                        DisplayImage(imageData);
                    }
                }
            }
        }
    }

    static void DisplayImage(byte[] imageData)
    {
        if (imageData == null || imageData.Length == 0)
        {
            Console.WriteLine("Нет данных изображения.");
            return;
        }

        using (MemoryStream ms = new MemoryStream(imageData))
        {
            Image image = Image.FromStream(ms);

            // Создайте форму и PictureBox для отображения изображения
            Form form = new Form();
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = image;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

            form.Controls.Add(pictureBox);

            // Отобразите форму
            Application.Run(form);
        }
    }
} */