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
    public partial class FormWordsToCat : Form
    {
        FormCategory frmCategor;

        public FormWordsToCat()
        {
            InitializeComponent();
        }

        public FormWordsToCat(FormCategory frm)
        {
            InitializeComponent();
            this.frmCategor = frm;
        }


        private void FormWordsToCat_Load(object sender, EventArgs e)
        {
            DBManager db = new DBManager();
            gridWords.DataSource = db.SelectWordsForCat();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string words = "";
            string idCtg = frmCategor.GetIdCat();

            for (int i = 0; i < gridWords.RowCount; i++)
            {
                if (gridWords[0, i].Selected)
                {
                    words = words + gridWords[0, i].Value.ToString() + ", ";
                }
            }

            char[] charsToTrim = {',', ' '};
            words = words.TrimEnd(charsToTrim);
           
            DBManager db = new DBManager();
            db.UpdateWordCategory(idCtg, words);
            frmCategor.SetGridDatasourse(db.SelectCategories());
            Close();
        }

        private void gridWords_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.gridWords.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.gridWords.Rows[index].HeaderCell.Value = indexStr; 
        }

    




    }
}
