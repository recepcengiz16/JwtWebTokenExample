using JwtWebTokenExample.Manager.AppExtensions.DependencyResolves;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

builder.Services.AddDependencies(builder.Configuration);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
//{
//    opt.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidIssuer = builder.Configuration["Token:Issuer"], //�lgili token'� olu�turan
//        ValidAudience = builder.Configuration["Token:Audience"], //Token'� kullanacak olanlar
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])), /*3 tip �ifreleme var.
//            Simetrik(�ifeleyen de �ifreyi ��zen de ayn� keye sahip oluyor. Ve bu �ekilde �ifreyi ��zebiliyoruz. Jwt simetri �ifrelenir.),
//            Asimetrik(�ifreleyen de �ifreyi ��zen de farkl� keyler kullan�yor.),
//            Hash(Data'y� bir kere �ireleyince o data art�k kaybederiz. O datay� art�k ��zemeyiz.) 

//        */
//        //ValidateIssuer = true, istersek kontrol ettiririz bunlar� da.
//        //ValidateAudience = true,
//        ValidateLifetime = true, //token zaman� ge�mi� mi ge�memi� mi kontrol�n� yap�yor
//        ValidateIssuerSigningKey = true ,//key kontrol� yap
//        ClockSkew=TimeSpan.Zero //sunucu ile client aras�nda gecikme s�resi atan�yor, zero diyerek herhangi bir gecikme s�resi olmas�n dedik

//    };
//})
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme , opt =>
    {
        opt.Cookie.Name = "CustomCookie";
        opt.Cookie.HttpOnly = true; //ilgili cookie bilgilerinin js ile �ekilmesini engelliyor. 
        opt.Cookie.SameSite = SameSiteMode.Strict; //cookie payla��ma kapal�
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
       // opt.ExpireTimeSpan = TimeSpan.FromDays(1);
        opt.LoginPath = new PathString("/Home/Login");
        opt.LogoutPath = new PathString("/Home/LogOut");

         opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();




app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
