using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Domain;
using Library.MVC.Models;
using Library.Application.Interfaces;

namespace Library.MVC.Controllers
{
    public class LoansController : Controller
    {
        private readonly ILoanService loanservice;
        private readonly IBookCopyService bookcopyservice;
        private readonly IMemberService memberservice;
        private readonly IDateTimeService datetimeservice;
        public LoansController(ILoanService loanservice, IBookCopyService bookcopyservice, IMemberService memberservice, IDateTimeService datetimeservice)
        {
            this.loanservice = loanservice;
            this.bookcopyservice = bookcopyservice;
            this.memberservice = memberservice;
            this.datetimeservice = datetimeservice;
        }

        //GET: loans
        public async Task<IActionResult> Index()
        {
            var vm = new LoanIndexVm();
            vm.Loans = loanservice.GetAllLoans();
            vm.CurrentDate = datetimeservice.Now;
            return View(vm);
        }

        //// GET: Loan/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (ModelState.IsValid)
            {
                var vm = new LoanDetailsVm();
                var loan = loanservice.GetLoan(id);
                vm.bookCopy = loan.bookCopy;
                vm.member = loan.member;
                vm.LoanDate = loan.LoanDate;
                vm.ReturnDate = loan.ReturnDate;
                vm.CurrentDate = datetimeservice.Now;
                vm.Id = loan.Id;
                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
        }

        // GET: Loan/Create
        public IActionResult Create()
        {
            var vm = new LoanCreateVm();
            vm.MemberList = new SelectList(memberservice.GetAllMembers(), "Id", "Name");
            //vm.MemberList = new SelectList(memberservice.GetAllMembers(), "Id", "Name");
            vm.AvailableBooksList = new SelectList(bookcopyservice.GetAllAvailableBooks(loanservice.GetAllCurrentLoans()), "Id", "Details.Title");
            foreach(var item in vm.AvailableBooksList)
            {
                item.Text += ", book-id: " + item.Value;
            }
            return View(vm);
        }

        // POST: Loan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoanCreateVm vm)
        {
            if (ModelState.IsValid)
            {
                //Skapa nytt lån
                var newLoan = new Loan();
                newLoan.MemberId = vm.MemberId;
                newLoan.LoanDate = datetimeservice.Now;
                newLoan.bookCopyId = vm.BookCopyId;
                
                loanservice.AddLoan(newLoan);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }

        public async Task<IActionResult> UpdateLoanStatus(int id)/*int? id*/
        {
            if (ModelState.IsValid)
            {
                var vm = new ReturnLoanVm();
                var loan = loanservice.GetLoan(id);
                vm.member = loan.member;
                vm.bookCopy = loan.bookCopy;
                vm.LoanDate = loan.LoanDate;
                vm.ReturnDate = loan.ReturnDate;
                vm.MemberId = vm.MemberId;
                vm.BookCopyId = vm.BookCopyId;
                vm.CurrentDate = datetimeservice.Now;
                vm.Id = loan.Id;
                return View(vm);
            }
            return RedirectToAction("Error", "Home", "");
        }

        //// POST: Books/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLoanStatus(ReturnLoanVm vm)
        {
            if (ModelState.IsValid)
            {
                var updatedLoan = new Loan();
                updatedLoan.Id = vm.Id;
                updatedLoan.MemberId = vm.MemberId;
                updatedLoan.bookCopyId = vm.BookCopyId;
                updatedLoan.LoanDate = vm.LoanDate;
                updatedLoan.Returned = true;
                updatedLoan.ReturnDate = datetimeservice.Now;
                loanservice.UpdateLoan(updatedLoan);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Error", "Home", "");
        }


        public async Task<IActionResult> Current()
        {
            var vm = new LoanCurrentVm();
            vm.Loans = loanservice.GetAllCurrentLoans();
            vm.CurrentDate = datetimeservice.Now;
            return View(vm);
        }
    }
}
