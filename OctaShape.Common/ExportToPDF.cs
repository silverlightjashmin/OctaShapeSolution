using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Http;
using System.Web.UI;
using System;

namespace OctaShape.Common
{
    public class ExportToPDF
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

        public void Export2pdf(DataTable datatable, string filename)    
        {


            string folderName = @"E:\Card Requested To Card Issuer";


            string pathString = System.IO.Path.Combine(folderName, DateTime.Now.Date.Year.ToString(), DateTime.Now.Date.Month.ToString());

            System.IO.Directory.CreateDirectory(pathString);


            string fileName = filename + ".pdf";
            pathString = System.IO.Path.Combine(pathString, fileName);

            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pathString, FileMode.OpenOrCreate, FileAccess.ReadWrite));
            document.Open();

            PdfPTable table = new PdfPTable(datatable.Columns.Count);
            table.WidthPercentage = 100;

            //Set columns names in the pdf file
            for (int k = 0; k < datatable.Columns.Count; k++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(datatable.Columns[k].ColumnName));

                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.BackgroundColor = new iTextSharp.text.BaseColor(51, 102, 102);

                table.AddCell(cell);
            }

            //Add values of DataTable in pdf file
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                for (int j = 0; j < datatable.Columns.Count; j++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(datatable.Rows[i][j].ToString()));

                    //Align the cell in the center
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                    table.AddCell(cell);
                }
            }

            document.Add(table);
            document.Close();
        }

        
    }
}
