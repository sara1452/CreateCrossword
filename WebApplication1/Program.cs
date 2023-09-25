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
//הגדרת הרשאת גישה
builder.Services.AddCors(opotion => opotion.AddPolicy("AllowAll",//נתינת שם להרשאה
    p => p.AllowAnyOrigin()//מאפשר כל מקור
    .AllowAnyMethod()//כל מתודה - פונקציה
    .AllowAnyHeader()));//וכל כותרת פונקציה

builder.Services.AddAutoMapper(typeof(Program));
//הגדרה למנהל התלויות מה להזריק כאשר הוא רואה שיש בנאי
//המחכה לקבל מופע:
//סוג המוזרק לDBContext:
builder.Services.AddDbContext<CrosswordContext>(x => x.UseSqlServer("Server=localhost\\SQLEXPRESS15;Database=crossword;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False"));

//הזרת תלויות של ממשקים ומחלקות
//תכני - עבור כל ממשק שיוצרים
//יש לכתוב שורה זו
//הסוג הראשון הוא שם הממשק
//השוג השני הוא שם המחלקה שמממשת את הממשק
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
    //שימוש בהרשאת גישה 
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
