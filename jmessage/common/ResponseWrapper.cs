using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using jmessage.util;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jmessage.common
{
    public class ResponseWrapper
    {
        private const int RESPONSE_CODE_NONE = -1;
        public HttpStatusCode responseCode = HttpStatusCode.BadRequest;
        private String _responseContent;
        public String responseContent
        {
            get
            {
                return _responseContent;
            }
            set
            {
                _responseContent = value;
            }
        }
        public void setErrorObject()
        {
            if (!string.IsNullOrEmpty(_responseContent))
            {
            }
        }

        public int rateLimitQuota;
        public int rateLimitRemaining;
        public int rateLimitReset;

        public bool isServerResponse()
        {
            return responseCode == HttpStatusCode.OK;
        }
        public String exceptionString;

        public ResponseWrapper()
        {
        }

    }
}
