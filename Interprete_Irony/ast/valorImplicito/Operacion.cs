/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/


using System;
using Interprete_Irony.ast.altaAbstraccion;
using Interprete_Irony.entorno;
using static Interprete_Irony.entorno.Simbolo;

namespace Interprete_Irony.ast.valorImplicito
{
    /**
     * @class   Operacion
     *
     * @brief   Clase que representa una operacion Logica, Relacional o Aritmetica.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Operacion : Expresion
    {
        /**
         * @enum    Operador
         *
         * @brief   Enumeración de todas las posibles operaciones lógicas y aritméticas.
         */
        public enum Operador
        {
            SUMA,
            RESTA,
            MULTIPLICACION,
            DIVISION,
            POTENCIA,
            MENOS_UNARIO,
            MAYOR_QUE,
            MENOR_QUE,
            IGUAL_IGUAL,
            DIFERENTE_QUE,
            OR,
            AND,
            NOT,
            DESCONOCIDO
        }

        /**
         * @fn  public static Operador getOperador(string op)
         *
         * @brief   Se obtiene en base a una cadena un tipo de operador.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   Una cadena que representa un operador.
         *
         * @return  Un tipo de operador.
         */

        public static Operador getOperador(string op)
        {
            switch (op)
            {
                case "+":
                    return Operador.SUMA;
                case "-":
                    return Operador.RESTA;
                case "*":
                    return Operador.MULTIPLICACION;
                case "/":
                    return Operador.DIVISION;
                case "^":
                    return Operador.POTENCIA;
                case ">":
                    return Operador.MAYOR_QUE;
                case "<":
                    return Operador.MENOR_QUE;
                case "==":
                    return Operador.IGUAL_IGUAL;
                case "!=":
                    return Operador.DIFERENTE_QUE;
                case "||":
                    return Operador.OR;
                case "&&":
                    return Operador.AND;
                default:
                    return Operador.DESCONOCIDO;
            }
        }

        private Expresion operando1;

        private Expresion operando2;

        private Expresion operandoU;

        private Operador operador;

        /**
         * @fn  public Operacion(Expresion operando1, Expresion operando2, Operador operador)
         *
         * @brief   Constructor para operaciones binarias.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   operando1   Operador 1 en las operaciones que esta clase gestiona.
         * @param   operando2   Operando 2 en las operaciones que esta clase gestiona.
         * @param   operador    Tipo de operación que se esta realizando.
         */

        public Operacion(Expresion operando1, Expresion operando2, Operador operador)
        {
            this.operando1 = operando1;
            this.operando2 = operando2;
            this.operador = operador;
        }

        /**
         * @fn  public Operacion(Expresion operandoU, Operador operador)
         *
         * @brief   Constuctor para operaciones unarias.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   operandoU   Operando unario en las operaciones que esta clase gestiona
         * @param   operador    Operador unario.
         */

        public Operacion(Expresion operandoU, Operador operador)
        {
            this.operandoU = operandoU;
            this.operador = operador;
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
            else
            {
                return Tipos.NULL;
            }
        }


