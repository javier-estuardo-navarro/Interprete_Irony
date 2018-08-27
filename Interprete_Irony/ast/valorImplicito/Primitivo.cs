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

namespace Interprete_Irony.ast.valorImplicito
{
    /**
     * @class   Primitivo
     *
     * @brief   Representa a todos los datos de tipo primitivo del lenguaje.
     *           
     * En este caso son cinco: booleano, entero, decimal, cadena y caracter.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Primitivo : Expresion
    {
        /*
         * 
         */
        private object valor;

        /**
         * @fn  public Primitivo(object valor)
         *
         * @brief   Constructor de un Primitivo.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   valor   Variable que se dedica a almacenar el valor específico de la valor que ha
         * reconocido.
         */

        public Primitivo(object valor)
        {
            this.valor = valor;
        }
        public Tipos getTipo(Entorno ent, AST arbol)
        {
            object valor = this.getValorImplicito(ent, arbol);
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
            else 
            {
                return Tipos.OBJETO;
            }
        }

        public object getValorImplicito(Entorno ent, AST arbol)
        {
            return valor;
        }

        public string getC3D()
        {
            throw new NotImplementedException();
        }

    }
}
