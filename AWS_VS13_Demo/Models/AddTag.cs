using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS_VS13_Demo.Models
{
    public class AddTag
    {
        public string BucketName { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
}