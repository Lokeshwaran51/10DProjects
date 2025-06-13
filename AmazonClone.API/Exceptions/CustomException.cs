namespace AmazonClone.API.Exceptions
{
    public class CustomException:Exception
    {
        protected CustomException(){ }
        protected CustomException(string Message):base(Message) { }
        protected CustomException(string Message,Exception InnerException):base(Message,InnerException)
        {
            
        }

    }
}
