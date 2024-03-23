using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalisticUIFramework
{
    public abstract class Panel: Control
    {
        //ATTENTION not private => but protected:
        //panel-children will also have this collection
        protected List<Control> _nestedChildren { get; } = new();

        protected abstract string GetName { get; }
        protected virtual string GetChildPropertiesToString(int childIndex)
        {
            return "";
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.GetName} {{");
            for ( int i = 0; i < _nestedChildren.Count; i++ )
            {
                //should pass index to GetChildPropertiesToString
                //List<Point> has corresponding info on the childIndex
                sb.AppendLine($"{_nestedChildren[i]} {GetChildPropertiesToString(i)} ");
            }
            sb.Append("}");
            return sb.ToString();
        }

        
    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
