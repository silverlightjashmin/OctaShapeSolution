using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Web;

namespace OctaShape.Common
{
    public class ExportToExcel
    {
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }

                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }

                    dataTable.Rows.Add(values);

                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }

        
        public void Excel2(DataTable table, string filename)
        {
            //call dynamic folder path
            string folderName = @"E:\Card Requested To Card Issuer";

            string pathString = System.IO.Path.Combine(folderName, DateTime.Now.Date.Year.ToString(), DateTime.Now.Date.Month.ToString());

            System.IO.Directory.CreateDirectory(pathString);

           
            pathString = System.IO.Path.Combine(pathString, filename);


                    /*Set up work book, work sheets, and excel application*/
                    Microsoft.Office.Interop.Excel.Application oexcel = new Microsoft.Office.Interop.Excel.Application();

            try

            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                object misValue = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Workbook obook = oexcel.Workbooks.Add(misValue);
                Microsoft.Office.Interop.Excel.Worksheet osheet = new Microsoft.Office.Interop.Excel.Worksheet();


                //  obook.Worksheets.Add(misValue);

                osheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Sheets["Sheet1"];
                int colIndex = 0;
                int rowIndex = 3;

                //fixed data 
              
                osheet.Cells[1, 0] = "Request Date";
                osheet.Cells[2, 0] = "Bank Name";
                osheet.Cells[1, 1] = DateTime.Now.ToString("yyyy-MM-dd");
                osheet.Cells[2, 1] = "Shangrila Development Bank Limited";
                /*
              osheet.Cells[3,0] = "BIN NO";
              osheet.Cells[3, 1] = "BRANCH";
              osheet.Cells[3, 2] = "A/C NO";
              osheet.Cells[3, 3] = "OPENING DATE";
              osheet.Cells[3, 4] = "ACTYPE";
              osheet.Cells[3, 5] = "IMPORTED";
              osheet.Cells[3, 6] = "CURRENCY";
              osheet.Cells[3, 7] = "CUSTOMER NAME";
              osheet.Cells[3, 8] = "REMARKS";
              osheet.Cells[3, 9] = "EXISTING CARD NO";
              */
                //auto column name
                foreach (DataColumn dc in table.Columns)
                {
                    colIndex++;
                    osheet.Cells[3, colIndex] = dc.ColumnName;
                }


                foreach (DataRow dr in table.Rows)
                {
                    rowIndex++;
                    colIndex = 0;

                    foreach (DataColumn dc in table.Columns)
                    {
                        colIndex++;
                        osheet.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                    }
                }

                osheet.Columns.AutoFit();
              
                //save excel
                obook.SaveAs(filename);
               // obook.Open();
                obook.Close();
                oexcel.Quit();
               
                GC.Collect();
            }
            catch (Exception)
            {
                oexcel.Quit();
              
            }
        }


    }
}
