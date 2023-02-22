﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony.IO.Interfaces
{
    public interface IWriter
    {
        void Write(string text);
        void WriteLine(string text);
    }
}
