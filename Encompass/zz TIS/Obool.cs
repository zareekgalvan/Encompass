/*TASK Boxing Boxing of primitives*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

//                                                          //AUTHOR: Towa (GLG-Gerardo López).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: February 19, 2016.
//                                                          //PURPOSE:
//                                                          //Base for all Bxxx.

namespace TowaInfrastructure
{
    //==================================================================================================================
    internal /*nonpartial*/ /*open*/ class Obool : BboxBaseBoxingAbtract
        //                                                  //Boxing bool.
    {
        //--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLE*/

        internal bool /*NSTD*/v/*END-NSTD*/;

        //--------------------------------------------------------------------------------------------------------------
        /*OBJECT CONSTRUCTORS*/

        //--------------------------------------------------------------------------------------------------------------
        internal Obool(                                     //Box a bool.
            //                                              //this.*[O], assing variable. 

            bool bool_I
            )
        {
            this.v = bool_I;
        }
    }
}
/*END-TASK*/