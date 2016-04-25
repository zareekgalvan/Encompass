/*TASK Rpss1 RpssRegistroPatronalImss class*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using TowaInfrastructure;

//                                                          //AUTHOR: Towa (ADGG-Ángel González,
//                                                          //      AGV-Armando Galván, ARC-Alejandro de la Rosa).
//                                                          //CO-AUTHOR: Towa (GLG-Gerardo López).
//                                                          //DATE: 24-Julio-2015.
//                                                          //PURPOSE:
//                                                          //Implementación

namespace EncompassInfrastructure
{
    //==================================================================================================================
    public /*INMUTABLE*/ class RpssRegistroPatronalImss     //Guardar en forma robusta el Registro Patronal del IMSS.
    {
        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //Identificación de un patrón (persona física o moral que
        //                                                  //      tiene empleados) ante el IMSS.
        //                                                  //Se forma con una letra (indica la región, Monterrey es Y)
        //                                                  //      10 dígitos.
        private readonly String strRegistroPatronalImss_Z;
        public String strRegistroPatronalImss { get { return this.strRegistroPatronalImss_Z; } }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        internal void subReset()
        {
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public override String ToString()
        {
            const String strCLASS = "Rpss";

            return strCLASS + "{strRegistroPatronalImss(" + Test.strToDisplay(this.strRegistroPatronalImss) + ")}";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        /*CONSTRUCTORS*/

        //--------------------------------------------------------------------------------------------------------------
        public RpssRegistroPatronalImss(                    //Crea objeto con información
            //                                              //this.*[O], asigna valores.

            String strRegistroPatronalImss_I
            )
        {
            if (
                !RpssRegistroPatronalImss.boolIsValid(this.strRegistroPatronalImss)
                )
                Tools.subAbort("this.strRegistroPatronalImss(" + Test.strToDisplay(this.strRegistroPatronalImss) + 
                    ") es invalido");

            this.strRegistroPatronalImss_Z = strRegistroPatronalImss_I;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        /*OTHER SUPORT METHODS*/

        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsValid(                     //Determina si parece ser valida la estructura del IMSS de
            //                                              //      Registro Patronal (Ej. F9023759281).
            //                                              //1. Inicia con una letra.
            //                                              //2. Después 10 dígitos.

            //                                              //String a validar
            String strRegistroPatronalImss_I
            )
        {
            bool boolIsValid;
            if (
                //                                          //Son EXACTAMENTE 11 caracteres e inicia con una letra.
                (strRegistroPatronalImss_I.Length == 11) &&
                    Tools.boolIsLetter(strRegistroPatronalImss_I[0])
                )
            {
                //                                          //Revisa a partir del segundo caracter.
                int intI = 1;
                /*WHILE-UNTIL*/
                while (!(
                    (intI >= strRegistroPatronalImss_I.Length) ||
                    !Tools.boolIsDigit(strRegistroPatronalImss_I[intI])
                    ))
                {
                    intI = intI + 1;
                }

                boolIsValid = (
                    //                                      //Todo son dígitos (llego al final del String)
                    intI >= strRegistroPatronalImss_I.Length
                    );
            }
            else
            {
                boolIsValid = false;
            }

            return boolIsValid;
        }
    }

    //==================================================================================================================
}
/*END-TASK*/
