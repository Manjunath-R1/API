using System;
using ThoughtFocus.Repository.Interfaces.Contact;
using ThoughtFocus.Domain.User;
using ThoughtFocus.Common.Exceptions.BusinessException;
using ThoughtFocus.Business.Interfaces.User;
using ThoughtFocus.Repository.Interfaces.User;
using ThoughtFocus.DataAccess.Models.User;

namespace ThoughtFocus.Business.Impl.User
{
    public class UserAccountImpl : IUserAccount
    {

        private IContactRepository _contactRepository;
        private IUserRepository _userRepository;
        private IRUserRoleRepository _rUserRoleRepository;

        public UserAccountImpl(IContactRepository ContactRepository, IUserRepository UserRepository, IRUserRoleRepository rUserRoleRepository)
        {
            _contactRepository = ContactRepository;
            _userRepository = UserRepository;
            _rUserRoleRepository = rUserRoleRepository;
        }
        
    public void UpdateUserOnLogin(DataAccess.Models.User.User User)
    {
        var currentDate = DateTime.Now;
        try
        {
            User.FirstLoginDateTime = User.FirstLoginDateTime == null ? currentDate : User.FirstLoginDateTime;
            User.LastLoginDateTime = currentDate;
            _userRepository.UpdateUser(User);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
       
    }
  }
}
