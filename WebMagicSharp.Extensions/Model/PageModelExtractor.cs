using System;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Generic;

using WebMagicSharp;
using WebMagicSharp.Model.Attributes;
using WebMagicSharp.Model.Formatter;
using WebMagicSharp.Selector;
using WebMagicSharp.Utils;

namespace WebMagicSharp.Model
{
    public class PageModelExtractor
    {
        private List<Regex> targetUrlRegexs = new List<Regex>();

        private List<Regex> helpUrlRegexs = new List<Regex>();

        private ISelector targetUrlRegionSelector;

        private ISelector helpUrlRegionSelector;

        private List<FieldExtractor> fieldExtractors;

        private List<PropertyExtractor> propertyExtractors;

        private Extractor objectExtractor;

        private Type type;

        public static PageModelExtractor Create(Type type)
        {
            PageModelExtractor pageModelExtractor = new PageModelExtractor();
            pageModelExtractor.Init(type);
            return pageModelExtractor;
        }

        private void Init(Type type)
        {
            this.type = type;
            InitClassExtractors();
            fieldExtractors = new List<FieldExtractor>();
            propertyExtractors = new List<PropertyExtractor>();
            var fields = TypeUtil.GetFieldsIncludeBaseClass(type);
            var propertys = TypeUtil.GetPropertysIncludeBaseClass(type);
            foreach(var field in fields)
            {
                var fieldExtractor = GetAttributeExtractBy(type, field);
                var fieldExtractorTmp = GetAttributeExtractCombo(type, field);
                if (fieldExtractor != null && fieldExtractorTmp != null)
                {
                    throw new Exception("Only one of 'ExtractByAttribute, ComboExtractAttribute or ExtractByUrl' can be added to a field!");
                }
                else if (fieldExtractor == null && fieldExtractorTmp != null)
                {
                    fieldExtractor = fieldExtractorTmp;
                }
                fieldExtractorTmp = GetAttributeExtractByUrl(type, field);
                if (fieldExtractor != null && fieldExtractorTmp != null)
                {
                    throw new Exception("Only one of 'ExtractBy ComboExtract ExtractByUrl' can be added to a field!");
                }
                else if (fieldExtractor == null && fieldExtractorTmp != null)
                {
                    fieldExtractor = fieldExtractorTmp;
                }
                if (fieldExtractor != null)
                {
                    fieldExtractor.ObjectFormatter = new ObjectFormatterBuilder<IObjectFormatter>().SetField(field).Build();
                    fieldExtractors.Add(fieldExtractor);
                }
            }
            foreach(var property in propertys)
            {
                var propertyExtractor = GetAttributeExtractBy(type, property);
                var propertyExtractorTmp = GetAttributeExtractCombo(type, property);
                if (propertyExtractor != null && propertyExtractorTmp != null)
                {
                    throw new Exception("Only one of 'ExtractByAttribute, ComboExtractAttribute or ExtractByUrl' can be added to a field!");
                }
                else if (propertyExtractor == null && propertyExtractorTmp != null)
                {
                    propertyExtractor = propertyExtractorTmp;
                }
                propertyExtractorTmp = GetAttributeExtractByUrl(type, property);
                if (propertyExtractor != null && propertyExtractorTmp != null)
                {
                    throw new Exception("Only one of 'ExtractBy ComboExtract ExtractByUrl' can be added to a field!");
                }
                else if (propertyExtractor == null && propertyExtractorTmp != null)
                {
                    propertyExtractor = propertyExtractorTmp;
                }
                if (propertyExtractor != null)
                {
                    propertyExtractor.ObjectFormatter = new ObjectFormatterBuilder<IObjectFormatter>().SetProperty(property).Build();
                    propertyExtractors.Add(propertyExtractor);
                }
            }              
        }

