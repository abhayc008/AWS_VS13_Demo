using Amazon;
using Amazon.S3.Model;
using AWS_Helper.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AWS_VS13_Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AwsS3Client s3Client = new AwsS3Client();
            var a = s3Client.getBucketLocation("abtests3bucket");
            //var addresult = s3Client.addBucketTagging("abtests3bucket", new List<Tag>() { new Tag() { Key = "abtest", Value = "abtest" } });
            AwsS3Client s3Client1 = new AwsS3Client(a.Data as RegionEndpoint);
            var b = s3Client1.getBuckettagging("abtests3bucket");

            return View();
        }

    }
}
