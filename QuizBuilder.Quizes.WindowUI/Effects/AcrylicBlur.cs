using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizBuilder.Quizes.WindowUI.Effects
{
    internal enum AccentState
    {
        ACCEND_DISABLED = 0,
        ACCEND_ENABLE_GRADIENT = 1,
        ACCEND_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCEND_ENABLE_BLUREBEHIND = 3,
        ACCEND_ENABLE_ACRYLICBLUREBEHIND = 4,
        ACCEND_INVALID_STATE = 5,
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public uint AccentFlags;
        public uint GradientColor;
        public uint AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompisitonAttributeData
    {


        public WindowCompisitonAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }
    internal enum WindowCompisitonAttribute
    {
        WCA_ACCENT_POLICY = 19
    }
}
