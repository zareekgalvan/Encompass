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
    static class TestTxxxcom
    {
        const String strNAME = "ARC - Alejandro de la Rosa";
        const String strPATH = @"D:\CONTROL DE VERSIONES\Encompass 16-Dic-15\Encompass\Encompass Test\Test Txxxcom" + @"\";

        //--------------------------------------------------------------------------------------------------------------
        internal static void subTestTxxxcom1()
        {
            SyspathPath syspathLog = new SyspathPath(strPATH + "Test Txxxcom1.log");
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
            TlveorgOrganization tlveorg = new TlveorgOrganization(tbsiorg, dateBegin, "Towa", "Towa System", "");
            tbsiorg.setComponents.subAdd(tlveorg);

            TbsicomCompany tbsicom = new TbsicomCompany(tlveorg, dateBegin, "Towa");

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
                    new TlvecommxMexico(tbsicom, dateCompany, strId, strDescription, "", strRfcpm, strRpss);

                tbsicom.setComponents.subAdd(tlvelenmx);
            }

            sysswLog.WriteLine(Test.strTo(tbsicom, "tbsicom"));

            //                                          //Resumen de objetos en todas las pruebas
            BclassBaseClassAbstract.subWriteSummary(sysswLog);

            sysswLog.Dispose();
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
