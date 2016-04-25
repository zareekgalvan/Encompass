/*TASK RPS.Txxxboj Relevant Part Business Object*/
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
//                                                          //DATE: 2-Diciembre-2015.
//                                                          //PURPOSE:
//                                                          //Especificación de clases para Business Object.

namespace Encompass
{
    //==================================================================================================================
    public partial class TbmubobBusinessObject : TbmuMultipleBranchesAbstract
    //                                                      //Clase especifica para el nodo que agrupa todos los
    //                                                      //      Business Objects
    //                                                      //Un TbmubobBusinessObject solo podrá contener objetos de
    //                                                      //      tipo TbmubobBusinessObject y 
    //                                                      //      TlvebojAbstractAbstract
    //                                                      //El conjunto de Business Objects puede ser solo 1 ó muchos.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        private const TbaccessMethodEnum tbaccessOPTION_Z = TbaccessMethodEnum.ARRAY_BINARY_SEARCH;
        protected override TbaccessMethodEnum tbaccessOPTION { get { return TbmubobBusinessObject.tbaccessOPTION_Z; } }

        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //El dict sólo podrá contener entidades tbmubob y 
        //                                                  //      tlveboj.

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Tbmubob";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbmubobBusinessObject(                     //Construye un business object ("rama") que contendrá otros
            //                                              //      business objects ya sea otras "ramas" u "hojas".
            //                                              //this.*[O], asigna valores.

            //                                              //Puede ser: 
            //                                              //a) tlveorg, si es el primer business object "raíz" de una 
            //                                              //      organización.
            //                                              //b) tbmubob, si es una rama secundaria del arbol de
            //                                              //      organizaciones.
            TTreeStructureAbstract tBelongTo_T,

            DateTime dateBegin_I,
            DateTime dateEnd_I,

            //                                              //Si es la "raíz", Id y Description deben ser identicas a
            //                                              //      las de la organización a la que pertenece.
            String strId_I,
            String strDescription_I,

            String strObservations_I
            )
            : base(tBelongTo_T, dateBegin_I, dateEnd_I, strId_I, strDescription_I, strObservations_I)
        {
            if (
                tBelongTo_T == null
                )
                Tools.subAbort("tBelongTo_T(" + Test.strToDisplay(tBelongTo_T) + ") no puede ser null");

            if (
                //                                          //Si es raiz debe pertenecer a una organización
                (tBelongTo_T is TlveLeaveAbstract) && !(tBelongTo_T is TlveorgOrganization)
                )
                Tools.subAbort("tBelongTo_T.GetType()(" + Test.strToDisplay(tBelongTo_T.GetType()) + 
                    "), debe ser un TlveorgOrganization");
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TlvebobAbstract : TlveLeaveAbstract
    //                                                      //Clase business object donde se guarda el registro de 
    //                                                      //      proyectos, centros de costos u alguna otra actividad
    //                                                      //      de la organiazación.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------


        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Tlveboj";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TlvebobAbstract(                           //Construye una "hoja" de business object.
            //                                              //this.*[O], asigna valores.

            //                                              //Nodo al que pertenece este business object que es una
            //                                              //      "hoja".
            TbmubobBusinessObject tbmubobBelongTo_T,
            DateTime dateBegin_I,
            DateTime dateEnd_I,
            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tbmubobBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TlvebojProject : TlveLeaveAbstract
    //                                                      //Clase business object donde se guarda el registro de 
    //                                                      //      proyectos, centros de costos u alguna otra actividad
    //                                                      //      de la organiazación.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------


        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Tlveboj";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TlvebojProject(                    //this.*[O], asigna valores.

            //                                              //Nodo al que pertenece la organización (será el mismo para
            //                                              //      todas las organización).
            TbmubobBusinessObject tbmubobBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta organización
            //                                              //      (normalmente será la fecha de constitución de la
            //                                              //      sociedad mercantil).
            DateTime dateBegin_I,

            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tbmubobBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TlvebojCostCenter : TlveLeaveAbstract
    //                                                      //Clase business object donde se guarda el registro de 
    //                                                      //      proyectos, centros de costos u alguna otra actividad
    //                                                      //      de la organiazación.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------


        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Tlveboj";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TlvebojCostCenter(                         //this.*[O], asigna valores.
            //                                              //PENDIENTE REVISAR *************

            //                                              //Nodo al que pertenece la organización (será el mismo para
            //                                              //      todas las organización).
            TbmubobBusinessObject tbmubobBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta organización
            //                                              //      (normalmente será la fecha de constitución de la
            //                                              //      sociedad mercantil).
            DateTime dateBegin_I,

            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tbmubobBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}
/*END-TASK*/
