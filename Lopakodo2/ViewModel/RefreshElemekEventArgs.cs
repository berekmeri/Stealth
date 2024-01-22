using System;

namespace Stealthy.ViewModel
{
    public class RefreshElemekEventArgs : EventArgs
    {
        private int _x1;
        private int _y1;
        private int _x2;
        private int _y2;
        public int X1 { get { return _x1; } }
        public int Y1 { get { return _y1; } }
        public int X2 { get { return _x2; } }
        public int Y2 { get { return _y2; } }

        public RefreshElemekEventArgs(int x1, int y1, int x2, int y2)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
        }
    }
}
