using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsUnitsLib {

    public struct Meters
    {
        public double Value { get; }

        public Meters(double value)
        {
            Value = value;
        }


        /// <summary>
        /// operator overload for sum
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Meters operator +(Meters left, Meters right) => new Meters(left.Value + right.Value);

        //no need in these overloaded operators fro every case of parameter's type
        //because there are implicit overload operators for int and double

        /// <summary>
        /// operator overload for difference
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Meters operator -(Meters left, Meters right) => new Meters(left.Value - right.Value);

        /// <summary>
        /// operator > overload for Meters, double, Int
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        /// 

        public static bool operator >(Meters left, Meters right) => left.Value > right.Value;

        public static bool operator <(Meters left, Meters right) => (left.Value < right.Value);


        //no need in these overloaded operators fro every case of parameter's type
        //because there are implicit overload operators for int and double

        /// <summary>
        /// method overload Meters => ToString 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Value.ToString() + "m";

        /// <summary>
        /// operator / overload for Meters and Seconds
        /// returns MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static MeterPerSeconds operator /(Meters left, Seconds right) => new MeterPerSeconds(left.Value / right.Value);


        /// <summary>
        /// implicit overload int into Meters
        /// </summary>
        /// <param name="m"></param>
        public static implicit operator Meters(int m) => new Meters(m);
       
        /// <summary>
        /// implicit overload double into Meters
        /// </summary>
        /// <param name="m"></param>
        public static implicit operator Meters(double m) => new Meters(m);
    }
}
