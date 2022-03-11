using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application
{
    public class ResultQC<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Result { get; private set; }    
        
        public ResultQC() { } //costruttore privato!       

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
