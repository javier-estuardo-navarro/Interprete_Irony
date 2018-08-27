/*
 * Interprete básico implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
 * 
*/

using Interprete_Irony.ast;
using Interprete_Irony.ast.altaAbstraccion;
using Interprete_Irony.entorno;
using Interprete_Irony.entorno.simbolos;
using Interprete_Irony.interprete;
using Irony.Parsing;
using System;
using System.IO;
using System.Windows.Forms;

namespace Interprete_Irony
{
    /**
     * @class   GUI
     *
     * @brief   Interfaz grafica de usuario.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        /**
         * @fn  public void appendSalida(string cadena)
         *
         * @brief   Concatena la cadana a la salida
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   cadena  La cadena a concatenar.
         */

        public void appendSalida(string cadena)
        {
            txtSalida.AppendText(cadena + "\n");
        }

        private void btnInterpretar_Click(object sender, EventArgs e)
        {
            if (!txtEntrada.Text.Equals(string.Empty))
            {
                Gramatica grammar = new Gramatica();
                LanguageData lenguaje = new LanguageData(grammar);
                Parser parser = new Parser(lenguaje);
                ParseTree arbol = parser.Parse(txtEntrada.Text);
                if (arbol.ParserMessages.Count == 0)
                {
                    Graficador j = new Graficador();
                    j.graficar(arbol.Root);
                    ConstructorAST an = new ConstructorAST();
                    AST auxArbol = an.Analizar(arbol.Root);
                    Entorno global = new Entorno(null);
                    try
                    {
                        if (auxArbol != null)
                        {
                            foreach (Instruccion ins in auxArbol.Instrucciones)
                            {
                                if (ins is Funcion)
                                {
                                    Funcion funcion = (Funcion)ins;
                                    global.agregar(funcion.Identificador, funcion);
                                    foreach (NodoAST instruccion in funcion.Instrucciones)
                                    {
                                        if (instruccion is DefinicionStruct)
                                        {
                                            DefinicionStruct crear = (DefinicionStruct)instruccion;
                                            crear.ejecutar(global, auxArbol);
                                        }
                                    }
                                }
                                if (ins is Declaracion)
                                {
                                    Declaracion declaracion = (Declaracion)ins;
                                    declaracion.ejecutar(global, auxArbol);
                                }
                                if (ins is DefinicionStruct)
                                {
                                    DefinicionStruct crear = (DefinicionStruct)ins;
                                    crear.ejecutar(global, auxArbol);
                                }

                            }
                            foreach (Instruccion ins in auxArbol.Instrucciones)
                            {
                                if (ins is Main)
                                {
                                    Main main = (Main)ins;
                                    foreach (NodoAST instruccion in main.Instrucciones)
                                    {
                                        if (instruccion is DefinicionStruct)
                                        {
                                            DefinicionStruct crear = (DefinicionStruct)instruccion;
                                            crear.ejecutar(global, auxArbol);
                                        }
                                    }
                                    Entorno local = new Entorno(global);
                                    main.ejecutar(local, auxArbol);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("El arbol de Irony no se construyo. Cadena invalida!\n");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());

                    }
                }
                else
                {
                    // Aca deben gestionar esos errores :)
                    MessageBox.Show("Hay errores lexicos o sintacticos\n El arbol de Irony no se construyo. Cadena invalida!\n");
                }
            }


        }

        /**
         * @fn  private void cargarArchivo_Click(object sender, EventArgs e)
         *
         * @brief   Se procede a cargar el archivo.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   sender  Source of the event.
         * @param   e       Event information.
         */

        private void cargarArchivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Seleccionar archivo de entrada";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Navarro files (*.navarro)|*.navarro|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtEntrada.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        /**
         * @fn  private void btnLimpiarEntrada_Click(object sender, EventArgs e)
         *
         * @brief   Limpia la consola de entrada.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   sender  Source of the event.
         * @param   e       Event information.
         */

        private void btnLimpiarEntrada_Click(object sender, EventArgs e)
        {
            txtEntrada.Text = string.Empty;
        }

        /**
         * @fn  private void btnLimpiarSalida_Click(object sender, EventArgs e)
         *
         * @brief   Limpia la consola de salida.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @param   sender  Source of the event.
         * @param   e       Event information.
         */

        private void btnLimpiarSalida_Click(object sender, EventArgs e)
        {
            txtSalida.Text = string.Empty;
        }
    }
}
