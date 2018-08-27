/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
 */

using Irony.Parsing;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Interprete_Irony.interprete
{
    /**
     * @class   Graficador
     *
     * @brief   Clase que representa un graficador de un ParseTree de Irony.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public class Graficador
    {
        private int index;
        // Para graficar con Graphviz se debera colocar en esta variable la ruta del ejecutable dot.exe
        private readonly string rutaExeDot = "C:\\Program Files (x86)\\Graphviz\\bin\\dot.exe";

        /**
         * @fn  public void graficar(ParseTreeNode nodo)
         *
         * @brief   El metodo graficar recorre recursivamente el arbol para definir los nodos y luego 
         *          enlazarlos al final se concatena el contenido a la variable "contenido" 
         *          se escribe el archivo, se genera la imagen .png y se pregunta si se desea visualizar.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   nodo    El nodo raiz.
         */

        public void graficar(ParseTreeNode nodo)
        {
            StreamWriter archivo = new StreamWriter("ArbolSintactico.dot");
            string contenido = "graph G {";
            contenido += "node [shape = egg];";
            index = 0;
            definirNodos(nodo, ref contenido);
            index = 0;
            enlazarNodos(nodo, 0, ref contenido);
            contenido += "}";
            archivo.Write(contenido);
            archivo.Close();
            DialogResult verImagen = MessageBox.Show("¿Desea visualizar el AST de la cadena ingresada?", "Grafica AST", MessageBoxButtons.YesNo);
            if (verImagen == DialogResult.Yes)
            {
                
                ProcessStartInfo startInfo = new ProcessStartInfo(rutaExeDot);
                startInfo.Arguments = "-Tpng ArbolSintactico.dot -o ArbolSintactico.png";
                Process.Start(startInfo);
                Thread.Sleep(2000);
                startInfo.FileName = "ArbolSintactico.png";
                Process.Start(startInfo);
            }


        }

        /**
         * @fn  public void definirNodos(ParseTreeNode nodo, ref string contenido)
         *
         * @brief   Definir nodos
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param           nodo        El nodo.
         * @param [in,out]  contenido   El contenido del archivo dot.
         */

        public void definirNodos(ParseTreeNode nodo, ref string contenido)
        {
            if (nodo != null)
            {
                contenido += "node" + index.ToString() + "[label = \"" + nodo.ToString() + "\", style = filled, color = lightblue];";
                index++;

                foreach (ParseTreeNode hijo in nodo.ChildNodes)
                {
                    definirNodos(hijo, ref contenido);
                }
            }
        }

        /**
         * @fn  public void enlazarNodos(ParseTreeNode nodo, int actual, ref string contenido)
         *
         * @brief   Enlazar nodos
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param           nodo        El nodo.
         * @param           actual      El indice del nodo actual.
         * @param [in,out]  contenido   El contenido del archivo dot.
         */

        public void enlazarNodos(ParseTreeNode nodo, int actual, ref string contenido)
        {
            if (nodo != null)
            {
                foreach (ParseTreeNode hijo in nodo.ChildNodes)
                {
                    index++;
                    contenido += "\"node" + actual.ToString() + "\"--" + "\"node" + index.ToString() + "\"";
                    enlazarNodos(hijo, index, ref contenido);
                }
            }
        }


    }
}
