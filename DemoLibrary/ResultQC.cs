using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class ResultQC<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Result { get; private set; }
        public string[]? Errors { get; set; }
        public IList<KeyValuePair<string, string[]>> _errorMessages { get; set; }
        public ResultQC() { } //costruttore privato!

        public ResultQC(IList<KeyValuePair<string, string[]>> errors = null) {
            _errorMessages = errors ?? new List<KeyValuePair<string, string[]>>();

        }

        public static ResultQC<T> Success(T value)
        {
            return new ResultQC<T>() { Result = value, IsSuccess = true };
        }

        public static ResultQC<T> Success()
        {
            return new ResultQC<T>() {IsSuccess = true };
        }

        public static ResultQC<T> Failure(T value)
        {
            return new ResultQC<T> { Result = value, IsSuccess = false };
        }

    }
}
