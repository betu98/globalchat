using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobalChat.Models
{
    public class Response<T>
    {
        public Response()
        {
            Data = default(T);
            Success = false;
            ResponseCode = 0;
        }

        public Response(T data)
        {
            Data = data;
            Success = true;
            ResponseCode = 0;
        }

        public Response(T data, bool success)
        {
            Data = data;
            Success = success;
            ResponseCode = 0;
        }

        public Response(T data, bool success, int code)
        {
            Data = data;
            Success = success;
            ResponseCode = code;
        }

        public T Data { get; set; }

        public bool Success { get; set; }

        public int ResponseCode { get; set; }
    }
}