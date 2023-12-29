using System.Diagnostics;

namespace CrudUi.Pages.Shared
{


    public class Result<T>
    {
        public Result(T value)
        {
            Value = value;
        }
        public Result(string error)
        {
            Error = error;
        }
        public Result(Exception ex)
        {
            Error = ex.Message;
        }
        public T? Value { get; }
        public string? Error { get; }
        public bool IsSuccess => Error is null;
        public bool IsFailure => Error is not null;
    }

    public class QueryResult<T>
    {

        public bool Result { get; set; }
        public string? Message { get; set; }
        public long? DurationInMs { get; set; }
        public T? Data
        {
            get => data; set
            {
                data = value;
                DurationInMs = (Stopwatch.GetTimestamp() - _start) / (Stopwatch.Frequency / 1000);
            }
        }

        private long _start;
        private T? data;

        public QueryResult()
        {
            Result = true;
            _start = Stopwatch.GetTimestamp();
        }

        public QueryResult(T data)
        {
            Result = true;
            Data = data;
        }

        public QueryResult(T data, int durationInMs)
        {
            Result = true;
            DurationInMs = durationInMs;

            Data = data;
        }

        public QueryResult(string message)
        {
            Result = false;
            Message = message;
        }

        public QueryResult(string message, int durationInMs)
        {
            Result = false;
            Message = message;
            DurationInMs = durationInMs;
        }
    }
}

namespace DataService.Shared.Entities
{
    public class QueryResult<T>
    {

        public bool Result { get; set; }
        public string? Message { get; set; }
        public long? DurationInMs { get; set; }
        public T? Data
        {
            get => data; set
            {
                data = value;
                DurationInMs = (Stopwatch.GetTimestamp() - _start) / (Stopwatch.Frequency / 1000);
            }
        }

        private long _start;
        private T? data;

        public QueryResult()
        {
            Result = true;
            _start = Stopwatch.GetTimestamp();
        }

        public QueryResult(T data)
        {
            Result = true;
            Data = data;
        }

        public QueryResult(T data, int durationInMs)
        {
            Result = true;
            DurationInMs = durationInMs;

            Data = data;
        }

        public QueryResult(string message)
        {
            Result = false;
            Message = message;
        }

        public QueryResult(string message, int durationInMs)
        {
            Result = false;
            Message = message;
            DurationInMs = durationInMs;
        }
    }
}
