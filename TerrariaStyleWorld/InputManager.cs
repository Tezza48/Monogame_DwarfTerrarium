using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace TerrariaStyleWorld
{
    class InputManager
    {
        private static KeyboardState KBStatePrev;
        private static KeyboardState KBStateCurrent;

        private static MouseState MouseStatePrev;
        private static MouseState MouseStateCurrent;

        public static void Init()
        {
            KBStateCurrent = KBStatePrev = Keyboard.GetState();
            MouseStateCurrent = MouseStatePrev = Mouse.GetState();
        }

        public static void Update()
        {
            KBStatePrev = KBStateCurrent;
            MouseStatePrev = MouseStateCurrent;

            KBStateCurrent = Keyboard.GetState();
            MouseStateCurrent = Mouse.GetState();
        }

        public static bool GetKey(Keys key)
        {
            return KBStateCurrent.IsKeyDown(key);
        }

        public static bool GetKeyDown(Keys key)
        {
            return KBStateCurrent.IsKeyDown(key) && KBStatePrev.IsKeyUp(key);
        }

        public static bool GetKeyUp(Keys key)
        {
            return KBStateCurrent.IsKeyUp(key) && KBStatePrev.IsKeyDown(key);
        }

    }
}