        public object getValorImplicito(Entorno ent, AST arbol)
        {
            object op1 = new object(), op2 = new object(), opU = new object();
            if (operandoU == null)
            {
                op1 = operando1.getValorImplicito(ent, arbol);
                op2 = operando2.getValorImplicito(ent, arbol);
            }
            else
            {
                opU = operandoU.getValorImplicito(ent, arbol);
            }
            switch (operador)
            {
                case Operador.SUMA:
                    //Tipo resultante de datos: Decimal
                    if (op1 is int && op2 is double)
                    {
                        return (int)op1 + (double)op2;
                    }
                    else if (op1 is double && op2 is int)
                    {
                        return (double)op1 + (int)op2;
                    }
                    else if (op1 is double && op2 is char)
                    {
                        return (double)op2 + (int)((char)(op2));
                    }
                    else if (op1 is char && op2 is double)
                    {
                        return (int)((char)(op1)) + (double)op2;
                    }
                    else if (op1 is bool && op2 is double)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 + (double)op2;
                    }
                    else if (op1 is double && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (double)op1 + o2;
                    }
                    else if (op1 is double && op2 is double)
                    {
                        return (double)op1 + (double)op2;
                    } //Tipo resultante de datos: Entero
                    else if (op1 is int && op2 is char)
                    {
                        return (int)op1 + (int)((char)op2);
                    }
                    else if (op1 is char && op2 is int)
                    {
                        return (int)((char)op1) + (int)op2;
                    }
                    else if (op1 is bool && op2 is int)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 + (int)op2;
                    }
                    else if (op1 is int && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (int)op1 + o2;
                    }
                    else if (op1 is int && op2 is int)
                    {
                        return (int)op1 + (int)op2;
                    } //Tipo resultante de datos: Cadena
                    else if (op1 is string || op2 is string)
                    {
                        return op1.ToString() + op2.ToString();
                    } //Tipo resultante de datos: Bool
                    else if (op1 is bool && op2 is bool)
                    {
                        return (bool)op1 || (bool)op2;
                    }
                    else
                    {
                        Program.getGUI().appendSalida("Error de tipos en la suma");
                    }
                    break;
                case Operador.RESTA:
                    //Tipo resultante de datos: Decimal
                    if (op1 is int && op2 is double)
                    {
                        return (int)op1 - (double)op2;
                    }
                    else if (op1 is double && op2 is int)
                    {
                        return (double)op1 - (int)op2;
                    }
                    else if (op1 is double && op2 is char)
                    {
                        return (double)op2 - (int)((char)(op2));
                    }
                    else if (op1 is char && op2 is double)
                    {
                        return (int)((char)(op1)) - (double)op2;
                    }
                    else if (op1 is bool && op2 is double)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 - (double)op2;
                    }
                    else if (op1 is double && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (double)op1 - o2;
                    }
                    else if (op1 is double && op2 is double)
                    {
                        return (double)op1 - (double)op2;
                    } //Tipo resultante de datos: Entero
                    else if (op1 is int && op2 is char)
                    {
                        return (int)op1 - (int)((char)op2);
                    }
                    else if (op1 is char && op2 is int)
                    {
                        return (int)((char)op1) - (int)op2;
                    }
                    else if (op1 is bool && op2 is int)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 - (int)op2;
                    }
                    else if (op1 is int && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (int)op1 - o2;
                    }
                    else if (op1 is int && op2 is int)
                    {
                        return (int)op1 - (int)op2;
                    } //Tipo resultante de datos: Cadena
                    else if (op1 is string || op2 is string)
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo el operador"
                                + " menos para dos cadenas");
                        return null;
                    } //Tipo resultante de datos: Bool
                    else if (op1 is bool && op2 is bool)
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo el operador"
                                + " menos para dos boolos");
                        return null;
                    }
                    break;
                case Operador.MULTIPLICACION:
                    //Tipo resultante de datos: Decimal
                    if (op1 is int && op2 is double)
                    {
                        return (int)op1 * (double)op2;
                    }
                    else if (op1 is double && op2 is int)
                    {
                        return (double)op1 * (int)op2;
                    }
                    else if (op1 is double && op2 is char)
                    {
                        return (double)op2 * (int)((char)(op2));
                    }
                    else if (op1 is char && op2 is double)
                    {
                        return (int)((char)(op1)) * (double)op2;
                    }
                    else if (op1 is bool && op2 is double)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 * (double)op2;
                    }
                    else if (op1 is double && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (double)op1 * o2;
                    }
                    else if (op1 is double && op2 is double)
                    {
                        return (double)op1 * (double)op2;
                    } //Tipo resultante de datos: Entero
                    else if (op1 is int && op2 is char)
                    {
                        return (int)op1 * (int)((char)op2);
                    }
                    else if (op1 is char && op2 is int)
                    {
                        return (int)((char)op1) * (int)op2;
                    }
                    else if (op1 is bool && op2 is int)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 * (int)op2;
                    }
                    else if (op1 is int && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (int)op1 * o2;
                    }
                    else if (op1 is int && op2 is int)
                    {
                        return (int)op1 * (int)op2;
                    } //Tipo resultante de datos: Cadena
                    else if (op1 is string || op2 is string)
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo el operador"
                                + " por para dos cadenas");
                        return null;
                    } //Tipo resultante de datos: Bool
                    else if (op1 is bool && op2 is bool)
                    {
                        return (bool)op1 && (bool)op2;
                    }
                    break;
                case Operador.DIVISION:
                    //Tipo resultante de datos: Decimal
                    if (op1 is int && op2 is double)
                    {
                        if ((double)op2 != 0.0)
                        {
                            return (int)op1 / (double)op2;
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Excepcion aritmetica: division(/) por cero");
                            return null;
                        }
                    }
                    else if (op1 is double && op2 is int)
                    {
                        if ((int)op2 != 0)
                        {
                            return (double)op1 / (int)op2;
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Excepcion aritmetica: division(/) por cero");
                            return null;
                        }
                    }
                    else if (op1 is double && op2 is char)
                    {
                        if ((int)((char)(op2)) != 0)
                        {
                            return (double)op2 / (int)((char)(op2));
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Excepcion aritmetica: division(/) por cero");
                            return null;
                        }
                    }
                    else if (op1 is char && op2 is double)
                    {
                        if ((double)op2 != 0.0)
                        {
                            return (int)((char)(op1)) / (double)op2;
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Excepcion aritmetica: division(/) por cero");
                            return null;
                        }
                    }
                    else if (op1 is bool && op2 is double)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        if ((double)op2 != 0.0)
                        {
                            return o1 / (double)op2;
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Excepcion aritmetica: division(/) por cero");
                            return null;
                        }
                    }
                    else if (op1 is double && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        if (o2 != 0)
                        {
                            return (double)op1 / o2;
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Excepcion aritmetica: division(/) por cero");
                            return null;
                        }
                    }
                    else if (op1 is double && op2 is double)
                    {
                        if ((double)op2 != 0.0)
                        {
                            return (double)op1 / (double)op2;
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Excepcion aritmetica: division(/) por cero");
                            return null;
                        }
                    } //Tipo resultante de datos: Entero
                    else if (op1 is int && op2 is char)
                    {
                        if ((int)((char)(op2)) != 0)
                        {
                            return (int)op1 / (int)((char)op2);
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Excepcion aritmetica: division(/) por cero");
                            return null;
                        }
                    }
                    else if (op1 is char && op2 is int)
                    {
                        if ((int)op2 != 0)
                        {
                            return (int)((char)op1) / (int)op2;
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Excepcion aritmetica: division(/) por cero");
                            return null;
                        }
                    }
                    else if (op1 is bool && op2 is int)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        if ((int)op2 != 0)
                        {
                            return o1 / (int)op2;
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Excepcion aritmetica: division(/) por cero");
                            return null;
                        }
                    }
                    else if (op1 is int && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        if (o2 != 0)
                        {
                            return (int)op1 / o2;
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Excepcion aritmetica: division(/) por cero");
                            return null;
                        }
                    }
                    else if (op1 is int && op2 is int)
                    {
                        if ((int)op2 != 0)
                        {
                            return (int)op1 / (int)op2;
                        }
                        else
                        {
                            Program.getGUI().appendSalida("Excepcion aritmetica: division(/) por cero");
                            return null;
                        }
                    } //Tipo resultante de datos: Cadena
                    else if (op1 is string || op2 is string)
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo el operador"
                                + " division para dos cadenas");
                        return null;
                    } //Tipo resultante de datos: Bool
                    else if (op1 is bool && op2 is bool)
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo el operador"
                                + " division para dos boolos");
                        return null;
                    }
                    break;
                case Operador.POTENCIA:
                    //Tipo resultante de datos: Decimal
                    if (op1 is int && op2 is double)
                    {
                        return Math.Pow((int)op1, (double)op2);
                    }
                    else if (op1 is double && op2 is int)
                    {
                        return Math.Pow((double)op1, (int)op2);
                    }
                    else if (op1 is double && op2 is char)
                    {
                        return Math.Pow((double)op2, (int)((char)(op2)));
                    }
                    else if (op1 is char && op2 is double)
                    {
                        return Math.Pow((int)((char)(op1)), (double)op2);
                    }
                    else if (op1 is bool && op2 is double)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return Math.Pow(o1, (double)op2);
                    }
                    else if (op1 is double && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return Math.Pow((double)op1, o2);
                    }
                    else if (op1 is double && op2 is double)
                    {
                        return Math.Pow((double)op1, (double)op2);
                    } //Tipo resultante de datos: Entero
                    else if (op1 is int && op2 is char)
                    {
                        return Math.Pow((int)op1, (int)((char)op2));
                    }
                    else if (op1 is char && op2 is int)
                    {
                        return Math.Pow((int)((char)op1), (int)op2);
                    }
                    else if (op1 is bool && op2 is int)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return Math.Pow(o1, (int)op2);
                    }
                    else if (op1 is int && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return Math.Pow((int)op1, o2);
                    }
                    else if (op1 is int && op2 is int)
                    {
                        return Math.Pow((int)op1, (int)op2);
                    } //Tipo resultante de datos: Cadena
                    else if (op1 is string || op2 is string)
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo el operador"
                                + " potencia para dos cadenas");
                        return null;
                    } //Tipo resultante de datos: Bool
                    else if (op1 is bool && op2 is bool)
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo el operador"
                                + " potencia para dos boolos");
                        return null;
                    }
                    break;
                case Operador.MENOS_UNARIO:
                    if (opU is double)
                    {
                        return 0.0 - (double)opU;
                    }
                    else if (opU is int)
                    {
                        return 0.0 - (int)opU;
                    }
                    else if (opU is char)
                    {
                        return 0 - (int)((char)opU);
                    }
                    else
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo el operador"
                                + " menos unario incorrectamente");
                        return null;
                    }
                case Operador.MAYOR_QUE:
                    //Tipo resultante de datos: Decimal
                    if (op1 is int && op2 is double)
                    {
                        return (int)op1 > (double)op2;
                    }
                    else if (op1 is double && op2 is int)
                    {
                        return (double)op1 > (int)op2;
                    }
                    else if (op1 is double && op2 is char)
                    {
                        return (double)op2 > (int)((char)(op2));
                    }
                    else if (op1 is char && op2 is double)
                    {
                        return (int)((char)(op1)) > (double)op2;
                    }
                    else if (op1 is bool && op2 is double)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 > (double)op2;
                    }
                    else if (op1 is double && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (double)op1 > o2;
                    }
                    else if (op1 is double && op2 is double)
                    {
                        return (double)op1 > (double)op2;
                    } //Tipo resultante de datos: Entero
                    else if (op1 is int && op2 is char)
                    {
                        return (int)op1 > (int)((char)op2);
                    }
                    else if (op1 is char && op2 is int)
                    {
                        return (int)((char)op1) > (int)op2;
                    }
                    else if (op1 is bool && op2 is int)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 > (int)op2;
                    }
                    else if (op1 is int && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (int)op1 > o2;
                    }
                    else if (op1 is int && op2 is int)
                    {
                        return (int)op1 > (int)op2;
                    } //Tipo resultante de datos: Cadena
                    else if (op1 is string || op2 is string)
                    {
                        return op1.ToString().Length > op2.ToString().Length;
                    } //Tipo resultante de datos: Bool
                    else if (op1 is bool && op2 is bool)
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo el operador"
                                + " mayor que para dos boolos");
                        return null;
                    }
                    break;
                case Operador.MENOR_QUE:
                    //Tipo resultante de datos: Decimal
                    if (op1 is int && op2 is double)
                    {
                        return (int)op1 < (double)op2;
                    }
                    else if (op1 is double && op2 is int)
                    {
                        return (double)op1 < (int)op2;
                    }
                    else if (op1 is double && op2 is char)
                    {
                        return (double)op2 < (int)((char)(op2));
                    }
                    else if (op1 is char && op2 is double)
                    {
                        return (int)((char)(op1)) < (double)op2;
                    }
                    else if (op1 is bool && op2 is double)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 < (double)op2;
                    }
                    else if (op1 is double && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (double)op1 < o2;
                    }
                    else if (op1 is double && op2 is double)
                    {
                        return (double)op1 < (double)op2;
                    } //Tipo resultante de datos: Entero
                    else if (op1 is int && op2 is char)
                    {
                        return (int)op1 < (int)((char)op2);
                    }
                    else if (op1 is char && op2 is int)
                    {
                        return (int)((char)op1) < (int)op2;
                    }
                    else if (op1 is bool && op2 is int)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 < (int)op2;
                    }
                    else if (op1 is int && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (int)op1 < o2;
                    }
                    else if (op1 is int && op2 is int)
                    {
                        return (int)op1 < (int)op2;
                    } //Tipo resultante de datos: Cadena
                    else if (op1 is string || op2 is string)
                    {
                        return op1.ToString().Length < op2.ToString().Length;
                    } //Tipo resultante de datos: Bool
                    else if (op1 is bool && op2 is bool)
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo el operador"
                                + " menor que para dos boolos");
                        return null;
                    }
                    break;
                case Operador.IGUAL_IGUAL:
                    //Tipo resultante de datos: Decimal
                    if (op1 is int && op2 is double)
                    {
                        return (int)op1 == (double)op2;
                    }
                    else if (op1 is double && op2 is int)
                    {
                        return (double)op1 == (int)op2;
                    }
                    else if (op1 is double && op2 is char)
                    {
                        return (double)op2 == (int)((char)(op2));
                    }
                    else if (op1 is char && op2 is double)
                    {
                        return (int)((char)(op1)) == (double)op2;
                    }
                    else if (op1 is bool && op2 is double)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 == (double)op2;
                    }
                    else if (op1 is double && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (double)op1 == o2;
                    }
                    else if (op1 is double && op2 is double)
                    {
                        return (double)op1 == (double)op2;
                    } //Tipo resultante de datos: Entero
                    else if (op1 is int && op2 is char)
                    {
                        return (int)op1 == (int)((char)op2);
                    }
                    else if (op1 is char && op2 is int)
                    {
                        return (int)((char)op1) == (int)op2;
                    }
                    else if (op1 is bool && op2 is int)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 == (int)op2;
                    }
                    else if (op1 is int && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (int)op1 == o2;
                    }
                    else if (op1 is int && op2 is int)
                    {
                        return (int)op1 == (int)op2;
                    } //Tipo resultante de datos: Cadena
                    else if (op1 is string || op2 is string)
                    {
                        return op1.ToString().Equals(op2.ToString());
                    } //Tipo resultante de datos: Bool
                    else if (op1 is bool && op2 is bool)
                    {
                        return (bool)op1 == (bool)op2;
                    }
                    break;
                case Operador.DIFERENTE_QUE:
                    //Tipo resultante de datos: Decimal
                    if (op1 is int && op2 is double)
                    {
                        return (int)op1 != (double)op2;
                    }
                    else if (op1 is double && op2 is int)
                    {
                        return (double)op1 != (int)op2;
                    }
                    else if (op1 is double && op2 is char)
                    {
                        return (double)op2 != (int)((char)(op2));
                    }
                    else if (op1 is char && op2 is double)
                    {
                        return (int)((char)(op1)) != (double)op2;
                    }
                    else if (op1 is bool && op2 is double)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 == (double)op2;
                    }
                    else if (op1 is double && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (double)op1 != o2;
                    }
                    else if (op1 is double && op2 is double)
                    {
                        return (double)op1 != (double)op2;
                    } //Tipo resultante de datos: Entero
                    else if (op1 is int && op2 is char)
                    {
                        return (int)op1 != (int)((char)op2);
                    }
                    else if (op1 is char && op2 is int)
                    {
                        return (int)((char)op1) != (int)op2;
                    }
                    else if (op1 is bool && op2 is int)
                    {
                        int o1 = (bool)op1 ? 1 : 0;
                        return o1 != (int)op2;
                    }
                    else if (op1 is int && op2 is bool)
                    {
                        int o2 = (bool)op1 ? 1 : 0;
                        return (int)op1 != o2;
                    }
                    else if (op1 is int && op2 is int)
                    {
                        return (int)op1 != (int)op2;
                    } //Tipo resultante de datos: Cadena
                    else if (op1 is string || op2 is string)
                    {
                        return !op1.ToString().Equals(op2.ToString());
                    } //Tipo resultante de datos: Bool
                    else if (op1 is bool && op2 is bool)
                    {
                        return (bool)op1 != (bool)op2;
                    }
                    break;
                case Operador.OR:
                    if (op1 is bool && op2 is bool)
                    {
                        return (bool)op1 || (bool)op2;
                    }
                    else
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo un operador"
                                + " or, ambos operandos deben ser de tipo boolo");
                        return null;
                    }
                case Operador.AND:
                    if (op1 is bool && op2 is bool)
                    {
                        return (bool)op1 && (bool)op2;
                    }
                    else
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo un operador"
                                + " and, ambos operandos deben ser de tipo boolo");
                        return null;
                    }
                case Operador.NOT:
                    if (opU is bool)
                    {
                        return !(bool)op1;
                    }
                    else
                    {
                        Program.getGUI().appendSalida("Error de tipos, se utilizo un operador"
                                + " not, el operando debe ser de tipo boolo");
                        return null;
                    }
                default:
                    break;
            }
            return null;
        }

        public string getC3D()
        {
            throw new NotImplementedException();
        }
    }
}
