using IntroSE.Kanban.Backend.DataAccessLayer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DAO
{
    public abstract class DAOs
    {
        protected DALcontroller control;
        protected DAOs(DALcontroller control)
        { this.control = control; }

    }
}

