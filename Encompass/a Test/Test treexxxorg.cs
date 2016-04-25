using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using TowaInfrastructure;
using Encompass;
using AllInMemoryBase;
using EncompassInfrastructure;

namespace Encompass
{
    //==================================================================================================================
    static class TestTxxxorg
    {
        const String strNAME = "ARC - Alejandro de la Rosa";
        const String strPATH = @"C:\Users\alejandro.delarosa\Desktop\Encompass 7-Dic-15\Encompass\Encompass Test\Test Txxxorg" + @"\";

        //--------------------------------------------------------------------------------------------------------------
        internal static void subTestTxxxorg1()
        {
            SyspathPath syspathLog = new SyspathPath(strPATH + "Test Txxxorg1.log");
            FileInfo sysfileLog = Sys.sysfileNew(syspathLog);

            StreamWriter sysswLog;
            if (
                syspathLog.boolIsFile
                )
            {
                sysswLog = Sys.sysswNewRewriteTextFile(sysfileLog);
            }
            else
            {
                sysswLog = Sys.sysswNewWriteTextFile(sysfileLog);
            }

            sysswLog.WriteLine(strNAME + ", Now(" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ")");
            sysswLog.WriteLine("");

            DateTime dateBegin = new DateTime(2002, 02, 01);

            TbsiorgOrganization tbsiorg = new TbsiorgOrganization(dateBegin, "");
            TlveorgOrganization tlveorgTowa1 = new TlveorgOrganization(tbsiorg, dateBegin, "001", "Towa1", "Towa1");
            TlveorgOrganization tlveorgTowa2 = new TlveorgOrganization(tbsiorg, dateBegin, "002", "Towa2", "Towa2");

            tbsiorg.setComponents.subAdd(tlveorgTowa1);
            tbsiorg.setComponents.subAdd(tlveorgTowa2);

            TbsicomCompany tbsicom1 = new TbsicomCompany(tlveorgTowa1, dateBegin, "Towa");
            TbsicomCompany tbsicom2 = new TbsicomCompany(tlveorgTowa2, dateBegin, "Towa");

            tlveorgTowa1.subAddCompany(tbsicom1);
            tlveorgTowa2.subAddCompany(tbsicom2);

            SyspathPath syspathCompanies = new SyspathPath(strPATH + "companies.csv");
            FileInfo sysfileCompanies = Sys.sysfileNew(syspathCompanies);
            StreamReader syssrCompanies = Sys.syssrNewTextFile(sysfileCompanies);

            while (!syssrCompanies.EndOfStream)
            {
                String strRecord = syssrCompanies.ReadLine();
                String[] arrstrCompanyData = strRecord.Split(',');

                String strId = arrstrCompanyData[0];
                String strDescription = arrstrCompanyData[1];

                DateTime dateCompany;
                bool boolDate = DateTime.TryParse(arrstrCompanyData[3], out dateCompany);
                if (
                    !boolDate
                    )
                    Tools.subAbort("arrstrCompanyData[4](" + arrstrCompanyData[3] +
                        ") no puede ser convertido a fecha");

                String strRfcpm = arrstrCompanyData[2];

                String strRpss;
                if (
                    arrstrCompanyData.Length >= 5
                    )
                {
                    strRpss = arrstrCompanyData[4];
                }
                else
                {
                    strRpss = null;
                }

                TlvecommxMexico tlvelenmx =
                    new TlvecommxMexico(tbsicom1, dateCompany, strId, strDescription, "", strRfcpm, strRpss);

                tbsicom1.setComponents.subAdd(tlvelenmx);
            }

            //                                              //Agrega "p11TIN" de com1 a com2 y lo elimina de com1.
            tbsicom2.setComponents.subAdd(tbsicom1.setComponents.tAccess("p11TIN"));
            tbsicom1.setComponents.subRemove(tbsicom1.setComponents.tAccess("p11TIN"));

            sysswLog.WriteLine("tbsicom.setComponents (" + tbsicom1.setComponents.ToString() + ")");
            sysswLog.WriteLine("tbsicom.setComponents (" + tbsicom2.setComponents.ToString() + ")");

            //                                          //Resumen de objetos en todas las pruebas
            BclassBaseClassAbstract.subWriteSummary(sysswLog);

            sysswLog.Dispose();
        }
    }
}
