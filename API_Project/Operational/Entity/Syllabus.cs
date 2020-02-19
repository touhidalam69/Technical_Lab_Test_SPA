using System;
using System.Collections.Generic;

namespace Operational.Entity
{
    public class Syllabus
    {
        public int SyllabusId { get; set; }
        public string SyllabusName { get; set; }
        public int TradeId { get; set; }
        public int LevelId { get; set; }
        public string DevelopmentOfficer { get; set; }
        public string Manager { get; set; }
        public DateTime ActiveDate { get; set; }
        public string SyllabusFileName { get; set; }
        public string SyllabusFileLocName { get; set; }
        public string TestplanFileName { get; set; }
        public string TestplanFileLocName { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }
        public int UpdatorId { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<SyllabusDetails> lstSyllabusDetails{ get; set; }
    }
    public class SyllabusDetails
    {
        public int SyllabusDetailsId { get; set; }
        public int SyllabusId { get; set; }
        public int LanguageId { get; set; }
    }
}
