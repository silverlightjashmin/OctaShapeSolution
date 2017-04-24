using OctaShape.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaShape.Common
{
    public  class ExportToText
    {
       
    
    public void ToText(List<Card_RequestDetail> ExportData,string filename)
{
            //create folder if not exist
            String folername = ConfigurationManager;
            System.IO.Directory.CreateDirectory(pathString);

            //create file 
            FileStream fs1 = new FileStream("E:\\Card Requested\"+filename" + ".txt"

            , FileMode.OpenOrCreate, FileAccess.Write);


            //write data to file
        TextWriter tw = new StreamWriter(fs1);



        foreach (var item in ExportData)
        {
            tw.WriteLine(item.ToString());
        }

        //writer close
        tw.Close();

            //open text file to see


    }

    }
}
