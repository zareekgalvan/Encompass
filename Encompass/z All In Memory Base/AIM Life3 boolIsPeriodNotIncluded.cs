/*TASK Life3 boolIsPeriodNotIncluded*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using TowaInfrastructure;

//                                                          //AUTHOR: Towa (??).
//                                                          //CO-AUTHOR: Towa (??, GLG-Gerardo López).
//                                                          //DATE: 9-Julio-2015????.
//                                                          //PURPOSE:
//                                                          //Implementación.

namespace AllInMemoryBase
{
    //==================================================================================================================
    public /*CLOSE*/ partial class LifePeriods : BclassBaseClassAbstract
    {
        //--------------------------------------------------------------------------------------------------------------
        internal bool boolIsPeriodNotIncluded(              //Determina si un período está totalmente excluido del life.
            //                                              //this[I], consulta la información.

            //                                              //bool, true si sí está totalmente excluido.

            //                                              //Periodo a verificar.
            DateTime dateBegin_I,
            DateTime dateEnd_I
            )
        {
            if (
                !Tools.boolIsDate(dateBegin_I)
                )
                Tools.subAbort(Test.strTo(dateBegin_I, TestoptionEnum.SHORT) + ") debe ser fecha");
            if (
                !Tools.boolIsDate(dateEnd_I)
                )
                Tools.subAbort(Test.strTo(dateEnd_I,TestoptionEnum.SHORT) + ") debe ser fecha");
            if (
                dateBegin_I > dateEnd_I
                )
                Tools.subAbort(Test.strTo(dateBegin_I, TestoptionEnum.SHORT) + "to" +
                    Test.strTo(dateEnd_I, TestoptionEnum.SHORT) + ") no es un período válido");

            //                                              //Busco el último período que inicie antes (o en la fecha)
            //                                              //      de inicio del período a verificar.
            int intI = arrdateBegin.Length - 1;
            /*DO-UNTIL*/
            while (!(
                (intI < 0) ||
                //                                          //Podría estar dentro de este período
                (dateBegin_I >= this.arrdateBegin[intI])
                ))
            {
                intI = intI - 1;
            }

            bool boolIsPeriodNotIncluded;
            /*CASE*/
            if (
                //                                          //Me salí del arreglo
                intI < 0
                )
            {
                boolIsPeriodNotIncluded = (
                    //                                      //El período a verificar esta totalmente antes del inicio
                    //                                      //      del life.
                    dateEnd_I < this.arrdateBegin[0]
                    );
            }
            else if (
                //                                          //Estoy al final del arreglo
                intI == (this.arrdateBegin.Length - 1)
                )
            {
                //                                          //El período a verificar esta totalmente después del fin del
                //                                          //      life.
                boolIsPeriodNotIncluded = (
                    this.arrdateEnd[this.arrdateBegin.Length - 1] < dateBegin_I
                    );
            }
            else
            {
                //                                          //Estoy en un renglón y existe otro después
                boolIsPeriodNotIncluded = (
                    //                                      //El período a verificar esta totalmente entre 2 perídos del
                    //                                      //      life.
                    (this.arrdateEnd[intI] < dateBegin_I) && (dateEnd_I < this.arrdateBegin[intI + 1])
                    );
            }
            /*END-CASE*/

            return boolIsPeriodNotIncluded;
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
