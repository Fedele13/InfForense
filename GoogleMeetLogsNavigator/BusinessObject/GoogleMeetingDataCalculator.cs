using GoogleMeetLogsNavigator.Interface;
using GoogleMeetLogsNavigator.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMeetLogsNavigator.BO
{
    /// <summary>
    /// 
    /// </summary>
    class GoogleMeetingDataCalculator
    {
        #region private
        
        /// <summary>
        /// 
        /// </summary>
        private ICSVReader<GoogleMeetingTO> _reader = null;

        #endregion

        #region .ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public GoogleMeetingDataCalculator(ICSVReader<GoogleMeetingTO> reader)
        {
            this._reader = reader;

            
        }

        #endregion

        #region prop

        #endregion


    }
}
