using System.ComponentModel;
namespace BitCoinPricesWebAPI.Core.Enums
{
    public enum MessagesEnum
    {
        [Description("Successfully")]
        Success = 1,
        [Description("Error While Fetching Data From Source")]
        Failed = 2,
        [Description("Error While Inserting Data Of Source")]
        InsertFailed = 3
    }
}
