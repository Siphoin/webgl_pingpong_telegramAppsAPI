[System.Serializable]
public class ArrayEmptryException : System.Exception
{
    public ArrayEmptryException() { }
    public ArrayEmptryException(string message) : base(message) { }
    public ArrayEmptryException(string message, System.Exception inner) : base(message, inner) { }
    protected ArrayEmptryException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}