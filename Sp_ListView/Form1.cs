using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Sp_ListView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PopulateProcessList();
        }
        private void PopulateProcessList()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("PID", 70);
            listView1.Columns.Add("Name", 150);
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                ListViewItem item = new ListViewItem(process.Id.ToString());
                item.SubItems.Add(process.ProcessName);
                listView1.Items.Add(item);
            }
        }
        private void listViewProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                int processId = int.Parse(selectedItem.Text);
                try
                {
                    Process selectedProcess = Process.GetProcessById(processId);
                    DisplayThreads(selectedProcess);
                    DisplayModules(selectedProcess);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void DisplayThreads(Process process)
        {
            listView2.Items.Clear();
            listView2.View = View.Details;
            listView2.Columns.Add("ID", 50);
            listView2.Columns.Add("Priority", 70);
            listView2.Columns.Add("Start Time", 120);
            try
            {
                ProcessThreadCollection threads = process.Threads;
                foreach (ProcessThread thread in threads)
                {
                    ListViewItem item = new ListViewItem(thread.Id.ToString());
                    item.SubItems.Add(thread.PriorityLevel.ToString());
                    item.SubItems.Add(thread.StartTime.ToString());
                    listView2.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching threads: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DisplayModules(Process process)
        {
            listView3.Items.Clear();
            listView3.View = View.Details;
            listView3.Columns.Add("Name", 200);
            listView3.Columns.Add("Path", 300);
            try
            {
                ProcessModuleCollection modules = process.Modules;
                foreach (ProcessModule module in modules)
                {
                    ListViewItem item = new ListViewItem(module.ModuleName);
                    item.SubItems.Add(module.FileName);
                    listView3.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching modules: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
