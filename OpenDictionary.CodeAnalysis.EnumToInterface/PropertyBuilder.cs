using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;

namespace OpenDictionary.CodeAnalysis.EnumToInterface;

public readonly ref struct PropertyBuilder
{
    private static readonly string type = "string ";
    private static readonly string open = "{ ";
    private static readonly string get = "get;";
    private static readonly string close = "} ";

    public readonly ref struct Creator
    {
        public static Appender Create(IEnumerable<string> names, string accessor)
        {
            int length = GetLength(names, accessor);

            return new(length, accessor);
        }

        private static int GetLength(IEnumerable<string> names, string accessor) =>
            names.Sum(name => GetLength(name, accessor));
        private static int GetLength(string name, string accessor) =>
            type.Length + name.Length + open.Length +
            accessor.Length + close.Length + get.Length;
    }

    public readonly ref struct Appender(int length, string accessor)
    {
        private readonly StringBuilder builder = new(length);

        public void Append(string name)
        {
            builder.Append(type).Append(name).Append(open)
                   .Append(get).Append(accessor).Append(close);
        }

        public void Assert()
        {
            if (builder.Capacity != length || builder.Length != length)
            {
                throw new ArgumentException();
            }
        }

        public override string ToString()
        {
            return builder.ToString();
        }
    }
}