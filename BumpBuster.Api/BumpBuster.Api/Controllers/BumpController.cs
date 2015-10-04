using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using BumpBuster.Api.DataObjects;
using BumpBuster.Api.Models;

namespace BumpBuster.Api.Controllers
{
    public class BumpController : TableController<Bump>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Bump>(context, Request, Services);
        }

        // GET tables/TodoItem
        public IQueryable<Bump> GetAllBumps()
        {
            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Bump> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Bump/
        public Task<Bump> PatchTodoItem(string id, Delta<Bump> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/bump
        public async Task<IHttpActionResult> PostBump(Bump item)
        {
            Bump current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/bump/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteBump(string id)
        {
            // return DeleteAsync(id);
            return null;
        }
    }
}