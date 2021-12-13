using System;
using System.ComponentModel.DataAnnotations;
using TransferDemo.API.Models;

namespace TransferDemo.API.Infraestructure.Annotations
{
    /// <summary>
    /// Atributo personalizado que permite simplificar la comparación entre propiedades.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CustomCompareAttribute : ValidationAttribute
    {
        #region Declaración de variables.
        /// <summary>
        /// El mensaje de error predeterminado.
        /// </summary>
        private const string DefaultErrorMessage = "{0} can't be equal to {1}.";

        /// <summary>
        /// El mensaje de error en caso que los bancos sean iguales.
        /// </summary>
        private const string BankErrorMessage = "Source bank can't be equal to destination bank.";

        /// <summary>
        /// La otra propiedad a la cual se hace referencia.
        /// </summary>
        public string OtherProperty { get; private set; }

        #endregion

        #region Constructores
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="otherProperty">La propiedad para comparar con la propiedad actual.</param>
        public CustomCompareAttribute(string otherProperty)
        {
            //Me aseguro que se haya estalecido la otra propiedad...
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            OtherProperty = otherProperty;
        }
        #endregion

        #region IsValid
        /// <summary>
        /// Representa un contenedor para los resultados de una solicitud de validación.
        /// </summary>
        /// <param name="value">El valor de la propiedad actual.</param>
        /// <param name="validationContext">Describe el contexto en el que se realiza una verificación de validación.</param>
        /// <returns>Un ValidationResult que indica si la operación tuvo exito o no.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);
                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

                BankInformation thisValue = (BankInformation)value;
                BankInformation otherValue = (BankInformation)otherPropertyValue;

                if (thisValue.BankName.Trim().Equals(otherValue.BankName.Trim()) && thisValue.CustomerAccount.Trim().Equals(otherValue.CustomerAccount.Trim()))
                {
                    return new ValidationResult(String.Format(DefaultErrorMessage, validationContext.DisplayName, otherProperty.Name));
                }
                else if (thisValue.BankName.Trim().Equals(otherValue.BankName.Trim()))
                {
                    return new ValidationResult(BankErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
        #endregion
    }
}
