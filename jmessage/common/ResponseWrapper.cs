using System;
using System.Net;

namespace jmessage.common
{
    public class ResponseWrapper
    {
        public HttpStatusCode responseCode = HttpStatusCode.BadRequest;

        private const int RESPONSE_CODE_NONE = -1;
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
