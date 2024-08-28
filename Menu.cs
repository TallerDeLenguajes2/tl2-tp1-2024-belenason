    public class Menu
    {
        int indiceSeleccionado;

        string [] opciones;
        string textoMenu; 

        public Menu (string TextoMenu, string [] Opciones)
        {
            textoMenu = TextoMenu;
            opciones = Opciones;
            indiceSeleccionado = 0;
        }

        private void MostrarOpciones()
        {
            Console.ResetColor();
            Console.WriteLine(textoMenu);
            for (int i = 0; i < opciones.Length; i++)
            {
                if(i == indiceSeleccionado)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($"{i+1}. {opciones[i]}");
            }
            Console.ResetColor();
        }

        public int MenuDisplay()
        {
            ConsoleKeyInfo teclaPresionada;
            do
            {
                Console.Clear();
                MostrarOpciones();
                teclaPresionada = Console.ReadKey(true);

                switch (teclaPresionada.Key)
                {
                    case ConsoleKey.DownArrow:
                        indiceSeleccionado = indiceSeleccionado == opciones.Length-1 ? 0: indiceSeleccionado+1;
                        break;
                    case ConsoleKey.UpArrow:
                        indiceSeleccionado = indiceSeleccionado == 0 ? opciones.Length-1: indiceSeleccionado-1;
                        break;
                }

            } while (teclaPresionada.Key != ConsoleKey.Enter);
            return indiceSeleccionado;
        }
    }

    