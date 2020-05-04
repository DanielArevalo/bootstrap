namespace Xpinn.Util.PaymentACH
{
    public enum PaymentTypeEnum
    {
        normal = 0,
        multicredit = 1
    }

    public enum PaymentStatusEnum
    {
        created = 0,
        pending = 1,
        approved = 2,
        rejected = 3,
        failed = 4
    }
}
