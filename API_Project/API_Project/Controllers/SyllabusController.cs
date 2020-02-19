using Newtonsoft.Json;
using Operational.BLL;
using Operational.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace API_Project.Controllers
{
    public class SyllabusController : ApiController
    {
        [Route("api/Syllabus/GetAllLanguageTradeLevelList")]
        [HttpGet]
        public IHttpActionResult GetAllLanguageTradeLevelList()
        {
            try
            {
                var result = new
                {
                    LanguageList = JsonConvert.SerializeObject(new SyllabusBLL().GetAllActiveLanguage()),
                    TradeList = JsonConvert.SerializeObject(new SyllabusBLL().GetAllActiveTrade()),
                    LevelList = JsonConvert.SerializeObject(new SyllabusBLL().GetAllActiveLevel())
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        [Route("api/Syllabus/Syllabus_GetDynamic")]
        [HttpGet]
        public IHttpActionResult Syllabus_GetDynamic(string WhereCondition)
        {
            try
            {
                var result = new
                {
                    SyllabusList = JsonConvert.SerializeObject(new SyllabusBLL().Syllabus_GetDynamic(WhereCondition))
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        [Route("api/Syllabus/UploadFiles")]
        [HttpPost]
        public HttpResponseMessage UploadFiles()
        {
            try
            {
                //Create the Directory.
                string path = HttpContext.Current.Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string Newfilename = Guid.NewGuid().ToString();
                //Save the Files.
                foreach (string key in HttpContext.Current.Request.Files)
                {
                    HttpPostedFile postedFile = HttpContext.Current.Request.Files[key];
                    string exttension = System.IO.Path.GetExtension(postedFile.FileName).ToUpper();
                    if (exttension == ".PDF" || exttension == ".DOC" || exttension == ".DOCX")
                    {
                        postedFile.SaveAs(path + Newfilename + exttension);
                        return Request.CreateResponse(Newfilename + exttension);
                    }
                    else
                    {
                        return Request.CreateResponse("File Extension Problem");
                    }
                }

                //Send OK Response to Client.
                return Request.CreateResponse(Newfilename);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(ex.Message);
            }
        }

        [Route("api/Syllabus/GetFile")]
        [HttpGet]
        public HttpResponseMessage GetFile(string fileName, string oName)
        {
            if (String.IsNullOrEmpty(fileName))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

                string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            //   string fileName;
            string localFilePath = path + fileName;
            int fileSize;

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = oName;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }

        [Route("api/Syllabus/Add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]string jsonString)
        {
            try
            {
                Syllabus aSyllabus = JsonConvert.DeserializeObject<Syllabus>(jsonString);
                bool result = new SyllabusBLL().Add(aSyllabus, 1);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