        private PropertyExtractor GetAttributeExtractByUrl(Type type, PropertyInfo property)
        {
            PropertyExtractor propertyExtractor = null;
            var extractByUrlAttr = AttributeUtil.GetAttribute<ExtractByUrlAttribute>(property);
            if (extractByUrlAttr != null)
            {
                var regexPattern = extractByUrlAttr.Value;
                if (regexPattern.Trim().Equals(""))
                {
                    regexPattern = ".*";
                }
                propertyExtractor = new PropertyExtractor(property,
                    new RegexSelector(regexPattern), Source.Url,
                    extractByUrlAttr.NotNull, extractByUrlAttr.IsMulti ||
                    typeof(List<object>).IsAssignableFrom(property.GetType()));
            }
            return propertyExtractor;
        }

        private FieldExtractor GetAttributeExtractByUrl(Type type, FieldInfo field)
        {
            FieldExtractor fieldExtractor = null;
            var extractByUrlAttr = AttributeUtil.GetAttribute<ExtractByUrlAttribute>(field);
            if(extractByUrlAttr != null)
            {
                var regexPattern = extractByUrlAttr.Value;
                if(regexPattern.Trim().Equals(""))
                {
                    regexPattern = ".*";
                }
                fieldExtractor = new FieldExtractor(field,
                    new RegexSelector(regexPattern), Source.Url,
                    extractByUrlAttr.NotNull, extractByUrlAttr.IsMulti ||
                    typeof(List<object>).IsAssignableFrom(field.GetType()))
                {
                    SetterMethod = GetSetterMethod(type, field)
                };
            }
            return fieldExtractor;
        }

        private PropertyExtractor GetAttributeExtractCombo(Type type, PropertyInfo property)
        {
           PropertyExtractor propertyExtractor = null;
            var comboExtract = AttributeUtil.GetAttribute<ComboExtractAttribute>(property);
            if (comboExtract != null)
            {
                var extractBys = comboExtract.Value;
                var selectors = ExtractorUtils.GetSelectors(extractBys);
                ISelector selector = new AndSelector(selectors);
                switch (comboExtract.Op)
                {
                    case Op.And:
                        selector = new AndSelector(selectors);
                        break;
                    case Op.Or:
                        selector = new OrSelector(selectors);
                        break;
                }
                var source = comboExtract.Source == ExtractSource.RawHtml ? Source.RawHtml : Source.Html;
                propertyExtractor = new PropertyExtractor(property, selector, source,
                    comboExtract.NotNull, comboExtract.IsMulti ||
                    typeof(List<object>).IsAssignableFrom(property.GetType()));
            }
            return propertyExtractor;
        }

        private FieldExtractor GetAttributeExtractCombo(Type type, FieldInfo field)
        {
            FieldExtractor fieldExtractor = null;
            var comboExtract = AttributeUtil.GetAttribute<ComboExtractAttribute>(field);
            if(comboExtract != null)
            {
                var extractBys = comboExtract.Value;
                var selectors = ExtractorUtils.GetSelectors(extractBys);
                ISelector selector = new AndSelector(selectors);
                switch(comboExtract.Op)
                {
                    case Op.And:
                        selector = new AndSelector(selectors);
                        break;
                    case Op.Or:
                        selector = new OrSelector(selectors);
                        break;
                }
                var source = comboExtract.Source == ExtractSource.RawHtml ? Source.RawHtml : Source.Html;
                fieldExtractor = new FieldExtractor(field, selector, source,
                    comboExtract.NotNull, comboExtract.IsMulti ||
                    typeof(List<object>).IsAssignableFrom(field.GetType()))
                {
                    SetterMethod = GetSetterMethod(type, field) ?? null
                };
            }
            return fieldExtractor;
        }

        private FieldExtractor GetAttributeExtractBy(Type type, FieldInfo field)
        {
            FieldExtractor fieldExtractor = null;
            var extractBy = AttributeUtil.GetAttribute<ExtractByAttribute>(field);
            if(extractBy != null)
            {
                var selector = ExtractorUtils.GetSelector(extractBy);
                var sourceTmp = extractBy.Source;
                if(extractBy.Type == ExtractType.JsonPath)
                {
                    sourceTmp = ExtractSource.RawText;
                }
                Source source = Source.Html;
                switch(sourceTmp)
                {
                    case ExtractSource.RawText:
                        source = Source.RawText;
                        break;
                    case ExtractSource.RawHtml:
                        source = Source.RawHtml;
                        break;
                    case ExtractSource.SelectedHtml:
                        source = Source.Html;
                        break;
                }
                fieldExtractor = new FieldExtractor(field, selector, source,
                    extractBy.NotNull, true)
                {
                    SetterMethod = null
                };

            }
            return fieldExtractor;
        }

