using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DatabaseStructure.Models;
using DatabaseStructure;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using AutoMapper;
using EducationalSystem.WebAPI.Profiles;

namespace EducationalSystem.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.Bind("Project", new Config());
            string con = "Server=(local);Database=EducationalSystem;Trusted_Connection=True;";
            // устанавливаем контекст данных
            services.AddDbContext<DBContext>(options => options.UseSqlServer(con));

            services.AddControllers(); // используем контроллеры без представлений

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ActivePersonProfile());
                mc.AddProfile(new PersonProfile());
                mc.AddProfile(new ActiveProfessorCoursesProfile());
                mc.AddProfile(new CreateCourseProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}