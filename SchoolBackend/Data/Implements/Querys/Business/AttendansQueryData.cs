using Entity.Context.Main;
using Entity.Model.Business;
using Entity.Model.Paramters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Implements.Querys.Business
{
    public class AttendansQueryData : BaseGenericQuerysData<Attendants>
    {
        protected readonly ILogger<AttendansQueryData> _logger;
        protected readonly AplicationDbContext _context;

        public AttendansQueryData(AplicationDbContext context, ILogger<AttendansQueryData> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public override async Task<IEnumerable<Attendants>> QueryAllAsyn(int? status)
        {
            try
            {
                // El as queryable me permite ir construyendo la consulta
                IQueryable<Attendants> query = _dbSet.
                                                AsQueryable()
                                                .Include(p => p.Person)
                                                    .ThenInclude(d=> d.DocumentType);

                if (status.HasValue)
                    query = query.Where(x => x.Status == status.Value);

                var model = await query.ToListAsync();

                _logger.LogInformation("Consulta de la enidad {Entity} se realizo exitosamente", typeof(Attendants).Name);
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Error en la consulta la entidad {Entity}", typeof(Attendants).Name);
                throw;
            }
        }

        public override async Task<Attendants?> QueryById(int id)
        {

            try
            {
                var query = await _dbSet
                  .AsNoTracking()
                  .Include(p => p.Person)
                  .FirstOrDefaultAsync(e => e.Id == id); ;

                return query;

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Error en la consulta con id {id}", typeof(Attendants).Name);
                return null;
            }

        }






    }
}
