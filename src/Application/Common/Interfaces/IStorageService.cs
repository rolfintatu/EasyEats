using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace Application.Common.Interfaces
{
    public interface IStorageService
    {
        Task<Uri> UploadImage(byte[] photoBase64String, string extension);
        Task DeleteImage(string imageName);
    }
}
