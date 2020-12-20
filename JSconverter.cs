using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Reflection;

namespace MathPanel
{
    public class JSconverter : JavaScriptConverter
    {
        /*public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            var instance = Activator.CreateInstance(type);

            foreach (var p in instance.GetType().GetProperties())//.GetPublicProperties())
            {
                instance.GetType().GetProperty(p.Name).SetValue(instance, dictionary[p.Name], null);
                dictionary.Remove(p.Name);
            }

            foreach (var item in dictionary)
                (instance).Add(item.Key, item.Value);

            return instance;
        }
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var result = new Dictionary<string, object>();
            var dictionary = obj as IDictionary<string, object>;
            foreach (var item in dictionary)
                result.Add(item.Key, item.Value);
            return result;
        }
        */
        //or
        public override object Deserialize(IDictionary<string, object> deserializedJSObjectDictionary, Type targetType, JavaScriptSerializer javaScriptSerializer)
        {
            Object targetTypeInstance = Activator.CreateInstance(targetType);

            FieldInfo[] targetTypeFields = targetType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            int iAdd = 0;
            foreach (FieldInfo fieldInfo in targetTypeFields)
            {
                fieldInfo.SetValue(targetTypeInstance, deserializedJSObjectDictionary[fieldInfo.Name]);
                iAdd++;
            }

            if (iAdd == 0 && targetType == typeof(Dictionary<string, string>))
            {
                var dic = targetTypeInstance as Dictionary<string, string>;
                foreach (var item in deserializedJSObjectDictionary)
                    dic.Add(item.Key, item.Value as string);
            }

            return targetTypeInstance;
        }

        public override IDictionary<string, object> Serialize(Object objectToSerialize, JavaScriptSerializer javaScriptSerializer)
        {
            IDictionary<string, object> serializedObjectDictionary = new Dictionary<string, object>();

            Type typ = objectToSerialize.GetType();
            FieldInfo[] objectToSerializeTypeFields = typ.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo fieldInfo in objectToSerializeTypeFields)
                serializedObjectDictionary.Add(fieldInfo.Name, fieldInfo.GetValue(objectToSerialize));

            if (serializedObjectDictionary.Count == 0 && typ == typeof(Dictionary<string, string>))
            {
                //serializedObjectDictionary.Add("a", "b");
                //serializedObjectDictionary.Add("cnt", (objectToSerialize as Dictionary<string, string>).Count);
                var dic = objectToSerialize as Dictionary<string, string>;
                foreach( var pair in dic )
                {
                    serializedObjectDictionary.Add(pair.Key, pair.Value);
                }
            }

            return serializedObjectDictionary;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new System.Collections.ObjectModel.ReadOnlyCollection<Type>(new Type[] { typeof(Dictionary<string, string>) });
            }
        }
    }
}
