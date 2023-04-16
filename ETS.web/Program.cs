using ETS.web.DAL;
using ETS.web.Helper;
using ETS.web.Helper.JWT;
using ETS.web.Interface;
using ETSystem.Helper;
using ETSystem.Interface;
//using ETSystem.Interface;
using ETSystem.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddSignalR();
builder.Services.AddTransient<INoticeRepository, NoticeRepository>();
builder.Services.AddTransient<IObjQRepository, ObjQRepository>();
builder.Services.AddTransient<IEUserRepository, EUserRepository>();
builder.Services.AddTransient<ISubjectRepository, SubjectRepository>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<IQuestionPaperRepository, QuestionPaperRepository>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<ITExamRepository, TExamRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IEQuestionsRepository, EQuestionsRepository>();
builder.Services.AddTransient<IEAnswerRepository, EAnswerRepository>();
builder.Services.AddTransient<IEDetailsRepository, EDetailsRepository>();
builder.Services.AddTransient<IResultRepository, ResultRepository>();
builder.Services.AddTransient<ITeacherDashRepoitory, TeacherDashRepository>();
builder.Services.AddTransient<IAdminDashRepository, AdminDashRepository>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IJWTService, JWTService>();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<APIHeaderFilter>();
});
var app = builder.Build();
StaticHelper.InitConfig(builder.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseMiddleware<JWTMiddleware>();
//app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins(""));
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));
app.UseAuthorization();
app.MapControllers();
app.Run();