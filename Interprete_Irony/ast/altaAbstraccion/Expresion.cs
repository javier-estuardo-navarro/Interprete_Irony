/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.entorno;
using static Interprete_Irony.entorno.Simbolo;

namespace Interprete_Irony.ast.altaAbstraccion
{
    /**
    * @interface   Expresion
    *
    * @brief   Esta interfaz se implementa en todas aquellas sentencias que pueden
    *          resultar en un valor.
    *          
    * Por ejemplo, una variable, la llamada a una funcion, una operacion aritmetica, 
    * una operacion logica, etc.
    *
    * @author  Javier Estuardo Navarro
    * @date    26/08/2018
    */

    public interface Expresion : NodoAST
    {

        /**
        * @fn  Tipo getTipo(Entorno ent, AST arbol);
        *
        * @brief   Método que permite conocer el tipo que el valor que se va retornar lleva implícito.
        *          Esto sera nuestro sistema de tipos.
        *
        * @param   ent     Un entorno actual de ejecucion el cual gestiona todo lo relacionado con 
        *                  la tabla de simbolos.
        * @param   arbol   Un Arbol Abstracto de Analisis Sintactico, el cual esta compuesto por NodosAST
        *                  los cuales pueden ser Expresiones o Instrucciones.
        *
        * @return  Devuelve el tipo implicito de la expresion, este sera de ayuda para realizar
        *          la comprobacion de tipos.  
        */
        Tipos getTipo(Entorno ent, AST arbol);


        /**
         * @fn  object getValorImplicito(Entorno ent, AST arbol);
         *
         * @brief   Metodo que devuelve el valor implicito de una expresion.
         *
        * @param   ent     Un entorno actual de ejecucion el cual gestiona todo lo relacionado con 
        *                  la tabla de simbolos.
        * @param   arbol   Un Arbol Abstracto de Analisis Sintactico, el cual esta compuesto por NodosAST
        *                  los cuales pueden ser Expresiones o Instrucciones.
        *
        * @return  Devuelve el valor implicito de la expresion.  
        */

        object getValorImplicito(Entorno ent, AST arbol);
    }
}
