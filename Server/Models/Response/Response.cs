namespace Server.Models.Response
{
  public abstract class Response
  {

    public Response(bool success)
    {
      Success = success;
    }

    public bool Success { get; }
  }
}
