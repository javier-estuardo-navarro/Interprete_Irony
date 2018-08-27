/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.ast.altaAbstraccion;
using System;
using System.Collections.Generic;
using Interprete_Irony.ast;

namespace Interprete_Irony.entorno
{
    /**
     * @class   DefinicionStruct
     *
     * @brief   Esta clase representa la Definicion de un Struct.
     *          
     * Este es el molde para las demas ocurrencias de los Structs, tras haberlo Definido y apodemos Declarar
     * variables de este tipo.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class DefinicionStruct : Instruccion
    {
        private string identificador;
        private LinkedList<Declaracion> atributos;

        /**
         * @fn  public DefinicionStruct(string identificador, LinkedList<Declaracion> parametros)
         *
         * @brief   Constructor de la Definicion de un Struct.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   identificador   El identificador del Struct.
         * @param   atributos      Los atributos del Struct.
         */

        public DefinicionStruct(string identificador, LinkedList<Declaracion> atributos)
        {
            this.identificador = identificador;
            this.atributos = atributos;
        }
        public object ejecutar(Entorno ent, AST arbol)
        {
            arbol.agregarStruct(new Struct(atributos, identificador));
            return null;
        }

        public string getC3D()
        {
            throw new NotImplementedException();
        }

    }
}
