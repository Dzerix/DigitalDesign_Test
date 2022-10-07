using DigitalDesign_Test.Commands;
using DigitalDesign_Test.ViewModels.Base;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using DigitalDesign_Test.ViewModels;


namespace DigitalDesign_Test.ViewModels
{
    internal class GameViewModel : ViewModel
    {
        #region Кол-во строк и столбцов
        private int _N = 3;

        public int N
        {
            get => _N;
            set => Set(ref _N, value);
        }
        #endregion

        #region Массив ручек сейфа
        private int[,] _handleArray;

        
        public int[,] HandleArray
        {
            get => _handleArray;
            set => Set(ref _handleArray, value);
        }
        #endregion


        #region Up
        public ICommand Up { get; }

        private void OnUpCommandExecuted(object p)
        {
            N++; 
        }
        private bool CanUpCommandExecut(object p)
        {
            if (N < 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Down
        public ICommand Down { get; }

        private void OnDownCommandExecuted(object p)
        {
            N--;
        }
        private bool CanDownCommandExecut(object p)
        {
            if (N > 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        public GameViewModel()
        {
            Up = new LambdaCommand(OnUpCommandExecuted, CanUpCommandExecut);
            Down = new LambdaCommand(OnDownCommandExecuted, CanDownCommandExecut);
        }

    }

}
