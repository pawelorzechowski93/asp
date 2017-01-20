using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskDataAccess;

namespace AspTaskService.Controllers
{
    public class TaskController : ApiController
    {
        public IEnumerable<task> Get()
        {
            using (TaskDBEntities entities = new TaskDBEntities())
            {
                return entities.task.ToList();
            }
        }


        public HttpResponseMessage Get(int id)
        {
            using (TaskDBEntities entities = new TaskDBEntities())
            {
                var entity = entities.task.FirstOrDefault(e => e.id == id);

                if (entity != null)
                {

                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Task not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] task Task)
        {
            try
            {
                using (TaskDBEntities entities = new TaskDBEntities())
                {
                    entities.task.Add(Task);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, Task);
                    return message;
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            using (TaskDBEntities entities = new TaskDBEntities())
            {
                try
                {

                    var entity = entities.task.FirstOrDefault(e => e.id == id);

                    if (entity != null)
                    {
                        entities.task.Remove(entities.task.FirstOrDefault(e => e.id == id));
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);

                    }

                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Task not found");
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }


        }

        public HttpResponseMessage Put(int id, [FromBody] task Task)
        {
            try {
                using (TaskDBEntities entities = new TaskDBEntities())
                {


                    var entity = entities.task.FirstOrDefault(e => e.id == id);

                    if (entity != null)
                    {
                        DateTime sd = entity.start_date;
                        DateTime ed = entity.end_date;
                        if (Task.name != null)
                        {
                            entity.name = Task.name;
                        }

                        if (Task.descryption != null)
                        {
                            entity.descryption = Task.descryption;
                        }

                        if (Task.start_date.ToString() != "0001-01-01 00:00:00")
                        {
                            entity.start_date = Task.start_date;
                        }
                      

                        if (Task.end_date.ToString() != "0001-01-01 00:00:00")
                        {
                            entity.end_date = Task.end_date;
                        }

                       


                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found");
                    }
                }
                
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }

        }


    }
}
