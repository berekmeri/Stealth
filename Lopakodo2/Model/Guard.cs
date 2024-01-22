using Stealthy.Persistence;
using System;
using System.Collections.Generic;
using Stealthy.ViewModel;

namespace Stealthy.Model
{
    public enum Direction
    {
        RIGHT, 
        LEFT, 
        UP, 
        DOWN, 
        STUCK
    }
    public class Guard
    {
        private StealthyGameModel _gameModel;
        private Int32 _guardX;
        private Int32 _guardY;
        private System.Timers.Timer _timer = null!;
        private Direction _direction;

        #region Constructor end Reset
        public Guard(StealthyGameModel model, Int32 x, Int32 y)
        {
            _gameModel = model;
            _guardX = x;
            _guardY = y;
            _timer = new System.Timers.Timer(500);


            _timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Tick);
            _direction = Direction.RIGHT;
        }
        public void GuradReset(Int32 x, Int32 y)
        {
            _guardX = x;
            _guardY = y;
            _direction = Direction.RIGHT;
        }
        #endregion

        #region Event
        public event EventHandler<RefreshElemekEventArgs>? MovedCharacter;
        #endregion

        #region Timer
        //public System.Timers.Timer Timer { get { return _timer; } }
        public void StopTime()
        {
            _timer.Stop();
        }
        public void StartTime()
        {
            _timer.Start();
            
        }
        private void Timer_Tick(Object? sender, EventArgs e)
        {
            GuardGo();
        }
        #endregion

        public List<(Int32, Int32, Direction)> Guard_Direction()
        {
            List<(Int32, Int32, Direction)> directions = new List<(Int32, Int32, Direction)>();

            if(_gameModel.GetGameTable.GetField(_guardX - 1, _guardY) == FieldElement.FLOOR){
                directions.Add((_guardX - 1, _guardY, Direction.UP));
            }
            if (_gameModel.GetGameTable.GetField(_guardX, _guardY - 1) == FieldElement.FLOOR)
            {
                directions.Add((_guardX, _guardY - 1, Direction.LEFT));
            }
            if (_gameModel.GetGameTable.GetField(_guardX + 1, _guardY) == FieldElement.FLOOR)
            {
                directions.Add((_guardX + 1, _guardY, Direction.DOWN));
            }
            if (_gameModel.GetGameTable.GetField(_guardX , _guardY + 1) == FieldElement.FLOOR)
            {
                directions.Add((_guardX, _guardY + 1, Direction.RIGHT));
            }
            return directions;
        }

        #region Guard new field
        public Boolean GuardGo()
        {
            Int32 i = 0;
            Int32 j = 0;
            if (Direction.UP == _direction && _gameModel.GetGameTable.GetField(_guardX - 1, _guardY) != FieldElement.FLOOR) {
                _direction = Direction.STUCK;
            }
            else if (Direction.DOWN == _direction && _gameModel.GetGameTable.GetField(_guardX + 1, _guardY) != FieldElement.FLOOR)
            {
                _direction = Direction.STUCK;
            }
            else if (Direction.RIGHT == _direction && _gameModel.GetGameTable.GetField(_guardX, _guardY + 1) != FieldElement.FLOOR)
            {
                _direction = Direction.STUCK;
            }
            else if (Direction.LEFT == _direction && _gameModel.GetGameTable.GetField(_guardX, _guardY - 1) != FieldElement.FLOOR)
            {
                _direction = Direction.STUCK;
            }

            switch (_direction)
            {
                case Direction.RIGHT:
                    i = _guardX;
                    j = _guardY + 1;
                    break;
                case Direction.LEFT:
                    i = _guardX;
                    j = _guardY - 1;
                    break;
                case Direction.UP:
                    i = _guardX - 1;
                    j = _guardY;
                    break;
                case Direction.DOWN:
                    i = _guardX + 1;
                    j = _guardY;
                    break;
                case Direction.STUCK:
                    List<(Int32, Int32, Direction)> lista = Guard_Direction();

                    Random rnd = new Random();
                    int num = rnd.Next(0, lista.Count);

                    i = lista[num].Item1;
                    j = lista[num].Item2;
                    _direction = lista[num].Item3;

                    break;
            }
            try
            {
                if (_gameModel.GetGameTable.GetField(i,j) == FieldElement.FLOOR)
                {  
                    if (Examination(i, j))
                    {
                        Int32 x1 = _guardX;
                        Int32 y1 = _guardY;
                        _gameModel.GetGameTable.FieldValue(i, j, FieldElement.GUARD);
                        _gameModel.GetGameTable.FieldValue(_guardX, _guardY, FieldElement.FLOOR);
                        _guardX = i;
                        _guardY = j;
                        MovedCharacter?.Invoke(this, new RefreshElemekEventArgs(x1, y1, _guardX, _guardY));
                        return true;
                    }
                }
                return false;
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
                //return false;
            }
        }

