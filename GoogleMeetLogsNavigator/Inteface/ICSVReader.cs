﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMeetLogsNavigator.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICSVReader<T>
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<string, T> MeetingDictionary { get; }
    }
}
