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
    internal /*nonpartial*/ /*open*/ class Oint : BboxBaseBoxingAbtract
        //                                                  //Boxing int.
    {
        //--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLE*/

        internal int /*NSTD*/v/*END-NSTD*/;

        //--------------------------------------------------------------------------------------------------------------
        /*OBJECT CONSTRUCTORS*/

        //--------------------------------------------------------------------------------------------------------------
        internal Oint(                                      //Box a int.
            //                                              //this.*[O], assing variable. 

            int int_I
            )
        {
            this.v = int_I;
        }
    }
}
/*END-TASK*/