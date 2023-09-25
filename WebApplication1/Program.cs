using BLL.FunctionBLL;
using BLL.InterfaceBLL;
using DAL.Functions;
using DAL.FunctionsDAL;
using DAL.Interfaces;
using DAL.InterfacesDAL;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//����� ����� ����
builder.Services.AddCors(opotion => opotion.AddPolicy("AllowAll",//����� �� ������
    p => p.AllowAnyOrigin()//����� �� ����
    .AllowAnyMethod()//�� ����� - �������
    .AllowAnyHeader()));//��� ����� �������

builder.Services.AddAutoMapper(typeof(Program));
//����� ����� ������� �� ������ ���� ��� ���� ��� ����
//����� ���� ����:
//��� ������ �DBContext:
builder.Services.AddDbContext<CrosswordContext>(x => x.UseSqlServer("Server=localhost\\SQLEXPRESS15;Database=crossword;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False"));

//���� ������ �� ������ �������
//���� - ���� �� ���� �������
//�� ����� ���� ��
//���� ������ ��� �� �����
//���� ���� ��� �� ������ ������ �� �����
builder.Services.AddScoped(typeof(IAllCrosswordBLL), typeof(AllCrosswordBLL));
builder.Services.AddScoped(typeof(IcrosswordUserBLL), typeof(UserCrosswordBLL));
builder.Services.AddScoped(typeof(IuserBLL), typeof(userBLL));
builder.Services.AddScoped(typeof(IwordAndDefinitionBLL), typeof(WordAndDefinitionBLL));

builder.Services.AddScoped(typeof(Icrosswords), typeof(crosswordsDAL));
builder.Services.AddScoped(typeof(Iuser), typeof(userDAL));
builder.Services.AddScoped(typeof(IuserCrossword), typeof(CrosswordUserDAL));
builder.Services.AddScoped(typeof(IwordAndDefinition), typeof(WordAndDefinitionFuncDAL));

builder.Services.AddControllers()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    //����� ������ ���� 
}

var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
