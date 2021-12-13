using Microsoft.EntityFrameworkCore;
using TransferDemo.API.Infraestructure.EntityConfigurations;
using TransferDemo.API.Models;

namespace TransferDemo.API.Infraestructure
{
    /// <summary>
    /// El contexto de datos del proyecto.
    /// </summary>
    public class TransferDbContext : DbContext
    {
        #region DbSet

        /// <summary>
        /// La tabla que administra las transferencias.
        /// </summary>
        public DbSet<Transfer> Transfers { get; set; }

        /// <summary>
        /// La tabla que administra las cuentas.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }
        #endregion

        #region Configuración del contexto
        /// <summary>
        /// Configura el contexto de datos.
        /// </summary>
        /// <param name="optionsBuilder">Las opciones de configuración del contexto.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TransferDb");                

            base.OnConfiguring(optionsBuilder);
        }
        #endregion

        #region Configuración del modelo
        /// <summary>
        /// Permite realizar actualizaciones al modelo de datos.
        /// </summary>
        /// <param name="modelBuilder">El constructor del modelo que se encarga de realizar los ajustes al modelo de datos.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new AccountConfiguration());
        }
        #endregion
    }
}
