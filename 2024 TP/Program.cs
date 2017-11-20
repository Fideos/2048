using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2024_TP
{
    class Program
    {
        static void guardarPuntaje(string nombreArchivo, long puntaje) //Implementar Guardar Partida.
        {
            nombreArchivo += ".txt";
            string texto = "";
            long puntajeRanking;
            if (File.Exists(nombreArchivo))
            {
                FileStream file = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(file);
                int i = 0;
                while (!sr.EndOfStream)
                {

                    texto = sr.ReadLine();
                    i++;
                }
                puntajeRanking = Convert.ToInt64(texto.Remove(0, 5));
                sr.Close();
                file.Close();
                if (puntaje > puntajeRanking)
                {
                    string nombre, nuevoPuntaje;
                    do
                    {
                        Console.WriteLine("¡Superaste al mejor jugador!\n\nIngrese nombre (Use 3 Caracteres)");
                        nombre = Console.ReadLine();
                        nombre += ": ";
                        Console.Clear();
                    }while (nombre.Length != 5);
                    nuevoPuntaje = nombre += puntaje;
                    FileStream file2 = new FileStream(nombreArchivo, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(file2);
                    for (int j = 0; j < nuevoPuntaje.Length; j++)
                    {
                        sw.Write(nuevoPuntaje[j]);
                    }
                    sw.Close();
                    file2.Close();
                }
                else
                {
                    Console.WriteLine("No lograste superar al mejor jugador\n");
                }
                Console.WriteLine("Mejor Jugador:\nName|Score\n\n" + Ranking("mejorJugador"));
            }
            else
            {
                Console.WriteLine("El archivo no existe");
            }
        }

        static string Ranking(string nombreArchivo)
        {
            nombreArchivo += ".txt";
            string linea = "No hay mejor jugador";
            char separador = ':';
            if (File.Exists(nombreArchivo))
            {
                FileStream file = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(file);
                int i = 0;
                while (!sr.EndOfStream)
                {

                    linea = sr.ReadLine();
                    i++;
                }
                linea.Split(separador);
                sr.Close();
                file.Close();
            }
            return linea;
        }

        static int[,] RotarArray(int[,] matriz)
        {
            int[] matrizRotada = new int[matriz.Length];
            int r = 0;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matrizRotada[r] = matriz[i, j];
                    r++;
                }
            }

            r = 0;

            Array.Reverse(matrizRotada);

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = matrizRotada[r];
                    r++;
                }
            }

            return matriz;
        }

        static void Teclas(ref int[,] matriz, ref bool salio, ref long ronda)
        {
            Console.WriteLine("Ronda: " + ronda);
            ConsoleKeyInfo tecla;
            tecla = Console.ReadKey(true);
            switch (tecla.Key) // Se repite el mismo codigo cambiado para cada tecla direccional.
            {
                case ConsoleKey.UpArrow:
                    int max = 0; // Este entero limita el area de movimiento para evitar que los valores se sobrescriban
                    bool junta, /* Bool que determina si 2 valores iguales se juntaron o no */
                    movio = false; // Determina si las fichas se movieron, en caso de no haberlo hecho no genera fichas nuevas ni suma movimientos.
                    for (int i = 0; i < matriz.GetLength(0); i++)
                    {
                        for (int j = 0; j < matriz.GetLength(1); j++)
                        {
                            max = 0;
                            movio = false;
                            junta = false;
                            if (matriz[i, j] > 0 && i != 0)
                            {
                                while (matriz[max, j] != 0 && max < matriz.GetLength(0) - 1 && max < i)
                                {
                                    max++;
                                }
                                matriz[max, j] = matriz[i, j];
                                if (max > 0 && matriz[max -1, j] == matriz[i, j])
                                {
                                    matriz[max -1, j] += matriz[i, j];
                                    matriz[max, j] = 0;
                                    junta = true;
                                }
                                if (max != i || junta)
                                {
                                    matriz[i, j] = 0;
                                }
                            }
                        }
                    }
                    GenerarFichas(matriz, ref salio);
                    ronda++;
                    break;
                case ConsoleKey.DownArrow:
                    //

                    RotarArray(matriz);
                    //
                    for (int i = 0; i < matriz.GetLength(0); i++)
                    {
                        for (int j = 0; j < matriz.GetLength(1); j++)
                        {
                            max = 0;
                            movio = false;
                            junta = false;
                            if (matriz[i, j] > 0 && i != 0)
                            {
                                while (matriz[max, j] != 0 && max < matriz.GetLength(0) - 1 && max < i)
                                {
                                    max++;
                                }
                                matriz[max, j] = matriz[i, j];
                                if (max > 0 && matriz[max - 1, j] == matriz[i, j])
                                {
                                    matriz[max - 1, j] += matriz[i, j];
                                    matriz[max, j] = 0;
                                    junta = true;
                                }
                                if (max != i || junta)
                                {
                                    matriz[i, j] = 0;
                                }
                            }
                        }
                    }
                    GenerarFichas(matriz, ref salio);
                    //
                    RotarArray(matriz);

                    ronda++;
                    //
                    break;
                case ConsoleKey.LeftArrow:
                    for (int i = 0; i < matriz.GetLength(0); i++)
                    {
                        for (int j = 0; j < matriz.GetLength(1); j++)
                        {
                            max = 0;
                            movio = false;
                            junta = false;
                            if (matriz[i, j] > 0 && j != 0)
                            {
                                while (matriz[i, max] != 0 && max < matriz.GetLength(1) - 1 && max < j)
                                {
                                    max++;
                                }
                                matriz[i, max] = matriz[i, j];
                                if (max > 0 && matriz[i, max -1] == matriz[i, j])
                                {
                                    matriz[i, max - 1] += matriz[i, j];
                                    matriz[i, max] = 0;
                                    junta = true;
                                }
                                if (max != j || junta)
                                {
                                    matriz[i, j] = 0;
                                }
                            }
                        }
                    }
                    GenerarFichas(matriz, ref salio);
                    ronda++;
                    break;
                case ConsoleKey.RightArrow:
                    //
                    RotarArray(matriz);
                    //

                    for (int i = 0; i < matriz.GetLength(0); i++)
                    {
                        for (int j = 0; j < matriz.GetLength(1); j++)
                        {
                            max = 0;
                            movio = false;
                            junta = false;
                            if (matriz[i, j] > 0 && j != 0)
                            {
                                while (matriz[i, max] != 0 && max < matriz.GetLength(1) - 1 && max < j)
                                {
                                    max++;
                                }
                                matriz[i, max] = matriz[i, j];
                                if (max > 0 && matriz[i, max - 1] == matriz[i, j])
                                {
                                    matriz[i, max - 1] += matriz[i, j];
                                    matriz[i, max] = 0;
                                    junta = true;
                                }
                                if (max != j || junta)
                                {
                                    matriz[i, j] = 0;
                                }
                            }
                        }
                    }
                    GenerarFichas(matriz, ref salio);

                    //
                    RotarArray(matriz);
                    ronda++;
                    //
                    break;
                case ConsoleKey.Escape:
                    salio = true;
                    break;
            }
        }

        static ConsoleColor ColorDeFicha(int ficha)
        {
            switch (ficha)
            {
                case 0:
                    return ConsoleColor.DarkGray;
                case 2:
                    return ConsoleColor.Cyan;
                case 4:
                    return ConsoleColor.Magenta;
                case 8:
                    return ConsoleColor.Blue;
                case 16:
                    return ConsoleColor.Green;
                case 32:
                    return ConsoleColor.Yellow;
                case 64:
                    return ConsoleColor.DarkCyan;
                case 128:
                    return ConsoleColor.DarkMagenta;
                case 256:
                    return ConsoleColor.DarkBlue;
                case 512:
                    return ConsoleColor.DarkGreen;
                case 1024:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.White;
            }
        }


        static int[,] GenerarFichas(int[,] matriz, ref bool gameOver)
        {
            Random aleatorio = new Random();
            int ficha = 0, total = matriz.Length, posicionX, posicionY, probabilidad = aleatorio.Next(1, 101);
            do
            {
                posicionX = aleatorio.Next(0, matriz.GetLength(0));
                posicionY = aleatorio.Next(0, matriz.GetLength(1));
            } while (matriz[posicionX, posicionY] != 0);  // Falta implementar GameOver.
            if (probabilidad <= 30)
            {
                ficha = 4;
            }
            else
            {
                ficha = 2;
            }
            matriz[posicionX, posicionY] = ficha;
            return matriz;
        }

        static void ImprimirTablero(int[,] matriz, long puntaje)
        {
            Console.Clear();
            int valor;
            Console.WriteLine("Puntos:" + puntaje + "\n");
            for (int f = 0; f < matriz.GetLength(0); f++)
            {
                for (int c = 0; c < matriz.GetLength(1); c++)
                {
                    valor = matriz[f, c];
                    Console.ForegroundColor = ColorDeFicha(valor);
                    Console.Write(matriz[f, c] + "\t");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("\nPresione Esc para finalizar la partida");
        }

        static void partida(int[,] matriz, ref long puntaje, ref bool gameover)
        {
            long ronda = 0;
            while (gameover == false)
            {
                if (ronda == 0)
                {
                    matriz = GenerarFichas(matriz, ref gameover);
                    matriz = GenerarFichas(matriz, ref gameover);
                }
                ImprimirTablero(matriz, puntaje);
                Teclas(ref matriz, ref gameover, ref ronda);
                puntaje++; // Simula el puntaje (Implementar variable "Movio" en teclas para incrementar puntaje por movimiento.)
            }
        }

        static void Main(string[] args)
        {
            int tamaño = 0;
            long puntaje = 0;
            string menu, modoDeJuego;
            bool gameover = false;
            do
            {
                Console.WriteLine("1) Nuevo juego.\n2) Cargar juego. //Clausurado\n3) Mejor jugador.\n4) Salir.");
                menu = Console.ReadLine();
                switch (menu)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Mejor Jugador:\nName|Score\n\n" + Ranking("mejorJugador") + "\n\n\n\nPresione cualquier tecla para volver al menu...");
                        Console.ReadKey();
                        break;
                    case "4":
                        gameover = true;
                        break;
                }
                Console.Clear();
            } while (menu != "1" && menu != "4");
            if (gameover == false)
            {
                do
                {
                    Console.WriteLine("Seleccione el tamaño de el tablero:\n1) Clásico 4x4\n2) Grande 6x6\n3) Enorme 8x8");
                    modoDeJuego = Console.ReadLine();
                    Console.Clear();
                } while (modoDeJuego != "1" && modoDeJuego != "2" && modoDeJuego != "3");
                switch (modoDeJuego)
                {
                    case "1":
                        tamaño = 4;
                        break;
                    case "2":
                        tamaño = 6;
                        break;
                    case "3":
                        tamaño = 8;
                        break;
                }
            }
            int[,] tablero = new int [tamaño , tamaño];
            partida(tablero, ref puntaje, ref gameover);
            Console.Clear();
            Console.WriteLine("Su puntaje fue " + puntaje + "\n");
            guardarPuntaje("mejorJugador", puntaje);
            Console.WriteLine("\n\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
