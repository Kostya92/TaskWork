using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace EnglishStudyHelper
{
    
    class DBManager
    {
        private String conString;
        private SQLiteConnection sqlCon;
        private SQLiteCommand sqlCmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
     
       
        public DBManager()
        {
            conString = "Data Source=Helper.db;Version=3;New=False;Compress=True;";
        }

        /************************************************************/
        //общий метод для выполнения запросов, не возвр. набор данных
        /************************************************************/
        private void ExecuteQuery(string txtQuery)
        {
            using (sqlCon = new SQLiteConnection(conString))
            {
                try
                {
                    sqlCon.Open();
                    sqlCmd = sqlCon.CreateCommand();
                    sqlCmd.CommandText = txtQuery;
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }

            }
        }

        /**********************************************************/
        //общий метод для выполнения запросов, возвр. набор данных
        /*************************************************************/
        private DataTable LoadData(string commandText, string paramName = "",  ValueType value = null)
        {
          
             using (sqlCon = new SQLiteConnection(conString))
            {
                try
                {
                    sqlCon.Open();
                  
                    sqlCmd = sqlCon.CreateCommand();

                    DB = new SQLiteDataAdapter(commandText, sqlCon);
                    DS.Reset();
                    DB.Fill(DS);
                    DT = DS.Tables[0];
                   
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }

            }

            return DT;
        }

        /**************************************************************************************/
        /*запросы к таблице words (id integer primarykey autoincrement, wordEN text, wordRU text)
        /****************************************************************************************/
        public DataTable SelectWords()
        {
            return LoadData("SELECT w.id, w.wordEN, w.wordRU, c.name as category FROM words w "
                                           + "LEFT OUTER JOIN categories c ON w.catId = c.id");

        }

        public DataTable SelectWordsForTest()
        {

            return LoadData("SELECT w.wordEN,  w.wordRU, c.name as category FROM words w "
                 + "LEFT OUTER JOIN categories c ON w.catId = c.id where c.isSelected = 1");
                                           
        }

        public DataTable SelectWordsForCat()
        {
            return LoadData("SELECT id, wordEN FROM words ");


        }

        public void InsertWordTrans(String word, String trans)
        {
            ExecuteQuery("insert into words (wordEN, wordRU) values ('" + word + "','" + trans + "')");
        }

        public void UpdateWordCategory(string catId, string wordsId)
        {

            ExecuteQuery("update words set catId = " + catId + " where id in (" + wordsId + ")");
        }

        /*******************************************************************************************/
        //запросы к таблице categories
        /*******************************************************************************************/

        public DataTable SelectCategories()
        {
            return LoadData("SELECT c.id, name, count(w.id) as words FROM categories c "
                                           + "LEFT JOIN words w ON w.catId = c.id GROUP BY name");
        }

        public DataTable SelectCategoriesSettings()
        {
            return LoadData("SELECT c.id, name, isSelected FROM categories c ");
        }

 

 
        public void InsertCategory(string name)
        {
            ExecuteQuery("insert into categories (name, isSelected) values ('" + name + "', 0)");
        }

        public void UpdateCategory(String name, int flag)
        {
            ExecuteQuery("update categories set isSelected = " + flag + " where name = '" + name + "'");
        }

        /*********************************************************************************************/
        //запросы к таблице statistic
        /*********************************************************************************************/
        public DataTable SelectStatisticByDate(double date)
        {
            return LoadData("select id from statistic where date = " + date + "");
        }
        
        
        public void InsertOrUpdateStatistic(int answer, double date)
        {
            DataTable dt = SelectStatisticByDate(date);
            String fieldName;

            if (answer == 1)
                fieldName = "trueCount";
            else
                fieldName = "falseCount";


            if (dt.Rows.Count == 0)
                ExecuteQuery("insert into statistic (date, " + fieldName + ") values (" + date + ", " + answer + ")");
            else
                ExecuteQuery("update statistic set " + fieldName + " = " + fieldName + " + 1 where date = " + date + "");
        }
    }
}
