using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interprete_Irony
{
    /**
     * @class   Program
     *
     * @brief   El archivo principal del programa.
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    static class Program
    {
        static GUI gui;

        /**
         * @fn  public static GUI getGUI()
         *
         * @brief   Devuelve la interfaz grafica de usuario.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         *
         * @return  La interfaz grafica de usuario.
         */

        public static GUI getGUI()
        {
            return gui;
        }

        /**
         * @fn  static void Main()
         *
         * @brief   Punto de entrada principal para la aplicación.
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         */

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            gui = new GUI();
            Application.Run(gui);
        }
    }
}
