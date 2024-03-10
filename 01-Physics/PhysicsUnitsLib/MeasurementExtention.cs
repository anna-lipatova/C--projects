using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsUnitsLib
{
    public static class MeasurementExtention
    {
        /// <summary>
        /// return new Meters-type instance with double value
        /// example:
        /// var m = 1.2.Meters() return new Meters-type instance with 1.2 value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Meters Meters(this double value)
        {
            return new Meters(value);
        }

        /// <summary>
        /// return new Meters-type instance with double( <= int) value
        /// example:
        /// var m = 1.Meters() return new Meters-type instance with 1.0 value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Meters Meters(this int value)
        {
            return new Meters(value);
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

        /// <summary>
        /// return new MetersPerSeconds-type instance with double value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MeterPerSeconds MeterPerSeconds(this double value)
        {
            return new MeterPerSeconds(value);
        }

        /// <summary>
        /// return new MetersPerSeconds-type instance with double ( <= int) value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MeterPerSeconds MeterPerSeconds(this int value)
        {
            return new MeterPerSeconds(value);
        }
    }
}
;