/*TASK SyspathwhereEnum*/
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
    public enum SyspathwhereEnum                            //Indica dónde se encuenta el directorio o file referido en
    //                                                      //      path.
    {
        //##############################################################################################################
        Z_ERROR_NOT_DEFINED,
        //##############################################################################################################

        //                                                  //Esta en el mismo equipo (c:...).
        LOCAL,
        //                                                  //Esta en la red (\\...).
        NETWORK,
    }

    //==================================================================================================================

}
/*END-TASK*/