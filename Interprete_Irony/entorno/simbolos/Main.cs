/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.ast.altaAbstraccion;
using System;
using System.Collections.Generic;

namespace Interprete_Irony.entorno.simbolos
{
    /**
     * @class   Main
     *
     * @brief   Esta clase representa la definicion del metodo principal
     *          del archivo .navarro. 
     * 
     * En el metodo ejecutar se ejecutan las instrucciones asociadas a tal metodo.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Main:Funcion
    {
        /**
         * @fn  public Main(Tipo tipo, string identificador, LinkedList<Simbolo> listaParametros, LinkedList<NodoAST> instrucciones) :base(tipo, identificador, listaParametros, instrucciones)
         *
         * @brief   Constructor de la Clase Main.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   tipo            El tipo de la funcion.
         * @param   identificador   El identificador de la funcion.
         * @param   listaParametros La lista de parametros que definen la funcion.
         * @param   instrucciones   Lista de las instrucciones que se encuentran dentro del método.
         */

        public Main(Tipos tipo, string identificador, LinkedList<Simbolo> listaParametros, LinkedList<NodoAST> instrucciones)
            :base(tipo, identificador, listaParametros, instrucciones)
        {
        }
    }
}
