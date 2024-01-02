namespace Server.Models
{
  public class Chat
  {
    public Chat(string id, string content)
    {
      Id = id;
      Content = content;
    }

    public string Id { get; set; }
    public string Content { get; set; }
  }
}
