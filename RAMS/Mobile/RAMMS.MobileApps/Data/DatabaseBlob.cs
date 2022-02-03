using Newtonsoft.Json;
using SQLite;
using System;
using System.Diagnostics;
using System.Reflection;

namespace RAMMS.MobileApps
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class TextBlobAttribute : IgnoreAttribute
    {
        public TextBlobAttribute(string textProperty)
        {
            TextProperty = textProperty;
        }

        public string TextProperty { get; private set; }
    }

    public static class TextBlobOperations
    {
        public static void GetTextBlobs(object element)
        {
            if (element == null)
                return;

            var type = element.GetType();
            foreach (var relationshipProperty in type.GetTextBlobProperties())
            {
                var relationshipAttribute = relationshipProperty.GetAttribute<TextBlobAttribute>();

                if (relationshipAttribute is TextBlobAttribute)
                {
                    TextBlobOperations.GetTextBlobChild(element, relationshipProperty);
                }
            }
        }

        private static void GetTextBlobChild(object element, PropertyInfo relationshipProperty)
        {
            if (element == null)
                return;

            var type = element.GetType();
            var relationshipType = relationshipProperty.PropertyType;

            Debug.Assert(relationshipType != typeof(string), "TextBlob property is already a string");

            var textblobAttribute = relationshipProperty.GetAttribute<TextBlobAttribute>();
            var textProperty = type.GetRuntimeProperty(textblobAttribute.TextProperty);
            Debug.Assert(textProperty != null && textProperty.PropertyType == typeof(string), "Text property for TextBlob relationship not found");

            var textValue = (string)textProperty.GetValue(element, null);
            var value = textValue != null ? JsonConvert.DeserializeObject(textValue, relationshipType) : null;

            relationshipProperty.SetValue(element, value, null);
        }

        public static void UpdateTextBlobs(object element)
        {
            if (element == null)
                return;

            var type = element.GetType();
            foreach (var relationshipProperty in type.GetTextBlobProperties())
            {
                var relationshipAttribute = relationshipProperty.GetAttribute<TextBlobAttribute>();

                if (relationshipAttribute is TextBlobAttribute)
                {
                    TextBlobOperations.UpdateTextBlobProperty(element, relationshipProperty);
                }
            }
        }

        private static void UpdateTextBlobProperty(object element, PropertyInfo relationshipProperty)
        {
            if (element == null)
                return;

            var type = element.GetType();
            var relationshipType = relationshipProperty.PropertyType;

            Debug.Assert(relationshipType != typeof(string), "TextBlob property is already a string");

            var textblobAttribute = relationshipProperty.GetAttribute<TextBlobAttribute>();
            var textProperty = type.GetRuntimeProperty(textblobAttribute.TextProperty);
            Debug.Assert(textProperty != null && textProperty.PropertyType == typeof(string), "Text property for TextBlob relationship not found");

            var value = relationshipProperty.GetValue(element, null);
            var textValue = value != null ? JsonConvert.SerializeObject(value) : null;

            textProperty.SetValue(element, textValue, null);
        }
    }
}