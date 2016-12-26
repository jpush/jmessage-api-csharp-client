using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jmessage.common;
using jmessage.user;

namespace jmessage
{
    /// <summary>
    /// Main Entrance - 该类为JPush服务的主要入口
    /// </summary>
    public class JMessageClient { 
        public UserClient _messageClient;
        /// <param name="app_key">Portal上产生的app_key</param>
        /// <param name="masterSecret">你的API MasterSecret</param>
        public JMessageClient(String app_key, String masterSecret)
        {
            this._messageClient= new UserClient(app_key, masterSecret);
        } 
    }
}
