﻿namespace ETSystem.Model.User
{
    public class Student
    {
        public int UserId { get; set; }
        public int StudentId { get; set; }
        public string? EmailId { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? GuardianName { get; set; }
        public int Grade { get; set; }
        public DateTime DOB { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
