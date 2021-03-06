﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Bifrost.Configuration;
using Web.Cart;
using Web.Messaging;
using Web.Navigation;

namespace Web
{
    public class BifrostConfigurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            configure
                .Serialization
                    .UsingJson()

#if(true)
                .Frontend
                    .Web(w =>
                    {
                        w.AsSinglePageApplication();

                        var baseNamespace = global::Bifrost.Configuration.Configure.Instance.EntryAssembly.GetName().Name;
                        var @namespace = string.Format("{0}.**.", baseNamespace);

                        w.PathsToNamespaces.Add("**/", @namespace);
                        w.PathsToNamespaces.Add("/**/", @namespace);
                        w.PathsToNamespaces.Add("", baseNamespace);

                        w.NamespaceMapper.Add(string.Format("{0}.**.", baseNamespace), string.Format("{0}.Domain.**.", baseNamespace));
                        w.NamespaceMapper.Add(string.Format("{0}.**.", baseNamespace), string.Format("{0}.Read.**.", baseNamespace));
                        w.NamespaceMapper.Add(string.Format("{0}.**.", baseNamespace), string.Format("{0}.**.", baseNamespace));
                    });
#endif

            configure.Container.Get<IMessageBroker>();
            configure.Container.Get<ICartRepository>().Configure();
            configure.Container.Get<ICategoryRepository>().Configure();
        }
    }
}