/*TASK T2CharDescriptionTuple*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

//                                                          //AUTHOR: Towa (GLG-Gerardo López).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: 13-Mayo-2011.
//                                                          //PURPOSE:
//                                                          //Implementación de funciones o subrutinas de uso compartido
//                                                          //      en todos los sistemas.

namespace TowaInfrastructure
{
    //==================================================================================================================
    public class T2charDescriptionTuple : BtupleBaseTupleAbstract
    //                                                      //Map special character description.
    {
        //--------------------------------------------------------------------------------------------------------------
        internal char charChar;
        internal String strDescription;

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo(TestoptionEnum testoptionSHORT_I)
        {
            return "<" + Test.strTo(this.charChar, TestoptionEnum.SHORT) + ", " +
                Test.strTo(this.strDescription, TestoptionEnum.SHORT) + ">";
        }

        //--------------------------------------------------------------------------------------------------------------
        public override String strTo()
        {
            return "<" + Test.strTo(this.charChar, "charChar") + ", " +
                Test.strTo(this.strDescription, "strDescription") + ">";
        }

        //--------------------------------------------------------------------------------------------------------------
        internal T2charDescriptionTuple(char charChar_I, String strDescription_I)
        {
            this.charChar = charChar_I;
            this.strDescription = strDescription_I;
        }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
