/*TASK SyspathPath*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

//                                                          //AUTHOR: Towa (GLG-Gerardo López).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: 19-Febrero-2014.
//                                                          //PURPOSE:
//                                                          //Clase para manipular paths.

namespace TowaInfrastructure
{
    //==================================================================================================================
    public /*nonpartial*/ /*MUTABLE*/ class SyspathPath : BclassBaseClassAbstract
    //                                                      //Clase para manipular path.
    //                                                      //Debe funcionar correctamente para archivos y directorios
    //                                                      //      locales y en la red.
    {
        //--------------------------------------------------------------------------------------------------------------
        /*CONSTANTS*/
        
        private const BclassmutabilityEnum bclassmutability_Z = BclassmutabilityEnum.MUTABLE;
        public override BclassmutabilityEnum bclassmutability { get { return SyspathPath.bclassmutability_Z; } }

        //                                                  //En teoria el conjunto de caracteres válidos en un path es
        //                                                  //      muy extenso, sin embargo, en la realidad, cuando se
        //                                                  //      desea mover archivos y directorios entre diferentes
        //                                                  //      sistemas operativos (windows, unix, os de mac, etc.)
        //                                                  //      suelen suceder problemas.
        //                                                  //Por estándar Towa optamos por permitir un conjunto MUY
        //                                                  //      CONSERVADOR DE CARACTERES.
        //                                                  //CONFORME ENTENDAMOS MEJOR ESTA PROBLEMATICA, ESTA LISTA
        //                                                  //      DE CARACTERES SERÁ AMPLIADA O RECORTADA.
        private static readonly String strCHAR_USEFUL =
            //                                              //Dígitos y letras sin acentos.
            "0123456789" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "abcdefghijklmnopqrstuvwxyz" +
            //                                              //Espacio.
            " " +
            //                                              //Algunos caracteres especiales.
            ",._()[]{}$@+-" +
            //                                              //Caracteres necesarios en un path. 
            //                                              //No incluí el Path.PathSeparator(;), no entiendo para que
            //                                              //      se usa.
            Path.VolumeSeparatorChar + Path.DirectorySeparatorChar;
        //                                                  //Información anterior en un arreglo ordenado.
        private static readonly char[] arrcharUSEFUL;

        //                                                  //Adicionalmente, algunos problemas se pueden corregir
        //                                                  //      fácilmente cambiando algunos caracteres.
        //                                                  //EN EL FUTURO SE PODRÁN AÑADIR OTROS PARES DE CARACTERES.
        private const String strCHAR_TO_CONVERT_AND_CONVERSION =
            //                                              //Caracteres acentuados, se eliminan los acentos.
            "ÁAÉEÍIÓOÚUÀAÈEÌIÒOÙUÄAËEÏIÖOÜUÂAÊEÎIÔOÛU" + "áaéeíióoúuàaèeìiòoùuäaëeïiöoüuâaêeîiôoûu" + "ÑNñn" +
            //                                              //También caracteres aceptados en mac os pero no en windows.
             "?_&_*_<_>_|_#_%_";

        //                                                  //Información anterior separada y 2 arreglos y ordenada por
        //                                                  //      el primero.
        private static readonly char[] arrcharTO_CONVERT;
        private static readonly char[] arrcharCONVERSION;

        //                                                  //Inicio de un full path en red.
        private static readonly String strINICIO_FULL_PATH_RED =
            "" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar;

        static SyspathPath(                                 //Prepara las constantes.
            //                                              //1. Prepara y ordena strCHAR_USEFUL en arrcharUSEFUL.
            //                                              //2. prepara y ordena strCHAR_TO_CONVERT_AND_CONVERSION en
            //                                              //      arrcharTO_CONVERT y arrcharCONVERSION.
            )
        {
            //                                              //1. Ordena arrcharUSEFUL.
            arrcharUSEFUL = strCHAR_USEFUL.ToCharArray();
            Array.Sort(arrcharUSEFUL);

            //                                              //Verifica que no haya caracteres duplicados.
            for (int intI = 1; intI < arrcharUSEFUL.Length; intI = intI + 1)
            {
                if (
                    //                                      //Esta duplicado.
                    arrcharUSEFUL[intI] == arrcharUSEFUL[intI - 1]
                    )
                    Tools.subAbort(Test.strTo(arrcharUSEFUL, "arrcharUSEFUL") + ", " +
                        Test.strTo(arrcharUSEFUL[intI], "arrcharUSEFUL[" + intI + "]") +
                        " duplicated character");
            }

            //                                              //2. Separa y ordena strCHAR_TO_CONVERT_AND_CONVERSION.
            //                                              //Convierte a 2 arreglos de caracteres y los ordena por el
            //                                              //      primero.
            arrcharTO_CONVERT = new char[strCHAR_TO_CONVERT_AND_CONVERSION.Length / 2];
            arrcharCONVERSION = new char[arrcharTO_CONVERT.Length];
            for (int intI = 0; intI < arrcharTO_CONVERT.Length; intI = intI + 1)
            {
                arrcharTO_CONVERT[intI] = strCHAR_TO_CONVERT_AND_CONVERSION[intI * 2];
                arrcharCONVERSION[intI] = strCHAR_TO_CONVERT_AND_CONVERSION[intI * 2 + 1];
            }
            Array.Sort(arrcharTO_CONVERT, arrcharCONVERSION);

            //                                              //Verifica que no haya caracteres duplicados.
            for (int intI = 1; intI < arrcharTO_CONVERT.Length; intI = intI + 1)
            {
                if (
                    //                                      //Esta duplicado.
                    arrcharTO_CONVERT[intI] == arrcharTO_CONVERT[intI - 1]
                    )
                    Tools.subAbort(Test.strTo(arrcharTO_CONVERT, "arrcharTO_CONVERT") + ", " +
                        Test.strTo(arrcharTO_CONVERT[intI], "arrcharTO_CONVERT[" + intI + "]") +
                        " duplicated character");
            }

            //                                              //Verifica que arrcharTO_CONVERT NO este en
            //                                              //      arrcharUSEFUL.
            for (int intI = 0; intI < arrcharTO_CONVERT.Length; intI = intI + 1)
            {
                if (
                    Array.BinarySearch(arrcharUSEFUL, arrcharTO_CONVERT[intI]) >= 0
                    )
                    Tools.subAbort(Test.strTo(arrcharTO_CONVERT[intI], "arrcharTO_CONVERT[" + intI + "]") +
                        " exists in " + Test.strTo(arrcharUSEFUL, "arrcharUSEFUL"));
            }

            //                                              //Verifica que arrcharCONVERSION SI este en arrcharUSEFUL.
            for (int intI = 0; intI < arrcharCONVERSION.Length; intI = intI + 1)
            {
                if (
                    Array.BinarySearch(arrcharUSEFUL, arrcharCONVERSION[intI]) < 0
                    )
                    Tools.subAbort(Test.strTo(arrcharCONVERSION[intI], "arrcharCONVERSION[" + intI + "]") +
                        " do not exist in " + Test.strTo(arrcharUSEFUL, "arrcharUSEFUL"));
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLES*/

        //                                                  //Si es válido y se tiene acceso, contiene el Full Path.
        //                                                  //En su defecto, contiene el Path tal se proporciono y no
        //                                                  //      se deben tomar sus propiedades (aborta).
        private /*MUTABLE*/ String strFullPath_Z;
        public String strFullPath { get { return this.strFullPath_Z; } }

        //                                                  //Determina si un Nombre, Path o Full Path (File or
        //                                                  //      Directory) es válido en su estructura de caracteres.
        //                                                  //Se utiliza Path.GetFullPath(strFileName_I) para
        //                                                  //      verificarlo.
        //                                                  //Si recibe un SecurityException no lo rechaza.
        private /*MUTABLE*/ bool boolIsValid_Z;
        public bool boolIsValid { get { return this.boolIsValid_Z; } }

        //                                                  //Determina si se tiene acceso a un Nombre, Path o Full Path
        //                                                  //      (File or Directory).
        //                                                  //Se utiliza Path.GetFullPath(strFileName_I) para
        //                                                  //      verificarlo.
        //                                                  //Si recibe un SecurityException indica que no tiene acceso.
        //                                                  //El path debe ser válido.
        private /*MUTABLE*/ bool boolHaveAccessTo_Z;
        public bool boolHaveAccessTo { get { return this.boolHaveAccessTo_Z; } } 

        //                                                  //LOCAL o NETWORK.
        private SyspathwhereEnum syspathwhere_Z;
        public SyspathwhereEnum syspathwhere { get { return this.syspathwhere_Z; } }

        //                                                  //NONE, FILE o DIRECTORY.
        private SyspathtypeEnum syspathtype_Z;
        public SyspathtypeEnum syspathtype { get { return this.syspathtype_Z; } }

        //--------------------------------------------------------------------------------------------------------------
        /*COMPUTED VARIABLES*/

        //                                                  //Es un Full Path local (esta en este equipo).
        private bool? boolIsLocal_Z; 
        public bool boolIsLocal
        {
            get
            {
                if (
                    this.boolIsLocal_Z == null
                    )
                {
                    this.boolIsLocal_Z =
                        (
                        this.syspathwhere == SyspathwhereEnum.LOCAL
                        );
                }

                return (bool)this.boolIsLocal_Z;
            }
        }

        //                                                  //Es un Full Path que esta en red.
        private bool? boolIsNetwork_Z;
        public bool boolIsNetwork
        {
            get
            {
                if (
                    this.boolIsNetwork_Z == null
                    )
                {
                    this.boolIsNetwork_Z =
                        (
                        this.syspathwhere == SyspathwhereEnum.NETWORK
                        );
                }

                return (bool)this.boolIsNetwork_Z;
            }
        }

        //                                                  //Corresponde a un File o Directory que existe.
        private bool? boolExists_Z;
        public bool boolExists
        {
            get
            {
                if (
                    this.boolExists_Z == null
                    )
                {
                    this.boolExists_Z =
                        (
                        //                                 //Si existe, es un File o Directory
                        this.syspathtype != SyspathtypeEnum.NONE
                        );
                }

                return (bool)this.boolExists_Z;
            }
        }

        //                                                  //Es un File.
        private bool? boolIsFile_Z;
        public bool boolIsFile
        {
            get
            {
                if (
                    this.boolIsFile_Z == null
                    )
                {
                    this.boolIsFile_Z =
                        (
                        this.syspathtype == SyspathtypeEnum.FILE
                        );
                }

                return (bool)this.boolIsFile_Z;
            }
        }

        //                                                  //Es un Directory.
        private bool? boolIsDirectory_Z;
        public bool boolIsDirectory
        {
            get
            {
                if (
                    this.boolIsDirectory_Z == null
                    )
                {
                    this.boolIsDirectory_Z =
                        (
                        this.syspathtype == SyspathtypeEnum.DIRECTORY
                        );
                }

                return (bool)this.boolIsDirectory_Z;
            }
        }

        //                                                  //SOLO SI EXISTE, PUEDE SER FILE O DIRECTORY.
        //                                                  //Si se solicitan cuando no existe va a abortar.

        //                                                  //Nombre del archivo (sin el directorio).
        private String strName_Z;
        public String strName
        {
            get
            {
                if (
                    this.strName_Z == null
                    )
                {
                    if (
                        //                                  //No existe.
                        !this.boolExists
                        )
                        Tools.subAbort(Test.strTo(this, "this") + " does not exist");

                    this.strName_Z = Path.GetFileName(this.strFullPath);
                }

                return this.strName_Z;
            }
        }

        //                                                  //syspath de la raíz.
        private SyspathPath syspathRoot_Z;
        public SyspathPath syspathRoot
        {
            get
            {
                if (
                    this.syspathRoot_Z == null
                    )
                {
                    if (
                        //                                  //No existe.
                        !this.boolExists
                        )
                        Tools.subAbort(Test.strTo(this, "this") + " does not exist");

                    //                                      //Extraigo raíz.
                    String strRoot = Path.GetPathRoot(this.strFullPath);

                    this.syspathRoot_Z = new SyspathPath(strRoot);
                }

                return this.syspathRoot_Z;
            }
        }

        //                                                  //SOLO SI ES UN FILE.
        //                                                  //Si se solicitan cuando no existe o es un directorio va a 
        //                                                  //      abortar.

        //                                                  //File extension.
        private String strFileExtension_Z;
        public String strFileExtension
        {
            get
            {
                if (
                    this.strFileExtension_Z == null
                    )
                {
                    if (
                        //                                  //No es file.
                        !this.boolIsFile
                        )
                        Tools.subAbort(Test.strTo(this, "this") + " is not a file");

                    this.strFileExtension_Z = Path.GetExtension(this.strFullPath);
                }

                return this.strFileExtension_Z;
            }
        }

        //                                                  //Nombre sin el file extensión.
        private String strFileNameWithoutExtension_Z;
        public String strFileNameWithoutExtension
        {
            get
            {
                if (
                    this.strFileNameWithoutExtension_Z == null
                    )
                {
                    if (
                        //                                  //No es file.
                        !this.boolIsFile
                        )
                        Tools.subAbort(Test.strTo(this, "this") + " is not a file");

                    this.strFileNameWithoutExtension_Z =
                        Path.GetFileNameWithoutExtension(this.strFullPath);
                }

                return this.strFileNameWithoutExtension_Z;
            }
        }

        //                                                  //Path del directorio del archivo que tenemos.
        private SyspathPath syspathDirectory_Z;
        public SyspathPath syspathDirectory
        {
            get
            {
                if (
                    this.syspathDirectory_Z == null
                    )
                {
                    if (
                        //                                  //No es file.
                        !this.boolIsFile
                        )
                        Tools.subAbort(Test.strTo(this, "this") + " is not a file");

                    //                                      //Extraigo directorio.
                    String strDirectory = Path.GetDirectoryName(this.strFullPath);

                    this.syspathDirectory_Z = new SyspathPath(strDirectory);
                }

                return this.syspathDirectory_Z;
            }
        }

        //                                                  //SOLO SI ES UN DIRECTORY.
        //                                                  //Si se solicitan cuando no existe o es un file va a 
        //                                                  //      abortar.

        //                                                  //syspath del directorio padre.
        private SyspathPath syspathParent_Z;
        public SyspathPath syspathParent
        {
            get
            {
                if (
                    this.syspathParent_Z == null
                    )
                {
                    if (
                        //                                  //No es directorio.
                        !this.boolIsDirectory
                        )
                        Tools.subAbort(Test.strTo(this, "this") + " is not a directory");

                    if (
                        //                                  //El directorio que ya tenemos es la raíz, no puede buscar
                        //                                  //      el padre de esto.
                        this.strFullPath_Z == this.syspathRoot.strFullPath
                        )
                        Tools.subAbort(Test.strTo(this, "this") +
                            " it is a root directory, it do not have a parent");

                    //                                      //Extraigo directorio.
                    String strDirectory = Path.GetDirectoryName(this.strFullPath_Z);

                    this.syspathParent_Z = new SyspathPath(strDirectory);
                }

                return this.syspathParent_Z;
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        public override void subReset()
        {
            this.boolIsLocal_Z = null;
            this.boolIsNetwork_Z = null;
            this.boolExists_Z = null;
            this.boolIsFile_Z = null;
            this.boolIsDirectory_Z = null;

            this.strName_Z = null;

            this.strFileExtension_Z = null;
            this.strFileNameWithoutExtension_Z = null;
            this.syspathDirectory_Z = null;

            this.syspathParent_Z = null;
            this.syspathRoot_Z = null;
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo(TestoptionEnum testoptionSHORT_I)
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "[" + base.strTo(TestoptionEnum.SHORT) + ", " +
                Test.strTo(this.strName/*strFullPath*/, TestoptionEnum.SHORT) + ", " +
                Test.strTo(this.boolIsValid, TestoptionEnum.SHORT) + ", " +
                Test.strTo(this.boolHaveAccessTo, TestoptionEnum.SHORT) + ", " + 
                Test.strTo(this.syspathwhere, TestoptionEnum.SHORT) + ", " +
                Test.strTo(this.syspathtype, TestoptionEnum.SHORT) + "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public override String strTo()
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "{" + Test.strTo(this.strFullPath, "strFullPath") + ", " +
                Test.strTo(this.boolIsValid, "boolIsValid") + ", " +
                Test.strTo(this.boolHaveAccessTo, "boolHaveAccessTo") + ", " +
                Test.strTo(this.syspathwhere, "syspathwhere") + ", " +
                Test.strTo(this.syspathtype, "syspathtype") + "}" + "==>" + base.strTo();
        }

        //--------------------------------------------------------------------------------------------------------------
        /*OBJECT CONSTRUCTORS*/

        //--------------------------------------------------------------------------------------------------------------
        public SyspathPath(                                 //Crea un objeto Path.
            //                                              //this.*[O], asigna valores. 

            //                                              //Nombre, Path relativo o Full Path.
            String strPath_I
            )
            : base()
        {
            if (
                strPath_I == null
                )
                Tools.subAbort(Test.strTo(strPath_I, "strPath_I") + " should have a value");

            //                                              //Por lo pronto asigna los valores que tendrá si no es
            //                                              //      válido o no se tiene acceso.
            this.strFullPath_Z = strPath_I;
            this.syspathwhere_Z = SyspathwhereEnum.Z_ERROR_NOT_DEFINED;
            this.syspathtype_Z = SyspathtypeEnum.Z_ERROR_NOT_DEFINED;

            this.subRefresh();
        }

        //--------------------------------------------------------------------------------------------------------------
        //                                                  //MÉTODOS DE CONSULTA Y/O TRANSFORMACIÓN.

        //--------------------------------------------------------------------------------------------------------------
        public void subRefresh(                             //Determina el Path o Full Path (File or Directory) es
            //                                              //      válido en su estructura de caracteres y si se tiene
            //                                              //      acceso.
            //                                              //Se utiliza Path.GetFullPath().
            //                                              //this[M], toma información y también refresca todo.
            )
        {
            //                                              //Asume que todo será correcto.
            this.boolIsValid_Z = true;
            this.boolHaveAccessTo_Z = true;

            /*NONSTANDAR*/
            try
            {
                //                                          //Trata de obtener el full path.
                this.strFullPath_Z = Path.GetFullPath(this.strFullPath_Z);
            }
            catch (System.Security.SecurityException sysexcepError)
            {
                //                                          //El nombre esta bien formado, pero no se tiene acceso.
                this.boolIsValid_Z = true;
                this.boolHaveAccessTo_Z = false;

                //                                          //ESTE MENSAJE SE DEBE ELIMINAR EN EL FUTURO.
                MessageBox.Show(Test.strTo(this.strFullPath, "strFullPath") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " do not have access permition");
            }
            catch (Exception sysexcepError)
            {
                //                                          //El nombre no esta bien formado.
                this.boolIsValid_Z = false;
                this.boolHaveAccessTo_Z = false;

                //                                          //ESTE MENSAJE SE DEBE ELIMINAR EN EL FUTURO.
                MessageBox.Show(Test.strTo(this.strFullPath, "strFullPath") + ", " +
                    Test.strTo(sysexcepError, "sysexcepError") + " is not a valid path");
            }
            /*END-NONSTANDAR*/

            //                                              //Por estándar solo se aceptan como validos los caracteres
            //                                              //      USEFUL y TO_CONVERT
            int intI = 0;
            /*UNTIL-DO*/
            while (!(
                (intI >= this.strFullPath.Length) ||
                (Array.BinarySearch(SyspathPath.arrcharUSEFUL, this.strFullPath[intI]) < 0) &&
                    (Array.BinarySearch(SyspathPath.arrcharTO_CONVERT, this.strFullPath[intI]) < 0)
                ))
            {
                intI = intI + 1;
            }

            //                                              //Si salió antes de terminar es que encontró char invalid
            if (
                intI < this.strFullPath.Length
                )
            {
                //                                          //Se marca.
                this.boolIsValid_Z = false;
                this.boolHaveAccessTo_Z = false;
            }

            //                                              //Adicionalmente, si inicia con ftp:, http:, https: o file:
            //                                              //      será inválido (y sin acceso).
            //                                              //Aparentemente el Path los acepta pero FileInfo y
            //                                              //      DirectoryInfo NO.

            //                                              //Corta y pasa a minúsculas.
            String strInicioFullPath;
            const int intCORTA = 10;
            if (
                this.strFullPath.Length <= intCORTA
                )
            {
                strInicioFullPath = this.strFullPath_Z;
            }
            else
            {
                strInicioFullPath  = this.strFullPath.Substring(0, 10);
            }
            strInicioFullPath = strInicioFullPath.ToLower();

            if (
                strInicioFullPath.StartsWith("ftp:") || strInicioFullPath.StartsWith("http:") ||
                 strInicioFullPath.StartsWith("https:") || strInicioFullPath.StartsWith("file:")
                )
            {
                //                                          //Se marca.
                this.boolIsValid_Z = false;
                this.boolHaveAccessTo_Z = false;
            }

            //                                              //Si todo sigue correcto es que no se fue por catch y que 
            //                                              //      ya tiene un Full Path.

            if (
                boolIsValid_Z && boolHaveAccessTo_Z
                )
            {
                //                                          //Determina el where

                if (
                    //                                      //Es local (tiene x:...).
                    this.strFullPath[1] == Path.VolumeSeparatorChar
                    )
                {
                    this.syspathwhere_Z = SyspathwhereEnum.LOCAL;
                }
                else if (
                    //                                      //Es red (tiene \\...).
                    this.strFullPath.StartsWith(strINICIO_FULL_PATH_RED)
                    )
                {
                    this.syspathwhere_Z = SyspathwhereEnum.NETWORK;
                }
                else
                {
                    if (
                        true
                        )
                        Tools.subAbort(Test.strTo(this, "this") + " path start not valid");
                }
                /*END-CASE*/

                //                                          //Determina tipo.

                //                                          //Será de este tipo a menos que se reconozca como archivo o
                //                                          //      directorio.
                this.syspathtype_Z = SyspathtypeEnum.NONE;

                if (
                    //                                      //File.Exists() lo reconoce como archivo.
                    File.Exists(this.strFullPath_Z)
                    )
                {
                    this.syspathtype_Z = SyspathtypeEnum.FILE;
                }

                if (
                    //                                      //Directory.Exists() lo reconoce como directorio.
                    Directory.Exists(this.strFullPath_Z)
                    )
                {
                    if (
                        this.syspathtype == SyspathtypeEnum.FILE
                        )
                        Tools.subAbort(Test.strTo(this, "this") +
                            " it can not be a file ana a directory at the same time");

                    this.syspathtype_Z = SyspathtypeEnum.DIRECTORY;
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        public bool boolIsCharSetTowa(                      //Verifica si esta de acuerdo con el conjunto de caracteres
            //                                              //      estándar Towa.
            //                                              //this[I], toma información.
            )
        {
            int intI = 0;
            /*UNTIL-DO*/
            while (!(
                (intI >= this.strFullPath.Length) ||
                (Array.BinarySearch(arrcharUSEFUL, this.strFullPath[intI]) < 0)
                ))
            {
                intI = intI + 1;
            }

            //                                              //Si llego al final es que todo esta correcto
            bool boolIsCharSetTowa = (
                intI >= this.strFullPath.Length
                );

            //                                              //TEMPORALMENTE, SI ES INVALIDO DESPLIEGA LA INFORMACIÓN,
            //                                              //      ESTO TIENE COMO FIN AYUDARNOS A ENTENDER LA
            //                                              //      PROBLEMÁTICA.
            if (
                !boolIsCharSetTowa
                )
            {
                MessageBox.Show(Test.strTo(this, TestoptionEnum.SHORT) + ", " + 
                    Test.strTo(this.strFullPath[intI], "strFullPath[" + intI + "]") +
                     " is invalid, THIS IS A TEMPORARY MESSAGE");
            }

            return boolIsCharSetTowa;        
        }

        //--------------------------------------------------------------------------------------------------------------
        public SyspathPath syspathConvertChars(             //Convierte los caracteres que tienen conversión.
            //                                              //this[I], toma información.

            //                                              //syspath, ya convertido.
            )
        {
            char[] arrcharFullPath = this.strFullPath.ToCharArray();

            for (int intI = 0; intI < arrcharFullPath.Length; intI = intI + 1)
            {
                //                                          //Busca si esta en A CONVERTIR.
                int intChar = Array.BinarySearch(arrcharTO_CONVERT, arrcharFullPath[intI]);

                if (
                    //                                      //Si lo encontró
                    intChar >= 0
                    )
                {
                    //                                      //Lo cambia.
                    arrcharFullPath[intI] = arrcharCONVERSION[intChar];
                }
            }

            //                                              //Convierte a String.
            String strFullPath = new String(arrcharFullPath);

            //                                              //Regresa un nuevo Path.
            return new SyspathPath(strFullPath);
        }

        //--------------------------------------------------------------------------------------------------------------
        public SyspathPath syspathAddName(                  //Le añade un nombre a un  syspath de un directorio.
            //                                              //this[I], debe ser un directorio, toma información.

            //                                              //syspath, ya combinado.

            //                                              //Nombre a combinar con el path del this.
            String strNameToCombine_I
            )
        {
            if (
                !this.boolIsDirectory
                )
                Tools.subAbort(Test.strTo(this, "this") + " do not exist as directory");

            String strFullPathMasName;
            /*NONSTANDAR*/
            strFullPathMasName = Path.Combine(this.strFullPath, strNameToCombine_I);
            /*END-NONSTANDAR*/

            //                                              //Regresa la combinación.
            return new SyspathPath(strFullPathMasName);
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================

}
/*END-TASK*/