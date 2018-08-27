/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.ast.altaAbstraccion;
using System;
using System.Collections.Generic;
using Interprete_Irony.entorno;

namespace Interprete_Irony.ast.controlFlujo
{
    /**
     * @class   If
     *
     * @brief   Clase que representa a la sentencia de control If, If-Else.
     *          
     * Para convertirlo en If,If-ElseIf,If-Else, es necesario modificar la gramatica y este nodo.
     * Una opcion es tomar al nodo if como una lista de "SubIfs".
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */
    public class If : Condicional, Instruccion
    {
        private LinkedList<NodoAST> instruccionesElse;

        /**
         * @fn  public If(Expresion condicion, LinkedList<NodoAST> instrucciones, LinkedList<NodoAST> instruccionesElse) 
         *      : base(condicion, instrucciones)
         *
         * @brief   Constructor de la clase If
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   condicion           La condicion que definira si se ejecutan las instrucciones.
         * @param   instrucciones       Las instrucciones que se ejecutaran si la condicion resulta 
         *                              ser True.
         * @param   instruccionesElse   The instrucciones else que se ejecutaran si la condicion 
         *                              resulta ser False.
         */

        public If(Expresion condicion, LinkedList<NodoAST> instrucciones, LinkedList<NodoAST> InstruccionesElse)
        : base(condicion, instrucciones)
        {
            this.InstruccionesElse = InstruccionesElse;
        }

        
        public object ejecutar(Entorno ent, AST arbol)
        {
            if ((bool)(Condicion.getValorImplicito(ent, arbol)))
            {
                // Si la condicion del if se cumple, entonces ejecuto las instrucciones.
                Entorno local = new Entorno(ent);
                foreach (NodoAST nodo in Instrucciones)
                {
                    /* 
                     * Por la estructura utilizada, se debe verificar si el nodo es 
                     * una expresion o una instruccion, ya que se manejan de diferente forma.
                     */
                    if (nodo is Instruccion)
                    {
                        Instruccion ins = (Instruccion)nodo;
                        object result = ins.ejecutar(local, arbol);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    else if (nodo is Expresion)
                    {
                        Expresion expr = (Expresion)nodo;
                        return expr.getValorImplicito(local, arbol);
                    }
                }
            }
            else
            {
                // Si la condicion del if NO se cumple, entonces ejecuto las instrucciones asociadas al Else.
                Entorno local = new Entorno(ent);
                foreach (NodoAST nodo in InstruccionesElse)
                {
                    /* 
                     * Por la estructura utilizada, se debe verificar si el nodo es 
                     * una expresion o una instruccion, ya que se manejan de diferente forma.
                     */
                    if (nodo is Instruccion)
                    {
                        Instruccion ins = (Instruccion)nodo;
                        object result = ins.ejecutar(local, arbol);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    else if (nodo is Expresion)
                    {
                        Expresion expr = (Expresion)nodo;
                        return expr.getValorImplicito(local, arbol);
                    }
                }
            }
            return null;
        }

        /**
         * @property    public LinkedList<NodoAST> InstruccionesElse
         *
         * @brief   Se obtienen o setean las instrucciones else
         *
         * @return  Las instrucciones else.
         */

        public LinkedList<NodoAST> InstruccionesElse
        {
            get
            {
                return instruccionesElse;
            }

            set
            {
                instruccionesElse = value;
            }
        }

        public string getC3D()
        {
            throw new NotImplementedException();
        }
    }
}
