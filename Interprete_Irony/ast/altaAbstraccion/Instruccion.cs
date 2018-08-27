/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.entorno;
using System;

namespace Interprete_Irony.ast.altaAbstraccion
{
    /**
    * @interface   Instruccion
    *
    * @brief   Esta interfaz se implementa en todas aquellas sentencias que ejecutan acciones
    *          pero no devuelven ningun valor. 
    *          
    * Las sentencias que son Instrucciones son los ciclos como el ciclo While.
    * @author  Javier Estuardo Navarro
    * @date    26/08/2018
    */

    public interface Instruccion: NodoAST
    {
        /**
        * @fn  object ejecutar(Entorno ent, AST arbol);
        *
        * @brief   El nivel de abstraccion del Arbol Abstracto de Analisis Sintactico requiere de 
        *          una forma de recorrerlo, ya que todo forma parte de un NodoAST se define el metodo 
        *          ejecutar() en todos aquellos nodos que sean Instrucciones, al hacer llamadas 
        *          al mismo se recorre de una forma abstracta nuestro arbol.
        *
        * @param   ent     Un entorno actual de ejecucion el cual gestiona todo lo relacionado con 
        *                  la tabla de simbolos.
        * @param   arbol   Un Arbol Abstracto de Analisis Sintactico, el cual esta compuesto por NodosAST
        *                  los cuales pueden ser Expresiones o Instrucciones.
        *
        * @return  Devuelve objetos resultado de sintetizar valores implicitos de una Expresion
        *          por ejemplo: En una Instruccion While puede haber una Expresion Return
        *          que retorna un valor.
        */

        object ejecutar(Entorno ent, AST arbol);
    }
}
