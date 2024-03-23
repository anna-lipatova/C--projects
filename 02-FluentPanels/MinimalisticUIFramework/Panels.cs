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

    public class StackPanel: Panel
    {
        public void AddChild(Control child) => _nestedChildren.Add(child);

        protected override string GetName => "StackPanel";

        //do not need GetChildPropertiesToString()
    }

    public class Canvas: Panel
    {
        private List<Point> _childPoints = new();

        protected override string GetName => "Canvas";

        protected override string GetChildPropertiesToString(int childIndex)
        {
            return $"at {_childPoints[childIndex].X}, {_childPoints[childIndex].Y}";
        }
        public void AddChild(Control child, Point point)
        {
            _nestedChildren.Add(child);
            _childPoints.Add(point);
        }
  
    }
}
