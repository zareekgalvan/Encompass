/*TASK TestchartypeEnum*/
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
    public enum TestchartypeEnum                            //Posibles tipos de caracteres..
    {
        //##############################################################################################################
        Z_ERROR_NOT_DEFINED,
        //##############################################################################################################

        //                                                  //Es un caracter que puede se producido en un teclado Mac o
        //                                                  //      Dell en español. (Spanish - ISO).
        KEYBOARD,
        //                                                  //Visible, pero que no es del teclado.
        VISIBLE_NONKEYBOARD,
        //                                                  //No visible que tiene una descripción.
        NONVISIBLE_WITH_DESCRIPTION,
        //                                                  //No visible que NO tiene descripción.
        NONVISIBLE_WITHOUT_DESCRIPTION,
    }

    //==================================================================================================================
}
/*END-TASK*/
