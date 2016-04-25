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
    static class TestEncompass
    {
        const String strNAME = "ARC - Alejandro de la Rosa";
        const String strPATHFILES = @"D:\CONTROL DE VERSIONES\Encompass 31-Dic-15\Encompass\Encompass Test\Test Encompass\CSVs Encompass" + @"\";
        const String strPATH = @"D:\CONTROL DE VERSIONES\Encompass 31-Dic-15\Encompass\Encompass Test\Test Encompass" + @"\";
        //--------------------------------------------------------------------------------------------------------------
        internal static void subTestEncompass1()
        {
            SyspathPath syspathLog = new SyspathPath(strPATH + "Test Encompass1.log");
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

            Encompass encompass = new Encompass(strPATHFILES);

            sysswLog.WriteLine(Test.strToDisplay(encompass));
            //                                          //Resumen de objetos en todas las pruebas
            BclassBaseClassAbstract.subWriteSummary(sysswLog);

            sysswLog.Dispose();
        }
    }
}

