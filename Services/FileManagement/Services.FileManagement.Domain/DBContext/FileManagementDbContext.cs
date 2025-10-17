using Microsoft.EntityFrameworkCore;
using Services.FileManagement.Domain.Entities;
using System.Collections.Generic;


namespace Services.FileManagement.Domain.DBContext
{
    public class FileManagementDbContext : DbContext
    {
        public FileManagementDbContext(DbContextOptions<FileManagementDbContext> options) : base(options) {  }


        public DbSet<MediaFile> MediaFile { get; set; }
        public DbSet<UploaderInfo> UploaderInfo { get; set; }
        public DbSet<DownloaderInfo> DownloaderInfo { get; set; }
        public DbSet<FileDetails> FileDetails { get; set; }
        public DbSet<UploaderErrorLog> UploaderErrorLog { get; set; }




    
    }
}
