using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsUnitsLib
{
    public static class MeasurementExtention
    {

        /// <summary>
        /// return new Seconds-type instance with double value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Seconds Seconds(this double value)
        {
            return new Seconds(value);
        }

        /// <summary>
        /// return new Seconds-type instance with double ( <= int) value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Seconds Seconds(this  int value)
        { 
            return new Seconds(value); 
        }

        /// <summary>
        /// return new Seconds-type instance with double value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Seconds Seconds(this double value)
        {
            return new Seconds(value);
        }

        /// <summary>
        /// return new Seconds-type instance with double ( <= int) value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Seconds Seconds(this int value)
        {
            return new Seconds(value);
        }

    }
}
