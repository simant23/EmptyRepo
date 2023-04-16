using System.ComponentModel.DataAnnotations;


namespace ETSystem.Model.LOGINREG
{
    public class SREG_Model
    {
        public string? FullName { get; set; }

        public string? EmailId { get; set; }

        public string? Phone { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public string? Password { get; set; }

        public string? GuardianName { get; set; }

        public int Class { get; set; }

        public string? DOB { get; set; }

        public string? EnrollmentDate { get; set; }

        public int StudentId { get; set; }


    }
}
