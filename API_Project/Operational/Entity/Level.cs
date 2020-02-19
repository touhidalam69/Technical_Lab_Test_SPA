using System;

namespace Operational.Entity
{
    public class Level
    {
        public int LevelId { get; set; }
        public int TradeId { get; set; }
        public string LevelName { get; set; }
        public bool IsActive { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }
        public int UpdatorId { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
