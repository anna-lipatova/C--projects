using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsUnitsLib {

    public struct Meters
    {
        public double Value { get; }

        //with default value = 0.0
        public Meters(double value = 0.0)
        {
            Value = value;
        }


        /// <summary>
        /// operator overload for sum
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Meters operator +(Meters left, Meters right)
        {
            return new Meters(left.Value + right.Value);
        }

        //no need in these overloaded operators fro every case of parameter's type
        //because there are implicit overload operators for int and double
        //public static Meters operator +(Meters left, double right)
        //{
        //    return new Meters(left.Value + right);
        //}

        //public static Meters operator +(double left, Meters right)
        //{
        //    return new Meters (left + right.Value);
        //}

        //public static Meters operator +(Meters left, int right)
        //{
        //    return new Meters(left.Value + right);
        //}

        //public static Meters operator +(int left, Meters right)
        //{
        //    return new Meters(left + right.Value);
        //}

        /// <summary>
        /// operator overload for difference
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Meters operator -(Meters left, Meters right)
        {
            return new Meters(left.Value - right.Value);
        }

        /// <summary>
        /// operator > overload for Meters, double, Int
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        /// 

        public static bool operator >(Meters left, Meters right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <(Meters left, Meters right)
        {
            return (left.Value < right.Value);
        }

        //no need in these overloaded operators fro every case of parameter's type
        //because there are implicit overload operators for int and double
        //public static bool operator >(Meters left, double right)
        //{
        //    return left.Value > right;
        //}

        //public static bool operator >(double left, Meters right)
        //{
        //    return (left > right.Value);
        //}

        //public static bool operator <(Meters left, double right)
        //{
        //    return (left.Value < right);
        //}

        //public static bool operator <(double left, Meters right)
        //{
        //    return (left < right.Value);
        //}

        /// <summary>
        /// method overload Meters => ToString 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Value} meters";
        }


        /// <summary>
        /// operator / overload for Meters and Seconds
        /// returns MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static MeterPerSeconds operator /(Meters left, Seconds right)
        {
            return new MeterPerSeconds(left.Value / right.Value);
        }


        /// <summary>
        /// implicit overload int into Meters
        /// </summary>
        /// <param name="m"></param>
        public static implicit operator Meters(int m)
        {
            return new Meters(m);
        }
       
        /// <summary>
        /// implicit overload double into Meters
        /// </summary>
        /// <param name="m"></param>
        public static implicit operator Meters(double m)
        {
            return new Meters(m);
        }
    }
}
