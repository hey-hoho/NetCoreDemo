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
using System.IO;

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

            //services.AddSwaggerGen(options =>
            //{
            //    //options.DescribeAllEnumsAsStrings();
            //    options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
            //    {
            //        Title = "Marketing HTTP API",
            //        Version = "v1",
            //        Description = "The Marketing Service HTTP API",
            //        TermsOfService = "Terms Of Service"
            //    });
            //    var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            //    var xmlPath = Path.Combine(basePath, "WebApp.xml");
            //    options.IncludeXmlComments(xmlPath);
            //});
            //API版本控制
            //services.AddApiVersioning(o =>
            //{
            //    //ReportApiVersions设置为true, 在Api请求的响应头部，会追加当前Api支持的版本
            //    o.ReportApiVersions = true;
            //    //标记当客户端没有指定版本号的时候，是否使用默认版本号
            //    o.AssumeDefaultVersionWhenUnspecified = true;
            //    //默认版本号
            //    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            //    //使用请求头来控制api版本
            //    //o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            //    //使用查询字符串和请求头来控制版本
            //    //o.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader(), new HeaderApiVersionReader("x-api-version"));
            //});
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

            //app.UseSwagger().UseSwaggerUI(c =>
            //  {
            //      c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Marketing.API V1");
            //      c.OAuthClientId("marketingswaggerui");
            //      c.OAuthAppName("Marketing Swagger UI");
            //  });

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
