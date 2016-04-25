/*TASK RPS. Relevant Part Encompass*/
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
//                                                          //DATE: 7-Diciembre-2015.
//                                                          //PURPOSE:
//                                                          //Especificación de clases para Encompass.

namespace Encompass
{
    //==================================================================================================================
    public partial class Encompass : BclassBaseClassAbstract
    //                                                      //Clase que contiene a todas las organizaciones y el tipo de
    //                                                      //      cambio.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        private const BclassmutabilityEnum bclassmutability_Z = BclassmutabilityEnum.MUTABLE;
        public override BclassmutabilityEnum bclassmutability { get { return Encompass.bclassmutability_Z; } }

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //Es el nodo que tiene el conjunto de todas las
        //                                                  //      Organizaciones.
        private TbsiorgOrganization tbsiorgOrganization_Z;
        internal TbsiorgOrganization tbsiorgOrganization { get { return this.tbsiorgOrganization_Z; } }

        //                                                  //Falta por definir currency

        //--------------------------------------------------------------------------------------------------------------
         internal new void subReset()
        {
            base.subReset();
        }
        
        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Encompass";
            const String strCOLLECTION_A = "tbsiorg";

            String strNL;
            String strLabel;
            //Test. subToStringStart(out strNL, out strLabel);

            String strToString = strCLASS + "{";
            strToString = strToString + strNL + ">>>>START_" + strLabel;
            strToString = strToString + strNL + "end log " + strCOLLECTION_A;
            strToString = strToString + strNL + "<<<<END_" + strLabel;
            strToString = strToString + strNL + "}";

            //Test.subToStringEnd(out strNL);

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal Encompass(                                 //Crea objeto DUMMY.
            //                                              //this.*[O], crea sin asignar nada.
            )
        {
        }

        //--------------------------------------------------------------------------------------------------------------
        internal Encompass(                                 //Constructor de la clase Encompass.

            //                                              //Path de la carpeta que contiene todos los archivos CSV.
            string strPATH_I
            )
        {
            DateTime dateBegin = new DateTime(2002, 02, 01);

            TbsiorgOrganization tbsiorg = new TbsiorgOrganization(dateBegin, "");
            this.tbsiorgOrganization_Z = tbsiorg;

            this.subLoadOrganization(strPATH_I);
        }
        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal void subAddOrganization(                   //Añade una compañía al set.
            //                                              //this[M], añade una compañía al set.

            //                                              //Conjunto de compañías que serán añadidas.
            TbsiorgOrganization tbsiorgToAdd_T
            )
        {
            this.tbsiorgOrganization_Z = tbsiorgToAdd_T;
        }
        //--------------------------------------------------------------------------------------------------------------

        /*METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal void subLoadOrganization(                  //Carga las organizaciones a la entidad emcompass.

            //                                              //Dirección del archivo csv que contiene las organizaciones.
            String strPath_I              
            )
        {
            SyspathPath syspathOrganization = new SyspathPath(strPath_I + "Organization.csv");
            FileInfo sysfileOrganization = Sys.sysfileNew(syspathOrganization);
            StreamReader syssrOrganization = Sys.syssrNewTextFile(sysfileOrganization);

            TextFieldParser tfpOrganization = new TextFieldParser(syssrOrganization);
            tfpOrganization.HasFieldsEnclosedInQuotes = true;
            tfpOrganization.Delimiters = new[]{","};

            String[] arrstrOrganization = tfpOrganization.ReadFields();

            while (
                !tfpOrganization.EndOfData
                )
            {
                arrstrOrganization = tfpOrganization.ReadFields();

                String strID = arrstrOrganization[0];
                String strDescription = arrstrOrganization[1];
                String strObservations = arrstrOrganization[2];
                String strlife = arrstrOrganization[3];

                DateTime dateBegin;
                bool boolDate = DateTime.TryParse(strlife, out dateBegin);

                TlveorgOrganization tlveorg = 
                    new TlveorgOrganization(this.tbsiorgOrganization, dateBegin, strID, strDescription, strObservations);

                tlveorg.subLoadCompany(strPath_I);
                // FALTA CARGAR BOJ

                this.tbsiorgOrganization.setComponents.subAdd(tlveorg);
            }
        }
        //--------------------------------------------------------------------------------------------------------------

    }

}
    //==================================================================================================================
    /*END-TASK*/
