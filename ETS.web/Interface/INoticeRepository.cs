using ETSystem.Model;
using ETSystem.Model.Notice;
using System.Data.SqlClient;

namespace ETSystem.Repository
{


    public interface INoticeRepository
    {

        List<Notice> GetAll(SqlConnection connection);
        Notice GetById(int notice, SqlConnection connection);


        Response Create(CreateNotice cnotice, SqlConnection connection);

        Response Update(CreateNotice cnotice, SqlConnection connection);

        Response Delete(int notice, SqlConnection connection);

    }



}
