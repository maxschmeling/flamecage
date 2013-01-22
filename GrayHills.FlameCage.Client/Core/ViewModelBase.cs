using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace GrayHills.FlameCage.Client.Core
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged<T>(Expression<Func<T>> propertyNameLambda)
        {
            var member = propertyNameLambda.Body as MemberExpression;
            if (member != null)
            {
                OnPropertyChanged(new PropertyChangedEventArgs(member.Member.Name));
            }
            OnPropertyChanged(new PropertyChangedEventArgs(""));
        }

        internal virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
    }

    public static class ViewModelBaseExtensions
    {
        public static void NotifyPropertyChanged<T, TProperty>(this T viewModelBase, Expression<Func<T, TProperty>> expression) where T : ViewModelBase
        {
            MemberExpression me;
            
            if (expression.Body is UnaryExpression)
            {
                var ue = (UnaryExpression) expression.Body;
                me = (MemberExpression) ue.Operand;
            }
            else
            {
                me = (MemberExpression) expression.Body;
            }

            viewModelBase.OnPropertyChanged(new PropertyChangedEventArgs(me.Member.Name));
        }
    }
}
