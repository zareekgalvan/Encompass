/*TASK Test Support for Testing*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

//                                                          //AUTHOR: Towa (GLG-Gerardo López).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: 13-Mayo-2011.
//                                                          //PURPOSE:
//                                                          //Implementación de clase estática para facilitar el Testing

namespace TowaInfrastructure
{
    //==================================================================================================================
    public static /*nonpartial*/ class Test                 //Clase que incuye métodos estáticos (funciones o
    //                                                      //      subrutinas) de uso compartido en todos los sistemas.
    {
        //--------------------------------------------------------------------------------------------------------------
        static Test(                                        //Prepara las constantes para poder utilizarlas.
            //                                              //CADA VEZ QUE SE AÑADAN CONSTANTES QUE REQUIERAN SER
            //                                              //      INICIALIZADAS, SE AÑADE LA LLAMADA A OTRO MÉTODO.
            )
        {
            Test.subPrepareConstantsForStrTo(out Test.arrcharKEYBOARD, out Test.arrcharNONVISIBLE,
                out Test.arrstrDESCRIPTION_NONVISIBLE);
            Test.subPrepareConstantTypes(out Test.arrstrPRIMITIVE_TYPE, out Test.arrstrPRIMITIVE_PREFIX,
                out Test.arrstrSYSTEM_TYPE, out Test.arrstrSYSTEM_PREFIX, out Test.arrstrGENERIC_TYPE,
                out Test.arrstrGENERIC_PREFIX);
            Test.subPrepareConstantsToBlockFormat();
            Test.subPrepareConstantsSubTrace();
            Test.subPrepareConstantsTestAbort();
        }

        //==============================================================================================================
        /*TASK Test.PrepareStrTo Constants and initializer for strTo*/
        //--------------------------------------------------------------------------------------------------------------
        //                                                  //Set of methods strTo to analyse and format:
        //                                                  //a) Objects: bclass, btuple & enum.
        //                                                  //b) System objects: sysfile, sysdir, syssr & syssw.
        //                                                  //c) Primitives: int, long, num, char & bool.
        //                                                  //d) Simple objects like: str, date, time, ts & type.

        //--------------------------------------------------------------------------------------------------------------
        /*CONSTANTS*/

        //                                                  //Si un String excede esta longitud, se muestra la longitud
        //                                                  //      ejemplo "abd def.... xyz"<88>.
        private const int intLONG_STRING = 50; 

        //                                                  //In methods strTo, an Item/Row/Matrix of this characters
        //                                                  //      size will be include in one long line.
        private const int intLONG_ITEM_ROW_MATRIX = 40;

        //                                                  //This will be the maximun space reseved for key when strTo
        //                                                  //      display a dictionary, if we have longhest key the
        //                                                  //      content will not be aligned.
        private const int intKEY_LEN_MAX = 50;

        //                                                  //Caracter que será usado como substituto cuando un caracter
        //                                                  //      no sea "visible".
        private const char charSUBSTITUTE_NONVISIBLE = '^';

        //                                                  //Se tiene 4 posible tipos de caracteres, ver charType.

        //                                                  //El siguiente String son caracteres que se pueden
        //                                                  //      introducir por el teclaro.
        //                                                  //charType.KEBOARD.
        private const String strCHAR_KEYBOARD =
            //                                              //El espacio en blanco es un caracter que se puede teclear.
            " " +
            //                                              //Caracteres normales.
            "0123456789" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "abcdefghijklmnopqrstuvwxyz" +
            //                                              //Caracteres acentuados.
            "ÁÉÍÓÚÀÈÌÒÙÄËÏÖÜÂÊÎÔÛ" + "áéíóúàèìòùäëïöüâêîôû" + "Ññ" +
            //                                              //Otras consonantes acentuadas.
            "ŔÝŚĹŹĆŃŸŴŶŜĜĤĴĈ" + "ŕýśĺźćńÿŵŷŝĝĥĵĉ" +
            //                                              //Acentos solos.
            "´`¨^" +

            //                                              //Caracteres especiales, que aparecen en teclado de Mac
            //                                              //      (Spanish - ISO).

            //                                              //Teclas de números.
            "ºª" + @"\" + "!|" + "\"" + "@" + "·#" + "$" + "%" + "&¬" + "/" + "(" + ")" + "=" + "'?" + "¡¿" +
            //                                              //Teclas QW.....
            "€[*+]" +
            //                                              //Teclas AS.....
            "{çÇ}" +
            //                                              //Teclas ZX.....
            "<>,;.:-_" +
            //                                              //Otras teclas que estan en DELL y no en la MAC
            "~";

        //                                                  //Los caracteres anteriores en un arreglo ordenado.
        private static readonly char[] arrcharKEYBOARD;

        //                                                  //El siguiente arreglo son tuplos con info de caracteres
        //                                                  //      que no se pueden desplegar (distorsionan la imagen
        //                                                  //      en pantalla y/o archivo de texto).
        //                                                  //charType.NONVISIBLE_WITH_DESCRIPTION.
        //                                                  //Los tuplos son: <caracter, descripción>.
        //                                                  //Estos caracteres no deben existir en arrcharKEYBOARD.
        private static readonly T2charDescriptionTuple[] arrt2charNONVISIBLE_WITH_DESCRIPTION =
        { 
            new T2charDescriptionTuple('\0', @"\0 Zero"),
            new T2charDescriptionTuple('\a', @"\a Bell (alert)"),
            new T2charDescriptionTuple('\b', @"\b Backspace"),
            new T2charDescriptionTuple('\f', @"\f Formfeed"),
            new T2charDescriptionTuple('\n', @"\n New Line"),
            new T2charDescriptionTuple('\r', @"\r Carriage Return"),
            new T2charDescriptionTuple('\t', @"\t Horizontal Tab"),
            new T2charDescriptionTuple('\v', @"\v Vertical Tab"),
            new T2charDescriptionTuple(Convert.ToChar(128 * 64 + 11), @"'' empty character"),
            new T2charDescriptionTuple(Convert.ToChar(128 * 64 + 12), @"'' empty character"),
            new T2charDescriptionTuple(Convert.ToChar(128 * 64 + 13), @"'' empty character"),
            new T2charDescriptionTuple(Convert.ToChar(128 * 64 + 14), @"'' empty character"),
            new T2charDescriptionTuple(Convert.ToChar(128 * 64 + 40), @"similar to \n New Line"),
            new T2charDescriptionTuple(Convert.ToChar(128 * 64 + 41), @"similar to \r Carriage Return"),
            new T2charDescriptionTuple(Convert.ToChar(128 * 64 + 42), @"'' empty character"),
            new T2charDescriptionTuple(Convert.ToChar(128 * 64 + 43), @"'' empty character"),
            new T2charDescriptionTuple(Convert.ToChar(128 * 64 + 44), @"'' empty character"),
            new T2charDescriptionTuple(Convert.ToChar(128 * 64 + 45), @"'' empty character"),
            new T2charDescriptionTuple(Convert.ToChar(128 * 64 + 46), @"'' empty character"),
        };

        //                                                  //Con el arreglo anterior se generan los siguientes 2
        //                                                  //      arreglos y se ordenan por el primero.
        private static readonly char[] arrcharNONVISIBLE;
        private static readonly String[] arrstrDESCRIPTION_NONVISIBLE;

        //                                                  //ESTA PENDIENTE ANALIZAR QUE CARACTERES DE LOS 256x256 QUE
        //                                                  //      SON POSIBLES, SON VISIBLES.

        //                                                  //Rangos de otros caracteres que no son visibles los cuales
        //                                                  //      no tienen descripción como los de arriba.
        //                                                  //charType.NONVISIBLE_WITHOUT_DESCRIPTION.
        //                                                  //Los rangos deben estar en orden ascendente, no traslaparse
        //                                                  //      no estar en arrcharKEYBOARD ni en
        //                                                  //      arrcharNONVISIBLE_WITH_DESCRIPTION.
        private static int[,] arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION =
        {
            { 1, 6 },
            { 14, 31 },
            { 127,  128 + 31 },
            { 128 + 45, 128 + 45 },
            { 128 * 5 + 94, 128 * 5 + 94 },
            //                                              //Distorcionan el despliegue en forma extraña, parece que 
            //                                              //      tiene efecto sobre lo que se desplego ANTES.
            { 128 * 5 + 101, 128 * 5 + 101 }, { 128 * 5 + 103, 128 * 5 + 104 },
            { 128 * 6, 128 * 6 + 17 }, { 128 * 6 + 19, 128 * 6 + 78 }, { 128 * 6 + 80, 128 * 6 + 111 },
            { 128 * 9 + 3, 128 * 9 + 6 },
            { 128 * 11 + 62, 128 * 11 + 62 }, { 128 * 11 + 64, 128 * 11 + 64 }, { 128 * 11 + 67, 128 * 11 + 67 }, 
            { 128 * 11 + 70, 128 * 11 + 70 }, { 128 * 11 + 80, 128 * 11 + 106 }, { 128 * 11 + 112, 128 * 11 + 116 },
            { 128 * 12, 128 * 12 + 3 }, { 128 * 12 + 11, 128 * 12 + 11 }, { 128 * 12 + 13, 128 * 12 + 13 }, 
            { 128 * 12 + 27, 128 * 12 + 27 }, { 128 * 12 + 30, 128 * 12 + 31 }, { 128 * 12 + 33, 128 * 12 + 58 },
            { 128 * 12 + 64, 128 * 12 + 74 }, { 128 * 12 + 109, 128 * 12 + 111 }, { 128 * 12 + 113, 128 * 12 + 127 },
            { 128 * 13, 128 * 13 + 85 }, { 128 * 13 + 93, 128 * 13 + 93 }, { 128 * 13 + 101, 128 * 13 + 102 },
            { 128 * 13 + 110, 128 * 13 + 111 }, { 128 * 13 + 122, 128 * 13 + 127 },
            { 128 * 14, 128 * 14 + 13 }, { 128 * 14 + 16, 128 * 14 + 16 }, { 128 * 14 + 18, 128 * 14 + 47 },
            { 128 * 14 + 77, 128 * 14 + 109 },
            { 128 * 15, 128 * 15 + 37 }, { 128 * 15 + 49, 128 * 15 + 49 },
            //                                              //Non printable character
            { '\xD800', '\xFFFF' },
        };

        //--------------------------------------------------------------------------------------------------------------
        /*STATIC VARIABLES*/

        //                                                  //Object previously processed in other strTo execution.
        private static List</*NSTD*/Object/*END-NSTD*/> lstobjPreviouslyProcessed;

        //--------------------------------------------------------------------------------------------------------------
        /*SUPPORT METHODS FOR STATIC CONSTRUCTOR*/

        //--------------------------------------------------------------------------------------------------------------
        private static void subPrepareConstantsForStrTo(    //Método de apoyo llamado en constructor estático. 
            //                                              //Prepara las constantes para poder utilizarlas.
            //                                              //1. Prepara arrcharKEYBOARD.
            //                                              //2. Prepara arrcharNONVISIBLE_WITH_DESCRIPTION y 
            //                                              //      arrstrDESCRIPTION_NONVISIBLE_WITH_DESCRIPTION.

            out char[] arrcharKEYBOARD_O,
            out char[] arrcharNONVISIBLE_O,
            out String[] arrstrDESCRIPTION_NONVISIBLE_O
            )
        {
            Test.subInitializeLstobjPreviouslyProcessed();

            //                                              //Prepara arrcharKEYBOARD.
            arrcharKEYBOARD_O = strCHAR_KEYBOARD.ToCharArray();
            Array.Sort(arrcharKEYBOARD_O);

            //                                              //Verifica que no haya caracteres duplicados.
            for (int intI = 1; intI < arrcharKEYBOARD.Length; intI = intI + 1)
            {
                if (
                    //                                      //Esta duplicado.
                    arrcharKEYBOARD[intI] == arrcharKEYBOARD[intI - 1]
                    )
                    Tools.subAbort(Test.strTo(arrcharKEYBOARD, "arrcharKEYBOARD") + ", " +
                        Test.strTo(arrcharKEYBOARD[intI], "arrcharKEYBOARD[" + intI + "]") +
                        " duplicated character");
            }

            //                                              //Prepara arrcharNONVISIBLE_WITH_DESCRIPTION y
            //                                              //      arrstrDESCRIPTION_NONVISIBLE_WITH_DESCRIPTION.

            arrcharNONVISIBLE_O = new char[arrt2charNONVISIBLE_WITH_DESCRIPTION.Length];
            arrstrDESCRIPTION_NONVISIBLE_O = new String[arrt2charNONVISIBLE_WITH_DESCRIPTION.Length];
            for (int intI = 0; intI < arrcharNONVISIBLE_O.Length; intI = intI + 1)
            {
                arrcharNONVISIBLE_O[intI] = arrt2charNONVISIBLE_WITH_DESCRIPTION[intI].charChar;
                arrstrDESCRIPTION_NONVISIBLE_O[intI] = arrt2charNONVISIBLE_WITH_DESCRIPTION[intI].strDescription;
            }

            Array.Sort(arrcharNONVISIBLE_O, arrstrDESCRIPTION_NONVISIBLE_O);

            //                                              //Verifica que no haya caracteres duplicados.
            for (int intI = 1; intI < arrcharNONVISIBLE.Length; intI = intI + 1)
            {
                if (
                    //                                      //Esta duplicado.
                    arrcharNONVISIBLE[intI] == arrcharNONVISIBLE[intI - 1]
                    )
                    Tools.subAbort(Test.strTo(arrcharNONVISIBLE, "arrcharNONVISIBLE") + ", " +
                        Test.strTo(arrcharNONVISIBLE[intI], "arrcharNONVISIBLE[" + intI + "]") +
                        " duplicated character");
            }

            //                                              //Verifica los rangos.
            for (int intI = 0; intI < arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION.GetLength(0); intI = intI + 1)
            {
                //                                          //Verifica que los rangos sean válidos.
                const int intMaximo = (int)'\xFFFF';//256 * 256 - 1;
                if (
                    (arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 0] < 0) ||
                    (arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 0] > intMaximo)
                    )
                    Tools.subAbort(
                        Test.strTo(arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 0],
                            "arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[" + intI + ", 0]") +
                        " should be between  0-" + intMaximo);
                if (
                    (arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 1] < 0) ||
                    (arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 1] > intMaximo)
                    )
                    Tools.subAbort(
                        Test.strTo(arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 1],
                            "arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[" + intI + ", 1]") +
                        " should be between  0-" + intMaximo);
                if (
                    arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 0] >
                        arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 1]
                    )
                    Tools.subAbort(
                        Test.strTo(arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 0],
                            "arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[" + intI + ", 0]") + " should be <= " +
                        Test.strTo(arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 1],
                            "arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[" + intI + ", 1]"));
                if (
                    //                                      //No es el primer rango.
                    (intI > 0) &&
                    //                                      //El rango NO ES ascendente.
                    (arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 0] <=
                        arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI - 1, 1])
                    )
                    Tools.subAbort(
                        Test.strTo(arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 0],
                            "arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[" + intI + ", 0]") + " should be > " +
                        Test.strTo(arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI - 1, 1],
                            "arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[" + (intI - 1) + ", 1]"));
            }

            //                                              //Existen 4 conjuntos de caracteres.

            //                                              //El conjunto KEYBOARD no debe estar en los conjuntos
            //                                              //      NONVISIBLE ni en
            //                                              //      NONVISIBLE_WITHOUT_DESCRIPTION.
            for (int intI = 0; intI < strCHAR_KEYBOARD.Length; intI = intI + 1)
            {
                //                                          //Verifica si esta en NONVISIBLE_WITH_DESCRIPTION.
                if (
                    Array.BinarySearch(arrcharNONVISIBLE, strCHAR_KEYBOARD[intI]) >= 0
                    )
                    Tools.subAbort(Test.strTo(strCHAR_KEYBOARD[intI], "strCHAR_KEYBOARD[" + intI + "]") +
                        " is in " + Test.strTo(arrcharNONVISIBLE, "arrcharNONVISIBLE"));
                //                                          //Verifica si esta en NONVISIBLE_WITHOUT_DESCRIPTION.
                if (
                    Test.boolIsNonVisibleWithoutDescription(strCHAR_KEYBOARD[intI])
                    )
                    Tools.subAbort(Test.strTo(strCHAR_KEYBOARD[intI], "strCHAR_KEYBOARD[" + intI + "]") +
                        " is in" +
                        Test.strTo(Test.arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION,
                            "arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION"));
            }

            //                                              //El conjunto NONVISIBLE no debe estar en el conjunto
            //                                              //      NONVISIBLE_WITHOUT_DESCRIPTION.
            for (int intI = 0; intI < arrcharNONVISIBLE.Length; intI = intI + 1)
            {
                //                                          //Verifica si esta en NONVISIBLE_WITH_DESCRIPTION.
                if (
                    Test.boolIsNonVisibleWithoutDescription(arrcharNONVISIBLE_O[intI])
                    )
                    Tools.subAbort(Test.strTo(arrcharNONVISIBLE_O[intI], "arrcharNONVISIBLE_O[" + intI + "]") +
                        " is in " + Test.strTo(Test.arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION,
                        "Test.arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION"));
            }
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static bool boolIsNonVisibleWithoutDescription(
            //                                              //Verifica si un caracter es no visible sin descripción.
            //                                              //bool, true si es no visible sin descripción.

            //                                              //Caracter que se desea verificar.
            char charAVerificar_I
            )
        {
            //                                              //Extrae el número del caracter.
            int intChar = (int)charAVerificar_I;

            //                                              //Busca el rango donde pudiera estar incluido.
            int intI = 0;
            /*UNTIL-DO*/
            while (!(
                //                                          //Ya no hay rangos.
                (intI >= arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION.GetLength(0)) ||
                //                                          //El caracter a verificar puede estar en este rango.
                (intChar <= arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 1])
                ))
            {
                intI = intI + 1;
            }

            return (
                //                                          //Esta posicionado en un rango.
                (intI < arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION.GetLength(0)) &&
                //                                          //El caracter a verificar esta incluido en este rango.
                (intChar >= arr2intRANGE_CHAR_NONVISIBLE_WITHOUT_DESCRIPTION[intI, 0])
                );
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static TestchartypeEnum testchartypeKeyboardOrEtc(
            //                                              //Revisa un caracter para determinar su tipo.

            //                                              //Caracter que debrerá ser revisado.
            char charARevise_I
            )
        {
            TestchartypeEnum testchartypeKeyboardOrEtc;

            /*CASE*/
            if (
                //                                          //Es caracter del teclado.
                Array.BinarySearch(arrcharKEYBOARD, charARevise_I) >= 0
                )
            {
                testchartypeKeyboardOrEtc = TestchartypeEnum.KEYBOARD;
            }
            else if (
                Array.BinarySearch(arrcharNONVISIBLE, charARevise_I) >= 0
                )
            {
                testchartypeKeyboardOrEtc = TestchartypeEnum.NONVISIBLE_WITH_DESCRIPTION;
            }
            else if (
                Test.boolIsNonVisibleWithoutDescription(charARevise_I)
                )
            {
                testchartypeKeyboardOrEtc = TestchartypeEnum.NONVISIBLE_WITHOUT_DESCRIPTION;
            }
            else
            {
                testchartypeKeyboardOrEtc = TestchartypeEnum.VISIBLE_NONKEYBOARD;
            }
            /*END-CASE*/

            return testchartypeKeyboardOrEtc;
        }

        //--------------------------------------------------------------------------------------------------------------
        /*SHARED METHODS*/

        //--------------------------------------------------------------------------------------------------------------
        public static void subInitializeLstobjPreviouslyProcessed(
            //                                              //Reset list of previously porcessed
            )
        {
            Test.lstobjPreviouslyProcessed = new List</*NSTD*/Object/*END-NSTD*/>();
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Test.strTo Set of Methods to Display Object Info*/
        //--------------------------------------------------------------------------------------------------------------
        public static String strTo(                         //Prepare for SHORT display.
            //                                              //The strategy is:
            //                                              //1. 2 strTo methods (this and next with obj_I parameter)
            //                                              //      will handle all types except generic containing
            //                                              //      bclass, btuple and enun.
            //                                              //2. 2 strTo methods with 3 paramenters will handle 1
            //                                              //      argument generic containing bclass, btuple and enun.
            //                                              //3. 2 strTo methods with 4 paramenters will handle
            //                                              //      dicbclass, dicbtuple and dicenun.
            //                                              //4. 2 strTo methods with 4 paramenters will handle
            //                                              //      kvpbclass, kvpbtuple and kvpenun.
            //                                              //5a. Each one of the pair of strTo methods call a
            //                                              //      strToSupportXxxxx private method (4 methods) to
            //                                              //      handle most checks needed, process null values and
            //                                              //      call strToSharedYyyyy private methods to generate
            //                                              //      the information requested.
            //                                              //5b. "primitives" are not easy to process (they require an
            //                                              //      specific method for each one), to solve this
            //                                              //      problem, "primitives" will be boxed using Oint,
            //                                              //      Olong, ... boxing clases, this will be done in the
            //                                              //      strToSupportAnyType method.

            //                                              //str, info to display

            //                                              //Read strToSupportAnyType method for paramenters
            //                                              //      description
            /*NSTD*/Object/*END-NSTD*/ obj_I,
            TestoptionEnum testoptionSHORT_I
            )
        {
            if (
                testoptionSHORT_I != TestoptionEnum.SHORT
                )
                Tools.subAbort(Test.strTo(testoptionSHORT_I, "testoptionSHORT_I") + " should be SHORT");

            return Test.strToSupportAnyType(obj_I, testoptionSHORT_I, null);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public static String strTo(                         //Prepare for FULL display.

            //                                              //str, info to display

            //                                              //Read strToSupportAnyType method for paramenters
            //                                              //      description
            /*NSTD*/Object/*END-NSTD*/ obj_I,
            String strText_I
            )
        {
            if (
                strText_I == null
                )
                Tools.subAbort(Test.strTo(strText_I, "strText_I") + " should have a value");

            return Test.strToSupportAnyType(obj_I, TestoptionEnum.FULL, strText_I);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strToSupportAnyType(          //Continue preparation for display.

            //                                              //str, info to display

            //                                              //Any standard type except generic types containing bclass,
            //                                              //      btuple or enum types (those require a transformation
            //                                              //      before calling strTo method with 3 or 4 paramenters)
            /*NSTD*/Object/*END-NSTD*/ obj_I,
            //                                              //SHORT or FULL display
            TestoptionEnum testoptionOption_I,
            //                                              //Variable name of the object.
            String strText_I
            )
        {
            String strToSupportAnyType;
            if (
                obj_I == null
                )
            {
                if (
                    testoptionOption_I == TestoptionEnum.SHORT
                    )
                {
                    strToSupportAnyType = "null";
                }
                else
                {
                    strToSupportAnyType = strText_I + "(null)";
                }
            }
            else
            {
                //                                          //Abort if not a valid type
                Test.subVerifyAnyType(obj_I);

                //                                          //Do the boxing.
                /*NSTD*/Object/*END-NSTD*/ objMain;
                /*NSTD*/Object/*END-NSTD*/ objKey;
                Test.subfunConvertAndBox(out objMain, out objKey, obj_I);

                //                                          //Call required strToSharedYyyyy
                Type typeObj = obj_I.GetType();
                /*CASE*/
                if (
                    typeObj./*END-NSTD*/IsArray/*END-NSTD*/
                    )
                {
                    //                                      //Is arrobj[], arrobj[,] or arrobj[, ,].
                    //                                      //All contents are boxed primitives, simple and
                    //                                      //      system types, bclass, btuple and enum

                    int intRank = typeObj./*NSTD*/GetArrayRank()/*END-NSTD*/;
                    /*CASE*/
                    if (
                        intRank == 1
                        )
                    {
                        //                                  //Is arrobj[], call with 3 paramenters
                        strToSupportAnyType = Test.strFormatArrOrOneArgumentGeneric(
                            (/*NSTD*/Object[]/*END-NSTD*/)objMain, testoptionOption_I, strText_I, obj_I);
                    }
                    else if (
                        intRank == 2
                        )
                    {
                        //                                  //Is arrobj[,]
                        strToSupportAnyType = Test.strFormatArr2Main((/*NSTD*/Object[,]/*END-NSTD*/)objMain,
                            testoptionOption_I, strText_I, obj_I);
                    }
                    else
                    {
                        //                                  //Is arrobj[, ,]
                        strToSupportAnyType = Test.strFormatArr3Main((/*NSTD*/Object[, ,]/*END-NSTD*/)objMain,
                            testoptionOption_I, strText_I, obj_I);
                    }
                    /*END-CASE*/
                }
                else if (
                    typeObj./*END-NSTD*/IsGenericType/*END-NSTD*/
                    )
                {
                    //                                      //Is 1 or 2 arguments.
                    //                                      //All contents are boxed primitives, simple and
                    //                                      //      system types

                    if (
                        //                                  //Is List<Object>, ...
                        typeObj.Name.EndsWith("`1")
                        )
                    {
                        //                                  //lstobj, ... were converted to arrobj
                        strToSupportAnyType = Test.strFormatArrOrOneArgumentGeneric(
                            (/*NSTD*/Object[]/*END-NSTD*/)objMain, testoptionOption_I, strText_I, obj_I);
                    }
                    else
                    {
                        //                                  //Is Dictionary<String, Object> or
                        //                                  //      KeyValuePair<String,_Object>

                        if (
                            typeObj.Name == Test.strGENERIC_DICTIONARY_TYPE
                            )
                        {
                            //                              //dicobj was convertes to arrstr and arrobj
                            strToSupportAnyType = Test.strFormatDicMain(
                                (/*NSTD*/Object[]/*END-NSTD*/)objMain, (String[])objKey, testoptionOption_I, strText_I,
                                obj_I);
                        }
                        else
                        {
                            //                              //kvpobj was convertes to str and obj
                            strToSupportAnyType = Test.strFormatKvpMain(objMain, (String)objKey, testoptionOption_I,
                                strText_I, obj_I);
                        }
                    }
                }
                else
                {
                    //                                      //Is single type
                    strToSupportAnyType = Test.strFormatSingle(objMain, testoptionOption_I, strText_I);
                }
                /*END-CASE*/
            }

            return strToSupportAnyType;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subVerifyAnyType(               //Verify it is standard type except a generic type
            //                                              //      containing bclass, btuple or enum type

            //                                              //Read strToSupportAnyType method for paramenters
            //                                              //      description
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            Type typeObj = obj_I.GetType();
            if (
                !Test.boolIsStandard(typeObj, false)
                )
                Tools.subAbort(Test.strTo(typeObj, "obj_I.GetType") + " is not an standard type");

            //                                              //If generic type, other verifications are required
            if (
                typeObj./*NSTD*/IsGenericType/*END-NSTD*/
                )
            {
                //                                          //More verifications are needed

                //                                          //Get argument type contained
                Type[] arrtypeArgument = typeObj./*NSTD*/GetGenericArguments()/*END-NSTD*/;
                Type typeArgument = arrtypeArgument[arrtypeArgument.Length - 1];

                if (
                    //                                      //Is bclass
                    typeArgument == typeof(BclassBaseClassAbstract) ||
                        typeArgument.IsSubclassOf(typeof(BclassBaseClassAbstract)) ||
                    //                                      //Is btuple
                    typeArgument == typeof(BtupleBaseTupleAbstract) ||
                        typeArgument.IsSubclassOf(typeof(BtupleBaseTupleAbstract)) ||
                    //                                      //Is enum
                    typeArgument == typeof(Enum) || typeArgument.IsSubclassOf(typeof(Enum)) ||
                    //                                      //Is Exception
                    typeArgument == typeof(Exception) || typeArgument.IsSubclassOf(typeof(Exception))
                    )
                    Tools.subAbort(Test.strTo(typeObj, "obj_I.GetType") +
                        " generic types containing bclass, btuple or enum are not valid in ths method, " +
                        "a 3 or 4 paramenters strTo method should be called instead");
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        public static String strTo(                         //Prepare for SHORT display.

            //                                              //str, info to display

            //                                              //Read strToSupportOneArgumentGeneric method for paramenters
            //                                              //      description
            /*NSTD*/Object[]/*END-NSTD*/ arrobj_I,
            TestoptionEnum testoptionSHORT_I,
            /*NSTD*/Object/*END-NSTD*/ objOneArgumentGeneric_I
            )
        {
            if (
                testoptionSHORT_I != TestoptionEnum.SHORT
                )
                Tools.subAbort(Test.strTo(testoptionSHORT_I, "testoptionSHORT_I") + " should be SHORT");

            return Test.strToSupportOneArgumentGeneric(arrobj_I, testoptionSHORT_I, null, objOneArgumentGeneric_I);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public static String strTo(                         //Prepare for FULL display.

            //                                              //str, info to display

            //                                              //Read strToSupportOneArgumentGeneric method for paramenters
            //                                              //      description
            /*NSTD*/Object[]/*END-NSTD*/ arrobj_I,
            String strText_I,
            /*NSTD*/Object/*END-NSTD*/ objOneArgumentGeneric_I
            )
        {
            if (
                strText_I == null
                )
                Tools.subAbort(Test.strTo(strText_I, "strText_I") + " should have a value");

            return Test.strToSupportOneArgumentGeneric(arrobj_I, TestoptionEnum.FULL, strText_I,
                objOneArgumentGeneric_I);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strToSupportOneArgumentGeneric(
            //                                              //Continue preparation for display.

            //                                              //str, info to display

            //                                              //arrstr, arrbclass, arrbtuple or arrenum.
            /*NSTD*/Object[]/*END-NSTD*/ arrobj_I,
            //                                              //SHORT or FULL display
            TestoptionEnum testoptionOption_I,
            //                                              //Variable name of the one argument generic.
            String strText_I,
            //                                              //lstbclass, lstbtuple or lstenum, queuebclass, ...
            //                                              //Main should contain str or the type (or subtype)
            //                                              //      contained in one argument generic.
            /*NSTD*/Object/*END-NSTD*/ objOneArgumentGeneric_I
            )
        {
            String strToSupportOneArgumentGeneric;
            if (
                objOneArgumentGeneric_I == null
                )
            {
                if (
                    testoptionOption_I == TestoptionEnum.SHORT
                    )
                {
                    strToSupportOneArgumentGeneric = "null";
                }
                else
                {
                    strToSupportOneArgumentGeneric = strText_I + "(null)";
                }
            }
            else
            {
                //                                          //Abort if both parameters are not consistent.
                Test.subVerifyOneArgumentGeneric(arrobj_I, objOneArgumentGeneric_I);

                strToSupportOneArgumentGeneric = Test.strFormatArrOrOneArgumentGeneric(arrobj_I, testoptionOption_I,
                    strText_I, objOneArgumentGeneric_I);
            }

            return strToSupportOneArgumentGeneric;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subVerifyOneArgumentGeneric(    //Verify it is a standard one argument generic type
            //                                              //      containing bclass, btuple or enum.

            //                                              //Read strToSupportOneArgumentGeneric method for paramenters
            //                                              //      description
            /*NSTD*/Object[]/*END-NSTD*/ arrobj_I,
            /*NSTD*/Object/*END-NSTD*/ objOneArgumentGeneric_I
            )
        {
            Type typeOneArgumentGeneric = objOneArgumentGeneric_I.GetType();
            //                                              //The generic should be List, ... (any standard 1
            //                                              //      argument generic type).
            if (!(
                typeOneArgumentGeneric./*END-NSTD*/IsGenericType/*END-NSTD*/ &&
                typeOneArgumentGeneric.Name.EndsWith("`1")
                ))
                Tools.subAbort(Test.strTo(typeOneArgumentGeneric, "objOneArgumentGeneric_I.GetType") +
                    " should be standard one argument generic type");

            Type typeArgument = typeOneArgumentGeneric./*NSTD*/GetGenericArguments()/*END-NSTD*/[0];
            if (!(
                //                                          //Is bclass
                typeArgument == typeof(BclassBaseClassAbstract) ||
                    typeArgument.IsSubclassOf(typeof(BclassBaseClassAbstract)) ||
                //                                          //Is btuple
                typeArgument == typeof(BtupleBaseTupleAbstract) ||
                    typeArgument.IsSubclassOf(typeof(BtupleBaseTupleAbstract)) ||
                //                                          //Is enum
                typeArgument == typeof(Enum) || typeArgument.IsSubclassOf(typeof(Enum)) ||
                //                                          //Is Exception
                    typeArgument == typeof(Exception) || typeArgument.IsSubclassOf(typeof(Exception))
                ))
                Tools.subAbort(Test.strTo(typeOneArgumentGeneric, "objOneArgumentGeneric_I.GetType") +
                    " should be generic type containing bclass, btuple or enum");

            if (
                arrobj_I == null
                )
                Tools.subAbort(Test.strTo(arrobj_I, "arrobj_I") + " should have a value");

            Type typeElement = arrobj_I.GetType().GetElementType();
            if (!(
                //                                          //Array and generic are compatible
                (typeElement == typeof(String)) || (typeElement == typeArgument)
                ))
                Tools.subAbort(Test.strTo(typeElement, "arrobj_I.GetType.GetElementType") + ", " +
                    Test.strTo(typeArgument, "objOneArgumentGeneric_I.GetType.GetGenericArguments[0]") + ", " +
                    " array and collection are not compatible");
        }

        //--------------------------------------------------------------------------------------------------------------
        public static String strTo(                         //Prepare for SHORT display.

            //                                              //str, info to display

            //                                              //Read strToSupportDic method for paramenters description
            /*NSTD*/Object[]/*END-NSTD*/ arrobjValue_I,
            String[] arrstrKey_I,
            TestoptionEnum testoptionSHORT_I,
            /*NSTD*/Object/*END-NSTD*/ objDicobj_I
            )
        {
            if (
                testoptionSHORT_I != TestoptionEnum.SHORT
                )
                Tools.subAbort(Test.strTo(testoptionSHORT_I, "testoptionSHORT_I") + " should be SHORT");

            return Test.strToSupportDic(arrobjValue_I, arrstrKey_I, testoptionSHORT_I, null, objDicobj_I);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public static String strTo(                         //Prepare for FULL display.

            //                                              //str, info to display

            //                                              //Read strToSupportDic method for paramenters description
            /*NSTD*/Object[]/*END-NSTD*/ arrobjValue_I,
            String[] arrstrKey_I,
            String strText_I,
            /*NSTD*/Object/*END-NSTD*/ objDicobj_I
            )
        {
            if (
                strText_I == null
                )
                Tools.subAbort(Test.strTo(strText_I, "strText_I") + " should have a value");

            return Test.strToSupportDic(arrobjValue_I, arrstrKey_I, TestoptionEnum.FULL, strText_I, objDicobj_I);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strToSupportDic(              //Continue preparation for display.

            //                                              //str, info to display

            //                                              //arrstr, arrbclass, arrbtuple or arrenum
            /*NSTD*/Object[]/*END-NSTD*/ arrobjValue_I,
            //                                              //dic.Keys
            String[] arrstrKey_I,
            //                                              //SHORT or FULL display
            TestoptionEnum testoptionOption_I,
            //                                              //Variable name of the dic.
            String strText_I,
            //                                              //dicbclass, dicbtuple or dicenum.
            //                                              //Value should contain str or be the same type (or subtype)
            //                                              //      contained in dic.
            /*NSTD*/Object/*END-NSTD*/ objDicobj_I
            )
        {
            String strToSupportDic;
            if (
                objDicobj_I == null
                )
            {
                strToSupportDic = "null";
                if (
                    testoptionOption_I == TestoptionEnum.SHORT
                    )
                {
                    strToSupportDic = "null";
                }
                else
                {
                    strToSupportDic = strText_I + "(null)";
                }
            }
            else
            {
                //                                          //Abort if not a valid dic
                Test.subVerifyDic(arrstrKey_I, arrobjValue_I, objDicobj_I);

                strToSupportDic = Test.strFormatDicMain(arrobjValue_I, arrstrKey_I, testoptionOption_I, strText_I,
                    objDicobj_I);
            }

            return strToSupportDic;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subVerifyDic(                   //Verify it is a valid dic.

            //                                              //Read strToSupportDic method for paramenters description
            String[] arrstrKey_I,
            /*NSTD*/Object[]/*END-NSTD*/ arrobjValue_I,
            /*NSTD*/Object/*END-NSTD*/ objDicobj_I
            )
        {
            Type typeDicobj = objDicobj_I.GetType();
            if (
                typeDicobj.Name != Test.strGENERIC_DICTIONARY_TYPE
                )
                Tools.subAbort(Test.strTo(typeDicobj, "objDicobj_I.GetType") + " should be dictionary");

            Type typeArgument = typeDicobj./*NSTD*/GetGenericArguments()/*END-NSTD*/[1];
            if (!(
                //                                          //Is bclass
                typeArgument == typeof(BclassBaseClassAbstract) ||
                    typeArgument.IsSubclassOf(typeof(BclassBaseClassAbstract)) ||
                //                                          //Is btuple
                typeArgument == typeof(BtupleBaseTupleAbstract) ||
                    typeArgument.IsSubclassOf(typeof(BtupleBaseTupleAbstract)) ||
                //                                          //Is enum
                typeArgument == typeof(Enum) || typeArgument.IsSubclassOf(typeof(Enum)) ||
                //                                          //Is Exception
                    typeArgument == typeof(Exception) || typeArgument.IsSubclassOf(typeof(Exception))
                ))
                Tools.subAbort(Test.strTo(typeDicobj, "typeDicobj.GetType") +
                    " should be dictionary type containing bclass, btuple or enum");

            if (
                arrstrKey_I == null
                )
                Tools.subAbort(Test.strTo(arrstrKey_I, "arrstrKey_I") + " should have a value");

            if (
                arrobjValue_I == null
                )
                Tools.subAbort(Test.strTo(arrobjValue_I, "arrobjValue_I") + " should have a value");

            //                                              //Array should contains str or the same type of Directory
            Type typeValueElement = arrobjValue_I.GetType().GetElementType();
            if (!(
                (typeValueElement == typeof(String)) || (typeValueElement == typeArgument)
                ))
                Tools.subAbort(Test.strTo(arrobjValue_I.GetType(), "arrobjValue_I.GetType") + ", " +
                    Test.strTo(typeDicobj, "objDicobj_I.GetType") + " array and dictionary are not compatible");

            if (
                arrstrKey_I.Length != arrobjValue_I.Length
                )
                Tools.subAbort(Test.strTo(Test.strGetObjId(arrstrKey_I), "arrstrKey_I.strGetObjId") + ", " +
                    Test.strTo(Test.strGetObjId(arrobjValue_I), "arrobjValue_I.strGetObjId") +
                    " both arrays should be the same size");
        }

        //--------------------------------------------------------------------------------------------------------------
        public static String strTo(                         //Prepare for SHORT display.

            //                                              //str, info to display

            //                                              //Read strToSupportKey method for paramenters description
            String strKey_I,
            /*NSTD*/Object/*END-NSTD*/ objValue_I,
            TestoptionEnum testoptionSHORT_I,
            /*NSTD*/Object/*END-NSTD*/ objKvpobj_I
            )
        {
            if (
                testoptionSHORT_I != TestoptionEnum.SHORT
                )
                Tools.subAbort(Test.strTo(testoptionSHORT_I, "testoptionSHORT_I") + " should be SHORT");

            return Test.strToSupportKvp(strKey_I, objValue_I, testoptionSHORT_I, null, objKvpobj_I);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public static String strTo(                         //Prepare for FULL display.

            //                                              //str, info to display

            //                                              //Read strToSupportKvp method for paramenters description
            String strKey_I,
            /*NSTD*/Object/*END-NSTD*/ objValue_I,
            String strText_I,
            /*NSTD*/Object/*END-NSTD*/ objKvpobj_I
            )
        {
            if (
                strText_I == null
                )
                Tools.subAbort(Test.strTo(strText_I, "strText_I") + " should have a value");

            return Test.strToSupportKvp(strKey_I, objValue_I, TestoptionEnum.FULL, strText_I, objKvpobj_I);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strToSupportKvp(              //Continue preparation for display.

            //                                              //str, info to display

            //                                              //kvp.Key
            String strKey_I,
            //                                              //kvp.Value (can be null)
            /*NSTD*/Object/*END-NSTD*/ objValue_I,
            //                                              //SHORT or FULL display
            TestoptionEnum testoptionOption_I,
            //                                              //Variable name of the kvp.
            String strText_I,
            //                                              //kvpbclass, kvpbtuple or kvpenum.
            /*NSTD*/Object/*END-NSTD*/ objKvpobj_I
            )
        {
            String strToSupportKvp;
            if (
                objKvpobj_I == null
                )
            {
                if (
                    testoptionOption_I == TestoptionEnum.SHORT
                    )
                {
                    strToSupportKvp = "null";
                }
                else
                {
                    strToSupportKvp = strText_I + "(null)";
                }
            }
            else
            {
                //                                          //Abort if not a valid kvp
                Test.subVerifyKvp(objValue_I, strKey_I, TestoptionEnum.FULL, strText_I, objKvpobj_I);

                strToSupportKvp = Test.strFormatKvpMain(objValue_I, strKey_I, testoptionOption_I, strText_I,
                    objKvpobj_I);
            }

            return strToSupportKvp;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subVerifyKvp(                   //Verify it is a valid kvp.

            //                                              //Read strToSupportKvp method for paramenters description
            /*NSTD*/Object/*END-NSTD*/ objValue_I,
            String strKey_I,
            TestoptionEnum testoptionOption_I,
            String strText_I,
            /*NSTD*/Object/*END-NSTD*/ objKvpobj_I
            )
        {
            Type typeKvpobj = objKvpobj_I.GetType();
            if (
                typeKvpobj.Name != Test.strGENERIC_KEYVALUEPAIR_TYPE
                )
                Tools.subAbort(Test.strTo(typeKvpobj, "objKvpobj_I.GetType") + " should be KeyValuePair");

            Type typeArgument = typeKvpobj./*NSTD*/GetGenericArguments/*END-NSTD*/()[1];
            if (!(
                //                                          //Is bclass
                typeArgument == typeof(BclassBaseClassAbstract) ||
                    typeArgument.IsSubclassOf(typeof(BclassBaseClassAbstract)) ||
                //                                          //Is btuple
                typeArgument == typeof(BtupleBaseTupleAbstract) ||
                    typeArgument.IsSubclassOf(typeof(BtupleBaseTupleAbstract)) ||
                //                                          //Is enum
                typeArgument == typeof(Enum) || typeArgument.IsSubclassOf(typeof(Enum)) ||
                //                                          //Is Exception
                    typeArgument == typeof(Exception) || typeArgument.IsSubclassOf(typeof(Exception))
                ))
                Tools.subAbort(Test.strTo(typeKvpobj, "typeKvpobj.GetType") +
                    " should be dictionary type containing bclass, btuple or enum");

            //                                              //Value could be null.
            if (
                //                                          //Value exists
                objValue_I != null
                )
            {
                Type typeValue = objValue_I.GetType();
                if (!(
                    //                                      //Value and KeyValuePair are compatible
                    (typeValue == typeArgument) || typeValue.IsSubclassOf(typeArgument)
                    ))
                    Tools.subAbort(Test.strTo(typeValue, "objValue_I.GetType") + ", " +
                        Test.strTo(typeKvpobj, "objKvpobj_I.GetType") + " value and KeyValuePair are not compatible");
            }
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Test.Types Set of Methods to Analize Types*/
        //--------------------------------------------------------------------------------------------------------------
        /*CONSTANTS*/

        //                                                  //Towa's standard primitives
        private static readonly String[,] arr2strPRIMITIVE_TYPE_AND_PREFIX = {
            //                                              //TO ADD NEW PRIMARY TYPES:
            //                                              //a. Add an entry in this array (standard prefix xxxx).
            //                                              //b. Add a method subfunConvertAndBoxXxxx, similar to
            //                                              //      subfunConvertAndBoxTs).
            //                                              //c. Add a method strAnalizeAndFormatXxxx, similar to
            //                                              //      strAnalizeAndFormatTs).
            //                                              //d. Add case branch in method
            //                                              //      subfunConvertAndBoxPrimitive.
            //                                              //e. Add case branch in methodstrAnalizeAndFormatBbox.
 
            { "Int32", "int" }, { "Int64", "long" }, { "Boolean", "bool" }, { "Char", "char" }, { "Double", "num" },
            //                                              //C# structures should be handled like primitives
            { "DateTime", "ts" },
            };
        //                                                  //Both arrays order by first.
        public static readonly String[] arrstrPRIMITIVE_TYPE;
        public static readonly String[] arrstrPRIMITIVE_PREFIX;

        //                                                  //Towa's standard system types
        private static readonly String[,] arr2strSYSTEM_TYPE_AND_PREFIX = { 
            //                                              //TO ADD NEW SYSTEM TYPES:
            //                                              //a. Add an entry in this array (standard prefix yyyyy).
            //                                              //b. Add a method subfunConvertYyyyy, similar to
            //                                              //      subfunConvertSysdir).
            //                                              //c. Add a method strAnalizeAndFormatYyyyy, similar to
            //                                              //      strAnalizeAndFormatSysdir).
            //                                              //d. Add case branch in method subfunConvertSystemType.
            //                                              //e. Add case branch in method
            //                                              //      strAnalizeAndFormatSystemType.

            //                                              //String should be handled like system tyes.
            { "String", "str" },
            //                                              //System types
            { "RuntimeType", "type" }, { "DirectoryInfo", "Sysdir" }, { "FileInfo", "Sysfile" },
            { "StreamReader", "Syssr" }, { "StreamWriter", "Syssw" },
            };

        //                                                  //Both arrays order by first.
        public static readonly String[] arrstrSYSTEM_TYPE;
        public static readonly String[] arrstrSYSTEM_PREFIX;

        //                                                  //Towa's standard other types
        private const String strGENERIC_LIST_TYPE = "List`1";
        private const String strGENERIC_QUEUE_TYPE = "Queue`1";
        private const String strGENERIC_STACK_TYPE = "Stack`1";
        private const String strGENERIC_DICTIONARY_TYPE = "Dictionary`2";
        private const String strGENERIC_KEYVALUEPAIR_TYPE = "KeyValuePair`2";
        private static readonly String[,] arr2strGENERIC_TYPE_AND_PREFIX = { 
            { strGENERIC_LIST_TYPE, "lst" }, { strGENERIC_QUEUE_TYPE, "queue" }, { strGENERIC_STACK_TYPE, "stack" },
            { strGENERIC_DICTIONARY_TYPE, "dic" }, { strGENERIC_KEYVALUEPAIR_TYPE, "kvp" }, 
            };

        //                                                  //Both arrays order by first.
        public static readonly String[] arrstrGENERIC_TYPE;
        public static readonly String[] arrstrGENERIC_PREFIX;

        //--------------------------------------------------------------------------------------------------------------
        /*SUPPORT METHODS FOR STATIC CONSTRUCTOR*/

        //--------------------------------------------------------------------------------------------------------------
        private static void subPrepareConstantTypes(        //Order and varify constants. 

            out String[] arrstrPRIMITIVE_TYPE_O,
            out String[] arrstrPRIMITIVE_PREFIX_O,
            out String[] arrstrSYSTEM_TYPE_O,
            out String[] arrstrSYSTEM_PREFIX_O,
            out String[] arrstrGENERIC_TYPE_O,
            out String[] arrstrGENERIC_PREFIX_O
            )
        {
            //                                              //Order arr2strPRIMITIVE_TYPE_AND_PREFIX.
            arrstrPRIMITIVE_TYPE_O = new String[Test.arr2strPRIMITIVE_TYPE_AND_PREFIX.GetLength(0)];
            arrstrPRIMITIVE_PREFIX_O = new String[arrstrPRIMITIVE_TYPE_O.Length];

            for (int intI = 0; intI < arrstrPRIMITIVE_TYPE_O.Length; intI = intI + 1)
            {
                arrstrPRIMITIVE_TYPE_O[intI] = Test.arr2strPRIMITIVE_TYPE_AND_PREFIX[intI, 0];
                arrstrPRIMITIVE_PREFIX_O[intI] = Test.arr2strPRIMITIVE_TYPE_AND_PREFIX[intI, 1];
            }

            Array.Sort(arrstrPRIMITIVE_TYPE_O, arrstrPRIMITIVE_PREFIX_O);

            for (int intI = 1; intI < arrstrPRIMITIVE_TYPE_O.Length; intI = intI + 1)
            {
                if (
                    //                                      //Is duplicated.
                    arrstrPRIMITIVE_TYPE_O[intI] == arrstrPRIMITIVE_TYPE_O[intI - 1]
                    )
                    Tools.subAbort(Test.strTo(arrstrPRIMITIVE_TYPE_O, "arrstrPRIMITIVE_TYPE_O") + ", " +
                        Test.strTo(arrstrPRIMITIVE_TYPE_O[intI], "arrstrPRIMITIVE_TYPE_O[" + intI + "]") +
                        " is duplicated");
            }

            //                                              //Order arr2strSYSTEM_TYPE_AND_PREFIX.
            arrstrSYSTEM_TYPE_O = new String[Test.arr2strSYSTEM_TYPE_AND_PREFIX.GetLength(0)];
            arrstrSYSTEM_PREFIX_O = new String[arrstrSYSTEM_TYPE_O.Length];

            for (int intI = 0; intI < arrstrSYSTEM_TYPE.Length; intI = intI + 1)
            {
                arrstrSYSTEM_TYPE_O[intI] = Test.arr2strSYSTEM_TYPE_AND_PREFIX[intI, 0];
                arrstrSYSTEM_PREFIX_O[intI] = Test.arr2strSYSTEM_TYPE_AND_PREFIX[intI, 1];
            }

            Array.Sort(arrstrSYSTEM_TYPE_O, arrstrSYSTEM_PREFIX_O);

            for (int intI = 1; intI < arrstrSYSTEM_TYPE_O.Length; intI = intI + 1)
            {
                if (
                    //                                      //Is duplicated.
                    arrstrSYSTEM_TYPE_O[intI] == arrstrSYSTEM_TYPE_O[intI - 1]
                    )
                    Tools.subAbort(Test.strTo(arrstrSYSTEM_TYPE_O, "arrstrSYSTEM_TYPE_O") + ", " +
                        Test.strTo(arrstrSYSTEM_TYPE_O[intI], "arrstrSYSTEM_TYPE_O[" + intI + "]") +
                        " is duplicated");
            }

            //                                              //Order arr2strGENERIC_TYPE_AND_PREFIX.
            arrstrGENERIC_TYPE_O = new String[Test.arr2strGENERIC_TYPE_AND_PREFIX.GetLength(0)];
            arrstrGENERIC_PREFIX_O = new String[arrstrGENERIC_TYPE_O.Length];

            for (int intI = 0; intI < arrstrGENERIC_TYPE.Length; intI = intI + 1)
            {
                arrstrGENERIC_TYPE_O[intI] = Test.arr2strGENERIC_TYPE_AND_PREFIX[intI, 0];
                arrstrGENERIC_PREFIX_O[intI] = Test.arr2strGENERIC_TYPE_AND_PREFIX[intI, 1];
            }

            Array.Sort(arrstrGENERIC_TYPE_O, arrstrGENERIC_PREFIX_O);

            for (int intI = 1; intI < arrstrGENERIC_TYPE_O.Length; intI = intI + 1)
            {
                if (
                    //                                      //Is duplicated.
                    arrstrGENERIC_TYPE_O[intI] == arrstrGENERIC_TYPE_O[intI - 1]
                    )
                    Tools.subAbort(Test.strTo(arrstrGENERIC_TYPE_O, "arrstrGENERIC_TYPE_O") + ", " +
                        Test.strTo(arrstrGENERIC_TYPE_O[intI], "arrstrGENERIC_TYPE_O[" + intI + "]") +
                        " is duplicated");
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        /*SHARE METHODS*/

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsStandard(                  //Evaluate if type is standard type.

            //                                              //bool, is valid.

            Type type_I,
            //                                              //true, abort if is not valid.
            bool boolAbort_I
            )
        {
            if (
                type_I == null
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " can not be null");

            bool boolIsStandard;
            /*CASE*/
            if (
                type_I./*NSTD*/IsArray/*END-NSTD*/
                )
            {
                boolIsStandard = Test.boolIsStandardArray(type_I, boolAbort_I);
            }
            else if (
                type_I./*NSTD*/IsGenericType/*END-NSTD*/
                )
            {
                boolIsStandard = Test.boolIsStandardGeneric(type_I, boolAbort_I);
            }
            else
            {
                boolIsStandard = Test.boolIsStandardSingle(type_I, boolAbort_I);
            }
            /*END-CASE*/

            return boolIsStandard;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static bool boolIsStandardSingle(            //Evaluate if type is standard single type.

            //                                              //bool, is valid.

            Type type_I,
            //                                              //true, abort if is not valid.
            bool boolAbort_I
            )
        {
            if (
                type_I == null
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " can not be null");

            bool boolIsStandardSingle;
            /*CASE*/
            if (
                Array.BinarySearch(Test.arrstrPRIMITIVE_TYPE, type_I.Name) >= 0
                )
            {
                boolIsStandardSingle = Test.boolIsStandardPrimitive(type_I, boolAbort_I);
            }
            else if (
                Array.BinarySearch(Test.arrstrSYSTEM_TYPE, type_I.Name) >= 0
                )
            {
                boolIsStandardSingle = Test.boolIsStandardSystem(type_I, boolAbort_I);
            }
            else if (
                (type_I == typeof(BclassBaseClassAbstract)) || type_I.IsSubclassOf(typeof(BclassBaseClassAbstract))
                )
            {
                boolIsStandardSingle = Test.boolIsStandardBclass(type_I, boolAbort_I);
            }
            else if (
                (type_I == typeof(BtupleBaseTupleAbstract)) || type_I.IsSubclassOf(typeof(BtupleBaseTupleAbstract))
                )
            {
                boolIsStandardSingle = Test.boolIsStandardBtuple(type_I, boolAbort_I);
            }
            else if (
                (type_I == typeof(Enum)) || type_I.IsSubclassOf(typeof(Enum))
                )
            {
                boolIsStandardSingle = Test.boolIsStandardEnum(type_I, boolAbort_I);
            }
            else if (
                (type_I == typeof(Exception)) || type_I.IsSubclassOf(typeof(Exception))
                )
            {
                boolIsStandardSingle = Test.boolIsStandardException(type_I, boolAbort_I);
            }
            else
            {
                boolIsStandardSingle = false;

                if (
                    boolAbort_I && !boolIsStandardSingle
                    )
                    Tools.subAbort(Test.strTo(Test.arrstrPRIMITIVE_TYPE, "arrstrPRIMITIVE_TYPE") + ", " +
                        Test.strTo(type_I, "type_I") + " is not an standard primitive type");
            }
            /*END-CASE*/

            return boolIsStandardSingle;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static bool boolIsStandardPrimitive(        //Evaluate if type is standard primitive type.

            //                                              //bool, is valid.

            Type type_I,
            //                                              //true, abort if is not valid.
            bool boolAbort_I
            )
        {
            if (
                type_I == null
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " can not be null");

            bool boolIsStandardPrimitive = (
                //                                          //Is a primitive included in Towa Standard.
                (Array.BinarySearch(Test.arrstrPRIMITIVE_TYPE, type_I.Name) >= 0)
                );

            if (
                boolAbort_I && !boolIsStandardPrimitive
                )
                Tools.subAbort(Test.strTo(Test.arrstrPRIMITIVE_TYPE, "arrstrPRIMITIVE_TYPE") + ", " +
                    Test.strTo(type_I, "type_I") + " is not an standard primitive type");

            return boolIsStandardPrimitive;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static bool boolIsStandardSystem(           //Evaluate if type is standard system type.

            //                                              //bool, is valid.

            Type type_I,
            //                                              //true, abort if is not valid.
            bool boolAbort_I
            )
        {
            bool boolX;
            boolX = type_I.IsAbstract;


            if (
                type_I == null
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " can not be null");

            bool boolIsStandardSimpleOrSystem = (
                Array.BinarySearch(Test.arrstrSYSTEM_TYPE, type_I.Name) >= 0
                );

            if (
                boolAbort_I && !boolIsStandardSimpleOrSystem
                )
                Tools.subAbort(Test.strTo(Test.arrstrSYSTEM_TYPE, "Test.arrstrSYSTEM_TYPE") + ", " +
                    Test.strTo(type_I, "type_I") + " is not an standard system type");

            return boolIsStandardSimpleOrSystem;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static bool boolIsStandardBclass(           //Evaluate if type is standard Bclass.

            //                                              //bool, is valid.

            Type type_I,
            //                                              //true, abort if is not valid.
            bool boolAbort_I
            )
        {
            if (
                type_I == null
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " can not be null");

            bool boolIsStandardBclass = (
                (type_I == typeof(BclassBaseClassAbstract)) || type_I.IsSubclassOf(typeof(BclassBaseClassAbstract))
                );

            if (
                boolAbort_I && !boolIsStandardBclass
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " is not an standard bclass type");

            if (
                //                                          //Is bclass (or subclass of)
                boolIsStandardBclass
                )
            {
                //                                          //It could be abstract or concrete class

                if (
                    type_I.IsAbstract
                    )
                {
                    boolIsStandardBclass = (
                        //                                  //The name has the form: Prefix.....Abstract
                        type_I.Name.EndsWith("Abstract") && (type_I.Name.Length > "Abstract".Length) &&
                            Tools.boolIsLetterUpper(type_I.Name[0])
                        );

                    if (
                        boolAbort_I && boolIsStandardBclass
                        )
                        Tools.subAbort(Test.strTo(type_I, "type_I") +
                            " an standard abstract bclass type should have the form 'Prefix....Abstract'");
                }
                else
                {
                    //                                      //It is concrete class

                    String strNameLower = type_I.Name.ToLower();
                    boolIsStandardBclass = (
                        //                                  //The name has de form: Prefix.... and do not end with
                        //                                  //      Abstract, Enum, Tuple, Interface or Delegate
                        !(strNameLower.EndsWith("abstract") || strNameLower.EndsWith("enum") ||
                            strNameLower.EndsWith("tuple") || strNameLower.EndsWith("interface") ||
                            strNameLower.EndsWith("delegate")) &&
                        Tools.boolIsLetterUpper(type_I.Name[0])
                        );

                    if (
                        boolAbort_I && boolIsStandardBclass
                        )
                        Tools.subAbort(Test.strTo(type_I, "type_I") +
                            " an standard concrete bclass type should have the form 'Prefix....' and" +
                            " can not ends with Abstract, Enum, Tuple, Interface or Delegate (upper or lower letters)");
                }
            }

            return boolIsStandardBclass;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static bool boolIsStandardBtuple(           //Evaluate if type is standard Btuple.

            //                                              //bool, is valid.

            Type type_I,
            //                                              //true, abort if is not valid.
            bool boolAbort_I
            )
        {
            if (
                type_I == null
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " can not be null");

            bool boolIsStandardBtuple = (
                ((type_I == typeof(BtupleBaseTupleAbstract)) || type_I.IsSubclassOf(typeof(BtupleBaseTupleAbstract))) &&
                //                                          //The name has the form: TNprefix...Tuple (at least 3 char
                //                                          //      before "Tuple".
                type_I.Name.EndsWith("Tuple") && (type_I.Name.Length >= ("Tuple".Length + 3)) &&
                    (type_I.Name[0] == 'T') && (type_I.Name[1] >= '1') && (type_I.Name[1] <= '9') &&
                    Tools.boolIsLetterLower(type_I.Name[2])
                );

            if (
                boolAbort_I && !boolIsStandardBtuple
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") +
                    " is not an standard tuple type, also should have the form 'TNprefix...Tuple'");

            return boolIsStandardBtuple;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static bool boolIsStandardEnum(             //Evaluate if type is standard enum type.

            //                                              //bool, is valid.

            Type type_I,
            //                                              //true, abort if is not valid.
            bool boolAbort_I
            )
        {
            if (
                type_I == null
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " can not be null");

            bool boolIsStandardEnum = (
                ((type_I == typeof(Enum)) || type_I.IsSubclassOf(typeof(Enum))) &&
                //                                          //Has the form Prefix...Enum (at least 1 char before
                //                                          //      "Enum").
                type_I.Name.EndsWith("Enum") && (type_I.Name.Length > "Enum".Length) &&
                    Tools.boolIsLetterUpper(type_I.Name[0])
                );

            if (
                boolAbort_I && !boolIsStandardEnum
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") +
                    " is not an standard Enum type, also should have the form 'Prefix...Enum'");

            return boolIsStandardEnum;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static bool boolIsStandardException(        //Evaluate if type is standard Exception type.
            //                                              //There is no much to check.

            //                                              //bool, is valid.

            Type type_I,
            //                                              //true, abort if is not valid.
            bool boolAbort_I
            )
        {
            if (
                type_I == null
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " can not be null");

            bool boolIsStandardException = (
                ((type_I == typeof(Exception)) || type_I.IsSubclassOf(typeof(Exception)))
                );

            if (
                boolAbort_I && !boolIsStandardException
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " is not an standard Exception type");

            return boolIsStandardException;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static bool boolIsStandardArray(            //Evaluate if type is standard array.

            //                                              //bool, is valid.

            Type type_I,
            //                                              //true, abort if is not valid.
            bool boolAbort_I
            )
        {
            if (
                type_I == null
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " can not be null");

            bool boolIsStandardArray = (
                type_I./*NSTD*/IsArray/*END-NSTD*/ &&
                //                                          Is obj[], obj[,] or obj[, ,]
                (type_I./*NSTD*/GetArrayRank()/*END-NSTD*/ <= 3)
                );

            if (
                boolAbort_I && !boolIsStandardArray
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " is not an standard array type");

            if (
                boolIsStandardArray
                )
            {
                //                                          //The "element" of an array should be standard type, but
                //                                          //      not array or generic

                Type typeElement = type_I.GetElementType();
                boolIsStandardArray = (!(
                    typeElement./*NSTD*/IsArray/*END-NSTD*/ ||
                    typeElement./*NSTD*/IsGenericType/*END-NSTD*/ ||
                    !Test.boolIsStandard(typeElement, false)
                    ));

                if (
                    boolAbort_I && !boolIsStandardArray
                    )
                    Tools.subAbort(Test.strTo(type_I, "type_I") + ", " +
                        Test.strTo(typeElement, "type_I.GetElementType") +
                        " the element of standard array type should be standard type, but not array or generic");
            }

            return boolIsStandardArray;
        }
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static bool boolIsStandardGeneric(          //Evaluate if type is standard generic type.

            //                                              //bool, is valid.

            Type type_I,
            //                                              //true, abort if is not valid.
            bool boolAbort_I
            )
        {
            if (
                type_I == null
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " can not be null");

            bool boolIsStandardGeneric = (
                type_I./*NSTD*/IsGenericType/*END-NSTD*/ &&
                (Array.BinarySearch(Test.arrstrGENERIC_TYPE, type_I.Name) >= 0)
                );

            if (
                boolAbort_I && !boolIsStandardGeneric
                )
                Tools.subAbort(Test.strTo(type_I, "type_I") + " is not an standard generic type");

            if (
                boolIsStandardGeneric
                )
            {
                //                                          //The "main argument" of an generic should be standard type,
                //                                          //      but not array or generic

                Type[] arrtypeArgument = type_I./*NSTD*/GetGenericArguments/*END-NSTD*/();
                Type typeMainArgument = arrtypeArgument[arrtypeArgument.Length - 1];

                boolIsStandardGeneric = (!(
                    typeMainArgument./*NSTD*/IsArray/*END-NSTD*/ ||
                    typeMainArgument./*NSTD*/IsGenericType/*END-NSTD*/ ||
                    !Test.boolIsStandard(typeMainArgument, false)
                    ));

                if (
                    boolAbort_I && !boolIsStandardGeneric
                    )
                    Tools.subAbort(Test.strTo(type_I, "type_I") + ", " +
                        Test.strTo(typeMainArgument, "typeMainArgument") +
                        " the main argument of standard generic type should be standard type," +
                        " but not array or generic");

                //                                          //It could be a 2 arguments generic (Dictionary or
                //                                          //      KeyValuePair).

                if (
                    boolIsStandardGeneric && type_I.Name.EndsWith("`2")
                    )
                {
                    //                                      //It should be dictionary or KeyValuePair, the first
                    //                                      //      argument should be String.

                    boolIsStandardGeneric = (
                        arrtypeArgument[0] == typeof(String)
                        );

                    if (
                        boolAbort_I && !boolIsStandardGeneric
                        )
                        Tools.subAbort(Test.strTo(type_I, "type_I") + ", " +
                            Test.strTo(arrtypeArgument[0], "arrtypeArgument[0]") +
                            " the first argument of standard 2 arguments generic type should be String");
                }
            }

            return boolIsStandardGeneric;
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Test.strFormatYyyyy Set of Private Methods to Format Object to Display*/
        //--------------------------------------------------------------------------------------------------------------
        private static String strFormatSingle(              //Format for display.

            //                                              //str, formated info

            //                                              //Any single type (no arrays or generic types)
            /*NSTD*/Object/*END-NSTD*/ obj_I,
            //                                              //FULL or SHORT display.
            TestoptionEnum testoptionOption_I,
            //                                              //Variable name of the single object.
            String strText_I
            )
        {
            Type typeObj = obj_I.GetType();
            if (
                typeObj./*END-NSTD*/IsArray/*END-NSTD*/ ||
                typeObj./*END-NSTD*/IsGenericType/*END-NSTD*/
                )
                Tools.subAbort(Test.strTo(typeObj, "obj_I.GetType") +
                    " only single types can be processed in this method");

            String strFormatSingle;
            if (
                testoptionOption_I == TestoptionEnum.SHORT
                )
            {
                strFormatSingle = Test.strFormatSingleShort(obj_I);
            }
            else
            {
                strFormatSingle = Test.strFormatSingleFull(obj_I, strText_I);
            }

            return strFormatSingle;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strFormatSingleShort(         //Format for display.

            //                                              //str, formated info

            //                                              //Read strFormatSingle method for paramenters description
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            String strFormatSingleShort;
            if (
                (obj_I is BclassBaseClassAbstract) || (obj_I is BtupleBaseTupleAbstract)
                )
            {
                //                                          //Only id.
                strFormatSingleShort = Test.strGetObjId(obj_I);
            }
            else
            {
                //                                          //Any othet type shows a single object
                strFormatSingleShort = Test.strAnalizeAndFormatCheckNulls(obj_I, TestoptionEnum.SHORT);
            }

            return strFormatSingleShort;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strFormatSingleFull(          //Format for display.

            //                                              //str, formated info

            //                                              //Read strFormatSingle method for paramenters description
            /*NSTD*/Object/*END-NSTD*/ obj_I,
            String strText_I
            )
        {
            //                                              //To display a bclass first time requires a block
            String strFormatSingleFull;
            if (
                //                                          //Bclass processed for first time.
                (obj_I is BclassBaseClassAbstract) && !Test.lstobjPreviouslyProcessed.Contains(obj_I)
                )
            {
                //                                          //A first time bclass should be display inside a block    
                String strNL;
                String strLabel;
                //                                          //The objId will be display before bclass, is not
                //                                          //      in the block headings
                Test.subBlockStart(out strNL, out strLabel, out strFormatSingleFull, strText_I, "");

                strFormatSingleFull = strFormatSingleFull + strNL +
                    Test.strAnalizeAndFormatCheckNulls((BclassBaseClassAbstract)obj_I, TestoptionEnum.FULL);

                Test.subBlockEnd(ref strNL, ref strFormatSingleFull, strLabel);
            }
            else
            {
                //                                          //No blocking requires, any single type
                strFormatSingleFull = strText_I + "(" +
                    Test.strAnalizeAndFormatCheckNulls(obj_I, TestoptionEnum.FULL) +")";
            }

            return strFormatSingleFull;
        }

        //--------------------------------------------------------------------------------------------------------------
        private static String strFormatArrOrOneArgumentGeneric(
            //                                              //Format for display
            //                                              //An arr or One Argument Generic object should be display
            //                                              //      only once per run.

            //                                              //str, formated info

            //                                              //arr to format
            /*NSTD*/Object[]/*END-NSTD*/ arrobj_I,
            //                                              //SHORT or FULL display
            TestoptionEnum testoptionOption_I,
            //                                              //Variable name of arr or one argument generic object
            String strText_I,
            //                                              //this is needed to get objId.
            /*NSTD*/Object/*END-NSTD*/ objOriginal_I
            )
        {
            String strObjId;
            if (
                objOriginal_I.GetType()./*NSTD*/IsArray/*END-NSTD*/
                )
            {
                strObjId = Test.strGetObjId(objOriginal_I);
            }
            else
            {
                //                                          //Get size from arrobj
                strObjId = Test.strGetObjId(objOriginal_I).Replace("[?]", "[" + arrobj_I.Length + "]");
            }

            String strFormatArrOrOneArgumentGeneric;
            if (
                testoptionOption_I == TestoptionEnum.SHORT
                )
            {
                strFormatArrOrOneArgumentGeneric = strObjId;
            }
            else
            {
                //                                          //An Arr or One Argument Generic object should be display
                //                                          //      only once per run.

                if (
                    Test.lstobjPreviouslyProcessed.Contains(objOriginal_I)
                    )
                {
                    strFormatArrOrOneArgumentGeneric = strText_I + "(" + strObjId + "|look object up|" + ")";
                }
                else
                {
                    //                                      //Register arr or one argument generic as processed
                    Test.lstobjPreviouslyProcessed.Add(objOriginal_I);

                    strFormatArrOrOneArgumentGeneric = Test.strFormatArr(arrobj_I, strText_I, strObjId);
                }
            }

            return strFormatArrOrOneArgumentGeneric;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strFormatArr(                 //Format for display, it could be:
            //                                              //Set of Lines(Items) or One Line(Row).

            //                                              //str, formated info.

            //                                              //Read strFormatArrOrOneArgumentGeneric method for
            //                                              //      paramenters description
            /*NSTD*/Object[]/*END-NSTD*/ arrobj_I,
            String strText_I,
            String strObjId_I
            )
        {
            //                                              //Find if Set of Lines(Items) format is required.
            bool boolSetOfLinesItems;
            if (
                (arrobj_I is BclassBaseClassAbstract[]) || (arrobj_I is BtupleBaseTupleAbstract[])
                )
            {
                boolSetOfLinesItems = true;
            }
            else
            {
                //                                          //Need to look for long item
                boolSetOfLinesItems = false;
                int intI = 0;
                /*UNTIL-DO*/
                if (!(
                    boolSetOfLinesItems ||
                    (intI >= arrobj_I.Length)
                    ))
                {
                    String strItem = Test.strAnalizeAndFormatCheckNulls(arrobj_I[intI], TestoptionEnum.FULL);
                    boolSetOfLinesItems = (
                        strItem.Length > Test.intLONG_ITEM_ROW_MATRIX
                        );

                    intI = intI + 1;
                }
            }

            String strFormatArr;
            if (
                boolSetOfLinesItems
                )
            {
                String strNL;
                String strLabel;
                Test.subBlockStart(out strNL, out strLabel, out strFormatArr, strText_I, strObjId_I);

                strFormatArr = strFormatArr + Test.strListItems(arrobj_I, strNL);

                Test.subBlockEnd(ref strNL, ref strFormatArr, strLabel);
            }
            else
            {
                strFormatArr = strText_I + "(" + strObjId_I + Test.strLineRow(arrobj_I) + ")";
            }

            return strFormatArr;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strListItems(                 //Format an array to a Set of Lines(Items) inside a block.
            //                                              //Example:
            //                                              //[
            //                                              //{
            //                                              //[0] item
            //                                              //...
            //                                              //[x] item
            //                                              //}
            //                                              //]

            //                                              //str, set in block format

            /*NSTD*/Object[]/*END-NSTD*/ arrobj_I,
            String strNL_I
            )
        {
            //                                              //Chars required for longest index: "[x]"
            int intCharsInLongestIndex = ("[" + (arrobj_I.Length - 1) + "]").Length;

            //                                              //Produces a Set of Lines(Items) ready to display.
            String[] arrstrIndexAndItem = new String[arrobj_I.Length];
            for (int intI = 0; intI < arrobj_I.Length; intI = intI + 1)
            {
                String strItem = strAnalizeAndFormatCheckNulls(arrobj_I[intI], TestoptionEnum.FULL);

                //                                          //Format: NL [i]_ item
                arrstrIndexAndItem[intI] = strNL_I + ("[" + intI + "]").PadRight(intCharsInLongestIndex) + " " +
                    strItem;
            }

            return strNL_I + "{" + String.Concat(arrstrIndexAndItem) + strNL_I + "}";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strLineRow(                   //Produces:
            //                                              //{ item, ..., item }.

            //                                              //str, arr in one line format.

            /*NSTD*/Object[]/*END-NSTD*/ arrobj_I
            )
        {
            //                                              //Convert arrobj to arrstr
            String[] arrstrItem = new String[arrobj_I.Length];
            for (int intI = 0; intI < arrobj_I.Length; intI = intI + 1)
            {
                arrstrItem[intI] = Test.strAnalizeAndFormatCheckNulls(arrobj_I[intI], TestoptionEnum.FULL);
            }

            //                                          //Format: { item, item, ..., item }
            return Test.strVectorFromSet(arrstrItem);
        }

        //--------------------------------------------------------------------------------------------------------------
        private static String strFormatArr2Main(            //Format for display.
            //                                              //An arr2 object should be display only once per run.

            //                                              //str, formated info.

            //                                              //arr2 to format
            /*NSTD*/Object[,]/*END-NSTD*/ arr2obj_I,
            //                                              //SHORT or FULL display
            TestoptionEnum testoptionOption_I,
            //                                              //Variable name of arr2
            String strText_I,
            //                                              //this is needed to get objId.
            /*NSTD*/Object/*END-NSTD*/ objOriginal_I
            )
        {
            String strObjId = Test.strGetObjId(objOriginal_I);

            String strFormatArr2Main;
            if (
                testoptionOption_I == TestoptionEnum.SHORT
                )
            {
                strFormatArr2Main = strObjId;
            }
            else
            {
                //                                          //An arr2 object should be display only once per run.
                if (
                    Test.lstobjPreviouslyProcessed.Contains(objOriginal_I)
                    )
                {
                    strFormatArr2Main = strText_I + "(" + strObjId + "|look object up|" + ")";
                }
                else
                {
                    //                                      //Register arr2 as processed
                    Test.lstobjPreviouslyProcessed.Add(objOriginal_I);

                    strFormatArr2Main = Test.strFormatArr2(arr2obj_I, strText_I, strObjId);
                }
            }

            return strFormatArr2Main;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strFormatArr2(                //Format for display, it could be:
            //                                              //Set of Lines(Items), Set of Lines(Rows) or One
            //                                              //      Line(Matrix).

            //                                              //str, formated info.

            //                                              //Read strFormatArr2Main method for paramenters description
            /*NSTD*/Object[,]/*END-NSTD*/ arr2obj_I,
            String strText_I,
            String strObjId_I
            )
        {
            //                                              //Find if Set of Lines(Items) or Set of Lines(Rows) format
            //                                              //      is required.
            bool boolSetOfLinesItems;
            bool boolSetOfLinesRows;
            if (
                (arr2obj_I is BclassBaseClassAbstract[,]) || (arr2obj_I is BtupleBaseTupleAbstract[,])
                )
            {
                boolSetOfLinesItems = true;
                boolSetOfLinesRows = true;
            }
            else
            {
                //                                          //Need to look for long row and item
                boolSetOfLinesItems = false;
                boolSetOfLinesRows = false;
                int intI = 0;
                /*UNTIL-DO*/
                while (!(
                    boolSetOfLinesRows ||
                    (intI >= arr2obj_I.GetLength(0))
                    ))
                {
                    int intRowLength = 0;

                    int intJ = 0;
                    /*UNTIL-DO*/
                    while (!(
                        boolSetOfLinesItems ||
                        (intJ >= arr2obj_I.GetLength(1))
                        ))
                    {
                        String strItem = Test.strAnalizeAndFormatCheckNulls(arr2obj_I[intI, intJ],
                            TestoptionEnum.FULL);
                        boolSetOfLinesItems = (
                            strItem.Length > Test.intLONG_ITEM_ROW_MATRIX
                            );

                        intRowLength = intRowLength + strItem.Length;

                        intJ = intJ + 1;
                    }

                    //                                      //Add formating chars { item, item, item }
                    intRowLength = intRowLength + 2 * arr2obj_I.GetLength(1) + 2;
                    boolSetOfLinesRows = (
                        intRowLength > Test.intLONG_ITEM_ROW_MATRIX
                        );

                    intI = intI + 1;
                }
            }

            String strFormatArr2;
            if (
                //                                          //Row or Item requires block (if item requires => row also)
                boolSetOfLinesRows
                )
            {
                String strNL;
                String strLabel;
                Test.subBlockStart(out strNL, out strLabel, out strFormatArr2, strText_I, strObjId_I);

                if (
                    boolSetOfLinesItems
                    )
                {
                    strFormatArr2 = strFormatArr2 + Test.strListItems(arr2obj_I, strNL);
                }
                else
                {
                    strFormatArr2 = strFormatArr2 + Test.strListRows(arr2obj_I, strNL);
                }

                Test.subBlockEnd(ref strNL, ref strFormatArr2, strLabel);
            }
            else
            {
                strFormatArr2 = strText_I + "(" + strObjId_I + Test.strLineMatrix(arr2obj_I) + ")";
            }

            return strFormatArr2;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strListItems(                 //Format a matrix to a Set of Lines(Items) inside a block.
            //                                              //Example:
            //                                              //[
            //                                              //{
            //                                              //  {
            //                                              //[0, 0] item
            //                                              //...
            //                                              //[0, y] item
            //                                              //  }
            //                                              //.....
            //                                              //  {
            //                                              //[x, 0] item
            //                                              //...
            //                                              //[x, y] item
            //                                              //  }
            //                                              //}
            //                                              //]
            //                                              //str, matrix in block format

            //                                              //Read strFormatArr2Main method for paramenters description
            /*NSTD*/Object[,]/*END-NSTD*/ arr2obj_I,
            String strNL_I
            )
        {
            //                                              //Chars required for longest index: "[x, y]"
            int intCharsInLargestIndex = 
                ("[" + (arr2obj_I.GetLength(0) - 1) + ", " + (arr2obj_I.GetLength(1) - 1) + "]").Length;

            String[] arrstrBlockForRow = new String[arr2obj_I.GetLength(0)];
            for (int intI = 0; intI < arr2obj_I.GetLength(0); intI = intI + 1)
            {
                //                                          //Produces a Set of Lines(Items) ready to display.
                String[] arrstrIndexAndItem = new String[arr2obj_I.GetLength(1)];
                for (int intJ = 0; intJ < arr2obj_I.GetLength(1); intJ = intJ + 1)
                {
                    String strItem = Test.strAnalizeAndFormatCheckNulls(arr2obj_I[intI, intJ], TestoptionEnum.FULL);

                    //                                      //Format: NL [i,j]_ item
                    arrstrIndexAndItem[intJ] = strNL_I +
                        ("[" + intI + ", " + intJ + "]").PadRight(intCharsInLargestIndex) + " " + strItem;
                }

                arrstrBlockForRow[intI] = strNL_I + "  {" + String.Concat(arrstrIndexAndItem) + strNL_I + "  }";
            }

            return strNL_I + "{" + String.Concat(arrstrBlockForRow) + strNL_I + "}";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strListRows(                  //Format a matrix to a Set of Lines(Rows) inside a block.
            //                                              //Example:
            //                                              //[
            //                                              //{
            //                                              //[0, *] { item, ..., item }
            //                                              //.......
            //                                              //[x, *] { item, ..., item }
            //                                              //}
            //                                              //]

            //                                              //str, matrix in block format

            //                                              //Read strFormatArr2Main method for paramenters description
            /*NSTD*/Object[,]/*END-NSTD*/ arr2Obj_I,
            String strNL_I
            )
        {
            //                                              //Chars required for longest index: "[x, *]"
            int intCharsInLongestIndex = ("[" + (arr2Obj_I.GetLength(0) - 1) + ", *]").Length;

            String[] arrstrIndexAndRow = new String[arr2Obj_I.GetLength(0)];
            for (int intI = 0; intI < arr2Obj_I.GetLength(0); intI = intI + 1)
            {
                //                                          //I need to separate a row
                String[] arrstrItemInRow = new String[arr2Obj_I.GetLength(1)];
                for (int intJ = 0; intJ < arr2Obj_I.GetLength(1); intJ = intJ + 1)
                {
                    arrstrItemInRow[intJ] = Test.strAnalizeAndFormatCheckNulls(arr2Obj_I[intI, intJ],
                        TestoptionEnum.FULL);
                }

                //                                          //Format: { item, item, ..., item }
                String strRow = Test.strVectorFromSet(arrstrItemInRow);

                //                                          //Format: NL [x, *]_ row
                arrstrIndexAndRow[intI] = strNL_I + ("[" + intI + ", *]").PadRight(intCharsInLongestIndex) + " " +
                    strRow;
            }

            return strNL_I + "{" + String.Concat(arrstrIndexAndRow) + strNL_I + "}";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strLineMatrix(                //Produces:
            //                                              //[
            //                                              //{ { item, ..., item }, ....., { item, ..., item } }
            //                                              //]

            //                                              //str, matrix in one long line format.

            //                                              //Read strFormatArr2Main method for paramenters description
            /*NSTD*/Object[,]/*END-NSTD*/ arr2obj_I
            )
        {
                //                                          //Format rows
                String[] arrstrRow = new String[arr2obj_I.GetLength(0)];
                for (int intI = 0; intI < arr2obj_I.GetLength(0); intI = intI + 1)
                {
                    //                                      //I need to separate a row
                    String[] arrstrItemInRow = new String[arr2obj_I.GetLength(1)];
                    for (int intJ = 0; intJ < arr2obj_I.GetLength(1); intJ = intJ + 1)
                    {
                        arrstrItemInRow[intJ] = Test.strAnalizeAndFormatCheckNulls(arr2obj_I[intI, intJ],
                            TestoptionEnum.FULL);
                    }

                    //                                       //Format: { item, item, ..., item }
                    arrstrRow[intI] = Test.strVectorFromSet(arrstrItemInRow);
                }

                //                                          //Format: { row, row, ..., row }
                return Test.strVectorFromSet(arrstrRow);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strFormatArr3Main(            //Format for display.
            //                                              //An arr3 object should be display only once per run.

            //                                              //str, formated info.

            //                                              //arr3 to format
            /*NSTD*/Object[, ,]/*END-NSTD*/ arr3obj_I,
            //                                              //SHORT or FULL display
            TestoptionEnum testoptionOption_I,
            //                                              //Variable name of arr3
            String strText_I,
            //                                              //this is needed to get objId.
            /*NSTD*/Object/*END-NSTD*/ objOriginal_I
            )
        {
            //                                              //To easy code
            String strObjId = Test.strGetObjId(objOriginal_I);

            String strFormatArr3Main;
            if (
                testoptionOption_I == TestoptionEnum.SHORT
                )
            {
                strFormatArr3Main = strObjId;
            }
            else
            {
                //                                          //An arr3 object should be display only once per run.
                if (
                    Test.lstobjPreviouslyProcessed.Contains(objOriginal_I)
                    )
                {
                    strFormatArr3Main = strText_I + "(" + strObjId + "|look object up|" + ")";
                }
                else
                {
                    //                                      //Register arr3 as processed
                    Test.lstobjPreviouslyProcessed.Add(objOriginal_I);

                    strFormatArr3Main = Test.strFormatArr3(arr3obj_I, strText_I, strObjId);
                }
            }

            return strFormatArr3Main;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strFormatArr3(                //Format for display, it could be:
            //                                              //Set of Lines(Items), Set of Lines(Rows), Set of
            //                                              //      Lines(Matrixes) or One Line(Cube).

            //                                              //str, formated info.

            //                                              //Read strFormatArr2Main method for paramenters description
            /*NSTD*/Object[, ,]/*END-NSTD*/ arr3obj_I,
            String strText_I,
            String strObjId_I
            )
        {
            //                                              //Find if Set of Lines(Matrixes), Set of Lines(Items) or Set
            //                                              //      of Lines(Rows) format is required.
            bool boolSetOfLinesItems;
            bool boolSetOfLinesRows;
            bool boolSetOfLinesMatrixes;
            if (
                (arr3obj_I is BclassBaseClassAbstract[, ,]) || (arr3obj_I is BtupleBaseTupleAbstract[, ,])
                )
            {
                boolSetOfLinesItems = true;
                boolSetOfLinesRows = true;
                boolSetOfLinesMatrixes = true;
            }
            else
            {
                //                                          //Need to look for long matrix, row and item
                boolSetOfLinesItems = false;
                boolSetOfLinesRows = false;
                boolSetOfLinesMatrixes = false;
                int intI = 0;
                while (!(
                    boolSetOfLinesMatrixes ||
                    (intI >= arr3obj_I.GetLength(0))
                    ))
                {
                    int intMatrixLength = 0;

                    int intJ = 0;
                    /*UNTIL-DO*/
                    while (!(
                        boolSetOfLinesRows ||
                        (intJ >= arr3obj_I.GetLength(1))
                        ))
                    {
                        int intRowLength = 0;

                        int intK = 0;
                        /*UNTIL-DO*/
                        while (!(
                            boolSetOfLinesItems ||
                            (intK >= arr3obj_I.GetLength(2))
                            ))
                        {
                            String strItem = Test.strAnalizeAndFormatCheckNulls(arr3obj_I[intI, intJ, intK],
                                TestoptionEnum.FULL);
                            boolSetOfLinesItems = (
                                strItem.Length > Test.intLONG_ITEM_ROW_MATRIX
                                );

                            intRowLength = intRowLength + strItem.Length;

                            intK = intK + 1;
                        }

                        //                                      //Add formating chars { item, item, item }
                        intRowLength = intRowLength + 2 * arr3obj_I.GetLength(1) + 2;
                        boolSetOfLinesRows = (
                            intRowLength > Test.intLONG_ITEM_ROW_MATRIX
                            );

                        intMatrixLength = intMatrixLength + intRowLength;

                        intJ = intJ + 1;
                    }

                    //                                      //Add formating chars { row, row, row }
                    intMatrixLength = intMatrixLength + 2 * arr3obj_I.GetLength(2) + 2;
                    boolSetOfLinesMatrixes = (
                        intMatrixLength > Test.intLONG_ITEM_ROW_MATRIX
                        );

                    intI = intI + 1;
                }
            }

            String strFormatArr3;
            if (
                //                                          //Matrix, Row or Item requires block (if item requires =>
                //                                          //      row also => matrix also)
                boolSetOfLinesMatrixes
                )
            {
                String strNL;
                String strLabel;
                Test.subBlockStart(out strNL, out strLabel, out strFormatArr3, strText_I, strObjId_I);

                /*CASE*/
                if (
                    boolSetOfLinesItems
                    )
                {
                    strFormatArr3 = strFormatArr3 + Test.strListItems(arr3obj_I, strNL);
                }
                else if (
                    boolSetOfLinesRows
                    )
                {
                    strFormatArr3 = strFormatArr3 + Test.strListRows(arr3obj_I, strNL);
                }
                else
                {
                    strFormatArr3 = strFormatArr3 + Test.strListMatrixes(arr3obj_I, strNL);
                }
                /*END-CASE*/

                Test.subBlockEnd(ref strNL, ref strFormatArr3, strLabel);
            }
            else
            {
                strFormatArr3 = strText_I + "(" + strObjId_I + Test.strLineCube(arr3obj_I) + ")";
            }

            return strFormatArr3;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strListItems(                 //Format a cube to a set of items inside a block.
            //                                              //Example:
            //                                              //[
            //                                              //{
            //                                              //  {
            //                                              //    {
            //                                              //[0, 0, 0] item
            //                                              //...
            //                                              //[0, 0, z] item
            //                                              //    }
            //                                              //.....
            //                                              //    {
            //                                              //[0, y, 0] item
            //                                              //...
            //                                              //[0, y, z] item
            //                                              //    }
            //                                              //  }
            //                                              //.......
            //                                              //  {
            //                                              //    {
            //                                              //[x, 0, 0] item
            //                                              //...
            //                                              //[x, 0, z] item
            //                                              //    }
            //                                              //.....
            //                                              //    {
            //                                              //[x, y, 0] item
            //                                              //...
            //                                              //[x, y, z] item
            //                                              //    }
            //                                              //  }
            //                                              //}
            //                                              //]
            //                                              //str, cube in block format

            /*NSTD*/Object[, ,]/*END-NSTD*/ arr3obj_I,
            String strNL_I
            )
        {
            //                                              //Chars required for longest index: "[x, y, z]"
            int intCharsInLargestIndex =
                ("[" + (arr3obj_I.GetLength(0) - 1) + ", " + (arr3obj_I.GetLength(1) - 1) + ", " + 
                    (arr3obj_I.GetLength(2) - 1) + "]").Length;

            String[] arrstrBlockForMatrix = new String[arr3obj_I.GetLength(0)];
            for (int intI = 0; intI < arr3obj_I.GetLength(0); intI = intI + 1)
            {
                String[] arrstrBlockForRow = new String[arr3obj_I.GetLength(1)];
                for (int intJ = 0; intJ < arr3obj_I.GetLength(1); intJ = intJ + 1)
                {
                    //                                      //Produces a set of lines ready to display.
                    String[] arrstrIndexAndItem = new String[arr3obj_I.GetLength(2)];
                    for (int intK = 0; intK < arr3obj_I.GetLength(2); intK = intK + 1)
                    {
                        String strItem = Test.strAnalizeAndFormatCheckNulls(arr3obj_I[intI, intJ, intK],
                            TestoptionEnum.FULL);

                        //                                  //Format: NL [i, j, k]_ item
                        arrstrIndexAndItem[intK] = strNL_I +
                            ("[" + intI + ", " + intJ + ", " + intK + "]").PadRight(intCharsInLargestIndex) + " " +
                            strItem;
                    }

                    arrstrBlockForRow[intJ] = strNL_I + "    {" + string.Concat(arrstrIndexAndItem) + strNL_I + "    }";
                }

                arrstrBlockForMatrix[intI] = strNL_I + "  {" + String.Concat(arrstrBlockForRow) + strNL_I + "  }";
            }

            return strNL_I + "{" + String.Concat(arrstrBlockForMatrix) + strNL_I + "}";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strListRows(                  //Format a cube to a set of rows inside a block.
            //                                              //Example:
            //                                              //[
            //                                              //{
            //                                              //  {
            //                                              //[0, 0, *] { item, ..., item }
            //                                              //.....
            //                                              //[0, y, *] { item, ..., item }
            //                                              //  }
            //                                              //.......
            //                                              //  {
            //                                              //[x, 0, *] { item, ..., item }
            //                                              //.....
            //                                              //[x, y, *] { item, ..., item }
            //                                              //  }
            //                                              //}
            //                                              //]

            //                                              //str, cube in block format

            /*NSTD*/Object[, ,]/*END-NSTD*/ arr3obj_I,
            String strNL_I
            )
        {
            //                                              //Chars required for longest index: "[x, y, *]"
            int intCharsInLongestIndex =
                ("[" + (arr3obj_I.GetLength(0) - 1) + ", " + (arr3obj_I.GetLength(1) - 1) + ", *]").Length;

            String[] arrstrBlockForMatrix = new String[arr3obj_I.GetLength(0)];
            for (int intI = 0; intI < arr3obj_I.GetLength(0); intI = intI + 1)
            {
                String[] arrstrIndexAndRow = new String[arr3obj_I.GetLength(1)];
                for (int intJ = 0; intJ < arr3obj_I.GetLength(1); intJ = intJ + 1)
                {
                    //                                      //I need to separate a row
                    String[] arrstrItemInRow = new String[arr3obj_I.GetLength(2)];
                    for (int intK = 0; intK < arr3obj_I.GetLength(2); intK = intK + 1)
                    {
                        String strItem = Test.strAnalizeAndFormatCheckNulls(arr3obj_I[intI, intJ, intK],
                            TestoptionEnum.FULL);
                        arrstrItemInRow[intK] = strItem;
                    }

                    //                                  //Format: { item, item, ..., item }
                    String strRow = Test.strVectorFromSet(arrstrItemInRow);

                    //                                  //Format: NL [i, j, *]_ row
                    arrstrIndexAndRow[intJ] = strNL_I +
                        ("[" + intI + ", " + intJ + ", *]").PadRight(intCharsInLongestIndex) + " " + strRow;
                }

                arrstrIndexAndRow[intI] = strNL_I + "  {" + String.Concat(arrstrIndexAndRow) + strNL_I + "  }";
            }

            return strNL_I + "{" + String.Concat(arrstrBlockForMatrix) + strNL_I + "}";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strListMatrixes(              //Format a cube to a set of matrixes inside a block.
            //                                              //Example:
            //                                              //[
            //                                              //{
            //                                              //[0, *, *] { { item, ..., item }, .....,
            //                                              //{ item, ..., item } }
            //                                              //.......
            //                                              //[x, *, *] { { item, ..., item }, .....,
            //                                              //{ item, ..., item } }
            //                                              //}
            //                                              //]

            //                                              //str, cube in block format

            /*NSTD*/Object[, ,]/*END-NSTD*/ arr3obj_I,
            String strNL_I
            )
        {
            //                                              //Chars required for longest index: "[x, *, *]"
            int intCharsInLongestIndex = ("[" + (arr3obj_I.GetLength(0) - 1) + ", *, *]").Length;

            String[] arrstrIndexAndMatrix = new String[arr3obj_I.GetLength(0)];
            for (int intI = 0; intI < arr3obj_I.GetLength(0); intI = intI + 1)
            {
                //                                          //I need to separate a matrix (array containing rows)
                String[] arrstrRowInMatrix = new String[arr3obj_I.GetLength(1)];
                for (int intJ = 0; intJ < arr3obj_I.GetLength(1); intJ = intJ + 1)
                {
                    //                                          //I need to separate a row
                    String[] arrstrItemInRow = new String[arr3obj_I.GetLength(2)];
                    for (int intK = 0; intK < arr3obj_I.GetLength(2); intK = intK + 1)
                    {
                        arrstrItemInRow[intK] = Test.strAnalizeAndFormatCheckNulls(arr3obj_I[intI, intJ, intK], 
                            TestoptionEnum.FULL);
                    }

                    //                                      //Format: { item, item, ..., item }
                    arrstrRowInMatrix[intJ] = Test.strVectorFromSet(arrstrItemInRow);
                }

                //                                          //Format: { row, row, ..., row }
                String strMatrix = Test.strVectorFromSet(arrstrRowInMatrix);

                //                                          //Format: NL [i, *, *]_ matrix
                arrstrIndexAndMatrix[intI] = strNL_I + ("[" + intI + ", *, *]").PadRight(intCharsInLongestIndex) + " " +
                    strMatrix;
            }

            return strNL_I + "{" + String.Concat(arrstrIndexAndMatrix) + strNL_I + "}";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strLineCube(                  //Produces:
            //                                              //[
            //                                              //{ { { item, ..., item }, ....., { item, ..., item } },
            //                                              // .......,
            //                                              //{ { item, ..., item }, ....., { item, ..., item } } }
            //                                              //]

            //                                              //str, array in one long line format.

            //                                              //Array of formated items
            /*NSTD*/Object[, ,]/*END-NSTD*/ arr3obj_I
            )
        {
            //                                              //Format matrixes
            String[] arrstrMatrixInCube = new String[arr3obj_I.GetLength(0)];
            for (int intI = 0; intI < arr3obj_I.GetLength(0); intI = intI + 1)
            {
                //                                          //I need to separate a matrix
                String[] arrstrRowInMatrix = new String[arr3obj_I.GetLength(1)];
                for (int intJ = 0; intJ < arr3obj_I.GetLength(1); intJ = intJ + 1)
                {
                    //                                      //I need to separate a row
                    String[] arrstrItemInRow = new String[arr3obj_I.GetLength(2)];
                    for (int intK = 0; intK < arr3obj_I.GetLength(2); intK = intK + 1)
                    {
                        arrstrItemInRow[intK] = Test.strAnalizeAndFormatCheckNulls(arr3obj_I[intI, intJ, intK],
                            TestoptionEnum.FULL);
                    }

                    //                                      //Format: { item, item, ..., item }
                    arrstrRowInMatrix[intJ] = Test.strVectorFromSet(arrstrItemInRow);
                }

                //                                          //Format: { row, row, ..., row }
                arrstrMatrixInCube[intI] = Test.strVectorFromSet(arrstrRowInMatrix);
            }

            //                                              //Format: { matriz, matriz, ..., matriz }
            return Test.strVectorFromSet(arrstrMatrixInCube);
        }

        //--------------------------------------------------------------------------------------------------------------
        private static String strVectorFromSet(             //Produces:
            //                                              //{ stuff, ..., stuff }.
            //                                              //Posibilities:
            //                                              //Put a set of strItem in a vector (strRow).
            //                                              //Put a set of strRow in a vector (strMatrix).
            //                                              //Put a set of strMatrix in a vector (strCube).

            //                                              //str, vector format.

            //                                              //Stuff to be included in strVector.
            String[] arrstrStuff_I
            )
        {
            String strRowFormatBeforeAddingBrackets;
            if (
                arrstrStuff_I.Length == 0
                )
            {
                strRowFormatBeforeAddingBrackets = " ";
            }
            else
            {
                strRowFormatBeforeAddingBrackets = " " + String.Join(", ", arrstrStuff_I) + " ";
            }

            return "{" + strRowFormatBeforeAddingBrackets + "}";
        }

        //--------------------------------------------------------------------------------------------------------------
        private static String strFormatDicMain(             //Format for display
            //                                              //A dic object should be display only once per run.

            //                                              //str, formated info

            //                                              //dic.Keys and dic.Values to format
            /*NSTD*/Object[]/*END-NSTD*/ arrobjValue_I,
            String[] arrstrKey_I,
            //                                              //SHORT or FULL display
            TestoptionEnum testoptionOption_I,
            //                                              //Variable name of dic
            String strText_I,
            //                                              //dic, any type (this is needed to get objId).
            /*NSTD*/Object/*END-NSTD*/ objDic_I
            )
        {
            //                                              //Get size from arrkey
            String strObjId = Test.strGetObjId(objDic_I).Replace("[?]", "[" + arrstrKey_I.Length + "]");

            String strFormatDicMain;
            if (
                testoptionOption_I == TestoptionEnum.SHORT
                )
            {
                strFormatDicMain = strObjId;
            }
            else
            {
                if (
                    Test.lstobjPreviouslyProcessed.Contains(objDic_I)
                    )
                {
                    strFormatDicMain = strText_I + "(" + strObjId + "|look object up|" + ")";
                }
                else
                {
                    //                                      //Register dic as processed
                    Test.lstobjPreviouslyProcessed.Add(objDic_I);

                    strFormatDicMain = Test.strFormatDic(arrobjValue_I, arrstrKey_I, strText_I, strObjId);
                }
            }

            return strFormatDicMain;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strFormatDic(                 //Format for display.

            //                                              //str, formated info.

            //                                              //Read strFormatDicMain method for paramenters description
            /*NSTD*/Object[]/*END-NSTD*/ arrobjValue_I,
            String[] arrstrKey_I,
            String strText_I,
            String strObjId_I
            )
        {
            Array.Sort(arrstrKey_I, arrobjValue_I);

            //                                              //Compute [key] size.
            int intLonghestKey = 0;
            foreach (String str in arrstrKey_I)
            {
                intLonghestKey = Math.Max(intLonghestKey, str.Length);
            }
            intLonghestKey = intLonghestKey + "[]".Length;
            intLonghestKey = Math.Min(intKEY_LEN_MAX, intLonghestKey);

            String strFormatDic;
            String strNL;
            String strLabel;
            Test.subBlockStart(out strNL, out strLabel, out strFormatDic, strText_I, strObjId_I);


            //                                              //Produces lines to include in block.
            String[] arrstrEntry = new String[arrobjValue_I.Length];
            for (int intI = 0; intI < arrobjValue_I.Length; intI = intI + 1)
            {
                //                                          //Confirm key has only visible chars
                String strKeyAnalized = Test.strAnalizeAndFormatStr(arrstrKey_I[intI]);
                String strKey;
                if (
                    //                                      //Has no diagnotic info (ends with "), if happen to be null
                    //                                      //      (should not) do not ends with "
                    strKeyAnalized.EndsWith("\"")
                    )
                {
                    //                                      //Take key without " "
                    strKey = strKeyAnalized.Substring(1, strKeyAnalized.Length - 2);
                }
                else
                {
                    //                                      //Include disgnostic info (or is null).
                    strKey = strKeyAnalized;
                }

                arrstrEntry[intI] = strNL + ("[" + strKey + "]").PadRight(intLonghestKey) + " " +
                    Test.strAnalizeAndFormatCheckNulls(arrobjValue_I[intI], TestoptionEnum.FULL);
            }

            strFormatDic = strFormatDic + strNL + "{" + String.Concat(arrstrEntry) + strNL + "}";

            Test.subBlockEnd(ref strNL, ref strFormatDic, strLabel);

            return strFormatDic;
        }

        //--------------------------------------------------------------------------------------------------------------
        private static String strFormatKvpMain(             //Format for display

            //                                              //str, formated info

            //                                              //kvp.Key and kvp.Value to format (Value could be null)
            /*NSTD*/Object/*END-NSTD*/ objValue_I,
            String strKey_I,
            //                                              //SHORT or FULL display
            TestoptionEnum testoptionOption_I,
            //                                              //Variable name of kvp
            String strText_I,
            //                                              //kvp, any type (this is needed to get objId).
            /*NSTD*/Object/*END-NSTD*/ objKvp_I
            )
        {
            //                                              //Confirm key has only visible chars
            String strKeyAnalized = Test.strAnalizeAndFormatStr(strKey_I);
            String strKey;
            if (
                //                                          //Has no diagnotic info (ends with "), if happen to be null
                //                                          //      (should not) do not ends with "
                strKeyAnalized.EndsWith("\"")
                )
            {
                //                                          //Take key without " "
                strKey = strKeyAnalized.Substring(1, strKeyAnalized.Length - 2);
            }
            else
            { 
                //                                          //Include disgnostic info (or is null).
                strKey = strKeyAnalized;
            }

            String strFormatKvpMain;
            if (
                testoptionOption_I == TestoptionEnum.SHORT
                )
            {
                strFormatKvpMain = "<" + strKey + ", " + 
                    Test.strAnalizeAndFormatCheckNulls(objValue_I, TestoptionEnum.SHORT) + ">";
            }
            else
            {
                strFormatKvpMain = "<" + strKey + ", " +
                    Test.strAnalizeAndFormatCheckNulls(objValue_I, TestoptionEnum.FULL) + ">";
            }

            return strFormatKvpMain;
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Test.Blocking Support blocking in the display Objects Info*/
        //--------------------------------------------------------------------------------------------------------------
        /*CONSTANTS*/

        //                                                  //Si hay mas de 28 niveles, se les pone el úlltimo.
        private const String strLETTERS_FOR_LEVEL = "?ABCDEFGHIJKLMNOPQRSTUVWXYZ*";

        //                                                  //Si hay mas de 25 niveles, se usa el último valor.
        private static int[] arrintLevelSpaces = { 
            0, 4, 8, 12, 16, 20, 24, 27, 30, 33, 36, 39, 42, 44, 46, 48, 50, 52, 54, 55, 56, 57, 58, 59, 60 
            };

        //--------------------------------------------------------------------------------------------------------------
        /*STATIC VARIABLES*/

        //                                                  //Todas las clases no estaticas incluyen el método strTo
        //                                                  //      para mostrar el contenido de dicha clase, algunos de
        //                                                  //      estos métodos requieren block START-END para mostrar
        //                                                  //      el contenido de sus objetos cuando estos contienen
        //                                                  //      colecciones.

        //                                                  //Cada block START-END debe estar en un nivel superior a su
        //                                                  //      base, se incrementa al iniciar el block y se 
        //                                                  //      decrementa al cerrarlo.
        private static int intLevel;

        //                                                  //Esta variable se usa para en cada block START-END 
        //                                                  //      asignarle un número único (para esto, al tomar el 
        //                                                  //      valor se incrementa en 1).
        private static int intStartEnd;

        //                                                  //Cada nivel, del 1 en adelane, tiene asociada una letra (A,
        //                                                  //      B, ...).
        //                                                  //Tambien, a cada nivel se le asocia una indentación al 
        //                                                  //      inicio de cada línea (esto es una cantidad de
        //                                                  //      espacios).

        //--------------------------------------------------------------------------------------------------------------
        /*STATIC CONSTRUCTOR SUPPORT METHODS*/

        //--------------------------------------------------------------------------------------------------------------
        private static void subPrepareConstantsToBlockFormat(
            //                                              //Método de apoyo llamado en constructor estático. 
            //                                              //Inicia Nivel y StartEnd necesarios para indentar el log.
            )
        {
            Test.intLevel = 0;
            Test.intStartEnd = 0;
        }

        //--------------------------------------------------------------------------------------------------------------
        private static String strNL(                        //NL + caracteres indentación.
            )
        {
            //                                              //Determina el NL+indentación que corresponde al block.
            if (
                intLevel < 0
                )
                Tools.subAbort(Test.strTo(intLevel, "intNivel") + " should be 0 or positive");

            //                                              //Determina la cantidad de espacios para la indentación.
            int intSpaces;
            if (
                //                                          //El nivel excede el arreglo.
                intLevel >= arrintLevelSpaces.Length
                )
            {
                //                                          //Cuando no alcance usa el último valor.
                intSpaces = arrintLevelSpaces[arrintLevelSpaces.Length - 1];
            }
            else
            {
                intSpaces = arrintLevelSpaces[intLevel];
            }

            //                                              //Return NL with spacing required
            return Environment.NewLine + "".PadRight(intSpaces);
        }

        //--------------------------------------------------------------------------------------------------------------
        private static void subBlockStart(                  //Genera los parámetros requerido para subToBlockFormat.
            //                                              //Solo se usa este método cuando block START-END.

            //                                              //NL + caracteres indentación.
            out String strNL_O,
            //                                              //Label for block START_??? y END_???. (this is ???).
            out String strLabel_O,
            //                                              //String to start block information
            out String strTo_O,
            //                                              //Text to describe the object
            String strText_I,
            //                                              //Object Id, if this block is por a bclass should be ""
            String strObjId_I
            )
        {
            strNL_O = strNL();

            //                                              //Asigna el siguiente nivel (lo regresa al cerrar block).
            intLevel = intLevel + 1;

            //                                              //Asigna una secuencia única.
            intStartEnd = intStartEnd + 1;

            //                                              //Determina la etiqueta que corresponde al block.
            char charLettersStartEnd;
            if (
                //                                          //El nivel excede las letras.
                intLevel >= strLETTERS_FOR_LEVEL.Length
                )
            {
                //                                          //Cuando no alcance ni la "Z" usa "*".
                charLettersStartEnd = '*';
            }
            else
            {
                charLettersStartEnd = strLETTERS_FOR_LEVEL[intLevel];
            }

            //                                              //Asigna la etiqueta StartEnd.
            strLabel_O = charLettersStartEnd.ToString() + intStartEnd;

            //                                              //Append Start of block.
            //                                              //If we are in level 1, is is the beginig of a test (new
            //                                              //      log or previously was a WriteLine), the NewLine
            //                                              //      should not be include.
            String strNlForStart;
            if (
                intLevel == 1
                )
            {
                //                                          //Remove NewLine Mark
                strNlForStart = strNL_O.Substring(Environment.NewLine.Length);
            }
            else
            {
                strNlForStart = strNL_O;
            }
            strTo_O = strNlForStart + "**********>>>>>START_" + strLabel_O;
            strTo_O = strTo_O + strNL_O + strText_I + "(" + strObjId_I;
        }

        //--------------------------------------------------------------------------------------------------------------
        private static void subBlockEnd(                    //Termina el block StartEnd (regresa el nivel).
            //                                              //Solo se usa este método cuando block START-END.

            //                                              //NL + caracteres indentación.
            ref String strNL_IO,
            //                                              //String to append information
            ref String strTo_IO,
            String strLabel_I
            )
        {
            //                                          //End of Block
            strTo_IO = strTo_IO + ")" + strNL_IO + "**********<<<<<END_" + strLabel_I;
            //                                              //Regresa el nivel.
            intLevel = intLevel - 1;

            strNL_IO = strNL();
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Test.ObjId set of methods to compute object id*/
        //--------------------------------------------------------------------------------------------------------------
        public static String strGetObjId(                   //Generate an object id.

            //                                              //str, prefixSize:HashCode.
            //                                              //prefix, data type prefix (int, arrint, lststr, etc.).
            //                                              //Size, [l], [l,m], [l,m,n] or "".

            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            if (
                obj_I == null
                )
                Tools.subAbort(Test.strTo(obj_I, "obj_I") + " should have a value");

            Type typeObj = obj_I.GetType();
            if (
                !Test.boolIsStandard(typeObj, false)
                )
                Tools.subAbort(Test.strTo(typeObj, "obj_I.GetType") + " is nonstandard type");

            return Test.strPrefix(obj_I.GetType()) + Test.strCollectionSize(obj_I) + ":" +
                /*NSTD*/obj_I.GetHashCode()/*END-NSTD*/;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static String strPrefix(                     //Generate the object prefix corresponding to type.
            //                                              //Class Name has the structure:
            //                                              //1. AaaaaBbbbbCcccc (a could be a digit).
            //                                              //2. AaaaaBbbbbCcccc[], [,] or [,,] (array)
            //                                              //3. Dictionary`2[String,other FullName], KeyValuePair.
            //                                              //4. List`1[ns.FullName], Queue or Stack.
            //                                              //AaaaaBbbbbCcccc could be:
            //                                              //a. Bclass, Btuple or standard Enumerator.
            //                                              //b. Primitive or system type (included in
            //                                              //      arrstrPRIMITIVE_TYPE or arrstrSYSTEM_TYPE).

            //                                              //str.
            //                                              //1. prefix if is single type (prefix form PRIMITIVE or
            //                                              //      SYSTEM, or aaaaa taken from class name if is a
            //                                              //      Bclass, Btuple o Enum the name structure is
            //                                              //      "AaaaaBbbbbbCcccc, 'a' could be a digit.
            //                                              //2. arrprefix, arr1prefix or arr2prefix where "prefix" is
            //                                              //      the prefix corresponding to element type.
            //                                              //3. xxxarg, where "xxx" is prefix form GENERIC and "arg"
            //                                              //      is the prefix corresponding to argument type (first
            //                                              //      or second argument).
            //                                              //(see type_I definition).

            //                                              //1. Single type (not a array or generic type), Ex. str,
            //                                              //      syspath, cod, codcb, sepoodt, ... .
            //                                              //2. Array type ([], [,] or [, ,]), Ex. arrstr, arr2int,
            //                                              //      arrarrstr, arrdicstr, ... .
            //                                              //3. Generic type (1 or 2 arguments). Ex. dicstr, kvpint,
            //                                              //      lsttok, queuecod, ... .
            Type type_I
            )
        {
            String strPrefix;
            /*CASE*/
            if (
                type_I./*NSTD*/IsArray/*END-NSTD*/
                )
            {
                strPrefix = "arr";

                int intRank = type_I./*NSTD*/GetArrayRank()/*END-NSTD*/;
                if (
                    intRank > 1
                    )
                {
                    //                                      //arr2 or arr3
                    strPrefix = strPrefix + intRank;
                }

                //                                          //arr?elem
                strPrefix = strPrefix + Test.strPrefix(type_I.GetElementType());
            }
            else if (
                type_I./*NSTD*/IsGenericType/*END-NSTD*/
                )
            {
                //                                          //dic, kvp, lst, queue or stack
                strPrefix = Test.arrstrGENERIC_PREFIX[Array.BinarySearch(Test.arrstrGENERIC_TYPE, type_I.Name)];

                //                                          //Use the last argument
                Type[] arrtypeArgument = type_I./*NSTD*/GetGenericArguments()/*END-NSTD*/;
                strPrefix = strPrefix + Test.strPrefix(arrtypeArgument[arrtypeArgument.Length - 1]);
            }
            else
            {
                //                                          //Single form (not an array or generic)

                int intPrimitive = Array.BinarySearch(Test.arrstrPRIMITIVE_TYPE, type_I.Name);
                if (
                    //                                      //Is standard primitive type
                    intPrimitive >= 0
                    )
                {
                    strPrefix = Test.arrstrPRIMITIVE_PREFIX[intPrimitive];
                }
                else
                {
                    int intSystem = Array.BinarySearch(Test.arrstrSYSTEM_TYPE, type_I.Name);
                    if (
                        //                                  //Is standard system type
                        intSystem >= 0
                        )
                    {
                        strPrefix = Test.arrstrSYSTEM_PREFIX[intSystem];
                    }
                    else
                    {
                        //                                  //Should be user defined type (bclass, btuple or enum)

                        if (!(
                            type_I == typeof(BclassBaseClassAbstract) ||
                            type_I.IsSubclassOf(typeof(BclassBaseClassAbstract)) ||
                            type_I == typeof(BtupleBaseTupleAbstract) ||
                            type_I.IsSubclassOf(typeof(BtupleBaseTupleAbstract)) ||
                            type_I == typeof(Enum) ||
                            type_I.IsSubclassOf(typeof(Enum))
                            ))
                            Tools.subAbort(Test.strTo(type_I, "type_I") +
                                " SOMETHING IS WRONG!!! at this point it should be bclass, btuple or enum");


                        //                                  //Is a user define type (bclass, btuple or enum).
                        //                                  //Search for B in AaaaaBbbbbCcccc.
                        //                                  //Start after first character (after 'A')
                        int intI = 1;
                        /*WHILE-DO*/
                        while (
                            (intI < type_I.Name.Length) &&
                            //                              //Between a-z or 0-9
                            Tools.boolIsDigitOrLetterLower(type_I.Name[intI])
                            )
                        {
                            intI = intI + 1;
                        }

                        //                                  //Subtract class prefix (aaaaa).
                        strPrefix = type_I.Name[0].ToString().ToLower() + type_I.Name.Substring(1, intI - 1);
                    }
                }
            }

            return strPrefix;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static String strCollectionSize(             //Generate the collection size.

            //                                              //str, "", [l], [l,m] o [l,m,n].

            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            Type typeCollection = obj_I.GetType();

            String strCollectionSize;
            /*CASE*/
            if (
                typeCollection./*NSTD*/IsArray/*END-NSTD*/
                )
            {
                int intRank = typeCollection./*NSTD*/GetArrayRank()/*END-NSTD*/;
                /*CASE*/
                if (
                    intRank == 1
                    )
                {
                    //                                      //Generate [l].
                    strCollectionSize = Test.strArrSize(obj_I);
                }
                else if (
                    intRank == 2
                    )
                {
                    //                                      //Generate [l,m].
                    strCollectionSize = Test.strArr2Size(obj_I);
                }
                else
                {
                    //                                      //Rank 3
                    //                                      //Generate [l,m,n].
                    strCollectionSize = Test.strArr3Size(obj_I);
                }
                /*END-CASE*/
            }
            else if (
                typeCollection./*END-NSTD*/IsGenericType/*END-NSTD*/
                )
            {
                if (
                    typeCollection.Name == Test.strGENERIC_DICTIONARY_TYPE
                    )
                {
                    //                                      //Generate [l].
                    strCollectionSize = Test.strDicSize(obj_I);
                }
                else if (
                    typeCollection.Name == Test.strGENERIC_KEYVALUEPAIR_TYPE
                    )
                {
                    //                                      //This is not a collection.
                    strCollectionSize = "";
                }
                else if (
                    typeCollection.Name == Test.strGENERIC_LIST_TYPE
                    )
                {
                    //                                      //Generate [l].
                    strCollectionSize = Test.strLstSize(obj_I);
                }
                else if (
                    typeCollection.Name == Test.strGENERIC_QUEUE_TYPE
                    )
                {
                    //                                      //Generate [l].
                    strCollectionSize = Test.strQueueSize(obj_I);
                }
                else if (
                    typeCollection.Name == Test.strGENERIC_STACK_TYPE
                    )
                {
                    //                                      //Generate [l].
                    strCollectionSize = Test.strStackSize(obj_I);
                }
                else
                {
                    if (
                        true
                        )
                        Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") +
                            " SOMETHING IS WRONG!!! is nonstandard generic collection");

                    strCollectionSize = null;

                }
                /*END-CASE*/
            }
            else
            {
                //                                          //It is not a collection
                strCollectionSize = "";
            }
            /*END-CASE*/

            return strCollectionSize;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static String strArrSize(                   //Get [l].

            //                                              //str, [l].

            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            int intLength;
            /*CASE*/
            if (
                obj_I is int[]
                )
            {
                intLength = ((int[])obj_I).Length;
            }
            else if (
                obj_I is long[]
                )
            {
                intLength = ((long[])obj_I).Length;
            }
            else if (
                obj_I is double[]
                )
            {
                intLength = ((double[])obj_I).Length;
            }
            else if (
                obj_I is bool[]
                )
            {
                intLength = ((bool[])obj_I).Length;
            }
            else if (
                obj_I is char[]
                )
            {
                intLength = ((char[])obj_I).Length;
            }
            else if (
                obj_I is DateTime[]
                )
            {
                intLength = ((DateTime[])obj_I).Length;
            }
            else
            {
                Type typeObj = obj_I.GetType();
                Type typeElement = typeObj.GetElementType();
                if (
                    //                                      //Is primitive type
                    (Array.BinarySearch(Test.arrstrPRIMITIVE_TYPE, typeElement.Name) >= 0)
                    )
                    Tools.subAbort(Test.strTo(Test.arrstrPRIMITIVE_TYPE, "Test.arrstrPRIMITIVE_TYPE") + ", " +
                        Test.strTo(typeObj, "obj_I.GetType") +
                        " SOMETHING IS WRONG!!! a branch in previous case is missing");

                intLength = ((/*NSTD*/Object[]/*END-NSTD*/)obj_I).Length;
            }
            /*END-CASE*/

            return "[" + intLength + "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static String strArr2Size(                  //Get [l,m].

            //                                              //str, [l,m].

            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            int intLength0;
            int intLength1;
            /*CASE*/
            if (
                obj_I is int[,]
                )
            {
                int[,] arr2int = (int[,])obj_I;
                intLength0 = arr2int.GetLength(0);
                intLength1 = arr2int.GetLength(1);
            }
            else if (
                obj_I is long[,]
                )
            {
                long[,] arr2long = (long[,])obj_I;
                intLength0 = arr2long.GetLength(0);
                intLength1 = arr2long.GetLength(1);
            }
            else if (
                obj_I is double[,]
                )
            {
                double[,] arr2num = (double[,])obj_I;
                intLength0 = arr2num.GetLength(0);
                intLength1 = arr2num.GetLength(1);
            }
            else if (
                obj_I is bool[,]
                )
            {
                bool[,] arr2bool = (bool[,])obj_I;
                intLength0 = arr2bool.GetLength(0);
                intLength1 = arr2bool.GetLength(1);
            }
            else if (
                obj_I is char[,]
                )
            {
                char[,] arr2char = (char[,])obj_I;
                intLength0 = arr2char.GetLength(0);
                intLength1 = arr2char.GetLength(1);
            }
            else if (
                obj_I is DateTime[,]
                )
            {
                DateTime[,] arr2ts = (DateTime[,])obj_I;
                intLength0 = arr2ts.GetLength(0);
                intLength1 = arr2ts.GetLength(1);
            }
            else
            {
                Type typeObj = obj_I.GetType();
                Type typeElement = typeObj.GetElementType();
                if (
                    //                                      //Is primitive type
                    (Array.BinarySearch(Test.arrstrPRIMITIVE_TYPE, typeElement.Name) >= 0)
                    )
                    Tools.subAbort(Test.strTo(Test.arrstrPRIMITIVE_TYPE, "Test.arrstrPRIMITIVE_TYPE") + ", " +
                        Test.strTo(typeObj, "obj_I.GetType") +
                        " SOMETHING IS WRONG!!! a branch in previous case is missing");

                /*NSTD*/
                Object[,]/*END-NSTD*/ arr2obj = (/*NSTD*/Object[,]/*END-NSTD*/)obj_I;
                intLength0 = arr2obj.GetLength(0);
                intLength1 = arr2obj.GetLength(1);
            }
            /*END-CASE*/

            return "[" + intLength0 + "," + intLength1 + "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static String strArr3Size(                  //Get [l,m,n].

            //                                              //str, [l,m,n].
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            int intLength0;
            int intLength1;
            int intLength2;
            /*CASE*/
            if (
                obj_I is int[, ,]
                )
            {
                int[, ,] arr3int = (int[, ,])obj_I;
                intLength0 = arr3int.GetLength(0);
                intLength1 = arr3int.GetLength(1);
                intLength2 = arr3int.GetLength(2);
            }
            else if (
                obj_I is long[, ,]
                )
            {
                long[, ,] arr3long = (long[, ,])obj_I;
                intLength0 = arr3long.GetLength(0);
                intLength1 = arr3long.GetLength(1);
                intLength2 = arr3long.GetLength(2);
            }
            else if (
                obj_I is double[, ,]
                )
            {
                double[, ,] arr3num = (double[, ,])obj_I;
                intLength0 = arr3num.GetLength(0);
                intLength1 = arr3num.GetLength(1);
                intLength2 = arr3num.GetLength(2);
            }
            else if (
                obj_I is bool[, ,]
                )
            {
                bool[, ,] arr3bool = (bool[, ,])obj_I;
                intLength0 = arr3bool.GetLength(0);
                intLength1 = arr3bool.GetLength(1);
                intLength2 = arr3bool.GetLength(2);
            }
            else if (
                obj_I is char[, ,]
                )
            {
                char[, ,] arr3char = (char[, ,])obj_I;
                intLength0 = arr3char.GetLength(0);
                intLength1 = arr3char.GetLength(1);
                intLength2 = arr3char.GetLength(2);
            }
            else if (
                obj_I is DateTime[, ,]
                )
            {
                DateTime[, ,] arr3ts = (DateTime[, ,])obj_I;
                intLength0 = arr3ts.GetLength(0);
                intLength1 = arr3ts.GetLength(1);
                intLength2 = arr3ts.GetLength(2);
            }
            else
            {
                Type typeObj = obj_I.GetType();
                Type typeElement = typeObj.GetElementType();
                if (
                    //                                      //Is primitive type
                    (Array.BinarySearch(Test.arrstrPRIMITIVE_TYPE, typeElement.Name) >= 0)
                    )
                    Tools.subAbort(Test.strTo(Test.arrstrPRIMITIVE_TYPE, "Test.arrstrPRIMITIVE_TYPE") + ", " +
                        Test.strTo(typeObj, "obj_I.GetType") + 
                        " SOMETHING IS WRONG!!! a branch in previous case is missing");

                /*NSTD*/
                Object[, ,]/*END-NSTD*/ arr3obj = (/*NSTD*/Object[, ,]/*END-NSTD*/)obj_I;
                intLength0 = arr3obj.GetLength(0);
                intLength1 = arr3obj.GetLength(1);
                intLength2 = arr3obj.GetLength(2);
            }
            /*END-CASE*/

            return "[" + intLength0 + "," + intLength1 + "," + intLength2 + "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static String strLstSize(                   //Get [l].

            //                                              //str, [l].

            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            String strCount;
            /*CASE*/
            if (
                obj_I is List<int>
                )
            {
                strCount = ((List<int>)obj_I).Count.ToString();
            }
            else if (
                obj_I is List<long>
                )
            {
                strCount = ((List<long>)obj_I).Count.ToString();
            }
            else if (
                obj_I is List<double>
                )
            {
                strCount = ((List<double>)obj_I).Count.ToString();
            }
            else if (
                obj_I is List<bool>
                )
            {
                strCount = ((List<bool>)obj_I).Count.ToString();
            }
            else if (
                obj_I is List<char>
                )
            {
                strCount = ((List<char>)obj_I).Count.ToString();
            }
            else if (
                obj_I is List<String>
                )
            {
                strCount = ((List<String>)obj_I).Count.ToString();
            }
            else if (
                obj_I is List<DateTime>
                )
            {
                strCount = ((List<DateTime>)obj_I).Count.ToString();
            }
            else if (
                obj_I is List<Type>
                )
            {
                strCount = ((List<Type>)obj_I).Count.ToString();
            }
            else if (
                obj_I is List<DirectoryInfo>
                )
            {
                strCount = ((List<DirectoryInfo>)obj_I).Count.ToString();
            }
            else if (
                obj_I is List<FileInfo>
                )
            {
                strCount = ((List<FileInfo>)obj_I).Count.ToString();
            }
            else if (
                obj_I is List<StreamReader>
                )
            {
                strCount = ((List<StreamReader>)obj_I).Count.ToString();
            }
            else if (
                obj_I is List<StreamWriter>
                )
            {
                strCount = ((List<StreamWriter>)obj_I).Count.ToString();
            }
            else
            {
                Type typeObj = obj_I.GetType();
                Type typeArgument = typeObj.GetGenericArguments()[0];
                if (
                    //                                      //Is primitive type
                    (Array.BinarySearch(Test.arrstrPRIMITIVE_TYPE, typeArgument.Name) >= 0)
                    )
                    Tools.subWarning(Test.strTo(Test.arrstrPRIMITIVE_TYPE, "Test.arrstrPRIMITIVE_TYPE") + ", " +
                        Test.strTo(typeObj, "obj_I.GetType") +
                        " SOMETHING IS WRONG!!! a branch in previous case is missing, you may continue");
                if (
                    //                                      //Is system type
                    (Array.BinarySearch(Test.arrstrSYSTEM_TYPE, typeArgument.Name) >= 0)
                    )
                    Tools.subWarning(Test.strTo(Test.arrstrSYSTEM_TYPE, "Test.arrstrSYSTEM_TYPE") + ", " +
                        Test.strTo(typeObj, "obj_I.GetType") +
                        " SOMETHING IS WRONG!!! a branch in previous case is missing, you may continue");

                //                                          /Can note get count.
                strCount = "?";
            }
            /*END-CASE*/

            return "[" + strCount + "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static String strQueueSize(                 //Get [l].

            //                                              //str, [l].

            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            String strCount;
            /*CASE*/
            if (
                obj_I is Queue<int>
                )
            {
                strCount = ((Queue<int>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Queue<long>
                )
            {
                strCount = ((Queue<long>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Queue<double>
                )
            {
                strCount = ((Queue<double>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Queue<bool>
                )
            {
                strCount = ((Queue<bool>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Queue<char>
                )
            {
                strCount = ((Queue<char>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Queue<String>
                )
            {
                strCount = ((Queue<String>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Queue<DateTime>
                )
            {
                strCount = ((Queue<DateTime>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Queue<Type>
                )
            {
                strCount = ((Queue<Type>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Queue<DirectoryInfo>
                )
            {
                strCount = ((Queue<DirectoryInfo>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Queue<FileInfo>
                )
            {
                strCount = ((Queue<FileInfo>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Queue<StreamReader>
                )
            {
                strCount = ((Queue<StreamReader>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Queue<StreamWriter>
                )
            {
                strCount = ((Queue<StreamWriter>)obj_I).Count.ToString();
            }
            else
            {
                Type typeObj = obj_I.GetType();
                Type typeArgument = typeObj.GetGenericArguments()[0];
                if (
                    //                                      //Is primitive type
                    (Array.BinarySearch(Test.arrstrPRIMITIVE_TYPE, typeArgument.Name) >= 0)
                    )
                    Tools.subWarning(Test.strTo(Test.arrstrPRIMITIVE_TYPE, "Test.arrstrPRIMITIVE_TYPE") + ", " +
                        Test.strTo(typeObj, "obj_I.GetType") +
                        " SOMETHING IS WRONG!!! a branch in previous case is missing, you may continue");
                if (
                    //                                      //Is system type
                    (Array.BinarySearch(Test.arrstrSYSTEM_TYPE, typeArgument.Name) >= 0)
                    )
                    Tools.subWarning(Test.strTo(Test.arrstrSYSTEM_TYPE, "Test.arrstrSYSTEM_TYPE") + ", " +
                        Test.strTo(typeObj, "obj_I.GetType") +
                        " SOMETHING IS WRONG!!! a branch in previous case is missing, you may continue");

                //                                          /Can note get count.
                strCount = "?";
            }
            /*END-CASE*/

            return "[" + strCount + "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static String strStackSize(                 //Get [l].

            //                                              //str, [l].

            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            String strCount;
            /*CASE*/
            if (
                obj_I is Stack<int>
                )
            {
                strCount = ((Stack<int>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Stack<long>
                )
            {
                strCount = ((Stack<long>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Stack<double>
                )
            {
                strCount = ((Stack<double>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Stack<bool>
                )
            {
                strCount = ((Stack<bool>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Stack<char>
                )
            {
                strCount = ((Stack<char>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Stack<String>
                )
            {
                strCount = ((Stack<String>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Stack<DateTime>
                )
            {
                strCount = ((Stack<DateTime>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Stack<Type>
                )
            {
                strCount = ((Stack<Type>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Stack<DirectoryInfo>
                )
            {
                strCount = ((Stack<DirectoryInfo>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Stack<FileInfo>
                )
            {
                strCount = ((Stack<FileInfo>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Stack<StreamReader>
                )
            {
                strCount = ((Stack<StreamReader>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Stack<StreamWriter>
                )
            {
                strCount = ((Stack<StreamWriter>)obj_I).Count.ToString();
            }
            else
            {
                Type typeObj = obj_I.GetType();
                Type typeArgument = typeObj.GetGenericArguments()[0];
                if (
                    //                                      //Is primitive type
                    (Array.BinarySearch(Test.arrstrPRIMITIVE_TYPE, typeArgument.Name) >= 0)
                    )
                    Tools.subWarning(Test.strTo(Test.arrstrPRIMITIVE_TYPE, "Test.arrstrPRIMITIVE_TYPE") + ", " +
                        Test.strTo(typeObj, "obj_I.GetType") +
                        " SOMETHING IS WRONG!!! a branch in previous case is missing, you may continue");
                if (
                    //                                      //Is system type
                    (Array.BinarySearch(Test.arrstrSYSTEM_TYPE, typeArgument.Name) >= 0)
                    )
                    Tools.subWarning(Test.strTo(Test.arrstrSYSTEM_TYPE, "Test.arrstrSYSTEM_TYPE") + ", " +
                        Test.strTo(typeObj, "obj_I.GetType") +
                        " SOMETHING IS WRONG!!! a branch in previous case is missing, you may continue");

                //                                          /Can note get count.
                strCount = "?";
            }
            /*END-CASE*/

            return "[" + strCount + "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private static String strDicSize(                   //Get [l].

            //                                              //str, [l].

            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            String strCount;
            /*CASE*/
            if (
                obj_I is Dictionary<String, int>
                )
            {
                strCount = ((Dictionary<String, int>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Dictionary<String, long>
                )
            {
                strCount = ((Dictionary<String, long>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Dictionary<String, double>
                )
            {
                strCount = ((Dictionary<String, double>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Dictionary<String, bool>
                )
            {
                strCount = ((Dictionary<String, bool>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Dictionary<String, char>
                )
            {
                strCount = ((Dictionary<String, char>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Dictionary<String, String>
                )
            {
                strCount = ((Dictionary<String, String>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Dictionary<String, DateTime>
                )
            {
                strCount = ((Dictionary<String, DateTime>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Dictionary<String, Type>
                )
            {
                strCount = ((Dictionary<String, Type>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Dictionary<String, DirectoryInfo>
                )
            {
                strCount = ((Dictionary<String, DirectoryInfo>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Dictionary<String, FileInfo>
                )
            {
                strCount = ((Dictionary<String, FileInfo>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Dictionary<String, StreamReader>
                )
            {
                strCount = ((Dictionary<String, StreamReader>)obj_I).Count.ToString();
            }
            else if (
                obj_I is Dictionary<String, StreamWriter>
                )
            {
                strCount = ((Dictionary<String, StreamWriter>)obj_I).Count.ToString();
            }
            else
            {
                Type typeObj = obj_I.GetType();
                Type typeArgument = typeObj.GetGenericArguments()[1];
                if (
                    //                                      //Is primitive type
                    (Array.BinarySearch(Test.arrstrPRIMITIVE_TYPE, typeArgument.Name) >= 0)
                    )
                    Tools.subWarning(Test.strTo(Test.arrstrPRIMITIVE_TYPE, "Test.arrstrPRIMITIVE_TYPE") + ", " +
                        Test.strTo(typeObj, "obj_I.GetType") + 
                        " SOMETHING IS WRONG!!! a branch in previous case is missing, you may continue");
                if (
                    //                                      //Is system type
                    (Array.BinarySearch(Test.arrstrSYSTEM_TYPE, typeArgument.Name) >= 0)
                    )
                    Tools.subWarning(Test.strTo(Test.arrstrSYSTEM_TYPE, "Test.arrstrSYSTEM_TYPE") + ", " +
                        Test.strTo(typeObj, "obj_I.GetType") +
                        " SOMETHING IS WRONG!!! a branch in previous case is missing, you may continue");

                //                                          //Can not get count.
                strCount = "?";
            }
            /*END-CASE*/

            return "[" + strCount + "]";
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Test.Boxing set of private methods to box primitives*/
        //--------------------------------------------------------------------------------------------------------------
        private static void subfunConvertAndBox(            //Convert and box if required.
            //                                              //Parameters have the following posibilities:
            //                                              //[ Main            Key             Object
            //                                              //  oppp              -             appp-primitive(int, ...) 
            //                                              //  arr?oppp          -             arr?ppp    
            //                                              //  arroppp           -             lstppp    
            //                                              //  arropppValue    arrstrKey       dicppp    
            //                                              //  opppValue       strKey          kvpppp    
            //                                              //
            //                                              //  yyy               -             yyy-system(str,ts,...)
            //                                              //  arr?yyy           -             arr?yyy
            //                                              //  arryyy            -             lstyyy
            //                                              //  arryyyValue     arrstrKey       dicyyy
            //                                              //  yyyValue        strKey          kvpyyy
            //                                              //
            //                                              //  eee               -             eee-enum(+subclasses)
            //                                              //  arr?eee           -             arr?eee
            //                                              //  (do not work)                   lsteee
            //                                              //  (do not work)                   diceee
            //                                              //  (do not work)                   kvpeee
            //                                              //
            //                                              //  ccc               -             ccc-bclass(+subclasses)
            //                                              //  arr?ccc           -             arr?ccc
            //                                              //  (do not work)                   lstccc
            //                                              //  (do not work)                   dicccc
            //                                              //  (do not work)                   kvpccc
            //                                              //
            //                                              //  ttt               -             ttt-tuple(+subclasses)
            //                                              //  arr?ttt           -             arr?ttt
            //                                              //  (do not work)                   lstttt
            //                                              //  (do not work)                   dicttt
            //                                              //  (do not work)                   kvpttt
            //                                              //]

            //                                              //obj or arrobj
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //arrstrKey or strKey, only for 2 atribute generic, other
            //                                              //      will return null
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //Any standard object, except generic with atribute enum,
            //                                              //      bclass or btuple.
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            //                                              //To easy code
            Type typeObj = obj_I.GetType();
            Type typeContent;
            /*CASE*/
            if (
                typeObj./*END-NSTD*/IsArray/*END-NSTD*/
                )
            {
                typeContent = typeObj.GetElementType();
            }
            else if (
                typeObj./*END-NSTD*/IsGenericType/*END-NSTD*/
                )
            {
                //                                          //Get needed argument type
                Type[] arrtypeArguments = typeObj./*NSTD*/GetGenericArguments/*END-NSTD*/();
                typeContent = arrtypeArguments[arrtypeArguments.Length - 1];
            }
            else
            {
                //                                          //Single type
                typeContent = typeObj;
            }
            /*END-CASE*/

            //                                              //Select process.
            //                                              //Primitive, simple and system types will process any
            //                                              //      structure (single, array or generic).
            //                                              //Enum, bclass and btuple will process only single and array
            //                                              //      structures.
            /*CASE*/
            if (
                //                                          //Is any form (single, array or generic) of primiteve type
                Array.BinarySearch(Test.arrstrPRIMITIVE_TYPE, typeContent.Name) >= 0
                )
            {
                Test.subfunConvertAndBoxPrimitive(out objMain_O, out objKey_O, obj_I, typeContent);
            }
            else if (
                //                                          //Is a any form (single, array or generic) of system type
                Array.BinarySearch(Test.arrstrSYSTEM_TYPE, typeContent.Name) >= 0
                )
            {
                //                                          //Only generic form need to be converted

                if (
                    typeObj./*NSTD*/IsGenericType/*END-NSTD*/
                    )
                {
                    Test.subfunConvertSystemType(out objMain_O, out objKey_O, obj_I, typeContent);
                }
                else
                {
                    //                                      //No convertion is required for single or array form

                    objMain_O = obj_I;
                    objKey_O = null;
                }
            }
            else if (
                //                                          //The agrument is any user defined type (bclass, btuple,
                //                                          //      Enum or Exception)
                (typeContent == typeof(BclassBaseClassAbstract)) ||
                typeContent.IsSubclassOf(typeof(BclassBaseClassAbstract)) ||
                (typeContent == typeof(BtupleBaseTupleAbstract)) ||
                typeContent.IsSubclassOf(typeof(BtupleBaseTupleAbstract)) || 
                (typeContent == typeof(Enum)) || 
                typeContent.IsSubclassOf(typeof(Enum)) ||
                (typeContent == typeof(Exception)) || 
                typeContent.IsSubclassOf(typeof(Exception))
                )
            {
                //                                          //Generic form can not be processed
                //                                          //This type can not be processed, please convert 1 argument
                //                                          //      generic to array, dic to 2 arrays and kvp to its 2
                //                                          //      components and call the correct strTo method

                if (
                    typeObj./*END-NSTD*/IsGenericType/*END-NSTD*/
                    )
                {
                    if (
                        typeObj.Name == Test.strGENERIC_DICTIONARY_TYPE
                        )
                        Tools.subAbort(Test.strTo(typeObj, "obj_I.GetType") +
                            " dic type with argument bclass, btuple or enum can not be processed in this method," +
                            " you should produce a arrobjValue and arrstrKey and call the correct strTo method");
                    if (
                        typeObj.Name == Test.strGENERIC_KEYVALUEPAIR_TYPE
                        )
                        Tools.subAbort(Test.strTo(typeObj, "obj_I.GetType") +
                            " kvp type with argument bclass, btuple or enum can not be processed in this method," +
                            " you should produce a objValue anr strKey and call the correct strTo method");
                    if (
                        true
                        )
                        Tools.subAbort(Test.strTo(typeObj, "obj_I.GetType") +
                            " 1 argument generic type with argument bclass, btuple or enum can not be processed in " +
                            " this method, you should produce a arrobj and call the correct strTo method");

                    objMain_O = null;
                    objKey_O = null;
                }
                else
                {
                    //                                      //No convertion is required for single or array form

                    objMain_O = obj_I;
                    objKey_O = null;
                }
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(typeObj, "obj_I.GetType") + Test.strTo(typeContent, "typeContent") + 
                        " SOMETHING IS WRONG!!!, the content seems nonstandard type");

                objMain_O = null;
                objKey_O = null;
            }
            /*END-CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertAndBoxPrimitive(   //Convert and box primitives.
            //                                              //Parameters have the following posibilities:
            //                                              //[ Main            Key             Object
            //                                              //  oppp              -             appp-primitive(int, ...) 
            //                                              //  arr?oppp          -             arr?ppp    
            //                                              //  arroppp           -             lstppp    
            //                                              //  arropppValue    arrstrKey       dicppp    
            //                                              //  opppValue       strKey          kvpppp
            //                                              //]

            //                                              //obj or arrobj
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //arrstrKey or strKey, only for 2 atribute generic, other
            //                                              //      will return null
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //Any standard object, except generic with atribute enum,
            //                                              //      bclass or btuple.
            /*NSTD*/Object/*END-NSTD*/ obj_I,
            Type typeContent_I
            )
        {
            //                                              //Select process.
            /*CASE*/
            if (
                typeContent_I == typeof(int)
                )
            {
                Test.subfunConvertAndBoxInt(out objMain_O, out objKey_O, obj_I);
            }
            else if (
                typeContent_I == typeof(long)
                )
            {
                Test.subfunConvertAndBoxLong(out objMain_O, out objKey_O, obj_I);
            }
            else if (
                typeContent_I == typeof(double)
                )
            {
                Test.subfunConvertAndBoxNum(out objMain_O, out objKey_O, obj_I);
            }
            else if (
                typeContent_I == typeof(bool)
                )
            {
                Test.subfunConvertAndBoxBool(out objMain_O, out objKey_O, obj_I);
            }
            else if (
                typeContent_I == typeof(char)
                )
            {
                Test.subfunConvertAndBoxChar(out objMain_O, out objKey_O, obj_I);
            }
            else if (
                typeContent_I == typeof(DateTime)
                )
            {
                Test.subfunConvertAndBoxTs(out objMain_O, out objKey_O, obj_I);
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") + " SOMETHING IS WRONG!!!," +
                        " it seems to be any form (single, array or generic) of nonstandard primitive");

                objMain_O = null;
                objKey_O = null;
            }
            /*END-CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertAndBoxInt(         //Convert and box int.

            //                                              //oint, oint[], oint[,] or oint[, ,].
            //                                              //lstint, ... (one atribute generic) will convert to oint[].
            //                                              //dicint will convert to ointValues[] and strKeys[].
            //                                              //kvpint will convert to ointValue and strKey.
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //null, str[] (keys from dic) or or str (key form kvp).
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //int type (single, array or generic).
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            /*END_CASE*/
            if (
                obj_I is int
                )
            {
                //                                          //Box primitive
                Oint oint = new Oint((int)obj_I);

                objMain_O = oint;
                objKey_O = null;
            }
            else if (
                obj_I is int[]
                )
            {
                int[] arrint = (int[])obj_I;

                //                                          //Box primitives
                Oint[] arroint = new Oint[arrint.Length];
                for (int intI = 0; intI < arrint.Length; intI = intI + 1)
                {
                    arroint[intI] = new Oint(arrint[intI]);
                }

                objMain_O = arroint;
                objKey_O = null;
            }
            else if (
                obj_I is List<int>
                )
            {
                List<int> lstint = (List<int>)obj_I;

                //                                          //Box primitives
                Oint[] arroint = new Oint[lstint.Count];
                for (int intI = 0; intI < lstint.Count; intI = intI + 1)
                {
                    arroint[intI] = new Oint(lstint[intI]);
                }

                objMain_O = arroint;
                objKey_O = null;
            }
            else if (
                obj_I is Queue<int>
                )
            {
                Queue<int> queueint = (Queue<int>)obj_I;

                //                                          //Box primitives
                Oint[] arroint = new Oint[queueint.Count];
                int intI = 0;
                foreach (int intX in queueint)
                {
                    arroint[intI] = new Oint(intX);

                    intI = intI + 1;
                }

                objMain_O = arroint;
                objKey_O = null;
            }
            else if (
                obj_I is Stack<int>
                )
            {
                Stack<int> stackint = (Stack<int>)obj_I;

                //                                          //Box primitives
                Oint[] arroint = new Oint[stackint.Count];
                int intI = 0;
                foreach (int intX in stackint)
                {
                    arroint[intI] = new Oint(intX);

                    intI = intI + 1;
                }

                objMain_O = arroint;
                objKey_O = null;
            }
            else if (
                obj_I is int[,]
                )
            {
                int[,] arr2int = (int[,])obj_I;

                //                                          //Box primitives
                Oint[,] arr2oint = new Oint[arr2int.GetLength(0), arr2int.GetLength(1)];
                for (int intI = 0; intI < arr2int.GetLength(0); intI = intI + 1)
                {
                    for (int intJ = 0; intJ < arr2int.GetLength(1); intJ = intJ + 1)
                    {
                        arr2oint[intI, intJ] = new Oint(arr2int[intI, intJ]);
                    }
                }

                objMain_O = arr2oint;
                objKey_O = null;
            }
            else if (
                obj_I is int[, ,]
                )
            {
                int[, ,] arr3int = (int[, ,])obj_I;

                //                                          //Box primitives
                Oint[, ,] arr3oint = new Oint[arr3int.GetLength(0), arr3int.GetLength(1), arr3int.GetLength(2)];
                for (int intI = 0; intI < arr3int.GetLength(0); intI = intI + 1)
                {
                    for (int intJ = 0; intJ < arr3int.GetLength(1); intJ = intJ + 1)
                    {
                        for (int intK = 0; intK < arr3int.GetLength(2); intK = intK + 1)
                        {
                            arr3oint[intI, intJ, intK] = new Oint(arr3int[intI, intJ, intK]);
                        }
                    }
                }

                objMain_O = arr3oint;
                objKey_O = null;
            }
            else if (
                obj_I is Dictionary<String, int>
                )
            {
                Dictionary<String, int> dicint = (Dictionary<String, int>)obj_I;

                //                                          //Convert to arrays.
                int[] arrintValues = new int[dicint.Count];
                dicint.Values.CopyTo(arrintValues, 0);
                String[] arrstrKeys = new String[dicint.Count];
                dicint.Keys.CopyTo(arrstrKeys, 0);

                //                                          //Box primitives
                Oint[] arrointValues = new Oint[arrintValues.Length];
                for (int intI = 0; intI < arrintValues.Length; intI = intI + 1)
                {
                    arrointValues[intI] = new Oint(arrintValues[intI]);
                }

                objMain_O = arrointValues;
                objKey_O = arrstrKeys;
            }
            else if (
                obj_I is KeyValuePair<String, int>
                )
            {
                KeyValuePair<String, int> kvpint = (KeyValuePair<String, int>)obj_I;

                //                                          //Extract attributes.
                int intValue = kvpint.Value;
                String strKey = kvpint.Key;

                //                                          //Box primitive
                Oint ointValue = new Oint(intValue);

                objMain_O = ointValue;
                objKey_O = strKey;
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") +
                        " SOMETHING IS WRONG!!!, this type could not be processed with other int types");

                objMain_O = null;
                objKey_O = null;
            }
            /*END_CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertAndBoxLong(        //Convert and box long.

            //                                              //olong, olong[], olong[,] or olong[, ,].
            //                                              //lstlong, ... (one atribute generic) will convert to
            //                                              //      olong[].
            //                                              //diclong will convert to olongValues[] and strKeys[].
            //                                              //kvplong will convert to olongValue and strKey.
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //null, str[] (keys from dic) or or str (key form kvp).
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //long type (single, array or generic).
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            /*END_CASE*/
            if (
                obj_I is long
                )
            {
                //                                          //Box primitive
                Olong olong = new Olong((long)obj_I);

                objMain_O = olong;
                objKey_O = null;
            }
            else if (
                obj_I is long[]
                )
            {
                long[] arrlong = (long[])obj_I;

                //                                          //Box primitives
                Olong[] arrolong = new Olong[arrlong.Length];
                for (int intI = 0; intI < arrlong.Length; intI = intI + 1)
                {
                    arrolong[intI] = new Olong(arrlong[intI]);
                }

                objMain_O = arrolong;
                objKey_O = null;
            }
            else if (
                obj_I is List<long>
                )
            {
                List<long> lstlong = (List<long>)obj_I;

                //                                          //Box primitives
                Olong[] arrolong = new Olong[lstlong.Count];
                for (int intI = 0; intI < lstlong.Count; intI = intI + 1)
                {
                    arrolong[intI] = new Olong(lstlong[intI]);
                }

                objMain_O = arrolong;
                objKey_O = null;
            }
            else if (
                obj_I is Queue<long>
                )
            {
                Queue<long> queuelong = (Queue<long>)obj_I;

                //                                          //Box primitives
                Olong[] arrolong = new Olong[queuelong.Count];
                int intI = 0;
                foreach (long longX in queuelong)
                {
                    arrolong[intI] = new Olong(longX);

                    intI = intI + 1;
                }

                objMain_O = arrolong;
                objKey_O = null;
            }
            else if (
                obj_I is Stack<long>
                )
            {
                Stack<long> stacklong = (Stack<long>)obj_I;

                //                                          //Box primitives
                Olong[] arrolong = new Olong[stacklong.Count];
                int intI = 0;
                foreach (long longX in stacklong)
                {
                    arrolong[intI] = new Olong(longX);

                    intI = intI + 1;
                }

                objMain_O = arrolong;
                objKey_O = null;
            }
            else if (
                obj_I is long[,]
                )
            {
                long[,] arr2long = (long[,])obj_I;

                //                                          //Box primitives
                Olong[,] arr2olong = new Olong[arr2long.GetLength(0), arr2long.GetLength(1)];
                for (int intI = 0; intI < arr2long.GetLength(0); intI = intI + 1)
                {
                    for (int intJ = 0; intJ < arr2long.GetLength(1); intJ = intJ + 1)
                    {
                        arr2olong[intI, intJ] = new Olong(arr2long[intI, intJ]);
                    }
                }

                objMain_O = arr2olong;
                objKey_O = null;
            }
            else if (
                obj_I is long[, ,]
                )
            {
                long[, ,] arr3long = (long[, ,])obj_I;

                //                                          //Box primitives
                Olong[, ,] arr3olong = new Olong[arr3long.GetLength(0), arr3long.GetLength(1), arr3long.GetLength(2)];
                for (int intI = 0; intI < arr3long.GetLength(0); intI = intI + 1)
                {
                    for (int intJ = 0; intJ < arr3long.GetLength(1); intJ = intJ + 1)
                    {
                        for (int intK = 0; intK < arr3long.GetLength(2); intK = intK + 1)
                        {
                            arr3olong[intI, intJ, intK] = new Olong(arr3long[intI, intJ, intK]);
                        }
                    }
                }

                objMain_O = arr3olong;
                objKey_O = null;
            }
            else if (
                obj_I is Dictionary<String, long>
                )
            {
                Dictionary<String, long> diclong = (Dictionary<String, long>)obj_I;

                //                                          //Convert to arrays.
                long[] arrlongValues = new long[diclong.Count];
                diclong.Values.CopyTo(arrlongValues, 0);
                String[] arrstrKeys = new String[diclong.Count];
                diclong.Keys.CopyTo(arrstrKeys, 0);

                //                                          //Box primitives
                Olong[] arrolongValues = new Olong[arrlongValues.Length];
                for (int intI = 0; intI < arrlongValues.Length; intI = intI + 1)
                {
                    arrolongValues[intI] = new Olong(arrlongValues[intI]);
                }

                objMain_O = arrolongValues;
                objKey_O = arrstrKeys;
            }
            else if (
                obj_I is KeyValuePair<String, long>
                )
            {
                KeyValuePair<String, long> kvplong = (KeyValuePair<String, long>)obj_I;

                //                                          //Extract attributes.
                long longValue = kvplong.Value;
                String strKey = kvplong.Key;

                //                                          //Box primitive
                Olong olongValue = new Olong(longValue);

                objMain_O = olongValue;
                objKey_O = strKey;
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") +
                        " SOMETHING IS WRONG!!!, this type could not be processed with other long types");

                objMain_O = null;
                objKey_O = null;
            }
            /*END_CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertAndBoxNum(         //Convert and box num.

            //                                              //onum, onum[], onum[,] or onum[, ,].
            //                                              //lstnum, ... (one atribute generic) will convert to onum[].
            //                                              //dicnum will convert to onumValues[] and strKeys[].
            //                                              //kvpnum will convert to onumValue and strKey.
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //null, str[] (keys from dic) or or str (key form kvp).
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //num type (single, array or generic).
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            /*END_CASE*/
            if (
                obj_I is double
                )
            {
                //                                          //Box primitive
                Onum onum = new Onum((double)obj_I);

                objMain_O = onum;
                objKey_O = null;
            }
            else if (
                obj_I is double[]
                )
            {
                double[] arrnum = (double[])obj_I;

                //                                          //Box primitives
                Onum[] arronum = new Onum[arrnum.Length];
                for (int intI = 0; intI < arrnum.Length; intI = intI + 1)
                {
                    arronum[intI] = new Onum(arrnum[intI]);
                }

                objMain_O = arronum;
                objKey_O = null;
            }
            else if (
                obj_I is List<double>
                )
            {
                List<double> lstnum = (List<double>)obj_I;

                //                                          //Box primitives
                Onum[] arronum = new Onum[lstnum.Count];
                for (int intI = 0; intI < lstnum.Count; intI = intI + 1)
                {
                    arronum[intI] = new Onum(lstnum[intI]);
                }

                objMain_O = arronum;
                objKey_O = null;
            }
            else if (
                obj_I is Queue<double>
                )
            {
                Queue<double> queuenum = (Queue<double>)obj_I;

                //                                          //Box primitives
                Onum[] arronum = new Onum[queuenum.Count];
                int intI = 0;
                foreach (double num in queuenum)
                {
                    arronum[intI] = new Onum(num);

                    intI = intI + 1;
                }

                objMain_O = arronum;
                objKey_O = null;
            }
            else if (
                obj_I is Stack<double>
                )
            {
                Stack<double> stacknum = (Stack<double>)obj_I;

                //                                          //Box primitives
                Onum[] arronum = new Onum[stacknum.Count];
                int intI = 0;
                foreach (double num in stacknum)
                {
                    arronum[intI] = new Onum(num);

                    intI = intI + 1;
                }

                objMain_O = arronum;
                objKey_O = null;
            }
            else if (
                obj_I is double[,]
                )
            {
                double[,] arr2num = (double[,])obj_I;

                //                                          //Box primitives
                Onum[,] arr2onum = new Onum[arr2num.GetLength(0), arr2num.GetLength(1)];
                for (int intI = 0; intI < arr2num.GetLength(0); intI = intI + 1)
                {
                    for (int intJ = 0; intJ < arr2num.GetLength(1); intJ = intJ + 1)
                    {
                        arr2onum[intI, intJ] = new Onum(arr2num[intI, intJ]);
                    }
                }

                objMain_O = arr2onum;
                objKey_O = null;
            }
            else if (
                obj_I is double[, ,]
                )
            {
                double[, ,] arr3num = (double[, ,])obj_I;

                //                                          //Box primitives
                Onum[, ,] arr3onum = new Onum[arr3num.GetLength(0), arr3num.GetLength(1), arr3num.GetLength(2)];
                for (int intI = 0; intI < arr3num.GetLength(0); intI = intI + 1)
                {
                    for (int intJ = 0; intJ < arr3num.GetLength(1); intJ = intJ + 1)
                    {
                        for (int intK = 0; intK < arr3num.GetLength(2); intK = intK + 1)
                        {
                            arr3onum[intI, intJ, intK] = new Onum(arr3num[intI, intJ, intK]);
                        }
                    }
                }

                objMain_O = arr3onum;
                objKey_O = null;
            }
            else if (
                obj_I is Dictionary<String, double>
                )
            {
                Dictionary<String, double> dicnum = (Dictionary<String, double>)obj_I;

                //                                          //Convert to arrays.
                double[] arrnumValues = new double[dicnum.Count];
                dicnum.Values.CopyTo(arrnumValues, 0);
                String[] arrstrKeys = new String[dicnum.Count];
                dicnum.Keys.CopyTo(arrstrKeys, 0);

                //                                          //Box primitives
                Onum[] arronumValues = new Onum[arrnumValues.Length];
                for (int intI = 0; intI < arrnumValues.Length; intI = intI + 1)
                {
                    arronumValues[intI] = new Onum(arrnumValues[intI]);
                }

                objMain_O = arronumValues;
                objKey_O = arrstrKeys;
            }
            else if (
                obj_I is KeyValuePair<String, double>
                )
            {
                KeyValuePair<String, double> kvpnum = (KeyValuePair<String, double>)obj_I;

                //                                          //Extract attributes.
                double numValue = kvpnum.Value;
                String strKey = kvpnum.Key;

                //                                          //Box primitive
                Onum onumValue = new Onum(numValue);

                objMain_O = onumValue;
                objKey_O = strKey;
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") +
                        " SOMETHING IS WRONG!!!, this type could not be processed with other num types");

                objMain_O = null;
                objKey_O = null;
            }
            /*END_CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertAndBoxBool(        //Convert and box bool.

            //                                              //obool, obool[], obool[,] or obool[, ,].
            //                                              //lstbool, ... (one atribute generic) will convert to
            //                                              //      obool[].
            //                                              //dicbool will convert to oboolValues[] and strKeys[].
            //                                              //kvpbool will convert to oboolValue and strKey.
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //null, str[] (keys from dic) or or str (key form kvp).
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //bool type (single, array or generic).
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            /*END_CASE*/
            if (
                obj_I is bool
                )
            {
                //                                          //Box primitive
                Obool obool = new Obool((bool)obj_I);

                objMain_O = obool;
                objKey_O = null;
            }
            else if (
                obj_I is bool[]
                )
            {
                bool[] arrbool = (bool[])obj_I;

                //                                          //Box primitives
                Obool[] arrobool = new Obool[arrbool.Length];
                for (int intI = 0; intI < arrbool.Length; intI = intI + 1)
                {
                    arrobool[intI] = new Obool(arrbool[intI]);
                }

                objMain_O = arrobool;
                objKey_O = null;
            }
            else if (
                obj_I is List<bool>
                )
            {
                List<bool> lstbool = (List<bool>)obj_I;

                //                                          //Box primitives
                Obool[] arrobool = new Obool[lstbool.Count];
                for (int intI = 0; intI < lstbool.Count; intI = intI + 1)
                {
                    arrobool[intI] = new Obool(lstbool[intI]);
                }

                objMain_O = arrobool;
                objKey_O = null;
            }
            else if (
                obj_I is Queue<bool>
                )
            {
                Queue<bool> queuebool = (Queue<bool>)obj_I;

                //                                          //Box primitives
                Obool[] arrobool = new Obool[queuebool.Count];
                int intI = 0;
                foreach (bool boolX in queuebool)
                {
                    arrobool[intI] = new Obool(boolX);

                    intI = intI + 1;
                }

                objMain_O = arrobool;
                objKey_O = null;
            }
            else if (
                obj_I is Stack<bool>
                )
            {
                Stack<bool> stackbool = (Stack<bool>)obj_I;

                //                                          //Box primitives
                Obool[] arrobool = new Obool[stackbool.Count];
                int intI = 0;
                foreach (bool boolX in stackbool)
                {
                    arrobool[intI] = new Obool(boolX);

                    intI = intI + 1;
                }

                objMain_O = arrobool;
                objKey_O = null;
            }
            else if (
                obj_I is bool[,]
                )
            {
                bool[,] arr2bool = (bool[,])obj_I;

                //                                          //Box primitives
                Obool[,] arr2obool = new Obool[arr2bool.GetLength(0), arr2bool.GetLength(1)];
                for (int intI = 0; intI < arr2bool.GetLength(0); intI = intI + 1)
                {
                    for (int intJ = 0; intJ < arr2bool.GetLength(1); intJ = intJ + 1)
                    {
                        arr2obool[intI, intJ] = new Obool(arr2bool[intI, intJ]);
                    }
                }

                objMain_O = arr2obool;
                objKey_O = null;
            }
            else if (
                obj_I is bool[, ,]
                )
            {
                bool[, ,] arr3bool = (bool[, ,])obj_I;

                //                                          //Box primitives
                Obool[, ,] arr3obool = new Obool[arr3bool.GetLength(0), arr3bool.GetLength(1), arr3bool.GetLength(2)];
                for (int intI = 0; intI < arr3bool.GetLength(0); intI = intI + 1)
                {
                    for (int intJ = 0; intJ < arr3bool.GetLength(1); intJ = intJ + 1)
                    {
                        for (int intK = 0; intK < arr3bool.GetLength(2); intK = intK + 1)
                        {
                            arr3obool[intI, intJ, intK] = new Obool(arr3bool[intI, intJ, intK]);
                        }
                    }
                }

                objMain_O = arr3obool;
                objKey_O = null;
            }
            else if (
                obj_I is Dictionary<String, bool>
                )
            {
                Dictionary<String, bool> dicbool = (Dictionary<String, bool>)obj_I;

                //                                          //Convert to arrays.
                bool[] arrboolValues = new bool[dicbool.Count];
                dicbool.Values.CopyTo(arrboolValues, 0);
                String[] arrstrKeys = new String[dicbool.Count];
                dicbool.Keys.CopyTo(arrstrKeys, 0);

                //                                          //Box primitives
                Obool[] arroboolValues = new Obool[arrboolValues.Length];
                for (int intI = 0; intI < arrboolValues.Length; intI = intI + 1)
                {
                    arroboolValues[intI] = new Obool(arrboolValues[intI]);
                }

                objMain_O = arroboolValues;
                objKey_O = arrstrKeys;
            }
            else if (
                obj_I is KeyValuePair<String, bool>
                )
            {
                KeyValuePair<String, bool> kvpbool = (KeyValuePair<String, bool>)obj_I;

                //                                          //Extract attributes.
                bool boolValue = kvpbool.Value;
                String strKey = kvpbool.Key;

                //                                          //Box primitive
                Obool oboolValue = new Obool(boolValue);

                objMain_O = oboolValue;
                objKey_O = strKey;
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") +
                        " SOMETHING IS WRONG!!!, this type could not be processed with other bool types");

                objMain_O = null;
                objKey_O = null;
            }
            /*END_CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertAndBoxChar(        //Convert and box char.

            //                                              //ochar, ochar[], ochar[,] or ochar[, ,].
            //                                              //lstchar, ... (one atribute generic) will convert to
            //                                              //      ochar[].
            //                                              //dicchar will convert to ocharValues[] and strKeys[].
            //                                              //kvpchar will convert to ocharValue and strKey.
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //null, str[] (keys from dic) or or str (key form kvp).
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //char type (single, array or generic).
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            /*END_CASE*/
            if (
                obj_I is char
                )
            {
                //                                          //Box primitive
                Ochar ochar = new Ochar((char)obj_I);

                objMain_O = ochar;
                objKey_O = null;
            }
            else if (
                obj_I is char[]
                )
            {
                char[] arrchar = (char[])obj_I;

                //                                          //Box primitives
                Ochar[] arrochar = new Ochar[arrchar.Length];
                for (int intI = 0; intI < arrchar.Length; intI = intI + 1)
                {
                    arrochar[intI] = new Ochar(arrchar[intI]);
                }

                objMain_O = arrochar;
                objKey_O = null;
            }
            else if (
                obj_I is List<char>
                )
            {
                List<char> lstchar = (List<char>)obj_I;

                //                                          //Box primitives
                Ochar[] arrochar = new Ochar[lstchar.Count];
                for (int intI = 0; intI < lstchar.Count; intI = intI + 1)
                {
                    arrochar[intI] = new Ochar(lstchar[intI]);
                }

                objMain_O = arrochar;
                objKey_O = null;
            }
            else if (
                obj_I is Queue<char>
                )
            {
                Queue<char> queuechar = (Queue<char>)obj_I;

                //                                          //Box primitives
                Ochar[] arrochar = new Ochar[queuechar.Count];
                int intI = 0;
                foreach (char charX in queuechar)
                {
                    arrochar[intI] = new Ochar(charX);

                    intI = intI + 1;
                }

                objMain_O = arrochar;
                objKey_O = null;
            }
            else if (
                obj_I is Stack<char>
                )
            {
                Stack<char> stackchar = (Stack<char>)obj_I;

                //                                          //Box primitives
                Ochar[] arrochar = new Ochar[stackchar.Count];
                int intI = 0;
                foreach (char charX in stackchar)
                {
                    arrochar[intI] = new Ochar(charX);

                    intI = intI + 1;
                }

                objMain_O = arrochar;
                objKey_O = null;
            }
            else if (
                obj_I is char[,]
                )
            {
                char[,] arr2char = (char[,])obj_I;

                //                                          //Box primitives
                Ochar[,] arr2ochar = new Ochar[arr2char.GetLength(0), arr2char.GetLength(1)];
                for (int intI = 0; intI < arr2char.GetLength(0); intI = intI + 1)
                {
                    for (int intJ = 0; intJ < arr2char.GetLength(1); intJ = intJ + 1)
                    {
                        arr2ochar[intI, intJ] = new Ochar(arr2char[intI, intJ]);
                    }
                }

                objMain_O = arr2ochar;
                objKey_O = null;
            }
            else if (
                obj_I is char[, ,]
                )
            {
                char[, ,] arr3char = (char[, ,])obj_I;

                //                                          //Box primitives
                Ochar[, ,] arr3ochar = new Ochar[arr3char.GetLength(0), arr3char.GetLength(1), arr3char.GetLength(2)];
                for (int intI = 0; intI < arr3char.GetLength(0); intI = intI + 1)
                {
                    for (int intJ = 0; intJ < arr3char.GetLength(1); intJ = intJ + 1)
                    {
                        for (int intK = 0; intK < arr3char.GetLength(2); intK = intK + 1)
                        {
                            arr3ochar[intI, intJ, intK] = new Ochar(arr3char[intI, intJ, intK]);
                        }
                    }
                }

                objMain_O = arr3ochar;
                objKey_O = null;
            }
            else if (
                obj_I is Dictionary<String, char>
                )
            {
                Dictionary<String, char> dicchar = (Dictionary<String, char>)obj_I;

                //                                          //Convert to arrays.
                char[] arrcharValues = new char[dicchar.Count];
                dicchar.Values.CopyTo(arrcharValues, 0);
                String[] arrstrKeys = new String[dicchar.Count];
                dicchar.Keys.CopyTo(arrstrKeys, 0);

                //                                          //Box primitives
                Ochar[] arrocharValues = new Ochar[arrcharValues.Length];
                for (int intI = 0; intI < arrcharValues.Length; intI = intI + 1)
                {
                    arrocharValues[intI] = new Ochar(arrcharValues[intI]);
                }

                objMain_O = arrocharValues;
                objKey_O = arrstrKeys;
            }
            else if (
                obj_I is KeyValuePair<String, char>
                )
            {
                KeyValuePair<String, char> kvpchar = (KeyValuePair<String, char>)obj_I;

                //                                          //Extract attributes.
                char charValue = kvpchar.Value;
                String strKey = kvpchar.Key;

                //                                          //Box primitive
                Ochar ocharValue = new Ochar(charValue);

                objMain_O = ocharValue;
                objKey_O = strKey;
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") +
                        " SOMETHING IS WRONG!!!, this type could not be processed with other char types");

                objMain_O = null;
                objKey_O = null;
            }
            /*END_CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertAndBoxTs(          //Convert and box ts.

            //                                              //ots, ots[], ots[,] or ots[, ,].
            //                                              //lstts, ... (one atribute generic) will convert to ots[].
            //                                              //dicts will convert to otsValues[] and strKeys[].
            //                                              //kvpts will convert to otsValue and strKey.
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //null, str[] (keys from dic) or or str (key form kvp).
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //ts type (single, array or generic).
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            /*END_CASE*/
            if (
                obj_I is DateTime
                )
            {
                //                                          //Box primitive
                Ots ots = new Ots((DateTime)obj_I);

                objMain_O = ots;
                objKey_O = null;
            }
            else if (
                obj_I is DateTime[]
                )
            {
                DateTime[] arrts = (DateTime[])obj_I;

                //                                          //Box primitives
                Ots[] arrots = new Ots[arrts.Length];
                for (int intI = 0; intI < arrts.Length; intI = intI + 1)
                {
                    arrots[intI] = new Ots(arrts[intI]);
                }

                objMain_O = arrots;
                objKey_O = null;
            }
            else if (
                obj_I is List<DateTime>
                )
            {
                List<DateTime> lstts = (List<DateTime>)obj_I;

                //                                          //Box primitives
                Ots[] arrots = new Ots[lstts.Count];
                for (int intI = 0; intI < lstts.Count; intI = intI + 1)
                {
                    arrots[intI] = new Ots(lstts[intI]);
                }

                objMain_O = arrots;
                objKey_O = null;
            }
            else if (
                obj_I is Queue<DateTime>
                )
            {
                Queue<DateTime> queuets = (Queue<DateTime>)obj_I;

                //                                          //Box primitives
                Ots[] arrots = new Ots[queuets.Count];
                int intI = 0;
                foreach (DateTime ts in queuets)
                {
                    arrots[intI] = new Ots(ts);

                    intI = intI + 1;
                }

                objMain_O = arrots;
                objKey_O = null;
            }
            else if (
                obj_I is Stack<DateTime>
                )
            {
                Stack<DateTime> stackts = (Stack<DateTime>)obj_I;

                //                                          //Box primitives
                Ots[] arrots = new Ots[stackts.Count];
                int intI = 0;
                foreach (DateTime ts in stackts)
                {
                    arrots[intI] = new Ots(ts);

                    intI = intI + 1;
                }

                objMain_O = arrots;
                objKey_O = null;
            }
            else if (
                obj_I is DateTime[,]
                )
            {
                DateTime[,] arr2ts = (DateTime[,])obj_I;

                //                                          //Box primitives
                Ots[,] arr2ots = new Ots[arr2ts.GetLength(0), arr2ts.GetLength(1)];
                for (int intI = 0; intI < arr2ts.GetLength(0); intI = intI + 1)
                {
                    for (int intJ = 0; intJ < arr2ts.GetLength(1); intJ = intJ + 1)
                    {
                        arr2ots[intI, intJ] = new Ots(arr2ts[intI, intJ]);
                    }
                }

                objMain_O = arr2ots;
                objKey_O = null;
            }
            else if (
                obj_I is DateTime[, ,]
                )
            {
                DateTime[, ,] arr3ts = (DateTime[, ,])obj_I;

                //                                          //Box primitives
                Ots[, ,] arr3ots = new Ots[arr3ts.GetLength(0), arr3ts.GetLength(1), arr3ts.GetLength(2)];
                for (int intI = 0; intI < arr3ts.GetLength(0); intI = intI + 1)
                {
                    for (int intJ = 0; intJ < arr3ts.GetLength(1); intJ = intJ + 1)
                    {
                        for (int intK = 0; intK < arr3ts.GetLength(2); intK = intK + 1)
                        {
                            arr3ots[intI, intJ, intK] = new Ots(arr3ts[intI, intJ, intK]);
                        }
                    }
                }

                objMain_O = arr3ots;
                objKey_O = null;
            }
            else if (
                obj_I is Dictionary<String, DateTime>
                )
            {
                Dictionary<String, DateTime> dicts = (Dictionary<String, DateTime>)obj_I;

                //                                          //Convert to arrays.
                DateTime[] arrtsValues = new DateTime[dicts.Count];
                dicts.Values.CopyTo(arrtsValues, 0);
                String[] arrstrKeys = new String[dicts.Count];
                dicts.Keys.CopyTo(arrstrKeys, 0);

                //                                          //Box primitives
                Ots[] arrotsValues = new Ots[arrtsValues.Length];
                for (int intI = 0; intI < arrtsValues.Length; intI = intI + 1)
                {
                    arrotsValues[intI] = new Ots(arrtsValues[intI]);
                }

                objMain_O = arrotsValues;
                objKey_O = arrstrKeys;
            }
            else if (
                obj_I is KeyValuePair<String, DateTime>
                )
            {
                KeyValuePair<String, DateTime> kvpts = (KeyValuePair<String, DateTime>)obj_I;

                //                                          //Extract attributes.
                DateTime tsValue = kvpts.Value;
                String strKey = kvpts.Key;

                //                                          //Box primitive
                Ots otsValue = new Ots(tsValue);

                objMain_O = otsValue;
                objKey_O = strKey;
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") +
                        " SOMETHING IS WRONG!!!, this type could not be processed with other ts types");

                objMain_O = null;
                objKey_O = null;
            }
            /*END_CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertSystemType(        //Convert system type.
            //                                              //Parameters have the following posibilities:
            //                                              //[ Main            Key             Object
            //                                              //  yyy               -             yyy-system(str,ts,...)
            //                                              //  arr?yyy           -             arr?yyy
            //                                              //  arryyy            -             lstyyy
            //                                              //  arryyyValue     arrstrKey       dicyyy
            //                                              //  yyyValue        strKey          kvpyyy
            //                                              //]

            //                                              //obj or arrobj
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //arrstrKey or strKey, only for 2 atribute generic, other
            //                                              //      will return null
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //Any standard object, except generic with atribute enum,
            //                                              //      bclass or btuple.
            /*NSTD*/Object/*END-NSTD*/ obj_I,
            Type typeContent_I
            )
        {
            //                                              //Select process.
            /*CASE*/
            if (
                typeContent_I == typeof(String)
                )
            {
                Test.subfunConvertStr(out objMain_O, out objKey_O, obj_I);
            }
            else if (
                typeContent_I == typeof(Type)
                )
            {
                Test.subfunConvertStr(out objMain_O, out objKey_O, obj_I);
            }
            else if (
                typeContent_I == typeof(DirectoryInfo)
                )
            {
                Test.subfunConvertSysdir(out objMain_O, out objKey_O, obj_I);
            }
            else if (
                typeContent_I == typeof(FileInfo)
                )
            {
                Test.subfunConvertSysfile(out objMain_O, out objKey_O, obj_I);
            }
            else if (
                typeContent_I == typeof(StreamReader)
                )
            {
                Test.subfunConvertSyssr(out objMain_O, out objKey_O, obj_I);
            }
            else if (
                typeContent_I == typeof(StreamWriter)
                )
            {
                Test.subfunConvertSyssw(out objMain_O, out objKey_O, obj_I);
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") + " SOMETHING IS WRONG!!!," +
                        " a case branch to process any form (single, array or generic)" +
                        " of this system type is missing");

                objMain_O = null;
                objKey_O = null;
            }
            /*END-CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertStr(               //Convert str.

            //                                              //str, str[], str[,] or str[, ,].
            //                                              //lststr, ... (one atribute generic) will convert to
            //                                              //      str[].
            //                                              //dicstr will convert to strValues[] and strKeys[].
            //                                              //kvpstr will convert to strValue and strKey.
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //null, str[] (keys from dic) or or str (key form kvp).
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //str type (single, array or generic).
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            /*END_CASE*/
            if (
                obj_I is List<String>
                )
            {
                List<String> lststr = (List<String>)obj_I;
                String[] arrstr = lststr.ToArray();

                objMain_O = arrstr;
                objKey_O = null;
            }
            else if (
                obj_I is Queue<String>
                )
            {
                Queue<String> queuestr = (Queue<String>)obj_I;
                String[] arrstr = queuestr.ToArray();

                objMain_O = arrstr;
                objKey_O = null;
            }
            else if (
                obj_I is Stack<String>
                )
            {
                Stack<String> stackstr = (Stack<String>)obj_I;
                String[] arrstr = stackstr.ToArray();

                objMain_O = arrstr;
                objKey_O = null;
            }
            else if (
                obj_I is Dictionary<String, String>
                )
            {
                Dictionary<String, String> dicsstr = (Dictionary<String, String>)obj_I;

                //                                          //Convert to arrays.
                String[] arrstrValues = new String[dicsstr.Count];
                dicsstr.Values.CopyTo(arrstrValues, 0);
                String[] arrstrKeys = new String[dicsstr.Count];
                dicsstr.Keys.CopyTo(arrstrKeys, 0);

                objMain_O = arrstrValues;
                objKey_O = arrstrKeys;
            }
            else if (
                obj_I is KeyValuePair<String, String>
                )
            {
                KeyValuePair<String, String> kvpstr = (KeyValuePair<String, String>)obj_I;

                //                                          //Extract attributes.
                String strValue = kvpstr.Value;
                String strKey = kvpstr.Key;

                objMain_O = strValue;
                objKey_O = strKey;
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") +
                        " SOMETHING IS WRONG!!!, this type could not be processed with other str types");

                objMain_O = null;
                objKey_O = null;
            }
            /*END_CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertSysdir(            //Convert sysdir.

            //                                              //sysdir, sysdir[], sysdir[,] or sysdir[, ,].
            //                                              //lstsysdir, ... (one atribute generic) will convert to
            //                                              //      sysdir[].
            //                                              //dicsysdir will convert to sysdirValues[] and sysdirKeys[].
            //                                              //kvpsysdir will convert to sysdirValue and sysdirKey.
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //null, sysdir[] (keys from dic) or or sysdir (key form
            //                                              //      kvp).
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //sysdir type (single, array or generic).
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            /*END_CASE*/
            if (
                obj_I is List<DirectoryInfo>
                )
            {
                List<DirectoryInfo> lstsysdir = (List<DirectoryInfo>)obj_I;
                DirectoryInfo[] arrsysdir = lstsysdir.ToArray();

                objMain_O = arrsysdir;
                objKey_O = null;
            }
            else if (
                obj_I is Queue<DirectoryInfo>
                )
            {
                Queue<DirectoryInfo> queuesysdir = (Queue<DirectoryInfo>)obj_I;
                DirectoryInfo[] arrsysdir = queuesysdir.ToArray();

                objMain_O = arrsysdir;
                objKey_O = null;
            }
            else if (
                obj_I is Stack<DirectoryInfo>
                )
            {
                Stack<DirectoryInfo> stacksysdir = (Stack<DirectoryInfo>)obj_I;
                DirectoryInfo[] arrsysdir = stacksysdir.ToArray();

                objMain_O = arrsysdir;
                objKey_O = null;
            }
            else if (
                obj_I is Dictionary<DirectoryInfo, DirectoryInfo>
                )
            {
                Dictionary<DirectoryInfo, DirectoryInfo> dicssysdir = (Dictionary<DirectoryInfo, DirectoryInfo>)obj_I;

                //                                          //Convert to arrays.
                DirectoryInfo[] arrsysdirValues = new DirectoryInfo[dicssysdir.Count];
                dicssysdir.Values.CopyTo(arrsysdirValues, 0);
                DirectoryInfo[] arrsysdirKeys = new DirectoryInfo[dicssysdir.Count];
                dicssysdir.Keys.CopyTo(arrsysdirKeys, 0);

                objMain_O = arrsysdirValues;
                objKey_O = arrsysdirKeys;
            }
            else if (
                obj_I is KeyValuePair<DirectoryInfo, DirectoryInfo>
                )
            {
                KeyValuePair<DirectoryInfo, DirectoryInfo> kvpsysdir = (KeyValuePair<DirectoryInfo, DirectoryInfo>)obj_I;

                //                                          //Extract attributes.
                DirectoryInfo sysdirValue = kvpsysdir.Value;
                DirectoryInfo sysdirKey = kvpsysdir.Key;

                objMain_O = sysdirValue;
                objKey_O = sysdirKey;
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") +
                        " SOMETHING IS WRONG!!!, this type could not be processed with other sysdir types");

                objMain_O = null;
                objKey_O = null;
            }
            /*END_CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertSysfile(           //Convert strfile.

            //                                              //strfile, strfile[], strfile[,] or strfile[, ,].
            //                                              //lststrfile, ... (one atribute generic) will convert to
            //                                              //      strfile[].
            //                                              //dicstrfile will convert to strfileValues[] and
            //                                              //      strfileKeys[].
            //                                              //kvpstrfile will convert to strfileValue and strfileKey.
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //null, strfile[] (keys from dic) or or strfile (key form
            //                                              //      kvp).
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //strfile type (single, array or generic).
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            /*END_CASE*/
            if (
                obj_I is List<FileInfo>
                )
            {
                List<FileInfo> lststrfile = (List<FileInfo>)obj_I;
                FileInfo[] arrstrfile = lststrfile.ToArray();

                objMain_O = arrstrfile;
                objKey_O = null;
            }
            else if (
                obj_I is Queue<FileInfo>
                )
            {
                Queue<FileInfo> queuestrfile = (Queue<FileInfo>)obj_I;
                FileInfo[] arrstrfile = queuestrfile.ToArray();

                objMain_O = arrstrfile;
                objKey_O = null;
            }
            else if (
                obj_I is Stack<FileInfo>
                )
            {
                Stack<FileInfo> stackstrfile = (Stack<FileInfo>)obj_I;
                FileInfo[] arrstrfile = stackstrfile.ToArray();

                objMain_O = arrstrfile;
                objKey_O = null;
            }
            else if (
                obj_I is Dictionary<FileInfo, FileInfo>
                )
            {
                Dictionary<FileInfo, FileInfo> dicsstrfile = (Dictionary<FileInfo, FileInfo>)obj_I;

                //                                          //Convert to arrays.
                FileInfo[] arrstrfileValues = new FileInfo[dicsstrfile.Count];
                dicsstrfile.Values.CopyTo(arrstrfileValues, 0);
                FileInfo[] arrstrfileKeys = new FileInfo[dicsstrfile.Count];
                dicsstrfile.Keys.CopyTo(arrstrfileKeys, 0);

                objMain_O = arrstrfileValues;
                objKey_O = arrstrfileKeys;
            }
            else if (
                obj_I is KeyValuePair<FileInfo, FileInfo>
                )
            {
                KeyValuePair<FileInfo, FileInfo> kvpstrfile = (KeyValuePair<FileInfo, FileInfo>)obj_I;

                //                                          //Extract attributes.
                FileInfo strfileValue = kvpstrfile.Value;
                FileInfo strfileKey = kvpstrfile.Key;

                objMain_O = strfileValue;
                objKey_O = strfileKey;
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") +
                        " SOMETHING IS WRONG!!!, this type could not be processed with other strfile types");

                objMain_O = null;
                objKey_O = null;
            }
            /*END_CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertSyssr(             //Convert syssr.

            //                                              //syssr, syssr[], syssr[,] or syssr[, ,].
            //                                              //lstsyssr, ... (one atribute generic) will convert to
            //                                              //      syssr[].
            //                                              //dicsyssr will convert to syssrValues[] and syssrKeys[].
            //                                              //kvpsyssr will convert to syssrValue and syssrKey.
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //null, syssr[] (keys from dic) or or syssr (key form kvp).
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //syssr type (single, array or generic).
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            /*END_CASE*/
            if (
                obj_I is List<StreamReader>
                )
            {
                List<StreamReader> lstsyssr = (List<StreamReader>)obj_I;
                StreamReader[] arrsyssr = lstsyssr.ToArray();

                objMain_O = arrsyssr;
                objKey_O = null;
            }
            else if (
                obj_I is Queue<StreamReader>
                )
            {
                Queue<StreamReader> queuesyssr = (Queue<StreamReader>)obj_I;
                StreamReader[] arrsyssr = queuesyssr.ToArray();

                objMain_O = arrsyssr;
                objKey_O = null;
            }
            else if (
                obj_I is Stack<StreamReader>
                )
            {
                Stack<StreamReader> stacksyssr = (Stack<StreamReader>)obj_I;
                StreamReader[] arrsyssr = stacksyssr.ToArray();

                objMain_O = arrsyssr;
                objKey_O = null;
            }
            else if (
                obj_I is Dictionary<StreamReader, StreamReader>
                )
            {
                Dictionary<StreamReader, StreamReader> dicssyssr = (Dictionary<StreamReader, StreamReader>)obj_I;

                //                                          //Convert to arrays.
                StreamReader[] arrsyssrValues = new StreamReader[dicssyssr.Count];
                dicssyssr.Values.CopyTo(arrsyssrValues, 0);
                StreamReader[] arrsyssrKeys = new StreamReader[dicssyssr.Count];
                dicssyssr.Keys.CopyTo(arrsyssrKeys, 0);

                objMain_O = arrsyssrValues;
                objKey_O = arrsyssrKeys;
            }
            else if (
                obj_I is KeyValuePair<StreamReader, StreamReader>
                )
            {
                KeyValuePair<StreamReader, StreamReader> kvpsyssr = (KeyValuePair<StreamReader, StreamReader>)obj_I;

                //                                          //Extract attributes.
                StreamReader syssrValue = kvpsyssr.Value;
                StreamReader syssrKey = kvpsyssr.Key;

                objMain_O = syssrValue;
                objKey_O = syssrKey;
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") +
                        " SOMETHING IS WRONG!!!, this type could not be processed with other syssr types");

                objMain_O = null;
                objKey_O = null;
            }
            /*END_CASE*/
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static void subfunConvertSyssw(             //Convert syssw.

            //                                              //syssw, syssw[], syssw[,] or syssw[, ,].
            //                                              //lstsyssw, ... (one atribute generic) will convert to
            //                                              //      syssw[].
            //                                              //dicsyssw will convert to sysswValues[] and sysswKeys[].
            //                                              //kvpsyssw will convert to sysswValue and sysswKey.
            out /*NSTD*/Object/*END-NSTD*/ objMain_O,
            //                                              //null, syssw[] (keys from dic) or or syssw (key form kvp).
            out /*NSTD*/Object/*END-NSTD*/ objKey_O,
            //                                              //syssw type (single, array or generic).
            /*NSTD*/Object/*END-NSTD*/ obj_I
            )
        {
            /*END_CASE*/
            if (
                obj_I is List<StreamWriter>
                )
            {
                List<StreamWriter> lstsyssw = (List<StreamWriter>)obj_I;
                StreamWriter[] arrsyssw = lstsyssw.ToArray();

                objMain_O = arrsyssw;
                objKey_O = null;
            }
            else if (
                obj_I is Queue<StreamWriter>
                )
            {
                Queue<StreamWriter> queuesyssw = (Queue<StreamWriter>)obj_I;
                StreamWriter[] arrsyssw = queuesyssw.ToArray();

                objMain_O = arrsyssw;
                objKey_O = null;
            }
            else if (
                obj_I is Stack<StreamWriter>
                )
            {
                Stack<StreamWriter> stacksyssw = (Stack<StreamWriter>)obj_I;
                StreamWriter[] arrsyssw = stacksyssw.ToArray();

                objMain_O = arrsyssw;
                objKey_O = null;
            }
            else if (
                obj_I is Dictionary<StreamWriter, StreamWriter>
                )
            {
                Dictionary<StreamWriter, StreamWriter> dicssyssw = (Dictionary<StreamWriter, StreamWriter>)obj_I;

                //                                          //Convert to arrays.
                StreamWriter[] arrsysswValues = new StreamWriter[dicssyssw.Count];
                dicssyssw.Values.CopyTo(arrsysswValues, 0);
                StreamWriter[] arrsysswKeys = new StreamWriter[dicssyssw.Count];
                dicssyssw.Keys.CopyTo(arrsysswKeys, 0);

                objMain_O = arrsysswValues;
                objKey_O = arrsysswKeys;
            }
            else if (
                obj_I is KeyValuePair<StreamWriter, StreamWriter>
                )
            {
                KeyValuePair<StreamWriter, StreamWriter> kvpsyssw = (KeyValuePair<StreamWriter, StreamWriter>)obj_I;

                //                                          //Extract attributes.
                StreamWriter sysswValue = kvpsyssw.Value;
                StreamWriter sysswKey = kvpsyssw.Key;

                objMain_O = sysswValue;
                objKey_O = sysswKey;
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") +
                        " SOMETHING IS WRONG!!!, this type could not be processed with other syssw types");

                objMain_O = null;
                objKey_O = null;
            }
            /*END_CASE*/
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Test.strAnalizeAndFormat set of private methods to format a single object*/
        //--------------------------------------------------------------------------------------------------------------
        private static String strAnalizeAndFormatCheckNulls(//Produces an object in string format.
            //                                              //Before calling strAnalizeAndFormatXxxx checks for null

            //                                              //str, object in string format, could be null.

            //                                              //Object to format
            /*NSTD*/Object/*END-NSTD*/ obj_I,
            //                                              //SHORT or FULL
            TestoptionEnum testoptionOption_I
            )
        {
            String strAnalizeAndFormatCheckNulls;
            if (
                obj_I == null
                )
            {
                strAnalizeAndFormatCheckNulls = "null";
            }
            else
            {
                /*CASE*/
                if (
                    obj_I is BboxBaseBoxingAbtract
                    )
                {
                    strAnalizeAndFormatCheckNulls = Test.strAnalizeAndFormatBbox((BboxBaseBoxingAbtract)obj_I);
                }
                else if (
                    obj_I is BclassBaseClassAbstract
                    )
                {
                    strAnalizeAndFormatCheckNulls =
                        Test.strAnalizeAndFormatBclass((BclassBaseClassAbstract)obj_I, testoptionOption_I);
                }
                else if (
                    obj_I is BtupleBaseTupleAbstract
                    )
                {
                    strAnalizeAndFormatCheckNulls =
                        Test.strAnalizeAndFormatBtuple((BtupleBaseTupleAbstract)obj_I, testoptionOption_I);
                }
                else if (
                    obj_I is Enum
                    )
                {
                    strAnalizeAndFormatCheckNulls = Test.strAnalizeAndFormatEnum((Enum)obj_I);
                }
                else if (
                    obj_I is Exception
                    )
                {
                    strAnalizeAndFormatCheckNulls = Test.strAnalizeAndFormatSysexcep((Exception)obj_I);
                }
                else
                {
                    strAnalizeAndFormatCheckNulls = Test.strAnalizeAndFormatSystemType(obj_I, testoptionOption_I);
                }
                /*END-CASE*/
            }

            return strAnalizeAndFormatCheckNulls;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatBbox(      //Produces an object in string format (boxed primitive)

            //                                              //str, object in string format.

            //                                              //bbox to format
            BboxBaseBoxingAbtract bbox_I
            )
        {
            String strAnalizeAndFormatBbox;
            /*CASE*/
            if (
                bbox_I is Oint
                )
            {
                strAnalizeAndFormatBbox = Test.strAnalizeAndFormatInt(((Oint)bbox_I).v);
            }
            else if (
                bbox_I is Olong
                )
            {
                strAnalizeAndFormatBbox = Test.strAnalizeAndFormatLong(((Olong)bbox_I).v);
            }
            else if (
                bbox_I is Onum
                )
            {
                strAnalizeAndFormatBbox = Test.strAnalizeAndFormatNum(((Onum)bbox_I).v);
            }
            else if (
                bbox_I is Obool
                )
            {
                strAnalizeAndFormatBbox = Test.strAnalizeAndFormatBool(((Obool)bbox_I).v);
            }
            else if (
                bbox_I is Ochar
                )
            {
                strAnalizeAndFormatBbox = Test.strAnalizeAndFormatChar(((Ochar)bbox_I).v);
            }
            else if (
                bbox_I is Ots
                )
            {
                strAnalizeAndFormatBbox = Test.strAnalizeAndFormatTs(((Ots)bbox_I).v);
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(bbox_I.GetType(), "bbox_I.GetType") +
                        " SOMETHING IS WRONG!!!, method strAnalizeAndFormatXxxx to process this bbox type is missing");

                strAnalizeAndFormatBbox = null;
            }
            /*END-CASE*/

            return strAnalizeAndFormatBbox;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatInt(       //Prepara un entero (long) para su despliege con información
            //                                              //      adicional si es mínimo o máximo.
            //                                              //Ejemplos:
            //                                              //1 -3,835.
            //                                              //2 -9,223,372,036,854,775,808<MinValue>.
            //                                              //3 9,223,372,036,854,775,807<MaxValue>.
            //                                              //str, String para despligue con información adicional.

            //                                              //Entero a desplegar.
            int int_I
            )
        {
            //                                              //Por lo pronto prepara sin información adicional.
            String strAnalizeAndFormatInt = int_I.ToString("#,##0");

            //                                              //Añade información adicional si es mínimo o máximo.
            if (
                int_I == Int32.MinValue
                )
            {
                strAnalizeAndFormatInt = strAnalizeAndFormatInt + "<MinValue>";
            }
            else if (
                int_I == Int32.MaxValue
                )
            {
                strAnalizeAndFormatInt = strAnalizeAndFormatInt + "<MaxValue>";
            }
            else
            {
                //                                          //Sin información adicional.
            }

            return strAnalizeAndFormatInt;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatLong(      //Prepara un entero (long) para su despliege con información
            //                                              //      adicional si es mínimo o máximo.
            //                                              //Ejemplos:
            //                                              //1 -3,835.
            //                                              //2 -9,223,372,036,854,775,808<MinValue>.
            //                                              //3 9,223,372,036,854,775,807<MaxValue>.
            //                                              //str, String para despligue con información adicional.

            //                                              //Entero a desplegar.
            long long_I
            )
        {
            //                                              //Por lo pronto prepara sin información adicional.
            String strAnalizeAndFormatLong = long_I.ToString("#,##0");

            //                                              //Añade información adicional si es mínimo o máximo.
            if (
                long_I == Int64.MinValue
                )
            {
                strAnalizeAndFormatLong = strAnalizeAndFormatLong + "<MinValue>";
            }
            else if (
                long_I == Int64.MaxValue
                )
            {
                strAnalizeAndFormatLong = strAnalizeAndFormatLong + "<MaxValue>";
            }
            else
            {
                //                                          //Sin información adicional.
            }

            return strAnalizeAndFormatLong;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatNum(       //Prepara un número para su despliege con información
            //                                              //      adicional si es mínimo, máximo, etc.
            //                                              //Ejemplos:
            //                                              //1 -1.23456789012345E+038.
            //                                              //2 -1.79769313486232E+308<MinValue>;
            //                                              //3 1.79769313486232E+308<MaxValue>;
            //                                              //4 NaN<0/0>;
            //                                              //5 -Infinity<-?/0>;
            //                                              //6 Infinity<?/0>;
            //                                              //str, String para despligue con información adicional.

            //                                              //Entero a desplegar.
            double num_I
            )
        {
            //                                              //Por lo pronto prepara sin información adicional.
            String strAnalizeAndFormatNum = num_I.ToString();

            //                                              //Añade información adicional si es mínimo o máximo.
            if (
                num_I == Double.MinValue
                )
            {
                strAnalizeAndFormatNum = strAnalizeAndFormatNum + "<MinValue>";
            }
            else if (
                num_I == Double.MaxValue
                )
            {
                strAnalizeAndFormatNum = strAnalizeAndFormatNum + "<MaxValue>";
            }
            else if (
                num_I == Double.NegativeInfinity
                )
            {
                strAnalizeAndFormatNum = strAnalizeAndFormatNum + "<-?/0.0>";
            }
            else if (
                num_I == Double.PositiveInfinity
                )
            {
                strAnalizeAndFormatNum = strAnalizeAndFormatNum + "<?/0.0>";
            }
            else if (
                //                                          //A number has 4 posibibilities:
                //                                          //1. Beetwen MinValue and MaxValue
                //                                          //2. NegativeInfinity, (-?/0.0).
                //                                          //3. PositiveInfinity, (?/0.0).
                //                                          //4. NaN, (0.0/0.0).
                //                                          //num_I == Double.NaN, DO NOT FUNCTION AS EXPECTED
                !((num_I >= Double.MinValue) && (num_I <= Double.MaxValue))
                )
            {
                strAnalizeAndFormatNum = strAnalizeAndFormatNum + "<0.0/0.0>";
            }
            else
            {
                //                                          //Sin información adicional.
            }

            return strAnalizeAndFormatNum;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatBool(      //Prepara un booleno para su despliege.
            //                                              //Ejemplos:
            //                                              //1 true.
            //                                              //2 false.
            //                                              //str, String para despligue.

            //                                              //Booleano a desplegar.
            bool bool_I
            )
        {
            //                                              //Se asigna true o false según corresponda.
            //                                              //Se hace de esta forma dado que el strTo() produce True
            //                                              //      y False (iniciando con mayúsculas que es distinto a
            //                                              //      las literales true y false).
            String strAnalizeAndFormatBool;
            if (
                bool_I
                )
            {
                strAnalizeAndFormatBool = "true";
            }
            else
            {
                strAnalizeAndFormatBool = "false";
            }

            return strAnalizeAndFormatBool;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatChar(      //Prepara un caracter para su despliege con información
            //                                              //      adicional si el es caracter extraño.
            //                                              //      caracter es extraño.
            //                                              //Ejemplos:
            //                                              //1 'c'.
            //                                              //2 '©'<0x00A9>.
            //                                              //3 '_'<0x0009, \t, Horizontal Tab>.
            //                                              //1) No tiene nada extraño, solo se añaden las comillas.
            //                                              //2) El caracter © no aparece en el teclado, incluyo su
            //                                              //      su hexadecimal.
            //                                              //3) El caracter es un Horizonal Tab, no es visible, se
            //                                              //      sustituye por _ (el caracter en 
            //                                              //      charSUBSTITUTE_NONVISIBLE), incluyo su hexadecimal y
            //                                              //      su descripción.
            //                                              //str, String para despligue con diagnostico si el

            //                                              //Caracter a analizar.
            char char_I
            )
        {
            //                                              //Determino tipo de caracter.
            TestchartypeEnum testchartype = Test.testchartypeKeyboardOrEtc(char_I);

            //                                              //Para formar lo que va a regresar, esto depende del tipo de
            //                                              //      caracter.
            String strAnalizeAndFormatChar;
            if (
                //                                          //Es visible.
                (testchartype == TestchartypeEnum.KEYBOARD) || (testchartype == TestchartypeEnum.VISIBLE_NONKEYBOARD)
                )
            {
                //                                          //Es visible, solo pone entre comillas
                strAnalizeAndFormatChar = "'" + char_I + "'";
            }
            else
            {
                //                                          //Procesa cuando no es visible.
                strAnalizeAndFormatChar = "'" + charSUBSTITUTE_NONVISIBLE + "'";
            }

            //                                              //Añade info de diagnóstico cuando no es del KEYBOARD.
            if (
                testchartype != TestchartypeEnum.KEYBOARD
                )
            {
                //                                          //Añade info de diagnóstico.

                //                                          //Formatea la tupla cuando no es visible (primera parte).
                strAnalizeAndFormatChar = strAnalizeAndFormatChar + "<" + "0x" + String.Format("{0:X4}", (int)char_I);

                if (
                    testchartype == TestchartypeEnum.NONVISIBLE_WITH_DESCRIPTION
                    )
                {
                    //                                      //Completa la tupla cuando tiene descripción.
                    int intDesctiption = Array.BinarySearch(arrcharNONVISIBLE, char_I);
                    strAnalizeAndFormatChar = strAnalizeAndFormatChar + ", " +
                        arrstrDESCRIPTION_NONVISIBLE[intDesctiption] + ">";
                }
                else
                {
                    //                                      //Completa la tupla cuando NO tiene descripción.
                    strAnalizeAndFormatChar = strAnalizeAndFormatChar + ">";
                }
            }

            return strAnalizeAndFormatChar;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatTs(        //Prepara un date, time o ts para su despliege.
            //                                              //Ejemplos:
            //                                              //1 2013-12-28.
            //                                              //2 2013-12-28 21:30:16.
            //                                              //3 2013-12-28T21:31:25.7030000-06:00.
            //                                              //str, String para despligue.

            //                                              //ts (date o time) a desplegar.
            //                                              //ts, si tiene milisegundos se asume que es ts.
            //                                              //time, si tiene hora, minuto y/o segundos se asume que es
            //                                              //      time.
            //                                              //date, si no fue ts o time.      
            DateTime ts_I
            )
        {
            //                                              //Se determina el formato que corresponde.
            String strAnalizeAndFormatTs;
            if (
                //                                          //Tiene milisegundos.
                ts_I.Millisecond != 0
                )
            {
                strAnalizeAndFormatTs = ts_I.ToString("o");
            }
            else if (
                //                                          //Tiene hora, minutos o segundos.
                (ts_I.Hour != 0) || (ts_I.Minute != 0) || (ts_I.Second != 0)
                )
            {
                strAnalizeAndFormatTs = ts_I.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                //                                          //Solo tiene fecha.
                strAnalizeAndFormatTs = ts_I.ToString("yyyy-MM-dd");
            }

            return strAnalizeAndFormatTs;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatBclass(    //Analize and format bclass (or subclass of bclass).
            //                                              //A bclass object should be display only once per run.
            //                                              //str, bclass formated to display.

            //                                              //Bclass to be analized and format
            BclassBaseClassAbstract bclass_I,
            //                                              //SHORT or FULL
            TestoptionEnum testoptionOption_I
            )
        {
            String strAnalizeAndFormatBclass;
            if (
                //                                          //Was processed before
                Test.lstobjPreviouslyProcessed.Contains(bclass_I)
                )
            {
                //                                          //Include only objId
                strAnalizeAndFormatBclass = Test.strGetObjId(bclass_I);
            }
            else
            {
                //                                          //Register as processed
                Test.lstobjPreviouslyProcessed.Add(bclass_I);

                if (
                    testoptionOption_I == TestoptionEnum.SHORT
                    )
                {
                    strAnalizeAndFormatBclass = bclass_I.strTo(TestoptionEnum.SHORT);
                }
                else
                {
                    strAnalizeAndFormatBclass = bclass_I.strTo();
                }
            }

            return strAnalizeAndFormatBclass;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatBtuple(    //Analize and format btuple (or subclass of btuple).
            //                                              //A btuple object should be display only once per run.
            //                                              //str, btuple formated to display.

            //                                              //Bclass to be analized and format
            BtupleBaseTupleAbstract btuple_I,
            //                                              //SHORT or FULL
            TestoptionEnum testoptionOption_I
            )
        {
            String strAnalizeAndFormatBtuple;
            if (
                //                                          //Was processed before
                Test.lstobjPreviouslyProcessed.Contains(btuple_I)
                )
            {
                //                                          //Include only objId
                strAnalizeAndFormatBtuple = Test.strGetObjId(btuple_I);
            }
            else
            {
                //                                          //Register as processed
                Test.lstobjPreviouslyProcessed.Add(btuple_I);

                if (
                    testoptionOption_I == TestoptionEnum.SHORT
                    )
                {
                    strAnalizeAndFormatBtuple = btuple_I.strTo(TestoptionEnum.SHORT);
                }
                else
                {
                    strAnalizeAndFormatBtuple = btuple_I.strTo();
                }
            }

            return strAnalizeAndFormatBtuple;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatEnum(      //Analize and format enum (or subclass of enum).
            //                                              //str, enum formated to display.

            //                                              //Enum to be analized and format
            Enum enum_I
            )
        {
            return enum_I.ToString();
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatSysexcep(  //Prepare a object to display.
            //                                              //str, sysexcep_I prepared to display.

            //                                              //Object to be analized and format
            Exception sysexcep_I
            )
        {
            return sysexcep_I.ToString();
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatSystemType(//Produces an object in string format (system type)

            //                                              //str, object in string format.

            //                                              //Object to format
            /*NSTD*/Object/*END-NSTD*/ obj_I,
            //                                              //SHORT or FULL
            TestoptionEnum testoptionOption_I
            )
        {
            String strAnalizeAndFormatSystemType;
            /*CASE*/
            if (
                obj_I is String
                )
            {
                strAnalizeAndFormatSystemType = Test.strAnalizeAndFormatStr((String)obj_I);
            }
            else if (
                obj_I is Type
                )
            {
                strAnalizeAndFormatSystemType = Test.strAnalizeAndFormatType((Type)obj_I, testoptionOption_I);
            }
            else if (
                obj_I is DirectoryInfo
                )
            {
                strAnalizeAndFormatSystemType = Test.strAnalizeAndFormatSysdir((DirectoryInfo)obj_I,
                    testoptionOption_I);
            }
            else if (
                obj_I is FileInfo
                )
            {
                strAnalizeAndFormatSystemType = Test.strAnalizeAndFormatSysfile((FileInfo)obj_I, testoptionOption_I);
            }
            else if (
                obj_I is StreamReader
                )
            {
                strAnalizeAndFormatSystemType = Test.strAnalizeAndFormatSyssr((StreamReader)obj_I, testoptionOption_I);
            }
            else if (
                obj_I is StreamWriter
                )
            {
                strAnalizeAndFormatSystemType = Test.strAnalizeAndFormatSyssw((StreamWriter)obj_I, testoptionOption_I);
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort(Test.strTo(obj_I.GetType(), "obj_I.GetType") + " SOMETHING IS WRONG!!!," +
                        " method strAnalizeAndFormatXxxx to process this system type is missing");

                strAnalizeAndFormatSystemType = null;
            }
            /*END-CASE*/

            return strAnalizeAndFormatSystemType;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatStr(       //Prepara un String para su despliege con información de
            //                                              //      caracteres que no son del KEYBOARD.
            //                                              //Ejemplos:
            //                                              //1. "Esto es lo que se analizo"<25>.
            //                                              //2. "©XYX"<4>{ <0, '©', 0x00A9 }.
            //                                              //3. "_XYX"<4>{ <0, '_', 0x0001> }.
            //                                              //4. "_XYX"<4>{ <0, '_', 0x0009, \t, Horizontal Tab> }.
            //                                              //1) Todo es del KEYBOARD, solo se añaden las comillas y su
            //                                              //      longitud.
            //                                              //2) El primer caracter © no aparece en el KEYBOARD, incluyo
            //                                              //      su hexadecimal.
            //                                              //3) El primer caracter es NONVISIBLE_WITHOUT_DESCRIPTION,
            //                                              //      se sustituye por _ (el caracter en
            //                                              //      charSUBSTITUTE_NONVISIBLE) e incluyo su hexadecimal.
            //                                              //4) El primer caracter es un Horizonal Tab, no es
            //                                              //      visible, se sustituye por _ (el caracter en
            //                                              //      charSUBSTITUTE_NONVISIBLE), incluyo su hexadecimal y
            //                                              //      su descripción.
            //                                              //Puede haber más de un caracter que no es del KEYBOARD, se
            //                                              //      añade "{ <.....>, <.....>, ..., <......> }".
            //                                              //Si no hay ningún caracter que no es del KEYBOARD, no se
            //                                              //      añade nada en esta parte, esto es no se añade "{ }",
            //                                              //      esto fue lo que sucedió en el ejemplo 1.
            //                                              //str, String para despligue con diagnostico de caracteres
            //                                              //      que no están en el KEYBOARD.

            //                                              //String a analizar.
            String str_I
            )
        {
            //                                              //Para formar lo que va a regresar.
            String strAnalizeAndFormatStr;
            if (
                //                                          //No hay String.
                str_I == null
                )
            {
                strAnalizeAndFormatStr = "null";
            }
            else
            {
                //                                          //Paso a arrchar para poder modificarlo.
                char[] arrcharToAnalize = str_I.ToCharArray();

                //                                          //Para conjunto de información de diagnóstico.
                List<String> lststrDiagnosticInfo = new List<String>();

                //                                          //Reviso todos los caracteres.
                for (int intI = 0; intI < arrcharToAnalize.Length; intI = intI + 1)
                {
                    //                                      //Paso un caracter a formato desplegable, el formato será:
                    //                                      //'c', KEYBOARD.
                    //                                      //'c'<0x1234>, VISIBLE_NONKEYBOARD.
                    //                                      //'_'<0x1234>, NONVISIBLE_WITHOUT_DESCRIPTION.
                    //                                      //'_'<0x1234, descripción>, NONVISIBLE_WITH_DESCRIPTION.
                    String strCharAnalized = Test.strAnalizeAndFormatChar(arrcharToAnalize[intI]);

                    //                                      //Si tiene información de diagnóstico la proceso.
                    if (
                        //                                  //Si tiene información de diagnóstico.
                        strCharAnalized.Length > 3
                        )
                    {
                        //                                  //Cambio caracter, la pos. 1 tiene el caracter revisado.
                        arrcharToAnalize[intI] = strCharAnalized[1];

                        //                                  //Debo formar un String:
                        //                                  //<n, 'c', 0x1234>, VISIBLE_NONKEYBOARD.
                        //                                  //<n, '_', 0x1234>, NONVISIBLE_WITHOUT_DESCRIPTION.
                        //                                  //<n, '_', 0x1234, descripción>,
                        //                                  //      NONVISIBLE_WITH_DESCRIPTION.
                        String strDiagnosticInfo = "<" + intI + ", " + strCharAnalized.Substring(0, 3) + ", " +
                            strCharAnalized.Substring(4);

                        //                                  //Añade info a la lista.
                        lststrDiagnosticInfo.Add(strDiagnosticInfo);
                    }
                }

                //                                          //Forma la longitud del String, solo de desea mostrar cuando
                //                                          //      excede intLONG_STRING.
                String strLongString;
                if (
                    str_I.Length > intLONG_STRING
                    )
                {
                    strLongString = "<" + str_I.Length + ">";
                }
                else
                {
                    strLongString = "";
                }

                //                                          //Forma el String a desplegar.
                if (
                    //                                      //No tiene ningún caracter con información de diagnóstico.
                    lststrDiagnosticInfo.Count == 0
                    )
                {
                    //                                      //Formatea cuando NO tiene información de diagnóstico.
                    strAnalizeAndFormatStr = "\"" + str_I + "\"" + strLongString;
                }
                else
                {
                    //                                      //Formatea cuando SI tiene información de diagnóstico.
                    strAnalizeAndFormatStr = "\"" + new String(arrcharToAnalize) + "\"" + "<" + arrcharToAnalize.Length +
                        ">" + "{ " + String.Join(", ", lststrDiagnosticInfo.ToArray()) + " }";
                }
            }

            return strAnalizeAndFormatStr;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatType(      //Prepare a object to display.
            //                                              //str, type prepared to display.

            //                                              //Object to be analized and format
            Type type_I,
            //                                              //SHORT or FULL
            TestoptionEnum testoptionOption_I
            )
        {
            String strAnalizeAndFormatType;
            if (
                testoptionOption_I == TestoptionEnum.SHORT
                )
            {
                strAnalizeAndFormatType = "<" + type_I.Name + ">";
            }
            else
            {
                strAnalizeAndFormatType = "<Name(" + type_I.Name + ")>";
            }

            return strAnalizeAndFormatType;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatSysdir(    //Prepare a object to display.
            //                                              //str, sysdir prepared to display.

            //                                              //Object to be analized and format
            DirectoryInfo sysdir_I,
            //                                              //SHORT or FULL
            TestoptionEnum testoptionOption_I
            )
        {
            String strAnalizeAndFormatSysdir;
            if (
                testoptionOption_I == TestoptionEnum.SHORT
                )
            {
                strAnalizeAndFormatSysdir = "<" + sysdir_I.FullName + ", " + sysdir_I.Exists;
                if (
                    sysdir_I.Exists
                    )
                {
                    strAnalizeAndFormatSysdir = strAnalizeAndFormatSysdir + ", " + sysdir_I.CreationTimeUtc + ", " +
                        sysdir_I.LastAccessTimeUtc + ", " + sysdir_I.LastWriteTimeUtc;
                }
                strAnalizeAndFormatSysdir = strAnalizeAndFormatSysdir + ">";
            }
            else
            {
                strAnalizeAndFormatSysdir = "<FullName(" + sysdir_I.FullName + "), Exists(" + sysdir_I.Exists;
                if (
                    sysdir_I.Exists
                    )
                {
                    strAnalizeAndFormatSysdir = strAnalizeAndFormatSysdir + "), CreationTimeUtc(" +
                        sysdir_I.CreationTimeUtc + "), LastAccessTimeUtc(" + sysdir_I.LastAccessTimeUtc +
                        "), LastWriteTimeUtc(" + sysdir_I.LastWriteTimeUtc;
                }
                strAnalizeAndFormatSysdir = strAnalizeAndFormatSysdir + ")>";
            }

            return strAnalizeAndFormatSysdir;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatSysfile(   //Prepare a object to display.
            //                                              //str, sysfile prepared to display.

            //                                              //Object to be analized and format
            FileInfo sysfile_I,
            //                                              //SHORT or FULL
            TestoptionEnum testoptionOption_I
            )
        {
            String strAnalizeAndFormatSysfile;
            if (
                testoptionOption_I == TestoptionEnum.SHORT
                )
            {
                strAnalizeAndFormatSysfile = "<" + sysfile_I.FullName + ", " + sysfile_I.Exists;
                if (
                    sysfile_I.Exists
                    )
                {
                    strAnalizeAndFormatSysfile = strAnalizeAndFormatSysfile + ", " + sysfile_I.Length + ", " +
                        sysfile_I.CreationTimeUtc + ", " + sysfile_I.LastAccessTimeUtc + ", " +
                        sysfile_I.LastWriteTimeUtc;
                }
                strAnalizeAndFormatSysfile = strAnalizeAndFormatSysfile + ">";
            }
            else
            {
                strAnalizeAndFormatSysfile = "<FullName(" + sysfile_I.FullName + "), Exists(" + sysfile_I.Exists;
                if (
                    sysfile_I.Exists
                    )
                {
                    strAnalizeAndFormatSysfile = strAnalizeAndFormatSysfile + "), Length(" + sysfile_I.Length + 
                        "), CreationTimeUtc(" + sysfile_I.CreationTimeUtc + "), LastAccessTimeUtc(" +
                        sysfile_I.LastAccessTimeUtc + "), LastWriteTimeUtc(" + sysfile_I.LastWriteTimeUtc;
                }
                strAnalizeAndFormatSysfile = strAnalizeAndFormatSysfile + ")>";
            }

            return strAnalizeAndFormatSysfile;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatSyssr(     //Prepare a object to display.
            //                                              //str, syssr prepared to display.

            //                                              //Object to be analized and format
            StreamReader syssr_I,
            //                                              //SHORT or FULL
            TestoptionEnum testoptionOption_I
            )
        {
            String strAnalizeAndFormatSyssr;
            if (
                testoptionOption_I == TestoptionEnum.SHORT
                )
            {
                strAnalizeAndFormatSyssr = "<" + syssr_I.CurrentEncoding + ", " + syssr_I.EndOfStream + ">";
            }
            else
            {
                strAnalizeAndFormatSyssr = "<CurrentEncoding(" + syssr_I.CurrentEncoding + "), EndOfStream(" +
                    syssr_I.EndOfStream + ")>";
            }

            return strAnalizeAndFormatSyssr;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private static String strAnalizeAndFormatSyssw(     //Prepare a object to display.
            //                                              //str, syssw prepared to display.

            //                                              //Object to be analized and format
            StreamWriter syssw_I,
            //                                              //SHORT or FULL
            TestoptionEnum testoptionOption_I
            )
        {
            String strAnalizeAndFormatSyssw;
            if (
                testoptionOption_I == TestoptionEnum.SHORT
                )
            {
                strAnalizeAndFormatSyssw = "<" + syssw_I.Encoding + ">";
            }
            else
            {
                strAnalizeAndFormatSyssw = "<Encoding(" + syssw_I.Encoding + ")>";
            }

            return strAnalizeAndFormatSyssw;
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Test.Trace Support to implement a trace*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //Implementación de apoyos para poder efectuar un Trece en
        //                                                  //      en una apliación.
        //                                                  //¿Cómo?.
        //                                                  //En el código "driver" para ejecutar la prueba, en
        //                                                  //      PruebaFase2.cs, llamar al método:
        //                                                  //Test.subSetLog(sysswLog);
        //                                                  //Dentro del código que se desea aplicar el trace en los
        //                                                  //      puntos que se crea conveniente, añadir:
        //                                                  //Test.subTrace(true, strLabel, intNivel, String a trace);
        //                                                  //Imprimie el log que contendrá el trace y otra información
        //                                                  //      de la prueba 

        //--------------------------------------------------------------------------------------------------------------
        /*CONSTANTS*/

        //--------------------------------------------------------------------------------------------------------------
        /*STATIC VARIABLES*/

        //                                                  //Log para trace.
        //                                                  //Se asigna un syssw con SubSetLog(sysswLog).
        private static StreamWriter sysswLogTrace;

        //                                                  //Cada Trace que se genere tendra un número único 1, 2, 3,
        //                                                  //      etc. (esto es, su secuencia).
        //                                                  //Antes de generar un nuevo trace se debe incrementar.
        private static int inTraceSequence;

        //--------------------------------------------------------------------------------------------------------------
        /*STATIC CONSTRUCTOR SUPPORT METHODS*/

        //--------------------------------------------------------------------------------------------------------------
        private static void subPrepareConstantsSubTrace(   //Initialiaze trace state.
            )
        {
            //                                              //Aún no hay log para trace definido.
            sysswLogTrace = null;

            //                                              //Inicia la cuenta de Trace que se genera.
            inTraceSequence = 0;
        }

        //--------------------------------------------------------------------------------------------------------------
        /*SHARED METHODS*/

        //--------------------------------------------------------------------------------------------------------------
        public static void subSetLog(                       //Asigna un log para el trace (seguira en off).

            //                                              //Log para trace.
            StreamWriter sysswLogTrace_T
            )
        {
            sysswLogTrace = sysswLogTrace_T;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subTrace(                        //Genera un trace a writeline.


            //                                              //true, se desea generar el trace.
            //                                              //false, No se genera el trace.
            //                                              //Se incluye este parámetro para sin tener que eliminar la
            //                                              //      la ejecución del trace poder activarlo/desactivarlo.
            bool boolIsTraceOn_I,
            //                                              //Etiqueta para identificar el registro del trace en la
            //                                              //      impresión. Cada instrucción trace que se agregue al
            //                                              //      código debe tener una etiqueta distinta.
            String strLabel_I,
            //                                              //Información a incluir en el trace, esta información se le
            //                                              //      da forma similar a los strTo.
            String strInfoTrace_I
            )
        {
            if (
                sysswLogTrace == null
                )
                Tools.subAbort(Test.strTo(sysswLogTrace, "sysswLogTrace") + " should be created and asigned");

            //                                              //Solo se procesa el trace si esta en ON.
            if (
                boolIsTraceOn_I
                )
            {
                //                                          //Avanza una secuencia (esta es la secuencia única de este
                //                                          //      trace).
                inTraceSequence = inTraceSequence + 1;

                //                                          //Produce trace.
                sysswLogTrace.WriteLine(">>> " + inTraceSequence + " <<< " + strLabel_I);
                sysswLogTrace.WriteLine(strInfoTrace_I);
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Test.Abort Support for testing subAbort*/

        //                                                  //Implementación de apoyos para poder efectuar pruebas de
        //                                                  //      subAbort y regitrar su información en un log.
        //                                                  //¿Cómo?.
        //                                                  //En el código "driver" para ejecutar la prueba (ej. en
        //                                                  //      Test Sys01.cs), llamar al método:
        //                                                  //Test.subSetTestAbort(); o.
        //                                                  //Test.subResetTestAbort(); o.

        //--------------------------------------------------------------------------------------------------------------
        /*CONSTANTS*/

        //--------------------------------------------------------------------------------------------------------------
        /*STATIC VARIABLES*/

        //                                                  //Indicador de se desea test.
        private static bool boolTestAbortOn;

        //--------------------------------------------------------------------------------------------------------------
        /*STATIC CONSTRUCTOR SUPPORT METHODS*/

        //--------------------------------------------------------------------------------------------------------------
        private static void subPrepareConstantsTestAbort(   //Intialize test state.
            )
        {
            Test.boolTestAbortOn = false;
        }

        //--------------------------------------------------------------------------------------------------------------
        /*SHARED METHODS*/

        //--------------------------------------------------------------------------------------------------------------
        public static void subSetTestAbort(                 //Marca que desea test.
            )
        {
            Test.boolTestAbortOn = true;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subResetTestAbort(               //Marca que desea concluir test.
            )
        {
            Test.boolTestAbortOn = false;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsTestAbortOn(               //Determina si se desea Test Abort.
            )
        {
            return Test.boolTestAbortOn;
        }
        /*END-TASK*/

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
