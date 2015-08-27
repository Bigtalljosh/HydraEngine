//Author - Josh
// This was meant to map a button to a specific function.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SharedContent;

namespace AWGP.Input
{
    class Input
    {
        public static Dictionary<Buttons, String>
        CreateGamepadDictionary(Buttons gamePad)
        {
            Dictionary<Buttons, String> dictionary =
               new Dictionary<Buttons, String>();
            dictionary.Add(Buttons.A, "");
            dictionary.Add(Buttons.B, "");
            dictionary.Add(Buttons.X, "");
            dictionary.Add(Buttons.Y, "");
            dictionary.Add(Buttons.LeftShoulder, "");
            dictionary.Add(Buttons.RightShoulder, "");
            dictionary.Add(Buttons.LeftTrigger, "");
            dictionary.Add(Buttons.RightTrigger, "");
            dictionary.Add(Buttons.LeftStick, "");
            dictionary.Add(Buttons.RightStick, "");
            dictionary.Add(Buttons.Back, "");
            dictionary.Add(Buttons.Start, "");
            dictionary.Add(Buttons.DPadDown, "");
            dictionary.Add(Buttons.DPadLeft, "");
            dictionary.Add(Buttons.DPadRight, "");
            dictionary.Add(Buttons.DPadUp, "");
            dictionary.Add(Buttons.LeftThumbstickDown, "");
            dictionary.Add(Buttons.LeftThumbstickLeft, "");
            dictionary.Add(Buttons.LeftThumbstickRight, "");
            dictionary.Add(Buttons.LeftThumbstickUp, "");
            dictionary.Add(Buttons.RightThumbstickDown,"");
            dictionary.Add(Buttons.RightThumbstickLeft, "");
            dictionary.Add(Buttons.RightThumbstickRight,"");
            dictionary.Add(Buttons.RightThumbstickUp,"");
            return dictionary;
        }

        private void processInput(PlayerIndex index){

            foreach (int playerindex in System.Enum.GetValues(typeof(PlayerIndex)))
            {
                foreach (int value in System.Enum.GetValues(typeof(Buttons)))
                {
                    
                }
            }
        }
    }
}
