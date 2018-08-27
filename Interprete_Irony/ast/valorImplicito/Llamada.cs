/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.ast.altaAbstraccion;
using System;
using Interprete_Irony.entorno;
using System.Collections.Generic;
using static Interprete_Irony.entorno.Simbolo;
using Interprete_Irony.entorno.simbolos;
using System.Collections;

namespace Interprete_Irony.ast.valorImplicito
{
    /**
     * @class   Llamada
     *
     * @brief   Clase que representa la llamada a cierto método o función.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Llamada : Expresion
    {
       
        private string identificador;

        private LinkedList<Expresion> valores;

        /**
         * @fn  public Llamada(string identificador)
         *
         * @brief   Constructor de la Clase LLamada.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   identificador   Identificador específico de la funcion 
         *                          a la que se esta llamando.
         */

        public Llamada(string identificador)
        {
            this.identificador = identificador;
            this.valores = new LinkedList<Expresion>();
        }

        /**
         * @fn  public Llamada(string identificador, LinkedList<Expresion> valores)
         *
         * @brief   Constructor de la Clase LLamada.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   identificador   Identificador específico de la funcion a la que 
         *                          se esta llamando.
         * @param   valores         Lista de valores que sirven de parámetros al 
         *                          método que se esta invocando.
         */

        public Llamada(string identificador, LinkedList<Expresion> valores)
        {
            this.identificador = identificador;
            this.valores = valores;
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
            if (ent.existe(identificador))
            {
                //La funcion existe en el entorno actual o externo
                Funcion funcion = (Funcion)ent.get(identificador);
                Entorno local = new Entorno(ent);
                /*
                 * Tras acceder al simbolo que representa la funcion que se llamara en el entorno
                 * Se crea un entorno local para el manejo de las variables locales a dicha funcion.
                 */
                if (verificarParametros(valores, funcion.ListaParametros, local, arbol))
                {
                    // Tras verificar que los parametros correspondan en longitud y tipo, se ejecuta la funcion.
                    return funcion.ejecutar(local, arbol);
                }
            }
            else
            {
                Program.getGUI().appendSalida("La funcion" + identificador + " no ha sido declarada");
            }
            return null;
        }

        /**
        * @fn  bool verificarParametros(LinkedList<Expresion> valores, LinkedList<Simbolo> parametros, Entorno ent, AST arbol)
        *
        * @brief   Verifica los parametros, su tipo y su longitud.
        *
        * @author  Javier Estuardo Navarro
        * @date    26/08/2018
        *
        * @param   valores     Los valores con los que se llamo la funcion.
        * @param   parametros  Los parametros que definen la funcion.
        * @param   ent     Un entorno actual de ejecucion el cual gestiona todo lo relacionado con 
        *                  la tabla de simbolos.
        * @param   arbol   Un Arbol Abstracto de Analisis Sintactico, el cual esta compuesto por NodosAST
        *                  los cuales pueden ser Expresiones o Instrucciones.
        *
        * @return  True if it succeeds, false if it fails.
        */

        bool verificarParametros(LinkedList<Expresion> valores, LinkedList<Simbolo> parametros, Entorno ent, AST arbol)
        {
            // Esto se agrego debido a que LinkedList no tiene metodo get(index) o ElementAt(index)
            IList param = new List<Simbolo>(parametros);
            IList vals = new List<Expresion>(valores);
            if (vals.Count == param.Count)
            {
                // La cantidad de valores y parametros es la correcta.

                /* 
                 * Variables auxiliares que permitirán verificacion de tipos y carga de parametros 
                 * al entorno local de la función.
                */
                Simbolo sim_aux;
                string id_aux;
                Tipos tipoPar_aux;
                Tipos tipoVal_aux;
                Expresion exp_aux;
                object val_aux;

                for (int i = 0; i < parametros.Count; i++)
                {
                    sim_aux = (Simbolo)param[i];
                    id_aux = sim_aux.Identificador;
                    tipoPar_aux = sim_aux.Tipo;

                    exp_aux = (Expresion)vals[i];
                    tipoVal_aux = exp_aux.getTipo(ent, arbol);
                    val_aux = exp_aux.getValorImplicito(ent, arbol);
                    if (tipoPar_aux == tipoVal_aux)
                    {
                        // Si los tipos corresponden se agregan los parametros con su respectivo valor.
                        ent.agregar(id_aux, new Simbolo(tipoPar_aux, id_aux, val_aux));
                    }
                    else
                    {
                        Program.getGUI().appendSalida("Llamada a: " + identificador + ", El tipo de los parametros no coinciden con "
                                + "el valor correspondiente utilizado en la llamada, Parametro:" + id_aux);
                        return false;
                    }
                }
                return true;
            }
            else
            {
                Program.getGUI().appendSalida("Llamada a: " + identificador + "La cantidad de parametros no coincide");
            }
            return false;
        }

        public string getC3D()
        {
            throw new NotImplementedException();
        }

    }
}
