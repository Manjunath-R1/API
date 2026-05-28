using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ThoughtFocus.DataAccess.Models.Master
{
    
    [Table("ApplicationType", Schema = "Master")]
    public partial class ApplicationType
    {

        [Key]
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long? DisplayOrder { get; set; }
            
    }
}