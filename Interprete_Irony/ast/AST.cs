/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

using Interprete_Irony.ast.altaAbstraccion;
using System.Collections.Generic;

namespace Interprete_Irony.ast
{
    /**
     * @class   AST
     *
     * @brief   Clase que representa nuestro Arbol Abstracto de Analisis Sintactico.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class AST
    {
        /*
         * 
         */
        private  LinkedList<Struct> structs;
        private  LinkedList<Instruccion> instrucciones;

        /**
         * @fn  public AST(LinkedList<Instruccion> instrucciones)
         *
         * @brief   Constructor de la clase AST
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   instrucciones   Lista que almacena el conjunto de clases 
         *                          definidas dentro del archivo que representa 
         *                          este árbol. En este caso son solo Structs, 
         *                          pero se puedeescalar a el manejo de clases.
         */

        public AST(LinkedList<Instruccion> instrucciones)
        {
            Structs = new LinkedList<Struct>();
            this.Instrucciones = instrucciones;
        }

        /**
         * @fn  public void agregarStruct(Struct s)
         *
         * @brief   Agrega Structs al arbol, para poder declarar posteriormente ocurrencias de la misma.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   s   El Struct
         */

        public void agregarStruct(Struct s)
        {
            Structs.AddLast(s);
        }


        public LinkedList<Struct> Structs
        {
            get
            {
                return structs;
            }

            set
            {
                structs = value;
            }
        }

        public LinkedList<Instruccion> Instrucciones
        {
            get
            {
                return instrucciones;
            }

            set
            {
                instrucciones = value;
            }
        }

    }
}
