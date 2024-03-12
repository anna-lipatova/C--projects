using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace PhysicsUnitsLib
{
    public struct MeterPerSeconds
    {
        public double Value {  get; }

        //with default value = 0.0
        public MeterPerSeconds(double value = 0.0)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value} m/s";
        }


        /// <summary>
        /// operator * overload for MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static MeterPerSeconds operator *(MeterPerSeconds left, MeterPerSeconds right)
        {
            return new MeterPerSeconds(left.Value * right.Value);
        }


        //no need to use dut to implicit operator int => MeterPerSeconds and double => MetrPerSeconds
        /// <summary>
        /// operator * overload for int and MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        //public static MeterPerSeconds operator *(MeterPerSeconds left, int right)
        //{
        //    return new MeterPerSeconds(left.Value * right);
        //}

        //public static MeterPerSeconds operator *(int left, MeterPerSeconds right)
        //{
        //    return new MeterPerSeconds(left * right.Value);
        //}

        /// <summary>
        /// operator * overload for double and MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        //public static MeterPerSeconds operator *(MeterPerSeconds left, double right)
        //{
        //    return new MeterPerSeconds(left.Value * right);
        //}

        //public static MeterPerSeconds operator *(double left, MeterPerSeconds right)
        //{
        //    return new MeterPerSeconds(left * right.Value);
        //}




        /// <summary>
        /// operator + overload for MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static MeterPerSeconds operator +(MeterPerSeconds left, MeterPerSeconds right)
        {
            return new MeterPerSeconds(left.Value + right.Value);
        }


        /// <summary>
        /// operator + overload for int and MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        //public static MeterPerSeconds operator +(MeterPerSeconds left, int right)
        //{
        //    return new MeterPerSeconds(left.Value + right);
        //}

        //public static MeterPerSeconds operator +(int left, MeterPerSeconds right)
        //{
        //    return new MeterPerSeconds(left + right.Value);
        //}

        /// <summary>
        /// operator + overload for double and MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        //public static MeterPerSeconds operator +(MeterPerSeconds left, double right)
        //{
        //    return new MeterPerSeconds(left.Value + right);
        //}

        //public static MeterPerSeconds operator +(double left, MeterPerSeconds right)
        //{
        //    return new MeterPerSeconds(left + right.Value);
        //}

        /// <summary>
        /// operator - overload for MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static MeterPerSeconds operator -(MeterPerSeconds left, MeterPerSeconds right)
        {
            return new MeterPerSeconds(left.Value - right.Value);
        }

        /// <summary>
        /// operator - overload for int and MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        //public static MeterPerSeconds operator -(MeterPerSeconds left, int right)
        //{
        //    return new MeterPerSeconds(left.Value - right);
        //}

        //public static MeterPerSeconds operator -(int left, MeterPerSeconds right)
        //{
        //    return new MeterPerSeconds(left - right.Value);
        //}

        /// <summary>
        /// operator - overload for double and MetersPerSeconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        //public static MeterPerSeconds operator -(MeterPerSeconds left, double right)
        //{
        //    return new MeterPerSeconds(left.Value - right);
        //}

        //public static MeterPerSeconds operator -(double left, MeterPerSeconds right)
        //{
        //    return new MeterPerSeconds(left - right.Value);
        //}


        /// <summary>
        /// operator * overload for MetersPerSeconds and Seconds
        /// return Meters
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Meters operator *(MeterPerSeconds left, Seconds right)
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
        public static Seconds operator /(Meters left, MeterPerSeconds right)
        {
            return new Seconds(left.Value / right.Value);
        }

        /// <summary>
        /// implicit operator double into MeterPerSeconds
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator MeterPerSeconds(double value)
        {
            return new MeterPerSeconds(value);
        }

        /// <summary>
        /// implicit operator int into MeterPerSeconds
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator MeterPerSeconds(int  value)
        {
            return new MeterPerSeconds(value);
        }
    }
}
