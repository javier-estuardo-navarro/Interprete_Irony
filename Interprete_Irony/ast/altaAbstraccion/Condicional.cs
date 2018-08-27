/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using System.Collections.Generic;

namespace Interprete_Irony.ast.altaAbstraccion
{
    /**
     * @class   Condicional
     *
     * @brief   Esta clase heredará a todas las clases que realizan o no cierto 
     *          conjunto de instrucciones dependiendo de cierta condicion. 
     * 
     * Por ejemplo un if, realiza todas las intrucciones que contiene cumple la cierta 
     * condicion, el While ejecuta cierto conjunto de instrucciones repetidamente 
     * mientras se cumple cierta condición y así con todas las instrucciones de este tipo.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */
    public class Condicional
    {

        private LinkedList<NodoAST> instrucciones;

        private Expresion condicion;

        /**
         * @fn  public Condicional(Expresion Condicion, LinkedList<NodoAST> Instrucciones)
         *
         * @brief   Constructor de la clase Condicional.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   Condicion       Condición de la que depende la ejecución o no ejecución
         *                          de la lista de instrucciones.
         * @param   Instrucciones   Lista de instrucciones que se ejecutaran en funcion
         *                          de la varible Condicion.
         */

        public Condicional(Expresion Condicion, LinkedList<NodoAST> Instrucciones)
        {
            this.Instrucciones = Instrucciones;
            this.Condicion = Condicion;
        }

        /**
         * @property    public LinkedList<NodoAST> Instrucciones
         *
         * @brief   Se obtiene o setea la lista de instrucciones que se ejecutaran en funcion de la
         *          varible Condicion.
         *
         * @return  La lista de instrucciones.
         */

        public LinkedList<NodoAST> Instrucciones
        {
            get
            {
                return instrucciones;
            }

            set
            {
                instrucciones = value;
            }
        }

        /**
         * @property    public Expresion Condicion
         *
         * @brief   Se obtiene o setea la condición de la que depende la ejecución
         *          o no ejecución de la lista de instrucciones.
         *
         * @return  La condicion.
         */

        public Expresion Condicion
        {
            get
            {
                return condicion;
            }

            set
            {
                condicion = value;
            }
        }

    }
}
