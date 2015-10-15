using System;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        [Route("api/points/distance")]
        public IHttpActionResult GetDistance(int startX, int startY, int endX, int endY)
        {
            return this.Ok((float) Math.Sqrt((startX - endX)*(startX - endX) +
                                             (startY - endY)*(startY - endY)));
        }
    }
}
