using System;
using System.Collections;
using System.Collections.Generic;

namespace GMCSVReaderPrototype.TO
{
    /// <summary>
    /// Google Meeting rappresentation
    /// </summary>
    public class GoogleMeetingTO : IList<GoogleMeetLogTO>
    {
        #region private

        /// <summary>
        /// 
        /// </summary>
        private string _meetingCode = string.Empty;
        
        /// <summary>
        /// 
        /// </summary>
        private IList<GoogleMeetLogTO> _googleMeetingLogs = new List<GoogleMeetLogTO>();

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
        public GoogleMeetingTO(string meetingCode, IList<GoogleMeetLogTO> logs)
        {
            this._meetingCode = meetingCode;

            if (logs != null && logs.Count > 0)
            {
                foreach (GoogleMeetLogTO log in logs)
                {
                    if (log.MeetingCode != meetingCode)
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
        /// <param name="index"></param>
        /// <returns></returns>
        public GoogleMeetLogTO this[int index] 
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
        public void Add(GoogleMeetLogTO item)
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
        public bool Contains(GoogleMeetLogTO item)
        {
            return this._googleMeetingLogs.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(GoogleMeetLogTO[] array, int arrayIndex)
        {
            this._googleMeetingLogs.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<GoogleMeetLogTO> GetEnumerator()
        {
            return this._googleMeetingLogs.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(GoogleMeetLogTO item)
        {
            return this._googleMeetingLogs.IndexOf(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, GoogleMeetLogTO item)
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
        public bool Remove(GoogleMeetLogTO item)
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
