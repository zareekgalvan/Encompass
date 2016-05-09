/*TASK RP.Treexxxcur Relevant Part Currency*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using TowaInfrastructure;
using AllInMemoryBase;
using EncompassInfrastructure;
using Microsoft.VisualBasic.FileIO;


//                                                          //AUTHOR: Towa (ADGG-Ángel González,
//                                                          //      AGV-Armando Galván, ARC-Alejandro de la Rosa).
//                                                          //CO-AUTHOR: Towa (GLG-Gerardo López).
//                                                          //DATE: 25-Enero-2016.
//                                                          //PURPOSE:
//                                                          //Especificación de clases para Currency.

namespace Encompass
{
    //==================================================================================================================
    public partial class TbsicurCurrency : TbsiSingleRootAbstract
    //                                                      //Clase agrupadora de currencies.
    //                                                      //Cada Currency es independiente de otras, no tienen
    //                                                      //      relación una con otra.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------
        private const TbaccessMethodEnum tbaccessOPTION_Z = TbaccessMethodEnum.ARRAY_BINARY_SEARCH;
        protected override TbaccessMethodEnum tbaccessOPTION { get { return TbsicurCurrency.tbaccessOPTION_Z; } }

        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------
        //                                                  //El arrtree sólo podrá contener entidades treelvecur.

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset(                         //Se nulifican los valores base de las propiedades para
            //                                              //      forzar que se recalculen de nuevo cuando se vuelvan
            //                                              //      a requerir.
            //                                              //this[M], reset propiedades calculadas.
            )
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo(TestoptionEnum testoptionSHORT_I)
        {
            const String strCLASS = "Tbsicur";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.strTo(TestoptionEnum.SHORT);
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo()
        {
            const String strCLASS = "Tbsicur";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.strTo();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbsicurCurrency(                        //this.*[O], asigna valores.
            //                                              //Fecha en la que inicia su vida el nodo raíz, de alguna
            //                                              //      forma este deberá ser la misma que la vida de
            //                                              //      Encompass.
            DateTime dateBegin_I,
            //                                              //Cualquier información relevante respecto a este nodo.
            String strObservations_I
            )
            : base(null, dateBegin_I, Tools.dateMAX_VALUE , "unique", "currency set", strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------

        /*METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        public void subAddCurrency(                         //this.*[O], asigna valores.
            //                                              //Fecha en la que inicia el life de esta Currency.
            DateTime dateBegin_I,
            //                                              //Id de la Currency en 3 caracteres (Ej. MXN, USD).
            String strId_I,
            //                                              //Nombre de la Currency (Ej. Peso Mexicano).
            String strDescription_I,
            //                                              //Cualquier información relevante respecto a esta
            //                                              //      currency.
            String strObservations_I

            )
        {
            TlvecurCurrency treelvecurCurrencyToAdd = new TlvecurCurrency
                (this, dateBegin_I, strId_I, strDescription_I, strObservations_I);

            this.setComponents.subAdd(treelvecurCurrencyToAdd);

        }
        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TlvecurCurrency : TlveLeaveAbstract
    //                                                      //Clase TreelvecurCurrency donde se guarda el registro de 
    //                                                      //       una moneda.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset(                         //Se nulifican los valores base de las propiedades para
            //                                              //      forzar que se recalculen de nuevo cuando se vuelvan
            //                                              //      a requerir.
            //                                              //this[M], reset propiedades calculadas.
            )
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo(TestoptionEnum testoptionSHORT_I)
        {
            const String strCLASS = "Tlvecur";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.strTo(TestoptionEnum.SHORT);
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo()
        {
            const String strCLASS = "Tlvecur";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.strTo();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TlvecurCurrency(                    //this.*[O], asigna valores.

            //                                              //Nodo al que pertenece la Currency (será el mismo para
            //                                              //      todas las currencies).
            TbsicurCurrency treebsicurBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta Currency.
            DateTime dateBegin_I,
            //                                              //Id de la Currency en 3 caracteres (Ej. MXN, USD).
            String strId_I,
            //                                              //Nombre de la Currency (Ej. Peso Mexicano).
            String strDescription_I,
            //                                              //Cualquier información relevante respecto a esta
            //                                              //      currency.
            String strObservations_I
            )
            : base(treebsicurBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE , strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------

        /*METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
    }
}
/*END-TASK*/
