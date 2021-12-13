using System.ComponentModel.DataAnnotations;

namespace TransferDemo.API.Models
{
    public class Account
    {
        [Key]
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public double Amount { get; set; }
    }
}
