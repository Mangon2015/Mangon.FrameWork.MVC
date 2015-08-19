using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mangon.FrameWork.MVC
{
    public class RecaptchaValidator
    {
        private const string VerifyUrl = "http://www.google.com/recaptcha/api/verify";

        private string challenge;

        private IWebProxy proxy;
        private string remoteIp;
        private string response;

        public string PrivateKey { get; set; }

        public string RemoteIP
        {
            get { return remoteIp; }

            set
            {
                IPAddress ip = IPAddress.Parse(value);

                if (ip == null ||
                    (ip.AddressFamily != AddressFamily.InterNetwork &&
                     ip.AddressFamily != AddressFamily.InterNetworkV6))
                {
                    throw new ArgumentException("Expecting an IP address, got " + ip);
                }

                remoteIp = ip.ToString();
            }
        }

        public string Challenge
        {
            get { return challenge; }
            set { challenge = value; }
        }

        public string Response
        {
            get { return response; }
            set { response = value; }
        }

        public IWebProxy Proxy
        {
            get { return proxy; }
            set { proxy = value; }
        }

        private void CheckNotNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public RecaptchaResponse Validate()
        {
            CheckNotNull(PrivateKey, "PrivateKey");
            CheckNotNull(RemoteIP, "RemoteIp");
            CheckNotNull(Challenge, "Challenge");
            CheckNotNull(Response, "Response");

            if (challenge == string.Empty || response == string.Empty)
            {
                return RecaptchaResponse.InvalidSolution;
            }

            var request = (HttpWebRequest)WebRequest.Create(VerifyUrl);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Timeout = 30 * 1000 /* 30 seconds */;
            request.Method = "POST";
            request.UserAgent = "reCAPTCHA/ASP.NET";
            if (proxy != null)
            {
                request.Proxy = proxy;
            }

            request.ContentType = "application/x-www-form-urlencoded";

            string formdata = String.Format(
                "privatekey={0}&remoteip={1}&challenge={2}&response={3}",
                HttpUtility.UrlEncode(PrivateKey),
                HttpUtility.UrlEncode(RemoteIP),
                HttpUtility.UrlEncode(Challenge),
                HttpUtility.UrlEncode(Response));

            byte[] formbytes = Encoding.ASCII.GetBytes(formdata);

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formbytes, 0, formbytes.Length);
            }

            string[] results;

            try
            {
                using (WebResponse httpResponse = request.GetResponse())
                {
                    using (TextReader readStream = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        results = readStream.ReadToEnd().Split(new[] { "\n", "\\n" },
                                                               StringSplitOptions.RemoveEmptyEntries);
                    }
                }
            }
            catch (WebException ex)
            {
                EventLog.WriteEntry("Application", ex.Message, EventLogEntryType.Error);
                return RecaptchaResponse.RecaptchaNotReachable;
            }

            switch (results[0])
            {
                case "true":
                    return RecaptchaResponse.Valid;
                case "false":
                    return new RecaptchaResponse(false, results[1].Trim(new[] { '\'' }));
                default:
                    throw new InvalidProgramException("Unknown status response.");
            }
        }
    }
}
