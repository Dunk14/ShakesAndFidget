using EntityUtils.Reflection;
using EnumUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityUtils.Generator
{
    public class EntityGenerator<T> where T : class
    {
        private Reflectionner reflectionner;
        private Dictionary<String, Object> itemProperties;

        public EntityGenerator()
        {
            reflectionner = new Reflectionner();

            if (typeof(T).Name.Equals(TypeEnum.LIST))
            {
                var type = Type.GetType(typeof(List<T>).AssemblyQualifiedName);
                var list = (List<T>)Activator.CreateInstance(type);
                itemProperties = reflectionner.ReadClass<T>();
            }
            else
            {
                itemProperties = reflectionner.ReadClass<T>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inheritance"></param>
        /// <returns></returns>
        public T GenerateItem(Int32 inheritance = 2)
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            if (inheritance > 0)
            {
                inheritance--;

                foreach (var item in itemProperties)
                {
                    PropertyInfo property = typeof(T).GetProperty(item.Key);
                    if (property.CanWrite && property.GetSetMethod(/*nonPublic*/ true).IsPublic)
                    {
                        switch (property.GetStringValue())
                        {
                            case StringValueEnum.NAME:
                                property.SetValue(result, Faker.Name.FullName());
                                break;
                            case StringValueEnum.PASSWORD:
                                property.SetValue(result, Faker.Internet.Password(4, 6));
                                break;
                            default:

                                switch (property.PropertyType.Name)
                                {
                                    case TypeEnum.BOOL:
                                    case TypeEnum.BOOLEAN:
                                        Boolean value = false;
                                        int rand = Faker.RandomNumber.Next(100);
                                        if (rand % 2 == 0)
                                        {
                                            value = true;
                                        }
                                        property.SetValue(result, value);
                                        break;
                                    case TypeEnum.DECIMAL:
                                        property.SetValue(result, new decimal(Faker.RandomNumber.NextDouble() + Faker.RandomNumber.Next(Int32.MaxValue / 2)));
                                        break;
                                    case TypeEnum.DATETIME:
                                        property.SetValue(result, DateTime.FromBinary(Faker.RandomNumber.Next(Int32.MaxValue) * Faker.RandomNumber.Next(Int32.MaxValue)));
                                        break;
                                    case TypeEnum.INT32:
                                        property.SetValue(result, Faker.RandomNumber.Next(Int32.MaxValue));
                                        break;
                                    case TypeEnum.INT:
                                        property.SetValue(result, Faker.RandomNumber.Next(Int32.MaxValue));
                                        break;
                                    case TypeEnum.DOUBLE:
                                        property.SetValue(result, Faker.RandomNumber.NextDouble() * 10 * (Faker.RandomNumber.Next(32)));
                                        break;
                                    case TypeEnum.STRING:
                                        property.SetValue(result, Faker.Name.FullName());
                                        break;
                                    case TypeEnum.COLLECTION:
                                    case TypeEnum.LIST:
                                        object list = Activator.CreateInstance(
                                            typeof(List<>).MakeGenericType(property.PropertyType.GetGenericArguments()));
                                        property.SetValue(result, list);
                                        break;
                                    default:
                                        object generator = Activator.CreateInstance(typeof(EntityGenerator<>)
                                            .MakeGenericType(new Type[] { property.PropertyType }));
                                        property.SetValue(result, generator.GetType().GetMethod("GenerateItem").Invoke(generator, new object[] { 2 }));
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public IEnumerable<T> GenerateListItems()
        {
            List<T> result = (List<T>)Activator.CreateInstance(typeof(List<T>));
            for (int i = 0; i < Faker.RandomNumber.Next(0, 100); i++)
            {
                result.Add(GenerateItem());
            }
            return result;
        }
    }
}
