using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.message
{


    class ImagePayload
    {

        public string media_id;
        public long media_crc32;
        public int width;
        public int height;
        public string format;
        public int fsize;

        public ImagePayload()
        {
            this.media_id = null;
            this.media_crc32 = 0;
            this.width = 0;
            this.height = 0;
            this.format = null;
            this.fsize = 0;
        }

        public ImagePayload(string media_id, long media_crc32, int width, int height, string format, int fsize)
        {
            this.media_id = media_id;
            this.media_crc32 = media_crc32;
            this.width = width;
            this.height = height;
            this.format = format;
            this.fsize = fsize;
        }
    }

    class FilePayload
    {

        public string media_id;
        public long media_crc32;
        public string fname;
        public int fsize;
        public string hash;

        public FilePayload()
        {
            this.media_id = null;
            this.media_crc32 = 0;
            this.fname = null;
            this.fsize = 0;
            this.hash = null;
        }

        public FilePayload(string media_id, long media_crc32, string fname, int fsize, string hash)
        {
            this.media_id = media_id;
            this.media_crc32 = media_crc32;
            this.fname = fname;
            this.fsize = fsize;
            this.hash = hash;
        }
    }


}
