using System.Collections.Generic;

namespace Xpinn.Tesoreria.Entities
{
    public class PaymentACHResult
    {
        public PaymentACH PaymentObj { get; set; }
        public List<PaymentACH> PaymentList { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
