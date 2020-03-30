using Amazon.S3.Model;
using AWS_VS13_Demo.Models;
using System;
using System.Collections.Generic;

namespace AWS_Helper.Interface
{
    interface IAwsS3Client
    {
        ResultResponce getBucketList();
        ResultResponce getBucketLocation(string BucketName);
        ResultResponce addBucketTagging(string BucketName, List<Tag> TagList);
        ResultResponce getBuckettagging(string BucketName);
        ResultResponce deleteBucketTagging(string BucketName);
    }
}
