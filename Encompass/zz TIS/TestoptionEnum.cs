/*TASK Test21 Levels for strTo(NL)*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

//                                                          //AUTHOR: Towa (GLG-Gerardo López).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: 13-Mayo-2011.
//                                                          //PURPOSE:
//                                                          //Implementación de clase estática para facilitar el Testing.

namespace TowaInfrastructure
{
    //==================================================================================================================
    public enum TestoptionEnum                              //Options for methods strTo.
    //                                                      //obj.strTo(StrtoOptionEnum.SHORT) uses only SHORT option,
    //                                                      //      other opction will be ignored.
    //                                                      //Test.strTo(icalobj, StrtoOptionEnum.SHORT).
    //                                                      //For normal display (FULL option) use:
    //                                                      //obj.strTo().
    {
        //##############################################################################################################
        Z_ERROR_NOT_DEFINED,
        //##############################################################################################################

        //                                                  //Minimum of information.
        //                                                  //In collection this will be: null or length/count.
        SHORT,
        //                                                  //FULL, complete information.
        //                                                  //Call method: object.strTo() or Test.strTo(object).
        FULL,
    }

    //==================================================================================================================
}
/*END-TASK*/
