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
    internal /*nonpartial*/ /*open*/ class Onum : BboxBaseBoxingAbtract
        //                                                  //Boxing num.
    {
        //--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLE*/

        internal double /*NSTD*/v/*END-NSTD*/;

        //--------------------------------------------------------------------------------------------------------------
        /*OBJECT CONSTRUCTORS*/

        //--------------------------------------------------------------------------------------------------------------
        internal Onum(                                      //Box a num.
            //                                              //this.*[O], assing variable. 

            double num_I
            )
        {
            this.v = num_I;
        }
    }
}
/*END-TASK*/