// Created: 2022-05-18 by Adam Daa
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoReminder
{
    /// <summary>
    /// This class is responsible for managing the list of tasks
    /// </summary>
    class TaskManager
    {
        List<Task> taskList;  // To stor the list values

        public TaskManager()
        {
            taskList = new List<Task>();
        }

        /// <summary>
        /// Adds an item to the task list 
        /// </summary>
        /// <param name="newTask"></param>
        /// <returns>
        /// True if the process is done successfully 
        /// False if the process fails 
        /// </returns>
        public bool AddNewTask(Task newTask)
        {
            bool ok = true;

            if (newTask != null)
                taskList.Add(newTask);
            else
                ok = false;

            return ok;
        }

        /// <summary>
        /// Adds an item to the task list at specific index
        /// </summary>
        /// <param name="newTask"></param>
        /// <param name="index"></param>
        /// <returns>
        /// True if the process is done successfully 
        /// False if the process fails  
        /// </returns>
        public bool AddNewTaskAt(Task newTask, int index)
        {
            bool ok = true;
            if (newTask != null)
                taskList[index] = newTask;
            else
                ok = false;

            return ok;
        
        }

        /// <summary>
        /// Deletes an item at specific index of task list
        /// </summary>
        /// <param name="index"></param>
        /// <returns>
        /// True if the process is done successfully 
        /// False if the process fails
        /// </returns>
        public bool DeleteParticipantAt(int index)
        {
            if (CheckIndex(index))
            {
                taskList.RemoveAt(index);
                return true;
            }
            else
                return false;  
        }

        /// <summary>
        /// Checks if the index whithin range of the task list
        /// </summary>
        /// <param name="index"></param>
        /// <returns>
        /// True if the index within the range of the list
        /// False if the index out of the range
        /// </returns>
        private bool CheckIndex(int index)
        {
            return (index >= 0) && (index < taskList.Count);
        }

        /// <summary>
        /// Gets the whole list 
        /// </summary>
        /// <returns>
        /// A String array has all the elements of the task list
        /// Or a null if the list is empty 
        /// </returns>
        public string[] GetInfoStringsList()
        {
            if(taskList.Count != 0) 
            { 
                string[] infoStrings = new string[taskList.Count];

                for (int i = 0; i < infoStrings.Length; i++)
                {
                    infoStrings[i] = taskList[i].ToString();
                }
                return infoStrings;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Reads a task list from a saved text file
        /// </summary>
        /// <param name="fileName"> the directory and the name of the text file</param>
        /// <returns>
        /// True if the process is done successfully 
        /// False if the process fails 
        /// </returns>
        public bool ReadDataFromFile(string fileName)
        {
            FileManager fileManager = new FileManager();

            return fileManager.ReadTaskListfrFile(taskList, fileName);
        }

        /// <summary>
        /// Writes a task list into the a text file 
        /// </summary>
        /// <param name="fileName">The directory and the name of the text file</param>
        /// <returns>
        /// True if the process is done successfully 
        /// False if the process fails
        /// </returns>
        public bool WriteDataToFile(string fileName)
        {
            FileManager fileManager = new FileManager();

            return fileManager.SaveTaskListToFile(taskList, fileName);
        }
    }
}
