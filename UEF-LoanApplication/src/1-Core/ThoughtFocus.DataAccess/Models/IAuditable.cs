namespace ThoughtFocus.DataAccess.Models
{
    public interface IAuditable
    {
        System.DateTime CreatedDateTime { get; set; }
        long CreatedByUserID { get; set; }
        System.DateTime LastModifiedDateTime { get; set; }
        long LastModifiedByUserID { get; set; }
        bool IsActive { get; set; }
    }
}