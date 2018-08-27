/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.ast.altaAbstraccion;
using System;
using Interprete_Irony.entorno;
using static Interprete_Irony.entorno.Simbolo;
using Interprete_Irony.entorno.simbolos;

namespace Interprete_Irony.ast.valorImplicito
{
    /**
     * @class   Identificador
     *
     * @brief   Clase que representa la Expresion Identificador.
     *          
     * Un identificador puede tener implicito un objeto, una variable o incluso un arreglo.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    class Identificador : Expresion
    {
        private string identificador;

        public Identificador(string identificador)
        {
            this.identificador = identificador;
        }


        public Tipos getTipo(Entorno ent, AST arbol)
        {
            object valor = getValorImplicito(ent, arbol);
            if (valor is bool)
            {
                return Tipos.BOOL;
            }
            else if (valor is string)
            {
                return Tipos.STRING;
            }
            else if (valor is char)
            {
                return Tipos.CHAR;
            }
            else if (valor is int)
            {
                return Tipos.INT;
            }
            else if (valor is double)
            {
                return Tipos.DOUBLE;
            }
            else if (valor is Objeto)
            {
                return Tipos.OBJETO;
            }
            else
            {
                return Tipos.NULL;
            }
        }

        public object getValorImplicito(Entorno ent, AST arbol)
        {
            Simbolo sim = ent.get(identificador);
            /*
             * "sim" es un simbolo, es decir puede ser una variable de tipo primitivo, o bien, un objeto.
             * Si es un objeto devuelvo el Objeto como tal.
             * Si no es objeto, entonces es un dato primitivo.
             */
            return sim.Tipo == Tipos.OBJETO ? sim : sim.Valor;
        }

        public string getC3D()
        {
            throw new NotImplementedException();
        }
    }
}
