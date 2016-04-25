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
    internal /*nonpartial*/ /*open*/ class Ochar : BboxBaseBoxingAbtract
        //                                                  //Boxing char.
    {
        //--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLE*/

        internal char /*NSTD*/v/*END-NSTD*/;

        //--------------------------------------------------------------------------------------------------------------
        /*OBJECT CONSTRUCTORS*/

        //--------------------------------------------------------------------------------------------------------------
        internal Ochar(                                     //Box a char.
            //                                              //this.*[O], assing variable. 

            char char_I
            )
        {
            this.v = char_I;
        }
    }
}
/*END-TASK*/