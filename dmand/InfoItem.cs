using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmand
{
    public abstract class InfoItem
    {
        protected readonly FileSystemInfo Info;
        protected InfoItem( FileSystemInfo info )
        {
            Info = info;
        }

        public string FullName => Info.FullName;

        public string Name => Info.Name;

        public DateTime LastWriteTime => Info.LastWriteTime;

        public virtual bool SupportsLength => false;

        public virtual long Length => -1;

        public string Extension => Info.Extension;
    }

    public class DirectoryInfoItem : InfoItem
    {
        public DirectoryInfoItem( string path ) : base( new DirectoryInfo( path ) )
        {
            // Nothing to do
        }
    }

    public class FileInfoItem : InfoItem
    {
        public FileInfoItem( string path ) : base( new FileInfo( path ) )
        {
            // Nothing to do
        }

        public override bool SupportsLength => true;

        public override long Length => ( Info as FileInfo ).Length;
    }
}
