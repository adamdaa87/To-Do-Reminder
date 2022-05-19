// Created: 2022-05-18 by Adam Daa
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ToDoReminder
{
    /// <summary>
    /// This class is responsible for managing the writing and reading 
    /// process for the text file. 
    /// </summary>
    internal class FileManager
    {
        private const string fileVersionToken = "ToDoRe_21";  // Token

        private const double fileVersionNr = 1.0; // Version

        /// <summary>
        /// Writes into a text file
        /// </summary>
        /// <param name="taskList"> The task list which will be copied</param>
        /// <param name="fileName"> The directory of the text file </param>
        /// <returns>
        /// True if the process is done successfully 
        /// False if the process fails
        /// </returns>
        public bool SaveTaskListToFile(List<Task> taskList, string fileName)
        {
            bool ok = true;
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(fileName);
                writer.WriteLine(fileVersionToken);
                writer.WriteLine(fileVersionNr);
                writer.WriteLine(taskList.Count);

                for (int i = 0; i < taskList.Count; i++)
                {
                    writer.WriteLine(taskList[i].Description);
                    writer.WriteLine(taskList[i].Priority.ToString());
                    writer.WriteLine(taskList[i].Date.Year);
                    writer.WriteLine(taskList[i].Date.Month);
                    writer.WriteLine(taskList[i].Date.Day);
                    writer.WriteLine(taskList[i].Date.Hour);
                    writer.WriteLine(taskList[i].Date.Minute);
                    writer.WriteLine(taskList[i].Date.Second);
                    
                }
            }
            catch
            {
                ok = false;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
            return ok;
        }

        /// <summary>
        /// Reads from a text file to a list 
        /// </summary>
        /// <param name="taskList"></param>
        /// <param name="fileName"></param>
        /// <returns>
        /// True if the process is done successfully 
        /// False if the process fails 
        /// </returns>
        public bool ReadTaskListfrFile(List<Task> taskList, string fileName)
        {
            bool ok = true;
            StreamReader reader = null;

            try
            {
                if (taskList != null)
                    taskList.Clear();
                else
                    taskList = new List<Task>();

                reader = new StreamReader(fileName);

                string versionTest = reader.ReadLine();

                double version = double.Parse(reader.ReadLine());

                if ((versionTest == fileVersionToken) && (version == fileVersionNr))
                {
                    int count = int.Parse(reader.ReadLine());
                    for (int i = 0; i < count; i++)
                    {
                        Task task = new Task();
                        task.Description = reader.ReadLine();
                        task.Priority = (PriorityType)Enum.Parse(typeof(PriorityType), reader.ReadLine());
                 
                        int year = 0, month = 0, day = 0;
                        int hour = 0, minute = 0, second = 0;

                        year = int.Parse(reader.ReadLine());
                        month = int.Parse(reader.ReadLine());
                        day = int.Parse(reader.ReadLine());
                        hour = int.Parse(reader.ReadLine());    
                        minute = int.Parse(reader.ReadLine());  
                        second = int.Parse(reader.ReadLine());
                     
                        task.Date = new DateTime(year, month, day, hour, minute, second);

                        taskList.Add(task);
                    }
                }
                else
                {
                    ok = false;
                }
            }
            catch
            {
                ok=false;
            }
            finally
            {
                if(reader != null)
                    reader.Close();
            }
            return ok;
        }
    }
}