        public Boolean Examination(Int32 x, Int32 y)
        {
            for (Int32 i = -1; i < 2; ++i)
            {
                for (Int32 j = -1; j < 2; ++j)
                {
                    
                    if (_gameModel.GetGameTable.GetField(x + i, y + j) == FieldElement.PLAYER)
                    {
                        _gameModel.OnGameOver(false);
                        return false;
                    }
                }
            }
            
            try
            {
                if (_gameModel.GetGameTable.GetField(x, y + 2) == FieldElement.PLAYER && _gameModel.GetGameTable.GetField(x, y + 1) == FieldElement.FLOOR)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }
            catch (IndexOutOfRangeException ) { }

            try
            {
                if (_gameModel.GetGameTable.GetField(x, y + 2) == FieldElement.PLAYER && _gameModel.GetGameTable.GetField(x, y + 1) == FieldElement.FLOOR)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }
            catch (IndexOutOfRangeException ) { }
            try
            {
                if (_gameModel.GetGameTable.GetField(x, y - 2) == FieldElement.PLAYER && _gameModel.GetGameTable.GetField(x, y - 1) == FieldElement.FLOOR)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (_gameModel.GetGameTable.GetField(x + 2, y) == FieldElement.PLAYER && _gameModel.GetGameTable.GetField(x + 1, y) == FieldElement.FLOOR)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }
            catch (IndexOutOfRangeException ) { }
            try
            {
                if (_gameModel.GetGameTable.GetField(x - 2, y) == FieldElement.PLAYER && _gameModel.GetGameTable.GetField(x - 1, y) == FieldElement.FLOOR)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }
            catch (IndexOutOfRangeException ) { }

            if (_gameModel.GetGameTable.GetField(x + 1, y + 1) == FieldElement.FLOOR)
            {
                if (_gameModel.GetGameTable.GetField(x + 2, y + 1) == FieldElement.PLAYER || _gameModel.GetGameTable.GetField(x + 2, y + 2) == FieldElement.PLAYER || _gameModel.GetGameTable.GetField(x + 1, y + 2) == FieldElement.PLAYER)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }

            if (_gameModel.GetGameTable.GetField(x - 1, y - 1) == FieldElement.FLOOR)
            {
                if (_gameModel.GetGameTable.GetField(x - 2, y - 1) == FieldElement.PLAYER || _gameModel.GetGameTable.GetField(x - 2, y - 2) == FieldElement.PLAYER || _gameModel.GetGameTable.GetField(x - 1, y - 2) == FieldElement.PLAYER)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }

            if (_gameModel.GetGameTable.GetField(x - 1, y + 1) == FieldElement.FLOOR)
            {
                if (_gameModel.GetGameTable.GetField(x - 2, y + 1) == FieldElement.PLAYER || _gameModel.GetGameTable.GetField(x - 2, y + 2) == FieldElement.PLAYER || _gameModel.GetGameTable.GetField(x - 1, y + 2) == FieldElement.PLAYER)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }

            if (_gameModel.GetGameTable.GetField(x + 1, y - 1) == FieldElement.FLOOR)
            {
                if (_gameModel.GetGameTable.GetField(x + 2, y - 1) == FieldElement.PLAYER || _gameModel.GetGameTable.GetField(x + 2, y - 2) == FieldElement.PLAYER || _gameModel.GetGameTable.GetField(x + 1, y - 2) == FieldElement.PLAYER)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}