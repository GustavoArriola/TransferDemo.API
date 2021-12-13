# TransferDemo.API
Ejemplos de uso de InMemoryDatabase con SeedData, anotaciones personalizadas y filtros de servicio.

# Instrucciones:
- El proyecto se asegura que la cuenta del banco origen exista y posea los fondos suficientes para realizar la operación de transferencia.
- No valida si la cuenta destino exista (eso se debería encargar otro servicio).
- El sistema solo registra como inválido aquellas operaciones donde la cuenta origen no posea los fondos suficientes.
- El objetivo del proyecto es mantener el controlador lo más limpio posible.
- Sin vueltas, simplemente ejecutar y probar con swagger.

# Cuentas para pruebas:
- AccountNumber: 12345678 (Saldo: 10.000.000)
- AccountNumber: 87654321 (Saldo: 15.000.000)
