using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Interfaces
{
    public interface IMemberService
    {

        void AddMember(Member member);
        ICollection<Member> GetAllMembers();

        Member GetMember(int id);

        void UpdateMember(Member member);
        void UpdateMember(int id, Member member);
        void DeleteMember(Member member, ICollection<Loan> loanedBooks);



    }
}
