using System;

namespace Stealthy.Model
{
    public class StealthyEventArgs : EventArgs
    {
        private Boolean _isWon;
        public Boolean IsWon { get { return _isWon; } }
        public StealthyEventArgs(Boolean isWon)
        {
            _isWon = isWon;
        }
    }
}
