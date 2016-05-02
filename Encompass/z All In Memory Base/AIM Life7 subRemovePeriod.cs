/*TASK Life7 subRemovePeriod*/
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
        internal void subRemovePeriod(                      //Se elimina la vigencia dentro de este periodo.
            //                                              //Ninguna de las entidades de información que hacen
            //                                              //      referencia a la entidad de información a la cual
            //                                              //      pertenece este life deben, en su propia life,
            //                                              //      incluir algún día de este período.
            //                                              //this[M], modifica la información.

            //                                              //Periodo que será removido.
            //                                              //Este período debe estar actualmente incluído.
            DateTime dateBegin_I,
            DateTime dateEnd_I
            )
        {
            if (
                !this.boolIsPeriodIncluded(dateBegin_I, dateEnd_I)
                )
                Tools.subAbort(Test.strTo(dateBegin_I, TestoptionEnum.SHORT) + "to" +
                    Test.strTo(dateEnd_I, TestoptionEnum.SHORT) + ") no esta incluído en this(" +
                    Test.strTo(this, TestoptionEnum.SHORT) + ")");


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
                //                                          //No se empalma con el limite superior ni inferior
                (dateBegin_I > arrdateBegin[intDatePos]) &&
                (dateEnd_I < arrdateEnd[intDatePos])
                )
            {
                DateTime[] arrNewDateBegin = new DateTime[this.arrdateBegin.Length + 1];
                DateTime[] arrNewDateEnd = new DateTime[this.arrdateBegin.Length + 1];

                Array.Copy(this.arrdateBegin, 0, arrNewDateBegin, 0, intDatePos);
                Array.Copy(this.arrdateEnd, 0, arrNewDateEnd, 0, intDatePos);

                arrNewDateBegin[intDatePos] = dateEnd_I;
                arrNewDateEnd[intDatePos] = arrdateEnd[intDatePos];

                arrNewDateBegin[intDatePos + 1] = arrdateBegin[intDatePos];
                arrNewDateEnd[intDatePos + 1] = dateBegin_I;

                if (
                    intDatePos < arrNewDateBegin.Length - 2
                    )
                {
                    Array.Copy(this.arrdateBegin, intDatePos, arrNewDateBegin, intDatePos + 2,
                        this.arrdateBegin.Length - intDatePos);
                    Array.Copy(this.arrdateEnd, intDatePos, arrNewDateEnd, intDatePos + 2,
                        this.arrdateBegin.Length - intDatePos);
                }

                this.arrdateBegin = arrNewDateBegin;
                this.arrdateEnd = arrNewDateEnd;
            }
            else if (
                //                                          //Se empalma con el limite superior e inferior
                (dateBegin_I == arrdateBegin[intDatePos]) &&
                (dateEnd_I == arrdateEnd[intDatePos])
                )
            {
                DateTime[] arrNewDateBegin = new DateTime[this.arrdateBegin.Length - 1];
                DateTime[] arrNewDateEnd = new DateTime[this.arrdateBegin.Length - 1];

                Array.Copy(this.arrdateBegin, 0, arrNewDateBegin, 0, intDatePos);
                Array.Copy(this.arrdateEnd, 0, arrNewDateEnd, 0, intDatePos);

                if (
                    intDatePos < arrNewDateBegin.Length
                    )
                {
                    Array.Copy(this.arrdateBegin, intDatePos + 1, arrNewDateBegin, intDatePos,
                        this.arrdateBegin.Length - intDatePos);
                    Array.Copy(this.arrdateEnd, intDatePos + 1, arrNewDateEnd, intDatePos,
                        this.arrdateBegin.Length - intDatePos);
                }

                this.arrdateBegin = arrNewDateBegin;
                this.arrdateEnd = arrNewDateEnd;
            }
            else if (
                //                                          //Se empalma empalma con el limite inferior
                dateBegin_I == arrdateBegin[intDatePos]
                )
            {
                this.arrdateBegin[intDatePos] = dateEnd_I;

            }
            else if (
                //                                          //Se empalma empalma con el limite superior
                dateEnd_I == arrdateEnd[intDatePos]
                )
            {
                this.arrdateEnd[intDatePos] = dateBegin_I;
            }
            /*END-CASE*/
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
