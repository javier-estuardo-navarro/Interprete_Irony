/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.ast.altaAbstraccion;
using Interprete_Irony.entorno;
using Interprete_Irony.entorno.simbolos;
using System;
using static Interprete_Irony.entorno.Simbolo;

namespace Interprete_Irony.ast.valorImplicito
{
    /**
     * @class   AccesoObjeto
     *
     * @brief   Clase que representa el acceso a un objeto. 
     * 
     * En este caso como se trata de Structs y estos tienen atributos de tipo primitivo 
     * implica que unicamente tendran un nivel de anidacion: idObjeto.idAtributo.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class AccesoObjeto : Expresion
    {


        private string idObjeto;
        private string idAtributo;

        /**
         * @fn  public AccesoObjeto(string idObjeto, string idAtributo)
         *
         * @brief   Constructor de la clase AccesoObjeto
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   idObjeto    Identificador del objeto al que se quiere acceder.
         * @param   idAtributo  Identificador del atributo que se quiere obtener.
         */

        public AccesoObjeto(string idObjeto, string idAtributo)
        {
            this.idObjeto = idObjeto;
            this.idAtributo = idAtributo;
        }

        public Tipos getTipo(Entorno ent, AST arbol)
        {
            Simbolo simbolo = ent.get(idObjeto);
            if (simbolo.Tipo == Tipos.OBJETO)
            {
                //Se verifica que sea objeto, seguido de esto se devuelve el valor de dicho atributo.
                return ((Objeto)simbolo).getAtributos().get(idAtributo).Tipo;
            }
            else
            {
                Program.getGUI().appendSalida("No es un objeto");
                return Tipos.NULL;
            }
        }

        public object getValorImplicito(Entorno ent, AST arbol)
        {
            Simbolo simbolo = ent.get(idObjeto);
            if (simbolo.Tipo == Tipos.OBJETO)
            {
                //Se verifica que sea objeto, seguido de esto se devuelve el valor de dicho atributo.
                return ((Objeto)simbolo).getAtributos().get(idAtributo).Valor;
            }
            else
            {
                Program.getGUI().appendSalida("No es un objeto");
            }
            return null;
        }
        
        public string getC3D()
        {
            throw new NotImplementedException();
        }
    }
}
