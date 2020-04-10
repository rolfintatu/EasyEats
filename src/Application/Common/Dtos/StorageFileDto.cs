using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos
{
    public class StorageFileDto
    {

        public StorageFileDto(string id, string name, long? size, long? version, DateTime? createdTime)
            => (Id, Name, Size, Version, CreatedTime) = (id, name, size, version, createdTime);

        public string Id { get; set; }
        public string Name { get; set; }
        public long? Size { get; set; }
        public long? Version { get; set; }
        public DateTime? CreatedTime { get; set; }

    }
}
