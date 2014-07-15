using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingExercises
{
	static class Program
	{
		private static readonly Dictionary<Type, Func<string, object>> parsers = new Dictionary<Type, Func<string, object>>
		{
			{typeof(string), str => str},
			{typeof(byte), str => byte.Parse(str)},
			{typeof(sbyte), str => sbyte.Parse(str)},
			{typeof(short), str => short.Parse(str)},
			{typeof(ushort), str => ushort.Parse(str)},
			{typeof(int), str => int.Parse(str)},
			{typeof(uint), str => uint.Parse(str)},
			{typeof(long), str => long.Parse(str)},
			{typeof(ulong), str => ulong.Parse(str)},
			{typeof(bool), str => bool.Parse(str)}
		};

		public static void Main(string[] args)
		{
			string input;
			while (!string.Equals(input = Console.ReadLine(), "quit"))
			{
				var parts = input.Split();
				var commandName = parts[0];
				var commandArgs = parts.Skip(1).Where(str => !string.IsNullOrWhiteSpace(str)).ToArray();
				try
				{
					Dispatch(commandName, commandArgs);
				}
				catch (Exception e)
				{
#if DEBUG
					Console.WriteLine(e);
#else
					Console.WriteLine("Unhandled error occurred: {0}.", e.Message);
#endif
				}
			}
		}

		private static void Dispatch(string commandName, string[] commandArgs)
		{
			var executeMethod = GetExecuteMethod(commandName);
			if(executeMethod == null)
				throw new ArgumentException(string.Format("No executable method found for command \"{0}\".", commandName));
			var parameters = ParseArguments(executeMethod.GetParameters(), commandArgs);
			executeMethod.Invoke(null, parameters);
		}

		private static MethodInfo GetExecuteMethod(string commandName)
		{
			return typeof(Program).Assembly.DefinedTypes
				.Where(type => type.IsClass)
				.Where(type => type.IsPublic)
				.Where(type => type.Name.Equals(commandName, StringComparison.InvariantCultureIgnoreCase))
				.Select(type => type.DeclaredMethods
					.Where(method => method.IsStatic)
					.Where(method => method.Name.Equals("Execute"))
					.First())
				.FirstOrDefault();
		}

		private static object[] ParseArguments(ParameterInfo[] parameters, string[] arguments)
		{
			return parameters.Select((param, index) =>
			{
				if (index >= arguments.Length)
				{
					if (param.IsOptional)
						return Type.Missing;
					else
						throw new ArgumentException(string.Format("Required parameter \"{0}\" is missing.", param.Name));
				}
				// Support 'params' array.
				if(param.GetCustomAttribute(typeof(ParamArrayAttribute)) != null)
				{
					var elementType = param.ParameterType.GetElementType();
					var array = Array.CreateInstance(elementType, arguments.Length - index);
					for(int i = index; i < arguments.Length; i++)
					{
						var val = parsers[elementType](arguments[i]);
						array.SetValue(val, i - index);
					}						
					return array;
				}
				return parsers[param.ParameterType](arguments[index]);
			}).ToArray();
		}
	}
}