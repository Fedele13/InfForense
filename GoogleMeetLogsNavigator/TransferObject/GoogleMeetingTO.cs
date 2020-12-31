using GoogleMeetLogsNavigator.TransferObject.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GoogleMeetLogsNavigator.TransferObject
{
    /// <summary>
    /// Google Meeting representation Transfer Object
    /// </summary>
    public class GoogleMeetingTO : IList<IGoogleMeetLogTO>
    {
        #region private

        /// <summary>
        /// The Meeting Code
        /// </summary>
        private string _meetingCode = string.Empty;
        
        /// <summary>
        /// The Google meeing Logs list
        /// </summary>
        private IList<IGoogleMeetLogTO> _googleMeetingLogs = new List<IGoogleMeetLogTO>();

        #endregion

        #region .ctor

        /// <summary>
        /// The Constructur
        /// </summary>
        /// <param name="meetingCode">The Meeting Code</param>
        public GoogleMeetingTO(string meetingCode)
        {
            this._meetingCode = meetingCode;
        }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="meetingCode">The Meeting Code</param>
        /// <param name="logs">The Logs list</param>
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
        /// The Meeting Code prop
        /// </summary>
        public string MeetingCode { get => this._meetingCode; }

        /// <summary>
        /// The instance
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>The Log</returns>
        public IGoogleMeetLogTO this[int index] 
        {
            get => this._googleMeetingLogs[index]; 
            set => this._googleMeetingLogs[index] = value; 
        }

        /// <summary>
        /// The list count
        /// </summary>
        public int Count => this._googleMeetingLogs.Count;

        /// <summary>
        /// True if it is read only, false otherwise
        /// </summary>
        public bool IsReadOnly => this._googleMeetingLogs.IsReadOnly;

        #endregion

        #region methods

        /// <summary>
        /// Add log into list
        /// </summary>
        /// <param name="item">The Log</param>
        /// <exception cref="InvalidOperationException">
        public void Add(IGoogleMeetLogTO item)
        {
            if (item.MeetingCode != item.MeetingCode)
            {
                throw new InvalidOperationException("The item meeting code is not equal to this meeting code");    
            }

            this._googleMeetingLogs.Add(item);
        }

        /// <summary>
        /// Clear the list
        /// </summary>
        public void Clear()
        {
            this._googleMeetingLogs.Clear();
        }

        /// <summary>
        /// True if item is already into list, false otherwise
        /// </summary>
        /// <param name="item">The Item</param>
        /// <returns>True if item is already into list, false otherwise</returns>
        public bool Contains(IGoogleMeetLogTO item)
        {
            return this._googleMeetingLogs.Contains(item);
        }

        /// <summary>
        /// Copy the instance into input array, starting froma arrayindex
        /// </summary>
        /// <param name="array">The output array</param>
        /// <param name="arrayIndex">The starting array index</param>
        public void CopyTo(IGoogleMeetLogTO[] array, int arrayIndex)
        {
            this._googleMeetingLogs.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Get list enumerator
        /// </summary>
        /// <returns>IEnumerator<IGoogleMeetLogTO> </returns>
        public IEnumerator<IGoogleMeetLogTO> GetEnumerator()
        {
            return this._googleMeetingLogs.GetEnumerator();
        }

        /// <summary>
        /// Get the position of input item
        /// </summary>
        /// <param name="item">The log</param>
        /// <returns>The index</returns>
        public int IndexOf(IGoogleMeetLogTO item)
        {
            return this._googleMeetingLogs.IndexOf(item);
        }

        /// <summary>
        /// Insert the input item into input index into list
        /// </summary>
        /// <param name="index">The index where to insert the input item</param>
        /// <param name="item">The log to be inserted</param>
        public void Insert(int index, IGoogleMeetLogTO item)
        {
            if (item.MeetingCode != item.MeetingCode)
            {
                throw new InvalidOperationException("The item meeting code is not equal to this meeting code");
            }

            this._googleMeetingLogs.Insert(index, item);
        }

        /// <summary>
        /// Remove the input log from list
        /// </summary>
        /// <param name="item">The log to be removed</param>
        /// <returns>True if the input log has been removed, false otherwise</returns>
        public bool Remove(IGoogleMeetLogTO item)
        {
            return this._googleMeetingLogs.Remove(item);
        }

        /// <summary>
        /// Remove the log at input index
        /// </summary>
        /// <param name="index">The index</param>
        public void RemoveAt(int index)
        {
            this._googleMeetingLogs.RemoveAt(index);
        }

        /// <summary>
        /// Get the IEnumerator
        /// </summary>
        /// <returns>IEnumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._googleMeetingLogs.GetEnumerator();
        }

        #endregion
    }
}
