namespace MultiBuffer.WebApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MultiBuffer.WebApi.DataModel.MultiBufferContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MultiBuffer.WebApi.DataModel.MultiBufferContext";
        }

        protected override void Seed(MultiBuffer.WebApi.DataModel.MultiBufferContext context)
        {

        }
    }
}
