using PeopleDictionary.Core.Enums;

namespace PeopleDictionary.Core.Base
{
    public class BaseModel<T>
    {
        public StatusCodeEnums StatusCode { get; set; }
        public int Code { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public BaseModel()
        {
            
        }

        public BaseModel(bool isSuccess, T? data, string? message)
        {
            IsSuccess = isSuccess;
            StatusCode = isSuccess ? StatusCodeEnums.Success : StatusCodeEnums.Fail;
            Code = (int)StatusCode;
            Data = data ?? default;
            Message = string.IsNullOrEmpty(message) ? string.Empty : message;
        }
    }
}
