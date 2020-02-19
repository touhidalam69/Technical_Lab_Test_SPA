using Operational.Entity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Operational.DAL
{
    public class SyllabusDAL
    {
        public DataTable Syllabus_GetDynamic(string WhereCondition)
        {
            using (SqlConnection objConnection = Connection.GetConnection())
            {
                SqlTransaction transaction = objConnection.BeginTransaction(IsolationLevel.ReadCommitted, "SampleTransaction");
                string query = "Syllabus_GetDynamic";
                SqlCommand sqCmd = new SqlCommand(query, objConnection, transaction);
                sqCmd.CommandType = CommandType.StoredProcedure;
                sqCmd.Parameters.AddWithValue("@WhereCondition", WhereCondition);
                DataTable dt = new DataTable();
                dt.Load(sqCmd.ExecuteReader());
                objConnection.Close();
                return dt;
            }
        }
        public DataTable GetAllActiveLanguage()
        {
            using (SqlConnection objConnection = Connection.GetConnection())
            {
                SqlTransaction transaction = objConnection.BeginTransaction(IsolationLevel.ReadCommitted, "SampleTransaction");
                string query = "GetAllActiveLanguage";
                SqlCommand sqCmd = new SqlCommand(query, objConnection, transaction);
                sqCmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                dt.Load(sqCmd.ExecuteReader());
                objConnection.Close();
                return dt;
            }
        }
        public DataTable GetAllActiveLevel()
        {
            using (SqlConnection objConnection = Connection.GetConnection())
            {
                SqlTransaction transaction = objConnection.BeginTransaction(IsolationLevel.ReadCommitted, "SampleTransaction");
                string query = "GetAllActiveLevel";
                SqlCommand sqCmd = new SqlCommand(query, objConnection, transaction);
                sqCmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                dt.Load(sqCmd.ExecuteReader());
                objConnection.Close();
                return dt;
            }
        }
        public DataTable GetAllActiveTrade()
        {
            using (SqlConnection objConnection = Connection.GetConnection())
            {
                SqlTransaction transaction = objConnection.BeginTransaction(IsolationLevel.ReadCommitted, "SampleTransaction");
                string query = "GetAllActiveTrade";
                SqlCommand sqCmd = new SqlCommand(query, objConnection, transaction);
                sqCmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                dt.Load(sqCmd.ExecuteReader());
                objConnection.Close();
                return dt;
            }
        }
        public bool Add(Syllabus aSyllabus, int EmployeeId)
        {
            using (SqlConnection objConnection = Connection.GetConnection())
            {
                SqlTransaction transaction = objConnection.BeginTransaction();
                try
                {
                    string query = aSyllabus.SyllabusId == 0 ? "Syllabus_Create" : "Syllabus_Update";
                    SqlCommand sqCmd = new SqlCommand(query, objConnection, transaction);
                    sqCmd.CommandType = CommandType.StoredProcedure;
                    if (aSyllabus.SyllabusId > 0)
                    {
                        sqCmd.Parameters.AddWithValue("@SyllabusId", aSyllabus.SyllabusId);
                    }

                    sqCmd.Parameters.AddWithValue("@SyllabusName", aSyllabus.SyllabusName);
                    sqCmd.Parameters.AddWithValue("@TradeId", aSyllabus.TradeId);
                    sqCmd.Parameters.AddWithValue("@LevelId", aSyllabus.LevelId);
                    sqCmd.Parameters.AddWithValue("@DevelopmentOfficer", aSyllabus.DevelopmentOfficer);
                    sqCmd.Parameters.AddWithValue("@Manager", aSyllabus.Manager);
                    sqCmd.Parameters.AddWithValue("@ActiveDate", aSyllabus.ActiveDate);
                    sqCmd.Parameters.AddWithValue("@SyllabusFileName", string.IsNullOrEmpty( aSyllabus.SyllabusFileName)?"" : aSyllabus.SyllabusFileName);
                    sqCmd.Parameters.AddWithValue("@SyllabusFileLocName", string.IsNullOrEmpty(aSyllabus.SyllabusFileLocName) ? "" : aSyllabus.SyllabusFileLocName);
                    sqCmd.Parameters.AddWithValue("@TestplanFileName", string.IsNullOrEmpty(aSyllabus.TestplanFileName) ? "" : aSyllabus.TestplanFileName);
                    sqCmd.Parameters.AddWithValue("@TestplanFileLocName", string.IsNullOrEmpty(aSyllabus.TestplanFileLocName) ? "" : aSyllabus.TestplanFileLocName);
                    sqCmd.Parameters.AddWithValue("@CreatorId", EmployeeId);
                    if (aSyllabus.SyllabusId > 0)
                    {
                        sqCmd.ExecuteNonQuery();
                        query = "SyllabusDetails_DeleteBySyllabusId";
                        sqCmd = new SqlCommand(query, objConnection, transaction);
                        sqCmd.CommandType = CommandType.StoredProcedure;
                        sqCmd.Parameters.AddWithValue("@SyllabusId", aSyllabus.SyllabusId);
                        sqCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        aSyllabus.SyllabusId = (Int32)(decimal)sqCmd.ExecuteScalar();
                    }
                    foreach (SyllabusDetails aSyllabusDetails in aSyllabus.lstSyllabusDetails)
                    {
                        query = "SyllabusDetails_Create";
                        sqCmd = new SqlCommand(query, objConnection, transaction);
                        sqCmd.CommandType = CommandType.StoredProcedure;
                        sqCmd.Parameters.AddWithValue("@SyllabusId", aSyllabus.SyllabusId);
                        sqCmd.Parameters.AddWithValue("@LanguageId", aSyllabusDetails.LanguageId);
                        sqCmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return true;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
