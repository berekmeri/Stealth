using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Stealthy.Model;
using Stealthy.Persistence;
using Stealthy.ViewModel;
using Stealthy.View;
using Microsoft.Win32;

namespace Stealthy
{
    public partial class App : Application
    {
        #region Fields
        private StealthyGameModel _model = null!;
        private StealthyViewModel _viewModel = null!;
        private MainWindow _view = null!;
        #endregion

        #region Constructors
        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }
        #endregion

        #region Application event handlers
        private void App_Startup(object? sender, StartupEventArgs e)
        {
            // modell létrehozása
            _model = new StealthyGameModel(new StealthyDataAccess());
            _model.GameOver += new EventHandler<StealthyEventArgs>(Model_GameOver);
            _model.NewGame();

            // nézemodell létrehozása
            _viewModel = new StealthyViewModel(_model);
            _viewModel.NewGameSmall += new EventHandler(ViewModel_NewGameSmall);
            _viewModel.NewGameMedium += new EventHandler(ViewModel_NewGameMedium);
            _viewModel.NewGameLarge += new EventHandler(ViewModel_NewGameLarge);
            _viewModel.ExitGame += new EventHandler(ViewModel_ExitGame);
            _viewModel.PauseGame += new EventHandler(ViewModel_PauseGame);
            _viewModel.StartGame += new EventHandler(ViewModel_StartGame);
            _viewModel.UpMove += new EventHandler(ViewModel_UpMove);
            _viewModel.DownMove += new EventHandler(ViewModel_DownMove);
            _viewModel.RightMove += new EventHandler(ViewModel_RightMove);
            _viewModel.LeftMove += new EventHandler(ViewModel_LeftMove);
            _viewModel.Guard.StartTime();

            // nézet létrehozása
            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Show();
        }
        #endregion

        #region ViewModel event handlers
        private void ViewModel_NewGameSmall(object? sender, EventArgs e)
        {  
            _viewModel.Guard.StopTime();
            _model.gameTable = GameTable.SMALL;
            _model.NewGame();
            _viewModel.GenerateTable();
            _viewModel.RefreshTable();
            _viewModel.Guard.StartTime();
        }
        private void ViewModel_NewGameMedium(object? sender, EventArgs e)
        {
            _viewModel.Guard.StopTime();
            _model.gameTable = GameTable.MEDIUM;
            _model.NewGame();
            _viewModel.GenerateTable();
            _viewModel.RefreshTable();
            _viewModel.Guard.StartTime();     
        }
        private void ViewModel_NewGameLarge(object? sender, EventArgs e)
        {
            _viewModel.Guard.StopTime();
            _model.gameTable = GameTable.LARGE;
            _model.NewGame();
            _viewModel.GenerateTable();
            _viewModel.RefreshTable();
            _viewModel.Guard.StartTime();       
        }
        private void ViewModel_PauseGame(object? sender, EventArgs e)
        {
            if (_model.GetGameStatus != GameStatus.END)
            {
                _viewModel.Guard.StopTime();
                _model.SetStatus(GameStatus.PAUSE);
            }
        }
        private void ViewModel_StartGame(object? sender, EventArgs e)
        {
            if (_model.GetGameStatus == GameStatus.PAUSE)
            {
                _model.SetStatus(GameStatus.START);
                _viewModel.Guard.StartTime();
            }
        }
        private void ViewModel_UpMove(object? sender, EventArgs e)
        {
            if (_model.GetGameStatus == GameStatus.START)
            {       
                _viewModel.Player.PlayerGo(_viewModel.Player.PlayerX() - 1, _viewModel.Player.PlayerY());  
            }
        }
        private void ViewModel_DownMove(object? sender, EventArgs e)
        {
            if (_model.GetGameStatus == GameStatus.START)
            {
                _viewModel.Player.PlayerGo(_viewModel.Player.PlayerX() + 1, _viewModel.Player.PlayerY());
            }
        }
        private void ViewModel_LeftMove(object? sender, EventArgs e)
        {
            if (_model.GetGameStatus == GameStatus.START)
            {
                _viewModel.Player.PlayerGo(_viewModel.Player.PlayerX(), _viewModel.Player.PlayerY() - 1);
            }
        }
        private void ViewModel_RightMove(object? sender, EventArgs e)
        {
            if (_model.GetGameStatus == GameStatus.START)
            {
                _viewModel.Player.PlayerGo(_viewModel.Player.PlayerX(), _viewModel.Player.PlayerY() + 1);
            }
        }
        private void ViewModel_ExitGame(object? sender, System.EventArgs e)
        {
            _viewModel.Guard.StopTime();
            _view.Close();
        }

        #endregion

        #region Model event handlers
        private void Model_GameOver(object? sender, StealthyEventArgs e)
        {
            _viewModel.Guard.StopTime();
            _model.SetStatus(GameStatus.END);

            if (e.IsWon) // győzelemtől függő üzenet megjelenítése
            {
                MessageBox.Show("Sikerült elmenekülni");
            }
            else
            {
                MessageBox.Show("Elkapott az őr");
            }
        }
       
        #endregion
    }
}
