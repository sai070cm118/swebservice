using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Entities
{
    public class MongoProfile
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

    }
}
