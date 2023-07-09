using System.Diagnostics.CodeAnalysis;

namespace ModelManagerServer.Models
{
    public readonly struct Option<T>
    {
        [MemberNotNullWhen(true, nameof(Value))]
        // TODO: How does this work if Value is a struct?
        public bool IsSome { get => this.Value is not null; }
        public bool IsNone { get => !this.IsSome; }

        private readonly T? Value;

        private Option(T value) { 
            this.Value = value; 
        }

        public static Option<T> Some(T value) => new(value);
        public static Option<T> None => new(default!);

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

        public Option<T> Map(Func<T, T> mapper)
        {
            return this.IsSome ? Option<T>.Some(mapper(this.Value)) : Option<T>.None;
        }

        public T MapOr(Func<T, T> mapper, T _default)
        {
            return this.IsSome ? mapper(this.Value) : _default;
        }

        public T MapOrElse(Func<T, T> mapper, Func<T> _default)
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
