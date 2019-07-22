namespace Rejime.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    public partial class Menu:DALS
    {
        public int id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? Parent_id { get; set; }

        [StringLength(5)]
        public string Type { get; set; }

        public string Link { get; set; }

        public int? Level { get; set; }

        public string Icon { get; set; }

        public int? Mnuid { get; set; }
        #region Method
        public List<Menu> Select()
        {
            return Entity.Menu.OrderBy(obj => obj.Mnuid).ToList();
        }
        #endregion
    }
}
