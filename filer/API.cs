using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.PortableExecutable;



/****************************************
 *  
 *  Object types in the API interface
 *  
 *  - User
 *  - Role, existing nodes are hardcoded (seeded from an enum?)
 *  -- User, An ordinary usre can register himself
 *  -- Admin, An admin can be created by another admin, but can not register himself
 *     An ordinary user can be ugraded to admin by another admin
 *     An admin can only be deleted or downgraded to ordinary user by sysadmin
 *  -- SysAdmin (can create/delete admins)
 *  
 *  Forum types
 *  
 * - Category - top level categorization, can be created/altered etc by admins
 * - Threades - can be created by ordinary users. Can be altered/deleted by admins
 * - Posts - can be created by ordinary users. Can be altered/deleted by admins
 * 
 * PM types
 * 
 * - Private message
 * 
 * News types
 * 
 * - NewsPost
 * 
 * 
 * General behaviour
 * ========================
 *
 * Unless otherwise noted objects can not be deleted deom the database, but have a status field that
 * can be set to either active or removed
 * 
 * The properties of each class are what is returned by Get/GetList calls, which not necessarily is
 * identical to what is stored in the model classes
 * 
 * The parameters for create and update methods might be classes instead
 * 
*****************************************/




namespace Project_3.Controllers
{
    internal class APIInterface {
        private class ForumPost {
            public int Id { get; set; }
            public int UserId { get; set; }
            public DateTime CreatedDate { get; set; }
            public int ThreadId { get; set; }
            public int QuotedPostId { get; set; }
            public string Text { get; set; }
            public Tag[] Tags { get; set; }

            // Accessible by all
            static public ForumPost Get(int id) {
                throw new NotImplementedException();
            }

            // Accessible by all
            static public ForumPost[] GetList(
                int threadId, // Posts for thread, if not null
                string filter, // Filter in text
                int userId, // Posts for user, if not 0
                int[] tagIds, // Posts with any of tag id's
                DateTime fromDate // Posts created after some date
                ) {
                throw new NotImplementedException(); 
            }

            // Accessible by users
            static public void Create(
                int ThreadId,
                int[] TagIds,
                int QuotedPostId,
                string Text
            ) {
                // Server sets userId and createDate
                throw new NotImplementedException();
            }

            // Accessible by users
            static public void Update(
                int id,
                int threadId,
                int[] tagIds,
                int quotedPostId,
                string text
                ) {
                // A user can edit text and quotedPostId on post created by himself within XX minutes after creation
                // A user can edit tag list on post created by himself at any time
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void Delete(int id) {
                // An admin can delete a post (i.e. set status to Deleted)
            }
        }


        private class Thread {
            public int Id { get; set; }
            public int Name { get; set; }
            public int CategoryId { get; set; }
            public int UserId { get; set; }
            public int CreatedDate { get; set; }

            // Accessible by all
            static public Thread Get(int id) {
                throw new NotImplementedException();
            }

