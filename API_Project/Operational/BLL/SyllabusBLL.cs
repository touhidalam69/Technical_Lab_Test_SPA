using Operational.DAL;
using Operational.Entity;
using System;
using System.Data;

namespace Operational.BLL
{
  public  class SyllabusBLL
    {
        private SyllabusDAL _SyllabusDAL;
        public SyllabusBLL()
        {
            _SyllabusDAL = new SyllabusDAL();
        }
        public DataTable Syllabus_GetDynamic(string WhereCondition)
        {
            try
            {
                return _SyllabusDAL.Syllabus_GetDynamic(WhereCondition);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetAllActiveLanguage()
        {
            try
            {
                return _SyllabusDAL.GetAllActiveLanguage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetAllActiveLevel()
        {
            try
            {
                return _SyllabusDAL.GetAllActiveLevel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetAllActiveTrade()
        {
            try
            {
                return _SyllabusDAL.GetAllActiveTrade();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Add(Syllabus aSyllabus, int EmployeeId)
        {
            try
            {
                return _SyllabusDAL.Add(aSyllabus, EmployeeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
