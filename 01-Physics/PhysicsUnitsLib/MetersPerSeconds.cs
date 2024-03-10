using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsUnitsLib
{
    public struct MetersPerSeconds
    {
        public double Value {  get; }
        public MetersPerSeconds(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value} m/s";
        }

        /// <summary>
        /// operator * overload for int and MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static MetersPerSeconds operator *(MetersPerSeconds left, int right)
        {
            return new MetersPerSeconds(left.Value * right);
        }

        public static MetersPerSeconds operator *(int left, MetersPerSeconds right)
        {
            return new MetersPerSeconds(left * right.Value);
        }

        /// <summary>
        /// operator * overload for double and MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static MetersPerSeconds operator *(MetersPerSeconds left, double right)
        {
            return new MetersPerSeconds(left.Value * right);
        }

        public static MetersPerSeconds operator *(double left, MetersPerSeconds right)
        {
            return new MetersPerSeconds(left * right.Value);
        }

        /// <summary>
        /// operator + overload for int and MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static MetersPerSeconds operator +(MetersPerSeconds left, int right)
        {
            return new MetersPerSeconds(left.Value + right);
        }

        public static MetersPerSeconds operator +(int left, MetersPerSeconds right)
        {
            return new MetersPerSeconds(left + right.Value);
        }

        /// <summary>
        /// operator + overload for double and MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static MetersPerSeconds operator +(MetersPerSeconds left, double right)
        {
            return new MetersPerSeconds(left.Value + right);
        }

        public static MetersPerSeconds operator +(double left, MetersPerSeconds right)
        {
            return new MetersPerSeconds(left + right.Value);
        }

        /// <summary>
        /// operator - overload for int and MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static MetersPerSeconds operator -(MetersPerSeconds left, int right)
        {
            return new MetersPerSeconds(left.Value - right);
        }

        public static MetersPerSeconds operator -(int left, MetersPerSeconds right)
        {
            return new MetersPerSeconds(left - right.Value);
        }

        /// <summary>
        /// operator - overload for double and MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static MetersPerSeconds operator -(MetersPerSeconds left, double right)
        {
            return new MetersPerSeconds(left.Value - right);
        }

        public static MetersPerSeconds operator -(double left, MetersPerSeconds right)
        {
            return new MetersPerSeconds(left - right.Value);
        }


        /// <summary>
        /// operator * overload for MetersPerSeconds and Seconds
        /// return Meters
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Meters operator *(MetersPerSeconds left, Seconds right)
        {
            return new Meters(left.Value * right.Value);
        }

        /// <summary>
        /// operator / overload for Meters and MetersPerSeconds
        /// return Seconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Seconds operator /(Meters left, MetersPerSeconds right)
        {
            return new Seconds(left.Value / right.Value);
        }
    }
}
