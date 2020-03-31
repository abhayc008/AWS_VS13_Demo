using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AWS_VS13_Demo.Models
{
    public class AddTag
    {
        [Required]
        public string BucketName { get; set; }

        [Required]
        public string key { get; set; }

        [Required]
        public string value { get; set; }
    }
}