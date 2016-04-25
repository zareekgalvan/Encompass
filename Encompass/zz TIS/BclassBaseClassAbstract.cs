/*TASK BclassBaseClassAbstract*/
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

//                                                          //AUTHOR: Towa (GLG-Gerardo López).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: June 24, 2015.
//                                                          //PURPOSE:
//                                                          //Base for all classes.

namespace TowaInfrastructure
{
    //==================================================================================================================
    public abstract /*nonpartial*/ class BclassBaseClassAbstract
    //                                                      //Clase base para todos los objetos, conforme al estandar
    //                                                      //      Towa, TODOS los objetos que diseñemos deben heredar
    //                                                      //      de esta clase.
    //                                                      //Entre otras cosas, esta clase provee facilidades para
    //                                                      //      evaluar el desempeño de una aplicación desarrollada
    //                                                      //      conforme a estos estándares.
    //                                                      //(algo ya esta aquí, sin embargo en el futuro se puede
    //                                                      //      añadir mas capacidades, ojo se debe SER CAUTELOSO
    //                                                      //      dado que todo esto afectará la eficiencia).
    {
        //--------------------------------------------------------------------------------------------------------------
        /*CONSTANTS*/

        //                                                  //Define si el objeto es INMUTABLE, MUTABLE o OPEN.
        //                                                  //Solo los MUTABLE recolectar información en UsedIn.
        //                                                  //Nótese que un objeto es MUTABLE si al menos UNA de sus
        //                                                  //      varibles es MUTABLE, esta variable puede estár en
        //                                                  //      la clase concreta o en cualquiera de las clases
        //                                                  //      abstractas que le dan forma (de esto se excluye
        //                                                  //      Bclass que es un caso especial).
        //                                                      
        public abstract BclassmutabilityEnum bclassmutability { get; }

        /*STATIC VARIABLES*/

        //                                                  //Diccionario para registrar la cuenta de todos los objetos
        //                                                  //      que contruye la aplicación al estar operando.
        //                                                  //Llave: Type (será el Fulname de la clase concreta) del
        //                                                  //      objeto.
        //                                                  //Info: Cantidad de objetos que se han creado durante la 
        //                                                  //      operación de la aplicación.
        private static Dictionary<String, int> dicintObjectCount;

        //                                                  //Total de UsedIn en TODOS los objetos de la aplicación.
        private static int intUsedInAddTotalCount;
        private static int intUsedInRemoveTotalCount;

        static BclassBaseClassAbstract(                      //Inicializa información estática.
            )
        {
            //                                              //Inicializa el diccionario para la cuenta de objetos que
            //                                              //      construye la aplicación.
            BclassBaseClassAbstract.dicintObjectCount = new Dictionary<string,int>();

            BclassBaseClassAbstract.intUsedInAddTotalCount = 0;
            BclassBaseClassAbstract.intUsedInRemoveTotalCount = 0;
        }

        //--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLES*/

        //                                                  //Registra objetos (concretos) que "usan" la información de
        //                                                  //      "este" objeto.
        //                                                  //Esto es necesario, dado que si este objeto es modificado,
        //                                                  //      por lo cual requiere ser reseteado, el reseteo debe
        //                                                  //      propagarse a todos los objetos que "usan" "este
        //                                                  //      objeto.
        //                                                  //Ejemplo, un objeto Journal Entry esta en USD y hace
        //                                                  //      referencia a un objeto Currency para tomar de ahí
        //                                                  //      los tipos de cambio, este objeto Journal Entry debe
        //                                                  //      debe añadirse a la lista de "used in" del objeto
        //                                                  //      currency para que al cambiar algo en currency le 
        //                                                  //      pueda avisar a Journal Entry que cambio.
        //                                                  //Nótese que el añadir una referencia de "uso" NO SIGNIFICA
        //                                                  //      que este objeto fue modificado (no se resetea).
        //                                                  //Para evitar que esta información sea usada indebidamente
        //                                                  //      se declara "private".
        //                                                  //Solo los objetos MUTABLE recolectan esta información, en
        //                                                  //      objetos, este valor debe ser null.
        private /*MUTABLE*/ List<BclassBaseClassAbstract> lstbclassThisIsUsedIn;

        //--------------------------------------------------------------------------------------------------------------
        /*COMPUTED VARIABLES*/

        //--------------------------------------------------------------------------------------------------------------
        public virtual void subReset() 
        {
            //                                              //No hay nada propio que resetear.

            if (
                //                                          //null significa que ese tipo de objeto no tiene UsedIn dato
                //                                          //      que no es MUTABLE, nótese que el objeto puede tener
                //                                          //      relaciones UsedIn, sin embargo, dado que no es
                //                                          //      MUTABLE no nos interesa registrarlas.
                this.lstbclassThisIsUsedIn == null
                )
            {
                //                                          //NO HACE NADA.
            }
            else
            {
                //                                          //Propaga el reseteo a los objetos que usan este objeto.
                foreach (BclassBaseClassAbstract bclassThisObjectIsUsedIn in this.lstbclassThisIsUsedIn)
                {
                    bclassThisObjectIsUsedIn.subReset();
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        public virtual String strTo(                        //SHORT display.
            //                                              //THIS METHOD SHOULD BE IMPLEMENTED IN EVERY CLASS (ABSTRACT
            //                                              //      OR CONCRETE).
            //                                              //The final format of the string will be:
            //                                              //ObjId[BclassVariables, AbstractVariables, ...,
            //                                              //      AbstractVariables, ConcreteVariables].
            //                                              //To produce this string:
            //                                              //1. Concrete class produces:
            //                                              //ObjId[base.testoption(S) + Variable + ... + Variable].
            //                                              //2. All abstract classes (except Bclass) produce:
            //                                              //base.testoption(S) + Variable + ... + Variable.
            //                                              //3. Bclass produces:
            //                                              //Variable + ... + Variable, (see below).
            //                                              //4a. Variable is Test.strTo(Variable, "Variable").
            //                                              //4b. When variable is lstobj, ..., you need to call strTo
            //                                              //      with 3 parameters, this method is an example (see
            //                                              //      support methods below).
            //                                              //4c. When variable is dirobj, ..., you need to call strTo
            //                                              //      with 4 parameters (see example in class 
            //                                              //      SemsolooObjectOriented).
            //                                              //4d. When variable is vkpobj, ..., you need to call strTo
            //                                              //      with 4 parameters (no example, should be similar to
            //                                              //      4c but simpler).
            //                                              //(see examples).
            //                                              //this[I], all its instance variables.

            //                                              //str, display information

            //                                              //SHORT option (other options will be ignored).
            TestoptionEnum testoptionSHORT
            )
        {
            return Test.strTo(this.arrbclassThisIsUsedIn(), TestoptionEnum.SHORT, this.lstbclassThisIsUsedIn);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public virtual String strTo(                        //FULL display.
            //                                              //THIS METHOD SHOULD BE IMPLEMENTED IN EVERY CLASS (ABSTRACT
            //                                              //      OR CONCRETE).
            //                                              //The final format of the string will be:
            //                                              //ObjId{Variables}==>Class{Variables}==>...==>
            //                                              //      Class{Variables}==>Bclass{Variables}. 
            //                                              //To produce this string:
            //                                              //1. Concrete class produces:
            //                                              //ObjId{Variable + ... + Variable}==>base.testoption().
            //                                              //2. All abstract classes (except Bclass) produce:
            //                                              //ClassPrefix{Variable + ... + Variable}==>base.strTo().
            //                                              //3. Bclass produces:
            //                                              //Bclass{Variable + ... + Variable}.
            //                                              //4 (see method description above).
            //                                              //this[I], all its instance variables.

            //                                              //str, display information
            )
        {
            const String strCLASS = "Bclass";

            //                                              //Will report only prefix of the objects in 
            //                                              //      lstbclassThisIsUsedIn (can be null)

            return strCLASS + "{" + Test.strTo(this.arrstrPrefix(), TestoptionEnum.SHORT, this.lstbclassThisIsUsedIn) +
                "}";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private BclassBaseClassAbstract[] arrbclassThisIsUsedIn(
            //                                              //lst, queue and stack of bclass, btuple, Enum or Exception
            //                                              //      need to be converted to arrobj before calling strTo
            //                                              //      method with 3 paramenters.
            //                                              //This method is an example and should be after class strTo
            //                                              //      methods.
            //                                              //
            //                                              //To call this method:
            //                                              //(see examples above).
            //                                              //If lst, ... is static, paramenter or local variable, you
            //                                              //      need an static method and pass lst,... as
            //                                              //      paramenter.
            //                                              //arrbclass, lstbclass converted

            //                                              //this[I], lstbclassThisIsUsedIn
            )
        {
            BclassBaseClassAbstract[] arrbclassThisIsUsedIn;
            if (
                this.lstbclassThisIsUsedIn == null
                )
            {
                arrbclassThisIsUsedIn = null;
            }
            else
            {
                arrbclassThisIsUsedIn = this.lstbclassThisIsUsedIn.ToArray();
            }

            return arrbclassThisIsUsedIn;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private String[] arrstrPrefix(                      //Sometimes content is convertes to str values.
            //                                              //
            //                                              //To call this method:
            //                                              //(see examples above).
            //                                              //arrbclass, lstbclass converted

            //                                              //this[I], lstbclassThisIsUsedIn
            )
        {
            String[] arrstrPrefix;
            if (
                this.lstbclassThisIsUsedIn == null
                )
            {
                arrstrPrefix = null;
            }
            else
            {
                arrstrPrefix = new String[this.lstbclassThisIsUsedIn.Count];
                for (int intI = 0; intI < this.lstbclassThisIsUsedIn.Count; intI = intI + 1)
                {
                    String strObjId = Test.strGetObjId(this.lstbclassThisIsUsedIn[intI]);

                    //                                      //ObjId has de form Prefix:HashCode
                    arrstrPrefix[intI] = strObjId.Substring(0, strObjId.LastIndexOf(':'));
                }
            }

            return arrstrPrefix;
        }

        //--------------------------------------------------------------------------------------------------------------
        /*OBJECT CONSTRUCTORS*/

        //--------------------------------------------------------------------------------------------------------------
        public BclassBaseClassAbstract(                     //Inicializa la parte más abstracta de cada objeto, y.
            //                                              //Acumula a la parte estática la creación de un objeto de
            //                                              //      cierto type.
            //                                              //this.*[O], Asigna lstbclass vacía. 
            )
        {
            //                                              //STATIC PART (ONE SET OF INFORMATION FOR THE APPLICATION).

            String strTypeThisFullNameAndMutability = this.GetType().FullName + "|" + this.bclassmutability;

            //                                              //Create dictionary entry if needed.
            if (
                 BclassBaseClassAbstract.dicintObjectCount.ContainsKey(strTypeThisFullNameAndMutability)
                )
            {
                //                                         //Do nothing
            }
            else
            {
                BclassBaseClassAbstract.dicintObjectCount.Add(strTypeThisFullNameAndMutability, 0);
            }

            //                                              //Add count
            BclassBaseClassAbstract.dicintObjectCount[strTypeThisFullNameAndMutability] =
                BclassBaseClassAbstract.dicintObjectCount[strTypeThisFullNameAndMutability] + 1;

            //                                              //INSTANCE PART.

            //                                              //Inicializa lista de UsedIn
            if (
                this.bclassmutability == BclassmutabilityEnum.MUTABLE
                )
            {
                this.lstbclassThisIsUsedIn = new List<BclassBaseClassAbstract>();
            }
            else
            {
                //                                          //Solo los objetos MUTABLE recolectan esta información.
                this.lstbclassThisIsUsedIn = null;
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        //                                                  //MÉTODOS DE CONSULTA.

        //--------------------------------------------------------------------------------------------------------------
        //                                                  //MÉTODOS DE TRANSFORMACIÓN.

        //--------------------------------------------------------------------------------------------------------------
        public void subAddUsedIn(                           //Añade una referencia UsedIn.
            //                                              //this[M], Añade referencia UsedIn.

            //                                              //Objeto que usa this.
            //                                              //Ejemplo, Journal Entry que usa Currency (se pasa un
            //                                              //      Journal Entryy el this que recibe este método es un
            //                                              //      Currency.
            BclassBaseClassAbstract bclassToAdd_T
            )
        {
            if (
                //                                          //Este objeto (this) no es MUTABLE
                this.lstbclassThisIsUsedIn == null
                )
                Tools.subAbort(Test.strTo(this.lstbclassThisIsUsedIn, "lstbclassThisIsUsedIn") + ", " +
                    Test.strTo(this.GetType(), "GetType") + ", " +
                    Test.strTo(this.bclassmutability, "bclassmutability") + ", " +
                    Test.strTo(bclassToAdd_T.GetType(), "bclassToAdd_T.GetType") + 
                    " this object can not have references UsedIn");

            this.lstbclassThisIsUsedIn.Add(bclassToAdd_T);

            BclassBaseClassAbstract.intUsedInAddTotalCount = BclassBaseClassAbstract.intUsedInAddTotalCount + 1;
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subRemoveUsedIn(                        //Remueve una referencia UsedIn.
            //                                              //this[M], Remueve referencia UsedIn.

            //                                              //Objeto que usaba this y que será removido.
            //                                              //Ejemplo, Journal Entry que usa Currency (se pasa un
            //                                              //      Journal Entryy el this que recibe este método es un
            //                                              //      Currency.
            BclassBaseClassAbstract bclassToRemove_T
            )
        {
            if (
                //                                          //Este objeto (this) no es MUTABLE
                this.lstbclassThisIsUsedIn == null
                )
                Tools.subAbort(
                    Test.strTo(this.arrbclassThisIsUsedIn(), TestoptionEnum.SHORT, this.lstbclassThisIsUsedIn) + ", " +
                    Test.strTo(this.GetType(), "this.GetType") + ", " +
                    Test.strTo(this.bclassmutability, "bclassmutability") + ", " +
                    Test.strTo(bclassToRemove_T.GetType(), "bclassToRemove_T.GetType") +
                    " this object can not have references UsedIn, neither can be remover");

            //                                              //Localiza el objeto y lo elimina.
            int intPos = this.lstbclassThisIsUsedIn.IndexOf(bclassToRemove_T);
            if (
                intPos < 0
                )
                Tools.subAbort("bclassToRemove_T.GetType()(" + bclassToRemove_T.GetType() + ", intPos(" + intPos +
                    " no encontro el objeto a remover");

            this.lstbclassThisIsUsedIn.RemoveAt(intPos);

            BclassBaseClassAbstract.intUsedInRemoveTotalCount = BclassBaseClassAbstract.intUsedInRemoveTotalCount + 1;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void subWriteSummary(                 //Escribe en el log de pruebas la información de la
            //                                              //      aplicación que se encuentra en la parte estática de
            //                                              //      esta clase.

            //                                              //Log en el cual se escribe la información.
            StreamWriter syssrLog_M
            )
        {
            syssrLog_M.WriteLine("");
            syssrLog_M.WriteLine("########## **********Bclass SUMMARY**********");
            syssrLog_M.WriteLine("BclassBaseClassbstract.dicintObjectCount(" +
                /*Test.strTo(Environment.NewLine, BclassBaseClassAbstract.dicintObjectCount)********/"");

            //                                              //Cantidad total de objetos construidos
            int intObjectsCount = 0;
            foreach (KeyValuePair<String, int> strint in BclassBaseClassAbstract.dicintObjectCount)
            {
                intObjectsCount = intObjectsCount + strint.Value;
            }

            syssrLog_M.WriteLine(Test.strTo(intObjectsCount, "intObjectsCount") + ", " +
                Test.strTo(BclassBaseClassAbstract.intUsedInAddTotalCount,
                    "BclassBaseClassAbstract.intUsedInAddTotalCount") +
                ", " +
                Test.strTo(BclassBaseClassAbstract.intUsedInRemoveTotalCount,
                    "BclassBaseClassAbstract.intUsedInRemoveTotalCount"));

            //                                              //Estima memoria utilizada en apuntadores UdedIn, asumimos
            //                                              //      que cada apuntador ocupa 8 bytes.
            int intMemoryUsedMB =
                (BclassBaseClassAbstract.intUsedInAddTotalCount - BclassBaseClassAbstract.intUsedInRemoveTotalCount) *
                    8 / (1024 * 1024);

            syssrLog_M.WriteLine(Test.strTo(intMemoryUsedMB ,"intMemoryUsedMB"));
        }
    }

    //==================================================================================================================
}
/*END-TASK*/