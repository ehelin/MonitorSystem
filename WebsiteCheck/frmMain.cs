using System;
using System.Windows.Forms;
using BLL.classes;
using Shared.data;
using Shared.misc;

namespace WebsiteCheck
{
    public partial class frmMain : Form
    {
        private Timer _time = null;

        public frmMain()
        {
            InitializeComponent();         
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            _time = SetupTimer();
            _time.Start();
        }
        
        private void Tick(Object myObject,EventArgs myEventArgs) 
        {
            try
            {
                _time.Stop();

                lbOutput.Items.Clear();
                lbOutput.Items.Add(Constants.PROCESSING);
                Application.DoEvents();
                System.Threading.Thread.Sleep(1000);
                                
                Run();

                _time.Start();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, Constants.ERROR);
            }
        }       
        
        private Timer SetupTimer()
        {
            Timer time = new Timer();
            time.Tick += new EventHandler(Tick);
            time.Interval = Convert.ToInt32(tbCheckInterval.Text);            
            time.Enabled = true;

            return time;
        }  

        private void Run()
        {
            Main mn = new Main(); 
            DisplayOutput dispOut = mn.Run();
            
            lbOutput.Items.Clear();
            foreach(string display in dispOut.Events)
            {
                lbOutput.Items.Add(display);
            }
        }
    }
}
