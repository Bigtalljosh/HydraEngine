////http://en.csharp-online.net/XNA_Game_Programming%E2%80%94Settings_Manager
////This handles the input of one player, can be init multiple times for multiple players 
////Takes in index of current player, So, if you have a two-player game, you need to have two InputHelper objects.
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework;
//using SharedContent;

//namespace AWGP.Input
//{
//    class InputHandle
//    {
//        PlayerIndex playerIndex;
//        // Keyboard
//        KeyboardState keyboardState;
//        KeyboardState lastKeyboardState;
//        Dictionary<Buttons, Keys> keyboardMap;

//        // Gamepad
//        GamePadState gamePadState;
//        GamePadState lastGamePadState;

//        public InputHandle(PlayerIndex playerIndex)
//            : this(playerIndex, null)
//        {
//        }

//        public InputHandle(PlayerIndex playerIndex,
//           Dictionary<Buttons, Keys> keyboardMap)
//        {
//            this.playerIndex = playerIndex;
//            this.keyboardMap = keyboardMap;
//        }

//        public void Update()
//        {
//            lastKeyboardState = keyboardState;
//            keyboardState = Keyboard.GetState(playerIndex);
//            lastGamePadState = gamePadState;
//            gamePadState = GamePad.GetState(playerIndex);
//        }

//        public bool IsKeyPressed(Buttons button)
//        {
//            bool pressed = false;
//            if (gamePadState.IsConnected)
//                pressed = gamePadState.IsButtonDown(button);
//            else if (keyboardMap != null)
//            {
//                Keys key = keyboardMap[button];
//                pressed = keyboardState.IsKeyDown(key);
//            }
//            return pressed;
//        }

//        public bool IsKeyJustPressed(Buttons button)
//        {
//            bool pressed = false;
//            if (gamePadState.IsConnected)
//                pressed = (gamePadState.IsButtonDown(button) &&
//                lastGamePadState.IsButtonUp(button));
//            else if (keyboardMap != null)
//            {
//                Keys key = keyboardMap[button];
//                pressed = (keyboardState.IsKeyDown(key) &&
//                lastKeyboardState.IsKeyUp(key));
//            }
//            return pressed;
//        }

//        public Vector2 GetLeftThumbStick()
//        {
//            Vector2 thumbPosition = Vector2.Zero;
//            if (gamePadState.IsConnected)
//                thumbPosition = gamePadState.ThumbSticks.Left;
//            else if (keyboardMap != null)
//            {
//                if (keyboardState.IsKeyDown(
//                   keyboardMap[Buttons.LeftThumbstickUp]))
//                    thumbPosition.Y = 1;
//                else if (keyboardState.IsKeyDown(
//                   keyboardMap[Buttons.LeftThumbstickDown]))
//                    thumbPosition.Y = -1;
//                if (keyboardState.IsKeyDown(
//                   keyboardMap[Buttons.LeftThumbstickRight]))
//                    thumbPosition.X = 1;
//                else if (keyboardState.IsKeyDown(
//                   keyboardMap[Buttons.LeftThumbstickLeft]))
//                    thumbPosition.X = -1;
//            }
//            return thumbPosition;
//        }

//        public Vector2 GetRightThumbStick()
//        {
//            Vector2 thumbPosition = Vector2.Zero;
//            if (gamePadState.IsConnected)
//                thumbPosition = gamePadState.ThumbSticks.Right;
//            else if (keyboardMap != null)
//            {
//                if (keyboardState.IsKeyDown(
//                   keyboardMap[Buttons.RightThumbstickUp]))
//                    thumbPosition.Y = 1;
//                else if (keyboardState.IsKeyDown(
//                   keyboardMap[Buttons.RightThumbstickDown]))
//                    thumbPosition.Y = -1;
//                if (keyboardState.IsKeyDown(
//                   keyboardMap[Buttons.RightThumbstickRight]))
//                    thumbPosition.X = 1;
//                else if (keyboardState.IsKeyDown(
//                   keyboardMap[Buttons.RightThumbstickLeft]))
//                    thumbPosition.X = -1;
//            }
//            return thumbPosition;
//        }

//        public static Dictionary<Buttons, Keys>
//        GetKeyboardDictionary(KeyboardSettings keyboard)
//        {
//            Dictionary<Buttons, Keys> dictionary =
//               new Dictionary<Buttons, Keys>();
//            dictionary.Add(Buttons.A, keyboard.A);
//            dictionary.Add(Buttons.B, keyboard.B);
//            dictionary.Add(Buttons.X, keyboard.X);
//            dictionary.Add(Buttons.Y, keyboard.Y);
//            dictionary.Add(Buttons.LeftShoulder, keyboard.LeftShoulder);
//            dictionary.Add(Buttons.RightShoulder, keyboard.RightShoulder);
//            dictionary.Add(Buttons.LeftTrigger, keyboard.LeftTrigger);
//            dictionary.Add(Buttons.RightTrigger, keyboard.RightTrigger);
//            dictionary.Add(Buttons.LeftStick, keyboard.LeftStick);
//            dictionary.Add(Buttons.RightStick, keyboard.RightStick);
//            dictionary.Add(Buttons.Back, keyboard.Back);
//            dictionary.Add(Buttons.Start, keyboard.Start);
//            dictionary.Add(Buttons.DPadDown, keyboard.DPadDown);
//            dictionary.Add(Buttons.DPadLeft, keyboard.DPadLeft);
//            dictionary.Add(Buttons.DPadRight, keyboard.DPadRight);
//            dictionary.Add(Buttons.DPadUp, keyboard.DPadUp);
//            dictionary.Add(Buttons.LeftThumbstickDown,
//               keyboard.LeftThumbstickDown);
//            dictionary.Add(Buttons.LeftThumbstickLeft,
//               keyboard.LeftThumbstickLeft);
//            dictionary.Add(Buttons.LeftThumbstickRight,
//               keyboard.LeftThumbstickRight);
//            dictionary.Add(Buttons.LeftThumbstickUp,
//               keyboard.LeftThumbstickUp);
//            dictionary.Add(Buttons.RightThumbstickDown,
//               keyboard.RightThumbstickDown);
//            dictionary.Add(Buttons.RightThumbstickLeft,
//               keyboard.RightThumbstickLeft);
//            dictionary.Add(Buttons.RightThumbstickRight,
//               keyboard.RightThumbstickRight);
//            dictionary.Add(Buttons.RightThumbstickUp,
//               keyboard.RightThumbstickUp);
//            return dictionary;
//        }

//    }
//}
