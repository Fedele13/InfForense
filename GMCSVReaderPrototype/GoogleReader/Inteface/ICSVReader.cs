﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMCSVReaderPrototype
{
    interface ICSVReader<T>
    {
        IDictionary<string, T> MeetingDictionary { get; }
    }
}
