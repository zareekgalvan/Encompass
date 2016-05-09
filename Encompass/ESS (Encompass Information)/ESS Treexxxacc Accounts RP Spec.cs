/*TASK RPS.Txxxacc Relevant Part Accounts*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using TowaInfrastructure;
using AllInMemoryBase;
using EncompassInfrastructure;
 
//                                                          //AUTHOR: Towa (ADGG-Ángel González,
//                                                          //      AGV-Armando Galván, ARC-Alejandro de la Rosa).
//                                                          //CO-AUTHOR: Towa (GLG-Gerardo López).
//                                                          //DATE: 10-Febrero-2016.
//                                                          //PURPOSE:
//                                                          //Especificación de clases para Accounts.

namespace Encompass
{
    //==================================================================================================================
    public partial class TbmucoaChartOfAccounts : TbmuMultipleBranchesAbstract
    //                                                      //Clase específica para el nodo que agrupa todas las
    //                                                      //      Accounts, ya sean de balance o de resultados (income
    //                                                      //      statement).
    //                                                      //Un TbmucoaChartOfAccounts sólo podrá contener objetos de
    //                                                      //      tipo TbmubaibalBalance y 
    //                                                      //      TbmubaiinsIncomeStatement.
    //                                                      //El conjunto de Accounts puede ser solo 1 ó muchos.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        private const TbaccessMethodEnum tbaccessOPTION_Z = TbaccessMethodEnum.ARRAY_BINARY_SEARCH;
        protected override TbaccessMethodEnum tbaccessOPTION { get { return TbmucoaChartOfAccounts.tbaccessOPTION_Z; } }

        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //El dict sólo podrá contener entidades 
        //                                                  //      TbmubaibalBalance y 
        //                                                  //      TbmubaiinsIncomeStatement.

        //                                                  //TbmubaibalBalance, nodo que contiene las
        //                                                  //      cuentas de balance.
        private TbmubaibalBalance tbmubaibalbalance_Z;
        internal TbmubaibalBalance tbmubaibalbalance { get { return this.tbmubaibalbalance_Z; } }

        //                                                  //TbmubaiinsIncomeStatement, nodo que contiene las
        //                                                  //      cuentas de resultados.
        private TbmubaiinsIncomeStatement tbmubaiinsIncomeStatement_Z;
        internal TbmubaiinsIncomeStatement tbmubaiinsIncomeStatement 
                                            { get { return this.tbmubaiinsIncomeStatement_Z; } }

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo(TestoptionEnum testoptionSHORT_I)
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "[" + Test.strTo(this.tbmubaibalbalance, TestoptionEnum.SHORT) +
                Test.strTo(this.tbmubaiinsIncomeStatement, TestoptionEnum.SHORT) + "]";
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public override String strTo()
        {
            String strObjId = Test.strGetObjId(this);

            return strObjId + "{" + Test.strTo(this.tbmubaibalbalance, "tbmubaibalbalance") +
                Test.strTo(this.tbmubaiinsIncomeStatement, "tbmubaiinsIncomeStatement") +  "}" + "==>" + base.strTo();
        }
        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbmucoaChartOfAccounts(                    //Construye una "rama" de Chart of Accounts.
            //                                              //this.*[O], asigna valores.
            //                                              //PENDIENTE REVISAR **********

            //                                              //Pertenece a una organización.
            TTreeStructureAbstract tBelongTo_T,
            DateTime dateBegin_I,
            //                                              //Cualquier información relevante respecto a este nodo.
            String strObservations_I
            )
            : base(tBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, "unique", "Chart of Accounts", strObservations_I)
        {
            if (
                tBelongTo_T == null
                )
                Tools.subAbort(Test.strTo(tBelongTo_T, TestoptionEnum.SHORT) + ") no puede ser null");

        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public abstract partial class TbmubaiBalanceAndIncomeStatementAbstract : TbmuMultipleBranchesAbstract
    //                                                      //Clase Balance And Income Statement base para las
    //                                                      //      siguientes clases:
    //                                                      //      1. Balance (TbmubaibalBalance)
    //                                                      //      2. Income Statement (TbmubaiinsIncomeStatement)
    //                                                      //Estas 2 clases tienen información similar entre sí
    //                                                      //      ******documentar qué tienen entre sí*******
    //                                                      //Esta clase cuenta con el método subAddGrouper.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        private const TbaccessMethodEnum tbaccessOPTION_Z = TbaccessMethodEnum.ARRAY_BINARY_SEARCH;
        protected override TbaccessMethodEnum tbaccessOPTION
        { get { return TbmubaiBalanceAndIncomeStatementAbstract.tbaccessOPTION_Z; } }

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
            const String strCLASS = "TbmubaiBalanceAndIncomeStatementAbstract";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbmubaiBalanceAndIncomeStatementAbstract(                 
            //                                              //this.*[O], asigna valores.
            //                                              //PENDIENTE REVISAR ************

            //                                              //Nodo al que pertenece la Accounts (será el mismo para
            //                                              //      todas las Accounts de una organización).
            //                                              //Pertenece a un Chart Of Accounts (tbmucoa)
            TbmucoaChartOfAccounts tbmucoaBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta Account.
            DateTime dateBegin_I,
            //                                              //Id del Account (Ej. ????).
            String strId_I,
            //                                              //Nombre de la Account (Ej. ???).
            String strDescription_I,
            //                                              //Cualquier información relevante respecto a este Account.
            String strObservations_I
            )
            : base(tbmucoaBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TbmubaibalBalance : TbmubaiBalanceAndIncomeStatementAbstract
    //                                                      //Clase Balance, contiene la información específica de las
    //                                                      //      accounts clasificadas como de balance.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

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
            const String strCLASS = "TbmubaibalBalance";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbmubaibalBalance(                      //this.*[O], asigna valores.

            //                                              //Nodo al que pertenece la cuenta (será el mismo para
            //                                              //      todas las cuentas).
            TbmucoaChartOfAccounts tbmucoaBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta cuenta.
            DateTime dateBegin_I,
            //                                              //Id de la cuenta (Ej. ????).
            String strId_I,
            //                                              //Nombre de la cuenta (Ej. ????).
            String strDescription_I,
            //                                              //Cualquier información relevante respecto a esta 
            //                                              //      cuenta.
            String strObservations_I
            )
            : base(tbmucoaBelongTo_T, dateBegin_I, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TbmubaiinsIncomeStatement : TbmubaiBalanceAndIncomeStatementAbstract
    //                                                      //Clase Income Statement, contiene la información específica
    //                                                      //       de las accounts clasificadas como de resultados.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

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
            const String strCLASS = "TbmubaiinsIncomeStatement";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbmubaiinsIncomeStatement(              //this.*[O], asigna valores.

            //                                              //Nodo al que pertenece la cuenta (será el mismo para
            //                                              //      todas las cuentas).
            TbmucoaChartOfAccounts tbmucoaBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta cuenta.
            DateTime dateBegin_I,
            //                                              //Id de la cuenta (Ej. ????).
            String strId_I,
            //                                              //Nombre de la cuenta (Ej. ????).
            String strDescription_I,
            //                                              //Cualquier información relevante respecto a esta 
            //                                              //      cuenta.
            String strObservations_I
            )
            : base(tbmucoaBelongTo_T, dateBegin_I, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TbmugroGrouper : TbmuMultipleBranchesAbstract
    //                                                      //Clase específica para los nodos agrupadores de cuentas.
    //                                                      //Ej. (Account 123, 1 y 2 son grouper).
    //                                                      //Un TbmugroGrouper sólo podrá contener objetos de
    //                                                      //      tipo TbmugroGrouper y TbmulacLAccount.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        private const TbaccessMethodEnum tbaccessOPTION_Z = TbaccessMethodEnum.ARRAY_BINARY_SEARCH;
        protected override TbaccessMethodEnum tbaccessOPTION { get { return TbmugroGrouper.tbaccessOPTION_Z; } }

        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------
        
        /*
        //                                                  //El dict sólo podrá contener entidades TbmugroGrouper
        //                                                  //      y TbmuacclacLAccount.

        //                                                  //arrtbmugroGrouper, nodo que agrupa cuentas.
        private TbmugroGrouper[] arrtbmugroGrouper_Z;
        internal TbmugroGrouper[] arrtbmugroGrouper { get { return this.arrtbmugroGrouper_Z; } }

        //                                                  //arrtbmuacclacLAccount, nodo que contiene las
        //                                                  //      cuentas de resultados.
        private TbmuacclacLAccount[] arrtbmuacclacLAccount_Z;
        internal TbmuacclacLAccount[] arrtbmuacclacLAccount { get { return this.arrtbmuacclacLAccount_Z; } }
        */
 

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "TbmugroGrouper";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbmugroGrouper(                            //this.*[O], asigna valores.
            //                                              //PENDIENTE REVISAR ***********

            //                                              //Nodo al que pertenece este grouper.
            TbmubaiBalanceAndIncomeStatementAbstract tbmubaiBelongTo_T,
            //                                              //Fecha en la que inicia el life de este grouper.
            DateTime dateBegin_I,

            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tbmubaiBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
        internal TbmugroGrouper(                            //this.*[O], asigna valores.
            //                                              //PENDIENTE REVISAR ***********

            //                                              //Nodo al que pertenece este grouper.
            TbmugroGrouper tbmugroBelongTo_T,
            //                                              //Fecha en la que inicia el life de este grouper.
            DateTime dateBegin_I,

            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tbmugroBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public abstract partial class TbmuaccAccountAbstract : TbmuMultipleBranchesAbstract
    //                                                      //Clase Account base para las siguietes clases:
    //                                                      //      1. LAccount (TbmuacclacLAccount)
    //                                                      //      2. Sub Account (TbmuaccsacSubAccount)
    //                                                      //Estas 2 clases tienen información similar entre sí:
    //                                                      //      -arrtlveaccAccount
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        private const TbaccessMethodEnum tbaccessOPTION_Z = TbaccessMethodEnum.ARRAY_BINARY_SEARCH;
        protected override TbaccessMethodEnum tbaccessOPTION { get { return TbmuaccAccountAbstract.tbaccessOPTION_Z; } }

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //El dict sólo podrá contener entidades 
        //                                                  //      arrtlveaccAccount.

        //                                                  //arrtlveaccAccount, nodo que agrupa cuentas.
        private TlveaccAccount[] arrtlveaccAccount_Z;
        internal TlveaccAccount[] arrtlveaccAccount { get { return this.arrtlveaccAccount_Z; } }

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "TbmuaccAccountAbstract";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbmuaccAccountAbstract(
            //                                              //this.*[O], asigna valores.
            //                                              //PENDIENTE REVISAR ***********

            //                                              //Nodo al que pertenece la Account (será el mismo para
            //                                              //      todas las Accounts de una organización).
            TbmubaiBalanceAndIncomeStatementAbstract tbmubaiBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta Account.
            DateTime dateBegin_I,

            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tbmubaiBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, strId_I, strDescription_I, strObservations_I)
        {
            this.arrtlveaccAccount_Z = new TlveaccAccount[0];
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TbmuacclacLAccount : TbmuaccAccountAbstract
    //                                                      //Clase específica para las cuentas de mayor(L Account).
    //                                                      //Un TbmuacclacLAccount sólo podrá contener objetos de
    //                                                      //      tipo TbmuaccsacSubAccount o TlveaccAccount, no
    //                                                      //      puede contener ambos tipos de objetos.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        private const TbaccessMethodEnum tbaccessOPTION_Z = TbaccessMethodEnum.ARRAY_BINARY_SEARCH;
        protected override TbaccessMethodEnum tbaccessOPTION { get { return TbmuacclacLAccount.tbaccessOPTION_Z; } }

        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //El dict sólo podrá contener entidades 
        //                                                  //      arrtbmuacclacLAccount.

        //                                                  //arrtbmuacclacLAccount, nodo que agrupa cuentas.
        private TbmuacclacLAccount[] arrtbmuacclacLAccount_Z;
        internal TbmuacclacLAccount[] arrtbmuacclacLAccount { get { return this.arrtbmuacclacLAccount_Z; } }

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "TbmuacclacLAccount";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbmuacclacLAccount(
            //                                              //this.*[O], asigna valores.
            //                                              //Nodo al que pertenece la Account (será el mismo para
            //                                              //      todas las Accounts de una organización).
            TbmubaiBalanceAndIncomeStatementAbstract tbmubaiBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta Account.
            DateTime dateBegin_I,
            //                                              //Id del Account (Ej. ????).
            String strId_I,
            //                                              //Nombre de la Account (Ej. ???).
            String strDescription_I,
            //                                              //Cualquier información relevante respecto a este Account.
            String strObservations_I
            )
            : base(tbmubaiBelongTo_T, dateBegin_I, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TbmuaccsacSubAccount : TbmuaccAccountAbstract
    //                                                      //Clase específica para las sub cuentas.
    //                                                      //Un TbmuaccsacSubAccount sólo podrá contener objetos de
    //                                                      //      tipo TlveaccAccount.
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        private const TbaccessMethodEnum tbaccessOPTION_Z = TbaccessMethodEnum.ARRAY_BINARY_SEARCH;
        protected override TbaccessMethodEnum tbaccessOPTION { get { return TbmuaccsacSubAccount.tbaccessOPTION_Z; } }

        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //El dict sólo podrá contener entidades 
        //                                                  //      TbmulacLAccount.

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "TbmuaccsacSubAccount";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TbmuaccsacSubAccount(
            //                                              //this.*[O], asigna valores.
            //                                              //Nodo al que pertenece la subaccount (será el mismo para
            //                                              //      todas las subaccounts de una organización).
            TbmubaiBalanceAndIncomeStatementAbstract tbmubaiBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta subaccount.
            DateTime dateBegin_I,
            //                                              //Id de la subaccount (Ej. ????).
            String strId_I,
            //                                              //Nombre de la subaccount (Ej. ???).
            String strDescription_I,
            //                                              //Cualquier información relevante respecto a la subaccount.
            String strObservations_I
            )
            : base(tbmubaiBelongTo_T, dateBegin_I, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
    public partial class TlveaccAccount : TlveLeaveAbstract
    //                                                      //Clase accounts donde se guarda la información que tiene
    //                                                      //      una cuenta, existen 4 tipos de cuentas:
    {
        /*CONSTANTS*/
        //--------------------------------------------------------------------------------------------------------------

        /*INSTANCE VARIABLES*/
        //--------------------------------------------------------------------------------------------------------------

        //                                                  //Se agregará información específica aquí si es necesario.

        //--------------------------------------------------------------------------------------------------------------
        internal new void subReset()
        {
            base.subReset();
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            const String strCLASS = "TlveaccAccount";

            String strToString = strCLASS + "{}";

            return strToString + "==>" + base.ToString();
        }

        //--------------------------------------------------------------------------------------------------------------

        /*TRANSFORMATION METHODS*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------

        /*CONSTRUCTORS*/
        //--------------------------------------------------------------------------------------------------------------
        internal TlveaccAccount(                            //this.*[O], asigna valores.
            //                                              //PENDIENTE REVISAR ***********

            //                                              //Nodo al que pertenece la cuenta (será el mismo para
            //                                              //      todas las cuenta).
            TbmuaccAccountAbstract tbmuaccBelongTo_T,
            //                                              //Fecha en la que inicia el life de esta cuenta.
            DateTime dateBegin_I,

            String strId_I,
            String strDescription_I,
            String strObservations_I
            )
            : base(tbmuaccBelongTo_T, dateBegin_I, Tools.dateMAX_VALUE, strId_I, strDescription_I, strObservations_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
