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
            media_id = null;
            media_crc32 = 0;
            width = 0;
            height = 0;
            format = null;
            fsize = 0;
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
            media_id = null;
            media_crc32 = 0;
            fname = null;
            fsize = 0;
            hash = null;
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
