
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gIdeas.Models
{
    /// <summary>
    ///  Store the user's last login time and the browser they used
    /// </summary>
    public class gLoginRecord
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "nvarchar(100)")]
        public string BrowserName { get; set; }

        public int UserId { get; set; }
    }

    public class gBrowser
    {
        public string Name { get; set; }
        public int TotalHits { get; set; }
    }
}