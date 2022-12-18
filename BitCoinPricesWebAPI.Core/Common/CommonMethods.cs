using BitCoinPricesWebAPI.Core.Enums;
using System.ComponentModel;
namespace BitCoinPricesWebAPI.Core.Common
{
    public static class CommonMethods
    {
        public static string GetEnumDescription(MessagesEnum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
    }
}
