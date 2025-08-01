using ClassRegistrationApplication2025.Application.DTOs;
using ClassRegistrationApplication2025.Application.Interfaces;
using ClassRegistrationApplication2025.Application.Services;
using ClassRegistrationApplication2025.Application.UseCases;
using ClassRegistrationApplication2025.Infrastructure;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Database;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Interfaces;
using ClassRegistrationApplication2025.Infrastructure.Persistence.Repositories;
using ClassRegistrationApplication2025.Infrastructure.Services;
using ClassRegistrationApplication2025.Presentation.Components;
using ClassRegistrationApplication2025.Presentation.Pages.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using SurveyBuilder.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IEmailService, SmtpEmailService>();
builder.Services.AddScoped<IValidator<CreateClassDto>, CreateClassDtoFluentValidator>();
builder.Services.AddScoped<IValidator<CreateSubjectDto>, CreateSubjectDtoFluentValidator>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISubjectSurveyService, SubjectSurveyService>();
builder.Services.AddScoped<IClassSurveyService, ClassSurveyService>();
builder.Services.AddSingleton<ISurveyJsonService, SurveyJsonService>();
builder.Services.AddScoped<GetAllClassesUseCase>();
builder.Services.AddScoped<GetClassDetailsUseCase>();
builder.Services.AddScoped<RegisterForClassUseCase>();
builder.Services.AddScoped<UpdateClassUseCase>();
builder.Services.AddScoped<DeleteClassUseCase>();
builder.Services.AddScoped<RegisteredUsersDto>();
builder.Services.AddScoped<GetRegisteredUsersUseCase>();
builder.Services.AddScoped<CreateClassUseCase>();
builder.Services.AddScoped<CreateSubjectUseCase>();
builder.Services.AddScoped<GetAllSubjectsUseCase>();
builder.Services.AddScoped<GetAllClassesBySubjectIdUseCase>();
builder.Services.AddScoped<UpdateSubjectUseCase>();
builder.Services.AddScoped<GetSubjectByIdUseCase>();
builder.Services.AddScoped<DeleteSubjectUseCase>();
builder.Services.AddScoped<GetSurveyBySubjectIdUseCase>();
builder.Services.AddScoped<GetSurveyByClassIdUseCase>();
builder.Services.AddScoped<GetSubjectIdFromClassIdUseCase>();
builder.Services.AddScoped<UpdateAttendanceUseCase>();









builder.Services.Configure<AdSettings>(builder.Configuration.GetSection("AdSettings"));

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
