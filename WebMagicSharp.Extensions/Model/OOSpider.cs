using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp;
using WebMagicSharp.Model;
using WebMagicSharp.Pipelines;
using WebMagicSharp.Processor;

namespace WebMagicSharp.Model
{
    public class OOSpider<T> : Spider
    {

        private ModelPageProcessor modelPageProcessor;

        private ModelPipeline<T> modelPipeline;

        private IPageModelPipeline<T> pageModelPipeline;

        private List<Type> pageModelTypes = new List<Type>();

        protected OOSpider(ModelPageProcessor modelPageProcessor) : base(modelPageProcessor)
        {
            this.modelPageProcessor = modelPageProcessor;
        }

        public OOSpider(IPageProcessor pageProcessor) : base(pageProcessor)
        {
            this.modelPipeline = new ModelPipeline<T>();
        }

        public OOSpider(Site site, IPageModelPipeline<T> pageModelPipeline, Type[] pageModels) : base(ModelPageProcessor.Create(site, pageModels))
        {
            this.modelPipeline = new ModelPipeline<T>();
            this.AddPipeline(modelPipeline);
            foreach (var pageModel in pageModels)
            {
                if (pageModelPipeline != null)
                    this.modelPipeline.Put(pageModel, pageModelPipeline);
                pageModelTypes.Add(pageModel);
            }
        }

        public OOSpider(Site site, IPageModelPipeline<T> pageModelPipeline, Type pageModel) : base(ModelPageProcessor.Create(site, pageModels))
        {
            this.modelPipeline = new ModelPipeline<T>();
            this.AddPipeline(modelPipeline);
            if (pageModelPipeline != null)
                    this.modelPipeline.Put(pageModel, pageModelPipeline);
            pageModelTypes.Add(pageModel);
        }

        protected override ICollectorPipeline<T1> GetCollectorPipeline<T1>()
        {
            return new PageModelCollectorPipeline<T1>(pageModelTypes.FirstOrDefault());
        }

        public OOSpider<T> AddPageModel(IPageModelPipeline<T> pageModelPipeline, Type[] pageModels)
        {
            foreach(var pageModel in pageModels)
            {
                modelPageProcessor.AddPageModel(pageModel);
                modelPipeline.Put(pageModel, pageModelPipeline);
            }
            return this;
        }

        public OOSpider<T> SetIsExtractLinks(bool isExtractLinks)
        {
            modelPageProcessor.IsExtractLinks = isExtractLinks;
            return this;
        }

    }

    public static class OOSpider
    {
        public static T Run<T>(string url) where T : class
        {
            return Create<T>(Site.Me, typeof(T)).Get<T>(url);
        }

        public static OOSpider<T> Create<T>(Site site, Type[] pageModels)
        {
            return new OOSpider<T>(site, null, pageModels);
        }

        public static OOSpider<T> Create<T>(Site site, IPageModelPipeline<T> pageModelPipeline)
        {
            return new OOSpider<T>(site, pageModelPipeline, typeof(T));
        }

        public static OOSpider<T> Create<T>(Site site, IPageModelPipeline<T> pageModelPipeline, Type[] pageModels)
        {
            return new OOSpider<T>(site, pageModelPipeline, pageModels);
        }

        public static OOSpider<T> Create<T>()
        {
            return new OOSpider<T>(Site.Me, null, typeof(T));
        }

        public static OOSpider<T> Create<T>(IPageModelPipeline<T> pageModelPipeline)
        {
            return new OOSpider<T>(Site.Me, pageModelPipeline, typeof(T));
        }

        public static OOSpider<T> Create<T>(Site site)
        {
            return new OOSpider<T>(site, null, typeof(T));
        }

        public static OOSpider<T> Create<T>(Site site, Type pageModel)
        {
            return new OOSpider<T>(site, null, pageModel);
        }

        public static OOSpider<T> Create<T>(Site site, IPageModelPipeline<T> pageModelPipeline, Type pageModel)
        {
            return new OOSpider<T>(site, pageModelPipeline, pageModel);
        }

    }

}
