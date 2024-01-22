using System;
using System.Collections.ObjectModel;
using Stealthy.Model;
using Stealthy.Persistence;

namespace Stealthy.ViewModel
{
    public class StealthyViewModel : ViewModelBase
    {
        #region Fields
        private StealthyGameModel _model;
        private Player _player;
        private Guard _guard;
        #endregion
        public Player Player { get { return _player; } }
        public Guard Guard { get { return _guard; } } 
        public ObservableCollection<StealthyField> Fields { get; set; }

        #region Consturctor
        public StealthyViewModel(StealthyGameModel model)
        {
            _model = model;
            _model.GameOver += new EventHandler<StealthyEventArgs>(Model_GameOver);

            // handling commands
            NewGameSmallCommand = new DelegateCommand(param => OnNewGameSmall());
            NewGameMediumCommand = new DelegateCommand(param => OnNewGameMedium());
            NewGameLargeCommand = new DelegateCommand(param => OnNewGameLarge());
            ExitCommand = new DelegateCommand(param => OnExitGame());
            PauseCommand = new DelegateCommand(param => OnPauseGame());
            StartCommand = new DelegateCommand(param => OnStartGame());
            UpMoveCommand = new DelegateCommand(param => OnUpMove());
            DownMoveCommand = new DelegateCommand(param => OnDownMove());
            RightMoveCommand = new DelegateCommand(param => OnRightMove());
            LeftMoveCommand = new DelegateCommand(param => OnLeftMove());

            GenerateTable();
            RefreshTable();
        }
        #endregion

        #region DelegateCommand
        public DelegateCommand NewGameSmallCommand { get; private set; }
        public DelegateCommand NewGameMediumCommand { get; private set; }
        public DelegateCommand NewGameLargeCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand PauseCommand { get; private set; }
        public DelegateCommand StartCommand { get; private set; }
        public DelegateCommand UpMoveCommand { get;private set; }
        public DelegateCommand DownMoveCommand { get;private set; }
        public DelegateCommand RightMoveCommand { get;private set; }
        public DelegateCommand LeftMoveCommand { get;private set; }
        public int TableSize { get { return _model.GetGameTable.GetSize; } }
        #endregion

        #region Events
        public event EventHandler? NewGameSmall;
        public event EventHandler? NewGameMedium;
        public event EventHandler? NewGameLarge;
        public event EventHandler? ExitGame;
        public event EventHandler? PauseGame;
        public event EventHandler? StartGame;
        public event EventHandler? UpMove;
        public event EventHandler? DownMove;
        public event EventHandler? RightMove;
        public event EventHandler? LeftMove;
        #endregion

        #region Table size
        public Boolean IsSmall
         {
             get { return _model.gameTable == GameTable.SMALL; }
             set
            {
                 if (_model.gameTable == GameTable.SMALL)
                     return;

                 _model.gameTable = GameTable.SMALL;
                 OnPropertyChanged(nameof(IsSmall));
                 OnPropertyChanged(nameof(IsMedium));
                 OnPropertyChanged(nameof(IsLarge));
             }
         }

        public Boolean IsMedium
        {
            get { return _model.gameTable == GameTable.MEDIUM; }
            set
            {
                if (_model.gameTable == GameTable.MEDIUM)
                    return;

                _model.gameTable = GameTable.MEDIUM;
                OnPropertyChanged(nameof(IsSmall));
                OnPropertyChanged(nameof(IsMedium));
                OnPropertyChanged(nameof(IsLarge));
            }
        }

        public Boolean IsLarge
        {
            get { return _model.gameTable == GameTable.LARGE; }
            set
            {
                if (_model.gameTable == GameTable.LARGE)
                    return;

                _model.gameTable = GameTable.LARGE;
                OnPropertyChanged(nameof(IsSmall));
                OnPropertyChanged(nameof(IsMedium));
                OnPropertyChanged(nameof(IsLarge));
            }
        }
        #endregion

        #region Table operations
        public void RefreshTable()
        {
            foreach (StealthyField field in Fields)
            {
                field.Elem = _model.GetGameTable.GetPalyaElemString(field.X,field.Y);
            }
            OnPropertyChanged(nameof(Fields));
        }
        public void GenerateTable()
        {
            Fields = new ObservableCollection<StealthyField>();
            for (Int32 i = 0; i < _model.GetGameTable.GetSize; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < _model.GetGameTable.GetSize; j++)
                {
                    Fields.Add(new StealthyField
                    {
                        Elem = _model.GetGameTable.GetPalyaElemString(i, j),
                        X = i,
                        Y = j
                    }
                    );
                    if (_model.GetGameTable.GetField(i, j) == FieldElement.PLAYER)
                    {
                        GeneratePlayer(i, j);
                    }else if(_model.GetGameTable.GetField(i, j) == FieldElement.GUARD)
                    {
                        GenerateGuard(i, j);
                    }
                }
            }
        }
        #endregion

        #region Generate Person
        private void GeneratePlayer(int i, int j)
        {
            if(_player != null)
            {
                _player.PlayerReset(i, j);
            }
            else
            {
            _player = new Player(_model, i, j);
            _player.MovedCharacter += MovedPlayer;
            }
        }
        private void GenerateGuard(int i, int j)
        {
            if (_guard != null)
            {
                _guard.GuradReset(i, j);
            }
            else
            {
                _guard = new Guard(_model, i, j);
                _guard.MovedCharacter += MovedGuard;
            }
        }
        #endregion

        #region Step refresh
        public void RefreshElemets(int x1, int y1, int x, int y)
        {
            for (int i = 0; i < Fields.Count; i++)
            {
                if (x1 == Fields[i].X && y1 == Fields[i].Y)
                {
                    Fields[i].Elem = _model.GetGameTable.GetPalyaElemString(x1,y1);                                 
                }
                else if (x == Fields[i].X && y == Fields[i].Y)
                {
                    Fields[i].Elem = _model.GetGameTable.GetPalyaElemString(x, y);
                }
            }
        }
        #endregion

        #region Event methods
        private void OnNewGameSmall()
        {
            NewGameSmall?.Invoke(this, EventArgs.Empty);
            OnPropertyChanged(nameof(TableSize));
        }
        private void OnNewGameMedium()
        {
            NewGameMedium?.Invoke(this, EventArgs.Empty);
            OnPropertyChanged(nameof(TableSize));
        }
        private void OnNewGameLarge()
        {
            NewGameLarge?.Invoke(this, EventArgs.Empty);
            OnPropertyChanged(nameof(TableSize));
        }
        private void OnPauseGame()
        {
            PauseGame?.Invoke(this, EventArgs.Empty);
        }
        private void OnStartGame()
        {
            StartGame?.Invoke(this, EventArgs.Empty);
        }
        private void Model_GameOver(object? sender, StealthyEventArgs e){}
        private void MovedPlayer(object? sender, RefreshElemekEventArgs e)
        {
            RefreshElemets(e.X1, e.Y1, e.X2, e.Y2);
        }
        private void MovedGuard(object? sender, RefreshElemekEventArgs e)
        {
            RefreshElemets(e.X1, e.Y1, e.X2, e.Y2);
        }
        private void OnExitGame()
        {
            ExitGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnUpMove()
        {
            UpMove?.Invoke(this, EventArgs.Empty);
        }
        private void OnDownMove()
        {
            DownMove?.Invoke(this, EventArgs.Empty);
        }
        private void OnRightMove()
        {
            RightMove?.Invoke(this, EventArgs.Empty);
        }
        private void OnLeftMove()
        {
            LeftMove?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
