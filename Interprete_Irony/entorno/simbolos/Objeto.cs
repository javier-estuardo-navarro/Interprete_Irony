/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

namespace Interprete_Irony.entorno.simbolos
{
    /**
     * @class   Objeto
     *
     * @brief   Clase objeto representara una estructura hecha a partir de los atributos 
     *          de su Struct generador.
     * 
     * Solo cuenta con un entorno con los atributos inicializados.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Objeto : Simbolo
    {
        private string idStructGenerador;
        private Entorno atributos;

        public Objeto(string claseGeneradora, Entorno atributos):base(Tipos.OBJETO, claseGeneradora)
        {
            this.idStructGenerador = claseGeneradora;
            this.atributos = atributos;
        }

        /**
         * @return the claseGeneradora
         */
        public string getClaseGeneradora()
        {
            return idStructGenerador;
        }

        /**
         * @return the atributos
         */
        public Entorno getAtributos()
        {
            return atributos;
        }

    }
}
