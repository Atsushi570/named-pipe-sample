namespace Server.Models.Response
{
  public class ResponseCreateChat : Response
  {

    public ResponseCreateChat(bool success, Chat chat) : base(success)
    {
      Chat = chat;
    }

    public Chat Chat { get; }
  }
}
