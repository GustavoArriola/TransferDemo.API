using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Linq;
using TransferDemo.API.Models;

namespace TransferDemo.API.Infraestructure.ActionFilters
{
    /// <summary>
    /// Atributo personalizado para validar la información bancaria.
    /// </summary>
    public class ValidateAccountBalanceFilterAttribute : IActionFilter
    {
        #region Declaración de variables
        /// <summary>
        /// La interfaz del componente AutoMaper que permite asignar las propiedades de un objeto a otro.
        /// </summary>
        private readonly IMapper _mapper;
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="mapper">La interfaz del componente AutoMaper que permite asignar las propiedades de un objeto a otro.</param>
        public ValidateAccountBalanceFilterAttribute(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        /// <summary>
        /// Se ejecuta antes de lanzar el controlador.
        /// </summary>
        /// <param name="context">El contexto de ejecución.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

            if (param is null)
            {
                context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action}");
                Log.Error($"Object is null. Controller: {controller}, action: {action}");
                return;
            }
            else if (!context.ModelState.IsValid)
            {
                Log.Error($"Invalid object'. Request: {context.ModelState}");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
            else
            {
                TransferDbContext db = new();
                Account accountInfo = db.Accounts.Where(w => w.AccountNumber.Equals(((TransferDto)param).BankInformationSource.CustomerAccount)).FirstOrDefault();

                if (accountInfo != null)
                {
                    if (accountInfo.Amount < ((TransferDto)param).Amount)
                    {
                        Transfer transfer = _mapper.Map<Transfer>((TransferDto)param);
                        transfer.Id = Guid.NewGuid();
                        transfer.Status = "Rejected";
                        db.Transfers.Add(transfer);
                        db.SaveChanges();

                        context.Result = new BadRequestObjectResult($"Insufficient funds.");
                    }
                }
                else
                {
                    context.Result = new BadRequestObjectResult($"Holder account not exists.");
                }
            }                        
        }

        /// <summary>
        /// Se ejecuta luego que finalice la acción del controlador.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new System.NotImplementedException();
        }
    }
}
