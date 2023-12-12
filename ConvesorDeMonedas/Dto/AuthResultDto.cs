namespace ConvesorDeMonedas.Dto
{
    public class AuthResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
