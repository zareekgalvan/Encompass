/*TASK Life2 boolIsPeriodIncluded*/
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
        internal bool boolIsPeriodIncluded(                 //Determina si un período está totalmente incluído en life.
            //                                              //this[I], consulta la información.

            //                                              //bool, true si sí está incluida.

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
                Tools.subAbort(Test.strTo(dateEnd_I, TestoptionEnum.SHORT) + ") debe ser fecha");
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

            return (
                (intI >= 0) &&
                //                                          //El período a verificar esta dentro de período localizado.
                (dateEnd_I <= this.arrdateEnd[intI])
                );
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
