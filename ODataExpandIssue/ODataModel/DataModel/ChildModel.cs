

namespace ODataModel
{

    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ChildModel")]
    public class ChildModel
    {
        [Column("Key")]
        public int Key { get; set; }

        [Column("Display")]
        public string Display { get; set; }

        [Column("ParentKey")]
        [ForeignKey("ParentModel")]
        public int ParentKey { get; set; }

        
        public virtual ParentModel ParentModel { get; set; }
    }
}
