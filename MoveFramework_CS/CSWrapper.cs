using System.Runtime.InteropServices;

namespace MoveFramework_CS
{
    public enum MoveButton
    {
        B_T1 = 0x1,
        B_T2 = 0x4,
        B_TRIANGLE = 0x10,
        B_CIRCLE = 0x20,
        B_CROSS = 0x40,
        B_SQUARE = 0x80,
        B_SELECT = 0x100,
        B_START = 0x800,
        B_STICK = 0x200,
        B_UP = 0x1000,
        B_RIGHT = 0x2000,
        B_DOWN = 0x4000,
        B_LEFT = 0x8000,
        B_PS = 0x10000,
        B_MOVE = 0x80000,
        B_T = 0x100000
    }

    public enum PSMove_Battery_Level
    {
        // copied from NoxWings' PSMoveData.cs
        Batt_MIN = 0x00, /*!< Battery is almost empty (< 20%) */
        Batt_20Percent = 0x01, /*!< Battery has at least 20% remaining */
        Batt_40Percent = 0x02, /*!< Battery has at least 40% remaining */
        Batt_60Percent = 0x03, /*!< Battery has at least 60% remaining */
        Batt_80Percent = 0x04, /*!< Battery has at least 80% remaining */
        Batt_MAX = 0x05, /*!< Battery is fully charged (not on charger) */
        Batt_CHARGING = 0xEE, /*!< Battery is currently being charged */
        Batt_CHARGING_DONE = 0xEF, /*!< Battery is fully charged (on charger) */
    };

    public class MoveWrapper
    {
        public delegate void MoveUpdateCallback(int id, [MarshalAs(UnmanagedType.Struct)] Vector3 position, [MarshalAs(UnmanagedType.Struct)] Quaternion orientation, int trigger);
        public delegate void NavUpdateCallback(int id, int trigger1, int trigger2, int stickX, int stickY);
        public delegate void MoveKeyCallback(int id, int keyCode);
        private static MoveUpdateCallback updateCallback;
        private static MoveKeyCallback keyDownCallback;
        private static MoveKeyCallback keyUpCallback;
        private static NavUpdateCallback navUpdateCallback;
        private static MoveKeyCallback navKeyDownCallback;
        private static MoveKeyCallback navKeyUpCallback;

        [StructLayout(LayoutKind.Sequential)]
        public struct Vector3
        {
            public float x;
            public float y;
            public float z;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Quaternion
        {
            public float w;
            public float x;
            public float y;
            public float z;
        }

        [DllImport("MF_CWrapper.dll")]
        public static extern void init();
        [DllImport("MF_CWrapper.dll")]
        public static extern void close();
        [DllImport("MF_CWrapper.dll")]
        public static extern int getMovesCount();
        [DllImport("MF_CWrapper.dll")]
        public static extern int getNavsCount();
        [DllImport("MF_CWrapper.dll")]
        public static extern void unsubscribeMove();
        [DllImport("MF_CWrapper.dll")]
        private static extern void subscribeMove(MoveUpdateCallback updateCallback, MoveKeyCallback keyDownCallback, MoveKeyCallback keyUpCallback, NavUpdateCallback navCallback, MoveKeyCallback navKeyDown, MoveKeyCallback navKeyUp);
        [DllImport("MF_CWrapper.dll")]
        [return: MarshalAs(UnmanagedType.Struct)]
        public static extern Quaternion getOrientation(int id);
        [DllImport("MF_CWrapper.dll")]
        [return: MarshalAs(UnmanagedType.Struct)]
        public static extern Vector3 getPosition(int id);
        [DllImport("MF_CWrapper.dll")]
        [return: MarshalAs(UnmanagedType.Struct)]
        public static extern Vector3 getAngularAcceleration(int id);
        [DllImport("MF_CWrapper.dll")]
        public static extern bool getButtonState(int id, [MarshalAs(UnmanagedType.I4)] MoveButton keyId);
        [DllImport("MF_CWrapper.dll")]
        public static extern int getTriggerValue(int id);
        [DllImport("MF_CWrapper.dll")]
        public static extern int getBatteryValue(int id);
        [DllImport("MF_CWrapper.dll")]
        public static extern void setRumble(int id, int value);
        [DllImport("MF_CWrapper.dll")]
        public static extern void setColour(int id, int r, int g, int b);
        [DllImport("MF_CWrapper.dll")]
        public static extern int getNavTrigger1(int id);
        [DllImport("MF_CWrapper.dll")]
        public static extern int getNavTrigger2(int id);
        [DllImport("MF_CWrapper.dll")]
        public static extern int getNavStickX(int id);
        [DllImport("MF_CWrapper.dll")]
        public static extern int getNavStickY(int id);

        public static void subscribeMoveUpdate(MoveUpdateCallback updateCallback, MoveKeyCallback keyDownCallback, MoveKeyCallback keyUpCallback, NavUpdateCallback navCallback, MoveKeyCallback navKeyDown, MoveKeyCallback navKeyUp)
        {
            MoveWrapper.updateCallback = updateCallback;
            MoveWrapper.keyDownCallback = keyDownCallback;
            MoveWrapper.keyUpCallback = keyUpCallback;
            MoveWrapper.navUpdateCallback = navCallback;
            MoveWrapper.navKeyDownCallback = navKeyDown;
            MoveWrapper.navKeyUpCallback = navKeyUp;
            subscribeMove(updateCallback, keyDownCallback, keyUpCallback, navCallback, navKeyDown, navKeyUp);
        }
    }
}
