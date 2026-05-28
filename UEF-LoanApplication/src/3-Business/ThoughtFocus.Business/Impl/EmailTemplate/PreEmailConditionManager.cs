 
using System;
using System.Net.Mail;
using ThoughtFocus.Business.Interfaces.EmailTemplate;

namespace ThoughtFocus.Business.Impl.EmailTemplate
{
    public class PreEmailConditionManager : IPreEmailConditionManager
    {
        #region Fields



        #endregion Fields

        #region Constructor

        public PreEmailConditionManager()
        {

        }

        #endregion Constructor

        #region Methods 
        
        public bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        #endregion Methods
    }
}
