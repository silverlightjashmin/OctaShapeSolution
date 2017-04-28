using OctaShape.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaShape.Common
{
    public class ExportToText
    {


        public void ToText(List<Card_RequestDetail> ExportData, string filename)
        {

            string folderName = @"E:\Card Requested To Card Issuer";


            string pathString = System.IO.Path.Combine(folderName, DateTime.Now.Date.Year.ToString(), DateTime.Now.Date.Month.ToString());

            System.IO.Directory.CreateDirectory(pathString);


            string fileName = filename + ".txt";
            pathString = System.IO.Path.Combine(pathString, fileName);

            //create file 
            if (!System.IO.File.Exists(pathString))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(pathString, FileMode.OpenOrCreate, FileAccess.ReadWrite))

                {


                    using (System.IO.StreamWriter file = new StreamWriter(fs))
                    {
                        foreach (var item in ExportData)
                        {
                        
                            file.Write(item.Branch_Code.ToString());
                            file.WriteLine(item.Account_No.ToString());
                            file.WriteLine(item.EmbossName.ToString());
                            file.WriteLine(item.Card_RequestType.Request_Name.ToString());

                        }
                    }
                }
            }

            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
                return;
            }

            var process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = pathString
            };

            process.Start();
            //process.WaitForExit();


        }

        public void Export<T>(List<T> ExportData, string filename)  
        {

            string folderName = @"E:\Card Requested To Card Issuer";


            string pathString = System.IO.Path.Combine(folderName, DateTime.Now.Date.Year.ToString(), DateTime.Now.Date.Month.ToString());

            System.IO.Directory.CreateDirectory(pathString);


            string fileName = filename + ".txt";
            pathString = System.IO.Path.Combine(pathString, fileName);

            //create file 
            if (!System.IO.File.Exists(pathString))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(pathString, FileMode.OpenOrCreate, FileAccess.ReadWrite))

                {


                    using (System.IO.StreamWriter file = new StreamWriter(fs))
                    {
                        foreach (T item in ExportData)
                        {

                            file.Write(item.ToString());
                           

                        }
                    }
                }
            }

            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
                return;
            }

            var process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = pathString
            };

            process.Start();
            //process.WaitForExit();


        }







    }

    
    
    }