/*TASK Boxing Boxing of primitives*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

//                                                          //AUTHOR: Towa (GLG-Gerardo López).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: February 20, 2016.
//                                                          //PURPOSE:
//                                                          //Base for all Bxxx.

namespace TowaInfrastructure
{
    //==================================================================================================================
    internal /*nonpartial*/ /*open*/ class Ots : BboxBaseBoxingAbtract
        //                                                  //Boxing ts.
    {
        //--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLE*/

        internal DateTime /*NSTD*/v/*END-NSTD*/;

        //--------------------------------------------------------------------------------------------------------------
        /*OBJECT CONSTRUCTORS*/

        //--------------------------------------------------------------------------------------------------------------
        internal Ots(                                      //Box a ts.
            //                                              //this.*[O], assing variable. 

            DateTime ts_I
            )
        {
            this.v = ts_I;
        }
    }
}
/*END-TASK*/