/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.entorno;
using System.Collections.Generic;

namespace Interprete_Irony.ast.altaAbstraccion
{
    /**
    * @class   Struct
    *
    * @brief   Clase Struct, esta será la base para trabajar con clases.
    *
    * Un Struct es un conjunto de atributos asociados a un identificador.    * 
    * Diferencias entre una clase y un Struct:
    * 
    * 1. Un Struct genera automáticamente un inicializador, mientras que en las clases no lo hacen.
    * 2. Los Struct son "por valor" y las clases son “por referencia”.
    * 3. Un Struct no puede manejar herencia, las clases si.
    * 4. Una clase tiene acciones (metodos o funciones) asociadas, un Struct no.
    * 
    * Para hacer que este interprete tenga la capacidad de manejar clases, deben cubrirse las diferencias
    * anteriores, es decir, se le deben agregar metodos o funciones, asi como una visibilidad, etc.
    * 
    * Por el momento son Structs el los que unicamente se incluyen atributos de tipo primitivo 
    * (enteros, caracteres, etc).
    *
    * @author  Javier Estuardo Navarro
    * @date    26/08/2018
    */
    public class Struct
    {

        private LinkedList<Declaracion> declaraciones;

        private string identificador;

        /**
         * @fn  public Struct(LinkedList<Declaracion> declaraciones, string identificador)
         *
         * @brief   Constructor de la clase Condicional.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param Instrucciones: Lista de instrucciones que se ejecutaran en funcion
         *                       de la varible Condicion.
         * @param Condicion: Condición de la que depende la ejecución o no ejecución
         *                   de la lista de instrucciones.
         */
        public Struct(LinkedList<Declaracion> declaraciones, string identificador)
        {
            this.Declaraciones = declaraciones;
            this.Identificador = identificador;
        }

        /**
         * @property    public LinkedList<Declaracion> Declaraciones
         *
         * @brief   Se obtiene o setea la lista de declaraciones que representan los 
         *          atributos que definen el Struct.
         *
         * @return  La lista de declaraciones.
         */

        public LinkedList<Declaracion> Declaraciones
        {
            get
            {
                return declaraciones;
            }

            set
            {
                declaraciones = value;
            }
        }

        /**
         * @property    public string Identificador
         *
         * @brief   Se obtiene o setea la cadena que identifica el Struct.
         *
         * @return  El identificador.
         */

        public string Identificador
        {
            get
            {
                return identificador;
            }

            set
            {
                identificador = value;
            }
        }
    }
}
