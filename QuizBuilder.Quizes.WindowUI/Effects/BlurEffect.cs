using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace QuizBuilder.Quizes.WindowUI.Effects
{
    class WindowBlurEffect
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompisitonAttributeData data);

        internal void EnableBlur(Window window)
        {
            var windowHelper = new WindowInteropHelper(window);
            var accent = new AccentPolicy();

            accent.AccentState = AccentState.ACCEND_ENABLE_BLUREBEHIND;
            var accentStructSize = Marshal.SizeOf(accent);


            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompisitonAttributeData();
            data.Attribute = WindowCompisitonAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        internal WindowBlurEffect(Window window)
        {
            EnableBlur(window);
        }
    }
}
