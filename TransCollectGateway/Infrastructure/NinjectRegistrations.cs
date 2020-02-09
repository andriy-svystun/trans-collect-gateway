using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using TransCollectGateway.Common;
using TransCollectGateway.Store;
using TransCollectGateway.Infrastructure;

namespace TransCollectGateway.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Transaction>>().To<TransactionRepository>();
            Bind<ITransLogFileProcessing>().To<TransLogFileProcessing>();
            Bind<ITransLogUploadService>().To<TransLogUploadService>();
        }
    }
}