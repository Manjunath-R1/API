 
namespace ThoughtFocus.Business.Interfaces.EmailTemplate
{
    public interface IPreEmailConditionManager
    {         
        bool IsEmailValid(string emailaddress);
    }
}
