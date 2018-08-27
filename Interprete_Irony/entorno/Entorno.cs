/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
 */


using System;
using System.Collections;

namespace Interprete_Irony.entorno
{
    /**
     * @class   Entorno
     *
     * @brief   Clase Entorno, se encarga de gestionar todo lo referente a 
     *          la(s) tabla(s) de simbolos durante la ejecucion del programa.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Entorno
    {

        private Hashtable tabla;

        private Entorno anterior;

        

        /**
         * @fn  public Entorno(Entorno anterior)
         *
         * @brief   Constructor de la clase Entorno, se construye un nuevo entorno 
         *          en base a un entorno anterior.
         *
         * La clase Entorno posee una tabla hash que almacenara los simbolos, es decir 
         * la tabla de simbolos actual.
         * 
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   anterior    Sera el entorno "padre".
         */
        public Entorno(Entorno anterior)
        {
            Tabla = new Hashtable();
            this.Anterior = anterior;
        }

        /**
         * @fn  public void agregar(string id, Simbolo simbolo)
         *
         * @brief   Metodo agregar cuyo objetivo es insertar un nuevo registro a la 
         *          tabla de simbolos actual.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   id      Es el nombre propio del simbolo, efectua el rol de la Clave.
         * @param   simbolo Es el simbolo como tal con sus atributos, efectua el rol de Valor.
         */
        public void agregar(string id, Simbolo simbolo)
        {
            Tabla.Add(id, simbolo);
        }

        /**
         * @fn  public bool existe(string id)
         *
         * @brief   Metodo Existe cuyo objetivo es insertar un nuevo registro a la 
         *          tabla de simbolos actual.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   id  Es el nombre propio del simbolo, efectua el rol de la Clave.
         *
         * @return  true si existe,de lo contrario false
         */

        public bool existe(string id)
        {
            for (Entorno e = this; e != null; e = e.Anterior)
            {
                if (e.Tabla.Contains(id))
                {
                    return true;
                }
            }
            return false;
        }

        /**
         * @fn  public bool existeEnActual(string id)
         *
         * @brief   Existe en actual
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   id  Es el nombre propio del simbolo, efectua el rol de la Clave.
         *
         * @return  true si existe,de lo contrario false
         */

        public bool existeEnActual(string id)
        {
            Simbolo encontrado = (Simbolo)(Tabla[id]);
            return encontrado != null;
        }

        /**
         * @fn  public Simbolo get(string id)
         *
         * @brief   Metodo get, cuyo objetivo es obtener un simbolo en base a una clave (id)
         *          La logica de los entornos es basada en la regla del bloque anidado más
         *          cercano nos indica que un identificador x se encuentra en el alcance de
         *          la declaración anidada más cercana de x; es decir, la declaración de x
         *          que se encuentra al examinar los bloques desde adentro hacia fuera,
         *          empezando con el bloque en el que aparece x.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   id  Es la clave necesaria para acceder al valor (Simbolo) en la
         *              tabla actual
         *
         * @return  Se retorna el simbolo declarado al alcance, es decir en la parte
         *          mas cercana a la ocurrencia del mismo.
         */

        public Simbolo get(string id)
        {
            for (Entorno e = this; e != null; e = e.Anterior)
            {
                Simbolo encontrado = (Simbolo)(e.Tabla[id]);
                if (encontrado != null)
                {
                    return encontrado;
                }
            }
            Console.WriteLine("El simbolo \"" + id + "\" no ha sido declarado en el entorno actual ni en alguno externo");
            return null;
        }


        /**
         * @fn  public void reemplazar(string id, Simbolo nuevoValor)
         *
         * @brief   Metodo que reemplaza un simbolo.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   id          Es la clave.
         * @param   nuevoValor  Es el nuevo valor que sera seteado al Simbolo con identificador=id.
         */

        public void reemplazar(string id, Simbolo nuevoValor)
        {
            for (Entorno e = this; e != null; e = e.Anterior)
            {
                Simbolo encontrado = (Simbolo)(e.Tabla[id]);
                if (encontrado != null)
                {
                    Tabla[id] = nuevoValor;
                }
            }
            Console.WriteLine("El simbolo \"" + id + "\" no ha sido declarado en el entorno actual ni en alguno externo");

        }

        /**
         * @property    public Hashtable Tabla
         *
         * @brief   Obtiene o setea la tabla de simbolos
         *
         * @return  La tabla.
         */

        public Hashtable Tabla
        {
            get
            {
                return tabla;
            }

            set
            {
                tabla = value;
            }
        }

        /**
         * @property    public Entorno Anterior
         *
         * @brief   Obtiene o setea el entorno anterior
         *
         * @return  El entorno anterior.
         */

        public Entorno Anterior
        {
            get
            {
                return anterior;
            }

            set
            {
                anterior = value;
            }
        }

    }
}
