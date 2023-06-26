using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace _5lab_
{
    public partial class mainForm : Form
    {
        private int BankAccount;
        private int AllResources;
        private int AllResources_temp;
        private int TimeForBuy;
        private int ResourceCost;
        private int TotalResources = 0;
        private int TotalWorkers = 0;
        private int TotalEarn = 0;
        private List<Worships> worships;
        private XmlDocument logXmlDocument;
        private XmlElement logRootElement;
        public mainForm()
        {
            InitializeComponent();
            logXmlDocument = new XmlDocument();
            logRootElement = logXmlDocument.CreateElement("Log");
            logXmlDocument.AppendChild(logRootElement);
            worships = new List<Worships>();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            
        }
         private void LogState(string message)
        {
            XmlElement logElement = logXmlDocument.CreateElement("State");
            logElement.SetAttribute("Time", DateTime.Now.ToString());
            logElement.InnerText = message;
            logRootElement.AppendChild(logElement);
            logXmlDocument.Save("log.xml");
        }
    


    private void addButton_Click(object sender, EventArgs e)
        {
            TotalResources = 0;
            TotalWorkers = 0;
            TotalEarn = 0;
            dataGridView1.AllowUserToAddRows = false;
            int workers = Convert.ToInt16(textBox1.Text);
            int resource = Convert.ToInt16(textBox2.Text);
            int product = Convert.ToInt16(textBox3.Text);
            int sell = Convert.ToInt16(textBox4.Text);
            Worships worship = new Worships(workers, resource, product, sell);
            worships.Add(worship);

            DataTable worsh = new DataTable();
            worsh.Columns.Add("Workers", typeof(int));
            worsh.Columns.Add("Resource", typeof(int));
            worsh.Columns.Add("Product", typeof(int));
            worsh.Columns.Add("Sell", typeof(int));
            worsh.Columns.Add("ResSpend", typeof(int));
            worsh.Columns.Add("Earn", typeof(int));
            for (int i = 0; i < worships.Count; i++)
            {
                worsh.Rows.Add(worships[i].NumberOfWorkers, worships[i].NumberOfResources, worships[i].NumberOfProduct,
                    worships[i].CostOfSell, worships[i].ResSpend, worships[i].Earn);
                TotalResources += worships[i].ResSpend;
                TotalWorkers += worships[i].NumberOfWorkers;
                TotalEarn += worships[i].Earn;
            }
            dataGridView1.DataSource = worsh;
            label9.Text = Convert.ToString(TotalResources);
            label13.Text = Convert.ToString(TotalWorkers);
            label14.Text = Convert.ToString(TotalEarn);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            BankAccount = Convert.ToInt32(textBox5.Text);
            AllResources = Convert.ToInt32(textBox6.Text);
            AllResources_temp = AllResources;
            TimeForBuy = Convert.ToInt16(textBox7.Text);
            ResourceCost = Convert.ToInt16(textBox8.Text);
            ThreadStart product = new ThreadStart(Production);
            Thread othread = new Thread(product);
            othread.Start();
            ThreadStart sell = new ThreadStart(Selling);
            Thread tthread = new Thread(sell);
            tthread.Start();
        }

        public void Production()
        {
            while (AllResources >= TotalResources)
            {
                Thread.Sleep(1000);
                AllResources -= TotalResources;
            }
            LogState("All workshops are out of resources. Production stopped.");
            AddPay();
        }

        public void Selling()
        {
            Thread.Sleep(TimeForBuy*1000);
            BankAccount += TotalEarn * TimeForBuy;
            LogState("Workshops sold all products for " + TotalEarn * TimeForBuy);
            
            
        }

        public void AddPay()
        {
            BankAccount -= AllResources_temp * ResourceCost;
            AllResources += AllResources_temp;
            LogState("Factory bought " + AllResources_temp + " resources for " + AllResources_temp * ResourceCost + " $");
            BankAccount -= TotalWorkers * 5;
            LogState("Factory payed " + TotalWorkers * 5 + " $ for " + TotalWorkers + " workers");
            Production();
        }

    }
}
