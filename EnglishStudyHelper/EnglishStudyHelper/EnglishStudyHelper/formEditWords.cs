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
    public partial class FormEditWords : Form
    {
        FormWords frmWords;
       
        public FormEditWords()
        {
            InitializeComponent();
        }

        public FormEditWords(FormWords frm)
        {
            InitializeComponent();
            this.frmWords = frm;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddWord_Click(object sender, EventArgs e)
        {
            DBManager db = new DBManager();
            
            if (textWord.Text != "" && textTranslate.Text != "")
            {

                db.InsertWordTrans(textWord.Text, textTranslate.Text);
                frmWords.SetGridDatasourse( db.SelectWords());
            }
            
        } 
    }
}
