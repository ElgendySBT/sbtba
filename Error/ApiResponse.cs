using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Error
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode,string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400=>"Bad Request ,you have made",
                401=>"Authorized ,you are not",
                404=> "The requested resource could not be found",
                500=>"Errors are the path to dark side",
                _=>null
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
