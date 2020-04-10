using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos
{
    public class ImageStorageDto
    {
        public ImageStorageDto(byte[] byteArray, string extension)
        {
            ByteArray = byteArray;
            Extension = extension;
        }

        public byte[] ByteArray { get; set; }
        public string Extension { get; set; }
    }
}
