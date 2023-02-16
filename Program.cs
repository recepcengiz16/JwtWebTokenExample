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
//        ValidIssuer = builder.Configuration["Token:Issuer"], //Ýlgili token'ý oluþturan
//        ValidAudience = builder.Configuration["Token:Audience"], //Token'ý kullanacak olanlar
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])), /*3 tip þifreleme var.
//            Simetrik(Þifeleyen de þifreyi çözen de ayný keye sahip oluyor. Ve bu þekilde þifreyi çözebiliyoruz. Jwt simetri þifrelenir.),
//            Asimetrik(Þifreleyen de þifreyi çözen de farklý keyler kullanýyor.),
//            Hash(Data'yý bir kere þireleyince o data artýk kaybederiz. O datayý artýk çözemeyiz.) 

//        */
//        //ValidateIssuer = true, istersek kontrol ettiririz bunlarý da.
//        //ValidateAudience = true,
//        ValidateLifetime = true, //token zamaný geçmiþ mi geçmemiþ mi kontrolünü yapýyor
//        ValidateIssuerSigningKey = true ,//key kontrolü yap
//        ClockSkew=TimeSpan.Zero //sunucu ile client arasýnda gecikme süresi atanýyor, zero diyerek herhangi bir gecikme süresi olmasýn dedik

//    };
//})
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme , opt =>
    {
        opt.Cookie.Name = "CustomCookie";
        opt.Cookie.HttpOnly = true; //ilgili cookie bilgilerinin js ile çekilmesini engelliyor. 
        opt.Cookie.SameSite = SameSiteMode.Strict; //cookie paylaþýma kapalý
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
