using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.MVC
{
    /// <summary>
    /// Encapsulates a response from reCAPTCHA web service.
    /// </summary>
    public class RecaptchaResponse
    {
        public static readonly RecaptchaResponse Valid = new RecaptchaResponse(true, string.Empty);

        public static readonly RecaptchaResponse InvalidChallenge = new RecaptchaResponse(false,
                                                                                          "Invalid reCAPTCHA request. Missing challenge value.");

        public static readonly RecaptchaResponse InvalidResponse = new RecaptchaResponse(false,
                                                                                         "Invalid reCAPTCHA request. Missing response value.");

        public static readonly RecaptchaResponse InvalidSolution = new RecaptchaResponse(false,
                                                                                         "The verification words are incorrect.");

        public static readonly RecaptchaResponse RecaptchaNotReachable = new RecaptchaResponse(false,
                                                                                               "The reCAPTCHA server is unavailable.");

        private readonly string errorMessage;
        private readonly bool isValid;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecaptchaResponse"/> class.
        /// </summary>
        /// <param name="isValid">Value indicates whether submitted reCAPTCHA is valid.</param>
        /// <param name="errorCode">Error code returned from reCAPTCHA web service.</param>
        internal RecaptchaResponse(bool isValid, string errorMessage)
        {
            RecaptchaResponse templateResponse = null;

            if (IsValid)
            {
                templateResponse = Valid;
            }
            else
            {
                switch (errorMessage)
                {
                    case "incorrect-captcha-sol":
                        templateResponse = InvalidSolution;
                        break;
                    case null:
                        throw new ArgumentNullException("errorMessage");
                }
            }

            if (templateResponse != null)
            {
                this.isValid = templateResponse.IsValid;
                this.errorMessage = templateResponse.ErrorMessage;
            }
            else
            {
                this.isValid = isValid;
                this.errorMessage = errorMessage;
            }
        }

        public bool IsValid
        {
            get { return isValid; }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
        }

        public override bool Equals(object obj)
        {
            var other = (RecaptchaResponse)obj;
            if (other == null)
            {
                return false;
            }

            return other.IsValid == isValid && other.ErrorMessage == errorMessage;
        }

        public override int GetHashCode()
        {
            return isValid.GetHashCode() ^ errorMessage.GetHashCode();
        }
    }
}
