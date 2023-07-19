using System.Diagnostics.CodeAnalysis;

namespace ModelManagerServer.Models
{
    public readonly struct Result<T, E>
    {
        public bool IsOk { get; }
        public bool IsError { get => !this.IsOk; }

        private readonly object _value;

        private Result(bool isOk, object value)
        {
            this.IsOk = isOk;
            this._value = value;
        }

        public Result(T value) : this(true, value!) { }
        public Result(E error) : this(false, error!) { }

        public static Result<T, E> Ok(T value) => new(value);
        public static Result<T, E> Error(E error) => new(error);

        public static implicit operator Result<T, E>(T value) => new(value);
        public static implicit operator Result<T, E>(E error) => new(error);

        public static explicit operator T(Result<T, E> result) => result.Get();
        public static explicit operator E(Result<T, E> result) => result.GetError();

        public Result<TNew, E> Map<TNew>(Func<T, TNew> func)
        {
            return new Result<TNew, E>(this.IsOk, this.IsOk ? func((T)this._value)! : this._value);
        }

        public Result<T, ENew> MapError<ENew>(Func<E, ENew> func)
        {
            return new Result<T, ENew>(this.IsOk, this.IsError ? func((E)this._value)! : this._value);
        }

        public Result<T, ENew> MapOr<ENew>(T _default)
        {
            return this.IsOk ? (T)this._value : _default;
        }

        public Result<T, ENew> MapOrElse<ENew>(Func<E, T> _default)
        {
            return this.IsOk ? (T)this._value : _default((E)this._value);
        }

        public T Get()
        {
            if (!this.IsOk)
                throw new ArgumentException("Tried getting Ok Value from Error Result!");
            return (T)this._value;
        }

        public T GetOr(T _default)
        {
            return this.IsOk ? (T)this._value : _default;
        }

        public T GetOrElse(Func<E, T> _default)
        {
            return this.IsOk ? (T)this._value : _default((E)this._value);
        }

        public T GetOrDefault()
        {
            return this.IsOk ? (T)this._value : default!;
        }

        public E GetError()
        {
            if (this.IsOk)
                throw new ArgumentException("Tried getting Error Value from Ok Result!");
            return (E)this._value;
        }

        // override object.Equals
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Result<T, E> res) return false;
            return this == res;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        public static bool operator ==(Result<T, E> left, Result<T, E> right)
        {
            if (left.IsOk)
            {
                if (right.IsOk)
                    return EqualityComparer<T>.Default.Equals((T) left._value, (T) right._value);
            }
            else if (!right.IsOk)
            {
                return EqualityComparer<E>.Default.Equals((E) left._value, (E) right._value);
            }
            return false;
        }

        public static bool operator !=(Result<T, E> left, Result<T, E> right)
        {
            return !(left == right);
        }

        public void Deconstruct(out bool isOk, out T ok, out E error)
        {
            isOk = this.IsOk;
            ok = this.IsOk ? (T) this._value : default!;
            error = this.IsError ? (E) this._value : default!;
        }
    }

    public static class ResultExtensions {
        public static Result<T, E> Flatten<T, E>(this Result<Result<T, E>, E> result)
        {
            return result.IsOk ? result.Get() : Result<T, E>.Error(result.GetError());
        }

        public static Option<T> Some<T, E>(this Result<T, E> result) 
        {
            return result.IsOk ? Option<T>.Some(result.Get()) : Option<T>.None;
        }

        public static Option<E> SomeError<T, E>(this Result<T, E> result)
        {
            return !result.IsOk ? Option<E>.Some(result.GetError()) : Option<E>.None;
        }
    }
}
