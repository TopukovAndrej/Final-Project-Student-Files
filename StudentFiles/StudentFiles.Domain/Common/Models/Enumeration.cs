namespace StudentFiles.Domain.Common.Models
{
    using System.Reflection;

    public abstract class Enumeration
    {
        public readonly byte Id;

        public readonly string Code;

        protected Enumeration(byte id, string code)
        {
            Id = id;
            Code = code;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration => typeof(T).GetFields(BindingFlags.Public |
                                                                                              BindingFlags.Static |
                                                                                              BindingFlags.DeclaredOnly)
                                                                                   .Select(f => f.GetValue(null))
                                                                                   .Cast<T>();

        public override string ToString() => Code;
    }
}
