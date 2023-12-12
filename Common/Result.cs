using System;

namespace Common
{
    public class Result<T> : Result
    {
        public Result(SolveFlag? flag, T value, string report = "") : base(flag, report)
        {
            this.Value = value;
        }
        public T Value { get; set; }
    }

    public  class Result
    {
        public SolveFlag? Flag { get; private set; } = SolveFlag.None;
        public string Report { get; private set; } = string.Empty;
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public Result() { }
        public Result(SolveFlag? flag, string report = "")
        {
            Flag = flag;
            Report = report;
        }

        public static Result NoneResult()
        {
            return new Result(SolveFlag.None);
        }

        public static Result OkResult()
        {
            return new Result();
        }

        public override string ToString()
        {
            return $"{CreatedTime?.ToLongTimeString()} | {Flag} | {Report}";
        }

    }

    public enum SolveFlag
    {
        OK = 0,
        Warning = 1,
        Error = 2,
        None = 99
    }

}
