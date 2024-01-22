using System;
using Stealthy.Persistence;

namespace Stealthy.Model
{
    public enum GameStatus { END, PAUSE, START }
    // Game table sizes
    public enum GameTable{ SMALL, MEDIUM, LARGE }
    public class StealthyGameModel
    {
        private IStealthyDataAcces? _dataAccess;
        private StealthyTable _table;
        private GameStatus _status;
        public void SetStatus(GameStatus a) { _status = a; }
        public GameTable gameTable;
        private String smallP = "..\\net6.0-windows\\Maps\\small.txt";
        private String mediumP = "..\\net6.0-windows\\Maps\\medium.txt";
        private String largeP = "..\\net6.0-windows\\Maps\\large.txt";

        #region Properties
        public StealthyTable GetGameTable { get { return _table; } }
        public GameStatus GetGameStatus { get { return _status; } }
        #endregion

        #region Constructor
        public StealthyGameModel(IStealthyDataAcces dataAccess)
        {
            _dataAccess = dataAccess;
            _status = GameStatus.START;
            gameTable = GameTable.LARGE;
            LoadGame(largeP);
        }
        #endregion
        
        #region Events
        public event EventHandler<StealthyEventArgs>? GameOver;
        #endregion

        #region Event methods
        internal void OnGameOver(Boolean isWon)
        {
            GameOver?.Invoke(this, new StealthyEventArgs(isWon));
        }
        #endregion

        #region Public game methods
        /// New game start.
        public void NewGame()
        {
            switch (gameTable)
            {
                case GameTable.SMALL:
                    LoadGame(smallP);

                    break;
                case GameTable.MEDIUM:
                    LoadGame(mediumP);
                    break;
                case GameTable.LARGE:
                    LoadGame(largeP);
                    break;
            }
            _status = GameStatus.START;
        }
        public void LoadGame(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            _table = _dataAccess.Load(path);
        }
        #endregion
    }
}
