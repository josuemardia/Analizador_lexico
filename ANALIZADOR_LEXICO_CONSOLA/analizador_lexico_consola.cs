using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANALIZADOR_LEXICO
{

    class Program
    {
        // Vector general que contiene a todos los tokens del lenguaje
        private static string[] Tokens = { "lib", "namespace", "ent", "dec", "flot", "cad", "car", "bit", "cons", "var", "romp", "verdadero", "falso", "public", "priv", "vac", "est", "nue", "retor", "interfaz", "intentar", "cap", "enum", "bol", "clase", "virtual", "releer", "abs", "base", "this", "si", "sino", "mientras", "para", "encaso", "caso", "defecto", "hacer", "imprimir", "nulo", "+", "*", "-", "/", "++", "--", "+=", "-=", "*=", "%", "==", "=", ">", "<", ">=", "<=", "||", "|", "!", "&&", "^", "!=", "<>", "?:", "{", "}", ";", "[", "]", "(", ")", ":", "'", "/", "/*", "*/" };
       
        //vectores especificos que contienen a una parte de los tokens 
        private static string[] PalabrasReservadas = { "lib", "namespace", "ent", "dec", "flot", "cad", "car", "bit", "cons", "var", "romp", "verdadero", "falso", "public", "priv", "vac", "est", "nue", "retor", "interfaz", "intentar", "cap", "enum", "bol", "clase", "virtual", "releer", "abs", "base", "this", "si", "sino", "mientras", "para", "encaso", "caso", "defecto", "hacer", "imprimir", "nulo" };
        private static string[] Delimitadores = { "{", "}", ";", "[", "]", "(", ")", ":", "'", "\"", "/", "/*", "*/" };
        private static string[] OperadoresAritmeticos = { "+", "*", "-", "/", "%", "^" };
        private static string[] OperadoresRelacionales = { "==", ">", "<", ">=", "<=", "<>", "?:" };
        private static string[] OperadoresLogicos = { "||", "|", "!", "&&", "!=" };
        private static string[] OperadoresAsignacion = { "++", "--", "+=", "-=", "*=", "=" };

        //metodo que analiza si se encuentra tokens en la cadena de entrada
        public static void AnalizadorDeTokens(string cadena)
        {

            String cadenaEntera = cadena;
            char salto = '\n';
            char[] limitador = { ' ' };

            //listas que guardan los tokens particulares
            List<string> PalabrasEncontradas = new List<string>();
            List<string> NumerosEncontrados = new List<string>();
            List<string> SimbolosEncontrados = new List<string>();
            List<string> IdentificadoresEncontrados = new List<string>();

            int caso = 0;
            string palabraAux;
            string numero = "";
            string palabra = "";
            string simbolo = "";

            //string[] palabra2 = new string[Tokens.Length];

            string[] filas = cadenaEntera.Split(salto);
            string[] palabras;


            for (int i = 0; i < filas.Length; i++)
            {
                //FALTA LA LÓGICA QUE OBTENGA A LOS IDENTIFICADORES
                palabras = filas[i].Split(limitador);

                for (int k = 0; k < palabras.Length; k++)
                {

                    palabraAux = palabras[k];

                    for (int j = 0; j < palabraAux.Length; j++)
                    {
                        switch (caso)
                        {
                            case 0://evita tomar los espacios vacíos
                                //caso = char.IsDigit(palabraAux[j]) ? 1 : char.IsLetter(palabraAux[j]) ? 2 : palabraAux[j] != ' ' ? 3 : 0;
                                if (char.IsDigit(palabraAux[j]))
                                {
                                    caso = 1;
                                    numero += palabraAux[j];
                                    if (j == palabraAux.Length - 1)
                                    {
                                        NumerosEncontrados.Add(numero);
                                        numero = "";
                                    }
                                }
                                else if (char.IsLetter(palabraAux[j]))
                                {
                                    caso = 2;
                                    palabra += palabraAux[j];
                                    if (j == palabraAux.Length - 1)
                                    {
                                        PalabrasEncontradas.Add(palabra);
                                        palabra = "";
                                    }
                                }
                                else if(palabraAux[j] != ' ')
                                {
                                    caso = 3;
                                    simbolo += palabraAux[j];
                                    if (j == palabraAux.Length - 1)
                                    {
                                        SimbolosEncontrados.Add(simbolo);
                                        simbolo = "";
                                    }
                                }
                                else
                                {
                                    caso = 0;
                                }
                                break;
                            case 1: //se evalúan números
                                if (char.IsDigit(palabraAux[j]))
                                {
                                    numero += palabraAux[j];
                                    if (j == palabraAux.Length - 1)
                                    {
                                        NumerosEncontrados.Add(numero);
                                        numero = "";
                                    }
                                }
                                else if (char.IsLetter(palabraAux[j]))
                                {
                                    palabra += palabraAux[j];
                                    caso = 2;
                                    PalabrasEncontradas.Add(palabra);
                                    palabra = "";
                                }
                                else if (palabraAux[j] == ' ')
                                {
                                    caso = char.IsDigit(palabraAux[j + 1]) ? 1 : char.IsLetter(palabraAux[j + 1]) ? 2 : palabraAux[j + 1] != ' ' ? 3 : 0;
                                    caso = 0;
                                    NumerosEncontrados.Add(numero);
                                    numero = "";
                                }
                                else
                                {
                                    simbolo += palabraAux[j];
                                    caso = 3;
                                    SimbolosEncontrados.Add(simbolo);
                                    simbolo = "";
                                }
                                break;
                            case 2: //se evalúan palabras
                                if (char.IsLetter(palabraAux[j]))
                                {
                                    palabra += palabraAux[j];
                                    if (j == palabraAux.Length - 1)
                                    {
                                        PalabrasEncontradas.Add(palabra);
                                        palabra = "";
                                    }
                                }
                                else if (char.IsDigit(palabraAux[j]))
                                {
                                    numero += palabraAux[j];
                                    caso = 1;
                                    NumerosEncontrados.Add(numero);
                                    numero = "";
                                }
                                else if (palabraAux[j] == ' ')
                                {
                                    caso = char.IsDigit(palabraAux[j + 1]) ? 1 : char.IsLetter(palabraAux[j + 1]) ? 2 : palabraAux[j + 1] != ' ' ? 3 : 0;
                                    PalabrasEncontradas.Add(palabra);
                                    palabra = "";
                                }
                                else
                                {
                                    simbolo += palabraAux[j];
                                    caso = 3;
                                    SimbolosEncontrados.Add(simbolo);
                                    simbolo = "";
                                }
                                break;
                            case 3: //evalúa otros caracteres
                                if (char.IsLetter(palabraAux[j]))
                                {
                                    palabra += palabraAux[j];
                                    caso = 2;
                                    PalabrasEncontradas.Add(palabra);
                                    palabra = "";
                                }
                                else if (char.IsDigit(palabraAux[j]))
                                {
                                    numero += palabraAux[j];
                                    caso = 1;
                                    NumerosEncontrados.Add(numero);
                                    numero = "";
                                }
                                else if (palabraAux[j] == ' ')
                                {
                                    caso = char.IsDigit(palabraAux[j + 1]) ? 1 : char.IsLetter(palabraAux[j + 1]) ? 2 : palabraAux[j + 1] != ' ' ? 3 : 0;
                                    SimbolosEncontrados.Add(simbolo);
                                    simbolo = "";
                                }
                                else
                                {
                                    simbolo += palabraAux[j];
                                    if (j == palabraAux.Length - 1)
                                    {
                                        SimbolosEncontrados.Add(simbolo);
                                        simbolo = "";
                                    }
                                }
                                break;
                            default: break;
                        }
                    }

                }
            }


            List<string> PalabrasReservadasEncontradas = new List<string>();
            bool esIdentificador = true;

            //Evalúo las palabras encontradas 

            for (int i = 0; i < PalabrasEncontradas.Count; i++)
            {
                for (int j = 0; j < PalabrasReservadas.Length; j++)
                {
                    if (Equals(PalabrasEncontradas[i], PalabrasReservadas[j]))
                    {
                        PalabrasReservadasEncontradas.Add(PalabrasReservadas[j]);
                        esIdentificador = false;
                        break;
                    }
                    //else
                    //{
                    //    IdentificadoresEncontrados.Add(PalabrasEncontradas[i]);
                    //}
                }

                if (esIdentificador)
                {
                    IdentificadoresEncontrados.Add(PalabrasEncontradas[i]);
                }

                esIdentificador = true;

            }

            List<string> DelimitadoresEncontrados = new List<string>();
            List<string> OperadoresAritmeticosEncontrados = new List<string>();
            List<string> OperadoresRelacionalesEncontrados = new List<string>();
            List<string> OperadoresLogicosEncontrados = new List<string>();
            List<string> OperadoresAsignacionEncontrados = new List<string>();

            //Evalúo los símbolos encontrados

            for (int i = 0; i < SimbolosEncontrados.Count; i++)
            {
                //Delimitadores
                for (int j = 0; j < Delimitadores.Length; j++)
                {
                    if (Equals(SimbolosEncontrados[i], Delimitadores[j]))
                    {
                        DelimitadoresEncontrados.Add(Delimitadores[j]);
                        break;
                    }
                }


                //Operadores Aritméticos
                for (int j = 0; j < OperadoresAritmeticos.Length; j++)
                {
                    if (Equals(SimbolosEncontrados[i], OperadoresAritmeticos[j]))
                    {
                        OperadoresAritmeticosEncontrados.Add(OperadoresAritmeticos[j]);
                        break;
                    }
                }

                //Operadores Relacionales
                for (int j = 0; j < OperadoresRelacionales.Length; j++)
                {
                    if (Equals(SimbolosEncontrados[i], OperadoresRelacionales[j]))
                    {
                        OperadoresRelacionalesEncontrados.Add(OperadoresRelacionales[j]);
                        break;
                    }
                }

                //Operadores Lógicos
                for (int j = 0; j < OperadoresLogicos.Length; j++)
                {
                    if (Equals(SimbolosEncontrados[i], OperadoresLogicos[j]))
                    {
                        OperadoresLogicosEncontrados.Add(OperadoresLogicos[j]);
                        break;
                    }
                }

                //Operadores de Asignación
                for (int j = 0; j < OperadoresAsignacion.Length; j++)
                {
                    if (Equals(SimbolosEncontrados[i], OperadoresAsignacion[j]))
                    {
                        OperadoresAsignacionEncontrados.Add(OperadoresAsignacion[j]);
                        break;
                    }
                }

            }

            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("Palabras reservadas: ");
            foreach(string palabraReservada in PalabrasReservadasEncontradas)
            {
                Console.WriteLine(palabraReservada);
            }

            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("Delimitadores: ");
            foreach (string delimitador in DelimitadoresEncontrados)
            {
                Console.WriteLine(delimitador);
            }

            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("Operadores Aritmeticos: ");
            foreach (string operadorAritmetico in OperadoresAritmeticosEncontrados)
            {
                Console.WriteLine(operadorAritmetico);
            }

            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("Operadores Relacionales: ");
            foreach (string opepradorRelacional in OperadoresRelacionalesEncontrados)
            {
                Console.WriteLine(opepradorRelacional);
            }

            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("Operadores Logicos: ");
            foreach (string operadorLogico in OperadoresLogicosEncontrados)
            {
                Console.WriteLine(operadorLogico);
            }

            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("Operadores Asignacion: ");
            foreach (string operadorAsignacion in OperadoresAsignacionEncontrados)
            {
                Console.WriteLine(operadorAsignacion);
            }

            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("Identificadores: ");
            foreach (string indentificador in IdentificadoresEncontrados)
            {
                Console.WriteLine(indentificador);
            }

        }


        //principal
        static void Main(string[] args)
        {
            Console.WriteLine("Escriba algo");
            string cadena = Console.ReadLine();
            AnalizadorDeTokens(cadena);
            Console.ReadKey();

        }
    }
}

