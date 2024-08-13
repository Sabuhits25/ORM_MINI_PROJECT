namespace ORM_MINI_PROJECT.Exceptions
{
    public class OrderAlreadyCompletedException : Exception
    {
        public OrderAlreadyCompletedException(string message) : base(message) 
        {

        }
    }
}
