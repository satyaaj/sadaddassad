using AirportManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AirportManagementSystem
{
    public class WebRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var context = new AllDbContext();
            //var managerContext = new ManagerDbContext();
            var result = (from su in context.SuperUser
                          join role in context.userroles on su.RoleID equals role.RoleID
                          where su.UserName == username
                          select role.RolesName).ToArray();

            var result1 = (from admin in context.Admins
                           join role in context.userroles on admin.RoleID equals role.RoleID
                           where admin.AdminID == username
                           select role.RolesName).ToArray();

            var result2 = (from manager in context.Managers
                           join role in context.userroles on manager.RoleID equals role.RoleID
                           where manager.ManagerID == username
                           select role.RolesName).ToArray();

            var result3 = (from pilot in context.Pilots
                           join role in context.userroles on pilot.RoleID equals role.RoleID
                           where pilot.PilotID.ToString() == username
                           select role.RolesName).ToArray();



            return ((result.Concat(result1).Concat(result2)).Concat(result3)).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}