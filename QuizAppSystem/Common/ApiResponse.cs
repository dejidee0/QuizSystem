namespace QuizAppSystem.Common
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }
}
