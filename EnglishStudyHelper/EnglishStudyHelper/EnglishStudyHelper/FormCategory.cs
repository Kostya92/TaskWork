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
    public partial class FormCategory : Form
    {
        EditCategory frmEditCat;
        FormWordsToCat frmWords;
        string idCat = ""; //ид выбранной категории для передачи в дочернюю форму

        /*
            При двойном клике по категории открывается список слов 
         * слова можно выделить и добавить в данную категорию 
         */

        public FormCategory()
        {
            InitializeComponent();
            frmEditCat = new EditCategory(this);
            frmWords = new FormWordsToCat(this);

        }

        private void SetIdCat(String idCat)
        {
            this.idCat = idCat;
        }

        public string GetIdCat()
        {
            return idCat;
        }

        public void SetGridDatasourse(DataTable dt)
        {
            gridCategory.DataSource = dt;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormCategory_Load(object sender, EventArgs e)
        {
            DBManager db = new DBManager();
            gridCategory.DataSource = db.SelectCategories();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmEditCat.ShowDialog();
        }

        private void gridCategory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SetIdCat(gridCategory[0, e.RowIndex].Value.ToString());

       
            frmWords.ShowDialog();
        }

     
    }
}
