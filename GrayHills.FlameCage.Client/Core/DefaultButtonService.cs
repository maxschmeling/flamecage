using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;

namespace GrayHills.FlameCage.Client.Core
{
  public static class DefaultButtonService
  {
    public static DependencyProperty DefaultButtonProperty =
        DependencyProperty.RegisterAttached("DefaultButton",
                                            typeof(Button),
                                            typeof(DefaultButtonService),
                                            new PropertyMetadata(null, DefaultButtonChanged));

    private static void DefaultButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var uiElement = d as UIElement;
      var button = e.NewValue as Button;
      if (uiElement != null && button != null)
      {
        uiElement.KeyUp += (sender, arg) =>
                               {
                                 if (arg.Key == Key.Enter && button.IsEnabled)
                                 {
                                   var peer = new ButtonAutomationPeer(button);
                                   var invokeProv =
                                       peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                                   if (invokeProv != null)
                                     invokeProv.Invoke();
                                 }
                               };
      }
    }

    public static void SetDefaultButton(UIElement obj, Button button)
    {
      obj.SetValue(DefaultButtonProperty, button);
    }

    public static void GetDefaultButton(UIElement obj)
    {
      obj.GetValue(DefaultButtonProperty);
    }
  }
}
