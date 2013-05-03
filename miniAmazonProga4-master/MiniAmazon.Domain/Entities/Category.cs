using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniAmazon.Domain.Entities
{
    public class Category : IEntity
    {

        public virtual string Title { get; set; }

     #region IEntity Members

        public virtual long Id { get; set; }

        #endregion
     

    }
}