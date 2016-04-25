/*TASK SyspathtypeEnum*/
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
    public enum SyspathtypeEnum                             //Indica tipo de referencia.
    {
        //##############################################################################################################
        Z_ERROR_NOT_DEFINED,
        //##############################################################################################################

        //                                                  //No existe el directorio o archivo.
        NONE,

        DIRECTORY,
        FILE,
    }

    //==================================================================================================================

}
/*END-TASK*/