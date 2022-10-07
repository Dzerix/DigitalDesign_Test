using DigitalDesign_Test.Commands.Base;
using System;

namespace DigitalDesign_Test.Commands
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecure;

        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecure = null)
        {
            _execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _canExecure = CanExecure;
        }

        public override bool CanExecute(object parameter) => _canExecure?.Invoke(parameter) ?? true;
        

        public override void Execute(object parameter) => _execute(parameter);
        
    }
}
