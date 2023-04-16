namespace ETS.web.Model.Result
{
    public class ResultListTeacher
    {
        public int FullMark { set; get; }
        public int PassMark { set; get; }
        public int Class { set; get; }
        public string SubjectName { set; get; }
        public string ExamDescription { set; get; }
        public DateTime ExamDate { set; get; }
        public int MarkObtained { get; set; }
        public string Grade { set; get; }

        public int IExamId { get; set; }
        public string FullName { get; set; }
    }
}
