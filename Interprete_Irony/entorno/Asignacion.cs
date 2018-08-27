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
using Interprete_Irony.ast.valorImplicito;
using static Interprete_Irony.entorno.Simbolo;
using Interprete_Irony.entorno.simbolos;

namespace Interprete_Irony.entorno
{
    /**
     * @class   Asignacion
     *
     * @brief   Clase que representa la asignacion de una expresion a una variable.
     *          
     * La variable a la que se le asignara la expresion puede ser de tipo primitivo
     * un entero, caracter, etc. o bien un acceso a un atributo de un objeto determinado.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Asignacion : Instruccion
    {
        private string idObjeto;
        private string idAtributo;
        private LinkedList<Simbolo> variables;
        private bool accesoObjeto;
        private Expresion valor;

        /**
         * @fn  public Asignacion(LinkedList<Simbolo> variables, Expresion valor)
         *
         * @brief   Constructor de la instrucción asignación.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   variables   Las variables a las que se va a asignar el valor.
         * @param   valor       Expresion que se va a asignar a las variables.
         */

        public Asignacion(LinkedList<Simbolo> variables, Expresion valor)
        {
            this.variables = variables;
            this.valor = valor;
            this.accesoObjeto = false;
            if (valor == null)
            {
                Program.getGUI().appendSalida("El valor por asignar es nulo o no valido.");
            }

        }

        /**
         * @fn  public Asignacion(string idObjeto, string idAtributo, Expresion valor)
         *
         * @brief   Constructor de la instrucción asignación para un acceso a un objeto.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   idObjeto    El identificador del objeto al que le pertenece el atributo a modificar.
         * @param   idAtributo  El identificador atributo a modificar.
         * @param   valor       Expresion que se va a asignar al atributo del objeto respectivo.
         */

        public Asignacion(string idObjeto, string idAtributo, Expresion valor)
        {
            this.valor = valor;
            this.idAtributo = idAtributo;
            this.idObjeto = idObjeto;
            accesoObjeto = true;
            variables = new LinkedList<Simbolo>();
            if (valor == null)
            {
                Program.getGUI().appendSalida("El valor por asignar es nulo o no valido.");
            }
        }
        public object ejecutar(Entorno ent, AST arbol)
        {
            object val = valor.getValorImplicito(ent, arbol);
            if (accesoObjeto)
            {
                Entorno atributosSinModificar = ((Objeto)ent.get(idObjeto)).getAtributos();
                Entorno atributosNuevos = new Entorno(null);
                foreach (Simbolo atributo in atributosSinModificar.Tabla.Values)
                {
                    if (!atributo.Identificador.Equals(idAtributo, StringComparison.InvariantCultureIgnoreCase))
                    {
                        atributosNuevos.agregar(atributo.Identificador, atributo);
                    }
                }
                atributosNuevos.agregar(idAtributo, new Simbolo(atributosSinModificar.get(idAtributo).Tipo, idAtributo, val));
                ent.reemplazar(idObjeto, new Objeto(idObjeto, atributosNuevos));
            }
            else
            {
                foreach (Simbolo variable in variables)
                {
                    string nombreVariable = variable.Identificador;
                    Tipos tipoVal = valor.getTipo(ent, arbol);
                    Tipos tipoVar = ent.get(nombreVariable).Tipo;
                    if (valor is Llamada)
                    {
                        Llamada ob = (Llamada)valor;
                        if (ob.getTipo(ent, arbol) == Tipos.NULL)
                        {
                            Program.getGUI().appendSalida("No es posible asignarle valor a: " + idAtributo + ","
                                           + "Debido a que las funciones de tipo void (Procedimientos) "
                                           + "No devuelven un valor ");
                            return null;
                        }
                    }
                    if (tipoVal == tipoVar)
                    {
                        if (val is Objeto)
                        {
                            ent.reemplazar(nombreVariable, (Objeto)val);
                        }
                        else
                        {
                            ent.get(nombreVariable).Valor = val;
                        }
                    }
                    else
                    {
                        Program.getGUI().appendSalida("Error de tipos"
                                + ", se intenta setear un valor a la variable "
                                + nombreVariable + " diferente al que fue declarado");
                        return null;
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
