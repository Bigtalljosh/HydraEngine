using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace SharedContent
{
    public class KeybindingsConfig
    {
        // Menu Options for Keyboard and Gamepad
        public Keys KBMenuSelect;
        public Keys KBMenuCancel;
        public Keys KBMenuUp;
        public Keys KBMenuDown;
        public Buttons GPMenuSelect;
        public Buttons GPMenuCancel;
        public Buttons GPMenuUp;
        public Buttons GPMenuDown;

        // Player Options for Keyboard and Gamepad
        public Keys KBPlayerMoveUp;
        public Keys KBPlayerMoveDown;
        public Keys KBPlayerMoveLeft;
        public Keys KBPlayerMoveRight;
        public Keys KBPlayerAction01;
        public Keys KBPlayerAction02;
        public Keys KBPlayerAction03;
        public Buttons GPPlayerAction01;
        public Buttons GPPlayerAction02;
        public Buttons GPPlayerAction03;

        // Audio interaction
        public Keys KBAudioVolumeUp;
        public Keys KBAudioVolumeDown;
        public Keys KBAudioToggle;
        public Buttons GPAudioVolumeUp;
        public Buttons GPAudioVolumeDown;
        public Buttons GPAudioToggle;

        // Interaction Demo specific Keybindings
        public Keys KBEnableEditUI;
        public Keys KBDisableEditUI;

        // Camera interaction
        public Keys KBCameraZoomIn;
        public Keys KBCameraZoomOut;
        public Keys KBCameraRotateLeft;
        public Keys KBCameraRotateRight;
        public Keys KBCameraReset;
    }
}
