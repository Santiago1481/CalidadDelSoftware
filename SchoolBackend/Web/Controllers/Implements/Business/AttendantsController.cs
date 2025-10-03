using Business.Interfaces.Commands;
using Business.Interfaces.Querys;
using Entity.Dtos.Business.Attendants;
using Entity.Model.Business;
using Web.Controllers.Implements.Abstract;

namespace Web.Controllers.Implements.Business
{
    public class AttendantsController
       : GenericController<
       Attendants,
       AttendantsQueryDto,
       AttendantsDto>
    {
        public AttendantsController(
            IQueryServices<Attendants, AttendantsQueryDto> q,
            ICommandService<Attendants, AttendantsDto> c)
          : base(q, c) { }
    }

}
