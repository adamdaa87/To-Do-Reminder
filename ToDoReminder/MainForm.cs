// Created: 2022-05-18 by Adam Daa
// This is the form class which recives all the inputs and displays all the outputs
// for "To Do Reminder" app
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoReminder
{
    /// <summary>
    /// This project works as To do remider list
    /// It allows the user to make a list of activities and a schedule time for each activity
    /// This projects gives the ability to change the details of an activity in the list 
    /// or even deleting an item.
    /// As well as printing all the activities and saving them into a txt file and loading them
    /// </summary>
    public partial class MainForm : Form
    {
        private TaskManager taskManager; // Handles all list of activities

        private StringReader myReader;  // A reader to read my current list in the list box (for printing) 
     
        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
          
            this.printDocument1.PrintPage +=    // For printing 
                new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
        }
        /// <summary>
        /// Initialize the GUI components
        /// </summary>
        private void InitializeGUI()
        {
            this.Text = "ToDo Reminder by Adam Daa";  

            taskManager = new TaskManager();

            cmbPriority.Items.Clear();
            cmbPriority.Items.AddRange(Enum.GetNames(typeof(PriorityType)));
            cmbPriority.SelectedIndex = (int)PriorityType.Normal;

            lstTasks.Items.Clear();
            lblClock.Text = string.Empty;
            clockTimer.Start();

            txtDescription.Text = string.Empty;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd   HH:mm";

            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(dateTimePicker1, "Click to open calender for date, write in time here.");
            toolTip1.SetToolTip(cmbPriority, "Select the priority");

            string tips = "TO CHANGE: Select an item first!" + Environment.NewLine;
            tips += "Make your changes in the input controls," + Environment.NewLine;
            tips += "Click the Change button." + Environment.NewLine;

            toolTip1.SetToolTip(lstTasks, tips);
            toolTip1.SetToolTip(btnChange, tips);

            string delTips = "Select an item first and then click this button!";
            toolTip1.SetToolTip(btnDelete, delTips);

            string desTips = "Write your sins here!";
            toolTip1.SetToolTip(txtDescription, desTips);

            menuFileOpen.Enabled = true;
            menuFileSave.Enabled = true;

            btnChange.Enabled = false;
            btnDelete.Enabled = false;
        }

        /// <summary>
        /// Trigers the about window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }

        /// <summary>
        /// Trigers the exit action to close the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Are you sure you want to exit?", "Think twice", MessageBoxButtons.OKCancel);
            if (dlgResult == DialogResult.OK)
                Application.Exit();
        }

        /// <summary>
        /// Changes a selected item in the list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChange_Click(object sender, EventArgs e)
        {
            int index = IsListBoxItemSelected();
            if (index < 0)
                return;

            Task task = ReadInput();

            if (task != null)
            {
                taskManager.AddNewTaskAt(task, index);
                UpdateGUI();
            }
        }

        /// <summary>
        /// Checks if an item in the list box is selected
        /// </summary>
        /// <returns>
        /// Returns the index of the selected item
        /// Or if no item is selected, it returns -1
        /// </returns>
        private int IsListBoxItemSelected()
        {
            int index = lstTasks.SelectedIndex;

            if (lstTasks.SelectedIndex < 0)
            {
                MessageBox.Show("Select an item in the list");
                return -1;
            }
            else
            {
                return index;
            }
        }

        private void lblClock_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Trigers an event at every second and updates the clock label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clockTimer_Tick_1(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToLongTimeString();
        }

        /// <summary>
        /// Allwos to add a new item in the list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Task task = ReadInput();
            if (taskManager.AddNewTask(task))
            {
                UpdateGUI();
            }
        }

        /// <summary>
        /// Updates GUI and enables the change button and delete button
        /// if there is an item in the list box.
        /// If there is no item the buttons (change and delete) will be disabled
        /// </summary>
        private void UpdateGUI()
        {
            lstTasks.Items.Clear();
            string[] infoString = taskManager.GetInfoStringsList();
            if (infoString != null)
            {
                lstTasks.Items.AddRange(infoString);
                btnChange.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnChange.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        /// <summary>
        /// Reads the input boxes and of the task
        /// </summary>
        /// <returns>
        /// Returns a task if it is not null
        /// Reurns a null if the description box is empty 
        /// </returns>
        private Task ReadInput()
        {
            Task task = new Task();
            if (ReadDescription(task))
            {
                task.Priority = (PriorityType)cmbPriority.SelectedIndex;
                task.Date = dateTimePicker1.Value;
                return task;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Reads the decription box
        /// </summary>
        /// <param name="task"></param>
        /// <returns>
        /// Returns true if thr process is fine 
        /// Rturns false if the txtDescription is empty or the value  
        /// </returns>
        bool ReadDescription(Task task)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                MessageBox.Show("Please, provide a description to the activity");
                return false;
            }
            else
            {
                task.Description = txtDescription.Text.Trim();
                return true;
            }
        }

        /// <summary>
        /// Initializes the whole window and the list box when we click on "New" in the file menu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InitializeGUI();
        }

        /// <summary>
        /// This method is triggered when we click on "file save" in the file tab
        /// It allows us to save the data in the list box into a text file  
        /// according to the specified directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSave_Click(object sender, EventArgs e)
        {
            string errMessage = "Something went wrong while saving data to file";  //error message
            bool ok = false;   
            string path = "";  // A directory

            SaveFileDialog sfd = new SaveFileDialog();  // Save file dialog box
            sfd.Title = "Save File To";  // Changes the title
            sfd.Filter = "|*.txt";       // Specifies the default type of the file

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                path = sfd.FileName;   // Assigns a directory and a file name from "SaveFileDialog"
                ok = taskManager.WriteDataToFile(path);  // Saves the data
            }

            if (!ok)
                MessageBox.Show(errMessage);
            else
                MessageBox.Show("Data saved to file:" + Environment.NewLine + path);
            
        }

        /// <summary>
        /// This method is triggerd when we clik on open file in the file tab
        /// It opens a text file from a saved file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            string errMessage = "Something went wrong while opening the file";
            bool ok = false;
            
            OpenFileDialog ofd = new OpenFileDialog();  // Opens the file dialog 
            ofd.Title = "Open File";                    // Changes the title
            ofd.Filter = "|*.txt";                      // Specifies the default type of the file

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ok = taskManager.ReadDataFromFile(ofd.FileName);   //Reads the saved text file
            }

            if (!ok)
                MessageBox.Show(errMessage);
            else
                UpdateGUI();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Delets an item from the list box and the real list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = IsListBoxItemSelected();
            if (index < 0)
                return;

            taskManager.DeleteParticipantAt(index);
            UpdateGUI();
        }

        /// <summary>
        /// Shows the print dialog for a printing process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDialog_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            string strText = "";
            foreach (object x in lstTasks.Items)
            {
               strText = strText + x.ToString() + "\n";
            }
            
            myReader = new StringReader(strText);
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.Print();
            }

        }

        /// <summary>
        /// Takes the document and reads it and print it out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float linesPerPage = 0;
            float yPosition = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;
            Font printFont = this.lstTasks.Font;
            SolidBrush myBrush = new SolidBrush(Color.Black);
            
             // Works out the number of lines per page, using the MarginBounds.
            linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            // Iterate over the string using the StringReader, printing each line.
            while (count < linesPerPage && ((line = myReader.ReadLine()) != null))
            {
                // calculates the next line position based on
                // the height of the font according to the printing device
                 yPosition = topMargin + (count * printFont.GetHeight(e.Graphics));
                
                // draw the next line in the rich edit control

                e.Graphics.DrawString(line, printFont,
                                                myBrush, leftMargin,
                                                yPosition, new StringFormat());
                count++;
            }           
            // If there are more lines, print another page.
            if (line != null)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
           
            myBrush.Dispose();
        }
    }
}