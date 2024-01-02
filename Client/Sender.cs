using System.IO.Pipes;
using System.Text.Json;
using Server.Models;
using Server.Models.Request;
using Server.Models.Response;


namespace Client
{
  public class Sender
  {

    public void SendChat(StreamWriter writer, StreamReader reader)
    {
      try
      {
        var chat = new Chat("id", "content\r\ncontent");
        var request = new RequestCreateChat(chat);
        var json = JsonSerializer.Serialize(request);
        writer.WriteLine(json);
        Console.WriteLine("チャットを作成しました。");

        var responseJson = reader.ReadLine();
        if (responseJson is null)
        {
          throw new Exception("レスポンスの受信に失敗しました。");
        }
        var response = JsonSerializer.Deserialize<ResponseCreateChat>(responseJson);
        Console.WriteLine("受信メッセージ: " + response);
      }
      catch (Exception e)
      {
        Console.WriteLine("エラー: {0}", e.Message);
      }
    }
  }
}
