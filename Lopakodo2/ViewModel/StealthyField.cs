using System;
using Stealthy.Persistence;

namespace Stealthy.ViewModel
{
    public class StealthyField : ViewModelBase
    {
        private FieldElement _elem;
        public String Elem
        {
            set{
                switch (value) {
                    case "P":
                        _elem = FieldElement.FLOOR;
                        break;
                    case "F":
                        _elem = FieldElement.WALL;
                        break;
                    case "J":
                        _elem = FieldElement.PLAYER;
                        break;
                    case "O":
                        _elem = FieldElement.GUARD;
                        break;
                    case "K":
                        _elem = FieldElement.EXIT;
                        break;
                    default:
                        throw new Exception("Incorrect data for the field elements");
                }
                OnPropertyChanged();
            }
            get
            {
                switch (_elem) {
                    case FieldElement.FLOOR:
                        return "P";
                    case FieldElement.WALL:
                        return "F";
                    case FieldElement.PLAYER:
                        return "J";
                    case FieldElement.GUARD:
                        return "O";
                    case FieldElement.EXIT:
                        return "K";
                    default:
                        throw new Exception("Not valid FieldElement");
                }
            }
        }
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
    }
}
