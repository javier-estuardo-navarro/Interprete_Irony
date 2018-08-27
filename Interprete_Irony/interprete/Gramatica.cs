/*
 * Interprete implementado con C# y Irony
 * Desarrollado por Javier Navarro
 * Como material de apoyo para el curso de Organizacion de Lenguajes y Compiladores 2
 * Agosto 2018
 */

using Irony.Parsing;


namespace Interprete_Irony.interprete
{
    /**
     * @class   Gramatica
     *
     * @brief   Gramatica para generar analizadores con Irony
     *
     * @author  Javier Estuardo Navarro
     * @date    26/08/2018
     */

    class Gramatica : Irony.Parsing.Grammar
    {

        /**
         * @fn  public Gramatica() : base(false)
         *
         * @brief   Constructor de la gramatica, hereda de Grammar, por lo que recibe una 
         *          bandera booleana para definir si hara diferencia entre minusculas y 
         *          mayusculas (Case sensitive).
         *          true: Diferencia entre mayusculas y minusculas JAviEr != javier
         *          false: No hay diferencia entre mayusculas y minusculas JAviEr = javier
         *
         * @author  Javier Estuardo Navarro
         * @date    26/08/2018
         */

        public Gramatica() : base(false)
        {
            #region Declaracion de TERMINALES
            /* En esta region se declararan los terminales, es decir, los nodos hoja del AST,
             * esta región contendrá:
             *  1. Palabras reservadas
             *  2. Operadores y simbolos
             *  3. Terminales definidos con una expresion regular
             *  4. Comentarios
            */

            #region Palabras reservadas
            // Se procede a declarar todas las palabras reservadas que pertenezcan al lenguaje.
            KeyTerm pr_int = ToTerm("int"),
             pr_double = ToTerm("double"),
             pr_char = ToTerm("char"),
             pr_bool = ToTerm("bool"),
             pr_String = ToTerm("String"),
             pr_void = ToTerm("void"),
             pr_return = ToTerm("return"),
             pr_main = ToTerm("main","pr_main"),
             pr_break = ToTerm("break"),
             pr_continue = ToTerm("continue"),
             pr_if = ToTerm("if"),
             pr_else = ToTerm("else"),
             pr_while = ToTerm("while"),
             pr_Struct = ToTerm("Struct"),
             pr_print = ToTerm("print");
            /* El metodo "MarkReservedWords" le dice al parser que Terminales seran palabras reservadas,
             * esto para que tengan prioridad sobre los identificadores, de lo contrario las palabras reservadas,
             * se tomarian como identificadores.            
            */
            MarkReservedWords("int", "double", "char", "bool", "String", "void", "return", "main",
                "break", "continue", "if", "else", "while", "Struct", "print");
            #endregion

            #region Operadores y simbolos
            Terminal ptcoma = ToTerm(";"),
                coma = ToTerm(","),
                punto = ToTerm("."),
                dospts = ToTerm(":"),
                parizq = ToTerm("("),
                parder = ToTerm(")"),
                llaizq = ToTerm("{"),
                llader = ToTerm("}"),
                signo_mas = ToTerm("+"),
                signo_menos = ToTerm("-"),
                signo_por = ToTerm("*"),
                signo_div = ToTerm("/"),
                signo_pot = ToTerm("^"),
                igual_que = ToTerm("=="),
                diferente_que = ToTerm("!="),
                menor_que = ToTerm("<"),
                mayor_que = ToTerm(">"),
                pr_or = ToTerm("||"),
                pr_and = ToTerm("&&"),
                pr_not = ToTerm("!"),
                pr_true = ToTerm("true", "true"),
                pr_false = ToTerm("false", "false"),
                igual = ToTerm("=");
            #endregion

            #region Terminales definidos con una expresion regular
            /* Tipo de literal que reconoce numeros equivalente a 
             * RegexBasedTerminal entero = new RegexBasedTerminal("entero", "[0-9]+");
             * RegexBasedTerminal decimal = new RegexBasedTerminal("decimal", "[0-9]+(.[0-9]+)?");
             * Pero mas completo porque reconoce gran variedad de tipos de números, 
             * desde enteros simples (por ejemplo, 1) hasta decimales (por ejemplo, 1.0) 
             * hasta números expresados ​​en notación científica (por ejemplo, 1.1e2).
             */
            NumberLiteral numero = new NumberLiteral("numero");
            /*
             * Este terminal identificará esos tokens en el código fuente que representan las variables 
             * expresadas de la manera estándar normal (es decir, comienza con un guión bajo o letra 
             * y contiene solo letras, números y guiones bajos) pero puede configurarse para identificar
             * otros métodos no estándar de expresión variables.
            */
            IdentifierTerminal identificador = new IdentifierTerminal("identificador");
            /*
             * Este terminal puede identificar literales de cadena y caracter 
             * ya que es posible configurar los caracteres de inicio/finalización.
             * Verificar el enum stringOptions, para mas informacion
            */
            StringLiteral cadena = new StringLiteral("cadena", "\"", StringOptions.AllowsAllEscapes);
            StringLiteral caracter = new StringLiteral("caracter", "\'", StringOptions.IsChar);
            #endregion

            #region Comentarios
            /* Se procede a definir los comentarios, los mismos seran ignorados por el analizador.
             * Constructor: CommentTerminal(<nombre>,<SimboloInicial>,<Simbolo(s)Final(es)>)
             * Un comentario podria terminar con uno o mas simbolos.  
             * Luego de definirlo es necesario agregar a la lista de terminales que seran ignorados, 
             * es decir, que no formaran parte de la gramatica con el metodo: NonGrammarTerminals.Add(<terminal>)
            */
            CommentTerminal comentarioMultilinea = new CommentTerminal("comentarioMultiLinea", "/*", "*/");

            base.NonGrammarTerminals.Add(comentarioMultilinea);
            CommentTerminal comentarioUnilinea = new CommentTerminal("comentarioUniLinea", "//", "\n", "\r\n");
            base.NonGrammarTerminals.Add(comentarioUnilinea);
            #endregion

            #endregion

            #region Declaracion de NO TERMINALES
            // En esta region se declararan los no terminales, es decir, los nodos intermedios del AST,
            NonTerminal INI = new NonTerminal("INI");
            NonTerminal FIELD_DECLARATION = new NonTerminal("FIELD_DECLARATION");
            NonTerminal FIELD = new NonTerminal("FIELD");
            NonTerminal METHOD_DECLARATION = new NonTerminal("METHOD_DECLARATION");
            NonTerminal BLOQUE_SENTENCIAS = new NonTerminal("BLOQUE_SENTENCIAS");
            NonTerminal DECLARACION = new NonTerminal("DECLARACION");
            NonTerminal ASIGNACION = new NonTerminal("ASIGNACION");
            NonTerminal LISTA_SIM = new NonTerminal("LISTA_SIM");
            NonTerminal LISTA_PARAMETROS = new NonTerminal("LISTA_PARAMETROS");
            NonTerminal PARAMETRO = new NonTerminal("PARAMETRO");
            NonTerminal LISTA_ATRIBUTOS = new NonTerminal("LISTA_ATRIBUTOS");
            NonTerminal ATRIBUTO = new NonTerminal("ATRIBUTO");
            NonTerminal SENTENCIA = new NonTerminal("SENTENCIA");
            NonTerminal STATEMENTS = new NonTerminal("STATEMENTS");
            NonTerminal SENTENCIA_IF = new NonTerminal("SENTENCIA_IF");
            NonTerminal SENTENCIA_WHILE = new NonTerminal("SENTENCIA_WHILE");
            NonTerminal SENTENCIA_BREAK = new NonTerminal("SENTENCIA_BREAK");
            NonTerminal SENTENCIA_CONTINUE = new NonTerminal("SENTENCIA_CONTINUE");
            NonTerminal SENTENCIA_RETURN = new NonTerminal("SENTENCIA_RETURN");
            NonTerminal SENTENCIA_PRINT = new NonTerminal("SENTENCIA_PRINT");
            NonTerminal SENTENCIA_STRUCT = new NonTerminal("SENTENCIA_STRUCT");
            NonTerminal SENTENCIA_LLAMADA = new NonTerminal("SENTENCIA_LLAMADA");
            NonTerminal SENTENCIA_ACCESO = new NonTerminal("SENTENCIA_ACCESO");
            NonTerminal EXPRESION = new NonTerminal("EXPRESION");
            NonTerminal EXPRESION_ARITMETICA = new NonTerminal("EXPRESION_ARITMETICA");
            NonTerminal EXPRESION_RELACIONAL = new NonTerminal("EXPRESION_RELACIONAL");
            NonTerminal EXPRESION_LOGICA = new NonTerminal("EXPRESION_LOGICA");
            NonTerminal PRIMITIVO = new NonTerminal("PRIMITIVO");
            NonTerminal LISTA_ARGUMENTOS = new NonTerminal("LISTA_ARGUMENTOS");
            NonTerminal TIPO = new NonTerminal("TIPO");
            #endregion

            #region Gramatica


            INI.Rule = FIELD_DECLARATION;

            FIELD_DECLARATION.Rule = MakePlusRule(FIELD_DECLARATION, FIELD);

            FIELD.Rule = METHOD_DECLARATION
                | DECLARACION + ptcoma
                | SENTENCIA_STRUCT + ptcoma;

            METHOD_DECLARATION.Rule = TIPO + identificador + parizq + LISTA_PARAMETROS + parder + BLOQUE_SENTENCIAS
                | identificador + identificador + parizq + LISTA_PARAMETROS + parder + BLOQUE_SENTENCIAS
                | TIPO + identificador + parizq + parder + BLOQUE_SENTENCIAS
                | identificador + identificador + parizq + parder + BLOQUE_SENTENCIAS
                | TIPO + pr_main + parizq + parder + BLOQUE_SENTENCIAS;

            LISTA_PARAMETROS.Rule = MakeStarRule(LISTA_PARAMETROS, coma, PARAMETRO);

            PARAMETRO.Rule = TIPO + identificador;

            LISTA_ATRIBUTOS.Rule = MakePlusRule(LISTA_ATRIBUTOS, coma, ATRIBUTO);

            ATRIBUTO.Rule = TIPO + identificador;

            BLOQUE_SENTENCIAS.Rule = llaizq + STATEMENTS + llader;

            STATEMENTS.Rule = MakeStarRule(STATEMENTS, SENTENCIA);

            SENTENCIA.Rule = DECLARACION + ptcoma
                | ASIGNACION + ptcoma
                | SENTENCIA_ACCESO + ptcoma
                | SENTENCIA_LLAMADA + ptcoma
                | SENTENCIA_IF
                | SENTENCIA_WHILE
                | SENTENCIA_RETURN + ptcoma
                | SENTENCIA_BREAK + ptcoma
                | SENTENCIA_CONTINUE + ptcoma
                | SENTENCIA_PRINT + ptcoma
                | SENTENCIA_STRUCT + ptcoma;


            DECLARACION.Rule = TIPO + LISTA_SIM + igual + EXPRESION
                | TIPO + LISTA_SIM
                | identificador + identificador;

            ASIGNACION.Rule = LISTA_SIM + igual + EXPRESION
                | identificador + punto + identificador + igual + EXPRESION;

            LISTA_SIM.Rule = MakePlusRule(LISTA_SIM, coma, identificador);

            SENTENCIA_IF.Rule = pr_if + parizq + EXPRESION + parder + BLOQUE_SENTENCIAS
                | pr_if + parizq + EXPRESION + parder + BLOQUE_SENTENCIAS + pr_else + BLOQUE_SENTENCIAS;

            SENTENCIA_WHILE.Rule = pr_while + parizq + EXPRESION + parder + BLOQUE_SENTENCIAS;

            SENTENCIA_RETURN.Rule = pr_return + EXPRESION
                | pr_return;

            SENTENCIA_BREAK.Rule = pr_break;

            SENTENCIA_CONTINUE.Rule = pr_continue;

            SENTENCIA_PRINT.Rule = pr_print + parizq + EXPRESION + parder;

            SENTENCIA_STRUCT.Rule = pr_Struct + identificador + parizq + LISTA_ATRIBUTOS + parder;

            SENTENCIA_LLAMADA.Rule = identificador + parizq + LISTA_ARGUMENTOS + parder
                | identificador + parizq + parder;

            SENTENCIA_ACCESO.Rule = identificador + punto + identificador;

            EXPRESION.Rule = EXPRESION_ARITMETICA
                | EXPRESION_LOGICA
                | EXPRESION_RELACIONAL
                | PRIMITIVO
                | SENTENCIA_ACCESO
                | parizq + EXPRESION + parder
                | SENTENCIA_LLAMADA;


            EXPRESION_ARITMETICA.Rule = signo_menos + EXPRESION
                | EXPRESION + signo_mas + EXPRESION
                | EXPRESION + signo_menos + EXPRESION
                | EXPRESION + signo_por + EXPRESION
                | EXPRESION + signo_div + EXPRESION
                | EXPRESION + signo_pot + EXPRESION;

            EXPRESION_RELACIONAL.Rule = EXPRESION + mayor_que + EXPRESION
                | EXPRESION + menor_que + EXPRESION
                | EXPRESION + igual_que + EXPRESION
                | EXPRESION + diferente_que + EXPRESION;

            EXPRESION_LOGICA.Rule = EXPRESION + pr_or + EXPRESION
                | EXPRESION + pr_and + EXPRESION
                | pr_not + EXPRESION;

            PRIMITIVO.Rule = numero
                | cadena
                | caracter
                | pr_true
                | pr_false
                | identificador;

            LISTA_ARGUMENTOS.Rule = MakeStarRule(LISTA_ARGUMENTOS, coma, EXPRESION);

            TIPO.Rule = pr_bool
                | pr_char
                | pr_String
                | pr_int
                | pr_double
                | pr_void;


            #endregion



            #region precedencia
            /*
             * En esta region se define la precedencia y asociatividad para remover la ambiguedad
             * de la gramatica de expresiones.             
            */
            RegisterOperators(1, Associativity.Right, igual);
            RegisterOperators(2, Associativity.Left, pr_or);
            RegisterOperators(4, Associativity.Left, pr_and);
            RegisterOperators(5, Associativity.Left, igual_que, diferente_que);
            RegisterOperators(6, Associativity.Left, mayor_que, menor_que);
            RegisterOperators(7, Associativity.Left, signo_mas, signo_menos);
            RegisterOperators(8, Associativity.Left, signo_por, signo_div);
            RegisterOperators(9, Associativity.Right, signo_pot);
            RegisterOperators(10, Associativity.Right, pr_not);
            RegisterOperators(11, Associativity.Left, punto);
            RegisterOperators(12, Associativity.Neutral, parizq, parder);
            #endregion
            /* Se define la raiz del AST es decir el simbolo inicial de la gramatica,
             * este debe ser un No Terminal
            */

            /*
             * Otros metodos que podrian ser utiles son los siguientes:
             * 
             * Marcar signos de puntuacion, esto para no tener nodos inutiles en el AST, ignora terminales.
             * MarkPunctuation(llaizq, llader, ptcoma, dospts, coma);
             * 
             * Marcar trascendentes Nodos No terminales, esto para no tener nodos inutiles en el AST, ignora No terminales.
             * MarkTransient(NoTerminal1, NoTerminal2,...,NoTerminalN);
             * Es util cuando se tienen nodos de listas por ejemplo. L->L Elemento |Elemento
             * Se podrian marcar trascendentes los nodos L, y asi tener el nodo padre con n hijos Elemento. 
            */
            MarkPunctuation(parizq, parder, llaizq, llader, ptcoma, dospts, coma, igual, punto);
            this.Root = INI;
        }
    }
}
