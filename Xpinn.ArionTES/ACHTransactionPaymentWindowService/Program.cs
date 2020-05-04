using System.ServiceProcess;

namespace ACHTransactionPaymentWindowService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new PaymentACH()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
