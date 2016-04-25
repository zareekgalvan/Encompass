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
    static class TestTxxxlife
    {
        const String strNAME = "ARC - Alejandro de la Rosa";
        const String strPATH = @"D:\CONTROL DE VERSIONES\Encompass 17-Dic-15\Encompass\Encompass Test\Test Txxxlife" +
            @"\";

        //--------------------------------------------------------------------------------------------------------------
        //                                              //prueba los métodos boolIsDateIncluded(),
        //                                              //      boolIsPeriodIncluded() y boolIsPeriodNotIncluded()
        //                                              //      utilizando un periodo abierto.
        internal static void subTestTxxxlife1()
        {
            SyspathPath syspathLog = new SyspathPath(strPATH + "Test Txxxlife1.log");
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
            LifePeriods life = new LifePeriods(null, dateBegin, Tools.dateMAX_VALUE);

            //                                              //Prueba de boolIsDateIncluded(). Se prueba utilizando una
            //                                              // Fecha que si está incluida y una que no está incluida.
            DateTime dateIncluded = new DateTime(2003, 02, 01);
            bool boolDateIncluded = life.boolIsDateIncluded(dateIncluded);
            sysswLog.WriteLine("dateIncluded (" + Test.strToDisplay(dateIncluded) + ") is included in life (" +
            Test.strToDisplay(life) + "): " + Test.strToDisplay(boolDateIncluded));

            DateTime dateNotIncluded = new DateTime(2001, 02, 01);
            boolDateIncluded = life.boolIsDateIncluded(dateNotIncluded);
            sysswLog.WriteLine("dateNotIncluded (" + Test.strToDisplay(dateNotIncluded) + ") is included in life (" +
            Test.strToDisplay(life) + "): " + Test.strToDisplay(boolDateIncluded));
            sysswLog.WriteLine("-------------------------------------------------------------------------------------");

            //                                              //Prueba de boolIsPeriodIncluded(). Se prueba con un periodo
            //                                              //      incluído, uno que empieza afuera y termina dentro
            //                                              //      del periodo y uno que está completamente fuera.
            DateTime dateBeginIncluded = new DateTime(2002, 02, 01);
            DateTime dateEndIncluded = new DateTime(2003, 02, 01);
            DateTime dateBeginNotIncluded = new DateTime(2001, 02, 01);
            DateTime dateEndNotIncluded = new DateTime(2002, 01, 31);

            //                                              //Prueba para el periodo incluído. (2002,02,01)-(2003,02,01)
            bool boolPeriodIncluded = life.boolIsPeriodIncluded(dateBeginIncluded, dateEndIncluded);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginIncluded) + "-" +
            Test.strToDisplay(dateEndIncluded) + ") is included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodIncluded));

            //                                              //Prueba para el periodo con inicio no incluído y final si
            //                                              //      incluído. (2001,02,01)-(2003,02,01)
            boolPeriodIncluded = life.boolIsPeriodIncluded(dateBeginNotIncluded, dateEndIncluded);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginNotIncluded) + "-" +
            Test.strToDisplay(dateEndIncluded) + ") is included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodIncluded));

            //                                              //Prueba Para Periodo excluido. (2001,02,01)-(2002,01,31)
            boolPeriodIncluded = life.boolIsPeriodIncluded(dateBeginNotIncluded, dateEndNotIncluded);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginNotIncluded) + "-" +
            Test.strToDisplay(dateEndNotIncluded) + ") is included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodIncluded));
            sysswLog.WriteLine("-------------------------------------------------------------------------------------");

            //                                              //Prueba de boolIsPeriodNotIncluded(). Se prueba con un
            //                                              //      Periodo incluído, utilizando que empieza afuera y
            //                                              //      termina dentro y uno excluido.
            bool boolPeriodNotIncluded = life.boolIsPeriodNotIncluded(dateBeginIncluded, dateEndIncluded);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginIncluded) + "-" +
            Test.strToDisplay(dateEndIncluded) + ") is not included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodNotIncluded));

            //                                              //Prueba para el periodo con inicio no incluído y final si
            //                                              //      incluído. (2001,02,01)-(2003,02,01)
            boolPeriodNotIncluded = life.boolIsPeriodNotIncluded(dateBeginNotIncluded, dateEndIncluded);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginNotIncluded) + "-" +
            Test.strToDisplay(dateEndIncluded) + ") is not included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodNotIncluded));

            //                                              //Prueba Para Periodo excluido. (2001,02,01)-(2002,01,31)
            boolPeriodNotIncluded = life.boolIsPeriodNotIncluded(dateBeginNotIncluded, dateEndNotIncluded);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginNotIncluded) + "-" +
            Test.strToDisplay(dateEndNotIncluded) + ") is not included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodNotIncluded));
            sysswLog.WriteLine("-------------------------------------------------------------------------------------");

            //                                          //Resumen de objetos en todas las pruebas
            BclassBaseClassAbstract.subWriteSummary(sysswLog);

            sysswLog.Dispose();
        }

        //--------------------------------------------------------------------------------------------------------------
        //                                              //prueba los métodos boolIsDateIncluded(),
        //                                              //      boolIsPeriodIncluded() y boolIsPeriodNotIncluded()
        //                                              //      utilizando un periodo cerrado.
        internal static void subTestTxxxlife2()
        {
            SyspathPath syspathLog = new SyspathPath(strPATH + "Test Txxxlife2.log");
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
            DateTime dateEnd = new DateTime(2015, 02, 01);
            LifePeriods life = new LifePeriods(null, dateBegin, dateEnd);

            //                                              //Prueba de boolIsDateIncluded(). Se prueba utilizando una
            //                                              // Fecha que si está incluida y 2 que no están incluidas.
            DateTime dateIncluded = new DateTime(2003, 02, 01);
            bool boolDateIncluded = life.boolIsDateIncluded(dateIncluded);
            sysswLog.WriteLine("dateIncluded (" + Test.strToDisplay(dateIncluded) + ") is included in life (" +
            Test.strToDisplay(life) + "): " + Test.strToDisplay(boolDateIncluded));

            DateTime dateNotIncluded = new DateTime(2001, 02, 01);
            boolDateIncluded = life.boolIsDateIncluded(dateIncluded);
            sysswLog.WriteLine("dateNotIncluded (" + Test.strToDisplay(dateNotIncluded) + ") is included in life (" +
            Test.strToDisplay(life) + "): " + Test.strToDisplay(boolDateIncluded));

            dateNotIncluded = new DateTime(2015, 10, 01);
            boolDateIncluded = life.boolIsDateIncluded(dateIncluded);
            sysswLog.WriteLine("dateNotIncluded (" + Test.strToDisplay(dateNotIncluded) + ") is included in life (" +
            Test.strToDisplay(life) + "): " + Test.strToDisplay(boolDateIncluded));
            sysswLog.WriteLine("-------------------------------------------------------------------------------------");

            //                                              //Prueba de boolIsPeriodIncluded(). Se prueba con un periodo
            //                                              //      previo, uno que empieza afuera y termina dentro,
            //                                              //      periodo incluído, uno que inicia adentro y termina
            //                                              //      fuera y uno posterior.
            DateTime dateBeginIncluded = new DateTime(2002, 02, 01);
            DateTime dateEndIncluded = new DateTime(2003, 02, 01);
            DateTime dateBeginNotIncluded = new DateTime(2001, 02, 01);
            DateTime dateEndNotIncluded = new DateTime(2002, 01, 31);
            DateTime dateBeginNotIncludedAfter = new DateTime(2015, 02, 02);
            DateTime dateEndNotIncludedAfter = new DateTime(2015, 04, 01);

            //                                              //Prueba Para Periodo excluido previo
            //                                              //      Ejemplo: (2001,02,01)-(2002,01,31)
            bool boolPeriodIncluded = life.boolIsPeriodIncluded(dateBeginNotIncluded, dateEndNotIncluded);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginNotIncluded) + "-" +
            Test.strToDisplay(dateEndNotIncluded) + ") is included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodIncluded));

            //                                              //Prueba para el periodo con inicio no incluído y final si
            //                                              //      incluído. (2001,02,01)-(2003,02,01)
            boolPeriodIncluded = life.boolIsPeriodIncluded(dateBeginNotIncluded, dateEndIncluded);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginNotIncluded) + "-" +
            Test.strToDisplay(dateEndIncluded) + ") is included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodIncluded));

            //                                              //Prueba para el periodo incluído. (2002,02,01)-(2003,02,01)
            boolPeriodIncluded = life.boolIsPeriodIncluded(dateBeginIncluded, dateEndIncluded);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginIncluded) + "-" +
            Test.strToDisplay(dateEndIncluded) + ") is included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodIncluded));

            //                                              //Prueba para el periodo con inicio incluído y final no
            //                                              //      incluído. (2001,02,01)-(2003,02,01)
            boolPeriodIncluded = life.boolIsPeriodIncluded(dateBeginIncluded, dateEndNotIncludedAfter);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginNotIncluded) + "-" +
            Test.strToDisplay(dateEndNotIncludedAfter) + ") is included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodIncluded));

            //                                              //Prueba Para Periodo excluido posterior
            //                                              //      (2012,02,02)-(2015,04,01)
            boolPeriodIncluded = life.boolIsPeriodIncluded(dateBeginNotIncludedAfter, dateEndNotIncludedAfter);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginNotIncludedAfter) + "-" +
            Test.strToDisplay(dateEndNotIncludedAfter) + ") is included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodIncluded));
            sysswLog.WriteLine("-------------------------------------------------------------------------------------");

            //                                              //Prueba de boolIsPeriodIncluded(). Se prueba con un periodo
            //                                              //      previo, uno que empieza afuera y termina dentro,
            //                                              //      periodo incluído, uno que inicia adentro y termina
            //                                              //      fuera y uno posterior.

            //                                              //Prueba Para Periodo excluido previo
            //                                              //      Ejemplo: (2001,02,01)-(2002,01,31)
            bool boolPeriodNotIncluded = life.boolIsPeriodNotIncluded(dateBeginNotIncluded, dateEndNotIncluded);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginNotIncluded) + "-" +
            Test.strToDisplay(dateEndNotIncluded) + ") is not included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodNotIncluded));

            //                                              //Prueba para el periodo con inicio no incluído y final si
            //                                              //      incluído. (2001,02,01)-(2003,02,01)
            boolPeriodNotIncluded = life.boolIsPeriodNotIncluded(dateBeginNotIncluded, dateEndIncluded);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginNotIncluded) + "-" +
            Test.strToDisplay(dateEndIncluded) + ") is not included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodNotIncluded));

            //                                              //Prueba para el periodo incluído. (2002,02,01)-(2003,02,01)
            boolPeriodNotIncluded = life.boolIsPeriodNotIncluded(dateBeginIncluded, dateEndIncluded);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginIncluded) + "-" +
            Test.strToDisplay(dateEndIncluded) + ") is not included in life (" + Test.strToDisplay(life) + "): " +
            Test.strToDisplay(boolPeriodNotIncluded));

            //                                              //Prueba para el periodo con inicio incluído y final no
            //                                              //      incluído. (2001,02,01)-(2003,02,01)
            boolPeriodNotIncluded = life.boolIsPeriodNotIncluded(dateBeginIncluded, dateEndNotIncludedAfter);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginNotIncluded) + "-" +
            Test.strToDisplay(dateEndNotIncludedAfter) + ") is not included in life (" +
            Test.strToDisplay(life) + "): " + Test.strToDisplay(boolPeriodNotIncluded));

            //                                              //Prueba Para Periodo excluido posterior
            //                                              //      (2012,02,02)-(2015,04,01)
            boolPeriodNotIncluded = life.boolIsPeriodNotIncluded(dateBeginNotIncludedAfter, dateEndNotIncludedAfter);
            sysswLog.WriteLine("Period (" + Test.strToDisplay(dateBeginNotIncludedAfter) + "-" +
            Test.strToDisplay(dateEndNotIncludedAfter) + ") is not included in life (" +
            Test.strToDisplay(life) + "): " + Test.strToDisplay(boolPeriodNotIncluded));
            sysswLog.WriteLine("-------------------------------------------------------------------------------------");

            //                                          //Resumen de objetos en todas las pruebas
            BclassBaseClassAbstract.subWriteSummary(sysswLog);

            sysswLog.Dispose();
        }

        //--------------------------------------------------------------------------------------------------------------
        //                                              //prueba los métodos subAddPeriod() y subRemovePeriod()
        //                                              //      utilizando un periodo cerrado.
        internal static void subTestTxxxlife3()
        {
            SyspathPath syspathLog = new SyspathPath(strPATH + "Test Txxxlife3.log");
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

            DateTime dateBegin = new DateTime(2014, 12, 01);
            DateTime dateEnd = new DateTime(2015, 12, 01);
            LifePeriods life = new LifePeriods(null, dateBegin, dateEnd);

            DateTime lastPeriodBegin = new DateTime(2016, 01, 15);
            DateTime lastPeriodEnd = new DateTime(2016, 01, 31);
            life.subAddPeriod(lastPeriodBegin, lastPeriodEnd);

            DateTime secondPeriodBegin = new DateTime(2010, 12, 01);
            DateTime secondPeriodEnd = new DateTime(2011, 12, 01);
            life.subAddPeriod(secondPeriodBegin, secondPeriodEnd);

            DateTime thirdPeriodBegin = new DateTime(2015, 12, 04);
            DateTime thirdPeriodEnd = new DateTime(2015, 12, 31);
            life.subAddPeriod(thirdPeriodBegin, thirdPeriodEnd);

            DateTime fourthPeriodBegin = new DateTime(2015, 12, 02);
            DateTime fourthPeriodEnd = new DateTime(2015, 12, 03);
            life.subAddPeriod(fourthPeriodBegin, fourthPeriodEnd);

            DateTime fifthPeriodBegin = new DateTime(2014, 08, 30);
            DateTime fifthPeriodEnd = new DateTime(2014, 09, 30);
            life.subAddPeriod(fifthPeriodBegin, fifthPeriodEnd);

            DateTime sixthPeriodBegin = new DateTime(2014, 10, 01);
            DateTime sixthPeriodEnd = new DateTime(2014, 10, 31);
            life.subAddPeriod(sixthPeriodBegin, sixthPeriodEnd);

            DateTime seventhPeriodBegin = new DateTime(2014, 08, 01);
            DateTime seventhPeriodEnd = new DateTime(2014, 08, 29);
            life.subAddPeriod(seventhPeriodBegin, seventhPeriodEnd);

            sysswLog.WriteLine("Life (" + Test.strToDisplay(life) + ")");

             DateTime periodToRemoveBegin = new DateTime(2010, 12, 01);
            DateTime periodToRemoveEnd = new DateTime(2011, 12, 01);
            life.subRemovePeriod(periodToRemoveBegin, periodToRemoveEnd);

            periodToRemoveBegin = new DateTime(2014, 08, 02);
            periodToRemoveEnd = new DateTime(2014, 10, 30);
            life.subRemovePeriod(periodToRemoveBegin, periodToRemoveEnd);

            periodToRemoveBegin = new DateTime(2014, 12, 01);
            periodToRemoveEnd = new DateTime(2014, 12, 31);
            life.subRemovePeriod(periodToRemoveBegin, periodToRemoveEnd);

            periodToRemoveBegin = new DateTime(2016, 01, 20);
            periodToRemoveEnd = new DateTime(2016, 01, 31);
            life.subRemovePeriod(periodToRemoveBegin, periodToRemoveEnd);

            sysswLog.WriteLine("Life (" + Test.strToDisplay(life) + ")");

            //                                          //Resumen de objetos en todas las pruebas
            BclassBaseClassAbstract.subWriteSummary(sysswLog);

            sysswLog.Dispose();
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
