// Created: 2022-05-18 by Adam Daa
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoReminder
{
    /// <summary>
    /// This class is responsible for handling all the details of a task
    /// </summary>
    class Task
    {
        private DateTime date;       // Date and time
        private string description;  // Description of the task
        private PriorityType priority; // Priority level

        /// <summary>
        /// A defualt constructor
        /// </summary>
        public Task()
        {
            priority = PriorityType.Normal;
        }

        /// <summary>
        /// A constructor with a one parameter
        /// </summary>
        /// <param name="taskDate"> Date and time</param>
        public Task(DateTime taskDate) : this(taskDate, string.Empty, PriorityType.Normal)
        {

        }

        /// <summary>
        /// A constructor with three parameter
        /// </summary>
        /// <param name="taskDate"> Date and time</param>
        /// <param name="description"> Description of the task</param>
        /// <param name="priority">The priority level of the task</param>
        public Task(DateTime taskDate, string description, PriorityType priority)
        {
            this.date = taskDate;
            this.description = description;
            this.priority = priority;
        }

        /// <summary>
        /// The description property
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    description = value;
            }
        }

        /// <summary>
        /// The Priority property
        /// </summary>
        public PriorityType Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        /// <summary>
        /// The date property
        /// </summary>
        public DateTime Date
        {
            set { date = value; }
            get { return date; }
        }

        /// <summary>
        /// Gets the time
        /// </summary>
        /// <returns>
        /// Returns a formated string for the time 
        /// </returns>
        public string GetTimeString()
        {
            string textOut = date.ToString("HH:mm:ss");
            return textOut;
        }

        /// <summary>
        /// Gets the priority
        /// </summary>
        /// <returns>
        /// Gets a string of the priorty after removing '_' 
        /// </returns>
        public string GetPriorityString()
        {
            string textOut = priority.ToString().Replace("_", " ");
            return textOut;
        }

        /// <summary>
        /// Formats all the details of a task object 
        /// </summary>
        /// <returns>
        /// Returns a string of all the details of a task object
        /// </returns>
        public override string ToString()
        {
            string textOut = String.Format("{0, -23} {1, 15} {2, 13} {3, 20}",
                date.ToLongDateString(),
                GetTimeString(),
                GetPriorityString(),
                Description);

            return textOut;
        }
    }
}
