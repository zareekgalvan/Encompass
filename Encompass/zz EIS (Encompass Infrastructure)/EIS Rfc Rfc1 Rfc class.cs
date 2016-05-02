/*TASK  RFCAbs*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using TowaInfrastructure;
using AllInMemoryBase;

//                                                          //AUTHOR: Towa (ADGG-Ángel González,
//                                                          //      AGV-Armando Galván, ARC-Alejandro de la Rosa).
//                                                          //CO-AUTHOR: Towa (GLG-Gerardo López).
//                                                          //DATE: 9-Julio-2015.
//                                                          //PURPOSE:
//                                                          //Implementación
//                                                          //Clase para Registro Federal de Contribuyentes (RFC).

namespace EncompassInfrastructure
{
    //==================================================================================================================
    public abstract class RfcRFCAbstract:BclassBaseClassAbstract
    //                                                      //Base para las clases concretas de Persona Moral y Física.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //En la parte concreta se deben incluir las siguientes
        //                                                  //      constantes requeridas para validación (no requieren
        //                                                  //      ser usadas en la parte abstracta).

        //                                                  //strCHAR_USEFUL_IN_INITIAL, caracteres útiles para el
        //                                                  //      inicio, serán letras mayúsculas (sin Ñ), en el caso
        //                                                  //      de personas morales también "&".
        //                                                  //arrcharUSEFUL_IN_INITIAL, lo anterior convertido a arreglo
        //                                                  //      y ordenado.

        //                                                  //strCHAR_USEFUL_IN_HOMOCLAVE, caracteres útiles para la
        //                                                  //      homoclave, serán letras mayúsculas y dígitos.
        //                                                  //arrcharUSEFUL_IN_HOMOCLAVE, lo anterior convertido a
        //                                                 //      arreglo y ordenado.

        //--------------------------------------------------------------------------------------------------------------
        protected static void subPrepareConstants(          //LOS CONSTRUCTORES ESTÁTICOS DE LAS CLASES RfcxxXxxxx
            //                                              //      DEBEN LLAMAR A ESTE MÉTODO PARA PREPARAR SU
            //                                              //      INFORMACIÓN.
            //                                              //Ordena los arreglos de caracteres útiles para la
            //                                              //      primera parte del RFC y para la parte del Homoclave.

            //                                              //Caracteres útiles para la primera parte del RFC.
            String strCHAR_USEFUL_IN_INITIAL_I,
            //                                              //Caracteres USEFUL IN INITIAL ordenados.
            out char[] arrcharUSEFUL_IN_INITIAL_O,
            //                                              //Caracteres útiles para la parte del Homoclave del RFC.
            String strCHAR_USEFUL_IN_HOMOCLAVE_I,
            //                                              //Caracteres USEFUL IN HOMOCLAVE ordenados.
            out char[] arrcharUSEFUL_IN_HOMOCLAVE_O
            )
        {
            //                                              //Ordena caracteres USEFUL IN INITIAL.
            RfcRFCAbstract.subPrepareUseful(strCHAR_USEFUL_IN_INITIAL_I, out arrcharUSEFUL_IN_INITIAL_O);

            //                                              //Ordena caracteres USEFUL IN HOMOCLAVE.
            RfcRFCAbstract.subPrepareUseful(strCHAR_USEFUL_IN_HOMOCLAVE_I, out arrcharUSEFUL_IN_HOMOCLAVE_O);

        }

        //--------------------------------------------------------------------------------------------------------------
        private static void subPrepareUseful(               //Ordena caracteres útiles.

            //                                              //Caracteres útiles en el RFC.
            String strCHAR_USEFUL_I,
            //                                              //Caracteres USEFUL ordenados.
            out char[] arrcharUSEFUL_O
            )
        {
            if (
                strCHAR_USEFUL_I == null
                )
                Tools.subAbort("strCHAR_USEFUL_I es null");
            if (
                strCHAR_USEFUL_I == ""
                )
                Tools.subAbort("strCHAR_USEFUL_I(" + strCHAR_USEFUL_I + ") no tiene nada");

            arrcharUSEFUL_O = strCHAR_USEFUL_I.ToCharArray();
            Array.Sort(arrcharUSEFUL_O);

            //                                              //Verifica que no haya caracteres duplicados.
            for (int intI = 1; intI < arrcharUSEFUL_O.Length; intI = intI + 1)
            {
                if (
                    //                                      //Esta duplicado.
                    arrcharUSEFUL_O[intI] == arrcharUSEFUL_O[intI - 1]
                    )
                    Tools.subAbort(Test.strTo(arrcharUSEFUL_O, TestoptionEnum.SHORT) + "," + 
                        Test.strTo(intI, TestoptionEnum.SHORT) + 
                        ") tiene carácteres duplicados");
            }
        }
        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------
        private readonly String strRfc_Z;
        internal String strRfc { get { return strRfc_Z; } }

        //--------------------------------------------------------------------------------------------------------------
        internal void subReset()
        {
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo(TestoptionEnum testoptionSHORT_I)
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "[" + base.strTo(TestoptionEnum.SHORT) + ", " +
                Test.strTo(this.strRfc, TestoptionEnum.SHORT) + "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public override String strTo()
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "{" + Test.strTo(this.strRfc, TestoptionEnum.SHORT) + ", " +
                 "}" + "==>" + base.strTo();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Rfc";

            String strToString =
                strCLASS + "{" + Test.strTo(this.strRfc, "strRfc") + "}";

            return strToString;
        }

        //--------------------------------------------------------------------------------------------------------------


        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal RfcRFCAbstract(                            //this.*[O], asigna valores.

            String strRfc_I
            )
        {
            this.strRfc_Z = strRfc_I;
        }

        //--------------------------------------------------------------------------------------------------------------

        /*OTHER SUPPORT METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        protected static bool boolIsValid(                  //Determina si parece ser válida la estructura del RFC de
            //                                              //      Persona Moral (Ej. VED100111P38) o de Persona
            //                                              //      Física (Ej. LOGG510818IBA).
            //                                              //1. Para el caso de Persona Moral:
            //                                              //      1.1 La parte incial "VED" debe contener sólo
            //                                              //          caracteres útiles para la parte inicial
            //                                              //          ("&" es útil).
            //                                              //      1.2 La parte númerica "100111" debe ser una fecha
            //                                              //          válida.
            //                                              //      1.3 La homoclave "P38" debe contener sólo caracteres
            //                                              //          útiles para homoclave.
            //                                              //2. Para el caso de Persona Física:
            //                                              //      2.1 La parte incial "LOGG" debe contener sólo
            //                                              //          caracteres útiles para la parte inicial.
            //                                              //      2.2 La parte númerica "510818" debe ser una fecha
            //                                              //          válida.
            //                                              //      2.3 La homoclave "IBA" debe contener sólo caracteres
            //                                              //          útiles para homoclave.

            //                                              //Arreglo ordenado de caracteres útiles para la primera
            //                                              //      parte del RFC.
            char[] arrcharUSEFUL_IN_INITIALS_I,
            //                                              //Contiene los primeros 3 o 4 caracteres del RFC.
            String strRfcInitial_I,
            //                                              //Contiene 6 dígitos que representan la fecha en el RFC.
            String strRfcDate_I,
            //                                              //Arreglo ordenado de caracteres útiles para la parte
            //                                              //      de la Homoclave del RFC.
            char[] arrcharUSEFUL_IN_HOMOCLAVE_I,
            //                                              //Contiene los últimos 6 caracteres del RFC, los cuales
            //                                              //      representan la Homoclave.
            String strRfcHomoclave_I
            )
        {
            bool boolIsValid;
            boolIsValid = true;

            //                                              //Aquí se revisa la parte inicial del RFC, buscando los
            //                                              //      caracteres de este en arrcharUSEFUL_IN_INITIALS_I.
            int intI = 0;
            /*UNTIL-DO*/
            while (!(
                //                                          //Ya no hay más caracteres.
                (intI >= strRfcInitial_I.Length) ||
                //                                          //El caracter no es válido.
                (Array.BinarySearch(arrcharUSEFUL_IN_INITIALS_I, strRfcInitial_I[intI]) < 0)
                ))
            {
                intI = intI + 1;
            }

            //                                              //En caso de que el caracter no sea válido, declara
            //                                              //      boolIsValid como false.
            if (
                intI < strRfcInitial_I.Length
                )
            {
                boolIsValid = false;
            }

            //                                              //Aquí se revisa la parte de la fecha en el RFC.
            //                                              //Separa strRfcDate en Día, Mes y Año.
            //                                              //Separa la parte de Año.
            String strYear = "20" + strRfcDate_I.Substring(0, 2);
            //                                              //Separa la parte de Mes.
            String strMonth = strRfcDate_I.Substring(2, 2);
            //                                              //Separa la parte de Día.
            String strDay = strRfcDate_I.Substring(4, 2);

            //                                              //Crea el String con el formato necesario para crear un
            //                                              //      objeto DateTime. (Ej. dd/mm/yyyy 12:00)
            String strDate = strDay + "/" + strMonth + "/" + strYear + " 12:00";

            //                                              //En caso de que el strRfcDate_I no sea una fecha válida,
            //                                              //      declara boolIsValid como false.
            DateTime date;
            if (
                //                                          //Comprueba que sea una fecha válida.
                !(DateTime.TryParse(strDate, out date))
                )
            {
                boolIsValid = false;
            }

            //                                              //Aquí se revisa la parte final del RFC, buscando los
            //                                              //      caracteres de este en arrcharUSEFUL_IN_HOMOCLAVE_I.
            int intH = 0;
            /*UNTIL-DO*/
            while (!(
                //                                          //Ya no hay más caracteres.
                (intH >= strRfcHomoclave_I.Length) ||
                //                                          //El caracter no es válido.
                (Array.BinarySearch(arrcharUSEFUL_IN_HOMOCLAVE_I, strRfcHomoclave_I[intH]) < 0)
                ))
            {
                intH = intH + 1;
            }

            //                                              //En caso de que el caracter no sea válido, declara
            //                                              //      boolIsValid como false.
            if (
                intH < strRfcHomoclave_I.Length
                )
            {
                boolIsValid = false;
            }

            return boolIsValid;
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public class RfcpmPersonaMoral : RfcRFCAbstract         //Ej. VED100229P38 para Valores Empresariales 2010, SA CV.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------
        private const BclassmutabilityEnum bclassmutability_Z = BclassmutabilityEnum.MUTABLE;
        public override BclassmutabilityEnum bclassmutability { get { return RfcpmPersonaMoral.bclassmutability_Z; } }



        //                                                  //El siguiente String son caracteres útiles en el inicio de
        //                                                  //      un RFC de persona moral.
        private const String strCHAR_USEFUL_IN_INITIAL = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "&";
        //                                                  //Información anterior en un arreglo ordenado.
        private static readonly char[] arrcharUSEFUL_IN_INITIAL;

        //                                                  //El siguiente String son caracteres utiles en homoclave.
        private const String strCHAR_USEFUL_IN_HOMOCLAVE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "0123456789";
        //                                                  //Información anterior en un arreglo ordenado.
        private static readonly char[] arrcharUSEFUL_IN_HOMOCLAVE;

        //--------------------------------------------------------------------------------------------------------------
        static RfcpmPersonaMoral(
            )
        {
            RfcRFCAbstract.subPrepareConstants(RfcpmPersonaMoral.strCHAR_USEFUL_IN_INITIAL,
                out RfcpmPersonaMoral.arrcharUSEFUL_IN_INITIAL, RfcpmPersonaMoral.strCHAR_USEFUL_IN_HOMOCLAVE,
                out RfcpmPersonaMoral.arrcharUSEFUL_IN_HOMOCLAVE);
        }

        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Rfcpm";

            return strCLASS + "{}" + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal RfcpmPersonaMoral(                         //this.*[O], asigna valores.

            String strRfc_I
            )
            : base(strRfc_I)
        {
            if (
                !RfcpmPersonaMoral.boolIsValid(this.strRfc)
                )
                Tools.subAbort(Test.strTo(strRfc, TestoptionEnum.SHORT) + ") es inválido");
        }

        //--------------------------------------------------------------------------------------------------------------

        /*OTHER SUPPORT METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsValid(                     //Determina si parece ser válida la estructura del RFC de
            //                                              //      Persona Moral (Ej. VED100111P38).
            //                                              //1. La parte incial "VED" debe contener sólo caracteres
            //                                              //      útiles para la parte inicial ("&" es útil).
            //                                              //2. La parte númerica "100111" debe ser una fecha válida.
            //                                              //3. La homoclave "P38" debe contener sólo caracteres útiles
            //                                              //      para homoclave.

            //                                              //Contiene el RFC de persona moral (Ej. VED100111P38).
            String strRfcPersonaMoral_I
            )
        {
            bool boolIsValid;
            if (
                strRfcPersonaMoral_I.Length != 12
                )
            {
                boolIsValid = false;
            }
            else
            {
                boolIsValid =
                    RfcRFCAbstract.boolIsValid(RfcpmPersonaMoral.arrcharUSEFUL_IN_INITIAL,
                        strRfcPersonaMoral_I.Substring(0, 3), strRfcPersonaMoral_I.Substring(3, 6),
                        RfcpmPersonaMoral.arrcharUSEFUL_IN_HOMOCLAVE, strRfcPersonaMoral_I.Substring(9));
            }

            return boolIsValid;
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public class RfcpfPersonaFisica : RfcRFCAbstract         //Ej. LOGG510818IBA para Gerardo López García.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------
        private const BclassmutabilityEnum bclassmutability_Z = BclassmutabilityEnum.MUTABLE;
        public override BclassmutabilityEnum bclassmutability { get { return RfcpfPersonaFisica.bclassmutability_Z; } }


        //                                                  //El siguiente String son caracteres útiles en el inicio de
        //                                                  //      un RFC de persona física.
        private const String strCHAR_USEFUL_IN_INITIAL = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //                                                  //Información anterior en un arreglo ordenado.
        private static readonly char[] arrcharUSEFUL_IN_INITIAL;

        //                                                  //El siguiente String son caracteres útiles en homoclave.
        private const String strCHAR_USEFUL_IN_HOMOCLAVE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "0123456789";
        //                                                  //Información anterior en un arreglo ordenado.
        private static readonly char[] arrcharUSEFUL_IN_HOMOCLAVE;

        //--------------------------------------------------------------------------------------------------------------
        static RfcpfPersonaFisica(
            )
        {
            RfcRFCAbstract.subPrepareConstants(RfcpfPersonaFisica.strCHAR_USEFUL_IN_INITIAL,
                out RfcpfPersonaFisica.arrcharUSEFUL_IN_INITIAL, RfcpfPersonaFisica.strCHAR_USEFUL_IN_HOMOCLAVE,
                out RfcpfPersonaFisica.arrcharUSEFUL_IN_HOMOCLAVE);
        }

        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Rfcpf";

            return strCLASS + "{}" + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal RfcpfPersonaFisica(                        //this.*[O], asigna valores.

            String strRfc_I
            )
            : base(strRfc_I)
        {
            if (
                !RfcpfPersonaFisica.boolIsValid(this.strRfc)
                )
                Tools.subAbort(Test.strTo(strRfc, TestoptionEnum.SHORT) + ") es inválido");
        }

        //--------------------------------------------------------------------------------------------------------------

        /*OTHER SUPPORT METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsValid(                     //Determina si parece ser válida la estructura del RFC de
            //                                              //      Persona Física (Ej. LOGG510818IBA).
            //                                              //1. La parte incial "LOGG" debe contener sólo caracteres
            //                                              //      útiles para la parte inicial.
            //                                              //2. La parte númerica "510818" debe ser una fecha válida.
            //                                              //3. La homoclave "IBA" debe contener sólo caracteres útiles
            //                                              //      para homoclave.

            //                                              //Contiene el RFC de persona física (Ej. LOGG510818IBA).

            String strRfcPersonaFisica_I
            )
            {
                bool boolIsValid;
                if (
                    strRfcPersonaFisica_I.Length != 13
                    )
                {
                    boolIsValid = false;
                }
                else
                {
                    boolIsValid =
                        RfcRFCAbstract.boolIsValid(RfcpfPersonaFisica.arrcharUSEFUL_IN_INITIAL,
                            strRfcPersonaFisica_I.Substring(0, 4), strRfcPersonaFisica_I.Substring(4, 7),
                            RfcpfPersonaFisica.arrcharUSEFUL_IN_HOMOCLAVE, strRfcPersonaFisica_I.Substring(10));
                }

                return boolIsValid;
            }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
