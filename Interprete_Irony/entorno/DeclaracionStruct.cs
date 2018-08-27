/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.ast.altaAbstraccion;
using Interprete_Irony.entorno;
using Interprete_Irony.entorno.simbolos;
using System;

namespace Interprete_Irony.ast.entorno
{
    /**
     * @class   DeclaracionStruct
     *
     * @brief   La clase DeclaracionStruct es un nodo en el cual se declara un 
     *          Struct y se agrega al entorno. 
     * 
     * Esto es equivalente a instanciar una clase en un lenguaje orientado a objetos.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class DeclaracionStruct : Instruccion
    {
        private string structGenerador;
        private string identificador;

        /**
         * @fn  public DeclaracionStruct(string structGenerador, string identificador)
         *
         * @brief   Constructor de la Declaracion de un Struct.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   structGenerador El Struct generador.
         * @param   identificador   El identificador de la ocurrencia del Struct.
         */

        public DeclaracionStruct(string structGenerador, string identificador)
        {
            this.structGenerador = structGenerador;
            this.identificador = identificador;
        }


        public object ejecutar(Entorno ent, AST arbol)
        {
            /* 
             * Los Structs se encuentran almacenados en el arbol, asi que podemos acceder a su informacion 
             * y luego trasladarlo al entorno actual para asi poder declarar Structs del mismo.
             * 
             * Esto no debe ser obligatoriamente asi, podriamos almacenar dicha informacion de una vez 
             * en la tabla de simbolos durante el analisis sintactico pero eso quedaría de tarea al lector.
            */
            foreach (Struct clase in arbol.Structs)
            {
                if (clase.Identificador.Equals(structGenerador, StringComparison.InvariantCultureIgnoreCase))
                {
                    /* 
                     * Si se encuentra el Struct del cual queremos generar un objeto o inicializar
                     * (si es con clases, instanciar un objeto), debemos cargar a un entorno los atributos
                     * para que asi se puedan acceder o modificar.
                    */
                    Entorno atributos = new Entorno(null);
                    foreach (Declaracion declaracion in clase.Declaraciones)
                    {
                        declaracion.ejecutar(atributos, arbol);
                    }
                    // Cuando se han cargado los atributos, se crea el objeto y se agrega al entorno.
                    ent.agregar(identificador, new Objeto(structGenerador, atributos));
                    return null;
                }
            }
            Program.getGUI().appendSalida("Struct inexistente");
            return null;
        }

        public string getC3D()
        {
            throw new NotImplementedException();
        }
    }
}
