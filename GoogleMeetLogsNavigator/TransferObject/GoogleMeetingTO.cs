﻿using GoogleMeetLogsNavigator.TransferObject.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GoogleMeetLogsNavigator.TransferObject
{
    /// <summary>
    /// Google Meeting rappresentation
    /// </summary>
    public class GoogleMeetingTO : IList<IGoogleMeetLogTO>
    {
        #region private

        /// <summary>
        /// 
        /// </summary>
        private string _meetingCode = string.Empty;
        
        /// <summary>
        /// 
        /// </summary>
        private IList<IGoogleMeetLogTO> _googleMeetingLogs = new List<IGoogleMeetLogTO>();

        #endregion

        #region .ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meetingCode"></param>
        public GoogleMeetingTO(string meetingCode)
        {
            this._meetingCode = meetingCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meetingCode"></param>
        /// <param name="logs"></param>
        public GoogleMeetingTO(string meetingCode, IList<IGoogleMeetLogTO> logs)
        {
            this._meetingCode = meetingCode;

            if (logs != null && logs.Count > 0)
            {
                foreach (IGoogleMeetLogTO log in logs)
                {
                    if (log.MeetingCode != this._meetingCode)
                    {
                        continue;
                    }
                    this._googleMeetingLogs.Add(log);
                }
            }
        }

        #endregion

        #region prop

        /// <summary>
        /// 
        /// </summary>
        public string MeetingCode { get => this._meetingCode; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IGoogleMeetLogTO this[int index] 
        {
            get => this._googleMeetingLogs[index]; 
            set => this._googleMeetingLogs[index] = value; 
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count => this._googleMeetingLogs.Count;

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly => this._googleMeetingLogs.IsReadOnly;

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(IGoogleMeetLogTO item)
        {
            if (item.MeetingCode != item.MeetingCode)
            {
                throw new InvalidOperationException("The item meeting code is not equal to this meeting code");    
            }

            this._googleMeetingLogs.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            this._googleMeetingLogs.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(IGoogleMeetLogTO item)
        {
            return this._googleMeetingLogs.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(IGoogleMeetLogTO[] array, int arrayIndex)
        {
            this._googleMeetingLogs.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IGoogleMeetLogTO> GetEnumerator()
        {
            return this._googleMeetingLogs.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(IGoogleMeetLogTO item)
        {
            return this._googleMeetingLogs.IndexOf(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, IGoogleMeetLogTO item)
        {
            if (item.MeetingCode != item.MeetingCode)
            {
                throw new InvalidOperationException("The item meeting code is not equal to this meeting code");
            }

            this._googleMeetingLogs.Insert(index, item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(IGoogleMeetLogTO item)
        {
            return this._googleMeetingLogs.Remove(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            this._googleMeetingLogs.RemoveAt(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._googleMeetingLogs.GetEnumerator();
        }

        #endregion
    }
}
