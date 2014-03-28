namespace Minate.Extensions
{
    using Castle.Windsor;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using Castle.Core;
    using Castle.Core.Resource;
    using Castle.Windsor.Configuration.Interpreters;

    public static class WindsorContainerExtensions
    {
        public static WindsorContainer PopulateFromConfig(this WindsorContainer container)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var controllerTypes = assembly.GetTypes().Where(t => typeof (IController).IsAssignableFrom(t));
            
            container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
            controllerTypes.ForEach(t => container.AddComponentLifeStyle(t.FullName, t, LifestyleType.Transient));

            return container;
        }
    }
}