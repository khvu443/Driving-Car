using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.script
{
    public class CarObj
    {
        public float force { get; set; }
        public int score { get; set; }

        //public float speed { get; set; }


        public CarObj() { } 
        public CarObj (int score) {
            this.score = score;
        }
    }
}
