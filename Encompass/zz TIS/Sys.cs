/*TASK Sys Support IO*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

//                                                          //AUTHOR: Towa (GLG-Gerardo López).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: 18-Febrero-2014.
//                                                          //PURPOSE:
//                                                          //Static methods to manage IO.

namespace TowaInfrastructure
{
    //==================================================================================================================
    public static class Sys
    {
        //--------------------------------------------------------------------------------------------------------------
        static Sys(                                         //Prepara las constantes para poder utilizarlas.
            //                                              //CADA VEZ QUE SE AÑADAN CONSTANTES QUE REQUIERAN SER
            //                                              //      INICIALIZADAS, SE AÑADE LA LLAMADA A OTRO MÉTODO.
            )
        {
        }

        //==============================================================================================================
        /*TASK Sys.Dir+File Directories and Files*/
        //--------------------------------------------------------------------------------------------------------------
        public static DirectoryInfo sysdirNew(              //Crea un DirectoryInfo, el directorio puede no existir.

            //                                              //sysdir, DirectoryInfo creado.

            //                                              //Path (completo y válido) del directorio a crear.
            SyspathPath syspathDirectory_I
            )
        {
            //                                              //Creo el DirectoryInfo.
            DirectoryInfo sysdirNew;
            /*NSTD*/
            try
            {
                sysdirNew = new DirectoryInfo(syspathDirectory_I.strFullPath);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathDirectory_I, "syspathDirectory_I") + ", " + 
                    Test.strTo(sysexcepError, "sysexcepError") +
                    " error in new DirectoryInfo(syspathDirectory_I.strFullPath)");

                sysdirNew = null;
            }
            /*END-NSTD*/

            //                                              //Regresa el DirectoryInfo creado.
            return sysdirNew;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static FileInfo sysfileNew(                  //Crea un FileInfo, el archivo puede no existir.

            //                                              //sysfile, FileInfo creado.

            //                                              //Path (completo y válido) del archivo a crear.
            SyspathPath syspathFile_I
            )
        {
            //                                              //Creo el FileInfo.
            FileInfo sysfileNew;
            /*NSTD*/
            try
            {
                sysfileNew = new FileInfo(syspathFile_I.strFullPath);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathFile_I, "syspathFile_I") + ", " + 
                    Test.strTo(sysexcepError, "sysexcepError") + 
                    " error in new FileInfo(syspathFile_I.strFullPath)");

                sysfileNew = null;
            }
            /*END-NSTD*/

            //                                              //Regresa el FileInfo creado.
            return sysfileNew;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static DirectoryInfo sysdirGetCurrentDirectory(
            //                                              //Localiza el directorio sobre el que se encuentra
            //                                              //      posicionada la aplicación.

            //                                              //sysdir, Current Directory.
            )
        {
            //                                              //Busco el current directory.
            String strCurrentDirectory;
            /*NSTD*/
            try
            {
                strCurrentDirectory = Directory.GetCurrentDirectory();
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(sysexcepError, "sysexcepError") + 
                    " error in Directory.GetCurrentDirectory()");

                strCurrentDirectory = null;
            }
            /*END-NSTD*/

            //                                              //Regresa un DirectoryInfo.
            SyspathPath syspathCurrentDirectory = new SyspathPath(strCurrentDirectory);
            return Sys.sysdirNew(syspathCurrentDirectory);
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subSetCurrentDirectory(          //Establece el Current Durectory.

            //                                              //DirectroyInfo sobre el que se desea posicionar.
            DirectoryInfo sysdirToSet_I
            )
        {
            //                                              //Crea el syspath del directorio, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathToSet = Sys.syspathGet(sysdirToSet_I);

            if (
                !syspathToSet.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathToSet, "syspathToSet") + " do not exist as directory");

            //                                              //Establece el Current Directory a partir de un path.
            /*NSTD*/
            try
            {
                Directory.SetCurrentDirectory(syspathToSet.strFullPath);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToSet, "syspathToSet") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") +
                    " error in Directory.SetCurrentDirectory(syspathDirToSet.strFullPath)");
            }
            /*END-NSTD*/
        }

        //--------------------------------------------------------------------------------------------------------------
        public static SyspathPath syspathGet(               //Extrae el syspath correspondiente al directorio.

            //                                              //syspath, similar al que se uso para crear el sysdir con su
            //                                              //      estado actualizado.

            //                                              //DirectoryInfo del cual se quiere información.
            DirectoryInfo sysdirToGetPath_I
            )
        {
            //                                              //Extrae el directory full path.
            String strFullPath;
            /*NSTD*/
            try
            {
                strFullPath = sysdirToGetPath_I.FullName;
            }
            catch (Exception sysexcepError)
            {
                //                                          //Nótese que no puedo desplegar el objeto syspath como lo
                //                                          //      estoy haciento en la mayoría de los diagnósticos.
                Tools.subAbort(Test.strTo(sysdirToGetPath_I, "sysdirToGetPath_I") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysdirToGetPath_I.FullName");

                strFullPath = null;
            }
            /*END-NSTD*/

            //                                              //Regresa el path con su estado actualizado.
            return new SyspathPath(strFullPath);
        }

        //--------------------------------------------------------------------------------------------------------------
        public static SyspathPath syspathGet(               //Extrae el syspath correspondiente al archivo.

            //                                              //syspath, similar al que se uso para crear el sysfile con
            //                                              //      su estado actualizado.

            //                                              //FileInfo del cual se quiere información.
            FileInfo sysfileToGetPath_I
            )
        {
            //                                              //Extre el file full path.
            String strFullPath;
            /*NSTD*/
            try
            {
                strFullPath = sysfileToGetPath_I.FullName;
            }
            catch (Exception sysexcepError)
            {
                //                                          //Nótese que no puedo desplegar el objeto syspath como lo
                //                                          //      estoy haciento en la mayoría de los diagnósticos.
                Tools.subAbort(Test.strTo(sysfileToGetPath_I, "sysfileToGetPath_I") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysfileToGetPath_I.FullName");

                strFullPath = null;
            }
            /*END-NSTD*/

            //                                              //Regresa el syspath con su estado actualizado.
            return new SyspathPath(strFullPath);
        }

        //--------------------------------------------------------------------------------------------------------------
        public static DirectoryInfo sysdirGetParent(        //Busca el directorio padre inmediato superior.

            //                                              //sysdir, correspondiente al directorio padre, o null si
            //                                              //      estaba en la raíz.

            //                                              //DirectoryInfo del cual se quiere información.
            DirectoryInfo sysdirToGetParent_I
            )
        {
            //                                              //Crea el syspath del directorio, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathDirectoryToGetParent = Sys.syspathGet(sysdirToGetParent_I);

            //                                              //Puede no existir, pero si existe debe ser directorio
            if (
                //                                          //Es un FILE.
                syspathDirectoryToGetParent.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathDirectoryToGetParent, "syspathDirectoryToGetParent") +
                    " it is not a directory, this is a file");

            //                                              //Extrae el padre.
            DirectoryInfo sysdirGetParent;
            /*NSTD*/
            try
            {
                sysdirGetParent = sysdirToGetParent_I.Parent;
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathDirectoryToGetParent, "syspathDirectoryToGetParent") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysdirToGetParent_I.Parent");

                sysdirGetParent = null;
            }
            /*END-NSTD*/

            //                                              //Regresa el padre.
            return sysdirGetParent;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static DirectoryInfo sysdirGetDirectory(     //Busca el directorio en el que esta el archivo.

            //                                              //sysdir, DirectoryInfo del archivo.

            //                                              //FileInfo del cual se quiere información.
            FileInfo sysfileToGetDirectory_I
            )
        {
            //                                              //Crea el syspath del archivo, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathDirectoryToGetDirectory = Sys.syspathGet(sysfileToGetDirectory_I);

            //                                              //Puede no existir, pero si existe debe ser archivo
            if (
                //                                          //Es un directorio.
                syspathDirectoryToGetDirectory.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathDirectoryToGetDirectory, "syspathDirectoryToGetDirectory") +
                    " it is not a file, this is a directory");

            //                                              //Extrae el directorio del archivo.
            DirectoryInfo sysdirGetDirectory;
            /*NSTD*/
            try
            {
                sysdirGetDirectory = sysfileToGetDirectory_I.Directory;
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathDirectoryToGetDirectory, "syspathDirectoryToGetDirectory") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysfileToGetDirectory_I.Directory");

                sysdirGetDirectory = null;
            }
            /*END-NSTD*/

            //                                              //Regresa el directorio del archivo.
            return sysdirGetDirectory;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static long longGetFileLength(               //Extrae DEL DISCO la longitud del archivo.

            //                                              //long, longitud del archivo (la toma del DISCO).

            //                                              //FileInfo del cual se quiere información.
            FileInfo sysfileToGetFileLength_I
            )
        {
            //                                              //Crea el syspath del archivo, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathFileToGetFileLength = Sys.syspathGet(sysfileToGetFileLength_I);

            if (
                !syspathFileToGetFileLength.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathFileToGetFileLength, "syspathFileToGetFileLength") +
                    " it is not a file");

            //                                              //Extrae la longitud del archivo.
            long longGetFileLength;
            /*NSTD*/
            try
            {
                //                                          //Toma la longitud DEL DISCO.
                longGetFileLength = sysfileToGetFileLength_I.Length;
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathFileToGetFileLength, "syspathFileToGetFileLength") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysfileToGetFileLength_I.Length");

                longGetFileLength = -1;
            }
            /*END-NSTD*/

            //                                              //Regresa longitud.
            return longGetFileLength;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subSetReadOnly(                  //Set ReadOnly del archivo a true.

            //                                              //FileInfo del archivo que se quiere modificar.
            FileInfo sysfileToUpdate_M
            )
        {
            //                                              //Crea el syspath del archivo, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathToUpdate = Sys.syspathGet(sysfileToUpdate_M);

            if (
                !syspathToUpdate.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathToUpdate, "syspathToUpdate") + " it is not a file");

            //                                              //Modifica propiedad.
            /*NSTD*/
            try
            {
                sysfileToUpdate_M.IsReadOnly = true;
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToUpdate, "syspathToUpdate") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysfileToUpdate_M.IsReadOnly = true");
            }
            /*END-NSTD*/
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subResetReadOnly(                //Permite que se puede escribir en el archivo.

            //                                              //FileInfo del archivo que se quiere modificar.
            FileInfo sysfileToUpdate_M
            )
        {
            //                                              //Crea el syspath del archivo, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathToUpdate = Sys.syspathGet(sysfileToUpdate_M);

            if (
                !syspathToUpdate.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathToUpdate, "syspathToUpdate") + " it is not a file");

            //                                              //Modifica propiedad.
            /*NSTD*/
            try
            {
                sysfileToUpdate_M.IsReadOnly = false;
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToUpdate, "syspathToUpdate") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysfileToUpdate_M.IsReadOnly = false");
            }
            /*END-NSTD*/
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subRefresh(                      //Refresca la información de un DirectoryInfo.

            //                                              //DirectoryInfo que se desea refrescar.
            DirectoryInfo sysdirToRefresh_M
            )
        {
            //                                              //Crea el syspath del directorio, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathToRefresh = Sys.syspathGet(sysdirToRefresh_M);

            if (
                !syspathToRefresh.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathToRefresh, "syspathToRefresh") + " it is not a directory");

            //                                              //Hace el refresh.
            /*NSTD*/
            try
            {
                sysdirToRefresh_M.Refresh();
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToRefresh, "syspathToRefresh") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysdirToRefresh_I.Refresh()");
            }
            /*END-NSTD*/
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subRefresh(                      //Refresca la información de un FileInfo.

            //                                              //FileInfo que se desea refrescar.
            FileInfo sysfileToRefresh_M
            )
        {
            //                                              //Crea el syspath del archivo, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathToRefresh = Sys.syspathGet(sysfileToRefresh_M);

            if (
                !syspathToRefresh.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathToRefresh, "syspathToRefresh") + " it is not a file");

            //                                              //Hace el refresh.
            /*NSTD*/
            try
            {
                sysfileToRefresh_M.Refresh();
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToRefresh, "syspathToRefresh") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysfileToRefresh_I.Refresh()");
            }
            /*END-NSTD*/
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subCreateDirectoryOnDisk(        //A partir de un DirectoryInfo CREA el directorio en disco.

            //                                              //DirectroyInfo con el cual se desea crear directorio en
            //                                              //      disco.
            DirectoryInfo sysdirToCreateOnDisk_M
            )
        {
            //                                              //Necesito un syspath para verificar la existencia ya sea
            //                                              //      como directorio o archivo.
            SyspathPath syspathToCreateOnDisk = Sys.syspathGet(sysdirToCreateOnDisk_M);

            if (
                //                                          //Ya existe como directorio o como archivo.
                syspathToCreateOnDisk.boolExists
                )
                Tools.subAbort(Test.strTo(syspathToCreateOnDisk, "syspathToCreateOnDisk") +
                    " can not create a directory, already exist as a directory o as a file");

            //                                              //Crea el directorio en disco.
            /*NSTD*/
            try
            {
                sysdirToCreateOnDisk_M.Create();
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToCreateOnDisk, "syspathToCreateOnDisk") + ", " + 
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysdirToCreateOnDisk_I.Create()");
            }
            /*END-NSTD*/
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subRename(                       //Modifica el nombre de un directorio (último
            //                                              //      subdirectorio), usará MoveTo, pero para que solo
            //                                              //      cambie el nombre del último subdirectorio.

            //                                              //DirectoryInfo del directorio que se quiere renombrar.
            DirectoryInfo sysdirToRename_M,
            //                                              //Nuevo nombre del subdirectorio (sin el path).
            String strNewSubdirectoryName_I
            )
        {
            //                                              //Crea el syspath del directorio, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathToRename = Sys.syspathGet(sysdirToRename_M);

            if (
                !syspathToRename.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathToRename, "syspathToRename") + " do not exist as directory");

            if (
                //                                          //El nuevo nombre es el mismo.
                strNewSubdirectoryName_I == sysdirToRename_M.Name
                )
                Tools.subAbort(Test.strTo(syspathToRename, "syspathToRename") + ", " +
                    Test.strTo(strNewSubdirectoryName_I, "strNewSubdirectoryName_I") +
                    " can not rename");

            //                                              //Crea el nuevo path para confirmar que su forma es válida.
            SyspathPath syspathRanamed = syspathToRename.syspathParent.syspathAddName(strNewSubdirectoryName_I);

            if (
                //                                          //Ya existe un archivo o directorio con el mismo nombre.
                syspathRanamed.boolExists
                )
                Tools.subAbort(Test.strTo(syspathRanamed, "syspathRanamed") +
                    " can not rename, already exists as directory o file");

            //                                              //Renombra el subdirectorio usando el MoveTo.
            //                                              //Nótese que ya se hicieron muchas verificaciones para hacer
            //                                              //      esto en forma segura.
            /*NSTD*/
            try
            {
                //                                          //Nótese que se mueve SOBRE el directorio renombrado (NO 
                //                                          //      queda abajo como un subdirectorio).
                sysdirToRename_M.MoveTo(syspathRanamed.strFullPath);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToRename, "syspathToRename") + ", " +
                    Test.strTo(syspathRanamed, "syspathRanamed") + ", " + Test.strTo(sysexcepError, "sysexcepError") +
                    " error in sysdirToRename_M.MoveTo(syspathRanamed.strFullPath)");
            }
            /*END-NSTD*/
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subRename(                       //Modifica el nombre de un archivo, usará MoveTo, pero
            //                                              //      que solo cambie el nombre.

            //                                              //FileInfo del archivo que se quiere renombrar.
            FileInfo sysfileToRename_M,
            //                                              //Nuevo nombre del archivo (sin el path).
            String strNewFileName_I
            )
        {
            //                                              //Crea el syspath del archivo, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathToRename = Sys.syspathGet(sysfileToRename_M);

            if (
                !syspathToRename.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathToRename, "syspathToRename") + " do not exist as file");

            if (
                //                                          //El nuevo nombre es el mismo.
                strNewFileName_I == sysfileToRename_M.Name
                )
                Tools.subAbort(Test.strTo(syspathToRename, "syspathToRename") + ", " +
                    Test.strTo(strNewFileName_I, "strNewFileName_I") + " rename to a diferent name");

            //                                              //Crea el nuevo path para confirmar que su forma es válida.
            SyspathPath syspathRanamed = syspathToRename.syspathDirectory.syspathAddName(strNewFileName_I);

            if (
                //                                          //Ya existe un archivo o directorio con el mismo nombre.
                syspathRanamed.boolExists
                )
                Tools.subAbort(Test.strTo(syspathRanamed, "syspathRanamed") +
                    " can not rename, already exist a directory or a file with same name");

            //                                              //Renombre el archivo usando el MoveTo.
            //                                              //Nótese que ya se hicieron muchas verificaciones para hacer
            //                                              //      esto en forma segura.
            /*NSTD*/
            try
            {
                sysfileToRename_M.MoveTo(syspathRanamed.strFullPath);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToRename, "syspathToRename") + ", " + 
                    Test.strTo(syspathRanamed, "syspathRanamed") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") +
                    " error in sysfileToRename_M.MoveTo(syspathRanamed.strFullPath)");
            }
            /*END-NSTD*/
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subCopyDirectoryAllBranch(       //Copia el directorio (con todo su contenido) a otro
            //                                              //      directorio, se copia con el mismo nombre.
            //                                              //No debe existir el nombre en el nuevo padre.

            //                                              //DirectoryInfo del directorio que se quiere copiar.
            DirectoryInfo sysdirToCopy_I,
            //                                              //DirectoryInfo del directorio al cual se desea copiar, este
            //                                              //      será el nuevo padre.
            DirectoryInfo sysdirNewParent_M,
            //                                              //Regresa el DirectoryInfo del directorio nuevo (lo que 
            //                                              //      quedo ya copiado).
            out DirectoryInfo sysdirCopied_O
            )
        {
            //                                              //Verifica que ambos directorios existan.
            SyspathPath syspathToCopy = Sys.syspathGet(sysdirToCopy_I);
            if (
                //                                          //No es un directorio.
                !syspathToCopy.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathToCopy, "syspathToCopy") + " do not exist as directory");
            SyspathPath syspathNewParent = Sys.syspathGet(sysdirNewParent_M);
            if (
                //                                          //No es un directorio.
                !syspathNewParent.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathNewParent, "syspathNewParent") + " do not exist as directory");

            //                                              //Verifica que el nuevo directorio no exista.
            SyspathPath syspathCopied = syspathNewParent.syspathAddName(syspathToCopy.strName);
            if (
                //                                          //El nuevo syspath, ya existe.
                syspathCopied.boolExists
                )
                Tools.subAbort(Test.strTo(syspathCopied, "syspathCopied") +
                    " can not copy, already exists a directory or file with same name");

            //                                              //Crea el nuevo sysdir y el directorio en el disco.
            sysdirCopied_O = Sys.sysdirNew(syspathCopied);
            Sys.subCreateDirectoryOnDisk(sysdirCopied_O);

            //                                              //Copia todos los archivos que se encuentran en el nivel
            //                                              //      inmediato.
            FileInfo[] arrsysfileToCopy = Sys.arrsysfileGetFiles(sysdirCopied_O);
            foreach (FileInfo sysfileF in arrsysfileToCopy)
            {
                //                                          //Copia cada uno, obviamente no habra remplazos.
                FileInfo sysfile;
                Sys.subCopyFileWrite(sysfileF, sysdirCopied_O, out sysfile);
            }

            //                                              //Copia todos los subdirectorios que se encuentran en el
            //                                              //      nivel inmediato.
            DirectoryInfo[] arrsysdirToCopy = Sys.arrsysdirGetDirectories(sysdirCopied_O);
            foreach (DirectoryInfo sysdirD in arrsysdirToCopy)
            {
                //                                          //Copia cada uno de los subdirectorios (llamada recursiva).
                DirectoryInfo sysdir;
                Sys.subCopyDirectoryAllBranch(sysdirD, sysdirCopied_O, out sysdir);
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subCopyFileWrite(                //Copia un archivo a otro directorio donde no existe.

            //                                              //FileInfo del archivo que se quiere copiar.
            FileInfo sysfileToCopy_I,
            //                                              //DirectoryInfo del directorio al cual se desea copiar el
            //                                              //      archivo, este será el nuevo padre.
            DirectoryInfo sysdirNewParent_M,
            //                                              //FileInfo del archivo donde se va a regresar.
            out FileInfo sysfileWrited_O
            )
        {
            //                                              //Verifica que el archivo y el directorio existan.
            SyspathPath syspathToCopy = Sys.syspathGet(sysfileToCopy_I);
            if (
                //                                          //No es un archivo.
                !syspathToCopy.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathToCopy, "syspathToCopy") + " file do not exist");

            SyspathPath syspathNewParent = Sys.syspathGet(sysdirNewParent_M);
            if (
                //                                          //No es un directorio.
                !syspathNewParent.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathNewParent, "syspathNewParent") + " is not a directory");

            //                                              //Verifica que el archivo receptor no exista.
            SyspathPath syspathFileToWrite = syspathNewParent.syspathAddName(syspathToCopy.strName);
            if (
                //                                          //El nuevo syspatth existe como DIRECTORIO o FILE.
                syspathFileToWrite.boolExists
                )
                Tools.subAbort(Test.strTo(syspathFileToWrite, "syspathFileToWrite") +
                    " already exists as directory or file");

            /*NSTD*/
            try
            {
                sysfileToCopy_I.CopyTo(syspathFileToWrite.strFullPath, true);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToCopy, "syspathToCopy") + ", " +
                    Test.strTo(syspathFileToWrite, "syspathFileToWrite") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") +
                    " error in sysfileToCopy_I.CopyTo(syspathFileToRewrite.strFullPath, false)");
            }
            /*END-NSTD*/

            //                                              //Regresa el nuevo FileInfo.
            sysfileWrited_O = Sys.sysfileNew(syspathFileToWrite);

            if (!(
                (sysfileWrited_O.Length == sysfileToCopy_I.Length) &&
                (sysfileWrited_O.IsReadOnly == sysfileToCopy_I.IsReadOnly)
                ))
                Tools.subAbort(Test.strTo(syspathToCopy, "syspathToCopy") + ", " +
                    Test.strTo(syspathFileToWrite, "syspathFileToWrite") + ", " +
                    Test.strTo(sysfileWrited_O, "sysfileWrited_O") + ", " +
                    Test.strTo(sysfileToCopy_I, "sysfileToCopy_I") +
                    " VERIFY VERIFY, after write the file seams to be different");
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subCopyFileRewrite(              //Copia un archivo a otro directorio donde ya existe y se
            //                                              //      debe reescribir.
            //                                              //No se permite reescribir el archivo si el receptor es
            //                                              //      ReadOnly.

            //                                              //FileInfo del archivo que se quiere copiar.
            FileInfo sysfileToCopy_I,
            //                                              //DirectoryInfo del directorio al cual se desea copiar el
            //                                              //      archivo, este será el nuevo padre.
            DirectoryInfo sysdirNewParent_M,
            //                                              //FileInfo del archivo donde se va a regresar.
            out FileInfo sysfileRewrited_O
            )
        {
            //                                              //Verifica que el archivo y el directorio existan.
            SyspathPath syspathToCopy = Sys.syspathGet(sysfileToCopy_I);
            if (
                //                                          //No es un archivo.
                !syspathToCopy.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathToCopy, "syspathToCopy") + " file do not exist");

            SyspathPath syspathNewParent = Sys.syspathGet(sysdirNewParent_M);
            if (
                //                                          //No es un directorio.
                !syspathNewParent.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathNewParent, "syspathNewParent") +
                    " directory do not exist");

            //                                              //Verifica que el archivo receptor exista y se pueda
            //                                              //      reescribir.
            SyspathPath syspathFileToRewrite = syspathNewParent.syspathAddName(syspathToCopy.strName);
            if (
                //                                          //El nuevo syspatth no existe como FILE.
                !syspathFileToRewrite.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathFileToRewrite, "syspathFileToRewrite") +
                    " does not exist, you can try subCopyFileWrite");

            FileInfo sysfileToRewrite = Sys.sysfileNew(syspathFileToRewrite);
            if (
                sysfileToRewrite.IsReadOnly
                )
                Tools.subAbort(Test.strTo(syspathFileToRewrite, "syspathFileToRewrite") + ", " + 
                    Test.strTo(sysfileToRewrite, "sysfileToRewrite") + " is ReadOnly, can not rewrite");

            /*NSTD*/
            try
            {
                sysfileToCopy_I.CopyTo(syspathFileToRewrite.strFullPath, true);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToCopy, "syspathToCopy") + ", " +
                    Test.strTo(syspathFileToRewrite, "syspathFileToRewrite") + ", " + 
                    Test.strTo(sysexcepError, "sysexcepError") +
                    " error in sysfileToCopy_I.CopyTo(syspathFileToRewrite.strFullPath, true)");
            }
            /*END-NSTD*/

            //                                              //Regresa el nuevo FileInfo.
            Sys.subRefresh(sysfileToRewrite);
            sysfileRewrited_O = sysfileToRewrite;

            if (!(
                (sysfileRewrited_O.Length == sysfileToCopy_I.Length) &&
                (sysfileRewrited_O.IsReadOnly == sysfileToCopy_I.IsReadOnly)
                ))
                Tools.subAbort(Test.strTo(syspathToCopy, "syspathToCopy") + ", " +
                    Test.strTo(syspathFileToRewrite, "syspathFileToRewrite") + ", " +
                    Test.strTo(sysfileRewrited_O, "sysfileRewrited_O") + ", " +
                    Test.strTo(sysfileToCopy_I, "sysfileToCopy_I") +
                    " ALGO EXTRAÑO PASO, el archivo al reescribirse no conservo sus mismas características");
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subMoveTo(                       //Mueve un directorio a ser parte de otro directorio, no
            //                                              //      no debe existir en el nuevo padre.
            //                                              //Este move solo puede ser al mismo dispositivo.

            //                                              //DirectoryInfo del directorio que se quiere mover, lo
            //                                              //      actualiza a la nueva ubicación.
            DirectoryInfo sysdirToMove_M,
            //                                              //DirectoryInfo del directorio al cual se desea mover el
            //                                              //      directorio anterior, este será el nuevo padre.
            DirectoryInfo sysdirNewParent_M
            )
        {
            //                                              //Verifica que ambos directorios existan.
            SyspathPath syspathToMove = Sys.syspathGet(sysdirToMove_M);
            if (
                //                                          //No es un directorio.
                !syspathToMove.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathToMove, "syspathToMove") + " directory do not exist");
            SyspathPath syspathNewParent = Sys.syspathGet(sysdirNewParent_M);
            if (
                //                                          //No es un directorio.
                !syspathNewParent.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathNewParent, "syspathNewParent") +
                    " directory do not exist");

            if (
                //                                          //Están en raices distintas.
                syspathToMove.syspathRoot.strFullPath != syspathNewParent.syspathRoot.strFullPath
                )
                Tools.subAbort(Test.strTo(syspathToMove, "syspathToMove") + ", " +
                    Test.strTo(syspathNewParent, "syspathNewParent") + " can not move, they are not in the same root");

            //                                              //Forma el syspath del directorio ya movido.
            SyspathPath syspathMoved = syspathNewParent.syspathAddName(syspathToMove.strName);

            //                                              //Aborta si existe otro directorio o archivo con el mismo
            //                                              //      nombre en el el disco.
            if (
                //                                          //El nuevo syspath, ya existe.
                syspathMoved.boolExists
                )
                Tools.subAbort(Test.strTo(syspathMoved, "syspathMoved") +
                    " can not move, already exists a directory or file with same name");

            //                                              //Mueve el directorio al nuevo directorio ("YaMOvido").
            /*NSTD*/
            try
            {
                //                                          //Nótese que se mueve abajo del NewParent y que conserva el
                //                                          //      nombre (última parte) que tenía.
                sysdirToMove_M.MoveTo(syspathMoved.strFullPath);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToMove, "syspathToMove") + ", " +
                    Test.strTo(syspathMoved, "syspathMoved") + ", " + Test.strTo(sysexcepError, "sysexcepError") +
                    " error in sysdirToMove_I.MoveTo(syspathMoved.strFullPath)");
            }
            /*END-NSTD*/

            //                                              //Actualiza a la nueva ubicación.
            sysdirToMove_M = Sys.sysdirNew(syspathMoved);
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subMoveTo(                       //Mueve el archivo a otro directorio, se mueve con el mismo
            //                                              //      nombre.

            //                                              //FileInfo del archivo que se quiere mover.
            FileInfo sysfileToMove_M,
            //                                              //DirectoryInfo del directorio al cual se desea mover el
            //                                              //      archivo, este será el nuevo padre.
            DirectoryInfo sysdirNewParent_M
            )
        {
            //                                              //Verifica que el archivo y el directorio existan.
            SyspathPath syspathToMove = Sys.syspathGet(sysfileToMove_M);
            if (
                //                                          //No es un archivo.
                !syspathToMove.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathToMove, "syspathToMove") + " directory do not exist");
            SyspathPath syspathNewParent = Sys.syspathGet(sysdirNewParent_M);
            if (
                //                                          //No es un directorio.
                !syspathNewParent.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathNewParent, "syspathNewParent") +
                    " directory do not exist");

            if (
                //                                          //Están en raices distintas.
                syspathToMove.syspathRoot.strFullPath != syspathNewParent.syspathRoot.strFullPath
                )
                Tools.subAbort(Test.strTo(syspathToMove, "syspathToMove") + ", " +
                    Test.strTo(syspathNewParent, "syspathNewParent") +
                    " can not be moved, they are not in the same root");

            //                                              //Forma el syspath del directorio ya movido.
            SyspathPath syspathMoved = syspathNewParent.syspathAddName(syspathToMove.strName);

            //                                              //Aborta si existe otro directorio o archivo con el mismo
            //                                              //      nombre en el el disco.
            if (
                //                                          //El nuevo syspath, ya existe.
                syspathMoved.boolExists
                )
                Tools.subAbort(Test.strTo(syspathMoved, "syspathMoved") +
                    " can no be move, disk already contains a directory or a file with the same name");

            //                                              //Mueve el archivo de directorio usando el MoveTo.
            //                                              //Nótese que ya se hicieron muchas verificaciones para hacer
            //                                              //      esto en forma segura.
            /*NSTD*/
            try
            {
                sysfileToMove_M.MoveTo(syspathMoved.strFullPath);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToMove, "syspathToMove") + ", " +
                    Test.strTo(syspathMoved, "syspathMoved") + ", " + Test.strTo(sysexcepError, "sysexcepError") +
                    " error in sysfileToMove_M.MoveTo(syspathMoved.strFullPath)");
            }
            /*END-NSTD*/

            //                                              //Actualiza a la nueva ubicación del archivo.
            sysfileToMove_M = Sys.sysfileNew(syspathMoved);
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subDelete(                       //Elimina un directorio del disco.

            //                                              //DirectoryInfo que se desea eliminar.
            DirectoryInfo sysdirToDelete_I,
            //                                              //Si es true, también borra su contenido, si es false, solo
            //                                              //      puede borrar si está vacío, en su defecto aborta.
            bool boolDeleteSubdirectoriesAndFiles_I
            )
        {
            //                                              //Crea el syspath del directorio, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathToDelete = Sys.syspathGet(sysdirToDelete_I);

            if (
                !syspathToDelete.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathToDelete, "syspathToDelete") + " do not exist as directory");

            //                                              //Hace el delete.
            /*NSTD*/
            try
            {
                sysdirToDelete_I.Delete(boolDeleteSubdirectoriesAndFiles_I);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToDelete, "syspathToDelete") + ", " +
                    Test.strTo(boolDeleteSubdirectoriesAndFiles_I, "boolDeleteSubdirectoriesAndFiles_I") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") +
                    " error in sysdirToDelete_I.Delete(boolDeleteSubdirectoriesAndFiles_I)");
            }
            /*END-NSTD*/
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subDelete(                       //Elimina un archivo del disco.

            //                                              //FileInfo que se desea eliminar.
            FileInfo sysfileToDelete_I
            )
        {
            //                                              //Crea el syspath del directorio, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathToDelete = Sys.syspathGet(sysfileToDelete_I);

            if (
                !syspathToDelete.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathToDelete, "syspathToDelete") + " do not exists as file");

            //                                              //Hace el delete.
            /*NSTD*/
            try
            {
                sysfileToDelete_I.Delete();
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathToDelete, "syspathToDelete") + ", " + 
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysfileToDelete_I.Delete()");
            }
            /*END-NSTD*/
        }

        //--------------------------------------------------------------------------------------------------------------
        public static DirectoryInfo[] arrsysdirGetDirectories(
            //                                              //Extrae el conjunto de subdirectorios de un directorio.

            //                                              //DirectoryInfo del cual se quiere información.
            DirectoryInfo sysdirToSearch_I
            )
        {
            //                                              //Crea el syspath del directorio, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathDirectoryToSearch = Sys.syspathGet(sysdirToSearch_I);

            if (
                !syspathDirectoryToSearch.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathDirectoryToSearch, "syspathDirectoryToSearch") + 
                    " no existe como directorio");

            //                                              //Extrae subdirectorios.
            DirectoryInfo[] arrsysdirGetDirectories;
            /*NSTD*/
            try
            {
                arrsysdirGetDirectories = sysdirToSearch_I.GetDirectories();
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathDirectoryToSearch, "syspathDirectoryToSearch") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysdirToSearch_I.GetDirectories()");

                arrsysdirGetDirectories = null;
            }
            /*END-NSTD*/

            //                                              //Regresa el conjunto de subdirectorios.
            return arrsysdirGetDirectories;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static FileInfo[] arrsysfileGetFiles(        //Extrae el conjunto de archivos de un directorio.

            //                                              //DirectoryInfo del cual se quiere información.
            DirectoryInfo sysdirToSearch_I
            )
        {
            //                                              //Crea el syspath del directorio, esto solo para confirmar
            //                                              //      que todo sigue bien y tener un mejor diagnóstico en
            //                                              //      caso de problemas.
            SyspathPath syspathDirectoryToSearch = Sys.syspathGet(sysdirToSearch_I);

            if (
                !syspathDirectoryToSearch.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(syspathDirectoryToSearch, "syspathDirectoryToSearch") +
                    " does not exists");

            //                                              //Extrae Archivos.
            FileInfo[] arrsysfileGetFiles;
            /*NSTD*/
            try
            {
                arrsysfileGetFiles = sysdirToSearch_I.GetFiles();
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathDirectoryToSearch, "syspathDirectoryToSearch") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysdirToSearch_I.GetFiles()");

                arrsysfileGetFiles = null;
            }
            /*END-NSTD*/

            //                                              //Regresa el conjunto de subdirectorios.
            return arrsysfileGetFiles;
        }
        /*END-TASK*/

        //==============================================================================================================
        /*TASK Sys.Text Text Files*/
        //--------------------------------------------------------------------------------------------------------------
        public static String[] arrstrReadAll(               //Carga la totalidad de un archivo de texto a memoria.

            //                                              //arrstr, archivo de texto en formato de arreglo de Strings.

            //                                              //FileInfo del archivo a cargar a memoría.
            FileInfo sysfileInputTextFile_M
            )
        {
            //                                              //Creo el stream reader (si no existe abortará).
            StreamReader syssr = Sys.syssrNewTextFile(sysfileInputTextFile_M);

            //                                              //Paso el archivo a memoria (un String).
            String strTextFile;
            /*NSTD*/
            try
            {
                //                                          //Lee TODO un archivo.
                strTextFile = syssr.ReadToEnd();
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(sysfileInputTextFile_M.Name, "sysfileInputTextFile_M.Name") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in syssr.ReadToEnd()");

                strTextFile = null;

                syssr.Dispose();
            }
            /*END-NSTD*/

            //                                              //Es necesaro cerrar el syssr
            syssr.Dispose();

            //                                              //Formo arreglo con lo leído.
            String[] arrstrLine;
            /*NSTD*/
            arrstrLine = strTextFile.Split(new String[] { Environment.NewLine }, StringSplitOptions.None);
            /*END-NSTD*/

            return arrstrLine;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subWriteAll(                     //Sube la totalidad de un arreglo en memoria a un archivo
            //                                              //      de texto (no debe existir el archivo).

            //                                              //arrstr, archivo de texto en formato de arreglo de Strings.
            String[] arrstrLine_I,
            //                                              //FileInfo del archivo a al cual se sube lo que se tiene en
            //                                              //      memoría.
            FileInfo sysfileOutputTextFile_M
            )
        {
            //                                              //Tomo el path para analizarlo y poder dar un mejor
            //                                              //      diagnostico.
            SyspathPath syspathFile = Sys.syspathGet(sysfileOutputTextFile_M);
            if (
                //                                          //Ya existe como DIRECTORY o FILE
                syspathFile.boolExists
                )
                Tools.subAbort(Test.strTo(syspathFile, "syspathFile") + " exists as DIRECTORY o FILE");

            //                                              //Creo el stream writer.
            StreamWriter syssw = Sys.sysswNewWriteTextFile(sysfileOutputTextFile_M);

            //                                              //Paso todo el arreglo a un solo String de líneas.
            String strTextFile = String.Join(Environment.NewLine, arrstrLine_I);

            //                                              //Escribo el String al archivo (un solo WriteLine).
            Sys.subWriteLine(strTextFile, syssw);

            //                                              //Es necesaro cerrar el syssw
            syssw.Dispose();
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subRewriteAll(                   //Sube la totalidad de un arreglo en memoria a un archivo
            //                                              //      de texto que ya existe (será reescritura).

            //                                              //arrstr, archivo de texto en formato de arreglo de Strings.
            String[] arrstrLine_I,
            //                                              //FileInfo del archivo a al cual se sube lo que se tiene en
            //                                              //      memoría.
            FileInfo sysfileOutputTextFile_M
            )
        {
            //                                              //Tomo el path para analizarlo y poder dar un mejor
            //                                              //      diagnostico.
            SyspathPath syspathFile = Sys.syspathGet(sysfileOutputTextFile_M);
            if (
                //                                          //NO existe como FILE
                !syspathFile.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathFile, "syspathFile") + " file do not exist");

            //                                              //Creo el stream writer.
            StreamWriter syssw = sysswNewRewriteTextFile(sysfileOutputTextFile_M);

            //                                              //Paso todo el arreglo a un solo String de líneas.
            String strTextFile = String.Join(Environment.NewLine, arrstrLine_I);

            //                                              //Escribo el String al archivo (un solo WriteLine).
            Sys.subWriteLine(strTextFile, syssw);

            syssw.Dispose();
        }

        //--------------------------------------------------------------------------------------------------------------
        public static StreamReader syssrNewTextFile(        //Genera el StreamReader para un archivo de texto, si no
            //                                              //      existe abortará.

            //                                              //syssr, StreamReader listo.

            //                                              //FileInfo del archivo.
            FileInfo sysfileInputTextFile_M
            )
        {
            //                                              //Confirma la existencia el archivo.
            SyspathPath syspathInput = Sys.syspathGet(sysfileInputTextFile_M);
            if (
                //                                          //No existe como archivo.
                !syspathInput.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathInput, "syspathInput") + " file does not exist");

            //                                              //Creo el acceso al archivo.
            StreamReader syssrTextFile;
            /*NSTD*/
            try
            {
                //                                          //Creo el stream reader.
                syssrTextFile = new StreamReader(sysfileInputTextFile_M.FullName, Encoding.Default);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathInput, "syspathInput") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysfileInput_M.OpenText()");

                syssrTextFile = null;
            }
            /*END-NSTD*/

            return syssrTextFile;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static StreamWriter sysswNewWriteTextFile(   //Genera el StreamWriter para un archivo de texto para 
            //                                              //      escritura, no debe existir el archivo.

            //                                              //syssw, StreamWriter listo.

            //                                              //FileInfo del archivo.
            FileInfo sysfileOutputTextFile_M
            )
        {
            //                                              //Confirma la no existencia de algo en el path.
            SyspathPath syspathFile = Sys.syspathGet(sysfileOutputTextFile_M);
            if (
                //                                          //Ya existe algo en el path, puede ser DIRECTORY o FILE.
                syspathFile.boolExists
                )
                Tools.subAbort(Test.strTo(syspathFile, "syspathFile") + " exists as a DIRECTORY or FILE");

            //                                              //Creo el acceso al archivo.
            StreamWriter sysswWriteTextFile;
            /*NSTD*/
            try
            {
                //                                          //Creo el stream reader.
                sysswWriteTextFile = sysfileOutputTextFile_M.CreateText();
                //sysswWriteTextFile = new StreamWriter(sysfileOutputTextFile_M.FullName, false, Encoding.Default);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathFile, "syspathFile") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysfileOutput_M.CreateText()");

                sysswWriteTextFile = null;
            }
            /*END-NSTD*/

            return sysswWriteTextFile;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static StreamWriter sysswNewRewriteTextFile( //Genera el StreamWriter para un archivo de texto para 
            //                                              //      reescritura.

            //                                              //syssw, StreamWriter listo.

            //                                              //FileInfo del archivo.
            FileInfo sysfileOutputTextFile_M
            )
        {
            //                                              //Confirma la existencia de algo en el path y que se le
            //                                              //      pueda reescribir
            SyspathPath syspathFile = Sys.syspathGet(sysfileOutputTextFile_M);
            if (
                //                                          //NO existe el FILE.
                !syspathFile.boolIsFile
                )
                Tools.subAbort(Test.strTo(syspathFile, "syspathFile") +
                    " file do not exist, to write use sysswWriteTextFile");
            if (
                sysfileOutputTextFile_M.IsReadOnly
                )
                Tools.subAbort(Test.strTo(syspathFile, "syspathFile") +
                    ", " + Test.strTo(sysfileOutputTextFile_M.IsReadOnly, "sysfileOutputTextFile_M.IsReadOnly") +
                    " is ReadOnly");

            //                                              //Creo el acceso al archivo.
            StreamWriter sysswRewriteTextFile;
            /*NSTD*/
            try
            {
                //                                          //Creo el stream reader.
                sysswRewriteTextFile = sysfileOutputTextFile_M.CreateText();
                //sysswRewriteTextFile = new StreamWriter(sysfileOutputTextFile_M.FullName, false, Encoding.Default);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(syspathFile, "syspathFile") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysfileOutput_M.CreateText()");

                sysswRewriteTextFile = null;
            }
            /*END-NSTD*/

            return sysswRewriteTextFile;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static String strReadLine(                   //Leer una línea de texto.

            //                                              //str, Línea leída.

            //                                              //StreamReader del archivo.
            StreamReader syssrInputTextFile_M
            )
        {
            //                                              //Leo una línea del archivo.
            String strReadLine;
            /*NSTD*/
            try
            {
                //                                          //Leo una línea.
                strReadLine = syssrInputTextFile_M.ReadLine();
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(sysexcepError, "sysexcepError") + " error in syssrTextFile_M.ReadLine()");

                strReadLine = null;

                syssrInputTextFile_M.Dispose();
            }
            /*END-NSTD*/

            return strReadLine;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subWriteLine(                    //Escribe una línea de texto.

            //                                              //Linea que se va a escribir.
            String strLine_I,
            //                                              //StreamWriter del archivo.
            StreamWriter sysswOutputTextFile_M
            )
        {
            //                                              //Escribe una línea en el archivo.
            /*NSTD*/
            try
            {
                //                                          //Escribo una línea.
                sysswOutputTextFile_M.WriteLine(strLine_I);
            }
            catch (Exception sysexcepError)
            {
                Tools.subAbort(Test.strTo(strLine_I, "strLine_I") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " error in sysswTextFile_M.WriteLine(strLine_I)");

                sysswOutputTextFile_M.Dispose();
            }
            /*END-NSTD*/
        }
        /*END-TASK*/

        //--------------------------------------------------------------------------------------------------------------
    }
}
/*END-TASK*/
