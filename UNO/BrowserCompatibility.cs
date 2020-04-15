using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO
{
    public class BrowserCompatibility
    {
        #region IsUplevel Browser property
        private enum UpLevel { chrome, firefox, safari, ie }

        public static bool IsUplevel
        {
            get
            {
                bool ret = false;
                string _browser;

                try
                {

                    if (HttpContext.Current == null) return ret;
                    _browser = HttpContext.Current.Request.UserAgent.ToLower();

                    foreach (UpLevel br in Enum.GetValues(typeof(UpLevel)))
                    { if (_browser.Contains(br.ToString())) { ret = true; break; } }

                    return ret;
                }
                catch { return ret; }
            }
        }
        #endregion
    }
}