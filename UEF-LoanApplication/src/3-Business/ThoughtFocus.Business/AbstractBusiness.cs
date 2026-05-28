namespace ThoughtFocus.Business
{ 
    using System;
    using System.Collections.Generic;
    using System.Reflection; 
    using ThoughtFocus.DataAccess.Models.Contact;
    using DataAccess.Models.Master;
    using ThoughtFocus.Domain;
    using ThoughtFocus.Common.Exceptions;      
    using System.Linq; 

    public class AbstractBusiness : IBaseBusiness
    {
        #region Fields

        // private static readonly ILog Logger = LogManager.GetLogger(typeof(AbstractBusiness));


        // #endregion Fields

        // #region Constructors

        // public AbstractBusiness()
        // {
        //  }

        // #endregion Constructors

        // #region Methods
       


        // public List<SalutationEntity> GetAllSalutationEntity()
        // {
        //     return this.masterPersistance.GetAllSalutationEntity();
        // }

        // public List<SchoolEntity> GetAllSchoolEntity()
        // {
        //     return this.masterPersistance.GetAllSchoolEntity();
        // }

        // public List<InstituionEntity> GetAllInstituionEntity(UserSessionEntity userSessionEntity)
        // {
        //     return this.masterPersistance.GetAllInstituionEntity(userSessionEntity);
        // }

        // public List<SchoolPersonTypeEntity> GetAllSchoolPersonTypeEntity()
        // {
        //     return this.masterPersistance.GetAllSchoolPersonTypeEntity();
        // }

        // public List<SchoolTypeEntity> GetAllSchoolTypeEntity()
        // {
        //     return this.masterPersistance.GetAllSchoolTypeEntity();
        // }

        // public List<SecurityQuestionEntity> GetAllSecurityQuestionEntity()
        // {
        //     return this.masterPersistance.GetAllSecurityQuestionEntity();
        // }

        // public List<SiteVisitAcceptanceStatusEntity> GetAllSiteVisitAcceptanceStatusEntity()
        // {
        //     return this.masterPersistance.GetAllSiteVisitAcceptanceStatusEntity();
        // }

        // public List<SiteVisitMemberRoleEntity> GetAllSiteVisitMemberRoleEntity()
        // {
        //     List<SiteVisitMemberRoleEntity> listOfSiteVisitRoleEntity = null;
        //     try
        //     {
        //         List<SiteVisitMemberRole> listOfSiteVisitMemberRole = siteVisitMemberRoleRepository.GetAllSiteVisitMemberRoleEntity();
        //         if (listOfSiteVisitMemberRole != null && listOfSiteVisitMemberRole.Count != 0)
        //         {
        //             listOfSiteVisitRoleEntity = new List<SiteVisitMemberRoleEntity>();

        //             foreach (var item in listOfSiteVisitMemberRole)
        //             {
        //                 listOfSiteVisitRoleEntity.Add(new SiteVisitMemberRoleEntity
        //                 {
        //                     SiteVisitMemberRoleID = item.SiteVisitMemberRoleID,
        //                     SiteVisitMemberRoleName = item.SiteVisitMemberRoleName,
        //                 });

        //             }
        //         }

        //         return listOfSiteVisitRoleEntity;
        //     }
        //     catch (RepositoryException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitMemberRoleEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (TargetInvocationException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitMemberRoleEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitMemberRoleEntity() >>", ex);
        //         throw ex;
        //     }
        // }

        // public List<SiteVisitSeasonEntity> GetAllSiteVisitSeasonEntity()
        // {
        //     List<SiteVisitSeasonEntity> listOfSiteVisitSeasonEntity = null;
        //     try
        //     {
        //         List<SiteVisitSeason> listOfSiteVisitSeason = this.siteVisitSeasonRepository.GetAllSiteVisitSeasonEntity();

        //         if (listOfSiteVisitSeason != null && listOfSiteVisitSeason.Count != 0)
        //         {
        //             listOfSiteVisitSeasonEntity = new List<SiteVisitSeasonEntity>();
        //             for (int i = 0; i < Convert.ToInt64(ConfigReaderUtility.GetConfigValueFromAppSettingsAsString(AppSettingsConstants.SiteVisitSeasonYearCount)); i++)
        //             {
        //                 foreach (var item in listOfSiteVisitSeason)
        //                 {
        //                     listOfSiteVisitSeasonEntity.Add(new SiteVisitSeasonEntity
        //                     {
        //                         SiteVisitSeasonID = item.SiteVisitSeasonID,
        //                         SiteVisitSeasonName = item.SiteVisitSeasonName + " " + (DateTime.Now.AddYears(i).Year.ToString()),
        //                     });
        //                 }
        //             }
        //         }

        //         return listOfSiteVisitSeasonEntity;
        //     }
        //     catch (RepositoryException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitSeasonEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (TargetInvocationException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitSeasonEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitSeasonEntity() >>", ex);
        //         throw ex;
        //     }
        // }

        // public List<MemberType> GetAllContactMemberTypes()
        // {
        //     return this.masterPersistance.GetAllContactMemberTypes();
        // }

        // public List<SiteVisitStateEntity> GetAllSiteVisitStateEntity()
        // {
        //     List<SiteVisitStateEntity> listOfSiteVisitStateEntity = null;
        //     try
        //     {
        //         List<SiteVisitState> listOfSiteVisitStatus = siteVisitStateRepository.GetAllSiteVisitStateEntity();

        //         if (listOfSiteVisitStatus != null && listOfSiteVisitStatus.Count != 0)
        //         {
        //             listOfSiteVisitStateEntity = new List<SiteVisitStateEntity>();

        //             foreach (var item in listOfSiteVisitStatus)
        //             {
        //                 listOfSiteVisitStateEntity.Add(new SiteVisitStateEntity
        //                 {
        //                     SiteVisitStateID = item.SiteVisitStateID,
        //                     SiteVisitStateName = item.SiteVisitStateName,
        //                     SiteVisitStateDescription = item.SiteVisitStateDescription
        //                 });
        //             }
        //         }

        //         return listOfSiteVisitStateEntity;
        //     }
        //     catch (RepositoryException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitStateEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (TargetInvocationException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitStateEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitStateEntity() >>", ex);
        //         throw ex;
        //     }
        // }

        // public List<SiteVisitTypeEntity> GetAllSiteVisitTypeEntity()
        // {
        //     List<SiteVisitTypeEntity> SiteVisitTypes = null;
        //     try
        //     {
        //         List<SiteVisitType> listOfSiteVisitType = this.siteVisitTypeRepository.GetAllSiteVisitTypeEntity();

        //         SiteVisitTypes = new List<SiteVisitTypeEntity>();

        //         foreach (var item in listOfSiteVisitType)
        //         {
        //             SiteVisitTypes.Add(new SiteVisitTypeEntity
        //             {
        //                 SiteVisitTypeID = item.SiteVisitTypeID,
        //                 SiteVisitTypeName = item.SiteVisitTypeName
        //             });
        //         }

        //         return SiteVisitTypes;
        //     }
        //     catch (RepositoryException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitTypeEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (TargetInvocationException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitTypeEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitTypeEntity() >>", ex);
        //         throw ex;
        //     }

        // }

        // public List<FeedbackTypeEntity> GetAllFeedbackTypeEntity()
        // {
        //     List<FeedbackTypeEntity> FeedbackTypes = null;
        //     try
        //     {
        //         List<FeedbackType> listOfFeedbackType = this.feedbackTypeRepository.GetAllFeedbackTypeEntity();

        //         FeedbackTypes = new List<FeedbackTypeEntity>();

        //         foreach (var item in listOfFeedbackType)
        //         {
        //             FeedbackTypes.Add(new FeedbackTypeEntity
        //             {
        //                 FeedbackTypeID = item.FeedbackTypeID,
        //                 FeedbackTypeName = item.Name
        //             });
        //         }

        //         return FeedbackTypes;
        //     }
        //     catch (RepositoryException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitTypeEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (TargetInvocationException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitTypeEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitTypeEntity() >>", ex);
        //         throw ex;
        //     }

        // }

        // public List<SiteVisitConflictOfInterestTypeEntity> GetAllSiteVisitConflictOfInterestTypeEntity()
        // {
        //     List<SiteVisitConflictOfInterestTypeEntity> SiteVisitConflictOfInterestTypes = null;
        //     try
        //     {
        //         List<ConflictOfInterestType> listofSiteVisitConflictOfInterestTypes = this.conflictOfInterestTypeRepository.GetAllSiteVisitConflictOfInterestTypeEntity();

        //         SiteVisitConflictOfInterestTypes = new List<SiteVisitConflictOfInterestTypeEntity>();

        //         foreach (var item in listofSiteVisitConflictOfInterestTypes)
        //         {
        //             SiteVisitConflictOfInterestTypes.Add(new SiteVisitConflictOfInterestTypeEntity
        //             {
        //                 ConflictOfInterestTypeID = item.ConflictOfInterestTypeID,
        //                 ConflictOfInterestTypeName = item.ConflictOfInterestTypeName
        //             });
        //         }

        //         return SiteVisitConflictOfInterestTypes;
        //     }
        //     catch (RepositoryException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitConflictOfInterestTypeEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (TargetInvocationException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitConflictOfInterestTypeEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllSiteVisitConflictOfInterestTypeEntity() >>", ex);
        //         throw ex;
        //     }

        // }

        // public List<StateEntity> GetAllStateEntity()
        // {
        //     return this.masterPersistance.GetAllStateEntity();
        // }

        // public string GetMinorityStatusByEthnicityID(long ethnicityID)
        // {
        //     if (this.masterPersistance.GetMinorityStatusByEthnicityID(ethnicityID))
        //     {
        //         return IsMinorityMethodReturnType.Yes.ToString();
        //     }
        //     else
        //     {
        //         return IsMinorityMethodReturnType.No.ToString();
        //     }
        // }

        // public string GetMinorityStatus(long? ethnicityID)
        // {
        //     if (this.masterPersistance.GetMinorityStatus(ethnicityID))
        //     {
        //         return IsMinorityMethodReturnType.Yes.ToString();
        //     }
        //     else
        //     {
        //         return IsMinorityMethodReturnType.No.ToString();
        //     }
        // }

        // public SiteVisitTypeEntity GetSiteVisitTypeByID(long siteVisitTypeID)
        // {
        //     return this.masterPersistance.GetSiteVisitTypeEntityByID(siteVisitTypeID);
        // }

        // public long GetUpcomingSiteVisitTypeBySchoolId(long SchoolID)
        // {            
        //     try
        //     {
        //         SiteVisit sitevisit = siteVisitRepository.GetRecentSiteVisitsBySchoolID(SchoolID);
        //        // SiteVisitTypeEntity siteVisitTypeEntity =this.masterPersistance.GetSiteVisitTypeEntityByID(sitevisit.SiteVisitTypeID);
        //         List<SiteVisitType> listOfSiteVisitType = this.siteVisitTypeRepository.GetAllSiteVisitTypeEntity();
        //         //If there is No SiteVisit 
        //         if (sitevisit == null)
        //         {
        //             return listOfSiteVisitType.OrderBy(od => od.SiteVisitTypeID).FirstOrDefault().SiteVisitTypeID;                    
        //         }
        //         //else if(sitevisit != null && sitevisit.SiteVisitTypeID > 0)
        //         //{
        //         //    long sitevisitTypeId = listOfSiteVisitType.Max(x => x.SiteVisitTypeID);
        //         //    if(sitevisit.SiteVisitTypeID<sitevisitTypeId)
        //         //    {
        //         //        return listOfSiteVisitType.Where(x=>x.SiteVisitTypeID >sitevisit.SiteVisitTypeID).OrderBy(od=>od.SiteVisitTypeID).FirstOrDefault().SiteVisitTypeID;
        //         //    }
        //         //    else  if(sitevisit.SiteVisitTypeID==sitevisitTypeId)
        //         //    {
        //         //        return listOfSiteVisitType.OrderBy(od => od.SiteVisitTypeID).FirstOrDefault().SiteVisitTypeID;     
        //         //    }
        //         //}
        //         //return listOfSiteVisitType.OrderBy(od => od.SiteVisitTypeID).FirstOrDefault().SiteVisitTypeID;
        //         return 0;
        //     }
        //     catch (RepositoryException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetUpcomingSiteVisitTypeBySchoolId() >>", ex);
        //         throw ex;
        //     }
        //     catch (TargetInvocationException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetUpcomingSiteVisitTypeBySchoolId() >>", ex);
        //         throw ex;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError("Error encountered at  GetUpcomingSiteVisitTypeBySchoolId() >>", ex);
        //         throw ex;
        //     }

        // }

        // public SchoolEntity GetShoolBySchoolId(long SchoolID)
        // {
        //     try
        //     {
        //         SchoolEntity schoolEntity =new SchoolEntity();
        //         School school= schoolRepository.GetSchoolByID(SchoolID);
        //         if (school != null)
        //         {
        //             schoolEntity.SchoolID = school.SchoolID;
        //             schoolEntity.SchoolName = school.SchoolName;
        //         }
        //         return schoolEntity;

        //     }
        //     catch (RepositoryException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetShoolBySchoolId() >>", ex);
        //         throw ex;
        //     }
        //     catch (TargetInvocationException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetShoolBySchoolId() >>", ex);
        //         throw ex;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError("Error encountered at  GetShoolBySchoolId() >>", ex);
        //         throw ex;
        //     }

        // }

        // public List<ProgramApplicationTypesEntity> GetAllProgramApplicationTypeEntity()
        // {
        //     List<ProgramApplicationTypesEntity> listOfProgramApplicationTypes = null;
        //     try
        //     {
        //         List<ProgramApplicationTypes> programApplicationTypes = programApplicationTypesRepository.GetAllProgramApplicationTypes();

        //         if (programApplicationTypes != null && programApplicationTypes.Count != 0)
        //         {
        //             listOfProgramApplicationTypes = new List<ProgramApplicationTypesEntity>();

        //             foreach (var applicationType in programApplicationTypes)
        //             {
        //                 listOfProgramApplicationTypes.Add(new ProgramApplicationTypesEntity
        //                 {
        //                     ApplicationTypeID = applicationType.ApplicationTypeID,
        //                     Name = applicationType.Name,
        //                     Description = applicationType.Description
        //                 });
        //             }
        //         }

        //         return listOfProgramApplicationTypes;
        //     }
        //     catch (RepositoryException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllProgramApplicationTypeEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (TargetInvocationException ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllProgramApplicationTypeEntity() >>", ex);
        //         throw ex;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError("Error encountered at  GetAllProgramApplicationTypeEntity() >>", ex);
        //         throw ex;
        //     }
        // }

        #endregion Methods
    }
}