# TP 1
## ¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?
La relación entre el pedido y el cliente es de composición, ya que si se elimina el pedido se elimina el cliente, si el pedido no existe el clientet ya no es más cliente nuestro y no nos interesa su información.
La relación entre el pedido y el cadete es de agregación, ya que necesitamos saber cuantos pedidos entregó el cadete (para pagarle), y porque los pedidos se pueden reasignar, por lo tanto deben existir de forma independiente. Se le asignan los pedidos al cadete, pero si no existe o desaparece un cadete, el pedido persiste y se asigna a otro cadete.
La relación entre el cadete y la cadeteria debe ser de agregación. Los cadetes se crean y luego se los envía a la cadetería para ser registrados.

## ¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?
La clase Cadetería debe tener los métodos:
- AsignarPedido: asigna un pedido al cadete con menos pedidos en curso.
- ReasignarPedidos: transfiere un pedido de un cadete a otro.
- CalcularJornales: usa el listado de cadetes y para cada uno va llamando al método JornalACobrar. De esta forma muestra los jornales de cada cadete.

La clase Cadete debe tener los métodos:
- JornalACobrar: permite conocer el monto que debe cobrar el cadete al final del día a partir de la cantidad de pedidos completados.
- EntregarPedido: cambia el estado del pedido de "En camino" a "Entregado".
- RetirarPedido: cambia el estado del pedido de "En preparación" a "En camino".
- DarDeBajaPedido: quita un pedido determinado de la lista de pedidos del cadete.
- CantidadDePedidosCompletados: utiliza una sentencia Linq para que a partir de las lista de pedidos obtenga la cantidad de pedidos completados.

## Teniendo en cuenta los principios de abstracción y ocultamiento, que atributos, propiedades y métodos deberían ser públicos y cuáles privados.
Todos los atributos de las clases son privados. La accesibilidad de los mismos se da a través de las propiedades get y set. En el caso de los campos que cuyo valor no cambio durante el uso del sistema como nombre, telefono, direccion, etc, se utiliza unicamente la propiedad get para poder mostrar su información por pantalla. En cambio, los campos como la lista de pedidos o el estado de los pedidos cuyo valor se va modificando con el sistema tiene la propiedad set para poder reflejar los cambios hechos por el usuario. El acceso a los campos así como su manipulación se realiza a partir de las propiedades y método de cada una de ellas que se mantienen públicos.

## ¿Cómo diseñaría los constructores de cada una de las clases?


## ¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?





