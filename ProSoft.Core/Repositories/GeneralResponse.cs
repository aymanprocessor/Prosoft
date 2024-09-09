using ProSoft.EF.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories
{
    public class GeneralResponse<T> : IGeneralResponse<T>
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }

        // Constructor to initialize a successful response with data
        public GeneralResponse(T? data = default, string? message = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        // Constructor to initialize an unsuccessful response
        public GeneralResponse(string message)
        {
            Success = false;
            Message = message;
            Data = default;
        }
    }
}
