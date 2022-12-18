using System.ComponentModel.DataAnnotations;
namespace BitCoinPricesWebAPI.Core.Entities
{
    public class SystemUser
    {
      
        public string UserName { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
