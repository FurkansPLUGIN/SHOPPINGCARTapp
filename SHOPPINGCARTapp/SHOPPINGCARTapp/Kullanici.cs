using System;
using System.Collections.Generic;
using System.Text;
using Realms;
namespace fingerDraw
{
   public  class Kullanici:RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string items { get; set; }
    }
}
