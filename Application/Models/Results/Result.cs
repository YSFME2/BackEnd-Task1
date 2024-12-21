using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Results
{
    public class Result
    {
        public bool IsSucceeded { get; set; }
        public string? Error { get; set; }
    }

    public class Result<TValue> : Result
    {
        public TValue Value { get; set; }
    }
}
