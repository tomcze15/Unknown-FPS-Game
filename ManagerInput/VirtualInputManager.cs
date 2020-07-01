﻿using UnknownFPSGame.GeneralScripts;

namespace ManagerInput
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool Forward;
        public bool Right;
        public bool Left;
        public bool Back;
        public bool Run;
        public bool Jump;
        public bool TurnLeft;
        public bool TurnRight;
        //public bool Any;
    }
}