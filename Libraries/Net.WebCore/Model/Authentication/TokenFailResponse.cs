namespace Net.WebCore.Model.Authentication
{
    sealed record TokenFailResponse
    {
        public string Message { get; set; } = "";
    }
}
