/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
*/

namespace Interprete_Irony.ast.altaAbstraccion
{
    /**
     * @interface   NodoAST
     *
     * @brief   La interfaz NodoAST define el elemento basico que conformara nuestro Arbol Abstracto
     *          de Analisis Sintactico.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public interface NodoAST
    {
        /**
         * @fn  string getC3D();
         *
         * @brief   Este metodo tan peculiar, no servirá hasta cuando nuestro interprete genere codigo intermedio.
         *
         * @return  Devuelve el codigo intermedio que representa la entrada.
         */
        string getC3D();
    }
}
