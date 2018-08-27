/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.ast;
using Interprete_Irony.ast.altaAbstraccion;
using System;
using System.Collections.Generic;
using static Interprete_Irony.entorno.Simbolo;

namespace Interprete_Irony.entorno
{
    /**
     * @class   Declaracion
     *
     * @brief   Clase que representa la declaracion de una variable, la cual puede estar inicializada.
     *          
     * La variable que sera declarada y posiblemente inicializada con la expresion puede ser de tipo 
     * primitivo un entero, caracter, objeto etc. 
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Declaracion : Instruccion
    {

        private Expresion valorInicilizacion;

        private LinkedList<Simbolo> variables;

        /**
         * @fn  bool isInicializacion()
         *
         * @brief   Consulta si la declaracion es tambien una inicializacion.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @return  True si es inicializacion, false si no.
         */

        bool isInicializacion()
        {
            return this.valorInicilizacion != null;
        }

        /**
         * @fn  public Declaracion(Tipo tipo, LinkedList<Simbolo> variables, Expresion valor)
         *
         * @brief   Constructor de la clase Declaracion, cuando la misma es una inicializacion.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   tipo        Tipo de los elementos declarados
         * @param   variables   Lista de todos los elementos declarados
         * @param   valor       Expresion que con el que se van a inicializar 
         *                      todos las variables declarados.
         */

        public Declaracion(Tipos tipo, LinkedList<Simbolo> variables, Expresion valor)
        {
            foreach (Simbolo variable in variables)
            {
                variable.Tipo = tipo;
            }
            this.variables = variables;
            this.valorInicilizacion = valor;
        }

        /**
         * @fn  public Declaracion(Tipo tipo, Simbolo variable)
         *
         * @brief   Constructor de la clase Declaracion
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   tipo        Tipo de los elementos declarados
         * @param   variable    Elementos declarado
         */

        public Declaracion(Tipos tipo, Simbolo variable)
        {
            variable.Tipo = tipo;
            LinkedList<Simbolo> variables = new LinkedList<Simbolo>();
            variables.AddLast(variable);
            this.variables = variables;
            this.valorInicilizacion = null;
        }

        public object ejecutar(Entorno ent, AST arbol)
        {
            foreach (Simbolo variable in variables)
            {
                string nombreVariable = variable.Identificador;
                if (isInicializacion())
                {
                    Tipos tipoVal = valorInicilizacion.getTipo(ent, arbol);
                    Tipos tipoVar = variable.Tipo;
                    if (ent.existeEnActual(nombreVariable))
                    {
                        Program.getGUI().appendSalida("Se intento declarar " + nombreVariable
                                + "una variable ya existente en el entorno actual");
                    }
                    else
                    {
                        if (tipoVal == tipoVar)
                        {
                            object val = valorInicilizacion.getValorImplicito(ent, arbol);
                            variable.Valor = val;
                            ent.agregar(nombreVariable, variable);
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Error de tipos"
                                    + ", se intenta setear un valor a la variable "
                                    + nombreVariable + " diferente al que fue declarado");
                        }
                    }
                }
                else
                {
                    if (ent.existeEnActual(nombreVariable))
                    {
                        Program.getGUI().appendSalida("Se intento declarar "
                                + "una variable ya existente en el entorno actual");
                    }
                    else
                    {
                        ent.agregar(nombreVariable, variable);
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
