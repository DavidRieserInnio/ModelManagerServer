namespace ModelManagerServer.Models
{
    public readonly struct Result<T, E>
    {
        public bool IsOk { get; }
        public bool IsError { get => !this.IsOk; }
        private readonly object Value;

        private Result(bool isOk, object value)
        {
            this.IsOk = isOk;
            this.Value = value;
        }

        public static Result<T, E> Ok(T value)
        {
            return new Result<T, E>(true, value!);
        }
        public static Result<T, E> Error(E value)
        {
            return new Result<T, E>(false, value!);
        }

        public static implicit operator Result<T, E>(T value)
        {
            return Ok(value);
        }

        public static implicit operator Result<T, E>(E error)
        {
            return Error(error);
        }

        public static explicit operator T(Result<T, E> result)
        {
            return result.Get();
        }

        public static explicit operator E(Result<T, E> result)
        {
            return result.TakeError();
        }

        public Result<TNew, E> Map<TNew>(Func<T, TNew> func)
        {
            return new Result<TNew, E>(this.IsOk, this.IsOk ? func((T)this.Value)! : this.Value);
        }

        public Result<T, ENew> MapError<ENew>(Func<E, ENew> func)
        {
            return new Result<T, ENew>(this.IsOk, this.IsError ? func((E)this.Value)! : this.Value);
        }

        public Result<T, ENew> MapOr<ENew>(T _default)
        {
            return this.IsOk ? (T)this.Value : _default;
        }

        public Result<T, ENew> MapOrElse<ENew>(Func<E, T> _default)
        {
            return this.IsOk ? (T)this.Value : _default((E)this.Value);
        }

        public T Get()
        {
            if (!this.IsOk)
                throw new ArgumentException("Tried getting Ok Value from Error Result!");
            return (T)this.Value;
        }

        public T GetOr(T _default)
        {
            return this.IsOk ? (T)this.Value : _default;
        }

        public T GetOrElse(Func<E, T> _default)
        {
            return this.IsOk ? (T)this.Value : _default((E)this.Value);
        }

        public T GetOrDefault()
        {
            return this.IsOk ? (T)this.Value : default!;
        }

        public E TakeError()
        {
            if (this.IsOk)
                throw new ArgumentException("Tried getting Error Value from Ok Result!");
            return (E)this.Value;
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
            return this.Value.GetHashCode();
        }

        public static bool operator ==(Result<T, E> left, Result<T, E> right)
        {
            if (left.IsOk != right.IsOk) return false;
            return left.Value == right.Value;
        }

        public static bool operator !=(Result<T, E> left, Result<T, E> right)
        {
            return !(left == right);
        }
    }

    public static class ResultExtensions {
        public static Result<T, E> Flatten<T, E>(this Result<Result<T, E>, E> result)
        {
            return result.IsOk ? result.Get() : Result<T, E>.Error(result.TakeError());
        }

        public static Option<T> Ok<T, E>(this Result<T, E> result) 
        {
            return result.IsOk ? Option<T>.Some(result.Get()) : Option<T>.None;
        }

        public static Option<E> Err<T, E>(this Result<T, E> result)
        {
            return !result.IsOk ? Option<E>.Some(result.TakeError()) : Option<E>.None;
        }
    }
}
