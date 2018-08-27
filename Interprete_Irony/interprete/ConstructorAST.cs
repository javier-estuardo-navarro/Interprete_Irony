/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
 */

using Interprete_Irony.ast;
using Interprete_Irony.ast.altaAbstraccion;
using Interprete_Irony.ast.bucles;
using Interprete_Irony.ast.cambioFlujo;
using Interprete_Irony.ast.controlFlujo;
using Interprete_Irony.ast.entorno;
using Interprete_Irony.ast.funcionesPrimitivas;
using Interprete_Irony.ast.valorImplicito;
using Interprete_Irony.entorno;
using Interprete_Irony.entorno.simbolos;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using static Interprete_Irony.entorno.Simbolo;

namespace Interprete_Irony.interprete
{
    /**
     * @class   ConstructorAST
     *
     * @brief   Clase ConstructorAST, en la cual se construira el AST.
     *          
     * Es importante llegar al convenio que la unica funcion del arbol ParseTree de Irony, sera
     * permitir la construccion de nuestro propio arbol.
     * 
     * Las ventajas son notables al ser multiplataforma y facil de traducir las acciones.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class ConstructorAST
    {
        /**
         * @fn  public AST Analizar(ParseTreeNode raiz)
         *
         * @brief   Metodo que devuelve un AST construido
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   raiz    La raiz del ParseTree de Irony
         *
         * @return  El AST construido.
         */

        public AST Analizar(ParseTreeNode raiz)
        {
            return (AST)auxiliar(raiz);
        }

        /**
         * @fn  public object auxiliar(ParseTreeNode actual)
         *
         * @brief   Metodo recursivo auxiliar con el que se recorrera el arbol generado por Irony
         *          para que sea posible construir nuestro propio AST.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   actual  El nodo actual.
         *
         * @return  Un Objecto cualquiera, puede ser cualquier tipo de nodo.
         */

