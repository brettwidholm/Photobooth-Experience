using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class VirtualKeyboardInjector : MonoBehaviour
{
    [StructLayout(LayoutKind.Sequential)]
    struct INPUT
    {
        public uint type;
        public InputUnion U;
    }

    [StructLayout(LayoutKind.Explicit)]
    struct InputUnion
    {
        [FieldOffset(0)] public KEYBDINPUT ki;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    const uint INPUT_KEYBOARD = 1;
    const uint KEYEVENTF_UNICODE = 0x0004;
    const uint KEYEVENTF_KEYUP   = 0x0002;

    [DllImport("user32.dll", SetLastError = true)]
    static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    public void SendAtSign()
    {
        // Create two inputs: key‐down and key‐up of the Unicode character '@' (0x0040)
        INPUT[] inputs = new INPUT[2];

        // Key down
        inputs[0].type = INPUT_KEYBOARD;
        inputs[0].U.ki.wVk = 0;
        inputs[0].U.ki.wScan = (ushort)'@'; 
        inputs[0].U.ki.dwFlags = KEYEVENTF_UNICODE;
        inputs[0].U.ki.time = 0;
        inputs[0].U.ki.dwExtraInfo = IntPtr.Zero;

        // Key up
        inputs[1].type = INPUT_KEYBOARD;
        inputs[1].U.ki.wVk = 0;
        inputs[1].U.ki.wScan = (ushort)'@';
        inputs[1].U.ki.dwFlags = KEYEVENTF_UNICODE | KEYEVENTF_KEYUP;
        inputs[1].U.ki.time = 0;
        inputs[1].U.ki.dwExtraInfo = IntPtr.Zero;

        uint sent = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        if (sent != inputs.Length)
            Debug.LogError("SendInput failed: " + Marshal.GetLastWin32Error());
    }
}