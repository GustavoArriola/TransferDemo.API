using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TransferDemo.API.Infraestructure;
using TransferDemo.API.Infraestructure.ActionFilters;
using TransferDemo.API.Models;

namespace TransferDemo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransfersController : ControllerBase
    {
        #region Declaración de variables
        /// <summary>
        /// El contexto de datos.
        /// </summary>
        private readonly TransferDbContext _transferDbContext;
        /// <summary>
        /// La interfaz del componente AutoMaper que permite asignar las propiedades de un objeto a otro.
        /// </summary>
        private readonly IMapper _mapper;
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="transferDbContext">El contexto de datos.</param>
        /// <param name="mapper">La interfaz del componente AutoMaper que permite asignar las propiedades de un objeto a otro.</param>
        public TransfersController(TransferDbContext transferDbContext, IMapper mapper)
        {
            _transferDbContext = transferDbContext;
            _mapper = mapper;
        }
        #endregion

        #region Get
        /// <summary>
        /// Obtiene la información de la transferencia de acuerdo a su identificador.
        /// </summary>
        /// <param name="id">El identificador de la transferencia.</param>
        /// <returns>Un <see cref="Transfer"/> que contiene la información de la transferencia.</returns>
        [HttpGet]
        [Route("{id:guid}", Name = nameof(GetById))]
        [ProducesResponseType(type: typeof(Transfer), statusCode: (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _transferDbContext.Transfers.Where(w => w.Id.Equals(id)).FirstOrDefaultAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtiene todas las transferencias realizadas.
        /// </summary>
        /// <returns>Un <see cref="List{Transfer}"/> que contiene el listado de transferencias.</returns>
        [HttpGet]       
        [ProducesResponseType(type: typeof(List<Transfer>), statusCode: (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _transferDbContext.Transfers.ToListAsync();
            return Ok(result);
        }
        #endregion

        #region Post
        /// <summary>
        /// Realiza una transferencia SIPAP.
        /// </summary>
        /// <param name="transferDto">Información de la operación de transferencia.</param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidateAccountBalanceFilterAttribute))]
        public async Task<IActionResult> CreateTransfer([FromBody] TransferDto transferDto)
        {

            #region Transferencia
            // Realizo la transferencia...
            var transfer = _mapper.Map<Transfer>(transferDto);
            transfer.Id = Guid.NewGuid();
            transfer.Status = "OK";
            _transferDbContext.Transfers.Add(transfer);
            #endregion

            #region Cuenta
            // Actualizo el saldo de la cuenta origen...
            var account = await _transferDbContext.Accounts.Where(w => w.AccountNumber.Equals(transferDto.BankInformationSource.CustomerAccount)).FirstOrDefaultAsync();
            account.Amount -= transferDto.Amount;
            
            _transferDbContext.Accounts.Update(account);

            #endregion

            //Aplico la operación...
            await _transferDbContext.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetById), new {id = transfer.Id}, transfer);
        }
        #endregion
    }
}
