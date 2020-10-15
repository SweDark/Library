using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Domain;
using Library.Infrastructure.Persistence;
using Library.Application.Interfaces;
using Library.MVC.Models;

namespace Library.MVC.Controllers
{
    public class MembersController : Controller
    {

        private readonly IMemberService memberservice;
        private readonly ILoanService loanservice;
        private readonly IDateTimeService datetimeservice;

        public MembersController(IMemberService memberservice, ILoanService loanservice, IDateTimeService datetimeservice)
        {
            this.memberservice = memberservice;
            this.loanservice = loanservice;
            this.datetimeservice = datetimeservice;
        }

        // GET: Members
        public async Task<IActionResult> Index(string ex)
        {


            var vm = new MemberIndexVm();            
            if (!string.IsNullOrEmpty(ex))
            {
                vm.Ex = ex;
            }
            vm.Members = memberservice.GetAllMembers(); 
            return View(vm);
        }

        public IActionResult Create()
        {
            var vm = new MemberCreateVm();
            return View(vm);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberCreateVm vm)
        {
            if (ModelState.IsValid)
            {
                //Skapa ny Medlem
                var newMember = new Member();
                newMember.Name = vm.Name;
                newMember.Email = vm.Email;

                memberservice.AddMember(newMember);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }


        //// GET: Members/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (ModelState.IsValid)
            {
                var vm = new MemberDetailsVm();
                var member = memberservice.GetMember(id);
                vm.Name = member.Name;
                vm.Email = member.Email;
                vm.Loans = loanservice.GetLoansByMember(id);
                vm.CurrentDate = datetimeservice.Now;
                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
        }

        //// GET: Books/Edit/5
        public async Task<IActionResult> Edit(int id)/*int? id*/
        {
            if (ModelState.IsValid)
            {
                var vm = new MemberEditVm();
                var member = memberservice.GetMember(id);
                vm.Name = member.Name;
                vm.Email = member.Email;
                vm.Id = member.Id;
                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
        }

        //// POST: Books/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MemberEditVm vm)
        {
            if (ModelState.IsValid)
            {
                var editMember = new Member();
                editMember.Id = vm.Id;
                editMember.Name = vm.Name;
                editMember.Email = vm.Email;
                memberservice.UpdateMember(editMember);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }

        //// GET: Books/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var vm = new MemberDeleteVm();
                var member = memberservice.GetMember(id);
                vm.Id = member.Id;
                vm.Name = member.Name;
                vm.Email = member.Email;
                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
        }
        //// POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(MemberDeleteVm vm)
        {
            if (ModelState.IsValid)
            {
                var deleteMember = new Member();
                deleteMember.Id = vm.Id;
                deleteMember.Name = vm.Name;
                deleteMember.Email = vm.Email;

                try
                {
                    memberservice.DeleteMember(deleteMember, loanservice.GetAllCurrentLoans());
                    return RedirectToAction(nameof(Index));
                } catch (OperationCanceledException ex)
                {
                    var test = ex;
                    var message = ex.Message;

                    ViewBag.message = ("You tried to delete a member which has an ongoing loan, thus we intercepted your request!");

                    return RedirectToAction(nameof(Index), new { ex = ViewBag.message });
                }
                
            }
            return RedirectToAction("Error", "Home", "");

        }
    }
}
