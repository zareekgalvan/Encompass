/*TASK Life5 subAddPeriod*/
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
        internal void subAddPeriod(                         //Se agrega un nuevo período a la vigencia.
            //                                              //this[M], modifica la información.

            //                                              //Periodo que será añadido.
            //                                              //Este período debe estar actualmente totalmente excluido.
            //                                              //El tBelongsTo de la entidad a la cual pertenece este life
            //                                              //      debe contener en su life este período.
            DateTime dateBegin_I,
            DateTime dateEnd_I
            )
        {
            if (
                !this.boolIsPeriodNotIncluded(dateBegin_I, dateEnd_I)
                )
                Tools.subAbort(Test.strTo(dateBegin_I, TestoptionEnum.SHORT) + "to" +
                    Test.strTo(dateEnd_I, TestoptionEnum.SHORT) +
                    ") no esta totalmente excluído en this(" + Test.strTo(this, TestoptionEnum.SHORT) + ")");

            int intDatePos = 0;
            /*DO-UNTIL*/
            while (!(
                (intDatePos >= arrdateBegin.Length) ||
                //                                          //Podría estar dentro de este período
                (dateBegin_I >= this.arrdateBegin[intDatePos])
                ))
            {
                intDatePos = intDatePos + 1;
            }

            /*CASE*/
            if (
                //                                          //Es mayor a todas las posiciones
                intDatePos == 0
                )
            {
                if (
                    //                                      //No se empalma con la fecha anterior
                    arrdateEnd[0].AddDays(1) < dateBegin_I
                    )
                {
                    DateTime[] arrNewDateBegin = new DateTime[this.arrdateBegin.Length + 1];
                    DateTime[] arrNewDateEnd = new DateTime[this.arrdateBegin.Length + 1];

                    arrNewDateBegin[0] = dateBegin_I;
                    arrNewDateEnd[0] = dateEnd_I;

                    Array.Copy(this.arrdateBegin, 0, arrNewDateBegin, 1, this.arrdateBegin.Length);
                    Array.Copy(this.arrdateEnd, 0, arrNewDateEnd, 1, this.arrdateBegin.Length);

                    this.arrdateBegin = arrNewDateBegin;
                    this.arrdateEnd = arrNewDateEnd;
                }
                else
                {
                    //                                      //Se modifica la ultima fecha de cierre
                    this.arrdateEnd[0] = dateEnd_I;
                }
            }
            else if (
                //                                          //Es menor a todas las fechas, va en la ultima posición
                intDatePos >= this.arrdateBegin.Length
                )
            {
                if (
                    //                                      //Se empalma con el ciclo anterior
                    dateEnd_I >= arrdateBegin[intDatePos - 1]
                    )
                {
                    this.arrdateBegin[intDatePos - 1] = dateBegin_I;
                }
                else
                {
                    DateTime[] arrNewDateBegin = new DateTime[this.arrdateBegin.Length + 1];
                    DateTime[] arrNewDateEnd = new DateTime[this.arrdateBegin.Length + 1];

                    Array.Copy(this.arrdateBegin, 0, arrNewDateBegin, 0, this.arrdateBegin.Length);
                    Array.Copy(this.arrdateEnd, 0, arrNewDateEnd, 0, this.arrdateBegin.Length);

                    arrNewDateBegin[intDatePos] = dateBegin_I;
                    arrNewDateEnd[intDatePos] = dateEnd_I;

                    this.arrdateBegin = arrNewDateBegin;
                    this.arrdateEnd = arrNewDateEnd;
                }
            }
            else if (
                //                                          //No se empalma con el periodo superior ni inferior
                (dateBegin_I > arrdateEnd[intDatePos].AddDays(1)) &&
                (dateEnd_I.AddDays(1) < arrdateBegin[intDatePos - 1])
                )
            {
                DateTime[] arrNewDateBegin = new DateTime[this.arrdateBegin.Length + 1];
                DateTime[] arrNewDateEnd = new DateTime[this.arrdateBegin.Length + 1];

                Array.Copy(this.arrdateBegin, 0, arrNewDateBegin, 0, intDatePos);
                Array.Copy(this.arrdateEnd, 0, arrNewDateEnd, 0, intDatePos);

                arrNewDateBegin[intDatePos] = dateBegin_I;
                arrNewDateEnd[intDatePos] = dateEnd_I;

                Array.Copy(this.arrdateBegin, intDatePos, arrNewDateBegin, intDatePos + 1,
                    this.arrdateBegin.Length - intDatePos);
                Array.Copy(this.arrdateEnd, intDatePos, arrNewDateEnd, intDatePos + 1,
                    this.arrdateBegin.Length - intDatePos);

                this.arrdateBegin = arrNewDateBegin;
                this.arrdateEnd = arrNewDateEnd;
            }
            else if (
                //                                          //Se empalma con el periodo superior e inferior
                (dateBegin_I == arrdateEnd[intDatePos].AddDays(1)) &&
                (dateEnd_I.AddDays(1) == arrdateBegin[intDatePos - 1])
                )
            {
                DateTime[] arrNewDateBegin = new DateTime[this.arrdateBegin.Length - 1];
                DateTime[] arrNewDateEnd = new DateTime[this.arrdateBegin.Length - 1];

                Array.Copy(this.arrdateBegin, 0, arrNewDateBegin, 0, intDatePos - 1);
                Array.Copy(this.arrdateEnd, 0, arrNewDateEnd, 0, intDatePos - 1);

                arrNewDateBegin[intDatePos - 1] = this.arrdateBegin[intDatePos];
                arrNewDateEnd[intDatePos - 1] = this.arrdateEnd[intDatePos - 1];

                Array.Copy(this.arrdateBegin, intDatePos + 1, arrNewDateBegin, intDatePos,
                    this.arrdateBegin.Length - 1 - intDatePos);
                Array.Copy(this.arrdateEnd, intDatePos + 1, arrNewDateEnd, intDatePos,
                    this.arrdateBegin.Length - 1 - intDatePos);

                this.arrdateBegin = arrNewDateBegin;
                this.arrdateEnd = arrNewDateEnd;
            }
            else if (
                //                                          //Se empalma empalma con el periodo inferior
                dateEnd_I.AddDays(1) == arrdateBegin[intDatePos - 1]
                )
            {
                this.arrdateBegin[intDatePos - 1] = dateBegin_I;
            }
            else if (
                //                                          //Se empalma empalma con el periodo superior
                dateBegin_I == arrdateEnd[intDatePos].AddDays(1)
                )
            {
                this.arrdateEnd[intDatePos] = dateEnd_I;
            }
            /*END-CASE*/
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
