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


        /// <summary>
        /// PlacedIn for canvas suppurts At with coordinates to locate
        /// </summary>
        /// <typeparam name="TControlChild"></typeparam>
        /// <param name="control"></param>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public static CanvasLocationSettings<TControlChild> PlacedIn<TControlChild>(this TControlChild control, Canvas canvas)
            where TControlChild : Control
        {
            return new CanvasLocationSettings<TControlChild>(canvas, control);
        }

    }

    /// <summary>
    /// struct specifying the location of a control on a canvas // OR CLASS?????
    /// At return TControl object located on a canvas
    /// </summary>
    /// <typeparam name="TControl"></typeparam>
    /// 
    //public class CanvasLocationSettings<TControl>
    public struct CanvasLocationSettings<TControl>
        where TControl: Control
    {
        private Canvas _canvas;
        private TControl _control;

        public CanvasLocationSettings(Canvas canvas, TControl control)
        {
            _canvas = canvas;
            _control = control;
        }

        /// <summary>
        /// set the location of the control on the canvas
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public TControl At(int x, int y)
        {
            _canvas.AddChild(_control, new Point { X = x, Y = y });
            return _control;
        }
    }
}
