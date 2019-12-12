using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AQuIP.Admin.Models;
using System.Web.Mvc;
using PagedList;
using AQuIP.Admin.Helpers;
using AQuIP.Admin.Services;
using System.Linq;

namespace AQuIP.Admin.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService = new UserService();

        // GET: User
        [Authorize]
        public ActionResult Index(string sortOrder, string searchQuery, int page = 1, int pageSize = 10)
        {
            ViewBag.searchQuery = String.IsNullOrEmpty(searchQuery) ? "" : searchQuery;

            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 10;

            ViewBag.FirstNameSortParam = sortOrder == "firstName" ? "firstName_desc" : "firstName";
            ViewBag.LastNameSortParam = sortOrder == "lastName" ? "lastName_desc" : "lastName";
            ViewBag.UserNameSortParam = sortOrder == "userName" ? "userName_desc" : "userName";
            ViewBag.OrganizationSortParam = sortOrder == "organization" ? "organization_desc" : "organization";

            ViewBag.CurrentSort = sortOrder;

            var userList = _userService.FindAll();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                userList = userList.Where(u => u.FirstName.ToUpper().Contains(searchQuery.ToUpper())
                                        || u.LastName.ToUpper().Contains(searchQuery.ToUpper()) 
                                        || u.UserName.ToUpper().Contains(searchQuery.ToUpper())
                                        || (u.FirstName + ' ' + u.LastName).ToUpper().Contains(searchQuery.ToUpper())).ToList();
            }            

            if (!string.IsNullOrEmpty(sortOrder))
            {
                userList = Extensions.GetSortedList(userList, sortOrder);
            }           

            return View(userList.ToPagedList(page, pageSize));           
        }

        // GET: User/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            UserAccount user = new UserAccount();
            user = _userService.FindByID(id);
           
            return View(user);
        }

        // GET: User/Create
        [Authorize]
        public ActionResult Create()
        {
            CreateUserViewModel user = new Models.CreateUserViewModel();
            user.Roles = new SelectList(_userService.GetRoles());         
        
            return View(user);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(CreateUserViewModel user)
        {
            try
            {
                user.Roles = new SelectList(_userService.GetRoles());

                CreateUserDTO userDTO = new Models.CreateUserDTO
                {
                    UserLogin = user.UserLogin,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    Phone = user.Phone,
                    Role = user.Role,
                    Organization = user.Organization
                };

                _userService.Add(userDTO);

                ViewBag.successMessage = Constant.CreateUserSuccessMsg;
                return View(user);
            }
            catch(SqlException ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }                     
        }

        // GET: User/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            UserAccount user = new UserAccount();
            user = _userService.FindByID(id);

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserAccount user)
        {
            try
            {
                _userService.UpdateUser(id, user);

                ViewBag.successMessage = Constant.EditUserSuccessMsg;
                return View(user);
            }
            catch(SqlException ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }
        }

        // GET: User/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {            
            var user = _userService.FindByID(id);          

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, bool delete = true)
        {          
            UserAccount user = new UserAccount();
            try
            {
                user = _userService.FindByID(id);
                if (user == null)
                {
                    ViewBag.Message = Constant.DeletionErrorMsg;
                    return View();
                }

                _userService.DeleteUser(user.UserName);

                ViewBag.successMessage = Constant.DeleteUserSuccessMsg;
                return View(user);
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }
        }

        //GET: User/ChangePassword/5
        [Authorize]
        public ActionResult ResetPassword()
        {
            return View();
        }

        //POST: User/ChangePassword/5
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel user)
        {
            int rowsAffected = 0;

            if (!string.IsNullOrEmpty(user.UserName))
            {
                try
                {
                    rowsAffected = _userService.ResetPassword(user);

                    if (rowsAffected != 0)
                    {
                        ViewBag.successMessage = Constant.ResetPasswordSuccessMsg;
                        return View();
                    }

                    ViewBag.Message = Constant.ResetPasswordErrorMsg;
                    return View();

                }
                catch (SqlException ex)
                {
                    ViewBag.Message = ex.Message;
                    return View();
                }
            }
            else
            {
                return View();
            }            
        }

        [Authorize]
        public ActionResult ActivateDeactivate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ActivateDeactivate(ActivateDeactivateViewModel user)
        {
            int rowsAffected = 0;

            try
            {                
                    switch(user.Status)
                    {
                        case "Activate":
                           rowsAffected = _userService.ActivateUser(user.Username);

                            if (rowsAffected != 0)
                            {
                                ViewBag.successMessage = Constant.ActivateUserSuccessMsg;
                                return View();
                            }
                            ViewBag.Message = Constant.ActivateUserErrorMsg;
                            return View();

                        case "Deactivate":
                            rowsAffected = _userService.DeactivateUser(user.Username);

                            if (rowsAffected != 0)
                            {
                                ViewBag.successMessage = Constant.DeactivateUserSuccessMsg;
                                return View();
                            }
                            ViewBag.Message = Constant.DeactivationErrorMsg;
                            return View();

                        default:
                            return View();
                    }               
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }           

        }
    }
}
