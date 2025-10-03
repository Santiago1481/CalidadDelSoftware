using Entity.Model.Global;
using Entity.Model.Security;

namespace Entity.Model.Business
{
    public class Attendants : ABaseEntity
    {
        public int PersonId { get; set; }

        public Person Person { get; set; }
    }
}
