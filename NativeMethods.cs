using System;
using System.Runtime.InteropServices;

namespace AutoClickerConsole
{
    class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            int dx;
            int dy;
            uint mouseData;
            uint dwFlags;
            uint time;
            IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            uint uMsg;
            ushort wParamL;
            ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)] 
            public MOUSEINPUT mi;
            [FieldOffset(4)] 
            public KEYBDINPUT ki;
            [FieldOffset(4)] 
            public HARDWAREINPUT hi;
        }

        [DllImport("User32.dll")]
        private static extern uint SendInput(uint numberOfInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] input, int structSize);

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private struct POINT
        {
            public uint X;
            public uint Y;
        }

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        public static void MouseClick(bool right)
        {
            POINT point;
            GetCursorPos(out point);

            uint X = point.X;
            uint Y = point.Y;

            if (!right)
                mouse_event(0x02 | 0x04, X, Y, 0, 0);
            else
                mouse_event(0x08 | 0x10, X, Y, 0, 0);
        }

        public static bool IsButtonPressed(int button)
        {
            for (int i = 0; i < 255; i++)
            {
                int keyState = GetAsyncKeyState(i);

                if (keyState != 0 && i == button)
                    return true;
            }

            return false;
        }

        public static void PressButton(ushort button)
        {
            INPUT input = new INPUT();

            input.type = 1;
            input.ki.wScan = 0;
            input.ki.time = 0;
            input.ki.dwExtraInfo = GetMessageExtraInfo();

            input.ki.wVk = button;
            input.ki.dwFlags = 0;
            SendInput(1, new INPUT[] { input }, Marshal.SizeOf(input));

            input.ki.dwFlags = 0x0002;
            SendInput(1, new INPUT[] { input }, Marshal.SizeOf(input));
        }
    }
}
