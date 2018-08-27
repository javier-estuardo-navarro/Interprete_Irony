/**
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.ast.altaAbstraccion;
using Interprete_Irony.ast.cambioFlujo;
using Interprete_Irony.entorno;
using System;
using System.Collections.Generic;

namespace Interprete_Irony.ast.bucles
{
    /**
     * @class   While
     *
     * @brief   Clase que representa el bucle While, este ciclo es basico y servira de base para 
     *          la construccion de los demas bucles.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class While : Condicional, Instruccion
    {
        /**
         * @fn  public While(LinkedList<NodoAST> Instrucciones, Expresion Condicion) : base(Condicion, Instrucciones)
         *
         * @brief   Constructor de clase While, el cual hereda el constructor de la Clase Condicional.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   Instrucciones   Las instrucciones de un Condicional.
         * @param   Condicion       La condicion de un Condicional.
         */

        public While(LinkedList<NodoAST> Instrucciones, Expresion Condicion) :
                base(Condicion, Instrucciones)
        {

        }

        public object ejecutar(Entorno ent, AST arbol)
        {
            
            siguiente:
            while ((bool)Condicion.getValorImplicito(ent, arbol))
            {
                // Se crea un entorno local para cada iteracion.
                Entorno local = new Entorno(ent);
                // Si la condicion del ciclo es verdadera, se procede a ejecutar la lista de instrucciones.
                foreach (Instruccion ins in Instrucciones)
                {
                    object result = ins.ejecutar(local, arbol);
                    if (result != null)
                    {
                        // Si existe un Return, se debe de retornar ese valor y conluir la ejecucion del ciclo.
                        return result;
                    }
                    if (ins is Break)
                    {
                        // Si es una sentencia Break, se debe de conluir la ejecucion del ciclo.
                        return null;
                    }
                    if (ins is Continue)
                    {
                        // Si es una sentencia Continue, se debe de pasar a la siguiente iteracion del ciclo.
                        goto siguiente;
                    }
                }
            }
            return null;
        }

        public string getC3D()
        {
            throw new NotImplementedException();
        }
    }
}
