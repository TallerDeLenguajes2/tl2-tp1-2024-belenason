# TP 1
## ¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?
La relación entre el pedido y el cliente es de composición, ya que si se elimina el pedido se elimina el cliente.
La relación entre el pedido y el cadete es de agregación, ya que necesitamos saber cuantos pedidos entregó el cadete (para pagarle), y porque los pedidos se pueden reasignar, por lo tanto deben existir de forma independiente.
La relación entre el cadete y la cadeteria debe ser de agregación.

## ¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?
La clase Cadetería debe tener los métodos:
- AsignarPedido: asigna un pedido al cadete con menos pedidos en curso.
- CalcularJornales: usa el listado de cadetes y para cada uno va llamando al método JornalACobrar.

La clase Cadete debe tener los métodos:
- JornalACobrar: permite conocer el monto que debe cobrar el cadete al final del día.
- EntregarPedido: cambia el estado del pedido de "En camino" a "Entregado".
- RetirarPedido: cambia el estado del pedido de "En preparación" a "En camino".
- DarDeBajaPedido: llama al metodo AsignarPedido.

## Teniendo en cuenta los principios de abstracción y ocultamiento, que atributos, propiedades y métodos deberían ser públicos y cuáles privados.
Pedidos
Los privados deben ser:

## ¿Cómo diseñaría los constructores de cada una de las clases?


## ¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?





