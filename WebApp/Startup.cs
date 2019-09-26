using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //添加session支持
            services.AddSession(option =>
            {
                option.Cookie.Name = "AspNetCore.Session";
                option.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            //自定义配置文件
            services.AddOptions();
            services.Configure<HostingSettingOption>(Configuration);

            //EF数据库上下文
            services.AddDbContext<BloggingContext>(option => option.UseSqlite("Filename=./efcoredemo.db"));

            //添加自定义路由参数验证
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("email", typeof(EmailRouterConstraint));
            });

            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new ProtobufFormatter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var pathBase = Configuration["PATH_BASE"];
            app.UseSession();
            app.Use(async (context, next) =>
            {
                System.Diagnostics.Debug.WriteLine("----------------------sessionID " + context.Session.Id);
                context.Items["Verified"] = true;
                context.Session.Set("sss", System.Text.Encoding.Default.GetBytes("hoho"));
                await next.Invoke();
            });

            app.Use(async (context, next) =>
            {
                System.Diagnostics.Debug.WriteLine("----------------------Items " + context.Items["Verified"]);
                byte[] byteArray;
                context.Session.TryGetValue("sss", out byteArray);
                System.Diagnostics.Debug.WriteLine("----------------------sessionkey " + System.Text.Encoding.Default.GetString(byteArray));
                await next.Invoke();
            });
            //自定义中间件
            app.UseRequestIP();

            //已定义日志存储
            loggerFactory.AddProvider(new ColorLoggerProvider());

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(env.WebRootPath)
            //    //RequestPath = new PathString("/staticfiles")
            //});

            //设置默认页面
            //var defaultOption = new DefaultFilesOptions();
            //defaultOption.DefaultFileNames.Clear();
            //defaultOption.DefaultFileNames.Add("mydefault.html");
            //app.UseDefaultFiles(defaultOption);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute("manage", "Manage", "Manage/{controller}/{action}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
