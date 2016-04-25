/*TASK SysexceptuserUserAbort*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

//                                                          //AUTHOR: Towa (GLG-Gerardo López).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: 1-Julio-2015.
//                                                          //PURPOSE:
//                                                          //Clase para manipular paths.

namespace TowaInfrastructure
{
    //==================================================================================================================
    public /*nonpartial*/class SysexceptuserUserAbort : Exception
    //                                                      //Excepción generada por SubAbort.
    {
        //--------------------------------------------------------------------------------------------------------------
        /*CONSTANTS*/

        //--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLES*/

        //--------------------------------------------------------------------------------------------------------------
        /*COMPUTED VARIABLES*/

        //--------------------------------------------------------------------------------------------------------------
        /*OBJECT CONSTRUCTORS*/

        //--------------------------------------------------------------------------------------------------------------
        internal SysexceptuserUserAbort(                    //Crea objeto DUMMY.
            //                                              //this.*[O], crea sin asignar nada. 
            )
        {
        }

        //--------------------------------------------------------------------------------------------------------------
        public SysexceptuserUserAbort(                      //Crea un objeto Exception.
            //                                              //this.*[O], asigna valores. 

            //                                              //Nombre, Path relativo o Full Path.
            String strMessage_I
            )
            : base(strMessage_I)
        {
        }

        //--------------------------------------------------------------------------------------------------------------
        //                                                  //MÉTODOS DE CONSULTA Y/O TRANSFORMACIÓN.

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/