using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedBuilder.FTP
{
    public enum FtpUploadTags
    {
        ITunesUpload = 1,
        NonItunesUpload = 2,
        ImageUpload = 4,
        EnclosureUpload = 8
    }
}
