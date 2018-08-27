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

namespace Interprete_Irony.ast.cambioFlujo
{
    /**
     * @class   Return
     *
     * @brief   Clase que representa a la sentencia de cambio de flujo Return.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Return : Expresion
    {
        private bool retornoVoid;
        private Expresion valorDeRetorno;

        /**
         * @fn  public Return(Expresion valorDeRetorno)
         *
         * @brief   Constructor utilizado en sentencias Return Expresion; es decir en funciones.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   valorDeRetorno The valorDeRetorno to return.
         */

        public Return(Expresion valorDeRetorno)
        {
            this.valorDeRetorno = valorDeRetorno;
            retornoVoid = false;
        }

        /**
         * @fn  public Return()
         *
         * @brief   Constructor utilizado en sentencias Return; es decir en metodos (void).
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         */

        public Return()
        {
            retornoVoid = true;
        }

        /**
         * @fn  public bool isRetornoVoid()
         *
         * @brief   Consulta si el retorno es void.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @return  True si devuelve void, false si no.
         */

        public bool isRetornoVoid()
        {
            return retornoVoid;
        }

        public Tipos getTipo(Entorno ent, AST arbol)
        {
            return valorDeRetorno.getTipo(ent, arbol);
        }


        public object getValorImplicito(Entorno ent, AST arbol)
        {
            return valorDeRetorno.getValorImplicito(ent, arbol);
        }

        public string getC3D()
        {
            throw new NotImplementedException();
        }

    }
}
