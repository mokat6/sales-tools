namespace big_data.Models
{
    // better to give int values explicitly
    [Flags]
    public enum CompClassification
    {
        Unspecified = 0,
        GoodMatch = 1 << 0, // 1
        Maybe = 1 << 1, //2
        Ecommerce = 1 << 2, // 4
        LowChance = 1 << 3, // 8
    }
}