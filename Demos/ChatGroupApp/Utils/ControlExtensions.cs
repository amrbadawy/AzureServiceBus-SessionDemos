using System;
using System.Windows.Forms;

namespace ChatGroupApp
{
    public static class ControlExtensions
    {
        public static void Invoke<TControlType>(this TControlType control, Action<TControlType> action)
            where TControlType : Control
        {
            if (control.InvokeRequired)
                control.Invoke(new Action(() => action(control)));
            else
                action(control);
        }
    }
}
