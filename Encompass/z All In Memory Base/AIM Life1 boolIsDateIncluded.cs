/*TASK Life1 boolIsDateIncluded*/
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
        internal bool boolIsDateIncluded(                   //Determina si una cierta fecha está incluida en "life".
            //                                              //this[I], consulta la información.

            //                                              //bool, true si sí está incluida.

            //                                              //Fecha a verificar.
            DateTime date_I
            )
        {
            if (
                !Tools.boolIsDate(date_I)
                )
                Tools.subAbort(Test.strTo(date_I, "date_I") + " debe ser fecha");

            //                                              //Busco el último período que inicie antes (o en la fecha)
            //                                              //      que la fecha a verificar.
            int intI = arrdateBegin.Length;
            /*DO-UNTIL*/
            while (!(
                (intI < 0) ||
                //                                          //Podría estar dentro de este período
                (date_I >= this.arrdateBegin[intI])
                ))
            {
                intI = intI - 1;
            }

            return (
                (intI >= 0) &&
                //                                          //Esta dentro de período localizado.
                (date_I <= this.arrdateEnd[intI])
                );
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
