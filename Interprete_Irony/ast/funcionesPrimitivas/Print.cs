/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using System;
using Interprete_Irony.ast.altaAbstraccion;
using Interprete_Irony.entorno;

namespace Interprete_Irony.ast.funcionesPrimitivas
{
    /**
     * @class   Print
     *
     * @brief   Clase que representa la Funcion Print, imprime en formato de cadena 
     *          la expresion que se recibe, sin importar su valor.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Print : Instruccion
    {

        private Expresion expresion;

        /**
         * @fn  public Print(Expresion expresion)
         *
         * @brief   Constructor de la clase Print.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   expresion   La expresion que se imprimira.
         */

        public Print(Expresion expresion)
        {
            this.expresion = expresion;
        }
        

        public object ejecutar(Entorno ent, AST arbol)
        {
            object ob = expresion.getValorImplicito(ent, arbol);
            if (ob != null)
            {
                Program.getGUI().appendSalida(ob.ToString());
            }
            else
            {
                Program.getGUI().appendSalida("Se intento imprimir una expresion nula");
            }
            return null;
        }

        public string getC3D()
        {
            throw new NotImplementedException();
        }
    }
}
