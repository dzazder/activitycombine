using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcxengine.Entities
{
    public class TcxActivity
    {
        public string Id { get; set; }
        public List<TcxLap> Lap { get; set; }
        public string Sport { get; set; }
    }
}
