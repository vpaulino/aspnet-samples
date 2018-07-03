using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Validation;
using Newtonsoft.Json;
using WebApiHost.Models;

namespace WebApiHost.Controllers
{
    
    public class ProcessController : ApiController
    {
        #region Public ctor
        
        

        //protected override void Initialize(HttpControllerContext controllerContext)
        //{
        //    try
        //    {
        //        var defaultModelValidator = controllerContext.Configuration.Services.GetBodyModelValidator();
        //        //controllerContext.Configuration.Services.Clear(typeof(IBodyModelValidator));
        //        controllerContext.Configuration.Services.Replace(typeof(IBodyModelValidator), new CustomModelValidator());

                
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
           
        //}

        public override async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            Stopwatch clock = new Stopwatch();
            try
            {
                clock.Start();
                Console.WriteLine($"################################### [{DateTime.UtcNow}] ProcessController.ExecuteAsync Started ###################################");
                var response = await base.ExecuteAsync(controllerContext, cancellationToken);
                clock.Stop();
                Console.WriteLine($"################################### [{DateTime.UtcNow}]  ProcessController.ExecuteAsync Ended Took {clock.Elapsed} ###################################");

                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        #endregion

        #region IAnalysisEngineController members

        [HttpPost]
        public IHttpActionResult Process(ProcessRequest request)
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Match(EvaluateRequest request)
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Enroll(ProcessRequest request)
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Identify(ProcessRequest request)
        {

            try
            {

                Console.WriteLine($"Arriving ProcessController.Identify {DateTime.UtcNow}");

            
          

                Console.WriteLine($"Leaving ProcessController.Identify {DateTime.UtcNow}");

                return Ok(new ProcessResult());
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [HttpPut]
        public IHttpActionResult Update([FromUri]string id, [FromBody]ProcessRequest request)
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Verify(ProcessRequest request)
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetSubject(string id)
        {
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteSubject(string id)
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult List()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Clear(ClearProcessRequest request)
        {
            return Ok();
        }

        #endregion
    }
}
