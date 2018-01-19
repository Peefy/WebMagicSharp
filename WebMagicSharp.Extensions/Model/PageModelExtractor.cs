using System;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using WebMagicSharp;
using WebMagicSharp.Selector;

namespace WebMagicSharp.Model
{
    public class PageModelExtractor
    {
        private List<Regex> targetUrlPatterns = new List<Regex>();

        private List<Regex> helpUrlPatterns = new List<Regex>();

        private ISelector targetUrlRegionSelector;

        private ISelector helpUrlRegionSelector;

        private List<FieldExtractor> fieldExtractors;

        private Extractor objectExtractor;

        private Type type;

        public static PageModelExtractor create(Type type)
        {
            PageModelExtractor pageModelExtractor = new PageModelExtractor();
            pageModelExtractor.init(type);
            return pageModelExtractor;
        }

        private void init(Type type)
        {
            this.type = type;
            initClassExtractors();
            fieldExtractors = new List<FieldExtractor>();
            for (Field field : ClassUtils.getFieldsIncludeSuperClass(clazz))
            {
                field.setAccessible(true);
                FieldExtractor fieldExtractor = getAnnotationExtractBy(clazz, field);
                FieldExtractor fieldExtractorTmp = getAnnotationExtractCombo(clazz, field);
                if (fieldExtractor != null && fieldExtractorTmp != null)
                {
                    throw new IllegalStateException("Only one of 'ExtractBy ComboExtract ExtractByUrl' can be added to a field!");
                }
                else if (fieldExtractor == null && fieldExtractorTmp != null)
                {
                    fieldExtractor = fieldExtractorTmp;
                }
                fieldExtractorTmp = getAnnotationExtractByUrl(clazz, field);
                if (fieldExtractor != null && fieldExtractorTmp != null)
                {
                    throw new IllegalStateException("Only one of 'ExtractBy ComboExtract ExtractByUrl' can be added to a field!");
                }
                else if (fieldExtractor == null && fieldExtractorTmp != null)
                {
                    fieldExtractor = fieldExtractorTmp;
                }
                if (fieldExtractor != null)
                {
                    fieldExtractor.setObjectFormatter(new ObjectFormatterBuilder().setField(field).build());
                    fieldExtractors.add(fieldExtractor);
                }
            }
        }

        private void initClassExtractors()
        {
            throw new NotImplementedException();
        }

    }
}
