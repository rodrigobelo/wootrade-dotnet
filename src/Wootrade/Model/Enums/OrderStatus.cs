namespace Wootrade.Model.Enums
{
    public enum OrderStatus
    {
        Unknown = 0,

        New = 1,
        Cancelled = 2,
        PartialFilled = 3,
        Filled = 4,
        Rejected = 5,
        Incomplete = 6,
        Complete = 7
    }
}