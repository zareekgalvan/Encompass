/*TASK Imss1 ImssNumeroAfiliacion class*/
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
    public /*INMUTABLE*/ class ImssNumeroAfiliacionImss:BclassBaseClassAbstract
        //                                                  //Guarda en forma robusta el Numero de Aliliación al IMSS de
        //                                                  //      una persona (obrero o empleado).
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------
        private const BclassmutabilityEnum bclassmutability_Z = BclassmutabilityEnum.MUTABLE;
        public override BclassmutabilityEnum bclassmutability 
                                             { get { return ImssNumeroAfiliacionImss.bclassmutability_Z; } }
        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //Identificación de un persona (obrero o empleado) ante el 
        //                                                  //      IMSS.
        //                                                  //Se forma con 11 dígitos.
        private readonly String strNumeroAfiliacionImss_Z;
        public String strNumeroAfiliacionImss { get { return this.strNumeroAfiliacionImss_Z; } }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo(TestoptionEnum testoptionSHORT_I)
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "[" + Test.strTo(this.strNumeroAfiliacionImss, TestoptionEnum.SHORT) + "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public override String strTo()
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "{" + Test.strTo(this.strNumeroAfiliacionImss, "strNumeroAfiliacionImss") +
                 "}" + "==>" + base.strTo();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        public ImssNumeroAfiliacionImss(                    //Crea objeto con información
            //                                              //this.*[O], asigna valores.

            String strNumeroAfiliacionImss_I
            )
        {
            if (
                !ImssNumeroAfiliacionImss.boolIsValid(this.strNumeroAfiliacionImss)
                )
                Tools.subAbort(Test.strTo(strNumeroAfiliacionImss, TestoptionEnum.SHORT) +
                    ") es inválido");

            this.strNumeroAfiliacionImss_Z = strNumeroAfiliacionImss_I;
        }

        /*OTHER SUPORT METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        public static bool boolIsValid(                     //Determina si parece ser válida la estructura del IMSS de
            //                                              //      Persona (Ej. 99023759281).
            //                                              //1.    La estructura "99023759281" debe contener solo
            //                                              //      números.

            //                                              //String a validar.
            String strAfiliacionImss_I
            )
        {
            bool boolIsValid;
            if (
                //                                          //Son EXACTAMENTE 11 caracteres.
                (strAfiliacionImss_I.Length == 11)
                )
            {
                //                                          //Revisa sus caracteres.
                int intI = 0;
                /*WHILE-UNTIL*/
                while (!(
                    (intI >= strAfiliacionImss_I.Length) ||
                    !Tools.boolIsDigit(strAfiliacionImss_I[intI])
                    ))
                {
                    intI = intI + 1;
                }

                boolIsValid = (
                    //                                      //Todo son dígitos (llego al final del String)
                    intI >= strAfiliacionImss_I.Length
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
