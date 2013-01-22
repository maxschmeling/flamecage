using System;
using System.Threading.Tasks;

namespace GrayHills.FlameCage.Client.Core
{
  public class AsyncRelayCommand : RelayCommand
  {
    public AsyncRelayCommand(Action execute) 
      : base(execute)
    {
    }

    public AsyncRelayCommand(Action<object> execute) 
      : base(execute)
    {
    }

    public AsyncRelayCommand(Action<object> execute, Predicate<object> canExecute) 
      : base(execute, canExecute)
    {
    }

    public override void Execute(object parameter)
    {
      var baseExecuteTask = new Task(() => base.Execute(parameter));

      baseExecuteTask.Start();
    }
  }
}