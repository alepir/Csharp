using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Maze.Annotations;
using Maze.Commands;
using Maze.Models;
using Maze.Views;

namespace Maze.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand GenerateMazeCommand { get; }

        public ICommand MazeButtonLeftClickCommand { get; }

        public ICommand MazeButtonRightClickCommand { get; }

        public ICommand MazeSolveCommand { get; }

        private string _width;
        private string _height;

        private MazeSolver mazeSolver;

        private bool RedrawAfterSolve = false;

        public string Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }
        public string Height {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        private int _rows;
        private int _cols;

        public MainViewModel()
        {
            mazeSolver = new MazeSolver();
            MazeButtonLeftClickCommand = new RelayCommand(MazeButtonLeftClick);
            MazeButtonRightClickCommand = new RelayCommand(MazeButtonRightClick);
            GenerateMazeCommand = new RelayCommand(GenerateMaze);
            MazeSolveCommand = new RelayCommand(MazeSolve);
        }

        private void MazeSolve(object parameter)
        {
            if (parameter is not MainWindow mainWindow)
            {
                return;
            }
            mazeSolver.SolveMaze();

            RedrawAfterSolve = true;
            GenerateMaze(mainWindow);
            RedrawAfterSolve = false;
        }


        private void GenerateGrid(MainWindow mainWindow)
        {
            if (ParsePropertiesAndSetToPrivateProperties()) return;

            CreateGrid(mainWindow);
        }

        private bool ParsePropertiesAndSetToPrivateProperties()
        {
            var resultAfterParseWidth = int.TryParse(Width, out _cols);
            var resultAfterParseHeight = int.TryParse(Height, out _rows);

            if (resultAfterParseWidth == false || resultAfterParseHeight == false)
            {
                MessageBox.Show("Width or Height was not a number");
                Height = "";
                Width = "";
                return true;
            }

            return false;
        }


        private void CreateGrid(MainWindow mainWindow)
        {
            mainWindow.MazeGrid.Children.Clear();

            if (RedrawAfterSolve == false)
            {
                mazeSolver.SetMazeSize(_rows, _cols);
            }
            CreateColumnDefinition(_cols, mainWindow.MazeGrid);
            CreateRowDefinition(_rows, mainWindow.MazeGrid);
            InitRowDefinition(mainWindow.MazeGrid);
            InitColDefinition(mainWindow.MazeGrid);
        }

        private static void CreateRowDefinition(int rows, Grid grid)
        {
            for (var i = 0; i < rows; ++i)
                grid.RowDefinitions.Add(new RowDefinition());
        }

        private static void CreateColumnDefinition(int cols, Grid grid)
        {
            for (var i = 0; i < cols; ++i)
                grid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private static void InitColDefinition(Grid grid)
        {
            foreach (var colD in grid.ColumnDefinitions)
            {
                colD.Width = new GridLength(100);
            }
        }

        private static void InitRowDefinition(Grid grid)
        {
            foreach (var rowD in grid.RowDefinitions)
            {
                rowD.Height = new GridLength(100);
            }
        }

        private void MazeButtonRightClick(object parameter)
        {
            if (parameter is not Button button)
            {
                return;
            }

            var column = GetColumnProperty(button);

            var row = GetRowProperty(button);

            var value = mazeSolver.GetItem(row, column);

            button.Background = Brushes.YellowGreen;

            HandleButtonStateRightClick(button, row, column, value);
        }

        private void HandleButtonStateRightClick(Button button, int row, int column, MazeStates value)
        {

            bool existStart = mazeSolver.ExistState(MazeStates.Start);
            bool existFinish = mazeSolver.ExistState(MazeStates.Finish);

            if (!existStart)
            {
                SetStart(button, row, column);
                return;
            }
            
            if (!existFinish)
            {
                SetFinish(button, row, column);
                return;
            }
            
            if (value == MazeStates.Empty)
            {
                MessageBox.Show("When you want mark new start or finish, un-mark already existing");
            }

            SetEmpty(button, row, column);
            
        }

        private void SetEmpty(Button button, int row, int column)
        {
            button.Background = Brushes.White;
            button.Content = "";
            mazeSolver.SetItem(row, column, MazeStates.Empty);
        }

        private void SetFinish(Button button, int row, int column)
        {
            button.Background = Brushes.YellowGreen;
            button.Content = "FINISH";
            mazeSolver.SetItem(row, column, MazeStates.Finish);
        }

        private void SetStart(Button button, int row, int column)
        {
            button.Background = Brushes.YellowGreen;
            button.Content = "START";
            mazeSolver.SetItem(row, column, MazeStates.Start);
        }
        private void SetWall(Button button, int row, int column)
        {
            button.Background = Brushes.Black;
            button.Content = "";
            mazeSolver.SetItem(row, column, MazeStates.Wall);
        }

        private void SetPath(Button button, int row, int column)
        {
            button.Background = Brushes.Red;
            button.Content = "";
            mazeSolver.SetItem(row, column, MazeStates.Path);
        }

        private void MazeButtonLeftClick(object parameter)
        {
            if (parameter is not Button button) { return; }

            var column = GetColumnProperty(button);

            var row = GetRowProperty(button);

            var value = mazeSolver.GetItem(row, column);

            if (value == MazeStates.Wall)
            {
                SetEmpty(button, row, column);
                return;
            }
            
            SetWall(button, row, column);
            
        }


        private static int GetRowProperty(Button button)
        {
            if (!int.TryParse(button.GetValue(Grid.RowProperty).ToString(), out var row))
            {
                throw new ArgumentException("Grid row property must be number");
            }

            return row;
        }

        private static int GetColumnProperty(Button button)
        {
            if (!int.TryParse(button.GetValue(Grid.ColumnProperty).ToString(), out var column))
            {
                throw new ArgumentException("Grid column property must be number");
            }

            return column;
        }

        private void GenerateMaze(object parameter)
        {
            if (parameter is not MainWindow mainWindow) { return; }

            GenerateGrid(mainWindow);
            
            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _cols; j++)
                {
                    GenerateButton(i, j, mainWindow);
                }
            }
        }

        private void GenerateButton(int i, int j, MainWindow mainWindow)
        {
            var button = new Button
            {
                Background = Brushes.White
            };

            button.SetValue(Grid.ColumnProperty, j);
            button.SetValue(Grid.RowProperty, i);
            //button.Style = (Style) mainWindow.Resources["GenerateButton"];
            AddCommandForElement(button, MouseAction.LeftClick, MazeButtonLeftClickCommand);
            AddCommandForElement(button, MouseAction.RightClick, MazeButtonRightClickCommand);

            if (RedrawAfterSolve)
            {
                var value = mazeSolver.GetItem(i, j);
                switch (value)
                {
                    case MazeStates.Empty:
                        SetEmpty(button,i,j);
                        break;
                    case MazeStates.Wall:
                        SetWall(button, i, j);
                        break;
                    case MazeStates.Start:
                        SetStart(button, i, j);
                        break;
                    case MazeStates.Finish:
                        SetFinish(button, i, j);
                        break;
                    case MazeStates.Path:
                        SetPath(button, i, j);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }


                mainWindow.MazeGrid.Children.Add(button);
        }


        private static void AddCommandForElement(UIElement element, MouseAction action, ICommand command)
        {
            var gesture = new MouseGesture
            {
                MouseAction = action
            };

            var mouseBinding = new MouseBinding
            {
                Gesture = gesture, 
                Command = command, 
                CommandParameter = element
            };

            element.InputBindings.Add(mouseBinding);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
