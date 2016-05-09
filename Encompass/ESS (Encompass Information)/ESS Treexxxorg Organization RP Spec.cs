/*TASK RPS.Txxxorg Relevant Part Organization*/
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
//                                                          //DATE: 3-Noviembre-2015.
//                                                          //PURPOSE:
//                                                          //Especificación de clases para Organization.

namespace Encompass
{
    //==================================================================================================================
    public partial class TbsiorgOrganization : TbsiSingleRootAbstract
    //                                                      //Clase agrupadora de organizaciones.
    //                                                      //Cada organización es independiente de otras, no tienen
    //                                                      //      relación una con otra.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        private const TbaccessMethodEnum tbaccessOPTION_Z = TbaccessMethodEnum.ARRAY_BINARY_SEARCH;
        protected override TbaccessMethodEnum tbaccessOPTION { get { return TbsiorgOrganization.tbaccessOPTION_Z; } }

        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //El dict sólo podrá contener entidades tlveorg.

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo(TestoptionEnum testoptionSHORT_I)
        {
            const String strCLASS = "Tbsiorg";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.strTo(TestoptionEnum.SHORT);
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo()
        {
            const String strCLASS = "Tbsiorg";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.strTo();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbsiorgOrganization(                       //Construción de la "raíz" de todas las organizaciones.
            //                                              //this.*[O], asigna valores.

            //                                              //Fecha en la que inicia su vida de la "raíz",
            //                                              //      probablemenete para el Encompass de Towa esta deba
            //                                              //      ser la fecha de inicio de Towa (1ºFeb2002).
            DateTime dateBegin_I,

            String strObservations_I
            )
            : base(null, dateBegin_I, Tools.dateMAX_VALUE, "unique", "organization set", strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TlveorgOrganization : TlveLeaveAbstract
    //                                                      //Clase organización donde se guarda el registro de una
    //                                                      //      entidad corporativa y sus entidades legales,
    //                                                      //      cuentas, business objects, pólizas, eventos y
    //                                                      //      general ledgers correspondientes.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------
         
        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //Es el nodo que tiene el conjunto de todos los Business
        //                                                  //      Object de la Organización.
        /*
        private TbmubobBusinessObject tbmubobBusinessObject_Z;
        internal TbmubobBusinessObject tbmubobBusinessObject { get { return this.tbmubobBusinessObject_Z; } }
        */

        //                                                  //Es el nodo que tiene el conjunto de cuentas de la 
        //                                                  //      organización.
        /*
        private TbmuaccAccounts tbmuaccAccounts_Z;
        internal TbmuaccAccounts tbmuaccAccounts { get { return this.tbmuaccAccounts_Z; } }
        */

        //                                                  //Es el nodo que tiene el conjunto de todas las
        //                                                  //      Compañías de la Organización.
        private TbsicomCompany tbsicomCompany_Z;
        internal TbsicomCompany tbsicomCompany { get { return this.tbsicomCompany_Z; } }

        //                                                  //Es el nodo que contiene el conjunto de pólizas de la 
        //                                                  //      organización.
        /*
        private TbmupolPolicy tbmupolPolicy_Z;
        internal TbmupolPolicy tbmupolPolicy { get { return this.tbmupolPolicy_Z; } }
        */

        //                                                  //Contiene el conjunto de General Ledger de la organización.
        /*
        private TbmugleGeneralLedger tbmugleGeneralLedger_Z;
        internal TbmupolPolicy tbmugleGeneralLedger { get { return this.tbmugleGeneralLedger_Z; } }
        */

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo(TestoptionEnum testoptionSHORT_I)
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "[" + //Test.strTo(this.tbmubobBusinessObject, TestoptionEnum.SHORT) +
                //Test.strTo(this.tbmuaccAccounts, TestoptionEnum.SHORT) + 
                Test.strTo(this.tbsicomCompany, TestoptionEnum.SHORT) +
                //Test.strTo(this.tbmupolPolicy, TestoptionEnum.SHORT) +
                //Test.strTo(this.tbmugleGeneralLedger, TestoptionEnum.SHORT) +
                "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public override String strTo()
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "{" + //Test.strTo(this.tbmubobBusinessObject, "tbmubobBusinessObject") +
                //Test.strTo(this.tbmuaccAccounts, "tbmuaccAccounts") +
                Test.strTo(this.tbsicomCompany, "tbsicomCompany") +
                //Test.strTo(this.tbmupolPolicy, "tbmupolPolicy") +
                //Test.strTo(this.tbmugleGeneralLedger, "tbmugleGeneralLedger") + 
                "}" + "==>" + base.strTo();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal void subAddCompany(                        //Añade una compañía al set.
            //                                              //this[M], añade una compañía al set.

            //                                              //Compañía que será añadida.
            TbsicomCompany tbsicomToAdd_T
            )
        {
            this.tbsicomCompany_Z = tbsicomToAdd_T;
        }
        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TlveorgOrganization(                       //this.*[O], asigna valores.
            //                                              //PENDIENTE REVISAR ************

            //                                              //Nodo al que pertenece la organización (será el mismo para
            //                                              //      todas las organización).
            TbsiorgOrganization tbsiorgBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta organización
            //                                              //      (normalmente será la fecha de constitución de la
            //                                              //      sociedad mercantil).
            DateTime dateBegin_I,

            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tbsiorgBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------

        /*METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal void subLoadCompany(                       //Carga las compañías a la organización.

            //                                              //Dirección del archivo csv que contiene las compañías.
            String strPath_I
            )
        {
            SyspathPath syspathCompany = new SyspathPath(strPath_I + "Companies" + this.strId + ".csv");
            FileInfo sysfileCompany = Sys.sysfileNew(syspathCompany);
            StreamReader syssrCompany = Sys.syssrNewTextFile(sysfileCompany);

            TextFieldParser tfpCompany = new TextFieldParser(syssrCompany);
            tfpCompany.HasFieldsEnclosedInQuotes = true;
            tfpCompany.Delimiters = new[] { "," };

            //                                              //Salta la primera línea, sólo describe los campos bsi.
            String[] arrstrCompany = tfpCompany.ReadFields();

            arrstrCompany = tfpCompany.ReadFields();

            String strObservations = arrstrCompany[0];
            String strlife = arrstrCompany[1];

            DateTime dateBegin;

            bool boolDateBegin = DateTime.TryParse(strlife, out dateBegin);

            TbsicomCompany tbsicom = new TbsicomCompany(this, dateBegin, strObservations);
            this.subAddCompany(tbsicom);
            //                                              //Salta la línea siguiente, solo describe los campos lve.
            arrstrCompany = tfpCompany.ReadFields();

            while (
                !tfpCompany.EndOfData
                )
            {
                arrstrCompany = tfpCompany.ReadFields();

                String strID = arrstrCompany[0];
                String strDescription = arrstrCompany[1];
                strObservations = arrstrCompany[2];
                String strRFC = arrstrCompany[3];
                String strRPSS = arrstrCompany[4];
                if (strRPSS == "")
                {
                    strRPSS = null;
                }
                strlife = arrstrCompany[5];

                bool boolDate = DateTime.TryParse(strlife, out dateBegin);

                TlvecommxMexico tlvecommx = 
                    new TlvecommxMexico
                        (tbsicom, dateBegin, strID, strDescription, strObservations, strRFC, strRPSS);

                tbsicom.setComponents.subAdd(tlvecommx);
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        internal void subLoadBusinessObject(                //Carga las compañías a la organización.
            //*************MÉTODO COPIA DE SUBLOADCOMPANY, QUEDA POR DEFINIR PARA BOJ*************************

            //                                              //Dirección del archivo csv que contiene las compañías.
            String strPath_I
            )
        {
            SyspathPath syspathBusinessObject = new SyspathPath(strPath_I + "Companies" + this.strId + ".csv");
            FileInfo sysfileBusinessObject = Sys.sysfileNew(syspathBusinessObject);
            StreamReader syssrBusinessObject = Sys.syssrNewTextFile(sysfileBusinessObject);

            TextFieldParser tfpBusinessObject = new TextFieldParser(syssrBusinessObject);
            tfpBusinessObject.HasFieldsEnclosedInQuotes = true;
            tfpBusinessObject.Delimiters = new[] { "," };

            //                                              //Salta la primera línea, solo describe los campos bsi.
            String[] arrstrCompany = tfpBusinessObject.ReadFields();

            arrstrCompany = tfpBusinessObject.ReadFields();

            String strObservations = arrstrCompany[0];
            String strlife = arrstrCompany[1];

            DateTime dateBegin;

            bool boolDateBegin = DateTime.TryParse(strlife, out dateBegin);

            TbsicomCompany tbsicom = new TbsicomCompany(this, dateBegin, strObservations);

            //                                              //Salta la línea siguiente, solo describe los campos lve.
            arrstrCompany = tfpBusinessObject.ReadFields();

            while (
                !tfpBusinessObject.EndOfData
                )
            {
                arrstrCompany = tfpBusinessObject.ReadFields();

                String strID = arrstrCompany[0];
                String strDescription = arrstrCompany[1];
                strObservations = arrstrCompany[2];
                String strRFC = arrstrCompany[3];
                String strRPSS = arrstrCompany[4];
                strlife = arrstrCompany[5];

                bool boolDate = DateTime.TryParse(strlife, out dateBegin);

                TlvecommxMexico tlvecommx =
                    new TlvecommxMexico
                        (tbsicom, dateBegin, strID, strDescription, strObservations, strRFC, strRPSS);

                tbsicom.setComponents.subAdd(tlvecommx);
            }
        }
        //--------------------------------------------------------------------------------------------------------------


    }

    //==================================================================================================================
}
/*END-TASK*/
