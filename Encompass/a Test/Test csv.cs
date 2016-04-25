using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using TowaInfrastructure;
using Encompass;
using AllInMemoryBase;
using EncompassInfrastructure;
using Microsoft.VisualBasic.FileIO;

namespace Encompass
{
    static class TestCsv
    {
        const String strNAME = "ARC - Alejandro de la Rosa";
        const String strPATH = @"C:\Users\alejandro.delarosa\Desktop\Encompass 29-Oct-15\Encompass\Encompass Test\Test csv" + @"\";

        internal static void subTestCsv1()
        {
            SyspathPath syspathLog = new SyspathPath(strPATH + "Test csv1.log");
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

            SyspathPath syspathPrueba = new SyspathPath(strPATH + "Prueba.csv");
            FileInfo sysfilePrueba = Sys.sysfileNew(syspathPrueba);
            StreamReader syssrPrueba = Sys.syssrNewTextFile(sysfilePrueba);

            while (!syssrPrueba.EndOfStream)
            {
                String strRecord = syssrPrueba.ReadLine();
                String[] arrstrData = strRecord.Split(',');

                foreach (String strDato in arrstrData)
                {
                    sysswLog.WriteLine(strDato);
                }
            }

            //                                          //Resumen de objetos en todas las pruebas
            BclassBaseClassAbstract.subWriteSummary(sysswLog);

            sysswLog.Dispose();
        }

        internal static void subTestCsv2()
        {
            SyspathPath syspathLog = new SyspathPath(strPATH + "Test csv2.log");
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

            SyspathPath syspathPrueba = new SyspathPath(strPATH + "PruebaConComas.csv");
            FileInfo sysfilePrueba = Sys.sysfileNew(syspathPrueba);
            StreamReader syssrPrueba = Sys.syssrNewTextFile(sysfilePrueba);

            while (!syssrPrueba.EndOfStream)
            {
                String strRecord = syssrPrueba.ReadLine();
                String[] arrstrData = strRecord.Split(',');

                foreach (String strDato in arrstrData)
                {
                    sysswLog.WriteLine(strDato);
                }
            }

            //                                          //Resumen de objetos en todas las pruebas
            BclassBaseClassAbstract.subWriteSummary(sysswLog);

            sysswLog.Dispose();
        }

        internal static void subTestCsv3()
        {
            SyspathPath syspathLog = new SyspathPath(strPATH + "Test csv3.log");
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

            SyspathPath syspathPrueba = new SyspathPath(strPATH + "Prueba.csv");
            FileInfo sysfilePrueba = Sys.sysfileNew(syspathPrueba);
            StreamReader syssrPrueba = Sys.syssrNewTextFile(sysfilePrueba);

            TextFieldParser tfpParser = new TextFieldParser(syssrPrueba);
            tfpParser.HasFieldsEnclosedInQuotes = true;
            tfpParser.Delimiters = new[] { "," };

            while(!tfpParser.EndOfData)
            {
                String[] arrstrData = tfpParser.ReadFields();

                foreach (String strDato in arrstrData)
                {
                    sysswLog.WriteLine(strDato);
                }
            }

            //                                          //Resumen de objetos en todas las pruebas
            BclassBaseClassAbstract.subWriteSummary(sysswLog);

            sysswLog.Dispose();
        }

        internal static void subTestCsv4()
        {
            SyspathPath syspathLog = new SyspathPath(strPATH + "Test csv4.log");
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

            SyspathPath syspathPrueba = new SyspathPath(strPATH + "PruebaConComas.csv");
            FileInfo sysfilePrueba = Sys.sysfileNew(syspathPrueba);
            StreamReader syssrPrueba = Sys.syssrNewTextFile(sysfilePrueba);

            TextFieldParser tfpParser = new TextFieldParser(syssrPrueba);
            tfpParser.HasFieldsEnclosedInQuotes = true;
            tfpParser.Delimiters = new[] { "," };

            while (!tfpParser.EndOfData)
            {
                String[] arrstrData = tfpParser.ReadFields();

                foreach (String strDato in arrstrData)
                {
                    sysswLog.WriteLine(strDato);
                }
            }

            //                                          //Resumen de objetos en todas las pruebas
            BclassBaseClassAbstract.subWriteSummary(sysswLog);

            sysswLog.Dispose();
        }
    }
}
