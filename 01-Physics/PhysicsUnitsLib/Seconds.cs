using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsUnitsLib
{
    public struct Seconds
    {
        public double Value {  get; }
        public Seconds(double value) {  Value = value; }

        public static Seconds operator +(Seconds left, Seconds right) => new Seconds(left.Value + right.Value);
        public static Seconds operator -(Seconds left, Seconds right) => new Seconds(left.Value - right.Value);
        public override string ToString()
        {
            return $"{Value} seconds";
        }
        
    }
}
