using System;
using System.ComponentModel.DataAnnotations;

namespace TransferDemo.API.Models
{
    public class BankInformation
    {
        [Required]
        public string BankName { get; set; }
        [Required]
        public string CustomerAccount { get; set; }

    }
}
