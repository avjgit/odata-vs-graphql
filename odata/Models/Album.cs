using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odata.Models
{
    public class Album
    {
        public Album()
        {
            Songs = new List<Song>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