        private PropertyExtractor GetAttributeExtractBy(Type type, PropertyInfo property)
        {
            PropertyExtractor propertyExtractor = null;
            var extractBy = AttributeUtil.GetAttribute<ExtractByAttribute>(property);
            if (extractBy != null)
            {
                var selector = ExtractorUtils.GetSelector(extractBy);
                var sourceTmp = extractBy.Source;
                if (extractBy.Type == ExtractType.JsonPath)
                {
                    sourceTmp = ExtractSource.RawText;
                }
                Source source = Source.Html;
                switch (sourceTmp)
                {
                    case ExtractSource.RawText:
                        source = Source.RawText;
                        break;
                    case ExtractSource.RawHtml:
                        source = Source.RawHtml;
                        break;
                    case ExtractSource.SelectedHtml:
                        source = Source.Html;
                        break;
                }
                propertyExtractor = new PropertyExtractor(property, selector, source,
                    extractBy.NotNull, true);
            }
            return propertyExtractor;
        }

        public static MethodInfo GetSetterMethod(Type type, PropertyInfo property)
        {
            return property?.SetMethod;
        }

        public static MethodInfo GetSetterMethod(Type type, FieldInfo field)
        {
            return null;
        }

        private void InitClassExtractors()
        {
            var targeturlAttr = AttributeUtil.GetAttribute<TargetUrlAttribute>(type);
            if (targeturlAttr == null)
                targetUrlRegexs.Add(new Regex(".*"));
            else
            {
                var value = (targeturlAttr as TargetUrlAttribute).Value;
                targetUrlRegexs.Add(new Regex(value.Replace(".", "\\.").
                    Replace("*", "[^\"'#]*")));
                if(!(targeturlAttr as TargetUrlAttribute).SourceRegion.Equals(""))
                {
                    targetUrlRegionSelector = new XPathSelector((targeturlAttr as TargetUrlAttribute).SourceRegion);
                }
            }
            var helpUrlAttr = AttributeUtil.GetAttribute<HelpUrlAttribute>(type);
            if(helpUrlAttr != null)
            {
                var value = helpUrlAttr.Value;
                helpUrlRegexs.Add(new Regex(value.Replace(".", "\\.")
                    .Replace("*", "[^\"'#]*")));
                if (!helpUrlAttr.SourceRegion.Equals(""))
                    helpUrlRegionSelector = new XPathSelector(helpUrlAttr.SourceRegion);
            }
            var extractByAttr = AttributeUtil.GetAttribute<ExtractByAttribute>(type);
            if(extractByAttr != null)
            {
                objectExtractor = new Extractor(new XPathSelector(extractByAttr.Value),
                    Source.Html, extractByAttr.NotNull, extractByAttr.IsMulti);
            }
        }

        public object Process(Page page)
        {
            bool matched = false;
            foreach(var regex in targetUrlRegexs)
            {
                if (regex.Match(page.GetUrl().ToString()).Success)
                    matched = true;
            }
            if (matched == false)
                return null;
            if (objectExtractor == null)
                return ProcessSingle(page, null, true);
            else
            {
                if(objectExtractor.IsMulti == true)
                {
                    var objects = new List<object>();
                    var list = objectExtractor.Selector.SelectList(page.GetRawText());
                    foreach(var str in list)
                    {
                        var o = ProcessSingle(page, str, false);
                        if (o != null)
                            objects.Add(o);
                    }
                    return objects;
                }
                else
                {
                    var select = objectExtractor.Selector.Select(page.GetRawText());
                    return ProcessSingle(page, select, false);
                }
            }
        }

