using System;

namespace Stealthy.Persistence 
{ 
    public enum FieldElement
    {
        PLAYER,
        GUARD,
        FLOOR,
        WALL,
        EXIT
    }
    public class StealthyTable
    {
        #region Fields
        private Int32 _size;
        public Int32 GetSize { get { return _size; }}
        private FieldElement[,] _table;
        #endregion

        #region Constructors
        public StealthyTable(Int32 size)
        {
            if(size < 0)
            {
                throw new ArgumentOutOfRangeException("The table size is less than 0.");
            }
            _size = size;
            _table = new FieldElement[_size, _size];
        }
        #endregion

        #region Public methods
        public void FieldValue(Int32 x, Int32 y, FieldElement p)
        {
            if (x < 0 || x >= _size || y < 0 || y >= _size)
            {
                throw new IndexOutOfRangeException();
            }
            _table[x, y] = p;
        }
        public FieldElement GetField(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _size || y < 0 || y >= _size)
            {
                throw new IndexOutOfRangeException();
            }
            return _table[x, y];
        }
        public String GetPalyaElemString(Int32 x, Int32 y)
        {
            switch(GetField(x, y))
            {
                case FieldElement.WALL:
                    return "F";
                case FieldElement.FLOOR:
                    return "P";
                case FieldElement.EXIT:
                    return "K";
                case FieldElement.GUARD:
                    return "O";
                case FieldElement.PLAYER:
                    return "J";
                default:
                    throw new Exception();
            }
        }
        #endregion
    }
}
