using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace TechBlog.Web.Common
{
    public static class ReCaptcha
    {
        /// <summary>
        /// Human validator from google. 
        /// </summary>
        /// <param name="response">Request["g-recaptcha-response"]</param>
        /// <param name="errorMesages">Return error messages.</param>
        /// <returns>Return true if is valid.</returns>
        public static bool ValidateCaptcha(string response, out string errorMesages)
        {
            errorMesages = null;
            bool result = false;

            //secret that was generated in key value pair
            const string secret = "XXX";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0)
                {
                    result = true;
                }

                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        errorMesages = "The secret parameter is missing.";
                        break;
                    case ("invalid-input-secret"):
                        errorMesages = "The secret parameter is invalid or malformed.";
                        break;

                    case ("missing-input-response"):
                        errorMesages = "The response parameter is missing.";
                        break;
                    case ("invalid-input-response"):
                        errorMesages = "The response parameter is invalid or malformed.";
                        break;

                    default:
                        errorMesages = "Error occured. Please try again";
                        break;
                }

            }
            else
            {
                errorMesages = "Valid";
                result = true;
            }

            return result;
        }

        public class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
    }
}