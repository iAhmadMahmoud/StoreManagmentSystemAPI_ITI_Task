using System.Text.Json.Serialization;

namespace Store.Common
{
    public class GeneralResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, List<Error>>? Errors { get; set; }

        public static GeneralResult SuccessResult(string message = "Success")
            => new() { Success = true, Message = message , Errors = null};
        
        public static GeneralResult NotFoundResult(string message = "Not Found")
            => new() { Success = false, Message = message , Errors = null};
        
        public static GeneralResult FailResult(string message = "Operation Failed")
            => new() { Success = false, Message = message , Errors = null};
        
        public static GeneralResult FailResult(Dictionary<string,List<Error>> errors,string message = "One Or More Validation errors occurred.")
            => new() { Success = false, Message = message , Errors = errors};
    }

    public class GeneralResult<T> : GeneralResult
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public T? Data { get; set; }

        public static GeneralResult<T> SuccessResult(T data,string message = "Success")
            => new() { Success = true, Message = message, Errors = null,Data =data };
      
        public static new GeneralResult<T> SuccessResult(string message = "Success")
            => new() { Success = true, Message = message, Errors = null };
        
        public static new GeneralResult<T> NotFoundResult(string message = "Not Found")
            => new() { Success = false, Message = message, Errors = null};
        
        public static new GeneralResult<T> FailResult(string message = "Operation Failed")
            => new() { Success = false, Message = message, Errors = null};
        
        public static new GeneralResult<T> FailResult(Dictionary<string, List<Error>> errors, string message = "One Or More Validation errors occurred.")
            => new() { Success = false, Message = message, Errors = errors };
    }

}