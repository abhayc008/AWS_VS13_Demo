using System;
using System.Threading.Tasks;
using AWS_Helper;
using AWS_Helper.Interface;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System.Collections.Generic;
using AWS_VS13_Demo.Models;

namespace AWS_Helper.Concrete
{
    public class AwsS3Client : IAwsS3Client
    {
        private IAmazonS3 s3Client;

        public AwsS3Client(RegionEndpoint bucketRegionName = null)
        {
            BasicAWSCredentials awsCreds = new BasicAWSCredentials(AWS_S3_Constant.AccessKeyId, AWS_S3_Constant.SecretAccessKey);

            AmazonS3Config S3Config = new AmazonS3Config
            {
                RegionEndpoint = bucketRegionName ?? AWS_S3_Constant.bucketRegion  //RegionEndpoint.USEast1
            };

            s3Client = new AmazonS3Client(awsCreds, S3Config);
        }

        public ResultResponce getBucketList()
        {
            ResultResponce result = new ResultResponce();
            try
            {
                ListBucketsResponse responce = s3Client.ListBuckets();
                result.Error = false;
                result.Data = responce.Buckets;
                return result;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }

        public ResultResponce getBucketLocation(string BucketName)
        {
            ResultResponce result = new ResultResponce();
            try
            {
                GetBucketLocationResponse responce = s3Client.GetBucketLocation(new GetBucketLocationRequest() { BucketName = BucketName });
                var a = RegionEndpoint.GetBySystemName(responce.Location);
                result.Error = false;
                result.Data = responce.Location;
                return result;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }

        public ResultResponce addBucketTagging(string BucketName, List<Tag> TagList)
        {
            ResultResponce result = new ResultResponce();

            if (string.IsNullOrEmpty(BucketName) || string.IsNullOrWhiteSpace(BucketName))
            {
                result.Error = true;
                result.ErrorMessage = "Please provide valid bucket Name.";
                return result;
            }

            if (TagList.Count == 0)
            {
                result.Error = true;
                result.ErrorMessage = "Please provide Tag";
                return result;
            }

            try
            {
                PutBucketTaggingResponse responce = s3Client.PutBucketTagging(new PutBucketTaggingRequest { BucketName = BucketName, TagSet = TagList });
                result.Data = "Tags created successfully.";
                result.Error = false;
                return result;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }

        public ResultResponce getBuckettagging(string BucketName)
        {
            ResultResponce result = new ResultResponce();

            if (string.IsNullOrEmpty(BucketName) || string.IsNullOrWhiteSpace(BucketName))
            {
                result.Error = true;
                result.ErrorMessage = "Please provide valid bucket Name.";
                return result;
            }
            try
            {
                GetBucketTaggingResponse responce = s3Client.GetBucketTagging(new GetBucketTaggingRequest() { BucketName = BucketName });

                result.Error = false;
                result.Data = responce.TagSet;
                return result;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }

        public ResultResponce deleteBucketTagging(string BucketName)
        {
            ResultResponce result = new ResultResponce();

            if (string.IsNullOrEmpty(BucketName) || string.IsNullOrWhiteSpace(BucketName))
            {
                result.Error = true;
                result.ErrorMessage = "Please provide valid bucket Name.";
                return result;
            }

            try
            {

                DeleteBucketTaggingResponse responce = s3Client.DeleteBucketTagging(BucketName);

                result.Error = false;
                result.Data = responce.HttpStatusCode;
                return result;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }
    }
}
