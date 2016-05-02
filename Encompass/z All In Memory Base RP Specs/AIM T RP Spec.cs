/*TASK RPS.T Relevant Part T Abstract*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using TowaInfrastructure;

//                                                          //AUTHOR: Towa (ADGG-Ángel González,
//                                                          //      AGV-Armando Galván, ARC-Alejandro de la Rosa).
//                                                          //CO-AUTHOR: Towa (GLG-Gerardo López).
//                                                          //DATE: 9-Julio-2015.
//                                                          //PURPOSE:
//                                                          //Especificación de clases Abstract para Tree Structure
//                                                          //      (Base para todas las applicaciones All In Memory).

namespace AllInMemoryBase
{
    //==================================================================================================================
    public abstract partial class TTreeStructureAbstract : BclassBaseClassAbstract
    //                                                      //Base para clases TbBranchAbstract (que a su vez será
    //                                                      //      base para TbmuMultipleBranchesAbstract y
    //                                                      //      TbsiSingleRootAbstract) y TlveLeaveAbstract.
    //                                                      //Información común a todas las estructuras de árbol.
    //                                                      //Nótese que una "tabla" de base de datos puede ser vista
    //                                                      //      como una estructura de árbol que tiene sólo una
    //                                                      //      raíz, esto sería un tbsi que contiene muchos tlve,
    //                                                      //      uno por cada registro de la tabla.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        private const BclassmutabilityEnum bclassmutability_Z = BclassmutabilityEnum.MUTABLE;
        public override BclassmutabilityEnum bclassmutability { get { return TTreeStructureAbstract.bclassmutability_Z; } }

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //Una "hoja" pertenece a una "rama" del mismo árbol.
        //                                                  //Una "rama" también pertence a un "rama" del mismo árbol, a
        //                                                  //      menos que sea la "rama raíz" la cual podrá ser un
        //                                                  //      dato (variable de instancia) de una "rama" u "hoja"
        //                                                  //      de otro árbol.
        //                                                  //O no tener pertenencia (ser null) dado que es la "raíz" de
        //                                                  //      un conglomerado de información que es un dato de
        //                                                  //      otro tipo de objeto.
        //                                                  //Ej. Encompass no es un "tree" y contiene varios
        //                                                  //      conglomerados de información los cuales en su "rama
        //                                                  //      raiz" su tBelongsTo es null).
        //                                                  //Ej. una "hoja" de Company (tlvecom) pertenece a la "rama
        //                                                  //      raíz" de Companies (tbsicom que es un "single
        //                                                  //      root") por lo cual el tBelongsTo deberá ser
        //                                                  //      un tbsicom que es del mismo árbol.
        //                                                  //Ej. la "raíz" (tbsicom) es un "dato" de una organization
        //                                                  //      por lo cual su tBelongsTo deberá ser un
        //                                                  //      tlveorg que es una "hoja" de otro árbol.
        //                                                  //Ej. la "raíz" (tbsiorg) es un conglomerado de información
        //                                                  //      que aglutina mucha de la información de Encompass,
        //                                                  //      su pertenencia será null.
        private readonly TTreeStructureAbstract tBelongsTo_Z;
        internal TTreeStructureAbstract tBelongsTo { get { return this.tBelongsTo_Z; } }

        //                                                  //Determina los periodos de tiempo en los que esta rama u
        //                                                  //      hoja esta vigente.
        //                                                  //Ej. 2014-01-01 a 2014-06-30 y 2015-04-22 a ????.
        private /*MUTABLE*/ LifePeriods life_Z;
        internal LifePeriods life { get { return this.life_Z; } }

        //                                                  //Ej. "Towa", "a # 1".
        //                                                  //La Id puede contener espacios simples intercalados (no al
        //                                                  //      principio ni al final, ni 2 ó mas espacios juntos) y
        //                                                  //      minúsculas, sin embargo, para efectos de
        //                                                  //      identificación se eliminan los espacios y las
        //                                                  //      minúsculas se convierten a mayúsculas, por lo cual
        //                                                  //      "a # 1" y "A#1" serán la misma Id en el set.
        //                                                  //Nótese que el tBelongsTo (en caso de ser diferente de
        //                                                  //      null" es la "rama" que en su set contiene esta
        //                                                  //      objeto ("rama" u "hoja").
        private /*MUTABLE*/ String strId_Z;
        internal String strId { get { return this.strId_Z; } }

        //                                                  //Ej. "Towa System". Sin espacios extra (debe cumplir con
        //                                                  //      TrimExcel
        private /*MUTABLE*/ String strDescription_Z;
        internal String strDescription { get { return this.strDescription_Z; } }

        //                                                  //Contiene cualquier tipo de información que se quiera dejar
        //                                                  //      registrada en el objeto. Sin espacios extra, puede
        //                                                  //      ser ""
        private /*MUTABLE*/ String strObservations_Z;
        internal String strObservations { get { return this.strObservations_Z; } }

        //                                                  //PENDIENTE ************.
        //                                                  //Información para auditoría, usuario y ts de creación y
        //                                                  //      última modificación de su contenido directo, esto
        //                                                  //      es, si se modifica el contenido del set esto no se
        //                                                  //      modifica. PENSAR MÁS.

        //                                                  //NOTA PARA DISEÑAR LOS OBJETOS CONCRETOS DE TREE STRUCTURE

        //                                                  //Un conglomerado de infomación es un "arbol", que inicia
        //                                                  //      con una "rama Raíz" de información con 2, 3 ó varios
        //                                                  //      niveles de "ramas" que al final contiene "hojas",
        //                                                  //      todo esto del mismo tipo de contenido (Ej,
        //                                                  //      organizations, companies, business objects, etc.).
        //                                                  //Las relaciones necesarias para esto son "set" y tBelongsTo
        //                                                  //      las cuales se establecen en esta parte abstracta de
        //                                                  //      las "hojas" y "ramas" de todos los "arboles"
        //                                                  //      descritas an RPS.t.

        //                                                  //Adicionalmente, con datos (variables de instancia)
        //                                                  //      incluidas en la parte concretas (en las clases
        //                                                  //      necesarias para cada tipo de contenido) se pueden
        //                                                  //      establecer otros tipos de relaciones:
        //                                                  //Las "hojas" y las "ramas" pueden contener "otros 
        //                                                  //      arboles" con otro tipo de información (Ej. una "hoja
        //                                                  //      de organizations" (tlveorg) contiene un "arbol de
        //                                                  //      companies" que inicia con una "rama raíz" (tbsicom).
        //                                                  //También, cada "hoja" o "rama" puede contener referencias a
        //                                                  //      "otras hojas" u "otras ramas" de otros conglomerados
        //                                                  //      (o del mismo), para mantener esta relación
        //                                                  //      bidireccional (esto es poder movernos del contenedor
        //                                                  //      a sus contenidos, pero también, de los contenidos a
        //                                                  //      contenedor) se crea la liga "Used In" (esto es, la
        //                                                  //      "hoja"/"rama" al ser contenida, mantiene una lista
        //                                                  //      de todos sus contenedores"). La relación "Used In"
        //                                                  //      se añade o elimina con los métodos
        //                                                  //      contenido.subAddUsedIn(contenedor) o
        //                                                  //      contenido.subRemoveUsedIn(contenedor).
        //                                                  //La relaciones "Used In" pueden ser de 2 formas:
        //                                                  //a) Durante todo el "life" del contenedor. Ej. la entidad
        //                                                  //      associate contiene nacionalidad, que es una clave de
        //                                                  //      país (country, un objeto de tipo tlvecry), o bien
        //                                                  //      podrían ser varias nacionalidades (icoltlvecry). En
        //                                                  //      estos casos los objetos tlvecry establecen la
        //                                                  //      relación "Used In".
        //                                                  //b) Durante un período, que debe estar contenido en el
        //                                                  //      "life" del contenedor. Ej. la entidad associate
        //                                                  //      contiene el business object (tlvebob) al que el
        //                                                  //      associate esta asignado del 1ºEne2016 al 30Abr2016,
        //                                                  //      esta relación se establece con objetos de tipo
        //                                                  //      "pdabob" (ver clase pdaPeriodAssignment), para esto
        //                                                  //      el associate contiene un pdabob o un icolpdabob. En
        //                                                  //      estos casos, los objetos tlvebob establecen la
        //                                                  //      relación "Used In" con los objetos pdabob (estos a
        //                                                  //      tienen en su tBelongsTo al associate.

        /*COMPUTED VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //Será la misma Id pero sin espacios ni minúsculas.
        private String strIdInSet_Z;
        internal String strIdInSet
        {
            get
            {
                if (
                    this.strIdInSet_Z == null
                    )
                {
                    //                                      //Elimina espacios.
                    //                                      //Nótese que ya estaba en formato TrimExcel.
                    this.strIdInSet_Z = this.strId.Replace(" ", "").ToUpper();
                }

                return strIdInSet_Z;
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            this.strIdInSet_Z = null;

            base.subReset();
        }


        //--------------------------------------------------------------------------------------------------------------
        public override String strTo(TestoptionEnum testoptionSHORT_I)
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "[" + base.strTo(TestoptionEnum.SHORT) + ", " +
                Test.strTo(this.tBelongsTo, TestoptionEnum.SHORT) + ", " +
                Test.strTo(this.life, TestoptionEnum.SHORT) + ", " + Test.strTo(this.strId, TestoptionEnum.SHORT) + ", " +
                Test.strTo(this.strDescription, TestoptionEnum.SHORT) + ", " +
                Test.strTo(this.strObservations, TestoptionEnum.SHORT) + "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public override String strTo()
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "{" + Test.strTo(this.tBelongsTo, TestoptionEnum.SHORT) + ", " +
                Test.strTo(this.life, "life") + ", " + Test.strTo(this.strId, "strId") + ", " +
                Test.strTo(this.strDescription, "strDescription") + ", " +
                Test.strTo(this.strObservations, "strObservations") + "}" + "==>" + base.strTo();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "T";

            String strToString =
                strCLASS + "{" + Test.strTo(this.tBelongsTo, "tBelongsTo") + ", " + Test.strTo(this.life, "life") + ", "
                + Test.strTo(this.strId, "strId") + ", " + Test.strTo(this.strDescription, "strDescription") +
                ", " + Test.strTo(this.strObservations, "strObservations") + "}";

            return strToString;
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TTreeStructureAbstract(                    //Construye la parte más abstracta de una "hoja" o "rama"
            //                                              //      (esta puede ser la "rama raíz")
            //                                              //this.*[O], asigna valores.

            //                                              //Existen 3 posibilidades válidas:
            //                                              //1. null. El objeto a construir debe ser una "rama raíz"
            //                                              //      (tb).
            //                                              //2. Del mismo tipo de contenido. El tBelongsTo debe ser una
            //                                              //      "rama" y el objeto a construir puede ser "hoja" o
            //                                              //      "rama".
            //                                              //3. De tipo de contenido diferente. El tBelongsTo puede ser
            //                                              //      "hoja" o "raiz" y el objeto a construir debe ser una
            //                                              //      "rama raíz".
            //                                              //      información a la que perteneces (dónde todo este
            //                                              //      árbol es un dato).
            TTreeStructureAbstract tBelongsTo_T,

            //                                              //Pedríodo inicial de vida (life) de esta entidad
            DateTime dateBegin_I,
            DateTime dateEnd_I,

            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base()
        {
            //                                              //Las fechas y la consistencia de las fechas será verificada
            //                                              //      en el constructor de life.

            if (
                tBelongsTo_T == null
                )
            {
            }
            else
            {
                if (
                    //                                      //El BelongsTo no incluye completamente el período.
                    !tBelongsTo_T.life.boolIsPeriodIncluded(dateBegin_I, dateEnd_I)
                    )
                {
                    Tools.subAbort(Test.strTo(dateBegin_I, TestoptionEnum.SHORT) + ", " +
                        Test.strTo(dateEnd_I, TestoptionEnum.SHORT) + "," + 
                        Test.strTo(tBelongsTo_T.life, TestoptionEnum.SHORT) + 
                        ") período no incluído completamente en life");
                }

                if (
                    //                                      //PENDIENTE ************
                    //                                      //Encontrar la forma de preguntar esto sin tener que usar
                    //                                      //      los caracteres del nombre de las clases. CREO que
                    //                                      //      esto se puede hacer teniendo una clase para cada
                    //                                      //      tipo de contenido (ej. ComCompany,
                    //                                      //      BobjBusinessObject) que lo único que tenga sea el
                    //                                      //      constructor dummy, luego tener la constante
                    //                                      //      bclassContentType en T como abstract y en cada clase
                    //                                      //      concreta (Ej. TbsixxxXxxxxx, TbmuxxxXxxxxx y
                    //                                      //      TlvexxxXxxxxx), usando estas constantes se sabe el
                    //                                      //      tipo de contenido.
                    //                                      //Son del mismo tipo de contenido
                    true
                )
                {
                    if (
                        //                                  //El padre no es una "rama", debía ser un tbmu o tbsi
                        !(tBelongsTo_T is TbBranchAbstract)
                        )
                        Tools.subAbort(Test.strTo(tBelongsTo_T.GetHashCode(), TestoptionEnum.SHORT) + ", " +
                            Test.strTo(this.GetType(), TestoptionEnum.SHORT) + ") el padre debía ser tbmu o tbsi");
                }
                else
                {
                    //                                      //Son de diferente tipo de contenido

                    if (
                        //                                  //El hijo no es una "rama raíz", debía ser un tbmu o tbsi
                        !(this is TbBranchAbstract)
                        )
                        Tools.subAbort(Test.strTo(tBelongsTo_T.GetHashCode(), TestoptionEnum.SHORT) + ", " +
                            Test.strTo(this.GetType(), TestoptionEnum.SHORT) + ") el hijo debía ser tbmu o tbsi");
                }
            }

            if (
                strId_I == null
                )
                Tools.subAbort(Test.strTo(strId_I, TestoptionEnum.SHORT) + " can not be null");
            if (
                strId_I == ""
                )
                Tools.subAbort(Test.strTo(strId_I, TestoptionEnum.SHORT) + " should have an Id");
            if (
                Tools.strTrimExcel(strId_I) == strId_I
                )
                Tools.subAbort(Test.strTo(strId_I, TestoptionEnum.SHORT) + " can not contain extra spaces");
            if (
                strDescription_I == null
                )
                Tools.subAbort(Test.strTo(strDescription_I, TestoptionEnum.SHORT) + " can not be null");
            if (
                strDescription_I == ""
                )
                Tools.subAbort(Test.strTo(strDescription_I, TestoptionEnum.SHORT) + " should have a Description");
            if (
                Tools.strTrimExcel(strDescription_I) == strDescription_I
                )
                Tools.subAbort(Test.strTo(strDescription_I, TestoptionEnum.SHORT) + " can not contain extra spaces");
            if (
                strObservations_I == null
                )
                Tools.subAbort(Test.strTo(strObservations_I, TestoptionEnum.SHORT) + " can not be null");
            if (
                Tools.strTrimExcel(strObservations_I) == strObservations_I
                )
                Tools.subAbort(Test.strTo(strObservations_I, TestoptionEnum.SHORT) + " can not contain extra spaces");

            this.tBelongsTo_Z = tBelongsTo_T;

            this.life_Z = new LifePeriods(this, dateBegin_I, dateEnd_I);

            this.strId_Z = strId_I;
            this.strDescription_Z = strDescription_I;
            this.strObservations_Z = strObservations_I;
        }

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal void subChangeId(                          //Cambia Id de la entidad.
            //                                              //Nótese que también debe cambiar la Id en el set que la
            //                                              //      contiene.
            //                                              //1) Puede no tener tBelongTo (es el inicio de un
            //                                              //      conglomerado que es contenido en un objeto que no es
            //                                              //      un t (ej. tbsiorg contenido en encompass).
            //                                              //2) Puede ser el inicio de un conglomerado que pertenece a
            //                                              //      un t pero que por ser un conclomerado no pertenece
            //                                              //      set de ese t (ej. tbsicom contenido en tlveorg).
            //                                              //this[M], modifica la información.

            //                                              //Nueva Id, la Id actual es la que se tiene en la entidad.
            //                                              //No debe tener espacios extra.
            String strIdNew_I
            )
        {
            if (
                strIdNew_I == null
                )
                Tools.subAbort(Test.strTo(strIdNew_I, TestoptionEnum.SHORT) + " can not be null");
            if (
                strIdNew_I == ""
                )
                Tools.subAbort(Test.strTo(strIdNew_I, TestoptionEnum.SHORT) + " should have an Id");
            if (
                Tools.strTrimExcel(strIdNew_I) == strIdNew_I
                )
                Tools.subAbort(Test.strTo(strIdNew_I, TestoptionEnum.SHORT) + " can not contain extra spaces");

            //                                              //Por CLARIDAD se separa el proceso totalmente en 2
            //                                              //      opciones aun cuando el cambio de Id es el mismo en
            //                                              //      ambos casos

            if (
                (this.tBelongsTo != null)
                //                                          //PENDIENTE *********
                //                                          //and, this esta en el set de tBelongsTo (se requiere un
                //                                          //      método nuevo abstracto y los concretos).
                )
            {
                //                                          //Esta contenido en el set

                ((TbBranchAbstract)this.tBelongsTo).setComponents.subRemove(this);

                this.strId_Z = strIdNew_I;

                ((TbBranchAbstract)this.tBelongsTo).setComponents.subAdd(this);
            }
            else
            {
                //                                          //No pertences a ningún set.
                //                                          //Solo requiere cambiar la Id.
                this.strId_Z = strIdNew_I;
            }
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public /*CLOSE*/ partial class LifePeriods : BclassBaseClassAbstract
    //                                                      //Clase para registrar los períodos de tiempo en los que una
    //                                                      //      entidad de información está vigente.
    //                                                      //La entidad de información que la contiene en su "set" debe
    //                                                      //      contener este "life" completamente.
    //                                                      //También, las entidades de información en las cuales este
    //                                                      //      es "Used In" deben tener un life que lo contenga:
    //                                                      //1) Si es "Used In" en forma total (Ej. la nacionalidad de
    //                                                      //      un asociado que es un país, el life del país debe
    //                                                      //      contener totalmente el life del asociado).
    //                                                      //2) Si es "Used In" solo por un período (Ej. un asociado
    //                                                      //      esta asignado a un business object solo del
    //                                                      //      1ºMay2015 al 31Ene2016, por una lado, el período
    //                                                      //      1ºMay2015 al 31Ene2016 debe estar contenido en el
    //                                                      //      life del asociado, y por el otro, busines object
    //                                                      //      debe contener totalmente este períod).
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        private const BclassmutabilityEnum bclassmutability_Z = BclassmutabilityEnum.MUTABLE;
        public override BclassmutabilityEnum bclassmutability { get { return LifePeriods.bclassmutability_Z; } }

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //Entidad a la cual pertence este life.
        private readonly TTreeStructureAbstract tBelongsTo;

        //                                                  //Períodos en los que está vigente, ambos
        //                                                  //      arreglos son de la misma longitud, e indican la
        //                                                  //      vigencia en los períodos arrdateBegin[0] a
        //                                                  //      arrdateEnd[0], arrdateBegin[1] a arrdateEnd[1], ...
        //                                                  //En cada rango Begin <= End.
        //                                                  //El período 0 es más reciente que el periodo 1 y así
        //                                                  //      sucesivamente. Si el período está abierto, en
        //                                                  //      arrdateEnd[i] tiene MAX_VALUE.
        //                                                  //Si 2 período son contiguos se deben colapsar en uno sólo.
        //                                                  //Estos 2 arreglos son ascendentes (el período más reciente
        //                                                  //      será el último).
        private /*MUTABLE*/ DateTime[] arrdateBegin;
        private /*MUTABLE*/ DateTime[] arrdateEnd;

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            //                                              //Se formatea un sólo dato complejo.
            //                                              //<2014-01-01 to 2014-06-30, ...,  2015-06-22 to 9999-12-31>

            String[] arrstrPeriod = new String[this.arrdateBegin.Length];

            //                                              //Se formatean los perídos
            for (int intI = 0; intI < arrdateBegin.Length; intI = intI + 1)
            {
                arrstrPeriod[intI] = Test.strTo(arrdateBegin[intI], TestoptionEnum.SHORT) + " to " +
                    Test.strTo(arrdateEnd[intI], TestoptionEnum.SHORT);
            }

            return "<" + String.Join(", ", arrstrPeriod) + ">";
        }

        /*OBJECT CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal LifePeriods(                               //Construye con período cerrado.
            //                                              //this.*[O], asigna valores.

            //                                              //Entidad a la cual pertenece este "life".
            //                                              //El tBelongsTo de la entidad a la cual pertenece este life
            //                                              //      debe contener en su life este período abierto.
            TTreeStructureAbstract tBelongsTo_T,
            //                                              //Período cerrado.
            DateTime dateBegin_I,
            DateTime dateEnd_I
            )
        {
            if (
                tBelongsTo_T == null
                )
                Tools.subAbort(Test.strTo(tBelongsTo_T, TestoptionEnum.SHORT) + " can not be null");
            if (
                !Tools.boolIsDate(dateBegin_I)
                )
                Tools.subAbort(Test.strTo(dateBegin_I, "dateBegin_I") + " debe ser fecha");
            if (
                !Tools.boolIsDate(dateEnd_I)
                )
                Tools.subAbort(Test.strTo(dateEnd_I, "dateEnd_I") + " debe ser fecha");
            if (
                dateBegin_I > dateEnd_I
                )
                Tools.subAbort("<dateBegin_I to dateEnd_I>(<" + Test.strTo(dateBegin_I, TestoptionEnum.SHORT) + " to " +
                    Test.strTo(dateEnd_I, TestoptionEnum.SHORT) + ">) no es un período válido");
            if (
                //                                          //La entidad padre no permite este período abierto
                !tBelongsTo_T.tBelongsTo.life.boolIsPeriodIncluded(dateBegin_I, dateEnd_I)
                )
                Tools.subAbort(Test.strTo(tBelongsTo_T.tBelongsTo.life, TestoptionEnum.SHORT) +
                    " no contiene el período " + Test.strTo(dateBegin_I, TestoptionEnum.SHORT) + " a " +
                    Test.strTo(dateEnd_I, TestoptionEnum.SHORT));

            this.tBelongsTo = tBelongsTo_T;

            this.arrdateBegin = new DateTime[] { dateBegin_I };
            this.arrdateEnd = new DateTime[] { dateEnd_I };
        }

        /*RETRIEVE METHODS*/
        /*TASK Life2 boolIsPeriodIncluded* /
        //--------------------------------------------------------------------------------------------------------------
        internal bool boolIsPeriodIncluded(                 //Determina si un período está totalmente incluído en life.
            //                                              //this[I], consulta la información.

            //                                              //bool, true si sí está incluida.

            //                                              //Periodo a verificar.
            DateTime dateBegin_I,
            DateTime dateEnd_I
            )
        {
            return false;
        }
        /*END-TASK*/

        /*TASK Life3 boolIsPeriodNotIncluded* /
        //--------------------------------------------------------------------------------------------------------------
        internal bool boolIsPeriodNotIncluded(              //Determina si un período está totalmente excluido del life.
            //                                              //this[I], consulta la información.

            //                                              //bool, true si sí está totalmente excluido.

            //                                              //Periodo a verificar.
            DateTime dateBegin_I,
            DateTime dateEnd_I
            )
        {
            return false;
        }
        /*END-TASK*/

        /*TASK Life4 boolIsValidToAddPeriod*/
        //--------------------------------------------------------------------------------------------------------------
        internal List<BclassBaseClassAbstract> lstbclassIsValidToAddPeriod(
            //                                              //Verifica si es valido añadir un período a este life.
            //                                              //this[I], consulta su informaciónmodifica la información.

            //                                              //bool, si es valido.

            //                                              //Periodo que se desea verificar.
            //                                              //Este período debe estar actualmente totalmente excluido.
            //                                              //El tBelongsTo de la entidad a la cual pertenece este life
            //                                              //      debe contener en su life este período.
            DateTime dateBegin_I,
            DateTime dateEnd_I
            )
        {
            return null;
        }
        /*END-TASK*/

        /*TASK Life6 boolIsValidToRemovePeriod*/
        //--------------------------------------------------------------------------------------------------------------
        internal void boolIsValidToRemovePeriod(            //Verifica si es válido reducir la vigencia del life
            //                                              //      removiendo un período de tiempo.
            //                                              //Una entidad de información, por un período de tiempo hace
            //                                              //      referenica otra entidad a la cual se le desea
            //                                              //      reducir su life.
            //                                              //Ej. un asociado (este tiene su propia life pero no es
            //                                              //      relevante aquí), por un período de tiempo
            //                                              //      (dateBegin to dateEnd) hace referencia a un business
            //                                              //      object (en ese período esta asignado a este business
            //                                              //      object).
            //                                              //El período de vigencia a remover debe estár antes o
            //                                              //      después del período de referencia en todas las
            //                                              //      relaciones existentes para la entidad que se reduce
            //                                              //      su life.
            //                                              //this[I], consulta su informaciónmodifica la información.

            //                                              //bool, si es valido.

            //                                              //Periodo que será removido.
            //                                              //Este período debe estar actualmente incluído en el life.
            DateTime dateBegin_I,
            DateTime dateEnd_I
            )
        {
        }
        /*END-TASK*/

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        /*TASK Life5 subAddPeriod* /
        //--------------------------------------------------------------------------------------------------------------
        internal void subAddPeriod(                         //Se agrega un nuevo período a la vigencia.
            //                                              //this[M], modifica la información.

            //                                              //Periodo que será añadido.
            //                                              //Este período debe estar actualmente totalmente excluido.
            //                                              //El tBelongsTo de la entidad a la cual pertenece este life
            //                                              //      debe contener en su life este período.
            DateTime dateBegin_I,
            DateTime dateEnd_I,
            out List<BclassBaseClassAbstract> lstbclassException_O
            )
        {
        }
        /*END-TASK*/

        /*TASK Life5 subAddPeriod* /
        //--------------------------------------------------------------------------------------------------------------
        internal void subRemovePeriod(                      //Se elimina la vigencia dentro de este periodo.
            //                                              //Ninguna de las entidades de información que hacen
            //                                              //      referencia a la entidad de información a la cual
            //                                              //      pertenece este life deben, en su propia life,
            //                                              //      incluir algún día de este período.
            //                                              //this[M], modifica la información.

            //                                              //Periodo que será removido.
            //                                              //Este período debe estar actualmente incluído.
            DateTime dateBegin_I,
            DateTime dateEnd_I
            )
        {
        }
        /*END-TASK*/

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public abstract partial class TbBranchAbstract : TTreeStructureAbstract
    //                                                      //Base agrupadora para clases
    //                                                      //      TbmuMultipleBranchesAbstract y
    //                                                      //      TbsiSingleRootAbstract.
    //
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //Define el metodo de acceso que se decidió para la entidad.
        protected abstract TbaccessMethodEnum tbaccessOPTION { get; }

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //Objeto que contendrá diccionary, arreglo o lo que se haya
        //                                                  //      elegido como método para almacenar y accesar el
        //                                                  //      conjunto de t components que contiene.
        //                                                  //Nótese que si alguno de los componentes cambia de Id se
        //                                                  //      debe actualizar la información de este objeto.
        private /*MUTABLE*/ SetOfComponentsAbstract setComponents_Z;
        internal SetOfComponentsAbstract setComponents { get { return this.setComponents_Z; } }

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo(TestoptionEnum testoptionSHORT_I)
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "[" + Test.strTo(this.setComponents, TestoptionEnum.SHORT) + "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public override String strTo()
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "{" + Test.strTo(this.setComponents, "setComponents") + "}" + "==>" + base.strTo();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbBranchAbstract(                          //Construye esta parte abstracta.
            //                                              //this.*[O], asigna valores.

            TTreeStructureAbstract tBelongsTo_T,
            DateTime dateBegin_I,
            DateTime dateEnd_I,
            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tBelongsTo_T, dateBegin_I, dateEnd_I, strId_I, strDescription_I, strObservations_I)
        {
            //                                              //Para facilitar el código.
            String strTypeOfThis = this.GetType().ToString();
            String strTypeOfBelongsTo;
            if (
                tBelongsTo_T != null
                )
            {
                strTypeOfBelongsTo = tBelongsTo_T.GetType().ToString();
            }
            else
            {
                //                                          //No se usará
                strTypeOfBelongsTo = null;
            }

            if (
                //                                          //La pertenencia es a otra rama
                (tBelongsTo_T != null) && (tBelongsTo_T is TbBranchAbstract) &&
                //                                          //La pertenencia es a otro tipo de entidad.
                //                                          //Ej. un tlvecom debe pertenecer a un tbsicom (notese
                //                                          //      que esto se soporta en los nombres de las clases que
                //                                          //      deberán ser TxxxeeeDescripción donde eee es la
                //                                          //      entidad).
                //                                          //En el substring se cortan a 17 caracteres por el nombre
                //                                          //      del objeto: "Encompass.TxxxeeeDescription"
                //                                          //      quedando así la pura descripcion del objeto.
                (strTypeOfThis.Substring(17, 3) != strTypeOfBelongsTo.Substring(17, 3))
                )
            {
                Tools.subAbort(Test.strTo(strTypeOfThis, "strTypeOfThis") + ", " +
                    Test.strTo(strTypeOfBelongsTo, "strTypeOfBelongsTo") + " no son del mismo tipo de entidad");
            }

            //                                              //Construye el set de acuerdo a la opción de acceso definida
            //                                              //      para la entidad.

            /*CASE*/
            if (
                this.tbaccessOPTION == TbaccessMethodEnum.DICTIONARY
                )
            {
                this.setComponents_Z = new SetdiDictionary(this.tbaccessOPTION);
            }
            else if (
                this.tbaccessOPTION == TbaccessMethodEnum.ARRAY_BINARY_SEARCH
                )
            {
                this.setComponents_Z = new SetaoOrderedArray(this.tbaccessOPTION);
            }
            else if (
                this.tbaccessOPTION == TbaccessMethodEnum.ARRAY_SEQUENTIAL_SEARCH
                )
            {
                this.setComponents_Z = new SetauUnorderedArray(this.tbaccessOPTION);
            }
            else if (
                this.tbaccessOPTION == TbaccessMethodEnum.LIST_BINARY_SEARCH
                )
            {
                this.setComponents_Z = new SetloOrderedList(this.tbaccessOPTION);
            }
            else if (
                this.tbaccessOPTION == TbaccessMethodEnum.LIST_SEQUENTIAL_SEARCH
                )
            {
                this.setComponents_Z = new SetluUnorderedList(this.tbaccessOPTION);
            }
            else
            {
                if (
                    true
                    )
                    Tools.subAbort("this.tbaccessOPTION(" + this.tbaccessOPTION + ") no es una opción válida");
            }
            /*END_CASE*/
        }

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public enum TbaccessMethodEnum                          //Posibles métodos de acceso de un Set de Entidades.
    {
        //##############################################################################################################
        Z_ERROR_NOT_DEFINED,
        //##############################################################################################################

        //                                                  //A través de la implementación de un diccionario.
        DICTIONARY,
        //                                                  //Busqueda binaria en un arreglo ordenado.
        ARRAY_BINARY_SEARCH,
        //                                                  //Busqueda secuencial en un arreglo no ordenado.
        ARRAY_SEQUENTIAL_SEARCH,
        //                                                  //Busqueda binaria en una lista ordenada.
        LIST_BINARY_SEARCH,
        //                                                  //Busqueda secuencial en una lista no ordenada.
        LIST_SEQUENTIAL_SEARCH,
    }

    //==================================================================================================================
    public /*CLOSE*/ abstract class SetOfComponentsAbstract //Clase base para:
    //                                                      //SetdiDictionary.
    //                                                      //SetaoOrderedArray y SetauUnorderedArray.
    //                                                      //SetloOrderedList y SetluUnorderedList.
    //                                                      //Para mantener un conjunto de información (set) accesible
    //                                                      //      a través de un Id, siempre existe al tradeof de:
    //                                                      //Rapidez de acceso (recurso utilizado en localizar la
    //                                                      //      información asociada a la Id correspondiente, vs.
    //                                                      //Rapidez de añadir otras Ids y/o removerlas.
    //                                                      //      entidad de información está vigente.
    //                                                      //Por ejemplo:
    //                                                      //Un arreglo ordenado en el cual se tiene el acceso
    //                                                      //      más rápido, sin embargo el añadir o remover requiere
    //                                                      //      la reconstrucción total del arreglo. Esta estrategia
    //                                                      //      es conveniente cuando el set tiene pocos cambios
    //                                                      //Una lista que no requiere mantener el orden puede ser muy
    //                                                      //      eficiente en su actualización, sin embargo su acceso
    //                                                      //      requiere buscar en toda la lista. Esta estrategia es
    //                                                      //      conveniente cuando el set tiene muchos cambios pero
    //                                                      //      siempres se mantiene pequeña (tal vez 10 ó 20
    //                                                      //      componentes)
    //                                                      //Independiente de como se mantenga la estructura del set,
    //                                                      //      para todos lo casos de debe tener la misma
    //                                                      //      funcionalidad:
    //                                                      //0. Inicilizar el set vacío (constructor).
    //                                                      //1. Acceder a un componente del set.
    //                                                      //2. Añadir componentes al set.
    //                                                      //3. Remover componentes del set.
    {
        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            //                                              //Se formatea un sólo dato complejo.
            //                                              //{ id0, id1, ..., idN } sin embargo esto lo debe hacer cada
            //                                              //      una de las estrategias específicas.
            return "";
        }

        /*OBJECT CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal SetOfComponentsAbstract(                   //this.*[O], asigna valores.

            //                                              //Opción con la que se accederá a este set.
            TbaccessMethodEnum tbaccessOPTION_I
            )
        {
        }

        /*RETRIEVE METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal abstract TTreeStructureAbstract tAccess(   //Localiza el contenido asociado a una Id.
            //                                              //t, null si NO existe, si existe regresa el contenido.
            //                                              //this[I], consulta la información.

            //                                              //Id a acceder.
            //                                              //Puede contener espacios y minúsculas, sin embargo en la
            //                                              //      Id del set los espacios son eliminados y minúsculas
            //                                              //      son convertidas en mayúsculas ("a # 1" y "A#1"
            //                                              //      corresponden ambas a las Id "A#1")
            //
            String strId_I
            );

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal abstract void subAdd(                      //Añade un componente al set.
            //                                              //this[M], añade un componente al set.

            //                                              //Componente que será añadido al set.
            TTreeStructureAbstract tToAdd_T
            );

        //--------------------------------------------------------------------------------------------------------------
        internal abstract void subRemove(                   //Remueve un componente del set.
            //                                              //this[M], remueve un componente del set.

            //                                              //Componente que será añadido al set.
            TTreeStructureAbstract tToRemove_T
            );

        //--------------------------------------------------------------------------------------------------------------

        /*OTHER SHARED METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public /*CLOSE*/ class SetdiDictionary : SetOfComponentsAbstract
    //                                                      //Implementación del método de acceso "diccionario".
    {
        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        private readonly /*MUTABLE*/ Dictionary<String, TTreeStructureAbstract> dictComponentes;

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            //                                              //Se formatea un sólo dato complejo.
            //                                              //{ id0, id1, ..., idN } sin embargo esto lo debe hacer cada
            //                                              //      una de las estrategias específicas.

            //                                              //Se pasan las dos Ids a dos arreglos ordenados de llaves.
            String[] arrstrIds = new String[this.dictComponentes.Count];
            String[] arrstrIdsInSet = new String[arrstrIds.Length];

            int intX = 0;
            foreach (KeyValuePair<String, TTreeStructureAbstract> strt in this.dictComponentes)
            {
                arrstrIds[intX] = strt.Value.strId;
                arrstrIdsInSet[intX] = strt.Key;

                intX = intX + 1;
            }

            Array.Sort(arrstrIdsInSet, arrstrIds);

            return Test.strTo(arrstrIds, TestoptionEnum.SHORT);
        }

        /*OBJECT CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal SetdiDictionary(                           //this.*[O], asigna valores.

            //                                              //Opción con la que se accederá este set.
            TbaccessMethodEnum tbaccessOPTION_I
            )
            : base(tbaccessOPTION_I)
        {
            if (
                tbaccessOPTION_I != TbaccessMethodEnum.DICTIONARY
                )
                Tools.subAbort("tbaccessOPTION_I(" + tbaccessOPTION_I + ") debía ser " +
                    TbaccessMethodEnum.DICTIONARY);

            this.dictComponentes = new Dictionary<String, TTreeStructureAbstract>();
        }

        /*RETRIEVE METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal override TTreeStructureAbstract tAccess(   //Localiza el contenido asociado a una Id.
            //                                              //t, null si NO existe, si existe regresa el contenido.

            //                                              //this[I], consulta la información.

            //                                              //Id a acceder.
            //                                              //Puede contener espacios y minúsculas, sin embargo en la
            //                                              //      Id del set los espacios son eliminados y minúsculas
            //                                              //      son convertidas en mayúsculas ("a # 1" y "A#1"
            //                                              //      corresponden ambas a las Id "A#1")
            //
            String strId_I
            )
        {
            TTreeStructureAbstract tAccess;
            if (
                this.dictComponentes.ContainsKey(strId_I.Replace(" ", "").ToUpper())
                )
            {
                tAccess = this.dictComponentes[strId_I.Replace(" ", "").ToUpper()];
            }
            else
            {
                tAccess = null;
            }

            return tAccess;
        }

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal override void subAdd(                      //Añade un componente al set.
            //                                              //this[M], añade un componente al set.

            //                                              //Componente que será añadido al set.
            TTreeStructureAbstract tToAdd_T
            )
        {
            this.dictComponentes.Add(tToAdd_T.strIdInSet, tToAdd_T);
        }

        //--------------------------------------------------------------------------------------------------------------
        internal override void subRemove(                   //Remueve un componente del set.
            //                                              //this[M], remueve un componente del set.

            //                                              //Componente que será añadido al set.
            TTreeStructureAbstract tToRemove_T
            )
        {
            this.dictComponentes.Remove(tToRemove_T.strIdInSet);
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public /*CLOSE*/ class SetaoOrderedArray : SetOfComponentsAbstract
    //                                                      //Implementación del método de acceso "arreglo ordenado".
    {
        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        private /*MUTABLE*/ String[] arrstrIdsInSet;
        private /*MUTABLE*/ TTreeStructureAbstract[] arrtComponents;

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            /*
            //                                              //Se formatea un sólo dato complejo.
            //                                              //{ id0, id1, ..., idN } sin embargo esto lo debe hacer cada
            //                                              //      una de las estrategias específicas.

            //                                              //Se crea un arreglo de Ids.
            String[] arrstrIds = new String[this.arrtComponents.Length];

            //                                              //Guarda los strId's de los objetos en el arreglo.
            for (int intX = 0; intX < this.arrtComponents.Length; intX = intX + 1)
            {
                arrstrIds[intX] = this.arrtComponents[intX].strId;
            }
            */
            return Test.strTo(this.arrtComponents, TestoptionEnum.SHORT);
        }

        /*OBJECT CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal SetaoOrderedArray(                         //this.*[O], asigna valores.

            //                                              //Opción con la que se accederá este set.
            TbaccessMethodEnum tbaccessOPTION_I
            )
            : base(tbaccessOPTION_I)
        {
            if (
                tbaccessOPTION_I != TbaccessMethodEnum.ARRAY_BINARY_SEARCH
                )
                Tools.subAbort("tbaccessOPTION_I(" + tbaccessOPTION_I + ") debía ser " +
                    TbaccessMethodEnum.ARRAY_BINARY_SEARCH);

            //                                              //Crea el arreglo de objetos TStructureAbstract.
            this.arrtComponents = new TTreeStructureAbstract[0];
            //                                              //Crea el arreglo de las llaves.
            this.arrstrIdsInSet = new String[0];
        }

        /*RETRIEVE METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal override TTreeStructureAbstract tAccess(   //Localiza el contenido asociado a una Id.
            //                                              //t, null si NO existe, si existe regresa el contenido.

            //                                              //this[I], consulta la información.

            //                                              //Id a acceder.
            //                                              //Puede contener espacios y minúsculas, sin embargo en la
            //                                              //      Id del set los espacios son eliminados y minúsculas
            //                                              //      son convertidas en mayúsculas ("a # 1" y "A#1"
            //                                              //      corresponden ambas a las Id "A#1")
            //
            String strId_I
            )
        {
            TTreeStructureAbstract tAccess;
            int intIdPosition = Array.BinarySearch(this.arrstrIdsInSet, strId_I.Replace(" ", "").ToUpper());
            if (
                //                                          //Id existe en el arreglo.
                intIdPosition >= 0
                )
            {
                //                                              //Extrae el objeto en la posición de strId.
                tAccess = this.arrtComponents[intIdPosition];
            }
            else
            {
                tAccess = null;
            }

            return tAccess;
        }

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal override void subAdd(                      //Añade un componente al set.
            //                                              //this[M], añade un componente al set.

            //                                              //Componente que será añadido al set.
            TTreeStructureAbstract tToAdd_T
            )
        {
            String[] arrstrIdsInSetNew = new String[this.arrstrIdsInSet.Length + 1];
            TTreeStructureAbstract[] arrtComponentsNew = new TTreeStructureAbstract[arrstrIdsInSetNew.Length];

            int intX = 0;
            /*WHILE-DO*/
            //                                              //Recorre el arreglo hasta encontrar la posición donde se
            //                                              //      agregará el objeto t.
            while (
                intX < arrstrIdsInSet.Length &&
                String.Compare(this.arrstrIdsInSet[intX], tToAdd_T.strIdInSet) < 0
            )
            {
                intX = intX + 1;
            }

            if (
                this.arrstrIdsInSet.Length == intX ||
                this.arrstrIdsInSet[intX] != tToAdd_T.strIdInSet
                )
            {
                //                                              //Se agrega nuevo Id en la posición correcta.
                Array.Copy(this.arrstrIdsInSet, 0, arrstrIdsInSetNew, 0, intX);
                arrstrIdsInSetNew[intX] = tToAdd_T.strIdInSet;
                Array.Copy(this.arrstrIdsInSet, intX, arrstrIdsInSetNew, intX + 1, this.arrstrIdsInSet.Length - intX);

                //                                              //Se agrega el nuevo t en la posición correcta.
                Array.Copy(this.arrtComponents, 0, arrtComponentsNew, 0, intX);
                arrtComponentsNew[intX] = tToAdd_T;
                Array.Copy(this.arrtComponents, intX, arrtComponentsNew, intX + 1,
                        this.arrstrIdsInSet.Length - intX);

                this.arrstrIdsInSet = arrstrIdsInSetNew;
                this.arrtComponents = arrtComponentsNew;
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        internal override void subRemove(                   //Remueve un componente del set.
            //                                              //this[M], remueve un componente del set.

            //                                              //Componente que será removido del set.
            TTreeStructureAbstract tToRemove_T
            )
        {
            String[] arrstrIdsInSetNew = new String[this.arrstrIdsInSet.Length - 1];
            TTreeStructureAbstract[] arrtComponentsNew = new TTreeStructureAbstract[arrstrIdsInSetNew.Length];

            int intX = Array.BinarySearch(arrstrIdsInSet, tToRemove_T.strIdInSet);

            if (
                intX >= 0
                )
            {
                Array.Copy(this.arrstrIdsInSet, 0, arrstrIdsInSetNew, 0, intX);
                Array.Copy(this.arrstrIdsInSet, intX + 1, arrstrIdsInSetNew, intX,
                        this.arrstrIdsInSet.Length - (intX + 1));

                Array.Copy(this.arrtComponents, 0, arrtComponentsNew, 0, intX);
                Array.Copy(this.arrtComponents, intX + 1, arrtComponentsNew, intX,
                        this.arrstrIdsInSet.Length - (intX + 1));

                this.arrstrIdsInSet = arrstrIdsInSetNew;
                this.arrtComponents = arrtComponentsNew;
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public /*CLOSE*/ class SetauUnorderedArray : SetOfComponentsAbstract
    //                                                      //Implementación del método de acceso "arreglo no ordenado".
    {
        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        private /*MUTABLE*/ String[] arrstrIdsInSet;
        private /*MUTABLE*/ TTreeStructureAbstract[] arrtComponents;

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            //                                              //Se formatea un sólo dato complejo.
            //                                              //{ id0, id1, ..., idN } sin embargo esto lo debe hacer cada
            //                                              //      una de las estrategias específicas.

            //                                              //Se crea un arreglo de Ids.
            String[] arrstrIds = new String[this.arrtComponents.Length];

            //                                              //Guarda los strId's de los objetos en el arreglo.
            for (int intX = 0; intX < this.arrtComponents.Length; intX = intX + 1)
            {
                arrstrIds[intX] = this.arrtComponents[intX].strId;
            }

            return Test.strTo(arrstrIds, TestoptionEnum.SHORT);
        }

        /*OBJECT CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal SetauUnorderedArray(                         //this.*[O], asigna valores.

            //                                              //Opción con la que se accederá este set.
            TbaccessMethodEnum tbaccessOPTION_I
            )
            : base(tbaccessOPTION_I)
        {
            if (
                tbaccessOPTION_I != TbaccessMethodEnum.ARRAY_SEQUENTIAL_SEARCH
                )
                Tools.subAbort("tbaccessOPTION_I(" + tbaccessOPTION_I + ") debía ser " +
                    TbaccessMethodEnum.ARRAY_SEQUENTIAL_SEARCH);

            //                                              //Crea el arreglo de objetos TStructureAbstract.
            this.arrtComponents = new TTreeStructureAbstract[0];
            //                                              //Crea el arreglo de las llaves.
            this.arrstrIdsInSet = new String[0];
        }

        /*RETRIEVE METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal override TTreeStructureAbstract tAccess(//Localiza el contenido asociado a una Id.
            //                                              //t, null si NO existe, si existe regresa el contenido.

            //                                              //this[I], consulta la información.

            //                                              //Id a acceder.
            //                                              //Puede contener espacios y minúsculas, sin embargo en la
            //                                              //      Id del set los espacios son eliminados y minúsculas
            //                                              //      son convertidas en mayúsculas ("a # 1" y "A#1"
            //                                              //      corresponden ambas a las Id "A#1")
            //
            String strId_I
            )
        {
            TTreeStructureAbstract tAccess;

            int intIdPosition = 0;
            /*WHILE-DO*/
            //                                              //Recorre el arreglo hasta encontrar la posición donde se
            //                                              //      encuentra el objeto t.
            while (
                intIdPosition < arrstrIdsInSet.Length &&
                this.arrstrIdsInSet[intIdPosition] != strId_I.Replace(" ", "").ToUpper()
            )
            {
                intIdPosition = intIdPosition + 1;
            }

            if (
                intIdPosition < arrstrIdsInSet.Length &&
                //                                          //Id existe en el arreglo.
                this.arrstrIdsInSet[intIdPosition] == strId_I.Replace(" ", "").ToUpper()
                )
            {
                //                                              //Extrae el objeto en la posición de strId.
                tAccess = this.arrtComponents[intIdPosition];
            }
            else
            {
                tAccess = null;
            }

            return tAccess;
        }

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal override void subAdd(                      //Añade un componente al set.
            //                                              //this[M], añade un componente al set.

            //                                              //Componente que será añadido al set.
            TTreeStructureAbstract tToAdd_T
            )
        {
            int intIdPosition = 0;
            /*WHILE-DO*/
            //                                              //Recorre el arreglo hasta encontrar la posición donde se
            //                                              //      encuentra el objeto t.
            while (
                intIdPosition < arrstrIdsInSet.Length &&
                this.arrstrIdsInSet[intIdPosition] != tToAdd_T.strIdInSet
            )
            {
                intIdPosition = intIdPosition + 1;
            }

            if (
                intIdPosition >= arrstrIdsInSet.Length
                )
            {
                //                                              //Se crean los nuevos arreglos.
                String[] arrstrIdsInSetNew = new String[this.arrstrIdsInSet.Length + 1];
                TTreeStructureAbstract[] arrtComponentsNew = new TTreeStructureAbstract[arrstrIdsInSetNew.Length];

                //                                              //Se agrega el nuevo Id al final del arreglo.
                Array.Copy(this.arrstrIdsInSet, 0, arrstrIdsInSetNew, 0, this.arrstrIdsInSet.Length);
                arrstrIdsInSetNew[this.arrstrIdsInSet.Length + 1] = tToAdd_T.strIdInSet;

                //                                              //Se agrega el nuevo t al final del arreglo.
                Array.Copy(this.arrtComponents, 0, arrtComponentsNew, 0, this.arrstrIdsInSet.Length);
                arrtComponentsNew[this.arrstrIdsInSet.Length + 1] = tToAdd_T;

                this.arrstrIdsInSet = arrstrIdsInSetNew;
                this.arrtComponents = arrtComponentsNew;
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        internal override void subRemove(                   //Remueve un componente del set.
            //                                              //this[M], remueve un componente del set.

            //                                              //Componente que será removido del set.
            TTreeStructureAbstract tToRemove_T
            )
        {
            String[] arrstrIdsInSetNew = new String[this.arrstrIdsInSet.Length - 1];
            TTreeStructureAbstract[] arrtComponentsNew = new TTreeStructureAbstract[arrstrIdsInSetNew.Length];

            int intIdPosition = 0;
            /*WHILE-DO*/
            //                                              //Recorre el arreglo hasta encontrar la posición donde se
            //                                              //      encuentra el objeto t.
            while (
                intIdPosition < arrstrIdsInSet.Length &&
                this.arrstrIdsInSet[intIdPosition] != tToRemove_T.strId
            )
            {
                intIdPosition = intIdPosition + 1;
            }

            if (
               intIdPosition < arrstrIdsInSet.Length
               )
            {
                Array.Copy(this.arrstrIdsInSet, 0, arrstrIdsInSetNew, 0, intIdPosition);
                Array.Copy(this.arrstrIdsInSet, intIdPosition + 1, arrstrIdsInSetNew, intIdPosition,
                    this.arrstrIdsInSet.Length - (intIdPosition + 1));

                Array.Copy(this.arrtComponents, 0, arrtComponentsNew, 0, intIdPosition);
                Array.Copy(this.arrtComponents, intIdPosition + 1, arrtComponentsNew, intIdPosition,
                        this.arrstrIdsInSet.Length - (intIdPosition + 1));

                this.arrstrIdsInSet = arrstrIdsInSetNew;
                this.arrtComponents = arrtComponentsNew;
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public /*CLOSE*/ class SetloOrderedList : SetOfComponentsAbstract
    //                                                      //Implementación del método de acceso "lista ordenada".
    {
        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        private /*MUTABLE*/ List<String> lststrIdsInSet = new List<String>();
        private /*MUTABLE*/ List<TTreeStructureAbstract> lsttComponents = new List<TTreeStructureAbstract>();

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            //                                              //Se formatea un sólo dato complejo.
            //                                              //{ id0, id1, ..., idN } sin embargo esto lo debe hacer cada
            //                                              //      una de las estrategias específicas.

            //                                              //Se crea una lista de Ids.
            List<String> lststrIds = new List<String>();

            //                                              //Guarda los strId's de los objetos en la lista.
            for (int intX = 0; intX < this.lsttComponents.Count; intX = intX + 1)
            {
                lststrIds[intX] = this.lsttComponents[intX].strId;
            }

            return Test.strTo(lststrIds, TestoptionEnum.SHORT);
        }

        /*OBJECT CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal SetloOrderedList(                         //this.*[O], asigna valores.

            //                                              //Opción con la se se accederá este set.
            TbaccessMethodEnum tbaccessOPTION_I
            )
            : base(tbaccessOPTION_I)
        {
            if (
                tbaccessOPTION_I != TbaccessMethodEnum.LIST_BINARY_SEARCH
                )
                Tools.subAbort("tbaccessOPTION_I(" + tbaccessOPTION_I + ") debía ser " +
                    TbaccessMethodEnum.LIST_BINARY_SEARCH);

            //                                              //Crea la lista de objetos TStructureAbstract.
            this.lsttComponents = new List<TTreeStructureAbstract>();
            //                                              //Crea la lista de las llaves.
            this.lststrIdsInSet = new List<String>();
        }

        /*RETRIEVE METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal override TTreeStructureAbstract tAccess(//Localiza el contenido asociado a una Id.
            //                                              //t, null si NO existe, si existe regresa el contenido.

            //                                              //this[I], consulta la información.

            //                                              //Id a acceder.
            //                                              //Puede contener espacios y minúsculas, sin embargo en la
            //                                              //      Id del set los espacios son eliminados y minúsculas
            //                                              //      son convertidas en mayúsculas ("a # 1" y "A#1"
            //                                              //      corresponden ambas a las Id "A#1")
            //
            String strId_I
            )
        {
            TTreeStructureAbstract tAccess;
            int intIdPosition = Array.BinarySearch(this.lststrIdsInSet.ToArray(), strId_I.Replace(" ", "").ToUpper());
            if (
                //                                          //Id existe en el arreglo.
                intIdPosition >= 0
                )
            {
                //                                              //Extrae el objeto en la posición de strId.
                tAccess = this.lsttComponents[intIdPosition];
            }
            else
            {
                tAccess = null;
            }

            return tAccess;
        }

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal override void subAdd(                      //Añade un componente al set.
            //                                              //this[M], añade un componente al set.

            //                                              //Componente que será añadido al set.
            TTreeStructureAbstract tToAdd_T
            )
        {

            int intX = 0;
            /*WHILE-DO*/
            //                                              //Recorre la lista hasta encontrar la posición donde se
            //                                              //      agregará el objeto t.
            while (
                intX < lststrIdsInSet.Count &&
                String.Compare(this.lststrIdsInSet[intX], tToAdd_T.strIdInSet) < 0
            )
            {
                intX = intX + 1;
            }

            if (
                intX == this.lststrIdsInSet.Count ||
                this.lststrIdsInSet[intX] != tToAdd_T.strIdInSet
                )
            {
                //                                              //Se agrega el nuevo Id a la lista.
                this.lststrIdsInSet.Insert(intX, tToAdd_T.strIdInSet);

                //                                              //Se agrega el nuevo t a la lista.
                this.lsttComponents.Insert(intX, tToAdd_T);
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        internal override void subRemove(                   //Remueve un componente del set.
            //                                              //this[M], remueve un componente del set.

            //                                              //Componente que será removido del set.
            TTreeStructureAbstract tToRemove_T
            )
        {
            //                                              //Busca la posición del objeto en la lista.
            int intX = Array.BinarySearch(lststrIdsInSet.ToArray(), tToRemove_T.strIdInSet);

            if (
                intX >= 0
                )
            {
                //                                              //Elimina el id de la lista.
                this.lststrIdsInSet.RemoveAt(intX);

                //                                              //Elimina el t de la lista.
                this.lsttComponents.RemoveAt(intX);
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public /*CLOSE*/ class SetluUnorderedList : SetOfComponentsAbstract
    //                                                      //Implementación del método de acceso "lista ordenada".
    {
        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        private /*MUTABLE*/ List<String> lststrIdsInSet = new List<String>();
        private /*MUTABLE*/ List<TTreeStructureAbstract> lsttComponents = new List<TTreeStructureAbstract>();

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            //                                              //Se formatea un sólo dato complejo.
            //                                              //{ id0, id1, ..., idN } sin embargo esto lo debe hacer cada
            //                                              //      una de las estrategias específicas.

            //                                              //Se crea una lista de Ids.
            List<String> lststrIds = new List<String>();

            //                                              //Guarda los strId's de los objetos en la lista.
            for (int intX = 0; intX < this.lsttComponents.Count; intX = intX + 1)
            {
                lststrIds[intX] = this.lsttComponents[intX].strId;
            }

            return Test.strTo(lststrIds, TestoptionEnum.SHORT);
        }

        /*OBJECT CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal SetluUnorderedList(                         //this.*[O], asigna valores.

            //                                              //Opción con la se se accederá este set.
            TbaccessMethodEnum tbaccessOPTION_I
            )
            : base(tbaccessOPTION_I)
        {
            if (
                tbaccessOPTION_I != TbaccessMethodEnum.LIST_SEQUENTIAL_SEARCH
                )
                Tools.subAbort("tbaccessOPTION_I(" + tbaccessOPTION_I + ") debía ser " +
                    TbaccessMethodEnum.LIST_SEQUENTIAL_SEARCH);

            //                                              //Crea la lista de objetos TStructureAbstract.
            this.lsttComponents = new List<TTreeStructureAbstract>();
            //                                              //Crea la lista de las llaves.
            this.lststrIdsInSet = new List<String>();
        }

        /*RETRIEVE METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal override TTreeStructureAbstract tAccess(//Localiza el contenido asociado a una Id.
            //                                              //t, null si NO existe, si existe regresa el contenido.

            //                                              //this[I], consulta la información.

            //                                              //Id a acceder.
            //                                              //Puede contener espacios y minúsculas, sin embargo en la
            //                                              //      Id del set los espacios son eliminados y minúsculas
            //                                              //      son convertidas en mayúsculas ("a # 1" y "A#1"
            //                                              //      corresponden ambas a las Id "A#1")
            //
            String strId_I
            )
        {
            TTreeStructureAbstract tAccess;

            int intIdPosition = 0;
            /*WHILE-DO*/
            //                                              //Recorre la lista hasta encontrar la posición donde se
            //                                              //      encuentra el objeto t.
            while (
                intIdPosition < lststrIdsInSet.Count &&
                this.lststrIdsInSet[intIdPosition] != strId_I.Replace(" ", "").ToUpper()
            )
            {
                intIdPosition = intIdPosition + 1;
            }

            if (
                intIdPosition < lststrIdsInSet.Count &&
                //                                          //Id existe en la lista.
                this.lststrIdsInSet[intIdPosition] == strId_I.Replace(" ", "").ToUpper()
                )
            {
                //                                              //Extrae el objeto en la posición de strId.
                tAccess = this.lsttComponents[intIdPosition];
            }
            else
            {
                tAccess = null;
            }

            return tAccess;
        }

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------
        internal override void subAdd(                      //Añade un componente al set.
            //                                              //this[M], añade un componente al set.

            //                                              //Componente que será añadido al set.
            TTreeStructureAbstract tToAdd_T
            )
        {
            int intIdPosition = 0;
            /*WHILE-DO*/
            //                                              //Recorre la lista hasta encontrar la posición donde se
            //                                              //      encuentra el objeto t.
            while (
                intIdPosition < lststrIdsInSet.Count &&
                this.lststrIdsInSet[intIdPosition] != tToAdd_T.strIdInSet
            )
            {
                intIdPosition = intIdPosition + 1;
            }

            if (
                intIdPosition >= lststrIdsInSet.Count
                )
            {
                //                                              //Se agrega el nuevo Id a la lista.
                this.lststrIdsInSet.Add(tToAdd_T.strIdInSet);

                //                                              //Se agrega el nuevo t a la lista.
                this.lsttComponents.Add(tToAdd_T);
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        internal override void subRemove(                   //Remueve un componente del set.
            //                                              //this[M], remueve un componente del set.

            //                                              //Componente que será removido del set.
            TTreeStructureAbstract tToRemove_T
            )
        {
            int intIdPosition = 0;
            /*WHILE-DO*/
            //                                              //Recorre la lista hasta encontrar la posición donde se
            //                                              //      encuentra el objeto t.
            while (
                intIdPosition < lststrIdsInSet.Count &&
                this.lststrIdsInSet[intIdPosition] != tToRemove_T.strId
            )
            {
                intIdPosition = intIdPosition + 1;
            }

            if (
               intIdPosition < lststrIdsInSet.Count
               )
            {
                //                                              //Elimina el Id de la lista.
                this.lststrIdsInSet.RemoveAt(intIdPosition);

                //                                              //Elimina el t de la lista.
                this.lsttComponents.RemoveAt(intIdPosition);
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public abstract partial class TbmuMultipleBranchesAbstract : TbBranchAbstract
    //                                                      //Base agrupadora para clases TbmuxxxXxxxxxx.
    //
    {
        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //El dict sólo podrá contener entidades tbmu o
        //                                                  //      tlve.

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Tbmu";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbmuMultipleBranchesAbstract(           //this.*[O], asigna valores.

            TTreeStructureAbstract tBelongTo_T,
            DateTime dateBegin_I,
            DateTime dateEnd_I,
            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tBelongTo_T, dateBegin_I, dateEnd_I, strId_I, strDescription_I, strObservations_I)
        {
            if (
                tBelongTo_T == null
                )
            {
                //                                          //NO HACE NADA, NO HAY NADA QUE VERIFICAR
            }
            else
            {
                //                                          //Para facilitar el código.
                String strTypeOfThis = this.GetType().ToString();
                String strTypeOfBelongsTo = tBelongTo_T.GetType().ToString();

                //                                          //Nótese que esto se soporta en los nombres de las clases
                //                                          //      que deberán ser TxxxeeeDescripción donde eee es
                //                                          //      el tipo de entidad).
                if (
                    tBelongTo_T is TlveLeaveAbstract
                    )
                {
                    //                                      //Si pertenece a una hoja es que se trata de la raíz y la
                    //                                      //      hoja debe ser se otro tipo de entidad

                    if (
                        //                                  //La pertenencia es del mismo tipo de entidad.
                        (strTypeOfThis.Substring(17, 3) == strTypeOfBelongsTo.Substring(17, 3))
                        )
                    {
                        Tools.subAbort("" + Test.strTo(strTypeOfThis, "strTypeOfThis") + ", " +
                            Test.strTo(strTypeOfBelongsTo, "strTypeOfBelongsTo") + ") son del mismo tipo de entidad");
                    }
                }
                else
                {
                    //                                      //Si pertenece a una una rama esta debe ser del mismo tipo
                    //                                      //      de entidad

                    if (
                        //                                  //La pertenencia es a otro tipo de entidad.
                        (strTypeOfThis.Substring(17, 3) != strTypeOfBelongsTo.Substring(17, 3))
                        )
                    {
                        Tools.subAbort(Test.strTo(strTypeOfThis, "strTypeOfThis") + ", " +
                            Test.strTo(strTypeOfBelongsTo, "strTypeOfBelongsTo") + " no son del mismo tipo de entidad");
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public abstract partial class TbsiSingleRootAbstract : TbBranchAbstract
    //                                                      //Base agrupadora para clases TbsixxxXxxxxxx.
    {
        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //El dict solo podrá contener entidades tlve.

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Tbsi";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbsiSingleRootAbstract(                    //Construye esta parte abstracta.
            //                                              //this.*[O], asigna valores.

            TlveLeaveAbstract tlveBelongTo_T,
            DateTime dateBegin_I,
            DateTime dateEnd_I,
            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tlveBelongTo_T, dateBegin_I, dateEnd_I, strId_I, strDescription_I, strObservations_I)
        {
            if (
                tlveBelongTo_T == null
                )
            {
                //                                          //NO HACE NADA, NO HAY NADA QUE VERIFICAR
            }
            else
            {
                //                                          //Para facilitar el código.
                String strTypeOfThis = this.GetType().ToString();
                String strTypeOfBelongsTo = tlveBelongTo_T.GetType().ToString();

                //                                          //Nótese que esto se soporta en los nombres de las clases
                //                                          //      que deberán ser TxxxeeeDescripción donde eee es
                //                                          //      el tipo de entidad).
                if (
                    //                                      //La pertenencia es del mismo tipo de entidad.
                    (strTypeOfThis.Substring(17, 3) == strTypeOfBelongsTo.Substring(17, 3))
                    )
                {
                    Tools.subAbort(Test.strTo(strTypeOfThis, "strTypeOfThis") + ", " +
                        Test.strTo(strTypeOfBelongsTo, "strTypeOfBelongsTo") + " son del mismo tipo de entidad");
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public abstract partial class TlveLeaveAbstract : TTreeStructureAbstract
    //                                                      //Base agrupadora para clases TlvexxxXxxxxxx.
    //
    {
        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "Tlve";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TlveLeaveAbstract(                  //this.*[O], asigna valores.

            TbBranchAbstract tbBelongsTo_T,
            //                                              //Pedríodo inicial de vida (life) de esta entidad
            DateTime dateBegin_I,
            DateTime dateEnd_I,

            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tbBelongsTo_T, dateBegin_I, dateEnd_I, strId_I, strDescription_I, strObservations_I)
        {
            //                                              //Para facilitar el código.
            String strTypeOfThis = this.GetType().ToString();
            String strTypeOfBelongsTo = tbBelongsTo_T.GetType().ToString();

            //                                              //Nótese que esto se soporta en los nombres de las clases
            //                                              //      que deberán ser TxxxeeeDescripción donde eee es
            //                                              //      el tipo de entidad).
            if (
                //                                          //La pertenencia es a otro tipo de entidad.
                (strTypeOfThis.Substring(17, 3) != strTypeOfBelongsTo.Substring(17, 3))
                )
            {
                Tools.subAbort(Test.strTo(strTypeOfThis, "strTypeOfThis") + ", " +
                    Test.strTo(strTypeOfBelongsTo, "strTypeOfBelongsTo") + " no son del mismo tipo de entidad");
            }
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
