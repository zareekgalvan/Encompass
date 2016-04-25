/*TASK BclassmutabilityEnum*/
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

//                                                          //AUTHOR: Towa (GLG-Gerardo López).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: June 24, 2015.
//                                                          //PURPOSE:
//                                                          //Base for all classes.

namespace TowaInfrastructure
{
    //==================================================================================================================
    public enum BclassmutabilityEnum                        //Posibles tipos de objeto.
    {
        //##############################################################################################################
        Z_ERROR_NOT_DEFINED,
        //##############################################################################################################

        //                                                  //Objetos en los cuales (en ninguno de sus niveles de
        //                                                  //      abstracción) tiene variables de instancia mutables
        //                                                  //      (una ves construidos estos objetos, NUNCA pueden se
        //                                                  //      alterados).
        //                                                  //Nótese que su contenido puede incluir objetos mutables
        //                                                  //      (arreglos, listas, objetos MUTABLE, u objetos OPEN)
        //                                                  //      sin embargo, si están usados en un objeto INMUTABLE
        //                                                  //      QEnabler se asegura que núnca cambian.
        INMUTABLE,
        //                                                  //Objetos que tiene variables de instancia que pueden
        //                                                  //      cambiar su valor durante la vida del objeto.
        MUTABLE,
        //                                                  //Objetos similares a arreglos, List, Queue, Stack, etc.
        OPEN,
    }

    //==================================================================================================================
}
/*END-TASK*/