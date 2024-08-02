using System;

namespace EduConnect
{
    public class Student
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string School { get; set; }
        public string Class { get; set; }
        public string Sport { get; set; }
        public string TrainersName { get; set; }
        public string Rank { get; set; }
        public string OrderNumber { get; set; }
        public string EnrollmentGroup { get; set; }
        public DateTime DateOfEnrollment { get; set; }
        public string OrderEnrollment { get; set; }
        public string OrderDismissal { get; set; }
        public string PaymentType { get; set; }
        public string ORP_or_SP { get; set; }
        public string ParentsFullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ParentsWorkPlace { get; set; }
        public string ParentsPosition { get; set; }
        public string BirthCertificate { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string IssuedBy { get; set; }
        public string SNILS { get; set; }
        public string INN { get; set; }
    }
}