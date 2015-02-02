using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.SQLite;

namespace EnglishStudyHelper
{
    public partial class FormWords : Form
    {
        FormEditWords frmEdit;
        DBManager db;

        public FormWords()
        {
            InitializeComponent();
            frmEdit = new FormEditWords(this);
            db = new DBManager();
        }

    
        public  void SetGridDatasourse(DataTable dt)
        {
            gridWords.DataSource = dt;
        }


        private void FormWords_Shown(object sender, EventArgs e)
        {

        }

        private void bntNew_Click(object sender, EventArgs e)
        {
            frmEdit.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormWords_Load(object sender, EventArgs e)
        {

            gridWords.DataSource = db.SelectWords();

           
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            
        }

        private void gridWords_DataSourceChanged(object sender, EventArgs e)
        {
          

        }

    }
}
