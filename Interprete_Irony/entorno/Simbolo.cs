/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
 */


using System.Collections.Generic;

namespace Interprete_Irony.entorno
{
    /**
     * @class   Simbolo
     *
     * @brief   Clase símbolo, que es un nodo de la tabla de símbolos. 
     * 
     * Estos símbolos son variables con su valor, identificador y tipo.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Simbolo
    {
        /**
         * @enum    Tipo
         *
         * @brief   Esta enumeración contiene todos los tipos posibles para una variable.
         */
        public enum Tipos
        {
            STRING,
            INT,
            DOUBLE,
            CHAR,
            BOOL,
            NULL,
            OBJETO
        }

        
        private string identificador;

        private object valor;

        private Tipos tipo;
        
        private LinkedList<Simbolo> listaParametros;
        
        private  bool funcion;


        /**
         * @fn  public Simbolo(string identificador)
         *
         * @brief   Constructor que se utiliza para declaracion de variables u objetos.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   identificador   Identificador del Simbolo.
         */

        public Simbolo(string Identificador)
        {
            this.Identificador = Identificador;
            this.Funcion = false;
        }


        /**
         * @fn  public Simbolo(Tipos tipo, string identificador)
         *
         * @brief   Constructor que se utiliza principalmente para declaracion 
         *          de parámetros de los métodos.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   tipo            Tipo específico de la variable, Entero, Decimal, 
         *                          Cadena, OBJETO, etc. del Simbolo.
         * @param   identificador   Identificador del Simbolo.
         */

        public Simbolo(Tipos Tipo, string Identificador)
        {
            this.Tipo = Tipo;
            this.Identificador = Identificador;
            Funcion = false;
        }

        /**
         * @fn  public Simbolo(Tipos tipo, string identificador, object valor)
         *
         * @brief   Constructor que se utiliza pripalmente para inicializacion de 
         *          parámetros en las llamadas a metodos.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   tipo            Tipo específico de la variable, Entero, Decimal, 
         *                          Cadena, OBJETO, etc. del Simbolo.
         * @param   identificador   Identificador del Simbolo.
         * @param   valor           El valor específico de la variable, ya sea que se
         *                          trate de un valor primitivo o de un objeto.
         */

        public Simbolo(Tipos Tipo, string Identificador, object Valor)
        {
            this.Tipo = Tipo;
            this.Identificador = Identificador;
            this.Valor = Valor;
            Funcion = false;
        }

        /**
         * @fn  public Simbolo(string Identificador, Tipos Tipo, LinkedList<Simbolo> ListaParametros)
         *
         * @brief   Constructor que se utiliza para definición de funciones.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   Identificador   Identificador del Simbolo.
         * @param   Tipo            Tipo específico de la variable, Entero, Decimal, 
         *                          Cadena, OBJETO, etc. del Simbolo.
         * @param   ListaParametros La lista parametros de la funcion.
         */

        public Simbolo(string Identificador, Tipos Tipo, LinkedList<Simbolo> ListaParametros)
        {
            this.Identificador = Identificador;
            this.Tipo = Tipo;
            this.ListaParametros = ListaParametros;
            Funcion = true;
        }

        /**
         * @property    public string Identificador
         *
         * @brief   Obtiene o setea el identificador
         *
         * @return  El identificador.
         */

        public string Identificador
        {
            get
            {
                return identificador;
            }

            set
            {
                identificador = value;
            }
        }

        /**
         * @property    public object Valor
         *
         * @brief   Obtiene o setea el valor
         *
         * @return  El valor.
         */

        public object Valor
        {
            get
            {
                return valor;
            }

            set
            {
                valor = value;
            }
        }

        /**
         * @property    public Tipos Tipo
         *
         * @brief   Obtiene o setea el tipo
         *
         * @return  El tipo.
         */

        public Tipos Tipo
        {
            get
            {
                return tipo;
            }

            set
            {
                tipo = value;
            }
        }

        /**
         * @property    public LinkedList<Simbolo> ListaParametros
         *
         * @brief   Obtiene o setea la lista de parametros
         *
         * @return  La lista parametros.
         */

        public LinkedList<Simbolo> ListaParametros
        {
            get
            {
                return listaParametros;
            }

            set
            {
                listaParametros = value;
            }
        }

        /**
         * @property    public bool Funcion
         *
         * @brief   Obtiene o establece un valor que indica si es función
         *
         * @return  Verdadero si es funcion, falso si no.
         */

        public bool Funcion
        {
            get
            {
                return funcion;
            }

            set
            {
                funcion = value;
            }
        }
    }
}
