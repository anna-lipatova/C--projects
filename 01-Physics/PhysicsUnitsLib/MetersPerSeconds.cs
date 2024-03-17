using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsUnitsLib
{
    public struct MeterPerSeconds
    {
        public double Value {  get; }

        public MeterPerSeconds(double value)
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