        public object auxiliar(ParseTreeNode actual)
        {
            if (EstoyAca(actual, "INI"))
            {
                return auxiliar(actual.ChildNodes[0]);
            }
            else if (EstoyAca(actual, "FIELD_DECLARATION"))
            {
                LinkedList<Instruccion> instrucciones = new LinkedList<Instruccion>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    instrucciones.AddLast((Instruccion)auxiliar(hijo));
                }
                return new AST(instrucciones);
            }
            else if (EstoyAca(actual, "FIELD"))
            {
                if (actual.ChildNodes.Count == 1)
                {
                    return auxiliar(actual.ChildNodes[0]);
                }
                else
                {
                    return new DefinicionStruct(getLexema(actual, 1), (LinkedList<Declaracion>)auxiliar(actual.ChildNodes[2]));
                }
            }
            else if (EstoyAca(actual, "METHOD_DECLARATION"))
            {
                if (actual.ChildNodes.Count == 4)
                {

                    if (EstoyAca(actual.ChildNodes[0], "TIPO"))
                    {
                        return new Funcion((Tipos)auxiliar(actual.ChildNodes[0]), getLexema(actual, 1), (LinkedList<Simbolo>)auxiliar(actual.ChildNodes[2]), (LinkedList<NodoAST>)auxiliar(actual.ChildNodes[3]));
                    }
                    else
                    {
                        // Devuelve un struct
                        return new Funcion(getLexema(actual, 0), getLexema(actual, 1), (LinkedList<Simbolo>)auxiliar(actual.ChildNodes[2]), (LinkedList<NodoAST>)auxiliar(actual.ChildNodes[3]));
                    }
                }
                else
                {
                    if (EstoyAca(actual.ChildNodes[0], "TIPO"))
                    {
                        if (EstoyAca(actual.ChildNodes[1], "pr_main"))
                        {
                            return new Main((Tipos)auxiliar(actual.ChildNodes[0]), getLexema(actual, 1), new LinkedList<Simbolo>(), (LinkedList<NodoAST>)auxiliar(actual.ChildNodes[2]));
                        }
                        else
                        {
                            return new Funcion((Tipos)auxiliar(actual.ChildNodes[0]), getLexema(actual, 1), new LinkedList<Simbolo>(), (LinkedList<NodoAST>)auxiliar(actual.ChildNodes[2]));
                        }
                    }
                    else if (EstoyAca(actual.ChildNodes[0], "identificador"))
                    {
                        // Devuelve un struct
                        return new Funcion(getLexema(actual, 0), getLexema(actual, 1), new LinkedList<Simbolo>(), (LinkedList<NodoAST>)auxiliar(actual.ChildNodes[2]));
                    }
                }
            }
            else if (EstoyAca(actual, "LISTA_PARAMETROS"))
            {
                LinkedList<Simbolo> parametros = new LinkedList<Simbolo>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    parametros.AddLast((Simbolo)auxiliar(hijo));
                }
                return parametros;
            }
            else if (EstoyAca(actual, "PARAMETRO"))
            {
                return new Simbolo((Tipos)auxiliar(actual.ChildNodes[0]), getLexema(actual, 1));
            }
            else if (EstoyAca(actual, "LISTA_ATRIBUTOS"))
            {
                LinkedList<Declaracion> atributos = new LinkedList<Declaracion>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    atributos.AddLast((Declaracion)auxiliar(hijo));
                }
                return atributos;
            }
            else if (EstoyAca(actual, "ATRIBUTO"))
            {
                return new Declaracion((Tipos)auxiliar(actual.ChildNodes[0]), new Simbolo(getLexema(actual, 1)));
            }
            else if (EstoyAca(actual, "BLOQUE_SENTENCIAS"))
            {
                return auxiliar(actual.ChildNodes[0]);
            }
            else if (EstoyAca(actual, "STATEMENTS"))
            {
                LinkedList<NodoAST> sentencias = new LinkedList<NodoAST>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    sentencias.AddLast((NodoAST)auxiliar(hijo));
                }
                return sentencias;
            }
            else if (EstoyAca(actual, "SENTENCIA"))
            {
                return auxiliar(actual.ChildNodes[0]);
            }
            else if (EstoyAca(actual, "DECLARACION"))
            {
                if (actual.ChildNodes.Count == 3)
                {
                    return new Declaracion((Tipos)auxiliar(actual.ChildNodes[0]), (LinkedList<Simbolo>)auxiliar(actual.ChildNodes[1]), (Expresion)auxiliar(actual.ChildNodes[2]));
                }
                else
                {
                    if (EstoyAca(actual.ChildNodes[0], "TIPO"))
                    {
                        return new Declaracion((Tipos)auxiliar(actual.ChildNodes[0]), (LinkedList<Simbolo>)auxiliar(actual.ChildNodes[1]), null);
                    }
                    else
                    {
                        return new DeclaracionStruct(getLexema(actual, 0), getLexema(actual, 1));
                    }
                }

            }
            else if (EstoyAca(actual, "ASIGNACION"))
            {
                if (actual.ChildNodes.Count == 2)
                {
                    return new Asignacion((LinkedList<Simbolo>)auxiliar(actual.ChildNodes[0]), (Expresion)auxiliar(actual.ChildNodes[1]));
                }
                else
                {
                    return new Asignacion(getLexema(actual, 0), getLexema(actual, 1), (Expresion)auxiliar(actual.ChildNodes[2]));
                }

            }
            else if (EstoyAca(actual, "LISTA_SIM"))
            {
                LinkedList<Simbolo> simbolos = new LinkedList<Simbolo>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    simbolos.AddLast(new Simbolo(hijo.Token.Text));
                }
                return simbolos;
            }
            else if (EstoyAca(actual, "SENTENCIA_IF"))
            {
                if (actual.ChildNodes.Count == 3)
                {
                    return new If((Expresion)auxiliar(actual.ChildNodes[1]), (LinkedList<NodoAST>)auxiliar(actual.ChildNodes[2]), new LinkedList<NodoAST>());
                }
                else
                {
                    return new If((Expresion)auxiliar(actual.ChildNodes[1]), (LinkedList<NodoAST>)auxiliar(actual.ChildNodes[2]), (LinkedList<NodoAST>)auxiliar(actual.ChildNodes[4]));
                }
            }
            else if (EstoyAca(actual, "SENTENCIA_WHILE"))
            {
                return new While((LinkedList<NodoAST>)auxiliar(actual.ChildNodes[2]), (Expresion)auxiliar(actual.ChildNodes[1]));
            }
            else if (EstoyAca(actual, "SENTENCIA_RETURN"))
            {
                if (actual.ChildNodes.Count == 2)
                {
                    return new Return((Expresion)auxiliar(actual.ChildNodes[1]));
                }
                else
                {
                    return new Return();
                }
            }
            else if (EstoyAca(actual, "SENTENCIA_BREAK"))
            {
                return new Break();
            }
            else if (EstoyAca(actual, "SENTENCIA_CONTINUE"))
            {
                return new Continue();
            }
            else if (EstoyAca(actual, "SENTENCIA_PRINT"))
            {
                return new Print((Expresion)auxiliar(actual.ChildNodes[1]));
            }
            else if (EstoyAca(actual, "SENTENCIA_STRUCT"))
            {
                return new DefinicionStruct(getLexema(actual, 1), (LinkedList<Declaracion>)auxiliar(actual.ChildNodes[2]));
            }
            else if (EstoyAca(actual, "SENTENCIA_LLAMADA"))
            {
                if (actual.ChildNodes.Count == 2)
                {
                    return new Llamada(getLexema(actual, 0), (LinkedList<Expresion>)auxiliar(actual.ChildNodes[1]));
                }
                else
                {
                    return new Llamada(getLexema(actual, 0));
                }
            }
            else if (EstoyAca(actual, "SENTENCIA_ACCESO"))
            {
                return new AccesoObjeto(getLexema(actual, 0), getLexema(actual, 1));
            }
            else if (EstoyAca(actual, "EXPRESION"))
            {
                return auxiliar(actual.ChildNodes[0]);
            }
            else if (EstoyAca(actual, "EXPRESION_ARITMETICA"))
            {
                if (actual.ChildNodes.Count == 3)
                {
                    return new Operacion((Expresion)auxiliar(actual.ChildNodes[0]), (Expresion)auxiliar(actual.ChildNodes[2]), Operacion.getOperador(getLexema(actual, 1)));
                }
                else if (actual.ChildNodes.Count == 2)
                {
                    return new Operacion((Expresion)auxiliar(actual.ChildNodes[1]), Operacion.Operador.MENOS_UNARIO);
                }
            }
            else if (EstoyAca(actual, "EXPRESION_RELACIONAL"))
            {
                return new Operacion((Expresion)auxiliar(actual.ChildNodes[0]), (Expresion)auxiliar(actual.ChildNodes[2]), Operacion.getOperador(getLexema(actual, 1)));
            }
            else if (EstoyAca(actual, "EXPRESION_LOGICA"))
            {
                if (actual.ChildNodes.Count == 3)
                {
                    return new Operacion((Expresion)auxiliar(actual.ChildNodes[0]), (Expresion)auxiliar(actual.ChildNodes[2]), Operacion.getOperador(getLexema(actual, 1)));
                }
                else if (actual.ChildNodes.Count == 2)
                {
                    return new Operacion((Expresion)auxiliar(actual.ChildNodes[1]), Operacion.Operador.NOT);
                }
            }
            else if (EstoyAca(actual, "PRIMITIVO"))
            {

                if (EstoyAca(actual.ChildNodes[0], "numero"))
                {
                    double result = Convert.ToDouble(getLexema(actual, 0));
                    try
                    {
                        int result2 = Convert.ToInt32(getLexema(actual, 0));
                        return new Primitivo(result2);

                    }
                    catch (Exception)
                    {
                        return new Primitivo(result);
                    }
                }
                else if (EstoyAca(actual.ChildNodes[0], "cadena"))
                {
                    string aux = getLexema(actual, 0).ToString();
                    aux = aux.Replace("\\n", "\n");
                    aux = aux.Replace("\\t", "\t");
                    aux = aux.Replace("\\r", "\r");
                    aux = aux.Substring(1, aux.Length - 2);
                    return new Primitivo(aux);
                }
                else if (EstoyAca(actual.ChildNodes[0], "caracter"))
                {
                    try
                    {
                        string aux = getLexema(actual, 0);
                        char result = Convert.ToChar(aux.Substring(1, 1));
                        return new Primitivo(result);
                    }
                    catch (Exception)
                    {

                    }
                }
                else if (EstoyAca(actual.ChildNodes[0], "true"))
                {
                    return new Primitivo(true);
                }
                else if (EstoyAca(actual.ChildNodes[0], "false"))
                {
                    return new Primitivo(false);
                }
                else if (EstoyAca(actual.ChildNodes[0], "identificador"))
                {
                    return new Identificador(getLexema(actual, 0));
                }
            }
            else if (EstoyAca(actual, "LISTA_ARGUMENTOS"))
            {
                LinkedList<Expresion> argumentos = new LinkedList<Expresion>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    argumentos.AddLast((Expresion)auxiliar(hijo));
                }
                return argumentos;
            }
            else if (EstoyAca(actual, "TIPO"))
            {
                if (EstoyAca(actual.ChildNodes[0], "String"))
                {
                    return Tipos.STRING;
                }
                else if (EstoyAca(actual.ChildNodes[0], "bool"))
                {
                    return Tipos.BOOL;
                }
                else if (EstoyAca(actual.ChildNodes[0], "int"))
                {
                    return Tipos.INT;
                }
                else if (EstoyAca(actual.ChildNodes[0], "double"))
                {
                    return Tipos.DOUBLE;
                }
                else if (EstoyAca(actual.ChildNodes[0], "char"))
                {
                    return Tipos.CHAR;
                }
                else
                {
                    return Tipos.OBJETO;
                }
            }
            return null;
        }

        /**
         * @fn  static bool comparar(string a, string b)
         *
         * @brief   Comparara dos cadenas sin tomar en cuenta mayusculas y minusculas.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   a   cadena a.
         * @param   b   cadena b.
         *
         * @return  True si son iguales, false si son diferentes.
         */

        static bool comparar(string a, string b)
        {
            return (a.Equals(b, System.StringComparison.InvariantCultureIgnoreCase));
        }

        /**
         * @fn  static bool EstoyAca(ParseTreeNode nodo, string nombre)
         *
         * @brief   Estoy aca define en que Parte de la gramatica me encuentro.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   nodo    El No terminal del lado izquierdo de la gramatica.
         * @param   nombre  El nombre de dicho No terminal..
         *
         * @return  True Si me encuentro en ese no terminal, false si no.
         */

        static bool EstoyAca(ParseTreeNode nodo, string nombre)
        {
            return nodo.Term.Name.Equals(nombre, System.StringComparison.InvariantCultureIgnoreCase);
        }

        /**
         * @fn  static string getLexema(ParseTreeNode nodo, int num)
         *
         * @brief   Obtiene el lexema de un token, que es un nodo hoja del arbol ParseTree de Irony
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   nodo    El nodo padre.
         * @param   num     El indice que corresponde al hijo del que se desea el lexema.
         *
         * @return  The lexema.
         */

        static string getLexema(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Text;
        }
    }
}
