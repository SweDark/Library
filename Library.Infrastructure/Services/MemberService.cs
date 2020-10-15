using Library.Application.Interfaces;
using Library.Domain;
using Library.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Infrastructure.Services
{
    public class MemberService : IMemberService
    {

        private readonly ApplicationDbContext context;

        public MemberService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddMember(Member member)
        {
            context.Add(member);
            context.SaveChanges();
        }

        public ICollection<Member> GetAllMembers()
        {
            // Here we are using .Include() to eager load the author, read more about loading related data at https://docs.microsoft.com/en-us/ef/core/querying/related-data
            return context.Members.OrderBy(x => x.Name).ToList();
        }

        public Member GetMember(int id)
        {
            Member member = new Member();
            member = context.Members.SingleOrDefault(x => x.Id == id);
            return member;
        }

        public void UpdateMember(Member member)
        {
            context.Update(member);
            context.SaveChanges();
        }
        public void UpdateMember(int id, Member member)
        {
            context.Update(member);
            context.SaveChanges();
        }
        public void DeleteMember(Member member, ICollection<Loan> loanedBooks)
        {
            foreach(var book in loanedBooks)
            {
                if(book.MemberId == member.Id)
                {
                    throw new OperationCanceledException();
                }
            }
            context.Remove(member);
            context.SaveChanges();
        }
    }
}
