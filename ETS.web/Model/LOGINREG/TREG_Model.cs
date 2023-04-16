﻿

using System.ComponentModel.DataAnnotations;

namespace ETSystem.Model.LOGINREG
{
    public class TREG_Model
    {
        public string? EmailId { get; set; }

        public string? FullName { get; set; }

        public string? Phone { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public string? Password { get; set; }

        public int TeacherId { get; set; }

        public string? Qualification { get; set; }

        public string? Major { get; set; }

        public string? HireDate { get; set; }

        public string? SubTeacher { get; set; }

        public int TExperience { get; set; }
    }
}
