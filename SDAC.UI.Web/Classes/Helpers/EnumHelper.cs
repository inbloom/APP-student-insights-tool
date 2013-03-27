/*
 * Copyright 2012-2013 inBloom, Inc. and its affiliates.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SDAC.UI.Web.CustomAttributes;

namespace SDAC.UI.Web.Helpers
{
	/// <summary>
	/// Enumeration Helper Functionality
	/// </summary>
	public static class EnumHelper
	{
		public static string GetEnumAttributeValue<TAttribute>(this Enum value, Expression<Func<TAttribute, string>> attributeProperty) where TAttribute : Attribute
		{
			var enumType = value.GetType();
			var name = Enum.GetName(enumType, value);
			var attribute = enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().FirstOrDefault();
			
			if (attribute == null)
				throw new Exception("Invalid attribute");
			
			var func = attributeProperty.Compile();
			
			return func(attribute);
		}

		public static T FromDescription<T>(string description)
		{
			var type = typeof(T);
			if (!type.IsEnum) throw new InvalidOperationException();
			foreach (var field in type.GetFields())
			{
				var attribute = Attribute.GetCustomAttribute(field,
					typeof(DescriptionAttribute)) as DescriptionAttribute;
				if (attribute != null)
				{
					if (attribute.Description == description)
						return (T)field.GetValue(null);
				}
				else
				{
					if (field.Name == description)
						return (T)field.GetValue(null);
				}
			}

			throw new ArgumentException("Not found.", "description");
		}

		public static string ToDescription(this Enum en)
		{
			Type type = en.GetType();
			MemberInfo[] memInfo = type.GetMember(en.ToString());
			
			if (memInfo.Length > 0)
			{
				object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (attrs.Length > 0)
					return ((DescriptionAttribute)attrs[0]).Description;
			}

			return en.ToString();
		}

		public static TEnum GetEnumByAttribute<TEnum, TAttribute>(string valueToCompare, Expression<Func<TAttribute, string>> attributeProperty) where TAttribute : Attribute
		{
			TEnum tenum = default(TEnum);
			var func = attributeProperty.Compile();
			foreach (var field in typeof(TEnum).GetFields())
			{
				var attribute = field.GetCustomAttributes(false).OfType<TAttribute>().FirstOrDefault();
				if (attribute != null)
				{
					var value = func(attribute);
					if (value == valueToCompare)
					{
						tenum = (TEnum)field.GetValue(null);
						break;
					}
				}
			}
			return tenum;
		}
	}
}