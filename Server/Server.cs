using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using Server.Models.Request;
using Server.Models.Response;

internal static class NamedServer
{
  internal static void StartServer()
  {
    Task.Factory.StartNew(() =>
    {
      using var pipeServer = new NamedPipeServerStream("test_pipe", PipeDirection.InOut);

      Console.WriteLine("Named Pipeサーバーが起動しました。");
      Console.WriteLine("クライアントの接続を待機しています...");

      pipeServer.WaitForConnection();
      Console.WriteLine("クライアントが接続しました。");

      try
      {
        using var reader = new StreamReader(pipeServer, Encoding.UTF8);
        using var writer = new StreamWriter(pipeServer, Encoding.UTF8);
        writer.AutoFlush = true;
        string? receivedJson;
        byte[] lengthBuffer = new byte[4];

        while ((receivedJson = reader.ReadLine()) is not null)
        {
          Console.WriteLine("受信メッセージ: " + receivedJson);
          var response = ProcessRequest(receivedJson);
          Console.WriteLine(response);
          var json = JsonSerializer.Serialize(response);
          Console.WriteLine("送信メッセージ: " + json);
          writer.WriteLine(json);
          Console.WriteLine("送信しました。");
        }
      }
      catch (IOException e)
      {
        Console.WriteLine("エラー: {0}", e.Message);
      }

    });
  }

  internal static object ProcessRequest(string json)
  {
    Console.WriteLine("リクエストを処理します。");
    Console.WriteLine("受信メッセージ: " + json);
    var request = JsonSerializer.Deserialize<RequestCreateChat>(json);
    Console.WriteLine(request);

    if (request is null)
    {
      throw new Exception("リクエストのデシリアライズに失敗しました。");
    }

    switch (request.Type)
    {
      case "CreateChat":
        var createChatRequest = JsonSerializer.Deserialize<RequestCreateChat>(json);
        return new ResponseCreateChat(true, createChatRequest.Chat);
      default:
        throw new Exception("不明なリクエストです。");
    }
  }

  internal static void Read(Stream stream, byte[] buffer, int offset, int count)
  {
    int read, totalRead = 0;
    while ((read = stream.Read(buffer, offset + totalRead, count - totalRead)) > 0)
    {
      totalRead += read;
    }
  }
}

