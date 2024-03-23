using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalisticUIFramework
{
    public static class ControlExtensions
    {
        /// <summary>
        /// Control Extension for StackPanel
        /// used when Image/Label/StackPanel/Canvas PLACED IN StackPanel
        /// </summary>
        /// <typeparam name="TControlChild"></typeparam>
        /// <param name="_control"></param>
        /// <param name="sP"></param>
        /// <returns></returns>
        public static TControlChild PlacedIn<TControlChild>(this TControlChild _control, StackPanel sP)
            where TControlChild : Control
        {
            sP.AddChild(_control);
            return _control;
        }

    }
}
