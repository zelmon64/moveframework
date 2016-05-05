using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace MoveFramework_CS
{
    class WrapperTest
    {
        static void Main(string[] args)
        {
            WrapperTest test =new WrapperTest();
            test.init();
        }

        public void init()
        {
            MoveWrapper.init();

            int moveCount = MoveWrapper.getMovesCount();
            Console.WriteLine("Moves: "+moveCount);
            Console.WriteLine("Navs: " + MoveWrapper.getNavsCount());

            if (moveCount > 0)
            {
                bool buttonX = MoveWrapper.getButtonState(0, MoveButton.B_CROSS);
                MoveWrapper.Quaternion q = MoveWrapper.getOrientation(0);
                MoveWrapper.Vector3 v = MoveWrapper.getPosition(0);
                int trigger = MoveWrapper.getTriggerValue(0);
                int battery = MoveWrapper.getBatteryValue(0);
                MoveWrapper.setRumble(0, 255);
                Thread.Sleep(40);
                MoveWrapper.setRumble(0, 0);
            }
            MoveWrapper.subscribeMoveUpdate(this.MoveUpdateCallback, this.MoveKeyDownCallback, this.MoveKeyUpCallback, this.NavUpdateCallback, this.NavKeyDownCallback, this.NavKeyUpCallback);
            MoveWrapper.somethink(this.MoveBatteryCallback);

            Console.ReadKey();
            MoveWrapper.unsubscribeMove();
            MoveWrapper.close();
        }

        void MoveUpdateCallback(int id, MoveWrapper.Vector3 position, MoveWrapper.Quaternion orientation, int trigger)
        {
            bool mbutton = MoveWrapper.getButtonState(id, MoveButton.B_MOVE);
            if (mbutton)
            {
                //UNCOMMENT if you want to see the position and orientation updates
                Console.WriteLine("UPDATE moveid: " + id +
                    " position: " + position.x + " " + position.y + " " + position.z +
                    " orientation:" + orientation.w + " " + orientation.x + " " + orientation.y + " " + orientation.z + " trigger: " + trigger);
            }
        }

        void MoveBatteryCallback(int id)
        {
            //bool mbutton = MoveWrapper.getButtonState(id, MoveButton.B_PS);
            //if (mbutton)
            {
                //UNCOMMENT if you want to see the position and orientation updates
                Console.WriteLine("BATTERY moveid: " + id + " level: " +
                    Enum.GetName(typeof(PSMove_Battery_Level), MoveWrapper.getBatteryValue(id)));


            }
        }

        void MoveKeyUpCallback(int id, int keyCode)
        {
            Console.WriteLine("KEYUP moveid: " + id + " keyname: " + Enum.GetName(typeof(MoveButton), keyCode));
            MoveWrapper.setRumble(id, 0);
            MoveWrapper.setColour(id, 100, 100, 100);
        }

        void MoveKeyDownCallback(int id, int keyCode)
        {
            Console.WriteLine("KEYDOWN moveid: " + id + " keyname: " + Enum.GetName(typeof(MoveButton), keyCode));
            int rumval = MoveWrapper.getTriggerValue(id);
            MoveWrapper.setRumble(id, rumval);
            MoveWrapper.setColour(id, 0, 0, 0);
            //Console.WriteLine("Battery level: " + MoveWrapper.getBatteryValue(id));
            Console.WriteLine(Enum.GetName(typeof(PSMove_Battery_Level), MoveWrapper.getBatteryValue(id)));
            // -1733 = 29.0, -1685.5 = 29 degrees C // -1681.5 = 29.5 
            int rawtemp = MoveWrapper.getTemperatureValue(id);
            double realtemp = 29.0 + 0.5/(1733.0-1681.5) * (rawtemp+1733);
            Console.WriteLine("TEMPERATURE: " + rawtemp + " = " + realtemp + " Deg C");
            //MoveWrapper.Vector3 angacc = MoveWrapper.getAngularAcceleration(id);
            //Console.WriteLine("ANGULARACCELERATION (X,Y,Z): ( " + angacc.x + " , " + angacc.y + " , " + angacc.z + " ) ");
            /*
            .Vector3 acc = MoveWrapper.getAcceleration(id);
            Console.WriteLine("ACCELERATION (X,Y,Z): ( " + acc.x + " , " + acc.y + " , " + acc.z + " ) ");
            /*
            MoveWrapper.Vector3 magnet = MoveWrapper.getMagnetisation(id);
            Console.WriteLine("MAGNETISATION (X,Y,Z): ( " + magnet.x + " , " + magnet.y + " , " + magnet.z + " ) ");
            Console.WriteLine("MAGNETISATION (X,Y,Z): ( " +
                MoveWrapper.getMagnetXValue(id) + " , " + MoveWrapper.getMagnetYValue(id) + " , " 
                + MoveWrapper.getMagnetZValue(id) + " ) ");
            */
        }

        void NavUpdateCallback(int id, int trigger1, int trigger2, int stickX, int stickY)
        {
            //UNCOMMENT if you want to see the trigger and stick updates
            Console.WriteLine("UPDATE navid: " + id + " trigger1: " + trigger1 + " trigger2: " + trigger2 +
                " stickX: " + stickX + " stickY:" + stickY);
        }

        void NavKeyUpCallback(int id, int keyCode)
        {
            Console.WriteLine("NAV KEYUP navid: " + id + " keyname: " + Enum.GetName(typeof(MoveButton), keyCode));
        }

        void NavKeyDownCallback(int id, int keyCode)
        {
            Console.WriteLine("NAV KEYDOWN navid: " + id + " keyname: " + Enum.GetName(typeof(MoveButton), keyCode));
        }
    }
}
