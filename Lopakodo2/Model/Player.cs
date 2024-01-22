using Stealthy.Persistence;
using System;
using Stealthy.ViewModel;
using static Stealthy.Model.Guard;

namespace Stealthy.Model
{
    public class Player
    {
        private StealthyGameModel _gameModel;
        private Int32 _playerX;
        private Int32 _playerY;
        public Int32 PlayerX() { return _playerX; }
        public Int32 PlayerY() { return _playerY; }

        #region Constructor end Reset
        public Player(StealthyGameModel model, Int32 x, Int32 y)
        {
            _gameModel = model;
            _playerX = x;
            _playerY = y;
        }
        public void PlayerReset(Int32 x, Int32 y)
        {
            _playerX = x;
            _playerY = y;
        }
        #endregion

        #region Event
        public event EventHandler<RefreshElemekEventArgs>? MovedCharacter;
        #endregion

        #region Player new field
        public Boolean PlayerGo(Int32 i, Int32 j)
        {
            try
            {
                if (_gameModel.GetGameTable.GetField(i, j) == FieldElement.EXIT)
                {
                    _gameModel.GetGameTable.FieldValue(i, j, FieldElement.PLAYER);
                    _gameModel.GetGameTable.FieldValue(_playerX, _playerY, FieldElement.FLOOR);
                    _playerX = i;
                    _playerY = j;
                    _gameModel.OnGameOver(true);
                    return true;
                }
                if (_gameModel.GetGameTable.GetField(i, j) == FieldElement.FLOOR)
                {

                    if (Examination(i, j))
                    {
                        Int32 x1 = _playerX;
                        Int32 y1 = _playerY;
                        _gameModel.GetGameTable.FieldValue(i, j, FieldElement.PLAYER);
                        _gameModel.GetGameTable.FieldValue(_playerX, _playerY, FieldElement.FLOOR);
                        _playerX = i;
                        _playerY = j;
                        MovedCharacter?.Invoke(this, new RefreshElemekEventArgs(x1, y1, _playerX, _playerY));

                        return true;
                    }
                }
                return false;
            }catch(IndexOutOfRangeException )
            {
                return false;
            }
        }
        public Boolean Examination(Int32 x, Int32 y)
        {
            for(Int32 i = -1; i < 2; ++i)
            {
                for(Int32 j = -1; j < 2; ++j)
                {
                    if(_gameModel.GetGameTable.GetField(x + i, y + j) == FieldElement.GUARD)
                    {
                        _gameModel.OnGameOver(false);
                        return false;
                    }
                }
            }

            try
            {
                if(_gameModel.GetGameTable.GetField(x, y + 2) == FieldElement.GUARD && _gameModel.GetGameTable.GetField(x, y + 1) == FieldElement.FLOOR)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }catch(IndexOutOfRangeException ) { }

            try
            {
                if (_gameModel.GetGameTable.GetField(x, y + 2) == FieldElement.GUARD && _gameModel.GetGameTable.GetField(x, y + 1) == FieldElement.FLOOR)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }
            catch (IndexOutOfRangeException ) { }
            try
            {
                if (_gameModel.GetGameTable.GetField(x, y - 2) == FieldElement.GUARD && _gameModel.GetGameTable.GetField(x, y - 1) == FieldElement.FLOOR)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (_gameModel.GetGameTable.GetField(x + 2, y) == FieldElement.GUARD && _gameModel.GetGameTable.GetField(x + 1, y) == FieldElement.FLOOR)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                if (_gameModel.GetGameTable.GetField(x - 2, y) == FieldElement.GUARD && _gameModel.GetGameTable.GetField(x - 1, y) == FieldElement.FLOOR)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }
            catch (IndexOutOfRangeException) { }
            
            if(_gameModel.GetGameTable.GetField(x + 1, y + 1) == FieldElement.FLOOR)
            {
                if(_gameModel.GetGameTable.GetField(x + 2, y + 1) == FieldElement.GUARD || _gameModel.GetGameTable.GetField(x + 2, y + 2) == FieldElement.GUARD || _gameModel.GetGameTable.GetField(x + 1, y + 2) == FieldElement.GUARD)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }

            if (_gameModel.GetGameTable.GetField(x - 1, y - 1) == FieldElement.FLOOR)
            {
                if (_gameModel.GetGameTable.GetField(x - 2, y - 1) == FieldElement.GUARD || _gameModel.GetGameTable.GetField(x - 2, y - 2) == FieldElement.GUARD || _gameModel.GetGameTable.GetField(x - 1, y - 2) == FieldElement.GUARD)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }

            if (_gameModel.GetGameTable.GetField(x - 1, y + 1) == FieldElement.FLOOR)
            {
                if (_gameModel.GetGameTable.GetField(x - 2, y + 1) == FieldElement.GUARD || _gameModel.GetGameTable.GetField(x - 2, y + 2) == FieldElement.GUARD || _gameModel.GetGameTable.GetField(x - 1, y + 2) == FieldElement.GUARD)
                {
                    _gameModel.OnGameOver(false);
                    return false;
                }
            }

            if (_gameModel.GetGameTable.GetField(x + 1, y - 1) == FieldElement.FLOOR)
            {
                if (_gameModel.GetGameTable.GetField(x + 2, y - 1) == FieldElement.GUARD || _gameModel.GetGameTable.GetField(x + 2, y - 2) == FieldElement.GUARD || _gameModel.GetGameTable.GetField(x + 1, y - 2) == FieldElement.GUARD)
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