using System;
using System.ComponentModel.DataAnnotations;
using TransferDemo.API.Infraestructure.Annotations;

namespace TransferDemo.API.Models
{
    public class Transfer
    {
        /// <summary>
        /// El identificador de la transferencia.
        /// </summary>
        [Key]
        public Guid? Id { get; set; }

        /// <summary>
        /// La fecha de la operación.
        /// </summary>
        [Required]
        public DateTime TransferDate { get; set; }

        /// <summary>
        /// El monto a transferir.
        /// </summary>
        [Required]
        public double Amount { get; set; }

        /// <summary>
        /// El banco origen.
        /// </summary>
        [Required]
        public string BankNameFrom { get; set; }

        /// <summary>
        /// La cuenta origen.
        /// </summary>
        [Required]
        public string CustomerAccountFrom { get; set; }

        /// <summary>
        /// El banco destino.
        /// </summary>
        [Required]
        public string BankNameTo { get; set; }

        /// <summary>
        /// La cuenta destino.
        /// </summary>
        [Required]
        public string CustomerAccountTo { get; set; }

        /// <summary>
        /// El estado de la transferencia.
        /// </summary>
        public string Status { get; set; }
    }   
    
    public class TransferDto
    {
        /// <summary>
        /// El identificador de la transferencia.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// La fecha de la operación.
        /// </summary>
        [Required]
        public DateTime TransferDate { get; set; }

        /// <summary>
        /// El monto a transferir.
        /// </summary>
        [Required]
        public double Amount { get; set; }

        /// <summary>
        /// Información de la cuenta origen.
        /// </summary>
        [Required]
        public BankInformation BankInformationSource { get; set; }

        /// <summary>
        /// Información de la cuenta destino.
        /// </summary>
        [Required]
        [CustomCompare(nameof(BankInformationSource))]
        public BankInformation BankInformationDestination { get; set; }
    }
}
