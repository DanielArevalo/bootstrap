using ACHTransactionPaymentWindowService.Infrastructure;
using System;
using System.ServiceProcess;
using System.Timers;
using Xpinn.Util.PaymentACH;

namespace ACHTransactionPaymentWindowService
{
    partial class PaymentACH : ServiceBase
    {
        Timer timer;

        public PaymentACH()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(TimeSpan.FromMinutes(3).TotalMilliseconds);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.AutoReset = true;
            timer.Start();
        }
        
        protected override void OnStop()
        {
            timer.Dispose();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                InterfazPaymentPSE IntPaymentPSE = new InterfazPaymentPSE();
                IntPaymentPSE.GenerateProcessConsult();
            }
            catch (Exception ex)
            {
                LogPaymentAction.Grabar(
                    new LogPayment(AppConstants.WindowServicesName, AppConstants.NameApplication, ex.Message, ex.StackTrace),
                    AppConstants.UrlLogApplication);
            }
        }

    }
}
