using System.Collections.Generic;
using System.Linq;

namespace GrayHills.FlameCage.Client.Core
{
  //Based on Jeremy Miller's Event Aggregator
  //http://codebetter.com/blogs/jeremy.miller/archive/2009/07/23/how-i-m-using-the-event-aggregator-pattern-in-storyteller.aspx
  public class EventAggregator : IEventAggregator
  {
    private readonly IList<object> listeners;

    public EventAggregator()
    {
      listeners = new List<object>();
    }

    public void SendMessage<T>(T message)
    {
      var applicableListeners = listeners.Where(x => x is IListener<T>);

      if (applicableListeners.Count() >= 0)
      {
        lock (listeners)
        {
          applicableListeners.ToList().ForEach(l => ((IListener<T>) l).Handle(message));
        }
      }
    }

    public void AddListener(object listener)
    {
      lock (listeners)
      {
        listeners.Add(listener);
      }
    }

    public void RemoveListener(object listener)
    {
      lock (listeners)
      {
        listeners.Remove(listener);
      }
    }
  }
}
