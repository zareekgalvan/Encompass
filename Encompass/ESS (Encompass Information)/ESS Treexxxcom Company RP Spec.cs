/*TASK RPS.Txxxcom Relevant Part Company*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using TowaInfrastructure;
using AllInMemoryBase;
using EncompassInfrastructure;

//                                                          //AUTHOR: Towa (ADGG-Ángel González,
//                                                          //      AGV-Armando Galván, ARC-Alejandro de la Rosa).
//                                                          //CO-AUTHOR: Towa (GLG-Gerardo López).
//                                                          //DATE: 9-Julio-2015.
//                                                          //PURPOSE:
//                                                          //Especificación de clases para Company.

namespace Encompass
{
    //==================================================================================================================
    public partial class TbsicomCompany : TbsiSingleRootAbstract
    //                                                      //Clase especifica para el nodo que agrupa todas las
    //                                                      //      Compañías.
    //                                                      //Una organización (Ej. Towa) opera en un conjunto de
    //                                                      //      Sociedades Mercantiles a las cuales llamaremos
    //                                                      //      Compañías para diferencialas de otras sociedades
    //                                                      //      mercantiles con las cuales se relaciona.
    //                                                      //El conjunto de Compañías puede ser solo 1 ó muchas. 
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        private const TbaccessMethodEnum tbaccessOPTION_Z = TbaccessMethodEnum.ARRAY_BINARY_SEARCH;
        protected override TbaccessMethodEnum tbaccessOPTION { get { return TbsicomCompany.tbaccessOPTION_Z; } }

        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //El dict sólo podrá contener entidades tlvecom.

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Tbsicom";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbsicomCompany(                            //Construcción de la "raíz" de todas las campañías de una
            //                                              //      organización.
            //                                              //this.*[O], asigna valores.

            //                                              //Organización a la que pertenece este "raiz".
            TlveorgOrganization tlveorgBelongTo_T,
            //                                              //Fecha en la que inicia la vida de la "raíz", probablemente
            //                                              //      sea la misma fecha de inicio de la organización.
            DateTime dateBegin_I,

            String strObservations_I
            )
            : base(tlveorgBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, tlveorgBelongTo_T.strId,
                tlveorgBelongTo_T.strDescription, strObservations_I)
        {   
            if (
                tlveorgBelongTo_T == null
                )
                Tools.subAbort(Test.strTo(tlveorgBelongTo_T, TestoptionEnum.SHORT) + 
                    ") no puede ser null");
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public abstract partial class TlvecomCompanyAbstract : TlveLeaveAbstract
    //                                                      //Clase Compañía base para la clase de Compañía concreta
    //                                                      //      para cada país (Ej. TlvecommxMexico).
    //                                                      //Una compañía de un país tiene información que es similar a
    //                                                      //      las compañías de otros paises, y dependiendo del
    //                                                      //      país tiene otra información que es especifica para
    //                                                      //      dicho país (Ej. en México se tendrá el RFC).
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //POSTERIORMENTE SE AÑADIRÁ MÁS INFORMACIÓN, tal como:
        //                                                  //1. General Leadger (de la compañía).
        //                                                  //2. Responsable, esta información será por período.

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Tlvecom";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TlvecomCompanyAbstract(                    //Construye la "hoja" de compañía.
            //                                              //this.*[O], asigna valores.

            TbsicomCompany tbsicomBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta compañía
            //                                              //      (normalmente será la fecha de constitución de la
            //                                              //      sociedad mercantil).
            DateTime dateBegin_I,

            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tbsicomBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TlvecommxMexico : TlvecomCompanyAbstract
    //                                                      //Clase de compañía concreta para México.
    {
        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //RFC, Registro Federal de Contribuyentes con el cual la
        //                                                  //      sociedad esta registrada ante la SHCP para cumplir
        //                                                  //      con sus obligaciones fiscales.
        private RfcpmPersonaMoral rfcpm_Z;
        internal RfcpmPersonaMoral rfcpm { get { return this.rfcpm_Z; } }

        //                                                  //Clave de Registro Patronal ante el IMSS.
        //                                                  //Puede ser null si la compañía no esta registrada ante el
        //                                                  //      IMSS.
        //                                                  //EN REALIDAD UNA COMPAÑÍA PODRÍA TENER VARIOS rpss, uno por
        //                                                  //      cada localidad en la que tiene personal, sin
        //                                                  //      embargo, dado el manejo "virtual" de la relación
        //                                                  //      con el IMSS, aparentemente esto ya no es necesario.
        //                                                  //SI ESTE ENTENDIMIENTO CAMBIARA EN EL FUTURO, ESTO SIN DUDA
        //                                                  //      REQUERIRA ALGUNOS CAMBIOS.
        private RpssRegistroPatronalImss rpss_Z;
        internal RpssRegistroPatronalImss rpss { get { return this.rpss_Z; } }

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Tlvecommx";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TlvecommxMexico(                        //this.*[O], asigna valores.

            TbsicomCompany tbsicomBelongTo_T,
            DateTime dateBegin_I,
            String strId_I,
            String strDescription_I,
            String strObservations_I,
            //                                              //Registro Federal de Contibuyentes VALIDO.
            String strRfcpm_I,
            //                                              //Registro Patronal ante el IMSS VALIDO.
            //                                              //Puede ser null
            String strRpss_I
            )
            : base(tbsicomBelongTo_T, dateBegin_I, strId_I, strDescription_I, strObservations_I)
        {
            this.rfcpm_Z = new RfcpmPersonaMoral(strRfcpm_I);

            //                                              //El rpss puede ser null.
            if (
                //                                          //No se tiene registro ante el IMSS
                strRpss_I == null
                )
            {
                rpss_Z = null;
            }
            else
            {
                rpss_Z = new RpssRegistroPatronalImss(strRpss_I);
            }
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
