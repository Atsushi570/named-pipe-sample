namespace Server.Models.Request
{
    public class RequestCreateChat : Request
    {

        public RequestCreateChat(Chat chat)
        {
            Chat = chat;
        }

        public override string Type => "CreateChat";
        public Chat Chat { get; set; }
    }
}