            // Accessible by all
            static public Thread[] GetList(
                int categoryId
                ) {
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void Create(
            ) {
                // Server sets userId and createDate
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void Update(
                int id,
                string name,
                int threadId
                ) {
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void Delete(int id) {
                // An admin can delete a thread (i.e. set status to Deleted)
                // All posts will be preserved
            }
        }

        private class Category {
            public int Id { get; set; }
            public int Name { get; set; }

            // Accessible by all
            static public Category Get(int id) {
                throw new NotImplementedException();
            }

            // Accessible by all
            static public Category[] GetList(
                string filter // Filter in name
                ) {
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void Create(
             string name
            ) {
                // Server sets userId and createDate
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void Update(
                int id,
                string name
            ) {
                // A user can edit text and quotedPostId on post created by himself within XX minutes after creation
                // A user can edit tag list on post created by himself at any time
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void Delete(int id) {
                // An admin can delete a post (i.e. set status to Deleted)
            }
        }

        private class NewsPost
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int CreatedDate { get; set; }
            public int Header { get; set; }
            public int Tags { get; set; }
            public int Text { get; set; }
            public int EventDate { get; set; }
            public int StateId { get; set; }

            // Accessible by all
            static public NewsPost Get(int id) {
                throw new NotImplementedException();
            }

            // Accessible by all
            static public NewsPost[] GetList(
                string filter, // Filter in text and header
                int[] tagIds,
                DateTime createdAfter,
                int idAfter,
                DateTime eventDateFrom,
                DateTime eventDateTo
            ) {
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void Create(
                string Header,
                string Tags,
                string Text,
                DateTime? EventDate
            ) {
                // Server sets userId and createDate
                throw new NotImplementedException();
            }

            // Accessible by admin
            static public void Update(
                int id,
                string Header,
                string Tags,
                string Text,
                DateTime? EventDate
                ) {
                // A admin can edit all fields in all posts at any time
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void Delete(int id) {
                // An admin can remove a post (set it to status removed)
            }
        }

        private class PrivateMessage
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int ReceiverId { get; set; }
            public int CreatedDate { get; set; }
            public int Header { get; set; }
            public int Text { get; set; }
            public int ReadDate { get; set; }
            public int ParentId { get; set; } // if is reply on other PM, 0 otherwise

            // Accessible by users
            static public PrivateMessage Get(int id) {
                // Will return NOT FOUND if not SenderId or ReceiverId is current user ID
                // On retrieve readdate will be set to current date/time if it is null
                throw new NotImplementedException();
            }

            // Accessible by users
            static public PrivateMessage[] GetListReceived(
                string filter, // Filter in text, header, and name of sender
                DateTime CreatedAfter,
                bool unreadOnly
                ) {
                // Will only return items where SenderId or ReceiverId is current user ID
                throw new NotImplementedException();
            }

            // Accessible by users
            static public ForumPost[] GetListSent(
                string filter, // Filter in text, header, and name of recipient
                DateTime CreatedAfter,
                bool unreadOnly
                ) {
                // Will only return items where SenderId or ReceiverId is current user ID
                throw new NotImplementedException();
            }

            // Accessible by users
            static public void Create(
                int ReceiverId,
                string Header,
                string Text,
                int ParentId
            ) {
                // Server sets SenderId and createDate
                throw new NotImplementedException();
            }

            // Accessible by users
            static public void Delete(int id) {
                // A message can be deleted if ReadDate is null?
            }
        }
        private class Tag {
            public int Id { get; set; }
            public string Name { get; set; }

            // Accessible by all
            static public Tag Get(int id) {
                throw new NotImplementedException();
            }

            // Accessible by all
            static public Tag[] GetList(
                string filter
                ) {
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void Create(
                string Name
            ) {
                // Should check that are no duplicates
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void Update(
                int id,
                string name
                ) {
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void Delete(int id) {
                // Either
                // Tag is deleted, as well as all references to it from forumPoests and newsposts
                // or
                // Tag is set to removed
            }
        }

        private class User {
            // Accessible by all
            static public User Get(int id) {
                // Possibly there should be some fields that are not set, depending on role of current user
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public ForumPost[] GetList(
                string filter, // Filter in username
                int roleId
                ) {
                // Possibly there should be some fields that are not set, depending on role of current user
                throw new NotImplementedException();
            }

            // Accessible only if not logged in, will create ordinary user
            static public void Register(
                string name,
                string email,
                string password
            ) {
                // Server sets userId and createDate
                throw new NotImplementedException();
            }

            // Accessible by admins
            static public void CreateAdminUser(
                string name,
                string email,
                string password
            ) {
                // Server sets userId and createDate
                throw new NotImplementedException();
            }

            // Accessible by users and admins, NOT by sysadmin
            static public void ChangePassword(
                string newPassword
                ) {
                throw new NotImplementedException();
            }

            // Accessible by users
            static public void Update(
                int id,
                string userName,
                string otherProperties
                ) {
                // A ordinary user or admin can edit himself, (except role)
                // An admin can edit ordinary users
                // A sysadmin can edit ordinary users and admins, but not himself
                throw new NotImplementedException();
            }

            // Accessible by admins
            public void Delete(int id) {
                // An admin can delete an ordinary user (i.e. set status to Deleted)
                // a sysadmin can delete ordinary users or admins
            }
        }

    }
}