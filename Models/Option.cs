using System.Diagnostics.CodeAnalysis;

namespace ModelManagerServer.Models
{
    public readonly struct Option<T>
    {
        [MemberNotNullWhen(true, nameof(Value))]
        public bool IsSome { get; }
        public bool IsNone { get => !this.IsSome; }

        private readonly T? Value;

        private Option(bool isSome, T? value)
        {
            this.IsSome = isSome;
            this.Value = value;
        }

        public Option(T value) {
            var type = typeof(T);
            this.Value = value;

            // See https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values
            if (type.IsEnum)
                // Enum
                this.IsSome = value == null;
            else if (type.IsValueType)
                // Struct
                this.IsSome = EqualityComparer<T>.Default.Equals(value, default);
            else
                // Class
                this.IsSome = value is not null;
        }

        public static Option<C> From<C>(C value)
            where C : class
        {
            return new Option<C>(value is not null, value!);
        }

        public static Option<S> FromStruct<S>(S value) 
            where S: struct
        {
            bool isSome = EqualityComparer<S>.Default.Equals(value, default);
            return new Option<S>(isSome, value);
        }

        public static Option<T> Some(T value) => new(true, value);
        public static Option<T> None => new(false, default!);

        public static implicit operator Option<T>(T value) => new(value);
        public static explicit operator T(Option<T> option)
        {
            if (!option.IsSome) 
                throw new ArgumentException("Tried getting Value from Option None Variant!");
            return option.Value;
        }

        public Option<T> Filter(Func<T, bool> predicate)
        {
            if (this.IsSome && !predicate(this.Value)) 
                return Option<T>.None;
            return this;
        }

        public T Get()
        {
            if (!this.IsSome)
                throw new ArgumentException("Tried getting Value from Option None Variant!");
            return this.Value;
        }

        public T GetOr(T _default)
        {
            return this.IsSome ? this.Value : _default;
        }

        public Option<O> Map<O>(Func<T, O> mapper)
        {
            return this.IsSome ? new Option<O>(mapper(this.Value)) : Option<O>.None;
        }

        public O MapOr<O>(Func<T, O> mapper, O _default)
        {
            return this.IsSome ? mapper(this.Value) : _default;
        }

        public O MapOrElse<O>(Func<T, O> mapper, Func<O> _default)
        {
            return this.IsSome ? mapper(this.Value) : _default();
        }

        public Option<(T, T1)> Zip<T1>(Option<T1> other)
        {
            if (this.IsSome && other.IsSome) 
                return Option<(T, T1)>.Some((this.Value, other.Value));
            return Option<(T, T1)>.None;
        }

        public Option<O> ZipWith<T2, O>(Option<T2> other, Func<T, T2, O> zipper)
        {
            var value = !(this.IsSome && other.IsSome) ? zipper(this.Value!, other.Value!) : default!;
            return new Option<O>(value);
        }

        // override object.Equals
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Option<T> opt) return false;
            return this == opt;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            if (this.IsSome) return this.Value.GetHashCode();
            return base.GetHashCode();
        }

        public static bool operator ==(Option<T> left, Option<T> right)
        {
            if (!(left.IsSome && right.IsSome)) return false;
            return EqualityComparer<T>.Default.Equals(left.Value, right.Value);
        }

        public static bool operator !=(Option<T> left, Option<T> right)
        {
            return !(left == right);
        }
    }

    public static class OptionExtensions
    {
        public static Option<T> Flatten<T>(this Option<Option<T>> option) {
            if (option.IsSome) return option.Get();
            return Option<T>.None;
        }

        public static (Option<T1>, Option<T2>) Unzip<T1, T2>(this Option<(T1, T2)> option)
        {
            if (option.IsSome)
            {
                var value = option.Get();
                return (Option<T1>.Some(value.Item1), Option<T2>.Some(value.Item2));
            }
            else
            {
                return (Option<T1>.None, Option<T2>.None);
            }
        }

        public static Result<T, E> OkOr<T, E>(this Option<T> option, E _default)
        {
            return option.IsSome ? Result<T, E>.Ok(option.Get()) : Result<T, E>.Error(_default);
        }

        public static Result<T, E> OkOrElse<T, E>(this Option<T> option, Func<E> _default)
        {
            return option.IsSome ? Result<T, E>.Ok(option.Get()) : Result<T, E>.Error(_default());
        }

        public static Result<T, E> ErrOr<T, E>(this Option<E> option, T _default)
        {
            return option.IsSome ? Result<T, E>.Error(option.Get()) : Result<T, E>.Ok(_default);
        }

        public static Result<T, E> ErrOrElse<T, E>(this Option<T> option, Func<T> _default)
        {
            return option.IsSome ? Result<T, E>.Ok(option.Get()) : Result<T, E>.Ok(_default());
        }
    }
}
