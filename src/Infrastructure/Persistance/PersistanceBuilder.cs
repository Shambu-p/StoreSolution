using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreSolution.Infrastructure.Persistance.configuration;

namespace StoreSolution.Infrastructure.Persistance
{
    public static class PersistanceBuilder {
        
        public static ModelBuilder Build(ModelBuilder builder) {

            builder = BoxConfiguration.onBuild(builder);
            builder = BoxItemConfiguration.onBuild(builder);
            builder = StoreConfiguration.onBuild(builder);
            builder = StoreItemConfiguration.onBuild(builder);
            builder = ItemConfiguration.onBuild(builder);
            // builder = UserConfiguration.onBuild(builder);

            return builder;

        }

    }
}