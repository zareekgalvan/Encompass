/*TASK Tools Miscellaneus support for all application*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

//                                                          //AUTHOR: Towa (GLG-Gerardo López).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: 13-Mayo-2011.
//                                                          //PURPOSE:
//                                                          //Shared methods for all applications.

namespace TowaInfrastructure
{
    //==================================================================================================================
    public static partial class Tools
    {
        //--------------------------------------------------------------------------------------------------------------
        static Tools(                                       //Prepara las constantes para poder utilizarlas.
            //                                              //CADA VEZ QUE SE AÑADAN CONSTANTES QUE REQUIERAN SER
            //                                              //      INICIALIZADAS, SE AÑADE LA LLAMADA A OTRO MÉTODO.
            )
        {
        }

        //==============================================================================================================
        /*TASK Tools.subAbort subAbort and subWarnint*/
        //--------------------------------------------------------------------------------------------------------------
        public static void subAbort(                        //Aborta ejecucion al detectar situación anormal. Puede ser
            //                                              //      WinForms app o Console app.

            //                                              //Mensaje descriptivo del aborto.
            String strMessage_I
            )
        {
            String strMethodCallS = Tools.strStackOnlyMethodCalls(Environment.StackTrace);

            String strFullMessage = "<<<ABNORMAL END>>>" + Environment.NewLine + "MESSAGE:" + Environment.NewLine +
                 strMessage_I + Environment.NewLine + "METHOD CALLS:" + Environment.NewLine + strMethodCallS;

            if (
                Application.MessageLoop
                )
            {
                //                                          //Aborto en WinForms app.
                MessageBox.Show(strFullMessage);
            }
            else
            {
                //                                          //Aborto en Console app.
                Console.WriteLine(strFullMessage);

                Console.WriteLine("");
                Console.WriteLine("ENTER KEY TO END");
                String strReadLine = Console.ReadLine();
            }

            //                                              //Existen 2 posibilidades para continuar o terminar
            if (
                Test.boolIsTestAbortOn()
                )
            {
                /*NSTD*/
                throw new SysexceptuserUserAbort(strFullMessage);
                /*END-NSTD*/
            }
            else
            {
                Environment.Exit(0);
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subWarning(                      //Ejecucion al detectar situación anormal.
            //                                              //NO ABORTA PARA PERMITIR UN DIAGNÓSTICO MAS COMPLETO, SIN
            //                                              //      EMBARGO PUEDE TENER COMPORTAMIENTO IMPREDECIBLE.
            //                                              //Puede ser WinForms app o Console app.

            //                                              //Mensaje descriptivo del aborto.
            String strMessage_I
            )
        {
            String strMethodCallS = Tools.strStackOnlyMethodCalls(Environment.StackTrace);

            String strFullMessage = "<<<SHOULD ABORT EXECUTION AFTER ENDING DIAGNOSTIC>>>" + Environment.NewLine + 
                "MESSAGE:" + Environment.NewLine + strMessage_I + Environment.NewLine + "METHOD CALLS:" + 
                Environment.NewLine + strMethodCallS;
            if (
                Application.MessageLoop
                )
            {
                //                                          //Aborto en WinForms app.
                MessageBox.Show(strFullMessage);
            }
            else
            {
                //                                          //Aborto en Console app.
                Console.WriteLine(strFullMessage);

                Console.WriteLine("");
                Console.WriteLine("ENTER KEY TO CONTINUE");
                String strReadLine = Console.ReadLine();
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        public static String strStackOnlyMethodCalls(       //ESTE METODO DEBE SER REESCRITO PARA CADA TECNOLOGÍA O
            //                                              //      INSTANCIA DE LA TECNOLOGÍA.
            //
            //                                              //EN C# al igual que en otras tecnologías el stack
            //                                              //      contendrá mucha información.
            //                                              //Se extrae solo la parte necesaria para desplegar:
            //                                              //[
            //                                              //namespace/package.class.method(parameters):line nnn
            //                                              //namespace/package.class.method(parameters):line nnn
            //                                              //...
            //                                              //namespace/package.class.method(parameters):line nnn
            //                                              //]
            //                                              //Lo que se requiere es la secuencia de métodos desde el
            //                                              //      inicio, hasta antes de la ejecusicón del subAbort.
            //                                              //nótese que para desplegar la información como se muestra,
            //                                              //      en el String será necesario incluir los caracteres
            //                                              //      de NewLine entre cada una de estas líneas.
            //                                              //Ejemplo:
            //                                              //[
            //                                              //TowaInfrastructure.Sys.subCopyFileRewrite(...):line 741
            //                                              //QEnablerBase.TestSys.subSys01TestC():line 341
            //                                              //QEnablerBase.frmPrueba.btnPrueba_Click(...):line 25
            //                                              //]
            //                                              //Stack (como lo proporciona la tecnología)
            String strStack_I
            )
        {
            //                                              //La estrategia será:
            //                                              //1. Localizar "TowaInfrastructure.", esta será la línea en
            //                                              //      subAborta donde se obtiene la información de Stack
            //                                              //      (esto no se require).
            //                                              //2. Localiza el siguiente NewLine, esto será la
            //                                              //      información donde se ejecuta subAborta, esto es
            //                                              //      donde sucedió la causa del aborto, a partir de aquí
            //                                              //      no interesa (después del NewLine).
            //                                              //3. Localiza "System.", nos interesa hasta antes de esta
            //                                              //      línea (se tiene NewLine + "___at_" + "System.".
            //                                              //4. Convierte a arrstr de líneas para poder analizarlas.
            //                                              //5. De cada línea elimina el principio que es "___at_" y lo 
            //                                              //      que esta después del ) hasta antes de :line.
            //                                              //6. Vuelve a formar el String (con los NewLine entre
            //                                              //      líneas).
            //                                              //7. Chars '&' should be removed.

            //                                              //Localiza a partir de donde interesa.
            int intStart = strStack_I.IndexOf("TowaInfrastructure.");
            intStart = strStack_I.IndexOf(Environment.NewLine, intStart);
            intStart = intStart + Environment.NewLine.Length;

            //                                              //Localiza a partir de donde ya no interesa.
            int intEndPlusOne = strStack_I.IndexOf("System.", intStart);
            intEndPlusOne = intEndPlusOne - (Environment.NewLine + "___at_").Length;
            //                                              //Toma la parte que nos interesa (esto aún tiene información
            //                                              //      de los archivos que se require eliminar).
            String strStackOnlyMethodCalls = strStack_I.Substring(intStart, intEndPlusOne - intStart);

            String[] arrstrLineOnlyMethodCall =
                strStackOnlyMethodCalls.Split(new String[] { Environment.NewLine }, StringSplitOptions.None);

            for (int intI = 0; intI < arrstrLineOnlyMethodCall.Length; intI = intI + 1)
            {
                //                                          //To easy code
                String strLine = arrstrLineOnlyMethodCall[intI];

                //                                          //Find ')', end of method call y luego :
                int intParenthesis = strLine.IndexOf(')');

                //                                          //Find ':', start of ":line nn", some tows of informatión do
                //                                          //      contain this ":line nn"
                int intColon = strLine.IndexOf(':');

                //                                          //Extrat required information.
                String strLineRevised = strLine.Substring("___at_".Length, intParenthesis + 1 - "___at_".Length);
                strLineRevised = strLineRevised.Replace("&", "");

                if (
                    intColon >= 0
                    )
                {
                    strLineRevised = strLineRevised + strLine.Substring(intColon);
                }

                arrstrLineOnlyMethodCall[intI] = strLineRevised;
            }

            //                                              //Vuelve a formato String.
            strStackOnlyMethodCalls = String.Join(Environment.NewLine, arrstrLineOnlyMethodCall);

            return strStackOnlyMethodCalls;
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Tools.boolIsDigit methods to test digits and letters*/
        //--------------------------------------------------------------------------------------------------------------
        //                                                  //En .net existen las funciones Char.IsDigits,
        //                                                  //      Char.IsLetter y Char.IsLetterOrDigit, estas
        //                                                  //      reconocen como válidos los dígitos y letras en
        //                                                  //      TODOS los lenguajes implementados en .net.
        //                                                  //310 dígitos y 47,672 letras.

        //                                                  //Aquí se implementas funciones para reconoder dígito (solo
        //                                                  //      0-9), letras (solo A-Z y a-z).

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsDigit(                     //Valida.

            //                                              //bool, true si es 0-9.

            //                                              //Caracter a validar.
            char charAValidar_I
            )
        {
            return (
                (charAValidar_I >= '0') && (charAValidar_I <= '9')
                );
        }

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsLetter(                    //Valida.

            //                                              //bool, true si es A-Z o a-z.

            //                                              //Caracter a validar.
            char charAValidar_I
            )
        {
            return (
                (charAValidar_I >= 'A') && (charAValidar_I <= 'Z') ||
                (charAValidar_I >= 'a') && (charAValidar_I <= 'z')
                );
        }

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsDigitOrLetter(             //Valida.

            //                                              //bool, true si es 0-9, A-Z o a-z.

            //                                              //Caracter a validar.
            char charAValidar_I
            )
        {
            return (
                (charAValidar_I >= '0') && (charAValidar_I <= '9') ||
                (charAValidar_I >= 'A') && (charAValidar_I <= 'Z') ||
                (charAValidar_I >= 'a') && (charAValidar_I <= 'z')
                );
        }

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsLetterUpper(               //Valida.

            //                                              //bool, true si es A-Z.

            //                                              //Caracter a validar.
            char charAValidar_I
            )
        {
            return (
                (charAValidar_I >= 'A') && (charAValidar_I <= 'Z')
                );
        }

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsLetterLower(               //Valida.

            //                                              //bool, true si es a-z.

            //                                              //Caracter a validar.
            char charAValidar_I
            )
        {
            return (
                (charAValidar_I >= 'a') && (charAValidar_I <= 'z')
                );
        }

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsDigitOrLetterUpper(        //Valida.

            //                                              //bool, true si es 0-9 o A-Z.

            //                                              //Caracter a validar.
            char charAValidar_I
            )
        {
            return (
                (charAValidar_I >= '0') && (charAValidar_I <= '9') ||
                (charAValidar_I >= 'A') && (charAValidar_I <= 'Z')
                );
        }

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsDigitOrLetterLower(        //Valida.

            //                                              //bool, true si es 0-9 o a-z.

            //                                              //Caracter a validar.
            char charAValidar_I
            )
        {
            return (
                (charAValidar_I >= '0') && (charAValidar_I <= '9') ||
                (charAValidar_I >= 'a') && (charAValidar_I <= 'z')
                );
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Tools.boolIsDigit methods to test digits and letters*/
        //--------------------------------------------------------------------------------------------------------------
        /*CONSTANTS*/

        public static readonly DateTime dateMIN_VALUE = new DateTime(0001, 01, 01);
        public static readonly DateTime dateMAX_VALUE = new DateTime(9999, 12, 31);
        public static readonly DateTime timeMIN_VALUE = new DateTime(0001, 01, 01, 0, 0, 0);
        public static readonly DateTime timeMAX_VALUE = new DateTime(9999, 12, 31, 23, 59, 59);

        //                                                  //En .net existen las funciones DateTime.MinValue y
        //                                                  //      DateTime.MaxValue que nos sirven para el timestamp.

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsDate(                      //Valida sea un fecha.

            //                                              //bool, true si es fecha.

            //                                              //Fecha a validar.
            DateTime date_I
            )
        {
            return (
                //                                          //Con excepción de la fecha todo esta en ceros.
                (date_I.Hour == 0) && (date_I.Minute == 0) && (date_I.Second == 0) && (date_I.Millisecond == 0)
                );
        }

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsTime(                      //Valida sea una time.

            //                                              //bool, true si es time.

            //                                              //Time a validar.
            DateTime time_I
            )
        {
            return (
                //                                          //Con excepción de la fecha y hora todo esta en ceros.
                time_I.Millisecond == 0
                );
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Tools.strAlign Alignment of text*/
        //--------------------------------------------------------------------------------------------------------------
        public static String strAlign(                      //Centra el texto y lo edita.

            //                                              //str, texto centrado conforme a los parámetros, si excede
            //                                              //      la longitud deseada se trunca.

            //                                              //Texto que debe ser alineado.
            String strText_I,
            //                                              //Longitud del texto nuevo que se debe producir.
            int intLong_I,
            //                                              //Opción de alineación, debe ser CENTER.
            ToolsalignmentEnum alignment_I,
            //                                              //Caracter para relleno a la izquierda.
            char charLeft_I,
            //                                              //Caracter para relleno a la derecha.
            char charRight_I
            )
        {
            if (
                alignment_I != ToolsalignmentEnum.CENTER
                )
                Tools.subAbort("alignment_I(" + alignment_I + ") debe ser CENTER");


            //                                          //Para formar el nuevo String.
            String strAlign = strText_I;

            //                                          //Si excede el tamaño deseado lo trunca del lado derecho.
            if (
                //                                      //Excede el tamaño
                strText_I.Length > intLong_I
                )
            {
                //                                      //Corta la parte excedente.
                strAlign = strAlign.Substring(0, intLong_I);
            }

            //                                          //Calcula la cantidad de caracteres de inicio y fin.
            int intRelleno = intLong_I - strAlign.Length;

            //                                          //Si el valor en impar lo redondea hacia arriba.
            int intLeft = (intRelleno + 1) / 2;
            int intRigth = intRelleno - intLeft;

            //                                          //Genera el texto con los inicio y fin y el texto alineado.
            //                                          //Nótese que es indistinto usar PadLeft o PadRight
            strAlign = "".PadLeft(intLeft, charLeft_I) + strAlign + "".PadRight(intRigth, charRight_I);

            return strAlign;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static String strAlign(                      //Alinea izquierda o derrecha el texto y lo edita.

            //                                              //str, texto alineado conforme a los parámetros, si excede
            //                                              //      la longitud deseada se trunca.

            //                                              //Texto que debe ser alineado.
            String strText_I,
            //                                              //Longitud del texto nuevo que se debe producir.
            int intLong_I,
            //                                              //Opción de alineación, debe ser LEFT, RIGTH.
            ToolsalignmentEnum alignment_I,
            //                                              //Caracter para relleno a la izquierda/derecha.
            char charLeftRight_I
            )
        {
            if (!(
                (alignment_I == ToolsalignmentEnum.LEFT) || (alignment_I == ToolsalignmentEnum.RIGHT)
                ))
                Tools.subAbort("alignment_I(" + alignment_I + ") debe ser LEFT o RIGHT");

            //                                              //Para formar el nuevo String.
            String strAlign = strText_I;

            //                                              //Si excede el tamaño deseado lo trunca del lado derecho.
            if (
                //                                          //Excede el tamaño
                strText_I.Length > intLong_I
                )
            {
                //                                          //Corta la parte excedente.
                strAlign = strAlign.Substring(0, intLong_I);
            }

            //                                              //Genera el texto con los inicio/fin y el texto alineado.
            if (
                alignment_I == ToolsalignmentEnum.LEFT
                )
            {
                //                                          //Completa con caracteres del lado derecho.
                strAlign = strAlign.PadRight(intLong_I, charLeftRight_I);
            }
            else
            {
                //                                          //Completa con caracteres del lado izquierdo.
                strAlign = strAlign.PadLeft(intLong_I, charLeftRight_I);
            }

            return strAlign;
        }

        //==============================================================================================================
        /*TASK Tools.strTrimExcel Trimming text like excel*/
        //--------------------------------------------------------------------------------------------------------------
        public static String strTrimExcel(                  //Hace un Trim similar al lo que hace Excel, esto es, 
            //                                              //      elimina los espacios al principio y al final y solo 
            //                                              //      deja un espacio entre las palabras que contenga, una
            //                                              //      palabra es un conjunto de caracteres contiguos
            //                                              //      diferentes a espacio.

            //                                              //str, ya sin espacios en exceso (Trim EXCEL).

            //                                              //String para hacer el Trim EXCEL
            String str_I
            )
        {
            //                                              //Se cicla para buscar el inicio de la primera palabra, sale
            //                                              //      cuando
            int intIni = 0;
            /*UNTIL-DO*/
            while (!(
                //                                          //Llego al fin del String
                (intIni >= str_I.Length) ||
                //                                          //Encuentra caracter diferente de espacio
                (str_I[intIni] != ' ')
                ))
            {
                intIni = intIni + 1;
            }

            String strTrimExcel = "";

            //                                              //Se cicla para procesar cada palabra
            /*LOOP*/
            while (true)
            {
                //                                          //Extrae la siguiente palabra del String
                String strWord;
                Tools.subWord(str_I, ref intIni, out strWord);

                //                                          //Concatena la palabra
                strTrimExcel = strTrimExcel + strWord;

                /*EXIT-IF*/
                if (
                    //                                      //sale cuando llega al fin del String
                    intIni >= str_I.Length
                    )
                    break;

                strTrimExcel = strTrimExcel + " ";
            }
            /*END-LOOP*/

            return strTrimExcel;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subWord(                        //Procesa una palabra del String.

            //                                              //String que contiene las palabras.
            String str_I,
            //                                              //Posición donde inicia la palabra que se procesará, regresa
            //                                              //      la posición del inicio de la siguiente palabra, o la
            //                                              //      posición inmediata al fin del String.
            ref int intI_IO,
            //                                              //Palabra procesada.
            out String strWord_O
            )
        {
            //                                              //Se cicla buscando el fin de la palabra, sale cuando;
            int intFin = intI_IO;
            /*UNTIL-DO*/
            while (!(
                //                                          //Llega al fin del String
                (intFin >= str_I.Length) ||
                //                                          //Encontró un espacio (fin de palabra)
                (str_I[intFin] == ' ')
                ))
            {
                intFin = intFin + 1;
            }

            strWord_O = str_I.Substring(intI_IO, intFin - intI_IO);

            //                                              //Se cicla buscando el inicio de la siguiente palabra, hasta
            intI_IO = intFin;
            /*UNTIL-DO*/
            while (!(
                //                                          //Llega al fin del String
                (intI_IO >= str_I.Length) ||
                //                                          //Encontró el inicio de la siguiente palabra
                (str_I[intI_IO] != ' ')
                ))
            {
                intI_IO = intI_IO + 1;
            }
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Tools.intSearchWordInString Search word in a string*/
        //--------------------------------------------------------------------------------------------------------------
        public static int intSearchWordInString(            //Busca una "palabra" en un String, una "palabra" es un 
            //                                              //      conjunto de caracteres (diferentes de espacios) 
            //                                              //      DELIMITED por el inicio o fin del String o por uno
            //                                              //      o varios espacios. Ej. en el String "__ABC___ZYZ" se
            //                                              //      tiene las palabras "ABC" y XYZ" (ojo en el String se
            //                                              //      uso _ como sustituto de espacio).

            //                                              //int, posición (base 0) donde encuentra la palabra, -1 si
            //                                              //      no encontró.

            //                                              //Palabra que se buscará. (Deben ser caracteres continuos 
            //                                              //      sin espacios).
            String strWord_I,
            //                                              //String sobre el cual se buscará la palabra.
            String str_I
            )
        {
            //                                              //Inicializa resultado a NO ENCONTRO
            int intSearchPalabraEnString = -1;

            int intIni = 0;

            /*LOOP*/
            while (true)
            {
                //                                          //Se cicla para buscar el incial de las siguiente palabra en
                //                                          //      String, termina cuando:
                /*UNTIL-DO*/
                while (!(
                    //                                      //Llego al fin del String.
                    (intIni >= str_I.Length) ||
                    //                                      //Encuentra el inicio de una palabra
                    (str_I[intIni] != ' ')
                    ))
                {
                    intIni = intIni + 1;
                }

                /*EXIT-IF*/
                if (
                    //                                      //En el ciclo anterior encontro la palabra
                    (intSearchPalabraEnString >= 0) ||
                    //                                      //LLego al fin del String.
                    (intIni >= str_I.Length)
                    )
                    break;

                //                                          //Se cicla para buscar el fin de la palabra que inicia en
                //                                          //      intIni.
                int intFin = intIni;
                /*UNTIL-DO*/
                while (!(
                    //                                      //Llego al fin del String
                    (intFin >= str_I.Length) ||
                    //                                      //Encontró el fin de la palabra
                    (str_I[intFin] == ' ')
                    ))
                {
                    intFin = intFin + 1;
                }

                if (
                    str_I.Substring(intIni, intFin - intIni) == strWord_I
                    )
                {
                    //                                      //Pasa la posición de la palabra 
                    intSearchPalabraEnString = intIni;
                }

                intIni = intFin + 1;
            }
            /*END-LOOP*/

            return intSearchPalabraEnString;
        }
        /*END-TASK*/

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
