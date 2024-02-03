using Diplom.PaternTest;
using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test1Vopros1 test1Vopros1 = new Test1Vopros1();
            test1Vopros1.Show();
            this.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
           RandomQuestions randomQuestions = new RandomQuestions();
           randomQuestions.Show();
           this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            TargetInQuestion targetInQuestion = new TargetInQuestion();
            targetInQuestion.Show();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AddQuestion addQuestion = new AddQuestion();
            addQuestion.Show();
            this.Close();
        }
    }
}
