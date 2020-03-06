using System;


namespace ODataModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Parent")]

    public class ParentModel
    {
        [Column("Key")]
        public int Key { get; set; }

        [Column("Display")]
        public string Name { get; set; }

        public ICollection<ChildModel> Childrens { get; set; }
    }
}
