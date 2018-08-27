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

namespace Interprete_Irony.entorno.simbolos
{
    /**
     * @class   Funcion
     *
     * @brief   Esta clase representa la definicion de una Funcion.
     * 
     * En el metodo ejecutar se ejecutan las instrucciones asociadas a la misma
     * cuando se realiza una llamada. 
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Funcion : Simbolo, Instruccion
    {
        private string tipoStruct;
        private LinkedList<NodoAST> instrucciones;
                
        /**
         * @fn  public Funcion(Tipo tipo, string identificador, LinkedList<Simbolo> listaParametros, LinkedList<NodoAST> instrucciones) 
         *      : base(identificador, tipo, listaParametros)
         *
         * @brief   Constructor de funcion de tipo primitivo.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   tipo            El tipo de la funcion.
         * @param   identificador   El identificador de la funcion.
         * @param   listaParametros La lista de parametros que definen la funcion.
         * @param   instrucciones   Lista de las instrucciones que se encuentran dentro del método.
         */

        public Funcion(Tipos tipo, string identificador, LinkedList<Simbolo> listaParametros, LinkedList<NodoAST> instrucciones)
            : base(identificador, tipo, listaParametros)
        {
            TipoStruct = string.Empty;
            this.Instrucciones = instrucciones;
        }

        /**
         * @fn  public Funcion(string tipoStruct, string identificador, LinkedList<Simbolo> listaParametros, LinkedList<NodoAST> instrucciones) : base(identificador, Tipo.OBJETO, listaParametros)
         *
         * @brief   Constructor de funcion de tipo Struct.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   tipoStruct      El identificador del Struct que generara el objeto a retornar.
         * @param   identificador   El identificador de la funcion.
         * @param   listaParametros La lista de parametros que definen la funcion.
         * @param   instrucciones   Lista de las instrucciones que se encuentran dentro del método.
         */

        public Funcion(string tipoStruct, string identificador, LinkedList<Simbolo> listaParametros, LinkedList<NodoAST> instrucciones)
             : base(identificador, Tipos.OBJETO, listaParametros)
        {
            this.TipoStruct = tipoStruct;
            this.Instrucciones = instrucciones;
        }


        public object ejecutar(Entorno ent, AST arbol)
        {
            foreach (NodoAST nodo in Instrucciones)
            {
                if (nodo is Instruccion)
                {
                    Instruccion ins = (Instruccion)nodo;
                    object result = ins.ejecutar(ent, arbol);
                    if (result != null)
                    {
                        if (verificarTipo(this.Tipo, result))
                        {
                            return result;
                        }
                        else
                        {
                            Program.getGUI().appendSalida("EL tipo del retorno no es el declarado en la funcion");
                            return null;
                        }

                    }
                }
                else if (nodo is Expresion)
                {
                    Expresion expr = (Expresion)nodo;
                    object result = expr.getValorImplicito(ent, arbol);
                    if (result != null)
                    {
                        if (expr.getTipo(ent, arbol) == this.Tipo)
                        {
                            return result;
                        }
                        else
                        {
                            Program.getGUI().appendSalida("EL tipo del retorno no es el declarado en la funcion");
                            return null;
                        }

                    }
                }
            }
            return null;
        }

        private bool verificarTipo(Tipos tipo, object result)
        {
            if (tipo == Tipos.INT && result is int)
            {
                return true;
            }
            if (tipo == Tipos.STRING && result is string)
            {
                return true;
            }
            else if (tipo == Tipos.CHAR && result is char)
            {
                return true;
            }
            else if (tipo == Tipos.DOUBLE && result is Double)
            {
                return true;
            }
            else if (tipo == Tipos.BOOL && result is bool)
            {
                return true;
            }
            else if (tipo == Tipos.OBJETO && result is Objeto)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /**
         * @property    public LinkedList<NodoAST> Instrucciones
         *
         * @brief   Se obtiene o setea la lista de instrucciones que se encuentran dentro del método.
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
         * @property    public string TipoStruct
         *
         * @brief  Se obtiene o setea el tipo de Struct que devolvera la funcion.
         *
         * @return  The tipo structure.
         */

        public string TipoStruct
        {
            get
            {
                return tipoStruct;
            }

            set
            {
                tipoStruct = value;
            }
        }

        public string getC3D()
        {
            throw new NotImplementedException();
        }
    }
}
