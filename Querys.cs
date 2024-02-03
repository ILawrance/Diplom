using System;
using System.IO;
using System.Windows.Forms;
using Npgsql;

namespace Diplom
{
    public static class Querys
    {
        public static NpgsqlConnection connection = new NpgsqlConnection(Connection.connectionStringSQL);
        public static void CreateTable()
        {
            try {
            string queryCreate = $@"
                CREATE TABLE IF NOT EXISTS QuestionsImage (
                QuestionImageID SERIAL PRIMARY KEY,
                Image bytea NOT NULL,
                Text VARCHAR(100) NOT NULL
                )";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(queryCreate, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Таблица QuestionsImage успешно создана.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании таблицы: {ex.Message}");
            }
            finally 
            { 
                connection.Close();
            }

        }
        
        public static void QuestInsertInTable(RichTextBox textBox, PictureBox pictureBox)
        {
            Byte[] imageData;
            try
            {

                using (MemoryStream ms =  new MemoryStream())
                {
                    pictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageData = ms.ToArray();
                }

                string queryInsert = @"
                    INSERT INTO public.questionsimage (image, text) 
                    VALUES 
                    (@Image, @Text)";

                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(queryInsert, connection)) 
                {
                    command.Parameters.AddWithValue("@Image", NpgsqlTypes.NpgsqlDbType.Bytea, imageData);
                    command.Parameters.AddWithValue("@Text", textBox.Text);
                    command.ExecuteNonQuery();
                    Console.WriteLine("В таблицу questionsimage успешно добавлены данные.");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении данных: {ex.Message}");
            }

            finally
            {
                connection.Close();
            }
        }
        public static void QuestDeleteTable()
        {

        }
        public static void QuestUpdateTable() 
        {

        }
    }
}
