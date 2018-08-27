/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using System;
using Interprete_Irony.ast.altaAbstraccion;
using Interprete_Irony.entorno;

namespace Interprete_Irony.ast.cambioFlujo
{
    /**
     * @class   Break
     *
     * @brief   Clase que representa a la sentencia de cambio de flujo Break. 
     * 
     * No tiene acciones especificas ya que solo debemos saber su ubicacion y en las 
     * sentencias de control de flujo, realizar las acciones necesarias.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Break : Instruccion
    {
        public object ejecutar(Entorno ent, AST arbol)
        {
            return null;
        }

        public string getC3D()
        {
            throw new NotImplementedException();
        }
    }
}
