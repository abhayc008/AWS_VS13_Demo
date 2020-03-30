using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS_VS13_Demo.Models
{
    public class ResultResponce
    {
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }
}