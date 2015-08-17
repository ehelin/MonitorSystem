using System;
using System.ServiceProcess;
using System.Timers;
using BLL.classes;
using Shared.misc;

namespace WebsiteCheckService
{
    public partial class Service : ServiceBase
    {
        private Timer _time = null;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry(ServiceConstants.STARTING);

            _time = SetupTimer();
            _time.Start();

            EventLog.WriteEntry(ServiceConstants.STARTING_EXCLAMATION);
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry(ServiceConstants.STOPPING);

            _time.Stop();
            _time.Dispose();
            _time = null;

            EventLog.WriteEntry(ServiceConstants.STOPPING_EXCLAMATION);
        }

        private Timer SetupTimer()
        {
            EventLog.WriteEntry(ServiceConstants.TIMER_SETUP);

            string intervalStr = System.Configuration.ConfigurationSettings.AppSettings["Interval"];
            int interval = Convert.ToInt32(intervalStr);

            _time = new Timer();
            _time.Enabled = true;
            _time.Interval = interval;   
            _time.AutoReset = true;
            _time.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimedEvent);

            EventLog.WriteEntry(ServiceConstants.TIMER_EXCLAMATION);

            return _time;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                _time.Stop();

                EventLog.WriteEntry(ServiceConstants.RUN_START);
                
                Run();

                EventLog.WriteEntry(ServiceConstants.RUN_START);

                _time.Start();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ServiceConstants.ERROR + ex.Message);
            }
        }

        private void Run()
        {
            Main mn = new Main();
            mn.Run();
        }
    }
}
