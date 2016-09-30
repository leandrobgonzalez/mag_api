using System;
using System.Collections.Generic;
using System.Web.Http;
using mag_api.Models;
using ServiceStack.OrmLite;

namespace mag_api.Controllers
{
    public class ClienteController : ApiController
    {
        private OrmLiteConnectionFactory _dbFactory
        {
            get
            {
                return new OrmLiteConnectionFactory(DatabaseConfig.ConnectionString, SqliteDialect.Provider);
            }
        }

        // GET: api/Cliente/?params
        public IEnumerable<Cliente> Get([FromUri]Cliente cliente)
        {
            using (var db = _dbFactory.Open())
            {
                var search = db.From<Cliente>();

                if (cliente != null)
                {
                    if (cliente.Idade > 0)
                        search.Where(c => c.Idade == cliente.Idade);
                    if (!string.IsNullOrWhiteSpace(cliente.Nome))
                        search.Where(c => c.Nome.Contains(cliente.Nome));
                    if (!string.IsNullOrWhiteSpace(cliente.Sexo))
                        search.Where(c => c.Sexo == cliente.Sexo);
                }

                return db.Select(search);
            }
        }

        // GET: api/Cliente/5
        public Cliente Get(string id)
        {
            using (var db = _dbFactory.Open())
            {
                return db.SingleById<Cliente>(id);
            }
        }

        // POST: api/Cliente
        public Cliente Post([FromBody]Cliente cliente)
        {
            cliente.Id = Guid.NewGuid().ToString();

            using (var db = _dbFactory.Open())
            {
                db.Insert(cliente);
            }

            return cliente;
        }

        // PUT: api/Cliente/5
        public void Put(string id, [FromBody]Cliente cliente)
        {
            cliente.Id = id;
            using (var db = _dbFactory.Open())
            {
                db.Update(cliente);
            }
        }

        // DELETE: api/Cliente/5
        public void Delete(string id)
        {
            using (var db = _dbFactory.Open())
            {
                db.DeleteById<Cliente>(id);
            }
        }
    }
}
