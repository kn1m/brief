namespace brief.Controllers.Models.BaseEntities
{
    public class ResponseMessage<T> : BaseResponseMessage
        where T : class 
    {
        public T Payload { get; set; }
    }
}
