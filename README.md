# TransferDemo.API
Ejemplos de uso de InMemoryDatabase con SeedData, anotaciones personalizadas y filtros de servicio.

# Instrucciones:
- El proyecto se asegura que la cuenta del banco origen exista y posea los fondos suficientes para realizar una operación de transferencia.
- No valida si la cuenta destino exista (eso se debería encargar otro servicio).
- El sistema solo registra como inválido aquellas operaciones donde la cuenta origen no posea los fondos suficientes.
- El objetivo del proyecto es mantener al controlador lo más limpio posible.
- Sin vueltas, simplemente ejecutar y probar con swagger.

# Cuentas para pruebas:
- AccountNumber: 12345678 (Saldo: 10.000.000)
- AccountNumber: 87654321 (Saldo: 15.000.000)

Operación exitosa de transferencia de fondos.
![1](https://user-images.githubusercontent.com/13947325/145749616-3734772b-a0d3-44a1-aacd-80f3c78b5284.PNG)

Operación no satisfactoria (insuficiencia de fondos).
![2](https://user-images.githubusercontent.com/13947325/145749638-d9ea56e3-5e21-4d6e-aab2-ebacdce29fae.PNG)

Listado de operaciones (válidas o no).
![3](https://user-images.githubusercontent.com/13947325/145749649-611609d4-afa6-4d1d-be8a-4637a26af626.PNG)
