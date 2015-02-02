using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EnglishStudyHelper
{
    //-------------////
    public partial class FormMain : Form
    {
        public FormWords formWords;
        public FormCategory formCat;
        public FormSetting formSet;
        private int trueAnswerPos;
        private RadioButton[] arrRadioBtn;
        public int timeShowTest = 60000; 


        public FormMain()
        {
            InitializeComponent();
            formWords = new FormWords();
            formCat = new FormCategory();
            formSet = new FormSetting(this);
           
        }

     /*
        При клике на answer или i don't now форма сварачивается в трей 
      * и по таймеру потом опять появляется 
      */
      
        private void FormMain_Load(object sender, EventArgs e)
        {
            arrRadioBtn = new RadioButton[5];
      
            arrRadioBtn[0] = rbtnAnswer1;
            arrRadioBtn[1] = rbtnAnswer2;
            arrRadioBtn[2] = rbtnAnswer3;
            arrRadioBtn[3] = rbtnAnswer4;
            arrRadioBtn[4] = rbtnAnswer5;

            timer1.Interval = timeShowTest;
            timer1.Start();
            SetRandomWord();
        }

    
        
        private void FormMain_Resize(object sender, EventArgs e)
        {

            HideMainWindow();

        }
        
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowMainWindow();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            //ShowMainWindow();
        }

        private void wordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formWords.ShowDialog();
        }

     
        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formCat.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HideMainWindow()
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        private void ShowMainWindow()
        {
            SetRandomWord();
            Show();
            WindowState = FormWindowState.Normal;
            
        }

        private void TimerEventProcessor(Object myObject,
                                           EventArgs myEventArgs)
        {
            ShowMainWindow();
        }

        //заполняет елементы главной формы случайными словом и переводами к нему
        private void SetRandomWord()
        {
            DBManager db = new DBManager();
            DataTable dt =  db.SelectWordsForTest();
            Random rnd;
          
            int[] arrRand = new int[5];
            int highRand = dt.Rows.Count;
            int seedRand;

            rnd = new Random();
            seedRand = rnd.Next(1, int.MaxValue);
            rnd = new Random(seedRand);
        
            //массив случайных чисел - используются в качестве индексов строк со словами-переводами 
            for (int i = 0; i < 5; i++)
            {
                arrRand[i] = rnd.Next(0, highRand);
            }

           

            //случайная позиция радиокнопки для правильного ответа
            //чтобы он не был всегда первым
            rnd = new Random();
            SetTrueAnswerPos(rnd.Next(0, 4));


            RadioButton temp = arrRadioBtn[trueAnswerPos];
            arrRadioBtn[trueAnswerPos] = arrRadioBtn[0];
            arrRadioBtn[0] = temp;

            lblCategory.Text = dt.Rows[arrRand[0]]["category"].ToString();
            lblWord.Text = dt.Rows[arrRand[0]]["wordEN"].ToString();
            


            for (int i = 0; i < arrRadioBtn.Length; i++)
            {
              arrRadioBtn[i].Text = dt.Rows[arrRand[i]]["wordRU"].ToString();
            }

            temp = arrRadioBtn[trueAnswerPos];
            arrRadioBtn[trueAnswerPos] = arrRadioBtn[0];
            arrRadioBtn[0] = temp;

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowMainWindow();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            DBManager db = new DBManager();
            int answer = 0;

            

            for (int i = 0; i < 5; i++)
            {
                if (arrRadioBtn[i].Checked)
                {
                    if (i == trueAnswerPos) 
                        answer = 1;
         
                    db.InsertOrUpdateStatistic(answer, ConvertToUnixTimestamp(DateTime.Today));
                    
                }
            }

            WindowState = FormWindowState.Minimized;
 
        }

        private void SetTrueAnswerPos(int pos)
        {
            this.trueAnswerPos = pos;
        }

        private int GetTrueAnswerPos()
        {
            return this.trueAnswerPos;
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formSet.ShowDialog();
        }

       
     
        private void btnNoAns_Click(object sender, EventArgs e)
        {
            DBManager db = new DBManager();

            db.InsertOrUpdateStatistic(0, ConvertToUnixTimestamp(DateTime.Today));

            WindowState = FormWindowState.Minimized;
        }

     
      
    }
}
