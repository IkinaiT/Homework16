using Homework16.Infrastructure.Commands.Base;

namespace Homework16.Infrastructure.Commands
{
    internal class RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null) : Command
    {
        private readonly Action<object> _execute = execute;
        private readonly Func<object, bool>? _canExecute = canExecute;

        public override bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object? parameter) => _execute(parameter);
    }
}
