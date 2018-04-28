using Nancy;
  
namespace NancyApplication
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => "Hello from Nancy running on CoreCLR");
            Get("/test/{name}", args => new Person() { Name = args.name });
        }
    }
 
    public class Person
    {
        public string Name { get; set; }
    }
}