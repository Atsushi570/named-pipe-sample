using System.IO.Pipes;
using System.Text;
using System.Text.Json;
using Server.Models;
using Server.Models.Request;
using Server.Models.Response;
using Client;

var pipeClient = new NamedPipeClientStream(".", "test_pipe", PipeDirection.InOut, PipeOptions.None);
Console.WriteLine("サーバーへの接続を試みています...");
pipeClient.Connect();

Console.WriteLine("サーバーに接続しました。");
using var writer = new StreamWriter(pipeClient, Encoding.UTF8);
writer.AutoFlush = true;
using var reader = new StreamReader(pipeClient, Encoding.UTF8);
Console.WriteLine("メッセージを入力してください (終了するには 'exit' と入力)：");

var sender = new Sender();
string? message;
while ((message = Console.ReadLine()) is not null && message != "exit")
{
  if (message == "chat")
  {
    sender.SendChat(writer, reader);
  };

  // writer.WriteLine(message);

}
