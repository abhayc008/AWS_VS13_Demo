using Amazon;
using Amazon.S3.Model;
using AWS_Helper.Concrete;
using AWS_VS13_Demo.Models;
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

            List<Tag> Tags = new List<Tag>();
            List<S3Bucket> buckets = new List<S3Bucket>();
            var result = s3Client.getBucketList();

            if (!result.Error)
                buckets = result.Data as List<S3Bucket>;

            ViewBag.Buckets = buckets;

            //var a = s3Client.getBucketLocation("abtests3bucket");
            ////var addresult = s3Client.addBucketTagging("abtests3bucket", new List<Tag>() { new Tag() { Key = "abtest", Value = "abtest" } });
            //AwsS3Client s3Client1 = new AwsS3Client(a.Data as RegionEndpoint);
            //var b = s3Client1.getBuckettagging("abtests3bucket");

            return View(Tags);
        }

        [HttpPost]
        public ActionResult SearchTag(string BucketName)
        {
            AwsS3Client s3Client = new AwsS3Client();

            List<S3Bucket> buckets = new List<S3Bucket>();
            List<Tag> Tags = new List<Tag>();

            var result = s3Client.getBucketList();

            if (!result.Error)
                buckets = result.Data as List<S3Bucket>;

            ViewBag.Buckets = buckets;

            var tagresult = s3Client.getBuckettagging(BucketName);

            if (!tagresult.Error)
                Tags = tagresult.Data as List<Tag>;

            return View("Index", Tags);
        }

        [HttpGet]
        public ActionResult AddTag()
        {
            AwsS3Client s3Client = new AwsS3Client();
            List<S3Bucket> buckets = new List<S3Bucket>();
            var result = s3Client.getBucketList();
            if (!result.Error)
                buckets = result.Data as List<S3Bucket>;
            ViewBag.Buckets = buckets;

            return View();
        }

        [HttpPost]
        public ActionResult AddTag(AddTag objAddTag)
        {

            AwsS3Client s3Client = new AwsS3Client();
            List<S3Bucket> buckets = new List<S3Bucket>();
            List<Tag> Tags = new List<Tag>();
            var result = s3Client.getBucketList();
            if (!result.Error)
                buckets = result.Data as List<S3Bucket>;
            ViewBag.Buckets = buckets;

            var existingTagsResult = s3Client.getBuckettagging(objAddTag.BucketName);
            if (!existingTagsResult.Error)
                Tags = existingTagsResult.Data as List<Tag>;

            Tags.Add(new Tag() { Key = objAddTag.key, Value = objAddTag.value });

            var AddTagsResult = s3Client.addBucketTagging(objAddTag.BucketName, Tags);

            if(!AddTagsResult.Error)
            {
                RedirectToAction("SearchTag", new { BucketName = objAddTag.BucketName });
            }

            return View();
        }

    }
}
