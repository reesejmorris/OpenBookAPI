using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Models
{
    public class Vote
    {
        public Guid Id { get; set; }
        /// <summary>
        /// As we use Guids for all id's we can use this for any item, not just snippets
        /// </summary>
        public Guid ItemId { get; set; }
        /// <summary>
        /// (Up vote = 1, Down vote = -1)
        /// Maybe in future we can have different values for pro members, that's why this is not a bool or enum
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Id of user who registered the vote, so we can stop people voting on an item more than once
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// Id for logged out users 
        /// </summary>
        public string CookieId { get; set; }
    }
}
