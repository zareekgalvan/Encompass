/*TASK RP.Treexxxcur Relevant Part Country*/
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
//                                                          //Especificación de clases para Country.

namespace Encompass
{
    //==================================================================================================================
    public partial class TbsicouCountry : TbsiSingleRootAbstract
    //                                                      //Clase agrupadora de countries.
    //                                                      //Cada Country es independiente de otras, no tienen
    //                                                      //      relación una con otra.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------
        private const TbaccessMethodEnum tbaccessOPTION_Z = TbaccessMethodEnum.ARRAY_BINARY_SEARCH;
        protected override TbaccessMethodEnum tbaccessOPTION { get { return TbsicouCountry.tbaccessOPTION_Z; } }

        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------
        //                                                  //El arrtree sólo podrá contener entidades treelvecou.

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
            const String strCLASS = "Tbsicou";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.strTo(TestoptionEnum.SHORT);
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo()
        {
            const String strCLASS = "Tbsicou";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.strTo();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbsicouCountry(                        //this.*[O], asigna valores.
            //                                              //Fecha en la que inicia su vida el nodo raíz, de alguna
            //                                              //      forma este deberá ser la misma que la vida de
            //                                              //      Encompass.
            DateTime dateBegin_I,
            //                                              //Cualquier información relevante respecto a este nodo.
            String strObservations_I
            )
            : base(null, dateBegin_I, Tools.dateMAX_VALUE, "unique", "country set", strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------

        /*METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        public void subAddCountry(                         //this.*[O], asigna valores.
            //                                              //Fecha en la que inicia el life de esta Currency.
            DateTime dateBegin_I,
            //                                              //Id de country en 3 caracteres (Ej. MEX, USA).
            String strId_I,
            //                                              //Nombre del país (Ej. Estados Unidos).
            String strDescription_I,
            //                                              //Cualquier información relevante respecto a esta
            //                                              //      country.
            String strObservations_I

            )
        {
            TlvecouCountry treelvecouCountryToAdd = new TlvecouCountry
                (this, dateBegin_I, strId_I, strDescription_I, strObservations_I);

            this.setComponents.subAdd(treelvecouCountryToAdd);

        }
        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TlvecouCountry: TlveLeaveAbstract
    //                                                      //Clase TreelvecouCountry donde se guarda el registro de 
    //                                                      //       un país.
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
            const String strCLASS = "Tlvecou";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.strTo(TestoptionEnum.SHORT);
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo()
        {
            const String strCLASS = "Tlvecou";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.strTo();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TlvecouCountry(                    //this.*[O], asigna valores.

            //                                              //Nodo al que pertenece la Country (será el mismo para
            //                                              //      todas las countries).
            TbsicouCountry treebsicouBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta Country.
            DateTime dateBegin_I,
            //                                              //Id de country en 3 caracteres (Ej. MEX, USA).
            String strId_I,
            //                                              //Nombre del país (Ej. Estados Unidos).
            String strDescription_I,
            //                                              //Cualquier información relevante respecto a esta
            //                                              //      country.
            String strObservations_I
            )
            : base(treebsicouBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------

        /*METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
    }
}
/*END-TASK*/

