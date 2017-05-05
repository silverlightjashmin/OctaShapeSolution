﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OctaShape.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OctaShape_eTicket_Entities : DbContext
    {
        public OctaShape_eTicket_Entities()
            : base("name=OctaShape_eTicket_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Day_EndDetail> Day_EndDetail { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TicketBranch> TicketBranch { get; set; }
        public virtual DbSet<TicketCategory> TicketCategory { get; set; }
        public virtual DbSet<TicketComment> TicketComment { get; set; }
        public virtual DbSet<TicketImage> TicketImage { get; set; }
        public virtual DbSet<TicketPriority> TicketPriority { get; set; }
        public virtual DbSet<TicketStatus> TicketStatus { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserLoginInfo> UserLoginInfo { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }
    
        public virtual int AddUserRole(Nullable<int> userId, Nullable<int> roleId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("RoleId", roleId) :
                new ObjectParameter("RoleId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddUserRole", userIdParameter, roleIdParameter);
        }
    
        public virtual ObjectResult<AdminUserList_Result> AdminUserList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AdminUserList_Result>("AdminUserList");
        }
    
        public virtual ObjectResult<CallForDayEnd_Result> CallForDayEnd(string username, string branchcode)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var branchcodeParameter = branchcode != null ?
                new ObjectParameter("branchcode", branchcode) :
                new ObjectParameter("branchcode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CallForDayEnd_Result>("CallForDayEnd", usernameParameter, branchcodeParameter);
        }
    
        public virtual ObjectResult<FindUserRole_Result> FindUserRole(Nullable<int> userid)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FindUserRole_Result>("FindUserRole", useridParameter);
        }
    
        public virtual ObjectResult<GetAllUserDetails_Result> GetAllUserDetails(string userName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllUserDetails_Result>("GetAllUserDetails", userNameParameter);
        }
    
        public virtual ObjectResult<GetCommentImage_Result> GetCommentImage()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCommentImage_Result>("GetCommentImage");
        }
    
        public virtual ObjectResult<Nullable<int>> GetLastCommentId()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetLastCommentId");
        }
    
        public virtual ObjectResult<GetPendingTicketsByUser_Result> GetPendingTicketsByUser(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetPendingTicketsByUser_Result>("GetPendingTicketsByUser", userIdParameter);
        }
    
        public virtual ObjectResult<GetRequestData_Result> GetRequestData(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate, string userName)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRequestData_Result>("GetRequestData", startDateParameter, endDateParameter, userNameParameter);
        }
    
        public virtual ObjectResult<GetTicketCountByUser_Result> GetTicketCountByUser(Nullable<int> userid)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTicketCountByUser_Result>("GetTicketCountByUser", useridParameter);
        }
    
        public virtual ObjectResult<GetTicketTotalByStatus_Result> GetTicketTotalByStatus()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTicketTotalByStatus_Result>("GetTicketTotalByStatus");
        }
    
        public virtual ObjectResult<GetWorkLoadPerUser_Result> GetWorkLoadPerUser(string userName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetWorkLoadPerUser_Result>("GetWorkLoadPerUser", userNameParameter);
        }
    
        public virtual ObjectResult<LoginByUsernamePassword_Result> LoginByUsernamePassword(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LoginByUsernamePassword_Result>("LoginByUsernamePassword", usernameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<Loginauthenticate_Result> Loginauthenticate(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Loginauthenticate_Result>("Loginauthenticate", usernameParameter, passwordParameter);
        }
    
        public virtual int ResetPassword(string username, string encryptpassword)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var encryptpasswordParameter = encryptpassword != null ?
                new ObjectParameter("encryptpassword", encryptpassword) :
                new ObjectParameter("encryptpassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ResetPassword", usernameParameter, encryptpasswordParameter);
        }
    
        public virtual ObjectResult<SearchUser_Result> SearchUser(string userName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SearchUser_Result>("SearchUser", userNameParameter);
        }
    
        public virtual int VerifyEmail(Nullable<int> userid, string emailid)
        {
            var useridParameter = userid.HasValue ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(int));
    
            var emailidParameter = emailid != null ?
                new ObjectParameter("emailid", emailid) :
                new ObjectParameter("emailid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("VerifyEmail", useridParameter, emailidParameter);
        }
    
        public virtual ObjectResult<GetDayEndStatus_Result> GetDayEndStatus()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetDayEndStatus_Result>("GetDayEndStatus");
        }
    
        public virtual int DayEndStatOff(string branchcode)
        {
            var branchcodeParameter = branchcode != null ?
                new ObjectParameter("branchcode", branchcode) :
                new ObjectParameter("branchcode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DayEndStatOff", branchcodeParameter);
        }
    
        public virtual ObjectResult<TicketBranch> GetPendingBranch()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TicketBranch>("GetPendingBranch");
        }
    
        public virtual ObjectResult<TicketBranch> GetPendingBranch(MergeOption mergeOption)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TicketBranch>("GetPendingBranch", mergeOption);
        }
    
        public virtual int DayEndStatOn(string branchcode)
        {
            var branchcodeParameter = branchcode != null ?
                new ObjectParameter("branchcode", branchcode) :
                new ObjectParameter("branchcode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DayEndStatOn", branchcodeParameter);
        }
    }
}