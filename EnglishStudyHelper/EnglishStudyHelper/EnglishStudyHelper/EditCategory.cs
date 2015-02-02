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
    public partial class EditCategory : Form
    {
        FormCategory frmCateg;

        public EditCategory()
        {
            InitializeComponent();
        }

        public EditCategory(FormCategory frmCateg)
        {
            InitializeComponent();
            this.frmCateg = frmCateg;
        }

      
    

        private void button1_Click(object sender, EventArgs e)
        {
            DBManager db = new DBManager();
            db.InsertCategory(textCategory.Text);
            frmCateg.SetGridDatasourse(db.SelectCategories());
        }
    }
}
