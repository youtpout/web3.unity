﻿using System.Collections.Generic;

namespace ChainSafe.Gaming.UnityPackage.Model
{
    public class IPFSUploadRequestModel
    {
        public string ApiKey { get; set; }
        public string BucketId { get; set; }
        public string FileNameMetaData { get; set; }
        public string FileNameImage { get; set; }
        public string Description { get; set; }
        public string External_url { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public List<Attributes> attributes { get; set; }

        public class Attributes
        {
            public List<string> Display_types { get; set; }
            public List<string> Trait_types { get; set; }
            public List<string> Values { get; set; }
        }
    }
    
    public class GetFileInfoResponse
    {
        [System.Serializable]
        public class Content
        {
            public string cid;
        }

        public Content content;
    }
}