        private object ProcessSingle(Page page, string html, bool isRaw)
        {
            object o = null;
            try
            {
                o = Activator.CreateInstance(type);
                foreach(var fieldExtractor in fieldExtractors)
                {
                    if(fieldExtractor.IsMulti)
                    {
                        List<string> values = fieldExtractor.Selector.SelectList(html);
                        switch(fieldExtractor.Source)
                        {
                            case Source.RawHtml:
                                values = page.GetHtml().SelectDocumentForList(fieldExtractor.Selector);
                                break;
                            case Source.Html:
                                if (isRaw)
                                    values = page.GetHtml().SelectDocumentForList(fieldExtractor.Selector);
                                else
                                    values = fieldExtractor.Selector.SelectList(html);
                                break;
                            case Source.Url:
                                values = fieldExtractor.Selector.SelectList(page.GetRawText());
                                break;
                            case Source.RawText:
                                break;
                        }
                        if ((values == null || values.Count == 0) && fieldExtractor.IsNotNull)
                            return null;
                        if (fieldExtractor.ObjectFormatter != null)
                        {
                            var converted = Convert(values, fieldExtractor.ObjectFormatter);
                            SetField(o, fieldExtractor, converted);
                        }
                        else
                            SetField(o, fieldExtractor, values);
                    }
                    else
                    {
                        string value = null;
                        switch(fieldExtractor.Source)
                        {
                            case Source.RawHtml:
                                value = page.GetHtml().SelectDocument(fieldExtractor.Selector);
                                break;
                            case Source.Html:
                                if (isRaw)
                                    value = page.GetHtml().SelectDocument(fieldExtractor.Selector);
                                else
                                    value = fieldExtractor.Selector.Select(html);
                                break;
                            case Source.Url:
                                value = fieldExtractor.Selector.Select(page.GetUrl().ToString());
                                break;
                            case Source.RawText:
                                value = fieldExtractor.Selector.Select(page.GetRawText());
                                break;
                        }
                        if (value == null && fieldExtractor.IsNotNull)
                            return null;
                        if (fieldExtractor.ObjectFormatter != null)
                        {
                            var converted = Convert(value, fieldExtractor.ObjectFormatter);
                            if (converted == null && fieldExtractor.IsNotNull)
                                return null;
                            SetField(o, fieldExtractor, converted);
                        }
                        else
                            SetField(o, fieldExtractor, value);
                    }
                }
                if(typeof(IAfterExtractor).IsAssignableFrom(type))
                {
                    ((IAfterExtractor)o).AfterProcess(page);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Extract fail: {ex}");
            }
            return o;
        }

        private object Convert(string value, IObjectFormatter objectFormatter)
        {
            try
            {
                var format = objectFormatter.Format(value);
                Debug.WriteLine($"String {value} is converted to {format}");
                return format;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

        private List<object> Convert(List<string> values, IObjectFormatter objectFormatter)
        {
            var objects = new List<object>();
            foreach(var value in values)
            {
                var converted = Convert(value, objectFormatter);
                if (converted != null)
                    objects.Add(converted);
            }
            return objects;
        }

        private void SetField(object obj, FieldExtractor fieldExtractor, object value)
        {
            if (value == null)
                return;
            if(fieldExtractor.SetterMethod != null)
            {
                fieldExtractor.SetterMethod.Invoke(obj, parameters: new object[] { value });
            }
            fieldExtractor.Field.SetValue(obj, value);
        }

        private void SetProperty(object obj, PropertyExtractor propertyExtractor, object value)
        {
            if (value == null)
                return;
            if(propertyExtractor.SetterMethod != null)
            {
                propertyExtractor.SetterMethod.Invoke(obj, parameters: new object[] { value });
            }
            propertyExtractor.Property.SetValue(obj, value);
        }

        public Type Type => type;

        public List<Regex> TargetUrlRegexs => targetUrlRegexs;

        public List<Regex> HelpUrlRegexs => helpUrlRegexs;

        public ISelector TargetUrlRegionSelector => targetUrlRegionSelector;

        public ISelector HelpUrlRegionSelector => helpUrlRegionSelector;


    }
}
